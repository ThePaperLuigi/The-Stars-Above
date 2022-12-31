using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SubworldModifiers
{
    public class ApproachingEvilBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[c/EFB43E:Approaching Evil]");
            Description.SetDefault("A horrifying entity is approaching..." +
                "\n");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

    }
}
