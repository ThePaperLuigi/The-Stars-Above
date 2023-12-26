using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Kifrosse
{
    public class KifrosseBuff1 : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Kifrosse (1 Tail)");
			// Description.SetDefault("Mystic energy chills the air around you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.Kifrosse.Kifrosse1>()] > 0) {
				modPlayer.Kifrosse1 = true;
			}
			if (!modPlayer.Kifrosse1) {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else {
				player.buffTime[buffIndex] = 18000;

			}

			player.GetDamage(DamageClass.Summon) += 0.05f;
		}
	}
}