
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Ranged.ShockAndAwe;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ranged.ShockAndAwe
{
    public class ShockAndAweRocketExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
        }


        public override void AI()
        {
            Projectile.netUpdate = true;
            //Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player p = Main.player[i];
                if (p.active && !p.dead && p.Distance(Projectile.Center) < 180f)
                {
                    float launchSpeed = 23f - p.Distance(Projectile.Center) / 8;
                    Vector2 position = Projectile.Center;
                    Vector2 direction = Vector2.Normalize(position - p.Center);
                    Vector2 arrowVelocity = direction * launchSpeed;
                    p.velocity -= arrowVelocity;
                    p.velocity.X = MathHelper.Clamp(p.velocity.X, -18f, 18f);
                    if (Projectile.owner == p.whoAmI)
                    {
                        p.Hurt(PlayerDeathReason.ByCustomReason(p.name + LangHelper.GetTextValue("DeathReason.ShockAndAwe")), (int)(p.statLifeMax2 * 0.05f), 0, false, false, -1, false, 0, 0, 0);
                        p.AddBuff(BuffType<DeathFromAbove>(), 240);
                    }
                }

            }
            Projectile.netUpdate = true;

            Projectile.ai[0] += 1f;

            float dustAmount = 40f;
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 18f;
            }

            for (int d = 0; d < 5; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 7, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default, 1.5f);
            }
            for (int d = 0; d < 6; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 269, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default, 1.5f);
            }
            for (int d = 0; d < 7; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 78, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 150, default, 1.5f);
            }
            // Smoke Dust spawn
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }


            // Fade in
            Projectile.alpha--;
            if (Projectile.alpha < 100)
            {
                Projectile.alpha = 100;
            }


        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {



        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!target.boss && target.CanBeChasedBy())
            {
                float launchSpeed = 4f;
                Vector2 position = Projectile.Center;
                Vector2 direction = Vector2.Normalize(position - target.Center);
                Vector2 arrowVelocity = direction * launchSpeed;
                target.velocity -= arrowVelocity;
                target.velocity.Y -= 12;
            }

        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.FinalDamage *= 0f;
            modifiers.FinalDamage.Flat += 10;

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.NonCritDamage += 0.2f;
            modifiers.CritDamage += 0.4f;

        }
    }
}
