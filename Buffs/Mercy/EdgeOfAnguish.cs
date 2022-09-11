using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Mercy
{
    public class EdgeOfAnguish : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Edge of Anguish");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.manaRegenDelay = 10;
            //player.manaRegen = 0;
        }
    }
}
