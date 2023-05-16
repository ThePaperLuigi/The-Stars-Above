
using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Ozma;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ozma
{
    public class OzmaSpike : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Takonomicon");

		}

		public override void SetDefaults() {
			Projectile.width = 60;
			Projectile.height = 200;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 150;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		public override void AI() {
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player

			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)

			Projectile.spriteDirection = Projectile.direction;


			if (Projectile.timeLeft < 145)
			{
				Projectile.velocity = Vector2.Zero;
			}
			else
            {
				Projectile.rotation = Projectile.ai[0];

			}



			if (Projectile.timeLeft < 50)
			{
				Projectile.alpha += 20;
			}
			else
			{

				Projectile.alpha -= 10;
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
			}
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}

			// These dusts are added later, for the 'ExampleMod' effect

		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.9f);

			}
			target.AddBuff(BuffType<Stun>(), 20);
			Player projOwner = Main.player[Projectile.owner];
			if (crit)
			{
				projOwner.AddBuff(BuffType<AnnihilationState>(), 180);
			}
			 
		}

		
	}
}
