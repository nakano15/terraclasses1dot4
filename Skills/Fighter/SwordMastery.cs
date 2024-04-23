using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace terraclasses.Skills.Fighter
{
    public class SwordMastery : SkillBase
    {
        public override string Name => "Sword Mastery";
        public override string Description => "Improves your performance when using Sword and Dagger type weapons.";
        public override SkillTypes SkillType => SkillTypes.Passive;
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new IncreaseMeleeDamage(),
            new IncreaseAttackSpeed(),
            new IncreaseCriticalRate()
        };

        public override void GetSkillIcon(out Texture2D Texture, out Rectangle Rect)
        {
            base.GetSkillIcon(out Texture, out Rect);
            Rect.X = Rect.Width;
        }

        public override void UpdateStatus(SkillData Data)
        {
            Player p = Data.GetCaster;
            ItemTypes Type = ItemMod.GetItemType(p.HeldItem);
            //if (Type == ItemTypes.Sword || Type == ItemTypes.Dagger)
            {
                p.GetDamage<MeleeDamageClass>() += Data.GetSkillAttributeValue(0);
                p.GetAttackSpeed<MeleeDamageClass>() += Data.GetSkillAttributeValue(1);
                p.GetCritChance<MeleeDamageClass>() += Data.GetSkillAttributeValue(2);
            }
        }

        public class IncreaseAttackSpeed : SkillAttribute
        {
            public override string Name => "Attack Speed";
            public override int MaxLevel => 10;
            public override int PointsUsed => 1;
            
            public override string AttributeDescription(int Level)
            {
                float Percentage = Value(Level) * 100;
                return "Increases Attack Speed by " + ((int)Percentage) + "%.";
            }
            public override float Value(int Level)
            {
                return Level * .01f;
            }
        }

        public class IncreaseMeleeDamage : SkillAttribute
        {
            public override string Name => "Damage";
            public override string AttributeDescription(int Level)
            {
                float Percentage = Value(Level) * 100f;
                return "Increases Melee Damage by " + ((int)Percentage) + "%";
            }
            public override int PointsUsed => 1;
            public override int MaxLevel => 10;

            public override float Value(int Level)
            {
                return Level * .015f;
            }
        }

        public class IncreaseCriticalRate : SkillAttribute
        {
            public override string Name => "Critical Rate";
            public override string AttributeDescription(int Level)
            {
                float Percentage = Value(Level);
                return "Increases Critical Rate by " + ((int)Percentage) + "%";
            }
            public override int PointsUsed => 1;
            public override int MaxLevel => 5;

            public override float Value(int Level)
            {
                return (int)(Level * 1.4f);
            }
        }
    }
}