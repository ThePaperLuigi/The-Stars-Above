using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    //
    public class InugamiRipsaw : ModProjectile
	{
		public override void SetDefaults() {
			Projectile.width = 26;
			Projectile.height = 68;
			Projectile.aiStyle = 20;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.ContinuouslyUpdateDamageStats = true;
		}

		public override void AI() {
			//int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.9f);
			//Main.dust[dust].noGravity = true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			//if (Main.rand.NextBool(10)) {
			//	target.AddBuff(BuffID.OnFire, 180, false);
			//}
		}

		//public override void OnHitPvp(Player target, int damage, bool crit) {
		//	if (Main.rand.NextBool(10)) {
		//		target.AddBuff(BuffID.OnFire, 180, false);
		//	}
		//}
	}
}