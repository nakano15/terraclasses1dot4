using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace terraclasses.Skills.Cerberus
{
    public class CerberusHeadSkill : SkillBase
    {
        const int ATTRIBUTE_DAMAGE = 0, ATTRIBUTE_RANGE = 1, ATTRIBUTE_AMOUNT = 2, ATTRIBUTE_BURN = 3;

        public override string Name => "Cerberus Head";
        public override string Description => "Empowers Cerberus Head.";
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new Damage(), new Range(), new Amount(), new OnFireInflictRate()
        };
        public override SkillData GetSkillData => new CerberusBiteData();

        public override void Update(SkillData Data)
        {
            CerberusBiteData data = (CerberusBiteData) Data;
            for (int h = 0; h < data.Heads.Count; h++)
            {
                UpdateHead(data, data.Heads[h], h);
            }
        }

        void UpdateHead(CerberusBiteData data, CerberusBiteData.CerberusHead head, int HeadIndex)
        {
            head.AttackTime--;
            if (head.AttackTime == 10)
            {
                //Dmg
                Vector2 BiteOrientation = new Vector2((float)Math.Cos(head.Rotation), (float)Math.Sin(head.Rotation));
                for (int x = 0; x < 12; x++)
                {
                    Vector2 SpawnPosition = BiteOrientation * Main.rand.Next(-14, 15) * head.Scale + head.Position;
                    Dust.NewDust(SpawnPosition, 4, 4, Terraria.ID.DustID.Torch);
                }
                Rectangle HitPosition = new Rectangle((int)head.Position.X - (int)(16 * head.Scale), (int)head.Position.Y - (int)(16 * head.Scale), (int)(32 * head.Scale), (int)(32 * head.Scale));
                float DamagePercentage = head.DamagePower;
                for (int n = 0; n < 255; n++)
                {
                    if (n < 200 && Main.npc[n].active && !Main.npc[n].friendly && !Main.npc[n].dontTakeDamage && Main.npc[n].getRect().Intersects(HitPosition))
                    {
                        if (data.HurtNpc(Main.npc[n], DamageClass.Melee, DamagePercentage, 0, 0) > 0 && Main.rand.Next(head.OnFireDebuffRate) == 0)
                        {
                            Main.npc[n].AddBuff(BuffID.Burning, 7 * 60);
                        }
                    }
                }
            }
            if (head.AttackTime == 10 && head.BiteTimes > 0)
                data.ResetBite(head);
            if (head.AttackTime <= 0)
            {
                data.Heads.RemoveAt(HeadIndex);
            }
        }

        public override void OnHitNPC(SkillData Data, NPC target, NPC.HitInfo hit, int damageDone)
        {
            TrySpawningOnNPC(Data, target);
        }

        public override void OnHitNPCWithProj(SkillData data, Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            TrySpawningOnNPC(data, target);
        }

        void TrySpawningOnNPC(SkillData Skill, NPC TargetNpc)
        {
            CerberusBiteData data = (CerberusBiteData)Skill;
            Vector2 Position = TargetNpc.Center + TargetNpc.velocity * 5;
            if (Main.rand.NextFloat() < (1f - data.Heads.Count * 0.334f) * 0.1f && CanCreateHead(Position, data))
            {
                data.AddCerberusHead(Position);
            }
        }

        public bool CanCreateHead(Vector2 Position, CerberusBiteData Data)
        {
            foreach (CerberusBiteData.CerberusHead head in Data.Heads)
            {
                if (Math.Abs(head.Position.X - Position.X) < 32 ||
                    Math.Abs(head.Position.Y - Position.Y) < 32)
                    return false;
            }
            return true;
        }

        public override void DrawBehindPlayer(SkillData Data, ref PlayerDrawSet drawInfo)
        {
            CerberusBiteData data = (CerberusBiteData)Data;
            int PosSum = 0;
            Texture2D HeadTexture = terraclasses.CerberusHeadTexture.Value;
            foreach (CerberusBiteData.CerberusHead head in data.Heads)
            {
                float Opacity = 1f;
                if (head.BiteTimes == 0 && head.AttackTime < 10)
                    Opacity = head.AttackTime * 0.1f;
                else if (!head.IsResetBite && head.AttackTime > head.MaxAttackTime - 10)
                {
                    Opacity = 1f - (head.AttackTime - (head.MaxAttackTime - 10)) * 0.1f;
                }
                Vector2 UpperMouthOrigin = new Vector2(15, 22) * 2,
                    LowerMouthOrigin = new Vector2(15, 22) * 2;
                float MouthMovementPercentage = 1f;
                if (head.AttackTime < 10)
                    MouthMovementPercentage = 0;
                else if (head.AttackTime >= 10 && head.AttackTime < 20)
                {
                    MouthMovementPercentage = (float)(head.AttackTime - 10) / 10;
                }
                else if(head.IsResetBite && head.AttackTime > head.MaxAttackTime - 10)
                {
                    MouthMovementPercentage = 1f - (float)(head.AttackTime - (head.MaxAttackTime - 10)) / 10;
                }
                float PixelMovementOfMouth = 8;
                if (head.MouthFull)
                    PixelMovementOfMouth = 2;
                Vector2 MouthMoveOrientationByRotation = Vector2.UnitY * PixelMovementOfMouth * MouthMovementPercentage; //new Vector2((float)Math.Sin(data.Rotation), (float)Math.Cos(data.Rotation))
                UpperMouthOrigin += MouthMoveOrientationByRotation;
                LowerMouthOrigin -= MouthMoveOrientationByRotation;
                int FrameX = head.MouthFull ? 64 : 0;
                Terraria.DataStructures.DrawData dd = new Terraria.DataStructures.DrawData(HeadTexture, head.Position - Main.screenPosition,
                    new Rectangle(FrameX, 0, 64, 64), Color.White * Opacity, head.Rotation, LowerMouthOrigin, head.Scale, SpriteEffects.None, 0);
                dd.ignorePlayerRotation = true;
                drawInfo.DrawDataCache.Insert(0 + PosSum * 2, dd);
                dd = new Terraria.DataStructures.DrawData(HeadTexture, head.Position - Main.screenPosition,
                     new Rectangle(FrameX, 64, 64, 64), Color.White * Opacity, head.Rotation, UpperMouthOrigin, head.Scale, SpriteEffects.None, 0);
                dd.ignorePlayerRotation = true;
                drawInfo.DrawDataCache.Insert(1 + PosSum * 2, dd);
                PosSum++;
            }
        }

        public class CerberusBiteData : SkillData
        {
            public List<CerberusHead> Heads = new List<CerberusHead>();
            public Dictionary<int, int> ShroudCooldown = new Dictionary<int, int>();

            public CerberusHead AddCerberusHead(Vector2 Position)
            {
                CerberusHead head = new CerberusHead();
                head.DamagePower = GetSkillAttributeValue(ATTRIBUTE_DAMAGE);
                head.BiteTimes = (byte)GetSkillAttributeValue(ATTRIBUTE_AMOUNT);
                head.AttackTime = head.MaxAttackTime = 40;
                head.Position = Position;
                head.Rotation = (Main.rand.NextFloat() - 0.5f) * (float)Math.PI * 0.5f;
                head.Scale = GetSkillAttributeValue(ATTRIBUTE_RANGE);
                head.OnFireDebuffRate = (int)MathF.Max(1, GetSkillAttributeValue(ATTRIBUTE_BURN));
                Heads.Add(head);
                return head;
            }

            public void ResetBite(CerberusHead head)
            {
                head.BiteTimes--;
                head.IsResetBite = true;
                head.AttackTime = head.MaxAttackTime;
                head.Rotation = (Main.rand.NextFloat() - 0.5f) * (float)Math.PI * 0.5f;
            }

            public bool IsTargetSmallForMouth(CerberusHead head, int Width, int Height)
            {
                float HeadDimension = 32f * head.Scale;
                return Width < HeadDimension && Height < HeadDimension;
            }

            public class CerberusHead
            {
                public int AttackTime = 0, MaxAttackTime = 0;
                public byte BiteTimes = 0;
                public float DamagePower = 1;
                public Vector2 Position = Vector2.Zero;
                public float Rotation = 0f, Scale = 1f;
                public int OnFireDebuffRate = 3;
                public bool IsResetBite = false;

                public bool MouthFull
                {
                    get
                    {
                        return false;
                    }
                }
            }
        }

        class Damage : SkillAttribute
        {
            public override string AttributeDescription(int Level)
            {
                return "Inflicts " + (int)(Value(Level) * 100) + "% Melee Damage.";
            }

            public override int MaxLevel => 10;

            public override float Value(int Level)
            {
                return .67f + .23f * Level;
            }
        }

        class Range : SkillAttribute
        {
            public override string AttributeDescription(int Level)
            {
                return "Head Size: " + (int)(Value(Level) * 100) + "%.";
            }

            public override int MaxLevel => 10;

            public override float Value(int Level)
            {
                return 1.35f + .19f * Level;
            }
        }

        class Amount : SkillAttribute
        {
            public override string AttributeDescription(int Level)
            {
                return "Bites " + (int)(Value(Level)) + " times.";
            }

            public override int MaxLevel => 10;

            public override float Value(int Level)
            {
                return 3 + (int)(Level * .33f);
            }
        }

        class OnFireInflictRate : SkillAttribute
        {
            public override string AttributeDescription(int Level)
            {
                return "Has 1 in " + (int)(Value(Level)) + " chance of inflicting On Fire! debuff.";
            }

            public override int MaxLevel => 2;

            public override float Value(int Level)
            {
                return 3 - Level;
            }
        }
    }
}