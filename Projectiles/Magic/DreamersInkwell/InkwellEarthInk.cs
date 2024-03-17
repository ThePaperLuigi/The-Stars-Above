
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.Magic.DreamersInkwell
{
    public class InkwellEarthInk : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Lotus");

        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;

            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 60;
        }

        public override void AI()
        {
            Player projOwner = Main.player[Projectile.owner];

            if (!projOwner.GetModPlayer<WeaponPlayer>().InkwellHeld)
            {
                //Projectile.Kill();
            }



            if (Main.rand.NextBool(5))
            {

                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemTopaz, Vector2.Zero, 0, default, 2f);
                dust.noGravity = true;
                dust.velocity *= 0.9f;
            }
            if (Main.rand.NextBool(15))
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Dirt, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].noGravity = true;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {


            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.boss)
            {
                //target.AddBuff(BuffID.Ichor, 60 * 8);
            }
            else
            {
                target.AddBuff(BuffType<Stun>(), 60);
                Projectile.timeLeft -= 240;
            }

        }
        public override void OnKill(int timeLeft)
        {

        }
    }
}
