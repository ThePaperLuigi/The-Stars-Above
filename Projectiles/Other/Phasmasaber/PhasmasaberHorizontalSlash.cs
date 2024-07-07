using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;

namespace StarsAbove.Projectiles.Other.Phasmasaber
{
    public class PhasmasaberHorizontalSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Catalyst's Memory");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 80;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;               //The width of projectile hitbox
            Projectile.height = 36;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 2;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = ModContent.GetInstance<Systems.ChionicDamageClass>();
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X -= 30;
            hitbox.Y -= 30;
            hitbox.Width += 60;
            hitbox.Height += 60;
            base.ModifyDamageHitbox(ref hitbox);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.SmallPurpleTrail).Draw(Projectile);

            return true;
        }
        public override void AI()
        {
            Projectile.ai[2]++;
            int drawOffset = 0;
            Color projcolor = new Color(174, 0, 255);
            Lighting.AddLight(Projectile.Center, projcolor.ToVector3());

            base.AI();
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = (float)(Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : Math.PI)) + MathHelper.ToRadians(45);

            if (Projectile.ai[2] == 7)
            {
                int dustAmount = 40;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 8f;
                }
            }
            if (Projectile.ai[2] == 14)
            {
                int dustAmount = 40;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 7f;
                }
            }
            if (Projectile.ai[2] == 20)
            {
                int dustAmount = 40;

                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 5f;
                }
            }
            if (Projectile.ai[2] == 1)
            {
                for (int g = 0; g < 4; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f + drawOffset, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), Projectile.velocity / 20, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f + drawOffset, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), Projectile.velocity / 20, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f + drawOffset, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), Projectile.velocity / 20, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f + drawOffset, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), Projectile.velocity / 20, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
                for (int d = 0; d < 20; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(24));
                    float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X + drawOffset, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 20; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(36));
                    float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X + drawOffset, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 20; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(38));
                    float scale = 0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X + drawOffset, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 20; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(37));
                    float scale = 0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X + drawOffset, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Blue, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                    Main.dust[dustIndex].noGravity = true;

                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
            float dustAmount = 54f;
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(115f, 4f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemSapphire);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(115f, 4f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemSapphire);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
            }

            for (int d = 0; d < 43; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
                float scale = 2f + Main.rand.NextFloat() * 0.6f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.BlueCrystalShard, perturbedSpeed.X, perturbedSpeed.Y, 150, default,1f);
                Main.dust[dustIndex].noGravity = true;

            }

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

            modifiers.SetCrit();
        }




        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 8; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.4f);
                Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleCrystalShard, Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8), 150, default, 0.8f);
            }
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            //SoundEngine.PlaySound(SoundID., Projectile.position);
        }
    }
}
