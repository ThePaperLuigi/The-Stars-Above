
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Magic.CloakOfAnArbiter;
using StarsAbove.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Magic.CloakOfAnArbiter
{
    public class ArbiterLock : ModProjectile
    {
        public override void SetStaticDefaults()
        {

            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 100;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.light = 0f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public static Texture2D texture;
        public override bool PreDraw(ref Color lightColor)
        {
            
            return true;
        }
        float projectileScaleExtra = 1f;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<LockDebuff>(), 2 * 60);
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, TorchID.Ichor);
            if (Projectile.ai[0] == 0)
            {
                Projectile.scale = 1.5f;
            }
            Projectile.ai[0]++;
            
            if (++Projectile.frameCounter >= 3)
            {
                if (Projectile.frame < 5)
                {
                    Projectile.frameCounter = 0;
                    if(Projectile.frame == 4)
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_ArbiterLock, Projectile.Center);
                        Projectile.friendly = true;
                        for (int i3 = 0; i3 < 60; i3++)
                        {

                            Dust d = Main.dust[Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y + Main.rand.Next(-40,40)), 0, 0, DustID.GemTopaz, 0, Main.rand.Next(-15, 15), 50, default, 1f)];
                            d.fadeIn = 0.3f;
                           
                            d.noGravity = true;
                        }
                    }
                    Projectile.frame++;
                }
                else
                {
                    Projectile.alpha += 6;
                }

            }

            Projectile.velocity *= 0.9f;

            if(projectileScaleExtra > 0f)
            {
                projectileScaleExtra -= 0.01f;
                Projectile.scale -= 0.01f;
            }

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }


        }
    }
}
