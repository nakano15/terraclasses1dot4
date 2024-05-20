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
    public class ClassInfosInterface : LegacyGameInterfaceLayer
    {
        static bool IsActive = false;
        const int WindowWidth = 720, WindowHeight = 520;
        static Color BackgroundColor = new Color(155, 95, 10);
        static Color PanelColor = new Color(207, 126, 12);
        static int SelectedClass = -1, SelectedSkill = -1;
        const int SlotDistance = 60;
        static Player player => MainMod.GetPlayerCharacter();
        static string[] ClassDescription = new string[0],
            SkillDescription = new string[0];
        const int ClassDescriptionMaxLines = 4, SkillDescriptionMaxLines = 6, MaxAttributeDisplay = 6;
        static int ClassDescriptionPage = 0, ClassDescriptionMaxPages = 0;
        static int SkillDescriptionPage = 0, SkillDescriptionMaxPages = 0;
        static int AttributesPage = 0, AttributesMaxPages = 0;
        const int MaxSkillIconsPerRow = 11;
        static int SkillScrollPage = 0, SkillScrollMaxPages = 0;

        public ClassInfosInterface() :
            base("TerraClasses : Class Infos", DrawInterface, InterfaceScaleType.UI)
        {

        }

        internal static void Open(int NewSelectedClass = -1, int NewSelectedSkill = -1)
        {
            Main.playerInventory = false;
            PlayerMod pm = player.GetModPlayer<PlayerMod>();
            SelectedClass = NewSelectedClass;
            OnSelectClass(SelectedClass > -1 ? pm.GetClasses[SelectedClass] : null);
            if (SelectedClass > -1)
            {
                SelectedSkill = NewSelectedSkill;
                OnSelectSkill(SelectedSkill > -1 ? pm.GetClasses[SelectedClass].GetSkills[SelectedSkill] : null);
            }
            else
            {
                SelectedSkill = -1;
                OnSelectSkill(null);
            }
            IsActive = true;
        }

        internal static void Close()
        {
            IsActive = false;
            SelectedClass = -1;
            SelectedSkill = -1;
            ClassDescription = new string[0];
            SkillDescription = new string[0];
        }

        static bool DrawInterface()
        {
            if (!IsActive) return true;
            if (Main.playerInventory)
            {
                Close();
                return true;
            }
            Vector2 StartPosition = new Vector2((int)((Main.screenWidth - WindowWidth) * .5f), (int)((Main.screenHeight - WindowHeight) * .5f));
            if (Main.mouseX >= StartPosition.X && Main.mouseX < StartPosition.X + WindowWidth && 
                Main.mouseY >= StartPosition.Y && Main.mouseY < StartPosition.Y + WindowHeight)
            {
                MainMod.GetPlayerCharacter().mouseInterface = true;
            }
            InterfaceHelper.DrawBackgroundPanel(StartPosition, WindowWidth, WindowHeight, BackgroundColor);
            Vector2 TitlePosition = new Vector2(StartPosition.X + 8, StartPosition.Y + 8);
            InterfaceHelper.DrawBackgroundPanel(TitlePosition, WindowWidth - 64, 48, PanelColor);
            Utils.DrawBorderString(Main.spriteBatch, "Classes", StartPosition + new Vector2((WindowWidth - 64) * .5f, 18), Color.White, 1.5f, 0.5f);
            {
                Vector2 ClosePosition = new Vector2(TitlePosition.X + WindowWidth - 64, TitlePosition.Y);
                InterfaceHelper.DrawBackgroundPanel(ClosePosition, 48, 48, PanelColor);
                Color closeColor = Color.Red;
                if (Main.mouseX >= ClosePosition.X && Main.mouseX < ClosePosition.X + 48 && 
                    Main.mouseY >= ClosePosition.Y && Main.mouseY < ClosePosition.Y + 48)
                {
                    closeColor = Color.White;
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Close();
                    }
                }
                Utils.DrawBorderString(Main.spriteBatch, "X", ClosePosition + new Vector2(24f, 12), closeColor, 1.5f, 0.5f);
            }
            string MouseText = "";
            PlayerMod pm = player.GetModPlayer<PlayerMod>();
            DrawClassInfos(StartPosition, pm, ref MouseText);
            DrawSkillInfos(StartPosition, pm, ref MouseText);
            if (MouseText != "")
            {
                MouseOverInterface.ChangeMouseText(MouseText);
            }
            return true;
        }

        static void DrawClassInfos(Vector2 StartPosition, PlayerMod pm, ref string MouseText)
        {
            Vector2 Position = new Vector2(StartPosition.X + 8, StartPosition.Y + 64);
            InterfaceHelper.DrawBackgroundPanel(Position, WindowWidth - 16, 60, PanelColor);
            {
                Vector2 SlotsStartPosition = new Vector2(Position.X + (WindowWidth - 16) * .5f, Position.Y + 4);
                float SlotsDistance = (int)((WindowWidth - 16) / 5);
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
                        int HalfLabelWidth = (int)(LabelWidth * .5f);
                        DrawLabel(InfoPosition, Class.GetLevelString(true), HalfLabelWidth, LabelHeight, TextColor);
                        {
                            Vector2 SPSubLabel = InfoPosition + Vector2.UnitX * (HalfLabelWidth);
                            DrawLabel(SPSubLabel, "Skill Points: " + Class.GetSkillPoints, HalfLabelWidth, LabelHeight, TextColor);
                        }
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
                        if (SelectedSkill == Index)
                        {
                            SkillSlotPositions.Y -= 4;
                        }
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
                        if (SelectedSkill == Index)
                        {
                            SkillSlotPositions.Y += 4;
                        }
                        SkillSlotPositions.X += SlotDistance;
                    }
                }
            }
            Position.Y += 68;
            {
                int LabelHeight = 28;
                int PanelWidth = (int)((WindowWidth - 16) * .5f);
                Vector2 SkillInfoPosition = Position;
                Vector2 SkillAttributesInfoPosition = Position + Vector2.UnitX * ((WindowWidth - 16) * .5f);
                SkillData Skill = SelectedSkill > -1 ? Class.GetSkills[SelectedSkill] : null;
                //Height = 208
                Color TextColor = Color.White;
                DrawLabel(SkillInfoPosition, Skill != null ? Skill.Name : "", PanelWidth, LabelHeight, TextColor);
                SkillInfoPosition.Y += 32;
                DrawLabel(SkillInfoPosition, Skill != null ? Skill.SkillType.ToString() : "", PanelWidth, LabelHeight, TextColor);
                SkillInfoPosition.Y += 32;
                InterfaceHelper.DrawBackgroundPanel(SkillInfoPosition, PanelWidth, 144, PanelColor);
                InterfaceHelper.DrawBackgroundPanel(SkillAttributesInfoPosition, PanelWidth, 208, PanelColor);
                if (Skill != null)
                {
                    for (int i = 0; i < SkillDescriptionMaxLines; i++)
                    {
                        int Index = i + SkillDescriptionMaxLines * SkillDescriptionPage;
                        if (Index >= SkillDescription.Length) break;
                        Vector2 DescPosition = new Vector2(SkillInfoPosition.X + 4, SkillInfoPosition.Y + 4 + 22f * i);
                        Utils.DrawBorderString(Main.spriteBatch, SkillDescription[i], DescPosition, TextColor);
                    }
                    //Draw skill Attributes on second area.
                    SkillAttributesInfoPosition += Vector2.One * 8f;
                    Utils.DrawBorderString(Main.spriteBatch, "Skill Attributes", SkillAttributesInfoPosition, Color.White);
                    SkillAttributesInfoPosition.Y += 30;
                    bool HasSkillPoint = Class.GetSkillPoints > 0;
                    for (int i = 0; i < MaxAttributeDisplay; i++)
                    {
                        Vector2 AttributePosition = SkillAttributesInfoPosition + Vector2.UnitY * i * 24f;
                        int Index = i + AttributesPage * MaxAttributeDisplay;
                        if (Index >= Skill.Base.GetSkillAttributes.Length) break;
                        SkillAttribute AttributeBase = Skill.Base.GetSkillAttributes[Index];
                        int AttributeLevel = Skill.GetAttributeLevel(Index);
                        Color color = AttributeLevel == AttributeBase.MaxLevel ? Color.Yellow : Color.White;
                        string Text = AttributeBase.Name + " ["+AttributeLevel+"/" +AttributeBase.MaxLevel + "]";
                        Vector2 Dimension = Utils.DrawBorderString(Main.spriteBatch, Text, AttributePosition, color);
                        if (Main.mouseX >= AttributePosition.X && Main.mouseX < AttributePosition.X + Dimension.X && 
                            Main.mouseY >= AttributePosition.Y && Main.mouseY < AttributePosition.Y + Dimension.Y)
                        {
                            MouseText = AttributeBase.Name + "\n\"" + AttributeBase.AttributeDescription(AttributeLevel) + "\"";
                            if (AttributeLevel < AttributeBase.MaxLevel)
                            {
                                MouseText += "\n\"Next: "+AttributeBase.AttributeDescription(AttributeLevel + 1)+"\"";
                            }
                        }
                        if (HasSkillPoint && AttributeLevel < AttributeBase.MaxLevel)
                        {
                            AttributePosition.X += Dimension.X + 4;
                            AttributePosition.Y += 3;
                            if (Main.mouseX >= AttributePosition.X && Main.mouseX < AttributePosition.X + 16 && 
                                Main.mouseY >= AttributePosition.Y && Main.mouseY < AttributePosition.Y + 16)
                            {
                                MouseText = "Spend Skill Point on this Attribute?\nUses " + AttributeBase.PointsUsed + " Skill Points.";
                                if (Main.mouseLeft && Main.mouseLeftRelease)
                                {
                                    Class.SpendSkillPointOnSkillAttribute(SelectedSkill, Index);
                                }
                            }
                            Main.spriteBatch.Draw(terraclasses.SkillUpButtonTexture.Value, AttributePosition, null, Color.White);
                        }
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
                AttributesMaxPages = Skill.Base.GetSkillAttributes.Length / MaxAttributeDisplay;
                AttributesPage = 0;
            }
        }
    }
}