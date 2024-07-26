
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Celestial.EverlastingPickaxe;
using StarsAbove.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Celestial.EverlastingPickaxe
{
    public class MiningModeGunpowder : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Everlasting Pickaxe");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = GetInstance<Systems.CelestialDamageClass>();
            Projectile.penetrate = 3;
            Projectile.hide = true;
            Projectile.timeLeft = 360;
        }

        // See ExampleBehindTilesProjectile. 
        public override void AI()
        {

            Vector2 center = Projectile.Center + new Vector2(0f, Projectile.height * -0.1f);

            // This creates a randomly rotated vector of length 1, which gets its components multiplied by the parameters
            Vector2 direction = Main.rand.NextVector2CircularEdge(Projectile.width * 0.6f, Projectile.height * 0.6f);
            float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
            Vector2 velocity = Vector2.Zero;

            Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
            dust.scale = 0.5f;
            dust.fadeIn = 1.1f;
            dust.noGravity = true;
            dust.noLight = true;
            dust.alpha = 0;

            if (Main.player[Projectile.owner].HasBuff(BuffType<EverlastingGunpowderMiningTrigger>()))
            {
                if (Main.player[Projectile.owner].Distance(Projectile.Center) < 40)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y), Vector2.Zero, ProjectileType<EverlastingPickaxeExplosion>(), 0, 0, Main.player[Projectile.owner].whoAmI);
                    for (int ix = 0; ix < 5; ix++)
                    {
                        Vector2 position = Vector2.Lerp(Projectile.Center, Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>().playerMousePos, (float)ix / 5);
                        int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, 0, 0, ProjectileType<EverlastingPickaxeExplosionSmall>(), 0, 0f, Main.player[Projectile.owner].whoAmI);

                        Main.projectile[index].originalDamage = Projectile.damage;

                    }
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y - 60), Vector2.Zero, ProjectileID.DD2ExplosiveTrapT3Explosion, 0, 0, Main.player[Projectile.owner].whoAmI);
                    Projectile.Kill();
                }
            }
        }


    }
}