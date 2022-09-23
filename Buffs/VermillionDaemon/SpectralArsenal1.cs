using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.VermillionDaemon
{
    public class SpectralArsenal1 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectral Arsenal");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.manaRegenDelay = 10;
            //player.manaRegen = 0;
        }
    }
}
