using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    // This file showcases the concept of piercing.
    // This file also shows an animated projectile
    // This file also shows advanced drawing to center the drawn projectile correctly
    /*
	
	NPC.immune determines if an npc can be hit by a item or projectile owned by a particular player (it is an array, each slot corresponds to different players (whoAmI))
	NPC.immune is decremented towards 0 every update
	Melee items set NPC.immune to player.itemAnimation, which starts at item.useAnimation and decrements towards 0

	Projectiles, however, provide mechanisms for custom immunity.
	1. MaxPenetrate = 1: A projectile with penetrate set to 1 will hit regardless of the npc's immunity counters
		Ex: Wooden Arrow. 
	2. No code and penetrate > 1 or -1: npc.immune[owner] will be set to 10. 
		The NPC will be hit if not immune and will become immune to all damage for 10 ticks
		Ex: Unholy Arrow
	3. Override OnHitNPC: If not immune, when it hits it manually set an immune other than 10
		Ex: Arkhalis: Sets it to 5
		Ex: Sharknado Minion: Sets to 20
		Video: https://gfycat.com/DisloyalImprobableHoatzin Notice how Sharknado minion hits prevent Arhalis hits for a brief moment.
	4. projectile.usesIDStaticNPCImmunity and projectile.idStaticNPCHitCooldown: Specifies that a type of projectile has a shared immunity timer for each npc.
		Use this if you want other projectiles a chance to damage, but don't want the same projectile type to hit an npc rapidly.
		Ex: Ghastly Glaive is the only one who uses this.
	5. projectile.usesLocalNPCImmunity and projectile.localNPCHitCooldown: Specifies the projectile manages it's own immunity timers for each npc
		Use this if you want the multiple projectiles of the same type to have a chance to attack rapidly, but don't want a single projectile to hit rapidly. A -1 value prevents the same projectile from ever hitting the npc again.
		Ex: Lightning Aura sentries use this. (localNPCHitCooldown = 3, but other code controls how fast the projectile itself hits) 
			Overlapping Auras all have a chance to hit after each other even though they share the same ID.

	Try the above by uncommenting out the respective bits of code in the projectile below.
	*/

    internal class NalhaunSwing2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 400;
			Projectile.height = 200;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.scale = 1f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.timeLeft = 400;
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
		bool attacking;
		int projectileTimer = 0;
		int timesWillShoot = 10;
		int swingAnimation = -1;
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			//3a: target.immune[projectile.owner] = 20;
			//3b: target.immune[projectile.owner] = 5;
		}

		/*public override Color? GetAlpha(Color lightColor) {
			//return Color.White;
			return new Color(255, 255, 255, 0) * (1f - (float)projectile.alpha / 255f);
		}*/

		public override void AI() {

			swingAnimation--;
			Projectile.ai[0] += 1f;
			//Main.PlaySound(SoundID.Item20, projectile.position);
			if (Projectile.ai[0] > 300f) {
				// Fade out
				Projectile.alpha += 10;
				if (Projectile.alpha > 255) {
					Projectile.alpha = 255;
				}
			}
			else {
				// Fade in
				Projectile.alpha -= 25;
				if (Projectile.alpha < 0) {
					Projectile.alpha = 0;
				}
			}
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++Projectile.frameCounter >= 200) {
				attacking = true;
				Projectile.position.X += 0;
				Projectile.frameCounter = 0;
				Projectile.frame++;
				swingAnimation = 10;

			}
			if(attacking)
            {
				//projectile.position.X += 27;
            }
			projectileTimer++;
			if (projectileTimer >= 5 && timesWillShoot > 0 && attacking)
			{

				for (int i = 0; i < 4; i++)
				{
					// Random upward vector.
					Vector2 vel = new Vector2(-40, Main.rand.Next(-20,20));
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center, vel, ProjectileID.FlamingScythe, 40, Projectile.knockBack, Projectile.owner, 0, 1);
					Vector2 vel2 = new Vector2(-40, Main.rand.Next(-20, 20));
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center, vel2, ProjectileID.FlamingScythe, 40, Projectile.knockBack, Projectile.owner, 0, 1);
				}
				
				projectileTimer = 0;
				timesWillShoot--;
			}
			if(timesWillShoot <= 0 && attacking)
            {
				//projectile.frame++;
				attacking = false;
				
				
			}
			if(swingAnimation == 0)
            {
				Projectile.frame++;
			}				
			// Kill this projectile after 1 second

			//projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;
			//projectile.rotation = projectile.velocity.ToRotation();
			//if (projectile.velocity.Y > 16f) {
			//	projectile.velocity.Y = 16f;
			//}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (Projectile.spriteDirection == -1) {
				//projectile.rotation += MathHelper.Pi;
			}
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 247, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 306, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 307, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
			}
		}

		// Some advanced drawing because the texture image isn't centered or symetrical.
		public override bool PreDraw(ref Color lightColor) {
			
			return true;
		}
	}
}
