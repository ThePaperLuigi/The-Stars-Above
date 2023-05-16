using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.RedMage
{
    public class RedMageHeldBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Duelist's Crimson");
            // Description.SetDefault("Exhibiting proficiency in maiming and mending");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
                   
        }
    }
}
