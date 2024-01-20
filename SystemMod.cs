using Terraria;
using Terraria.ModLoader;
using terraclasses.Interface;
using System.Collections.Generic;
using Terraria.UI;

namespace terraclasses
{
    public class SystemMod : ModSystem
    {
        DrawSkillEffectOnScreen DrawSkillEffectOnScreenDefinition;
        DrawClassLevelProgress DrawClassLevelProgressDefinition;

        public override void Load()
        {
            DrawSkillEffectOnScreenDefinition = new DrawSkillEffectOnScreen();
            DrawClassLevelProgressDefinition = new DrawClassLevelProgress();
        }

        public override void Unload()
        {
            DrawSkillEffectOnScreenDefinition = null;
            DrawClassLevelProgressDefinition = null;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int InventoryPosition = -1;
            for (int i = 0; i < layers.Count; i++)
            {
                switch(layers[i].Name)
                {
                    case "Vanilla: Inventory":
                        InventoryPosition = i;
                        break;
                }
            }
            layers.Insert(InventoryPosition, DrawClassLevelProgressDefinition);
            layers.Insert(0, DrawSkillEffectOnScreenDefinition);
        }
    }
}