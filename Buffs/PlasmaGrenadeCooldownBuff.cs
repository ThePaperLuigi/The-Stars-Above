using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class PlasmaGrenadeCooldownBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Plasma Grenade Cooldown");
            // Description.SetDefault("Plasma Grenade fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
