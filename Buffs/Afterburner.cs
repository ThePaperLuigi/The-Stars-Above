using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Afterburner : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Afterburner");
            // Description.SetDefault("All attacks will crit");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Generic) = 100;


            /*Mod Calamity = ModLoader.GetMod("CalamityMod");
            if (Calamity != null)
            {
                Calamity.Call("AddRogueCrit", player, 100);
            }*/
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
