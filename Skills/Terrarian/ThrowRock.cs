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
        public override string Description => "";
        public override SkillTypes SkillType => SkillTypes.Active;
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]
        {
            new DamageAttribute()
        };
        public override int Cooldown => GetFrameFromTime(10);

        public override void Update()
        {
            if (GetTime == 0)
            {
                Vector2 FiringDirection = new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y) - GetCaster.MountedCenter;
                FiringDirection.Normalize();
                Projectile.NewProjectile(Player.GetSource_None(), GetCaster.MountedCenter, FiringDirection * 8f, ProjectileID.Seed, (int)GetAttributeValue(0), 3f, GetCaster.whoAmI);
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
    }
}