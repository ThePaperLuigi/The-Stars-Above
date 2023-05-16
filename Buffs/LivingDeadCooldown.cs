using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class LivingDeadCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Living Dead Cooldown");
            // Description.SetDefault("Living Dead will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
