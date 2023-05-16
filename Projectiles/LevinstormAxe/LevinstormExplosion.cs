
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.LevinstormAxe
{
    public class LevinstormExplosion : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Boltstorm Axe");
			
		}

		public override void SetDefaults() {
			Projectile.width = 150;
			Projectile.height = 150;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.tileCollide = false;

		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {
			//Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));

			Projectile.ai[0] += 1f;

			
			for (int d = 0; d < 5; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 7, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 6; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 269, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 7; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 78, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 150, default(Color), 1.5f);
			}
			// Smoke Dust spawn
			for (int i = 0; i <10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Fire Dust spawn
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			

			// Fade in
			Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

			
			
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

           
        }
    }
}
