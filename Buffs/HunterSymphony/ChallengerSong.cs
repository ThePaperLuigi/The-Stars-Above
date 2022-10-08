using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.HunterSymphony
{
    public class ChallengerSong : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Challenger Song");
            Description.SetDefault("Powerful melodies grant 14% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.14f;
        }
    }
}
