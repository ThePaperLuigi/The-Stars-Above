using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.ParadiseLost
{
    public class ParadiseLostBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(ModContent.BuffType<Invincibility>(), 10);
            player.immuneTime = 10;
            player.immune = true;
        }
    }
}
