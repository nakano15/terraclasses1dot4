using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Microsoft.CodeAnalysis;
using System;

namespace terraclasses1dot4.Skills.Mage
{
    public class MagicMastery : SkillBase
    {
        public override string Name => "Magic Mastery";
        public override string Description => "Improves your control over magic.";
        public override SkillTypes SkillType => SkillTypes.Passive;
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new Damage(),
            new ManaCost(),
            new ManaRegen()
        };

        public override void UpdateStatus()
        {
            GetCaster.GetDamage<MagicDamageClass>() *= GetAttributeValue(0) * .01f;
            GetCaster.manaCost -= (100 - GetAttributeLevel(1)) * .01f;
            GetCaster.manaRegen += (int)GetAttributeLevel(2);
        }

        class Damage : SkillAttribute
        {
            public override string Name => "Magic Damage Change";
            public override string AttributeDescription(int Level)
            {
                return "Magic Damage is " + (int)Value(Level) + "% strong.";
            }
            public override int MaxLevel => 6;
            public override float Value(int Level)
            {
                return 100f + Level * 6f;
            }
        }

        class ManaCost : SkillAttribute
        {
            public override string Name => "Mana Cost Change";
            public override string AttributeDescription(int Level)
            {
                return "Magic Usage costs " + (int)Value(Level) + "% of Mana.";
            }
            public override int MaxLevel => 5;
            public override float Value(int Level)
            {
                return 100f - 7f * Level;
            }
        }

        class ManaRegen : SkillAttribute
        {
            public override string Name => "Mana Regeneration";
            public override string AttributeDescription(int Level)
            {
                return "Increases Mana Regeneartion by " + Value(Level) + ".";
            }
            public override int MaxLevel => 3;
            public override float Value(int Level)
            {
                return Level;
            }
        }
    }
}