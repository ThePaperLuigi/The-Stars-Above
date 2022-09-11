using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class HonedEdgeCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honed Edge Cooldown");
            Description.SetDefault("When this debuff ends, you will be able to use Honed Edge again");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
           
        }
    }
}
