
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.TagDamage;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.Chronoclock
{
    public class TimePulseExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Time Pulse");

        }

        public override void SetDefaults()
        {
            Projectile.width = 600;
            Projectile.height = 600;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.hide = true;

        }
        public bool firstSpawn = true;
        public float sizeX;
        public float sizeY;
        public override bool? CanCutTiles()
        {

            return false;
        }
        public override void AI()
        {


            Player projOwner = Main.player[Projectile.owner];

            Projectile.ai[0] += 1f;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {



        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

            target.AddBuff(BuffType<ChronoclockTagDamage>(), 240);

        }
        public override void OnKill(int timeLeft)
        {

            for (int d = 0; d < 30; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default, 1.5f);
            }

            for (int d = 0; d < 26; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Firework_Blue, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default, 1.5f);
            }
            for (int d = 0; d < 30; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.TreasureSparkle, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 1.5f);
            }

            for (int d = 0; d < 50; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
            }
            base.OnKill(timeLeft);
        }
    }
}
