using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StarfarerAttire
{
    public class LucentBliss : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lucent Bliss");
            Description.SetDefault("Faerie luck has blessed you, increasing maximum Luck and granting Luck");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.luckMaximumCap += 10f;
            player.luck += 2f;
           
        }
    }
}
