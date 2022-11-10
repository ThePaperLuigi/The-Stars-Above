using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SoulReaver
{
    public class DarkSoul : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Soul");
            Description.SetDefault("Harvested souls are granting an increase to offensive capabilities");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += (float)(player.GetModPlayer<StarsAbovePlayer>().SoulReaverSouls * 0.02);
        }
    }
}