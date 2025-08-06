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

namespace terraclasses1dot4.Interface
{
    public class CommonInterfaceMethods
    {
        public static bool IsMouseOverIcon(Vector2 Position)
        {
            const float MinGap = 4f, MaxGap = 52f;
            return Main.mouseX >= Position.X + MinGap && Main.mouseX < Position.X + MaxGap &&
                Main.mouseY >= Position.Y + MinGap && Main.mouseY < Position.Y + MaxGap;
        }

        public static void DrawClassSlotIcon(Vector2 Position, ClassData Class, bool CanUnlockNextClass)
        {
            Texture2D ClassSlotTexture = terraclasses.ClassIconSlotTexture.Value;
            Main.spriteBatch.Draw(ClassSlotTexture, Position, new Rectangle(Class.IsMastered ? 56 : 0, 0, 56, 56), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            if (Class.IsUnlocked)
            {
                Texture2D ClassIcon;
                Rectangle ClassRect;
                Class.Base.GetClassIcon(out ClassIcon, out ClassRect);
                float Scale = ClassRect.Width;
                if (Scale < ClassRect.Height)
                {
                    Scale = ClassRect.Height;
                }
                Scale = 48f / Scale;
                Position.X += 4;
                Position.Y += 4;
                Main.spriteBatch.Draw(ClassIcon, Position, ClassRect, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
            else
            {
                Texture2D Icon = terraclasses.ClassIconsTexture.Value;
                Rectangle LockRect = new Rectangle(48, 0, 48, 48);
                if (CanUnlockNextClass)
                {
                    LockRect.X *= 2;
                }
                float Scale = 1f;
                Position.X += 4;
                Position.Y += 4;
                Main.spriteBatch.Draw(Icon, Position, LockRect, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }

        public static void DrawClassSlotIcon(Vector2 Position, ClassBase Class)
        {
            Texture2D ClassSlotTexture = terraclasses.ClassIconSlotTexture.Value;
            Main.spriteBatch.Draw(ClassSlotTexture, Position, new Rectangle(0, 0, 56, 56), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            Texture2D ClassIcon;
            Rectangle ClassRect;
            Class.GetClassIcon(out ClassIcon, out ClassRect);
            float Scale = ClassRect.Width;
            if (Scale < ClassRect.Height)
            {
                Scale = ClassRect.Height;
            }
            Scale = 48f / Scale;
            Position.X += 4;
            Position.Y += 4;
            Main.spriteBatch.Draw(ClassIcon, Position, ClassRect, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        public static void DrawSkillIcon(Vector2 Position, SkillData Skill, string QuickSlotKey = "")
        {
            Texture2D ClassSlotTexture = terraclasses.ClassIconSlotTexture.Value;
            Main.spriteBatch.Draw(ClassSlotTexture, Position, new Rectangle(0, 0, 56, 56), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            if (Skill != null)
            {
                Color color = Skill.IsUnlocked ? Color.White : Color.Gray;
                Position.X += 4;
                Position.Y += 4;
                Skill.Base.GetSkillIcon(out Texture2D Icon, out Rectangle rect);
                float Scale = rect.Width;
                if (rect.Height > Scale)
                {
                    Scale = rect.Height;
                }
                Scale = 48f / Scale;
                Main.spriteBatch.Draw(Icon, Position, rect, color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
            if (QuickSlotKey != "")
            {
                Utils.DrawBorderString(Main.spriteBatch, QuickSlotKey, Position, Color.White, .7f);
            }
        }

        public static void DrawSkillQuickslotIcon(Vector2 Position, SkillData Skill, int SlotIndex, string QuickSlotKey = "")
        {
            Texture2D ClassSlotTexture = terraclasses.SkillQuickslotTexture.Value;
            Color color;
            switch (SlotIndex)
            {
                default:
                    color = Color.White;
                    break;
                case 0:
                case 1:
                case 2:
                case 3:
                    color = Color.Green;
                    break;
                case 4:
                    color = Color.Red;
                    break;
            }
            Main.spriteBatch.Draw(ClassSlotTexture, Position, null, color, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            Position.X += 4;
            Position.Y += 4;
            if (Skill != null)
            {
                color = Skill.IsUnlocked ? Color.White : Color.Gray;
                Skill.Base.GetSkillIcon(out Texture2D Icon, out Rectangle rect);
                float Scale = rect.Width;
                if (rect.Height > Scale)
                {
                    Scale = rect.Height;
                }
                Scale = 32f / Scale;
                Main.spriteBatch.Draw(Icon, Position, rect, color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
            if (QuickSlotKey != "")
            {
                Utils.DrawBorderString(Main.spriteBatch, QuickSlotKey, Position, Color.White, .7f);
            }
        }

        public static bool SkillGetIconButton(Vector2 Position, out bool MouseOver)
        {
            if (ClassHeldIconInterface.IsHoldingSkillIcon)
            {
                MouseOver = false;
                return false;
            }
            Main.spriteBatch.Draw(terraclasses.SkillIconPlusButtonTexture.Value, Position, Color.White);
            if (Main.mouseX >= Position.X && Main.mouseX < Position.X + 16 && 
                Main.mouseY >= Position.Y && Main.mouseY < Position.Y + 16)
            {
                MouseOver = true;
                return Main.mouseLeft && Main.mouseLeftRelease;
            }
            MouseOver = false;
            return false;
        }
    }
}