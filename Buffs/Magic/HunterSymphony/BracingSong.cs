using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.HunterSymphony
{
    public class BracingSong : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bracing Song");
            // Description.SetDefault("Powerful melodies grant 12 Defense and Knockback Resistance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 12;
            player.noKnockback = true;
        }
    }
}
