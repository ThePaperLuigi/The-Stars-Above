using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.LevinstormAxe
{
    public class GatheringLevinstormCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gathering Levinstorm Cooldown");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
