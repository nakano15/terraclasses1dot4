using Terraria;
using Terraria.ModLoader;

namespace terraclasses
{
    public class NpcMod : GlobalNPC
    {
        public override bool IsCloneable => false;
        protected override bool CloneNewInstances => false;

        public static int CPReward = 0;

        public override void SetDefaults(NPC entity)
        {
            CPReward = (int)(entity.lifeMax / 8 + (entity.damage * 2 + entity.defense) / 4);
        }

        public override void OnKill(NPC npc)
        {
            for (int i = 0; i < 255; i++)
            {
                if (npc.playerInteraction[i] && Main.player[i].active)
                {
                    PlayerMod.GiveCP(Main.player[i], CPReward, npc.getRect());
                }
            }
        }
    }
}