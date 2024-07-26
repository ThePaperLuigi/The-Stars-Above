using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class SurtrTwilight : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Surtr's Twilight");
            // Description.SetDefault("All attacks will burn foes");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
            {
                player.GetDamage(DamageClass.Generic) *= 1.1f;
                player.manaRegen += 2;
                //
            }
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
