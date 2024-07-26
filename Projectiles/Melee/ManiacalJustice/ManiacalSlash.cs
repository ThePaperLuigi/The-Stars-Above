
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Melee.ManiacalJustice;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Melee.ManiacalJustice
{
    public class ManiacalSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Maniacal Justice");

            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 1200;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 500;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Melee;



            Projectile.alpha = 0;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.light = 1f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            DrawOriginOffsetY = 450;
            //DrawOffsetX = 48;
        }


        public override void AI()
        {


            Projectile.ai[0] += 1f;


            if (Projectile.ai[0] == 60)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_ManiacalSlashSpecial, Projectile.position);
                Projectile.scale *= 4;
            }

            if (Projectile.ai[0] >= 60)
            {
                Projectile.friendly = true;

                if (++Projectile.frameCounter >= 3)
                {
                    Projectile.frameCounter = 0;
                    if (Projectile.frame < 7)
                    {
                        Projectile.frame++;
                    }
                    else
                    {
                        Projectile.Kill();
                    }

                }
            }

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }


        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks += 2;

            if (Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks > 100)
            {
                Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks = 100;

            }
            if (Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks < 100 && Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks >= 50)
            {
                target.AddBuff(BuffType<KarmicRetribution>(), 60 * 10);

            }
            OnHitDust(target);

        }

        private void OnHitDust(NPC target)
        {

            float dustAmount = 33f;
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(38f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(38f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
            }
        }
    }
}
