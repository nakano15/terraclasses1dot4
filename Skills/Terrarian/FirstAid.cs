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
            new DebuffRemoval()
        };
        public override int Cooldown => GetFrameFromTime(30);

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
    }
}