using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace terraclasses.DrawLayers
{
    public class SkillDrawLayers
    {
        public class SkillDrawBackLayer : PlayerDrawLayer
        {
            public override Position GetDefaultPosition()
            {
                return new BeforeParent(PlayerDrawLayers.Tails);
            }

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                foreach (SkillData sd in drawInfo.drawPlayer.GetModPlayer<PlayerMod>().GetActiveSkills)
                {
                    sd.Base.DrawBehindPlayer(sd, ref drawInfo);
                }
            }
        }
        public class SkillDrawFrontLayer : PlayerDrawLayer
        {
            public override Position GetDefaultPosition()
            {
                return new AfterParent(PlayerDrawLayers.ArmOverItem);
            }

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                foreach (SkillData sd in drawInfo.drawPlayer.GetModPlayer<PlayerMod>().GetActiveSkills)
                {
                    sd.Base.DrawInFrontOfPlayer(sd, ref drawInfo);
                }
            }
        }
    }
}