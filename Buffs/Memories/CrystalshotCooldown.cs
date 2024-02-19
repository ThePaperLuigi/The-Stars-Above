using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Memories
{
    public class CrystalshotCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blade Will Cooldown");
            // Description.SetDefault("Blade Will will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
