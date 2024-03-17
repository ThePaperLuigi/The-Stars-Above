using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.AshenAmbition
{
    public class AshenAmbitionCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ashen Ambition Cooldown");
            // Description.SetDefault("Ashen Ambition will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
