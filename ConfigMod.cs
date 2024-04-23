using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.Collections.Generic;
using Terraria.Localization;

namespace terraclasses
{
    class ConfigMod : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public Dictionary<ItemDefinition, ItemTypes> ItemTypesList = ItemMod.AssignItemTypes();

        public override void OnChanged()
        {
            ItemMod.ListItemTypes = ItemTypesList;
        }
    }
}