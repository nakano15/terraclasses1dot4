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
    public class ClassInfosInterface : LegacyGameInterfaceLayer
    {
        static bool IsActive = false;
        const int WindowWidth = 720, WindowHeight = 520;
        static Color BackgroundColor = Color.Green;
        static Color PanelColor = new Color(100, 255, 50);
        static int SelectedClass = -1, SelectedSkill = -1;
        const int SlotDistance = 60;
        static Player player => MainMod.GetPlayerCharacter();
        static string[] ClassDescription = new string[0],
            SkillDescription = new string[0];
        const int ClassDescriptionMaxLines = 4;
        static int ClassDescriptionPage = 0, ClassDescriptionMaxPages = 0;
        static int SkillDescriptionPage = 0, SkillDescriptionMaxPages = 0;
        const int MaxSkillIconsPerRow = 10;
        static int SkillScrollPage = 0, SkillScrollMaxPages = 0;

        public ClassInfosInterface() :
            base("TerraClasses : Class Infos", DrawInterface, InterfaceScaleType.UI)
        {

        }

        static bool DrawInterface()
        {
            if (!IsActive) return false;
            Vector2 StartPosition = new Vector2((int)((Main.screenWidth - WindowWidth) * .5f), (int)((Main.screenHeight - WindowHeight) * .5f));
            if (Main.mouseX >= StartPosition.X && Main.mouseX < StartPosition.X + WindowWidth && 
                Main.mouseY >= StartPosition.Y && Main.mouseY < StartPosition.Y + WindowHeight)
            {
                MainMod.GetPlayerCharacter().mouseInterface = true;
            }
            InterfaceHelper.DrawBackgroundPanel(StartPosition, WindowWidth, WindowHeight, BackgroundColor);
            Vector2 TitlePosition = new Vector2(StartPosition.X + 8, StartPosition.Y + 8);
            InterfaceHelper.DrawBackgroundPanel(TitlePosition, WindowWidth - 32, 48, PanelColor);
            Utils.DrawBorderString(Main.spriteBatch, "Classes", StartPosition + new Vector2((WindowWidth - 32) * .5f, 18), Color.White, 1.5f, 0.5f);
            string MouseText = "";
            PlayerMod pm = player.GetModPlayer<PlayerMod>();
            DrawClassInfos(StartPosition, pm, ref MouseText);
            DrawSkillInfos(StartPosition, pm, ref MouseText);
            return true;
        }

        static void DrawClassInfos(Vector2 StartPosition, PlayerMod pm, ref string MouseText)
        {
            Vector2 Position = new Vector2(StartPosition.X + 8, StartPosition.Y + 64);
            InterfaceHelper.DrawBackgroundPanel(Position, WindowWidth - 32, 60, PanelColor);
            {
                Vector2 SlotsStartPosition = new Vector2(Position.X + (WindowWidth - 32) * .5f, Position.Y + 4);
                float SlotsDistance = (int)((WindowWidth - 32) / 5);
                SlotsStartPosition.X -= (SlotsDistance * PlayerMod.MaxClasses) * .5f;
                bool CanUnlockNextClass = false;
                for (int i = 0; i < PlayerMod.MaxClasses; i++)
                {
                    Vector2 SlotPosition = new Vector2(SlotsStartPosition.X + i * SlotsDistance + SlotDistance * .5f, SlotsStartPosition.Y);
                    ClassData Class = pm.GetClasses[i];
                    if (SelectedClass == i)
                    {
                        SlotPosition.Y -= 4;
                    }
                    CommonInterfaceMethods.DrawClassSlotIcon(SlotPosition, Class, CanUnlockNextClass);
                    if (CommonInterfaceMethods.IsMouseOverIcon(SlotPosition))
                    {
                        if (Class.IsUnlocked)
                        {
                            MouseText = Class.Name + " " + Class.GetLevelString();
                            if (Main.mouseLeft && Main.mouseLeftRelease)
                            {
                                if (SelectedClass == i)
                                {
                                    SelectedClass = -1;
                                    OnSelectClass(null);
                                }
                                else
                                {
                                    SelectedClass = i;
                                    OnSelectClass(Class);
                                }
                            }
                        }
                        else 
                        {
                            if (CanUnlockNextClass)
                            {
                                MouseText = "New Class Slot Unlocked!\nClick to pick a new class.";
                            }
                            else
                            {
                                MouseText = "Class Slot Locked.\nTrain more your current class to unlock it.";
                            }
                        }
                    }
                    CanUnlockNextClass = false;
                    if (Class.IsMastered)
                    {
                        CanUnlockNextClass = true;
                    }
                }
            }
            {
                Position.Y += 68;
                Vector2 InfoPosition = new Vector2(Position.X, Position.Y);
                if (SelectedClass > -1)
                {
                    ClassData Class = pm.GetClasses[SelectedClass];
                    if (Class.IsUnlocked)
                    {
                        int LabelWidth = (int)(WindowWidth * .5f - 16), LabelHeight = 28;
                        Color TextColor = Class.IsMastered ? Color.Yellow : Color.White;
                        DrawLabel(InfoPosition, Class.Name, LabelWidth, LabelHeight, TextColor);
                        InfoPosition.Y += 32;
                        DrawLabel(InfoPosition, Class.GetLevelString(true), LabelWidth, LabelHeight, TextColor);
                        InfoPosition.Y += 32;
                        DrawLabel(InfoPosition, Class.GetExpString(), LabelWidth, LabelHeight, TextColor);
                        InfoPosition.Y -= 64;
                        InfoPosition.X += (int)(WindowWidth * .5f - 16);
                        InterfaceHelper.DrawBackgroundPanel(InfoPosition, LabelWidth + 16, 96, PanelColor);
                        for (int i = 0; i < ClassDescriptionMaxLines; i++)
                        {
                            int Index = i + ClassDescriptionMaxLines * ClassDescriptionPage;
                            if (Index >= ClassDescription.Length) break;
                            Vector2 DescPosition = new Vector2(InfoPosition.X + 4, InfoPosition.Y + 4 + 22f * i);
                            Utils.DrawBorderString(Main.spriteBatch, ClassDescription[i], DescPosition, TextColor);
                        }
                    }
                    else
                    {
                        InterfaceHelper.DrawBackgroundPanel(InfoPosition, WindowWidth - 16, 96, PanelColor);
                        Utils.DrawBorderString(Main.spriteBatch, "This class is locked.", InfoPosition + Vector2.One * 4, Color.White);
                    }
                }
                else
                {
                    InterfaceHelper.DrawBackgroundPanel(InfoPosition, WindowWidth - 16, 96, PanelColor);
                    Utils.DrawBorderString(Main.spriteBatch, "No class selected.", InfoPosition + Vector2.One * 4, Color.White);
                }
            }
        }

        static void DrawSkillInfos(Vector2 StartPosition, PlayerMod pm, ref string MouseText)
        {
            Vector2 Position = new Vector2(StartPosition.X + 8, StartPosition.Y + 228);
            ClassData Class = SelectedClass > -1 ? pm.GetClasses[SelectedClass] : null;
            {
                InterfaceHelper.DrawBackgroundPanel(Position, WindowWidth - 16, SlotDistance, PanelColor);
                if (Class != null)
                {
                    Vector2 SkillSlotPositions = new Vector2(Position.X + ((WindowWidth - 16) - SlotDistance * MaxSkillIconsPerRow) * .5f, Position.Y + 4);
                    for (int i = 0; i < MaxSkillIconsPerRow; i++)
                    {
                        int Index = i + SkillScrollPage * MaxSkillIconsPerRow;
                        SkillData Skill = Index < Class.GetSkills.Length ? Class.GetSkills[Index] : null;
                        CommonInterfaceMethods.DrawSkillIcon(SkillSlotPositions, Skill);
                        if (Skill != null && CommonInterfaceMethods.IsMouseOverIcon(SkillSlotPositions))
                        {
                            MouseText = Skill.Name;
                            if (Main.mouseLeft && Main.mouseLeftRelease)
                            {
                                if (SelectedSkill == Index)
                                {
                                    SelectedSkill = -1;
                                    OnSelectSkill(null);
                                }
                                else
                                {
                                    SelectedSkill = Index;
                                    OnSelectSkill(Skill);
                                }
                            }
                        }
                        SkillSlotPositions.X += SlotDistance;
                    }
                }

            }
        }

        static void DrawLabel(Vector2 Position, string Text, int Width, int Height, Color color)
        {
            InterfaceHelper.DrawBackgroundPanel(Position, Width, Height, PanelColor);
            Utils.DrawBorderString(Main.spriteBatch, Text, Position + Vector2.One * 4, color);

        }

        static void OnSelectClass(ClassData Class)
        {
            if (Class != null)
            {
                ClassDescription = InterfaceHelper.WordwrapText(Class.Base.Description, FontAssets.MouseText.Value, 344);
                ClassDescriptionMaxPages = ClassDescription.Length / ClassDescriptionMaxLines;
                ClassDescriptionPage = 0;
                SelectedSkill = -1;
                OnSelectSkill(null);
            }
            else
            {
                ClassDescription = new string[0];
            }
        }

        static void OnSelectSkill(SkillData Skill)
        {
            if (Skill == null)
            {
                SkillDescription = new string[0];
            }
            else
            {
                SkillDescription = InterfaceHelper.WordwrapText(Skill.Description, FontAssets.MouseText.Value, 344);
                SkillDescriptionMaxPages = SkillDescription.Length / ClassDescriptionMaxLines;
                SkillDescriptionPage = 0;
            }
        }
    }
}