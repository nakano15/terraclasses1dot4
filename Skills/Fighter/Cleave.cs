using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Microsoft.CodeAnalysis;
using System;

namespace terraclasses1dot4.Skills.Fighter
{
    public class Cleave : SkillBase
    {
        public override string Name => "Cleave";
        public override string Description => "May retaliate attacks directed at you.";
        public override SkillTypes SkillType => SkillTypes.Passive;
        public override SkillData GetSkillData => new CleaveData();
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new Damage(),
            new CleaveRate(),
            new CritRate(),
            new AttackSize()
        };
        Vector2 Origin = new Vector2(17, 38);

        public override void Update()
        {
            CleaveData d = Data as CleaveData;
            if (d.IsCleaving)
            {
                d.UpdateCleavingTime();
                float Scale = d.GetSkillAttributeValue(3) * .01f;
                Rectangle rect = new Rectangle((int)Data.GetCaster.Center.X, (int)Data.GetCaster.Center.Y, (int)(34 * Scale), (int)(34 * Scale));
                rect.X += (int)(MathF.Sin(d.AttackAngle) * 20) - 17;
                rect.Y += (int)(MathF.Cos(d.AttackAngle) * 20) - 17;
                int Damage = (int)(GetBestDamage(Data.GetCaster, DamageClass.Melee) * (d.GetAttributeLevel(0) * .01f));
                //You forgot to get melee damage.
                for (int n = 0; n < 255; n++)
                {
                    if (n < 200 && Main.npc[n].active && !Main.npc[n].friendly && !Main.npc[n].dontTakeDamage && Main.npc[n].getRect().Intersects(rect))
                    {
                        d.HurtNpc(Main.npc[n], DamageClass.Melee, Damage, Data.GetCaster.Center.X < Main.npc[n].Center.X ? 1 : -1, 6f, 30);
                    }
                }
            }
        }

        public override void DrawInFrontOfPlayer(ref PlayerDrawSet drawInfo)
        {
            CleaveData d = Data as CleaveData;
            if (d.IsCleaving)
            {
                Texture2D texture = terraclasses.CleaveEffectTexture.Value;
                float Scale = Data.GetSkillAttributeValue(3) * .01f;
                DrawData dd = new DrawData(texture, drawInfo.Center - Main.screenPosition, new Rectangle(d.AnimationFrame * 34, 0, 34, 38), Color.White, d.AttackAngle, Origin, Scale, SpriteEffects.None, 0);
                drawInfo.DrawDataCache.Add(dd);
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (CanTriggerCleave())
            {
                (Data as CleaveData).SpawnCleaveDirection(Data.GetCaster.Center, npc.Center);
            }
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (CanTriggerCleave())
            {
                (Data as CleaveData).SpawnCleaveDirection(Data.GetCaster.Center, proj.Center);
            }
        }

        public bool CanTriggerCleave()
        {
            return Main.rand.Next(0, 100) < (Data as CleaveData).GetSkillAttributeValue(1);
        }

        class CleaveData : SkillData
        {
            public int AnimationTime = 0;
            public const int MaxAnimationTime = 14;
            public const float AttackDuration = 0.25f;
            public int AnimationFrame = 0;
            public bool IsCleaving = false;
            public float AttackAngle = 0f;

            public void UpdateCleavingTime()
            {
                AnimationTime++;
                AnimationFrame = (int)(AnimationTime * AttackDuration);
                if (AnimationTime >= MaxAnimationTime)
                {
                    IsCleaving = false;
                }
            }

            public void SpawnCleaveDirection(Vector2 CasterPosition, Vector2 TargetCenter)
            {
                if (IsCleaving) return;
                AttackAngle = (float)(Math.Atan2(TargetCenter.Y - CasterPosition.Y, TargetCenter.X - CasterPosition.X) + MathF.PI * .5f);
                AnimationFrame = 0;
                IsCleaving = true;
                AnimationTime = 0;
            }
        }

        class Damage : SkillAttribute
        {
            public override string Name => "Damage";
            public override int MaxLevel => 8;
            public override string AttributeDescription(int Level)
            {
                return "Cleave damage inflicts " + Value(Level) + "% Melee Damage.";
            }

            public override float Value(int Level)
            {
                return 80 + (14 * Level);
            }
        }

        class CritRate : SkillAttribute
        {
            public override string Name => "Critical Rate";
            public override int MaxLevel => 5;
            public override string AttributeDescription(int Level)
            {
                return "Critical Rate increases by " + Value(Level) + "%.";
            }

            public override float Value(int Level)
            {
                return (4 * Level);
            }
        }

        class AttackSize : SkillAttribute
        {
            public override string Name => "Cleave Scale";
            public override int MaxLevel => 7;
            public override string AttributeDescription(int Level)
            {
                return "Cleave Scale is " + Value(Level) + "%.";
            }

            public override float Value(int Level)
            {
                return 180 + Level * 16f;
            }
        }

        class CleaveRate : SkillAttribute
        {
            public override string Name => "Cleave Rate";
            public override int MaxLevel => 5;
            public override string AttributeDescription(int Level)
            {
                return "Chance of triggering cleave when hit: " + Value(Level) + "%.";
            }

            public override float Value(int Level)
            {
                return 15f + 4.2f * Level;
            }
        }
    }
}