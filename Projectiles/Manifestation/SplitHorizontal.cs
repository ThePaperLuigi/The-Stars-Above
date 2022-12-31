
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Manifestation
{
    public class SplitHorizontal : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Manifestation");
			
		}

		public override void SetDefaults() {
			Projectile.width = 2350;
			Projectile.height = 2350;
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
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1f);
			}
			for (int d = 0; d < 44; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Clentaminator_Red, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
			}
			for (int d = 0; d < 26; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Firework_Red, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default(Color), 1f);
			}
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.Center,0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1f);
			}
			for (int d = 0; d < 40; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Blood, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1f);
			}
			
			
			
			
			// Fade in
			Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			



		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			crit = true;

			
			for (int d = 0; d < 24; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 150, default(Color), 2f);
			}
			

			if (target.life < (int)(target.lifeMax * 0.3) && !target.boss)
			{
				damage = target.life;
			}

		}
    }
}
