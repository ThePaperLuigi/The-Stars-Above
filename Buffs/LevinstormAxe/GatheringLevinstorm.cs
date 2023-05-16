using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.LevinstormAxe
{
    public class GatheringLevinstorm : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gathering Levinstorm");
            // Description.SetDefault("Overcharged electrical energy is enhancing certain attacks");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
