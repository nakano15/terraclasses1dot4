using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Microsoft.CodeAnalysis;
using System;

namespace terraclasses1dot4.Skills.Terrarian
{
    public class FirstAid : SkillBase
    {
        public override string Name => "First Aid";
        public override string Description => "Allows the character to recover some health, and also maybe remove some bad effects.";
        public override SkillTypes SkillType => SkillTypes.Active;
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new HealingPower(),
            new DebuffRemoval(),
            new ManaCost(),
            new Cooldown()
        };
        public override byte ManaCostAttributeIndex => 2;
        public override byte CooldownAttributeIndex => 3;

        public override void Update()
        {
            if (GetTime == 30)
            {
                int Heal = (int)GetAttributeValue(0);
                GetCaster.Heal(Heal);
                int DebuffsToRemove = (int)GetAttributeValue(1);
                if (DebuffsToRemove >= 1)
                {
                    GetCaster.ClearBuff(BuffID.Bleeding);
                }
                if (DebuffsToRemove >= 2)
                {
                    GetCaster.ClearBuff(BuffID.Poisoned);
                }
                if (DebuffsToRemove >= 3)
                {
                    GetCaster.ClearBuff(BuffID.OnFire);
                }
            }
            if (GetTime >= 60)
            {
                EndUse();
            }
        }

        public class HealingPower : SkillAttribute
        {
            public override string Name => "Healing Power";
            public override int MaxLevel => 5;

            public override string AttributeDescription(int Level)
            {
                return "Recovers " + Value(Level) + " Health on use.";
            }

            public override float Value(int Level)
            {
                return Level * 5;
            }
        }

        public class DebuffRemoval : SkillAttribute
        {
            public override string Name => "Debuff Removal";
            public override int MaxLevel => 3;

            public override string AttributeDescription(int Level)
            {
                return "Removes debuffs based on level.\nLevel 1 removes Bleeding.\nLevel 2 removes Poisoned.\nLevel 3 removes On Fire!";
            }

            public override float Value(int Level)
            {
                return Level;
            }
        }

        public class ManaCost : SkillAttribute
        {
            public override string Name => "Mana Cost";
            public override string AttributeDescription(int Level)
            {
                return "Costs " + Value(Level) + " Mana to use.";
            }
            public override int MaxLevel => 0;
            public override float Value(int Level)
            {
                return 3;
            }
        }

        class Cooldown : SkillAttribute
        {
            public override string Name => "Cooldown";
            public override string AttributeDescription(int Level)
            {
                return "Reusable after " + Value(Level) + " seconds.";
            }
            public override int MaxLevel => 0;
            public override float Value(int Level)
            {
                return .5f;
            }
        }
    }
}