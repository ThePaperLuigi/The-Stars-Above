
using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Celestial.EverlastingPickaxe
{
    public class EverlastingPickaxeExplosionSafe : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Everlasting Pickaxe");

        }

        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;

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
            //Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));

            Projectile.ai[0] += 1f;
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.Center);
            Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
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
            for (int g = 0; g < 4; g++)
            {
                int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1f;
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
        public override void OnKill(int timeLeft)
        {

            base.OnKill(timeLeft);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {



        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

            //target.AddBuff(BuffID.OnFire, 240);

        }
    }
}
