using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class EverlastingLightPreview : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harsh Light");
            Description.SetDefault("Encroaching light blankets the sky in a faint glint" +
                "Nighttime becomes faint; with time, the world may be imprisoned in endless daylight" +
                "");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
        }

    }
}
