using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.BloodBlade
{
    public class BladeArtDragonPrepBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blade Art: Dragon");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}