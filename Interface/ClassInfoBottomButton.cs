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
    public class ClassInfoBottomButton : nterrautils.BottomButton
    {
        public override string Text => "Classes";
        public override int InternalWidth => 560;
        public override int InternalHeight => 220;
        public override Color TabColor => Color.Green;
        int SelectedClass = -1;
        int SelectedSkill = -1;
        int SubWindowScroll = 0;
        int SubWindowMaxScroll = 0;
        const int ClassSlotDistance = 60;
        Color PanelsColor = new Color(100, 255, 50);
        string[] SkillDescription = new string[0];

        void GetPlayerInfos(out PlayerMod pm)
        {
            pm = Main.LocalPlayer.GetModPlayer<PlayerMod>();
        }

        public override void OnClickAction(bool OpeningTab)
        {
            if (!OpeningTab)
            {
                SelectedClass = -1;
                SelectedSkill = -1;
                SubWindowScroll = 0;
                SubWindowMaxScroll = 0;
            }
        }

        public override void DrawInternal(Vector2 DrawPosition)
        {
            GetPlayerInfos (out PlayerMod pm);
            DrawClassList(DrawPosition, pm);
        }

        void DrawClassList(Vector2 StartPosition, PlayerMod pm)
        {
            StartPosition.Y = (int)(StartPosition.Y + 8);
            ClassData[] Classes = pm.GetClasses;
            string MouseText = "";
            bool CanUnlockNextClass = pm.GetLastClass.IsMastered;
            if (SelectedClass > -1)
            {
                ClassData Class = Classes[SelectedClass];
                if (SelectedSkill > -1)
                {
                    DrawSkillInfo(StartPosition, Class, Class.GetSkills[SelectedSkill], ref MouseText);
                }
                else
                {
                    if (Class.IsUnlocked)
                    {
                        DrawClassInformation(StartPosition, Class, ref MouseText);
                    }
                    else
                    {
                        {
                            Vector2 TitlePosition = new Vector2(StartPosition.X + InternalWidth * .5f, StartPosition.Y);
                            Utils.DrawBorderString(Main.spriteBatch, "Pick a New Class", TitlePosition, Color.White, 1f, 0.5f);
                        }
                    }
                }
                {
                    Vector2 ReturnButtonPosition = new Vector2(StartPosition.X + InternalWidth * .5f, StartPosition.Y + InternalHeight - 20);
                    const string CloseText = "Return";
                    Vector2 CloseButtonDim = FontAssets.MouseText.Value.MeasureString(CloseText);
                    ReturnButtonPosition.X -= CloseButtonDim.X * .5f;
                    ReturnButtonPosition.Y -= CloseButtonDim.Y;
                    Color c = Color.White;
                    if (Main.mouseX >= ReturnButtonPosition.X && Main.mouseX < ReturnButtonPosition.X + CloseButtonDim.X && 
                        Main.mouseY >= ReturnButtonPosition.Y && Main.mouseX < ReturnButtonPosition.Y + CloseButtonDim.Y)
                    {
                        c = Color.Red;
                        if (Main.mouseLeft && Main.mouseLeftRelease)
                        {
                            if (SelectedSkill > -1)
                                SelectedSkill = -1;
                            else
                                SelectedClass = -1;
                            SubWindowScroll = 0;
                        }
                    }
                    Utils.DrawBorderString(Main.spriteBatch, CloseText, ReturnButtonPosition, c);
                }
            }
            else
            {
                DrawPlayerClassList(StartPosition, CanUnlockNextClass, Classes, ref MouseText);
            }
            if (MouseText != "")
            {
                Vector2 Position = new Vector2(Main.mouseX + 16, Main.mouseY + 16);
                Utils.DrawBorderString(Main.spriteBatch, MouseText, Position, Color.White);
            }
        }

        void DrawPlayerClassList(Vector2 StartPosition, bool CanUnlockNextClass, ClassData[] Classes, ref string MouseText)
        {
            StartPosition.X += (InternalWidth - ClassSlotDistance * 5) * .5f;
            Texture2D ClassSlotTexture = terraclasses.ClassIconSlotTexture.Value;
            for (int i = 0; i < 5; i++)
            {
                StartPosition.X = (int)StartPosition.X;
                bool MouseOver = Main.mouseX >= StartPosition.X + 4 && Main.mouseX < StartPosition.X + 52 && 
                    Main.mouseY >= StartPosition.Y + 4 && Main.mouseY < StartPosition.Y + 52;
                bool DrawCanUnlock = false;
                if (CanUnlockNextClass && !Classes[i].IsUnlocked)
                {
                    DrawCanUnlock = true;
                    CanUnlockNextClass = false;
                }
                DrawClassSlotIcon(StartPosition, Classes[i], DrawCanUnlock);
                if (MouseOver)
                {
                    if (Classes[i].IsUnlocked)
                    {
                        MouseText = Classes[i].Name + " " + Classes[i].GetLevelString()+ "\nClick to check information.";
                        if (Main.mouseLeft && Main.mouseLeftRelease)
                        {
                            if (SelectedClass == i)
                            {
                                SelectedClass = -1;
                            }
                            else
                            {
                                SelectedClass = i;
                            }
                            SelectedSkill = -1;
                            SubWindowScroll = 0;
                        }
                    }
                    else
                    {
                        if (DrawCanUnlock)
                        {
                            MouseText = "New Class Unlocked\nClick to pick new one.";
                            if (Main.mouseLeft && Main.mouseLeftRelease)
                            {
                                if (SelectedClass == i)
                                {
                                    SelectedClass = -1;
                                }
                                else
                                {
                                    SelectedClass = i;
                                }
                            }
                        }
                        else
                        {
                            MouseText = "Empty Slot";
                        }
                    }
                }
                StartPosition.X += ClassSlotDistance;
            }
        }

        void DrawClassInformation(Vector2 StartPosition, ClassData Class, ref string MouseText)
        {
            Vector2 InfoPosition = new Vector2(StartPosition.X + 4, StartPosition.Y + 4);
            InterfaceHelper.DrawBackgroundPanel(InfoPosition, InternalWidth - 8, 68, PanelsColor);
            InfoPosition.X += 4;
            InfoPosition.Y += 4;
            DrawClassSlotIcon(InfoPosition, Class, false);
            if (Main.mouseX >= InfoPosition.X + 4 && Main.mouseX < InfoPosition.X + 52 && 
                Main.mouseY >= InfoPosition.Y + 4 && Main.mouseY < InfoPosition.Y + 52)
            {
                MouseText = "\"" + Class.Base.Description + "\"";
            }
            InfoPosition.X += ClassSlotDistance;
            string Text = Class.Name + " " + Class.GetLevelString(true);
            Utils.DrawBorderString(Main.spriteBatch, Text, InfoPosition, Color.White);
            InfoPosition.Y += 26;
            Text = Class.GetExpString();
            Utils.DrawBorderString(Main.spriteBatch, Text, InfoPosition, Color.White);
            InfoPosition.Y = StartPosition.Y + 76f;
            InfoPosition.X -= 4 + ClassSlotDistance;
            InterfaceHelper.DrawBackgroundPanel(InfoPosition, InternalWidth - 8, 68, PanelsColor);
            InfoPosition.Y += 4;
            //Skills List
            const int SkillsPerPage = 7;
            const int SkillSlotDimension = 60;
            InfoPosition.X = StartPosition.X + (InternalWidth - SkillSlotDimension * SkillsPerPage) * .5f;
            for (int i = 0; i < SkillsPerPage; i++)
            {
                int Index = i + SubWindowScroll * SkillsPerPage;
                SkillData skill = Index < Class.GetSkills.Length ? Class.GetSkills[Index] : null;
                DrawSkillIcon(InfoPosition, skill);
                if (skill != null && Main.mouseX >= InfoPosition.X + 4 && Main.mouseX < InfoPosition.X + 52 && 
                    Main.mouseY >= InfoPosition.Y + 4 && Main.mouseY < InfoPosition.Y + 52)
                {
                    MouseText = skill.Base.Name + "\nClick for Information.";
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        SelectedSkill = i;
                        OnChangeSelectedSkill(Class, skill);
                    }
                }
                InfoPosition.X += SkillSlotDimension;
            }
        }

        void DrawSkillInfo(Vector2 Position, ClassData Class, SkillData Skill, ref string MouseText)
        {
            Vector2 StartPosition = new Vector2(Position.X, Position.Y);
            Position.X += 4;
            Position.Y += 4;
            InterfaceHelper.DrawBackgroundPanel(Position, InternalWidth - 8, InternalHeight - 38, PanelsColor);
            Position.X += (InternalWidth - 4) * .5f;
            InterfaceHelper.DrawSeparator(Position, InternalHeight - 32, false, Color.LightGreen);
            Position.X = StartPosition.X + 4;
            Position.X += 4;
            Position.Y += 4;
            DrawSkillIcon(Position, Skill);
            Position.X += ClassSlotDistance;
            Utils.DrawBorderString(Main.spriteBatch, Skill.Name, Position, Color.White);
            Position.Y += 24f;
            Utils.DrawBorderString(Main.spriteBatch, Skill.SkillType.ToString(), Position, Color.White, .9f);
            Position.Y += 30f;
            Position.X -= ClassSlotDistance;
            foreach (string s in SkillDescription)
            {
                Utils.DrawBorderString(Main.spriteBatch, s, Position, Color.White, .9f);
                Position.Y += 20;
            }
            Position.X = StartPosition.X + (InternalWidth) * .5f + 6;
            Position.Y = StartPosition.Y + 4;
            SkillAttribute[] att = Skill.Base.GetSkillAttributes;
            bool HasSkillPoints = Class.GetSkillPoints > 0;
            for (int i = 0; i < att.Length; i++)
            {
                Vector2 InfoPosition = new Vector2(Position.X, Position.Y);
                int Level = Skill.GetAttributeLevel(i);
                if (HasSkillPoints && Class.GetSkillPoints >= att[i].PointsUsed && Skill.GetAttributeLevel(i) < att[i].MaxLevel)
                {
                    const string IncreaseText = "+";
                    Vector2 Dimension = FontAssets.MouseText.Value.MeasureString(IncreaseText) * .8f;
                    if (Main.mouseX >= InfoPosition.X && Main.mouseX < InfoPosition.X + Dimension.X && 
                        Main.mouseY >= InfoPosition.Y && Main.mouseY < InfoPosition.Y + Dimension.Y)
                    {
                        MouseText = "Uses " + att[i].PointsUsed + " Skill Points to Level Up.";
                        if (Main.mouseLeft && Main.mouseLeftRelease)
                        {
                            int PointsSpent = att[i].PointsUsed;
                            Skill.ChangeAttributeLevel(i, PointsSpent);
                            Skill.UpdateSkillUnlockedState();
                            Skill.UpdatePassiveSkill(MainMod.GetPlayerCharacter());
                            Class.ChangeSkillPoints(-PointsSpent);
                        }
                    }
                    Utils.DrawBorderString(Main.spriteBatch, IncreaseText, InfoPosition, Color.Green, 0.8f);
                    InfoPosition.X += Dimension.X;
                }
                string Text = att[i].AttributeDescription(Level);
                Utils.DrawBorderString(Main.spriteBatch, Text, InfoPosition, Color.White, 0.8f);
                float Percent = att[i].MaxLevel > 0 ? ((float)Level / att[i].MaxLevel) : 1f;
                Text = Text.Substring(0, (int)(Text.Length * Percent));
                Utils.DrawBorderString(Main.spriteBatch, Text, InfoPosition, Color.Yellow, 0.8f);
                Position.Y += 24f;
            }
        }

        void OnChangeSelectedSkill(ClassData Class, SkillData Skill)
        {
            SkillDescription = InterfaceHelper.WordwrapText(Skill.Description, FontAssets.MouseText.Value, (InternalWidth - 4) * .5f, .9f);
        }

        void DrawClassSlotIcon(Vector2 Position, ClassData Class, bool CanUnlockNextClass)
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

        void DrawSkillIcon(Vector2 Position, SkillData Skill)
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