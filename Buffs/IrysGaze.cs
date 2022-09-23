using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class IrysGaze : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Irys Gaze");
            Description.SetDefault("Can be crit by minions");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            //player.wingTime = 10;
            
            
        }
    }
}
