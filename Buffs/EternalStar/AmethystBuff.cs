using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.EternalStar
{
    public class AmethystBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Amethyst Fragment");
            // Description.SetDefault("Gain 5% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.manaRegenBonus += 20;
            
            player.GetDamage(DamageClass.Generic) += 0.05f;

            /*Mod Calamity = ModLoader.GetMod("CalamityMod");
            if (Calamity != null)
            {
                Calamity.Call("AddRogueCrit", player, 100);
            }*/
        }
    }
}
