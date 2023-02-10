using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Buffs.Chronoclock
{
    public class ChronoclockMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fragment of Time");
            Description.SetDefault("A fragment of time is aiding you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {/*
            StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.Chronoclock.ChronoclockMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            

            */
        }
    }
}
