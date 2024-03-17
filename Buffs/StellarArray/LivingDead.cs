using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarArray
{
    public class LivingDead : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Living Dead");
            // Description.SetDefault("Dark magic keeps you from death; if you are not healed to 150 HP before Living Dead ends, you will die");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {


        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }

    }
}
