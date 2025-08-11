using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System;

namespace terraclasses1dot4.DrawLayers
{
    public class CastBarLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new BeforeParent(PlayerDrawLayers.Tails);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            float CastTime = drawInfo.drawPlayer.GetModPlayer<PlayerMod>().CastBarPercentage;
            if (CastTime > 0f)
            {
                Vector2 Position = drawInfo.Position + new Vector2((int)((drawInfo.drawPlayer.width - 64 * 2) * .5f), drawInfo.drawPlayer.height - 72f) - Main.screenPosition;
                Texture2D cBarTexture = terraclasses.CastBarTexture.Value;
                drawInfo.DrawDataCache.Add(new DrawData(cBarTexture, Position, new Rectangle(0, 0, 64, 8), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None));
                drawInfo.DrawDataCache.Add(new DrawData(cBarTexture, Position + Vector2.One * 2, new Rectangle(1, 9, (int)(62 * Math.Clamp(CastTime, 0f, 1f)), 6), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None));
            }
        }
    }
}