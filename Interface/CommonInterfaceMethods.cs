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

namespace terraclasses.Interface
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

        public static void DrawSkillIcon(Vector2 Position, SkillData Skill)
        {
            Texture2D ClassSlotTexture = terraclasses.ClassIconSlotTexture.Value;
            Main.spriteBatch.Draw(ClassSlotTexture, Position, new Rectangle(0, 0, 56, 56), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            if (Skill == null) return;
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
    }
}