
using Microsoft.Xna.Framework;
using Terraria;using Terraria.GameContent;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ozma
{
	public class OzmaAttack5Slash2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ozma Ascendant");
			//DrawOriginOffsetY = 12;
			Main.projFrames[Projectile.type] = 8;

		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 255;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			DrawOffsetX = -50;
			DrawOriginOffsetY = -50;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 5;
		}
		bool firstSpawn = true;
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!

		public override void AI() {
			if(firstSpawn)
            {
				//Projectile.rotation = MathHelper.ToRadians(Main.rand.NextFloat(-360, 360));
				Projectile.scale = 1.6f;
				firstSpawn = false;
            }
			float rotationsPerSecond = 6f;
			bool rotateClockwise = true;
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			
			
			
			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 8)
				{

					Projectile.frame++;
				}
				else
				{
					Projectile.Kill();

				}

			}
		}
	}
}
