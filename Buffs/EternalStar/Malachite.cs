using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.EternalStar
{
    public class MalachiteBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Malachite Fragment");
            Description.SetDefault("Gain 10% increased critical strike chance and 5% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.GetCritChance(DamageClass.Generic) += 0.1f;
            player.GetDamage(DamageClass.Generic) += 0.05f;

            /*Mod Calamity = ModLoader.GetMod("CalamityMod");
            if (Calamity != null)
            {
                Calamity.Call("AddRogueCrit", player, 100);
            }*/
        }
    }
}
