using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Youmu
{
    public class PhantomMinion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Phantom Spirit");
            // Description.SetDefault("A phantom spirit is attacking foes");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Youmu.YoumuSpirit>()] > 0)
			{
				modPlayer.YoumuMinion = true;
			}
			if (!modPlayer.YoumuMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;

			}
		}
	}
}
