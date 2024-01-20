using Terraria;
using Terraria.UI;
using Terraria.GameContent;
using Terraria.UI.Chat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace terraclasses.Interface
{
    public class DrawSkillEffectOnScreen : LegacyGameInterfaceLayer
    {
        public DrawSkillEffectOnScreen() : base("TerraClasses: Skill Effect", DrawSelf, InterfaceScaleType.Game)
        {

        }

        new protected static bool DrawSelf()
        {
            for (int p = 0; p < 255; p++)
            {
                if (Main.player[p].active)
                {
                    foreach (SkillData sd in Main.player[p].GetModPlayer<PlayerMod>().GetActiveSkills)
                    {
                        sd.Base.DrawInFrontOfEverything(sd);
                    }
                }
            }
            return true;
        }
    }
}