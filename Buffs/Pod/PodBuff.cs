using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Buffs.Pod
{
    public class PodBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Automaton Pod");
            // Description.SetDefault("An automaton pod is pressing the attack");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.Pod.PodMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            


        }
    }
}
