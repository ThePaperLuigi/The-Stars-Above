
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Chronoclock
{
    public class TimePulse : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Time Pulse");
			
		}

		public override void SetDefaults() {
			Projectile.width = 750;
			Projectile.height = 750;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 10;
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
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
			hitbox = new Rectangle((int)(Projectile.Center.X - sizeX/2), (int)(Projectile.Center.Y - sizeY/2), (int)sizeX, (int)sizeY);

			return;
        }
        public override void AI() {
			if(firstSpawn)
            {
				sizeY = 0;
				sizeX = 0;
				firstSpawn = false;
			}

			Player projOwner = Main.player[Projectile.owner];
			Projectile.timeLeft = 10;
			sizeX = sizeY += 0.5f;
			sizeX = sizeY = Math.Clamp(sizeX, 0, 400);
			if(sizeX >= 400)
            {
				Projectile.Kill();
            }
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player p = Main.player[i];
				if (p.active && !p.dead && p.Distance(Projectile.Center) < sizeX/2)
				{
					p.AddBuff(BuffID.Ironskin, 10);//Replace with different buff
				}

			}
			for (int i = 0; i < 90; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * (sizeX/2));
				offset.Y += (float)(Math.Cos(angle) * (sizeY/2));

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.GemSapphire, Projectile.velocity, 20, default(Color), 0.4f);

				d.fadeIn = 1f;
				d.noGravity = true;
			}

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
			Projectile.friendly = true;

			Projectile.ai[1] = 0;
			int type = ProjectileType<TimePulseExplosion>();
			SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.Center);


			Vector2 position = Projectile.Center;
			float rotation = (float)Math.Atan2(position.Y - (Main.MouseWorld.Y), position.X - (Main.MouseWorld.X));

			float launchSpeed = 0f;
			Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
			Vector2 velocity = direction * launchSpeed;

			int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage + sizeX, 0f, projOwner.whoAmI);

			Main.projectile[index].originalDamage = Projectile.damage;

			base.Kill(timeLeft);
        }
    }
}
