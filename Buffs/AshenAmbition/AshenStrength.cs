using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.AshenAmbition
{
    public class AshenStrength : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashen Strength");
            Description.SetDefault("Long-lost ashes are granting massive buffs to health regeneration and damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLife++;
            player.GetDamage(DamageClass.Generic) += 0.7f;
            //player.velocity = Vector2.Zero;
            //player.manaRegen = 0;
        }
    }
}
