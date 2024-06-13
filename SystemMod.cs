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
        ClassHeldIconInterface ClassHeldIconInterfaceDefinition;

        public override void Load()
        {
            DrawSkillEffectOnScreenDefinition = new DrawSkillEffectOnScreen();
            DrawClassLevelProgressDefinition = new DrawClassLevelProgress();
            ClassInfosInterfaceDefinition = new ClassInfosInterface();
            ClassHeldIconInterfaceDefinition = new ClassHeldIconInterface();
        }

        public override void Unload()
        {
            DrawSkillEffectOnScreenDefinition = null;
            DrawClassLevelProgressDefinition = null;
            ClassInfosInterfaceDefinition = null;
            ClassHeldIconInterfaceDefinition = null;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int InventoryPosition = -1;
            int MousePosition = -1;
            for (int i = 0; i < layers.Count; i++)
            {
                switch(layers[i].Name)
                {
                    case "Vanilla: Inventory":
                        InventoryPosition = i;
                        break;
                    case "Vanilla: Mouse Text":
                        MousePosition = i;
                        break;
                }
            }
            if (MousePosition > -1)
            {
                layers.Insert(MousePosition, ClassHeldIconInterfaceDefinition);
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