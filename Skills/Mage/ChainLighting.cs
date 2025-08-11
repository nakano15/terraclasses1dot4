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
    public class ChainLightning : SkillBase
    {
        public override string Name => "Chain Lightning";
        public override string Description => "Release Lightning Bolts that target one enemy after another dealing magic damage.";
        protected override SkillAttribute[] SetSkillAttributes => new SkillAttribute[]{

        };
        public override SkillTypes SkillType => SkillTypes.Active;
        public override byte CooldownAttributeIndex => base.CooldownAttributeIndex;

        public override void Update()
        {
            
        }


    }
}