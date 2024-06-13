using Terraria;
using Terraria.UI;
using Terraria.GameContent;
using Terraria.UI.Chat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ModLoader;
using ReLogic.Graphics;
using nterrautils;
using Microsoft.CodeAnalysis.Text;

namespace terraclasses.Interface
{
    public class ClassHeldIconInterface : LegacyGameInterfaceLayer
    {
        static int ClassPos = -1, SkillPos = -1;
        public static bool IsHoldingSkillIcon => SkillPos > -1;
        static bool JustClicked = false;

        public ClassHeldIconInterface() : base("TerraClasses: Held Class Icon UI", DrawInterface, InterfaceScaleType.UI)
        {

        }

        public static bool BeginHoldingSkillIcon(int NewClassPos, int NewSkillPos)
        {
            if (NewClassPos >= 0 && NewClassPos < PlayerMod.MaxClasses && NewSkillPos >= 0)
            {
                ClassData[] Classes = MainMod.GetPlayerCharacter().GetModPlayer<PlayerMod>().GetClasses;
                if (Classes[NewClassPos].IsUnlocked && NewSkillPos < Classes[NewClassPos].GetSkills.Length && Classes[NewClassPos].GetSkills[NewSkillPos].IsUnlocked)
                {
                    ClassPos = NewClassPos;
                    SkillPos = NewSkillPos;
                    JustClicked = true;
                    return true;
                }
            }
            return false;
        }

        public static void GetHeldSkillIcon(out int GotClassPos, out int GotSkillPos)
        {
            GotClassPos = ClassPos;
            GotSkillPos = SkillPos;
            ClassPos = -1;
            SkillPos = -1;
            JustClicked = false;
        }

        static bool DrawInterface()
        {
            if (IsHoldingSkillIcon)
            {
                if (!JustClicked && Main.mouseLeft && Main.mouseLeftRelease)
                {
                    ClassPos = -1;
                    SkillPos = -1;
                }
                else
                {
                    Vector2 Position = new Vector2(Main.mouseX + 16, Main.mouseY + 16);
                    float MaxX = Main.screenWidth - 56, MaxY = Main.screenHeight - 56;
                    if (Position.X > MaxX) Position.X = MaxX;
                    if (Position.Y > MaxY) Position.Y = MaxY;
                    CommonInterfaceMethods.DrawSkillIcon(Position, MainMod.GetPlayerCharacter().GetModPlayer<PlayerMod>().GetClasses[ClassPos].GetSkills[SkillPos]);
                }
            }
            if (!Main.mouseLeft)
                JustClicked = false;
            return true;
        }
    }
}