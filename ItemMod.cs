using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.Collections.Generic;

namespace terraclasses1dot4
{
    public class ItemMod : GlobalItem
    {
		internal static Dictionary<ItemDefinition, ItemTypes> ListItemTypes = new Dictionary<ItemDefinition, ItemTypes>();
        public override bool InstancePerEntity => true;
        ItemTypes _ItemType = ItemTypes.None;

        public static ItemTypes GetItemType(Item item)
        {
            return item.GetGlobalItem<ItemMod>()._ItemType;
        }

        public override void SetDefaults(Item entity)
        {
            if (entity.damage > 0 && entity.netID < Main.maxItems)
            {
                foreach (ItemDefinition def in ListItemTypes.Keys)
                {
                    if (def.Type == entity.type)
                    {
                        _ItemType = ListItemTypes[def];
                        break;
                    }
                }
                /*ItemDefinition def = new ItemDefinition(entity.netID);
                if (ListItemTypes.ContainsKey(def))
                {
                    _ItemType = ListItemTypes[def];
                }*/
            }
        }

        public override void Unload()
        {
            ListItemTypes.Clear();
            ListItemTypes = null;
        }

        internal static Dictionary<ItemDefinition, ItemTypes> AssignItemTypes()
        {
            Dictionary<ItemDefinition, ItemTypes> List = new Dictionary<ItemDefinition, ItemTypes>();
            ItemTypes type = ItemTypes.Dagger;
            //Daggers
            List.Add(new ItemDefinition(ItemID.CopperShortsword), type);
            List.Add(new ItemDefinition(ItemID.CopperShortswordOld), type);
            List.Add(new ItemDefinition(ItemID.TinShortsword), type);
            List.Add(new ItemDefinition(ItemID.TinShortswordOld), type);
            List.Add(new ItemDefinition(ItemID.IronShortsword), type);
            List.Add(new ItemDefinition(ItemID.LeadShortsword), type);
            List.Add(new ItemDefinition(ItemID.LeadShortswordOld), type);
            List.Add(new ItemDefinition(ItemID.SilverShortsword), type);
            List.Add(new ItemDefinition(ItemID.SilverShortswordOld), type);
            List.Add(new ItemDefinition(ItemID.TungstenShortsword), type);
            List.Add(new ItemDefinition(ItemID.TungstenShortswordOld), type);
            List.Add(new ItemDefinition(ItemID.GoldShortsword), type);
            List.Add(new ItemDefinition(ItemID.GoldShortswordOld), type);
            List.Add(new ItemDefinition(ItemID.PlatinumShortsword), type);
            List.Add(new ItemDefinition(ItemID.PlatinumShortswordOld), type);
            List.Add(new ItemDefinition(ItemID.Gladius), type);
            //Swords
            type = ItemTypes.Sword;
            List.Add(new ItemDefinition(ItemID.WoodenSword), type);
            List.Add(new ItemDefinition(ItemID.BorealWoodSword), type);
            List.Add(new ItemDefinition(ItemID.CopperBroadsword), type);
            List.Add(new ItemDefinition(ItemID.CopperBroadswordOld), type);
            List.Add(new ItemDefinition(ItemID.PalmWoodSword), type);
            List.Add(new ItemDefinition(ItemID.RichMahoganySword), type);
            List.Add(new ItemDefinition(ItemID.CactusSword), type);
            List.Add(new ItemDefinition(ItemID.TinBroadsword), type);
            List.Add(new ItemDefinition(ItemID.TinBroadswordOld), type);
            List.Add(new ItemDefinition(ItemID.EbonwoodSword), type);
            List.Add(new ItemDefinition(ItemID.IronBroadsword), type);
            List.Add(new ItemDefinition(ItemID.ShadewoodSword), type);
            List.Add(new ItemDefinition(ItemID.LeadBroadsword), type);
            List.Add(new ItemDefinition(ItemID.LeadBroadswordOld), type);
            List.Add(new ItemDefinition(ItemID.SilverBroadsword), type);
            List.Add(new ItemDefinition(ItemID.SilverBroadswordOld), type);
            List.Add(new ItemDefinition(ItemID.GoldBroadsword), type);
            List.Add(new ItemDefinition(ItemID.GoldBroadswordOld), type);
            List.Add(new ItemDefinition(ItemID.TungstenBroadsword), type);
            List.Add(new ItemDefinition(ItemID.TungstenBroadswordOld), type);
            List.Add(new ItemDefinition(ItemID.ZombieArm), type);
            List.Add(new ItemDefinition(ItemID.AshWoodSword), type);
            List.Add(new ItemDefinition(ItemID.Flymeal), type);
            List.Add(new ItemDefinition(ItemID.AntlionClaw), type);
            List.Add(new ItemDefinition(ItemID.PlatinumBroadsword), type);
            List.Add(new ItemDefinition(ItemID.PlatinumBroadswordOld), type);
            List.Add(new ItemDefinition(ItemID.BoneSword), type);
            List.Add(new ItemDefinition(ItemID.BatBat), type);
            List.Add(new ItemDefinition(ItemID.TentacleSpike), type);
            List.Add(new ItemDefinition(ItemID.CandyCaneSword), type);
            List.Add(new ItemDefinition(ItemID.Katana), type);
            List.Add(new ItemDefinition(ItemID.IceBlade), type);
            List.Add(new ItemDefinition(ItemID.LightsBane), type);
            List.Add(new ItemDefinition(ItemID.Muramasa), type);
            List.Add(new ItemDefinition(ItemID.DyeTradersScimitar), type);
            List.Add(new ItemDefinition(ItemID.RedPhaseblade), type);
            List.Add(new ItemDefinition(ItemID.BluePhaseblade), type);
            List.Add(new ItemDefinition(ItemID.GreenPhaseblade), type);
            List.Add(new ItemDefinition(ItemID.WhitePhaseblade), type);
            List.Add(new ItemDefinition(ItemID.OrangePhaseblade), type);
            List.Add(new ItemDefinition(ItemID.PurplePhaseblade), type);
            List.Add(new ItemDefinition(ItemID.YellowPhaseblade), type);
            List.Add(new ItemDefinition(ItemID.BloodButcherer), type);
            List.Add(new ItemDefinition(ItemID.Starfury), type);
            List.Add(new ItemDefinition(ItemID.EnchantedSword), type);
            List.Add(new ItemDefinition(ItemID.PurpleClubberfish), type);
            List.Add(new ItemDefinition(ItemID.BeeKeeper), type);
            List.Add(new ItemDefinition(ItemID.FalconBlade), type);
            List.Add(new ItemDefinition(ItemID.BladeofGrass), type);
            List.Add(new ItemDefinition(ItemID.FieryGreatsword), type);
            List.Add(new ItemDefinition(ItemID.NightsEdge), type);
            List.Add(new ItemDefinition(ItemID.PearlwoodSword), type);
            List.Add(new ItemDefinition(ItemID.CobaltSword), type);
            List.Add(new ItemDefinition(ItemID.PalladiumSword), type);
            List.Add(new ItemDefinition(ItemID.RedPhasesaber), type);
            List.Add(new ItemDefinition(ItemID.BluePhasesaber), type);
            List.Add(new ItemDefinition(ItemID.GreenPhasesaber), type);
            List.Add(new ItemDefinition(ItemID.WhitePhasesaber), type);
            List.Add(new ItemDefinition(ItemID.PurplePhasesaber), type);
            List.Add(new ItemDefinition(ItemID.YellowPhasesaber), type);
            List.Add(new ItemDefinition(ItemID.RedPhasesaberOld), type);
            List.Add(new ItemDefinition(ItemID.BluePhasesaberOld), type);
            List.Add(new ItemDefinition(ItemID.GreenPhasesaberOld), type);
            List.Add(new ItemDefinition(ItemID.WhitePhasesaberOld), type);
            List.Add(new ItemDefinition(ItemID.PurplePhasesaberOld), type);
            List.Add(new ItemDefinition(ItemID.YellowPhasesaberOld), type);
            List.Add(new ItemDefinition(ItemID.IceSickle), type);
            List.Add(new ItemDefinition(ItemID.DD2SquireDemonSword), type);
            List.Add(new ItemDefinition(ItemID.MythrilSword), type);
            List.Add(new ItemDefinition(ItemID.OrichalcumSword), type);
            List.Add(new ItemDefinition(ItemID.BreakerBlade), type);
            List.Add(new ItemDefinition(ItemID.Cutlass), type);
            List.Add(new ItemDefinition(ItemID.Frostbrand), type);
            List.Add(new ItemDefinition(ItemID.AdamantiteSword), type);
            List.Add(new ItemDefinition(ItemID.BeamSword), type);
            List.Add(new ItemDefinition(ItemID.TitaniumSword), type);
            List.Add(new ItemDefinition(ItemID.Bladetongue), type);
            List.Add(new ItemDefinition(ItemID.HamBat), type);
            List.Add(new ItemDefinition(ItemID.Excalibur), type);
            List.Add(new ItemDefinition(ItemID.TrueExcalibur), type);
            List.Add(new ItemDefinition(ItemID.ChlorophyteSaber), type);
            List.Add(new ItemDefinition(ItemID.DeathSickle), type);
            List.Add(new ItemDefinition(ItemID.PsychoKnife), type);
            List.Add(new ItemDefinition(ItemID.Keybrand), type);
            List.Add(new ItemDefinition(ItemID.ChlorophyteClaymore), type);
            List.Add(new ItemDefinition(ItemID.TheHorsemansBlade), type);
            List.Add(new ItemDefinition(ItemID.ChristmasTreeSword), type);
            List.Add(new ItemDefinition(ItemID.TrueNightsEdge), type);
            List.Add(new ItemDefinition(ItemID.Seedler), type);
            List.Add(new ItemDefinition(ItemID.DD2SquireBetsySword), type);
            List.Add(new ItemDefinition(ItemID.TerraBlade), type);
            List.Add(new ItemDefinition(ItemID.InfluxWaver), type);
            List.Add(new ItemDefinition(ItemID.StarWrath), type);
            List.Add(new ItemDefinition(ItemID.Meowmere), type);

            return List;
        }
    }
}