using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Misc
{
    public class DeathDefianceCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Death Defiance Cooldown");
            // Description.SetDefault("Death Defiance will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
