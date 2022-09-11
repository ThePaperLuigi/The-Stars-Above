using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SkyStrikerBuffs
{
    public class StrikerShieldBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aerial Forme: Defender");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.aggro += 30;
            player.thorns = 0.80f;
            player.statDefense += 20;
        }
    }
}
