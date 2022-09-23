using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Chemtank
{
    public class ChemtankDashCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Disdain Cooldown");
            Description.SetDefault("Disdain will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
