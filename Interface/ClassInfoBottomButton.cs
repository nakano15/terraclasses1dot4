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
    public class ClassInfoBottomButton : nterrautils.BottomButton
    {
        public override string Text => "Class";
        public override int InternalWidth => 480;
        public override int InternalHeight => 220;
        public override Color TabColor => new Color (255, 196, 112);

        void GetPlayerInfos(out PlayerMod pm)
        {
            pm = Main.LocalPlayer.GetModPlayer<PlayerMod>();
        }

        public override void OnClickAction(bool OpeningTab)
        {
            
        }

        public override void DrawInternal(Vector2 DrawPosition)
        {
            GetPlayerInfos (out PlayerMod pm);
            DrawClassList(DrawPosition, pm);
        }

        void DrawClassList(Vector2 StartPosition, PlayerMod pm)
        {
            const int ClassSlotDistance = 60;
            StartPosition.Y = (int)(StartPosition.Y + 8);
            StartPosition.X += (InternalWidth - ClassSlotDistance * 5) * .5f;
            Texture2D ClassSlotTexture = terraclasses.ClassIconSlotTexture.Value;
            ClassData[] Classes = pm.GetClasses;
            bool CanUnlockNextClass = pm.GetLastClass.IsMastered;
            for (int i = 0; i < 5; i++)
            {
                StartPosition.X = (int)StartPosition.X;
                Main.spriteBatch.Draw(ClassSlotTexture, StartPosition, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                if (Classes[i].IsUnlocked)
                {
                    Texture2D ClassIcon;
                    Rectangle ClassRect;
                    Classes[i].Base.GetClassIcon(out ClassIcon, out ClassRect);
                    float Scale = ClassRect.Width;
                    if (Scale < ClassRect.Height)
                    {
                        Scale = ClassRect.Height;
                    }
                    Scale = 48f / Scale;
                    Vector2 IconPosition = StartPosition;
                    IconPosition.X += 4;
                    IconPosition.Y += 4;
                    Main.spriteBatch.Draw(ClassIcon, IconPosition, ClassRect, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                }
                else
                {
                    Texture2D Icon = terraclasses.ClassIconsTexture.Value;
                    Rectangle LockRect = new Rectangle(48, 0, 48, 48);
                    if (CanUnlockNextClass)
                    {
                        LockRect.X *= 2;
                        CanUnlockNextClass = false;
                    }
                    float Scale = 1f;
                    Vector2 IconPosition = StartPosition;
                    IconPosition.X += 4;
                    IconPosition.Y += 4;
                    Main.spriteBatch.Draw(Icon, IconPosition, LockRect, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
                }
                StartPosition.X += ClassSlotDistance;
            }
        }
    }
}