using Terraria;
using Terraria.UI;
using Terraria.GameContent;
using Terraria.UI.Chat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ModLoader;
using ReLogic.Graphics;

namespace terraclasses.Interface
{
    public class DrawClassLevelProgress : LegacyGameInterfaceLayer
    {
        static int LastExp = 0, LastMaxExp = 0;
        static float LastExpPercentage = 0;
        static bool LastInterfaceOpened = false;

        public DrawClassLevelProgress() : base("TerraClasses: Class Level Progress", DrawSelf, InterfaceScaleType.UI)
        {

        }
        
        new protected static bool DrawSelf()
        {
            ClassData LastClass = Main.player[Main.myPlayer].GetModPlayer<PlayerMod>().GetLastClass;
            if (LastExp != LastClass.GetExp || LastMaxExp != LastClass.GetMaxExp)
            {
                LastExp = LastClass.GetExp;
                LastMaxExp = LastClass.GetMaxExp;
                LastExpPercentage = (float)LastExp / LastMaxExp;
            }
            Vector2 DrawPosition = new Vector2(Main.screenWidth, Main.screenHeight - 40);
            if (Main.playerInventory)
                DrawPosition.X -= 228;
            bool Maxed = LastClass.IsMastered;
            bool IsMouseOverExpBar = !Maxed && Main.mouseX >= DrawPosition.X && Main.mouseX < DrawPosition.X + 228 && 
                Main.mouseY >= DrawPosition.Y + 18 && Main.mouseY < DrawPosition.Y + 36;
            string ClassName = LastClass.Name;
            if (IsMouseOverExpBar)
            {
                ClassName = LastClass.GetExpString();
            }
            else
            {
                if (Maxed)
                    ClassName += " Master";
                else
                    ClassName += " Lv " + LastClass.GetLevel;
            }
            Utils.DrawBorderString(Main.spriteBatch, ClassName, DrawPosition - Vector2.UnitX * 16, (Maxed ? Color.Yellow : Color.White), anchorx: 1);
            DrawPosition.Y += 18;
            bool MouseOver = false;
            if (!Maxed)
            {
                DrawPosition.X -= 150;
                Main.spriteBatch.Draw(terraclasses.ExpBarTexture.Value, DrawPosition, new Rectangle(0, 0, 150, 12), Color.Green);
                DrawPosition.X += 2;
                DrawPosition.Y += 2;
                MouseOver = Main.mouseX >= DrawPosition.X && Main.mouseX < DrawPosition.X + 150 &&
                    Main.mouseY >= DrawPosition.Y && Main.mouseY < DrawPosition.Y + 12;
                int BarFill = (int)(144 * LastExpPercentage);
                DrawPosition.X += (144 - BarFill);
                Main.spriteBatch.Draw(terraclasses.ExpBarTexture.Value, DrawPosition, new Rectangle(2 + (144 - BarFill), 14, BarFill, 8), Color.Green);
            }
            return true;
        }
    }
}