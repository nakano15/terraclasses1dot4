using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Microsoft.CodeAnalysis;

namespace terraclasses.Skills.Fighter
{
    public class Endure : SkillBase
    {
        public override string Name => "Endure";
        public override string Description => "Increases defense rate against attacks for a limited amount of time.";
        public override SkillTypes SkillType => SkillTypes.Active;
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new DamageReduction(),
            new KnockbackResist(),
            new SkillDuration()
        };

        public override void Update(SkillData Data)
        {
            if (Data.GetTime >= Data.GetSkillAttributeValue(2))
            {
                Data.EndUse();
            }
        }

        public override void UpdateStatus(SkillData Data)
        {
            if (Data.GetSkillAttributeValue(1) > 0)
                Data.GetCaster.noKnockback = true;
        }

        public override void ModifyHitByNPC(SkillData data, NPC npc, ref Player.HurtModifiers modifiers)
        {
            float DR = 1f - data.GetSkillAttributeValue(0);
            modifiers.FinalDamage *= DR;
        }

        public override void ModifyHitByProjectile(SkillData data, Projectile proj, ref Player.HurtModifiers modifiers)
        {
            float DR = 1f - data.GetSkillAttributeValue(0);
            modifiers.FinalDamage *= DR;
        }

        class DamageReduction : SkillAttribute
        {
            public override string Name => "Damage Reduction";
            public override int MaxLevel => 7;
            public override string AttributeDescription(int Level)
            {
                return "Reduces damage received by " + Value(Level) + "%.";
            }

            public override float Value(int Level)
            {
                return 3 + 3 * Level;
            }
        }

        class KnockbackResist : SkillAttribute
        {
            public override string Name => "Knockback Immunity";
            public override int MaxLevel => 1;
            public override string AttributeDescription(int Level)
            {
                if (Level == 0)
                    return "No Knockback Immunity given.";
                return "Get Knockback Immunity.";
            }
        }

        class SkillDuration : SkillAttribute
        {
            public override string Name => "Duration";
            public override int MaxLevel => 5;
            public override string AttributeDescription(int Level)
            {
                return "Skill lasts for " + Value(Level) + " seconds.";
            }
            public override float Value(int Level)
            {
                return 30 + Level * 15;
            }
        }
    }
}