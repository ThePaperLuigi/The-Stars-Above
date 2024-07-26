using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.Gundbits
{
    public class GundbitLaserCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Core of Flames Cooldown");
            // Description.SetDefault("Liberation Blazing will not activate Core of Flames when this debuff is active");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
