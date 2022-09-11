using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class DashInvincibility : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dash Invincibility");
            Description.SetDefault("You will not take damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
