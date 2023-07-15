using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.StellarNovas;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.StellarNovas.GuardiansLight
{
    public class SilenceSquallDamageField : ModProjectile
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Projectile.width = 400;
			Projectile.height = 400;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 480;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			
			Projectile.tileCollide = false;
			Projectile.hide = true;
			Projectile.netUpdate = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 60;

		}

		
		public override void AI() {
			Player projOwner = Main.player[Projectile.owner];
			Lighting.AddLight(Projectile.Center, TorchID.Purple);

			Vector2 position = Main.rand.NextVector2FromRectangle(Projectile.Hitbox);
			ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
			particleOrchestraSettings.PositionInWorld = (new Vector2(Projectile.Center.X + Main.rand.Next(-250, 250), Projectile.Center.Y + Main.rand.Next(-250, 250)));
			ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.SilverBulletSparkle, particleOrchestraSettings, Projectile.owner);
			Vector2 vector = new Vector2(
						Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
						Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
			Dust d = Main.dust[Dust.NewDust(
				Projectile.Center + vector, 1, 1,
				DustID.GemSapphire, 0, 0, 255,
				new Color(0.8f, 0.4f, 1f), 0.8f)];
			d.velocity = -vector / 12;
			d.velocity -= Projectile.velocity / 8;
			d.noGravity = true;
			for (int i2 = 0; i2 < 5; i2++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 200f);
				offset.Y += (float)(Math.Cos(angle) * 200f);

				Dust d2 = Dust.NewDustPerfect(Projectile.Center + offset, DustID.FireworkFountain_Blue, Vector2.Zero, 200, default(Color), 0.7f);
				d2.fadeIn = 0.0001f;
				d2.noGravity = true;
			}
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void Kill(int timeLeft)
		{
			Player projOwner = Main.player[Projectile.owner];
			
			
			base.Kill(timeLeft);
        }
    }
}
