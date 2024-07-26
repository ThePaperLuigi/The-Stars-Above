using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.Other.OrbitalExpresswayPlush
{
    public class ExpresswayProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hawkmoon");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;               //The width of projectile hitbox
            Projectile.height = 200;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?

            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 2;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void AI()
        {
            Player projOwner = Main.player[Projectile.owner];
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;
            //Destroy nearby hostile projectiles
            Projectile closest = null;
            float closestDistance = 9999999;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
                float distance = Vector2.Distance(projectile.Center, Projectile.Center);


                if (projectile.hostile && projectile.Distance(Projectile.Center) < closestDistance && projectile.damage > 0)
                {
                    closest = projectile;
                    closestDistance = projectile.Distance(Projectile.Center);
                }

                if (closestDistance < 200f && closest.active)
                {
                    for (int d = 0; d < 15; d++)
                    {
                        int dustIndex = Dust.NewDust(closest.Center, 0, 0, DustID.Flare, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, default, 2f);
                        Main.dust[dustIndex].noGravity = true;
                    }
                    closest.Kill();
                    closest.active = false;
                }


            }
            
            base.AI();
        }
        public override Color? GetAlpha(Color lightColor)
        {
            //return Color.White;
            return Color.White;
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                if(Projectile.spriteDirection == 1)
                {
                    Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

                }
                else
                {
                    Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.FlipHorizontally, 0);

                }
            }
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player projOwner = Main.player[Projectile.owner];
            projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -60;

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SetCrit();

            if (Projectile.ai[2] == 0)
            {
                modifiers.SourceDamage += 0.5f;
                //The first hit
                SoundEngine.PlaySound(StarsAboveAudio.SFX_CounterImpact, target.Center);
                Player projOwner = Main.player[Projectile.owner];
                projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -60;
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.Center, 0, 0, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default, 1.5f);
                }
                for (int d = 0; d < 44; d++)
                {
                    Dust.NewDust(target.Center, 0, 0, 0, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
                }
                for (int d = 0; d < 26; d++)
                {
                    Dust.NewDust(target.Center, 0, 0, 133, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default, 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.Center, 0, 0, 7, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 1.5f);
                }
                for (int d = 0; d < 40; d++)
                {
                    Dust.NewDust(target.Center, 0, 0, 269, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 1.5f);
                }
                for (int d = 0; d < 50; d++)
                {
                    Dust.NewDust(target.Center, 0, 0, 78, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
                }
                // Smoke Dust spawn
                for (int i = 0; i < 70; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn
                for (int i = 0; i < 80; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                    dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 3f;
                }
                // Large Smoke Gore spawn
                for (int g = 0; g < 4; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(target.position.X + Projectile.width / 2 - 24f, target.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + Projectile.width / 2 - 24f, target.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + Projectile.width / 2 - 24f, target.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + Projectile.width / 2 - 24f, target.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
            }
            Projectile.ai[2]++;

            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void OnKill(int timeLeft)
        {
        }
    }
}
