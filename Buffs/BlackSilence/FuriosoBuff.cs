using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.BlackSilence
{
    public class FuriosoBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Black Silence");
            Description.SetDefault("Mastery of armaments, speed, and skill has granted a myriad of buffs while cycling through weapons of the Black Silence");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Do this in ModPlayer so Furioso can activate them all.
            
        }
    }
}
