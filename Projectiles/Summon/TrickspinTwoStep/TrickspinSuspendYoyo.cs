
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using Terraria.Audio;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.Summon.TrickspinTwoStep
{

    public class TrickspinSuspendYoyo : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = true;
            //ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            //ProjectileID.Sets.Homing[Projectile.type] = true;
            //ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 240;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            //ProjectileType<TakodachiRound>();


        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            float rotationsPerSecond = 0.3f;
            bool rotateClockwise = true;
            //The rotation is set here
            Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);


            Player owner = Main.player[Projectile.owner];
            owner.GetModPlayer<WeaponPlayer>().TrickspinCenter = Projectile.Center;

            if (!CheckActive(owner))
            {
                return;
            }
            for (int i3 = 0; i3 < 50; i3++)
            {
                Vector2 position = Vector2.Lerp(owner.Center, Projectile.Center, (float)i3 / 50);
                Dust d = Dust.NewDustPerfect(position, DustID.GemDiamond, null, 240, default, 0.2f);
                d.fadeIn = 0f;
                d.velocity = Vector2.Zero;
                d.noGravity = true;
            }
            Projectile.velocity *= 0.94f;

            if ((Projectile.velocity.X < 1 || Projectile.velocity.X > -1) && Projectile.ai[0] < 180)
            {
                owner.GetModPlayer<WeaponPlayer>().TrickspinReady = true;
                if (StarsAbove.weaponActionKey.JustPressed && owner.whoAmI == Main.myPlayer && Projectile.ai[0] > 20)
                {
                    Projectile.ai[0] = 180;
                }
                for (int i2 = 0; i2 < 5; i2++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 400f);
                    offset.Y += (float)(Math.Cos(angle) * 400f);

                    Dust d2 = Dust.NewDustPerfect(Projectile.Center + offset, DustID.GemTopaz, Projectile.velocity, 0, default, 0.7f);
                    d2.fadeIn = 0.0001f;
                    d2.noGravity = true;
                }

                Projectile.friendly = false;

            }
            else
            {
                owner.GetModPlayer<WeaponPlayer>().TrickspinReady = false;

                Projectile.friendly = true;
            }

            if (Projectile.Distance(owner.Center) > 400)
            {
                Projectile.ai[0] = 180;

            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 180)
            {
                Projectile.ai[1]++;

                Projectile.position.X = MathHelper.Lerp(Projectile.position.X, owner.Center.X - Projectile.width / 2, Projectile.ai[1] / 60);
                Projectile.position.Y = MathHelper.Lerp(Projectile.position.Y, owner.Center.Y - Projectile.height / 2, Projectile.ai[1] / 60);

            }
            if (Projectile.ai[1] >= 20)
            {
                Projectile.Kill();
            }
        }


        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {


                return false;
            }


            return true;
        }


        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);

            float dustAmount = 20f;
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
            }
        }

    }
}

