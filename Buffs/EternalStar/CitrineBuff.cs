using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.EternalStar
{
    public class CitrineBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Citrine Fragment");
            // Description.SetDefault("Gain 20 defense and 5% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.statDefense += 20;
            player.GetDamage(DamageClass.Generic) += 0.05f;

            /*Mod Calamity = ModLoader.GetMod("CalamityMod");
            if (Calamity != null)
            {
                Calamity.Call("AddRogueCrit", player, 100);
            }*/
        }
    }
}
