using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class AstarteDriver : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astarte Driver");
            // Description.SetDefault("The stars are granting you strength");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.armorEffectDrawShadow = true;

            player.wingTime = 2;
            player.nebulaLevelDamage = player.GetModPlayer<StarsAbovePlayer>().astarteDriverAttacks;
        }
    }
}
