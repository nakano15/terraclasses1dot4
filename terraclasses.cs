using Terraria;
using Terraria.Audio;
using Terraria.IO;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;

namespace terraclasses
{
	public class terraclasses : Mod
	{
		internal static string GetModName => self.Name;
		internal static Mod GetMod => self;
		static Mod self;
		public static Asset<Texture2D> CastBarTexture, MagicCircleTexture;
		public static Asset<Texture2D> CerberusTexture, CerberusHeadTexture;
		public static Asset<Texture2D> ExpBarTexture;
		internal static ModKeybind[] SkillSlot;
		public static bool ShowExpRewardAsPercentage = true;

        public override void Load()
        {
			self = this;
			SkillContainer.AddSkillContainer(this, new SkillDB());
			ClassContainer.AddClassContainer(this, new ClassDB());
			if (!Main.dedServ)
			{
				CastBarTexture = ModContent.Request<Texture2D>("terraclasses/Content/CastBar");
				MagicCircleTexture = ModContent.Request<Texture2D>("terraclasses/Content/MagicCircle");
				CerberusTexture = ModContent.Request<Texture2D>("terraclasses/Content/Effects/cerberus");
				CerberusHeadTexture = ModContent.Request<Texture2D>("terraclasses/Content/Effects/CerberusBiteHead");
				ExpBarTexture = ModContent.Request<Texture2D>("terraclasses/Content/Interface/LevelArrow");
				SkillSlot = new ModKeybind[4];
				SkillSlot[0] = KeybindLoader.RegisterKeybind(this, "Skill Slot 1", "Q");
				SkillSlot[1] = KeybindLoader.RegisterKeybind(this, "Skill Slot 2", "Z");
				SkillSlot[2] = KeybindLoader.RegisterKeybind(this, "Skill Slot 3", "X");
				SkillSlot[3] = KeybindLoader.RegisterKeybind(this, "Skill Slot 4", "C");
			}
        }

        public override void Unload()
        {
			self = null;
			CastBarTexture = null;
			MagicCircleTexture = null;
			ExpBarTexture = null;
			CerberusTexture = null;
			CerberusHeadTexture = null;
			ClassContainer.Unload();
			SkillContainer.Unload();
        }
    
	}
}