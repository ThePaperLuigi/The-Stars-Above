
using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Chronoclock
{
    public class TimePulse : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Time Pulse");
			
		}

		public override void SetDefaults() {
			Projectile.width = 750;
			Projectile.height = 750;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Summon;

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
		public float sizeFast;
		public float sizePulse;
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

			int maxSize = 600;

			Projectile.timeLeft = 10;
			sizeX = sizeY = Projectile.ai[0] += 0.1f + MathHelper.Lerp(2, 0, sizeX / maxSize);
			sizeX = sizeY = Math.Clamp(sizeX, 0, maxSize);
			sizeFast += 0.1f + MathHelper.Lerp(14, 0, sizeFast / maxSize);
			sizeFast = Math.Clamp(sizeFast, 0, maxSize);

			
				sizePulse += 0.5f + MathHelper.Lerp(14, 0, sizePulse / sizeX);
			
			if (sizePulse >= sizeX - 20)
            {
				sizePulse = 0;
            }

			Projectile.netUpdate = true;

			

			if ((projOwner.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.Old) || Projectile.ai[0] >= maxSize)
			{
				Projectile.Kill();
			}
			if (sizeX >= maxSize)
            {
				Projectile.ai[0] += 1f;
			}
			

			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player p = Main.player[i];
				if (p.active && !p.dead && p.Distance(Projectile.Center) < sizeX/2)
				{
					p.AddBuff(BuffType<Alacrity>(), 10);
				}

			}

			for (int i = 0; i < 40; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * (sizeX/2));
				offset.Y += (float)(Math.Cos(angle) * (sizeY/2));

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.GemSapphire, Projectile.velocity, 20, default(Color), 1.3f);

				d.fadeIn = 1f;
				d.noGravity = true;
			}
			for (int i = 0; i < 40; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * (sizeX / 2));
				offset.Y += (float)(Math.Cos(angle) * (sizeY / 2));

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.TreasureSparkle, Projectile.velocity, 20, default(Color), 0.5f);

				d.fadeIn = 1f;
				d.noGravity = true;
			}
			for (int i = 0; i < 20; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * (sizeFast / 2));
				offset.Y += (float)(Math.Cos(angle) * (sizeFast / 2));

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.GemSapphire, Projectile.velocity, 20, default(Color), 1f);

				d.fadeIn = 0.1f;
				d.noGravity = true;
			}
			if (sizeX > 250)
			{
				//Periodic pulses for better visual effect.
				for (int i = 0; i < 20; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * (sizePulse / 2));
					offset.Y += (float)(Math.Cos(angle) * (sizePulse / 2));

					Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.GemSapphire, Projectile.velocity, 20, default(Color), 1f);

					d.fadeIn = 0.1f;
					d.noGravity = true;
				}
			}


		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

			
			
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {


           
        }
        public override void OnKill(int timeLeft)
        {
			Projectile.friendly = true;
			

			Player projOwner = Main.player[Projectile.owner];

			int type = ProjectileType<TimePulseExplosion>();
			SoundEngine.PlaySound(StarsAboveAudio.SFX_TimeEffect, Projectile.Center);


			Vector2 position = Projectile.Center;
			float rotation = (float)Math.Atan2(position.Y - (Main.MouseWorld.Y), position.X - (Main.MouseWorld.X));

			int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, Vector2.Zero, type, (int)(Projectile.damage + sizeX), 0f, projOwner.whoAmI);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, Vector2.Zero, ProjectileType<fastRadiate>(), 0, 0f, projOwner.whoAmI);

			Main.projectile[index].originalDamage = Projectile.damage;

			base.OnKill(timeLeft);
        }
    }
}
