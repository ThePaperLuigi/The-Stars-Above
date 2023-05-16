using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.MorningStar
{
    public class ArcaneArtCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Arcane Art Cooldown");
            // Description.SetDefault("Arcane Art will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
