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
    public class Fireball : SkillBase
    {
        public override string Name => "Fireball";
        public override string Description => "Launches Fireballs at the mouse direction.";
        public override SkillTypes SkillType => SkillTypes.Active;
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new Damage(),
            new BoltCount(),
            new CastTime(),
            new ManaCost(),
            new Cooldown()
        };
        public override byte CastTimeAttributeIndex => 2;
        public override byte ManaCostAttributeIndex => 3;
        public override byte CooldownAttributeIndex => 4;

        public override void Update()
        {
            if (IsStepStart)
            {
                Projectile.NewProjectile(Player.GetSource_None(), GetCaster.MountedCenter, GetAimedVelocity(GetCaster.MountedCenter, GetMousePosition) * 8f, ProjectileID.Flamelash, GetBestDamage(GetCaster, DamageClass.Magic, GetAttributeValue(0) * .01f), 3f, GetCaster.whoAmI);
            }
            if (GetTime >= 14)
            {
                if (Data.GetStep >= GetAttributeValue(1))
                {
                    EndUse();
                }
                else
                {
                    ChangeStep();
                }
            }
        }

        class Damage : SkillAttribute
        {
            public override string Name => "Spell Damage";
            public override int MaxLevel => 6;
            public override string AttributeDescription(int Level)
            {
                return "Each bolt inflicts " + (int)Value(Level) + "% of Magic Damage.";
            }
            public override float Value(int Level)
            {
                return 85f + Level * 6.5f;
            }
        }

        class BoltCount : SkillAttribute
        {
            public override string Name => "Fire Balls Count";
            public override int MaxLevel => 7;
            public override string AttributeDescription(int Level)
            {
                return "Fires " + (int)Value(Level) + " consecutive Fire Balls.";
            }
            public override float Value(int Level)
            {
                return 1 + 2 * Level;
            }
        }

        class CastTime : SkillAttribute
        {
            public override string Name => "Cast Time";
            public override int MaxLevel => 0;
            public override string AttributeDescription(int Level)
            {
                return "Spell Cast Time is " + Value(Level) + " seconds.";
            }
            public override float Value(int Level)
            {
                return 3;
            }
        }

        class ManaCost : SkillAttribute
        {
            public override string Name => "Mana Cost";
            public override string AttributeDescription(int Level)
            {
                return "Casting this spell costs " + Value(Level) + " Mana.";
            }
            public override int MaxLevel => 3;
            public override float Value(int Level)
            {
                return 14 - Level * 2;
            }
        }

        class Cooldown : SkillAttribute
        {
            public override string Name => "Cooldown";
            public override string AttributeDescription(int Level)
            {
                return "Spell Cooldown is " + Value(Level) + " seconds.";
            }
            public override int MaxLevel => 5;
            public override float Value(int Level)
            {
                return 28 - 4 * Level;
            }
        }
    }
}