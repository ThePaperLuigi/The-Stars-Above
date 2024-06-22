using Microsoft.Xna.Framework;
using StarsAbove.Projectiles.Celestial.BuryTheLight;
using StarsAbove.Projectiles.StellarNovas.FireflyTypeIV;
using StarsAbove.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.StellarNovas.FireflyTypeIV
{
    //
    public class FireflySkillCombo : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 9;
            //DrawOriginOffsetY = 30;
            //DrawOffsetX = -60;
        }
        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 60;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;

            Projectile.hide = false;
            Projectile.ownerHitCheck = false;
            Projectile.tileCollide = false;
            Projectile.friendly = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }

       
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, new Vector3(1f, 0.4f, 0.64f));

            Player projOwner = Main.player[Projectile.owner];
           
            Projectile.netUpdate = true;
            Projectile.scale = 1.2f;
            if (Projectile.frame == 9)
            {

            }

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 9)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        // Random upward vector.
                        Vector2 vel = new Vector2(Projectile.Center.X + Main.rand.Next(-24, 24), Projectile.Center.Y + Main.rand.Next(-24, 24));
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), vel, Vector2.Zero, ProjectileType<FireflySlashFollowUp>(), 0, Projectile.knockBack, Projectile.owner, 0, 1);
                    }
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceDurandalHit, Projectile.Center);
                    Projectile.frame++;
                }
                else
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<FireflyComboFinisher>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);
                   
                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald, 0f + Main.rand.Next(-33, 33), 0f + Main.rand.Next(-33, 33), 150, default, 1.5f);
                    }


                    // Play explosion sound

                    // Smoke Dust spawn
                    for (int i = 0; i < 40; i++)
                    {
                        int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f + Main.rand.Next(-26, 26), 0f + Main.rand.Next(-26, 26), 100, default, 2f);
                        Main.dust[dustIndex].velocity *= 1.4f;
                    }

                    // Large Smoke Gore spawn
                    for (int g = 0; g < 4; g++)
                    {
                        int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    }
                    Projectile.Kill();
                }

            }
           

        }
    }
}