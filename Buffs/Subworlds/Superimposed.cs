using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Subworlds
{
    public class Superimposed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[c/3B9FE4:Cosmic Voyager]");
            /* Description.SetDefault("Corporeal body is transposed into the sea of stars" +
                "\nThe following actions have been restricted:" +
                "\nBuilding, mining, mounts, explosives, flying, and the Rod of Discord" +
                "\nAbilities that scale with modded world progression have reverted to initial values"); */
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing

        }

        public override void Update(Player player, ref int buffIndex)
        {
           
            player.mount.Dismount(player);
            player.noFallDmg = true;
            player.wingTime = 0;
            player.wingsLogic = 0;
            player.wings = 0;
            //player.rocketTime = 0;
            //player.rocketTimeMax = 0;
            //player.carpetTime = 0;
            //player.rocketTime = 0;
            //player.rocketTimeMax = 0;
            
            //player.chaosState = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
           
            
            
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
