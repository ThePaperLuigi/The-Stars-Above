
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Projectiles.Melee.Umbra;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.StellarNovas.FireflyTypeIV
{
	public class FireflyKick : ModProjectile
	{
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.timeLeft = 280;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.light = 1f;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
		bool targetWasHit = false;
        Vector2 targetPosition = Vector2.Zero;
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.FireflyVFX).Draw(Projectile);

            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            targetWasHit = true;
            targetPosition = target.Center;
            Projectile.friendly = false;
            Projectile.ai[1] = 15;
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
                float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
                float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(3));
                float scale = 0.3f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
                float scale = 0.4f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                Main.dust[dustIndex].noGravity = true;

            }

        }
        public override void AI() {
            Player player = Main.player[Projectile.owner];

            if (Projectile.velocity.X < 0)
			{
                Projectile.spriteDirection = -1;
                //DrawOffsetX = -80;

            }
            else
            {
                //DrawOffsetX = -120;

            }
            //DrawOriginOffsetY = -90;
            Projectile.alpha -= 20;
            Projectile.ai[1]--;

            if (Projectile.ai[1] > 0)
            {
                Projectile.velocity *= 0.3f;
                if (Projectile.ai[1] == 1)
                {
                    if (Main.myPlayer == player.whoAmI)
                    {
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), targetPosition, Vector2.Zero, ModContent.ProjectileType<FireflyKickExplosion>(), Projectile.damage, 0f, Main.myPlayer);

                    }
                    int speed = 30;
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_ThundercrashEnd, Projectile.Center);
                    //SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceDurandalHit, Projectile.Center);
                    if (Projectile.direction == 1)
                    {
                        Projectile.velocity = new Vector2(speed, speed);

                    }
                    else
                    {
                        Projectile.velocity = new Vector2(-speed, speed);

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
                        float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
                        float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(3));
                        float scale = 0.3f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
                        float scale = 0.4f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                }
            }
            else
            {
                Projectile.ai[0]++;
            }
			
			//DrawOriginOffsetY = -110;

            if (Projectile.ai[0] > 30)
            {
                if (Projectile.frame == 0)
                {
                    Projectile.frame++;
                    int speed = 30;
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, Projectile.Center);
                    //SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceDurandalHit, Projectile.Center);
                    if(Projectile.direction == 1)
                    {
                        Projectile.velocity = new Vector2(speed, speed);

                    }
                    else
                    {
                        Projectile.velocity = new Vector2(-speed, speed);

                    }
                    for (int g = 0; g < 4; g++)
                    {

                        int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), -Projectile.velocity/20, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), -Projectile.velocity/20, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), -Projectile.velocity/20, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), -Projectile.velocity/20, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(24));
                        float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(36));
                        float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(38));
                        float scale = 0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(37));
                        float scale = 0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                }
                
            }
            else
            {
                Projectile.velocity *= 0.89f;

            }
            if (Projectile.ai[0] > 40)
            {
                Projectile.velocity *= 0.8f;

            }
            if (Projectile.ai[0] > 45)
            {
                Projectile.Kill();
                Projectile.alpha += 80;

            }
            if (Projectile.alpha > 260)
			{
				
			}

		}
		
	}
}
