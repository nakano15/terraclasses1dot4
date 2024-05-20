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
        ClassInfosInterface ClassInfosInterfaceDefinition;

        public override void Load()
        {
            DrawSkillEffectOnScreenDefinition = new DrawSkillEffectOnScreen();
            DrawClassLevelProgressDefinition = new DrawClassLevelProgress();
            ClassInfosInterfaceDefinition = new ClassInfosInterface();
        }

        public override void Unload()
        {
            DrawSkillEffectOnScreenDefinition = null;
            DrawClassLevelProgressDefinition = null;
            ClassInfosInterfaceDefinition = null;
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
            if (InventoryPosition > -1)
            {
                layers.Insert(InventoryPosition, DrawSkillEffectOnScreenDefinition);
                layers.Insert(InventoryPosition, DrawClassLevelProgressDefinition);
                layers.Insert(InventoryPosition, ClassInfosInterfaceDefinition);
            }
        }
    }
}