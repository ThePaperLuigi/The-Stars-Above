using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Buffs.Adornment
{
    public class AdornmentMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adornment of the Chaotic God");
            Description.SetDefault("A manifestation of chaos is attacking your foes");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.Adornment.AdornmentMinion>()] > 0)
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
