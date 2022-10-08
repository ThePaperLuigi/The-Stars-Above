using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class FlashOfEternityCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flash of Eternity Cooldown");
            Description.SetDefault("When this debuff ends, you will be able to use Flash of Eternity again");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
           
        }
    }
}
