using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class TakodachiBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Void Octopi");
			Description.SetDefault("The void octopi surround you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Takodachi.TakodachiMinion>()] > 0) {
				modPlayer.TakodachiMinion = true;
			}
			if (!modPlayer.TakodachiMinion) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}

			player.GetDamage(DamageClass.Summon) += (player.ownedProjectileCounts[ProjectileType<Projectiles.Takodachi.TakodachiMinion>()] * 0.01f);

		}
	}
}