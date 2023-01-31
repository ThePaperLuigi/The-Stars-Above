using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SaltwaterScourge
{
    public class CannonfireDelugeCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cannonfire Deluge Cooldown");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

    }
}
