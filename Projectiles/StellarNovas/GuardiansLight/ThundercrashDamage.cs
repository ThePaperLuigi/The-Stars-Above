using Microsoft.Xna.Framework;
using StarsAbove.Buffs.StellarNovas;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.StellarNovas.GuardiansLight
{
    public class ThundercrashDamage : ModProjectile
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Projectile.width = 150;
			Projectile.height = 150;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 60;
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
			Projectile.localNPCHitCooldown = -1;

		}

		
		public override void AI() {
			Player projOwner = Main.player[Projectile.owner];

			Projectile.Center = projOwner.Center;

		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Projectile.timeLeft = 1;
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void Kill(int timeLeft)
		{
			Player projOwner = Main.player[Projectile.owner];
			projOwner.ClearBuff(BuffType<ThundercrashActive>());
			projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
			projOwner.fullRotation = 0f;

			//Boom
			SoundEngine.PlaySound(StarsAboveAudio.SFX_ThundercrashEnd, Projectile.Center);
			for (int d = 0; d < 50; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, 0f + Main.rand.Next(-50, 50), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 54; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
			}

			// Smoke Dust spawn
			for (int i = 0; i < 30; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Large Smoke Gore spawn
			for (int g = 0; g < 7; g++)
			{
				int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 4.5f + Main.rand.Next(-18, 18);
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 4.5f + Main.rand.Next(-18, 18);
				goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 4.5f + Main.rand.Next(-18, 18);
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 4.5f + Main.rand.Next(-18, 18);
				goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 4.5f + Main.rand.Next(-18, 18);
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 4.5f + Main.rand.Next(-18, 18);
				goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 4.5f + Main.rand.Next(-18, 18);
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 4.5f + Main.rand.Next(-18, 18);
			}

			base.Kill(timeLeft);
        }
    }
}
