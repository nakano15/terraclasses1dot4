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

namespace terraclasses1dot4
{
	public class terraclasses : Mod
	{
		internal static string GetModName => self.Name;
		internal static Mod GetMod => self;
		static Mod self;
		public static Asset<Texture2D> CastBarTexture, MagicCircleTexture;
		public static Asset<Texture2D> CerberusTexture, CerberusHeadTexture;
		public static Asset<Texture2D> ExpBarTexture, SkillUpButtonTexture, SkillIconPlusButtonTexture;
		public static Asset<Texture2D> ClassIconsTexture;
		public static Asset<Texture2D> SkillIconsTexture;
		public static Asset<Texture2D> ClassIconSlotTexture, SkillQuickslotTexture;
		public static Asset<Texture2D> CleaveEffectTexture;
		internal static ModKeybind[] SkillSlot;
		public static bool ShowExpRewardAsPercentage = true;

		public static List<ClassID> StarterClasses = new List<ClassID>();
		internal static List<ClassID> UnlockedClasses = new List<ClassID>();

        public override void Load()
        {
			self = this;
			SkillContainer.AddSkillContainer(this, new SkillDB());
			ClassContainer.AddClassContainer(this, new ClassDB());
			//
			StarterClasses.Add(new ClassID(ClassDB.Terrarian, this));
			StarterClasses.Add(new ClassID(ClassDB.Fighter, this));
			UnlockedClasses.Add(new ClassID(ClassDB.Cerberus, this));
			//
			if (!Main.dedServ)
			{
				ClassIconSlotTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/Interface/ClassIconSlot");
				SkillQuickslotTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/Interface/SkillQuickSlot");
				ClassIconsTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/ClassIcons");
				SkillIconsTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/SkillIcons");
				CastBarTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/CastBar");
				MagicCircleTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/MagicCircle");
				CerberusTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/Effects/cerberus");
				CerberusHeadTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/Effects/CerberusBiteHead");
				CleaveEffectTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/Effects/CleaveAnimation");
				ExpBarTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/Interface/LevelArrow");
				SkillUpButtonTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/Interface/SkillUpArrowButton");
				SkillIconPlusButtonTexture = ModContent.Request<Texture2D>("terraclasses1dot4/Content/Interface/PlusButton");
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
			ClassIconSlotTexture = null;
			ClassIconsTexture = null;
			CastBarTexture = null;
			MagicCircleTexture = null;
			ExpBarTexture = null;
			CerberusTexture = null;
			CerberusHeadTexture = null;
			ClassContainer.Unload();
			SkillContainer.Unload();
			StarterClasses.Clear();
			StarterClasses = null;
			UnlockedClasses.Clear();
			UnlockedClasses = null;
        }

		public static ClassID[] GetUnlockedClasses(params ClassID[] Exceptions)
		{
			return StarterClasses.Union(UnlockedClasses).Except(Exceptions).ToArray();
		}

        public override void PostSetupContent()
        {
			nterrautils.Interfaces.BottomButtonsInterface.AddNewTab(new Interface.ClassInfoBottomButton());
        }
	}
}