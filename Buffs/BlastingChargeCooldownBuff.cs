using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class BlastingChargeCooldownBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blasting Charge Cooldown");
            Description.SetDefault("Blasting Charge fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
