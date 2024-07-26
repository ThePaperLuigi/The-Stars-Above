using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Misc
{
    public class UmbralAspectDepletedBow : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Umbral Depletion");
            // Description.SetDefault("The Umbral Aspect has drained your energy; Swap to Astral Aspect to recover");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
