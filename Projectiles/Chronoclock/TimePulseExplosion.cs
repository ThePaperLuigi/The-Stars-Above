
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Chronoclock
{
    public class TimePulseExplosion : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Time Pulse");
			
		}

		public override void SetDefaults() {
			Projectile.width = 400;
			Projectile.height = 400;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = 1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.hide = true;

		}
		public bool firstSpawn = true;
		public float sizeX;
		public float sizeY;
        
        public override void AI() {
			

			Player projOwner = Main.player[Projectile.owner];
			
			Projectile.ai[0] += 1f;
			
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

			
			
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {


           
        }
        public override void Kill(int timeLeft)
        {
			
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
			}
			
			for (int d = 0; d < 26; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Firework_Blue, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.BlueFairy, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1.5f);
			}
			
			for (int d = 0; d < 50; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.BlueTorch, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
			}
			base.Kill(timeLeft);
        }
    }
}
