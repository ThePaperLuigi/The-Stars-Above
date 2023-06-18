using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SubworldModifiers
{
    public class AnomalyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[c/EFB43E:Anomaly Voyage]");
            /* Description.SetDefault("The nearby surroundings disobey laws of reality" +
                "\nLingering for too long will result in a catastrophic danger approaching"); */
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

    }
}
