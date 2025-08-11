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
    public class ThrowRock : SkillBase
    {
        public override string Name => "Throw Rock";
        public override string Description => "Throws a rock or more in the mouse direction, inflicting also 70% of ranged damage on target hit.";
        public override SkillTypes SkillType => SkillTypes.Active;
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new DamageAttribute(),
            new ExtraRocksAttribute(),
            new Cooldown()
        };
        public override byte CooldownAttributeIndex => 2;

        public override void Update()
        {
            if (GetTime == 0)
            {
                int Rocks = (int)GetAttributeValue(1);
                int Damage = (int)GetAttributeValue(0) + GetBestDamage(GetCaster, DamageClass.Ranged, .7f);
                for (int i = 0; i < Rocks; i++)
                {
                    Vector2 FiringDirection = new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y) - GetCaster.MountedCenter;
                    if (i > 0)
                    {
                        FiringDirection.X += Main.rand.Next(-30, 30);
                        FiringDirection.Y += Main.rand.Next(-30, 30);
                    }
                    FiringDirection.Normalize();
                    Projectile.NewProjectile(Player.GetSource_None(), GetCaster.MountedCenter, FiringDirection * 8f, ProjectileID.Seed, Damage, 3f, GetCaster.whoAmI);
                }
            }
            if (GetTime >= 60)
            {
                EndUse();
            }
        }

        public class DamageAttribute : SkillAttribute
        {
            public override string Name => "Damage";
            public override string AttributeDescription(int Level)
            {
                return "Physical Damage " + Value(Level) + " inflicted.";
            }

            public override float Value(int Level)
            {
                return 2 + Level * 3;
            }

            public override int MaxLevel => 5;
        }

        public class ExtraRocksAttribute : SkillAttribute
        {
            public override string Name => "Rock Count";
            public override string AttributeDescription(int Level)
            {
                return "Throws " + (int)(Value(Level) + 1) + " rocks.";
            }
            public override int MaxLevel => 2;

            public override float Value(int Level)
            {
                return 1 + Level;
            }
        }

        class Cooldown : SkillAttribute
        {
            public override string Name => "Cooldown";
            public override string AttributeDescription(int Level)
            {
                return "Cooldown: " + Value(Level) + " seconds.";
            }
            public override int MaxLevel => 0;
            public override float Value(int Level)
            {
                return .2f;
            }
        }
    }
}