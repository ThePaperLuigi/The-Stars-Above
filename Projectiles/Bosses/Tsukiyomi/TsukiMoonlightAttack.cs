using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using StarsAbove.Effects;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{

    internal class TsukiMoonlightAttack : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Carian Dark Moon");     //The English name of the projectile

			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 160;
			Projectile.height = 160;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			

			//1: projectile.penetrate = 1; // Will hit even if npc is currently immune to player
			//2a: projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
			//2b: projectile.penetrate = 3; // Same, but max 3 hits before dying
			//5: projectile.usesLocalNPCImmunity = true;
			//5a: projectile.localNPCHitCooldown = -1; // 1 hit per npc max
			//5b: projectile.localNPCHitCooldown = 20; // o
		}
		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			hitbox.Width /= 2;
			hitbox.Height /= 2;


			base.ModifyDamageHitbox(ref hitbox);
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			
		}

		public override Color? GetAlpha(Color lightColor) {
			//return Color.White;
			return new Color(255, 255, 255, 0) * (1f - (float)Projectile.alpha / 255f);
		}

		public override void AI() {

			Projectile.ai[0] += 1f;
			
				Projectile.alpha -= 25;
				if (Projectile.alpha < 100) {
					Projectile.alpha = 100;
				}
			
			// Slow down
			Projectile.velocity *= 0.98f;
			//Projectile.scale = (int)(Projectile.scale * 1.9);
			if (Projectile.ai[0] >= 50f)
			{

				Projectile.scale -= 0.05f;
			}


			// Kill this projectile after 1 second
			if (Projectile.ai[0] >= 60f) {
				
				for (int d = 0; d < 16; d++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default(Color), 1f);
				}
				for (int d = 0; d < 12; d++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6,6), 150, default(Color), 1f);
				}
				Projectile.Kill();
			}
			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 16f) {
				Projectile.velocity.Y = 16f;
			}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation += MathHelper.Pi;
			}
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 247, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
				
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(LargeBlueTrail).Draw(Projectile);

			return true;
		}
	}
}
