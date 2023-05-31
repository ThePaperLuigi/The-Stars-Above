using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    
    internal class TheBitterEndProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults() {
			Projectile.width = 500;
			Projectile.height = 500;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.scale = 1f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;

			//1: projectile.penetrate = 1; // Will hit even if npc is currently immune to player
			//2a: projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
			//2b: projectile.penetrate = 3; // Same, but max 3 hits before dying
			//5: projectile.usesLocalNPCImmunity = true;
			//5a: projectile.localNPCHitCooldown = -1; // 1 hit per npc max
			//5b: projectile.localNPCHitCooldown = 20; // o
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			//3a: target.immune[projectile.owner] = 20;
			//3b: target.immune[projectile.owner] = 5;
		}

		public override Color? GetAlpha(Color lightColor) {
			//return Color.White;
			return new Color(255, 255, 255, 0) * (1f - (float)Projectile.alpha / 255f);
		}

		public override void AI() {

			Projectile.ai[0] += 1f;
			SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
			if (Projectile.ai[0] > 40f) {
				// Fade out
				Projectile.alpha += 10;
				if (Projectile.alpha > 255) {
					Projectile.alpha = 255;
				}
			}
			else {
				// Fade in
				Projectile.alpha -= 25;
				if (Projectile.alpha < 100) {
					Projectile.alpha = 100;
				}
			}
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++Projectile.frameCounter >= 10) {
				Projectile.frameCounter = 0;
				Projectile.frame++;
			}
			// Kill this projectile after 1 second
			if (Projectile.frame > 4) {
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
			
		}

		// Some advanced drawing because the texture image isn't centered or symetrical.
		public override bool PreDraw(ref Color lightColor) {
			
			return true;
		}
	}
}
