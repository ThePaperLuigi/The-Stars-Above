using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class BlueOrb : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blue Orb");
            Description.SetDefault("Preparing 'Lightbreak Strike'");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            

        }
    }
}
