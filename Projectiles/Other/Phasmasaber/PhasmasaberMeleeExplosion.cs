
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Other.Phasmasaber;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Other.Phasmasaber
{
    public class PhasmasaberMeleeExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sunset of the Sun God");

        }

        public override void SetDefaults()
        {
            Projectile.width = 1750;
            Projectile.height = 1750;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

       
        public override void AI()
        {
            Player projOwner = Main.player[Projectile.owner];
            projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.Center);

            float dustAmount = 120f;
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(5f, 5f);
                //spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = spinningpoint5.SafeNormalize(Vector2.UnitY) * 50;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(5f, 5f);
                //spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = spinningpoint5.SafeNormalize(Vector2.UnitY) * 40;
            }

            Projectile.ai[0] += 1f;
            
            for (int d = 0; d < 40; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleCrystalShard, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 150, default, 1.5f);
            }
            for (int d = 0; d < 30; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.BlueCrystalShard, 0f + Main.rand.Next(-23, 23), 0f + Main.rand.Next(-23, 23), 150, default, 1.5f);
            }
            for (int d = 0; d < 40; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.PinkCrystalShard, 0f + Main.rand.Next(-23, 23), 0f + Main.rand.Next(-23, 23), 150, default, 1.5f);
            }
            for (int d = 0; d < 50; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
            }
            // Smoke Dust spawn
            for (int i = 0; i < 70; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.BlueCrystalShard, 0f + Main.rand.Next(-26, 26), 0f + Main.rand.Next(-26, 26), 100, default, 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.PinkCrystalShard, 0f + Main.rand.Next(-26, 26), 0f + Main.rand.Next(-26, 26), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 3f;
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

            // Fade in
            Projectile.alpha--;
            if (Projectile.alpha < 100)
            {
                Projectile.alpha = 100;
            }


        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.AddBuff(ModContent.BuffType<SpiritflameDebuff>(), 60 * 10);
            modifiers.SetCrit();

        }

    }
}
