using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class StarshieldCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starshield Cooldown");
            // Description.SetDefault("Starshield will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.statDefense += player.statLifeMax2 / 10;
        }

        
    }
}
