using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.EternalStar
{
    public class CeruleanBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cerulean Fragment");
            // Description.SetDefault("Gain 5% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {


            player.GetDamage(DamageClass.Generic) += 0.05f;

            /*Mod Calamity = ModLoader.GetMod("CalamityMod");
            if (Calamity != null)
            {
                Calamity.Call("AddRogueCrit", player, 100);
            }*/
        }
    }
}
