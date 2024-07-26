
using Microsoft.Xna.Framework;
 
using StarsAbove.Projectiles.Generics;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.KazimierzSeraphim
{
    public class KazimierzSeraphimSword : StarsAboveSword
    {
        public override string Texture => "StarsAbove/Projectiles/Summon/KazimierzSeraphim/KazimierzSeraphimSword";
        public override bool UseRecoil => false;
        public override bool DoSpin => false;
        public override float BaseDistance => 70;
        public override Color BackDarkColor => new Color(238, 183, 99);
        public override Color MiddleMediumColor => new Color(244, 216, 137);
        public override Color FrontLightColor => new Color(244, 245, 208);
        public override bool CenterOnPlayer => true;
        public override bool Rotate45Degrees => false;
        public override float EffectScaleAdder => 1.3f;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.SummonMeleeSpeed;
            Projectile.width = 182;
            Projectile.height = 182;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            //DrawOriginOffsetY = -6;
            return true;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X -= 30;
            hitbox.Y -= 30;
            hitbox.Width += 60;
            hitbox.Height += 60;
            base.ModifyDamageHitbox(ref hitbox);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Vanilla has several particles that can easily be used anywhere.
            // The particles from the Particle Orchestra are predefined by vanilla and most can not be customized that much.
            // Use auto complete to see the other ParticleOrchestraType types there are.
            // Here we are spawning the Excalibur particle randomly inside of the target's hitbox.
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);

            Player player = Main.player[Projectile.owner];
            player.GetModPlayer<WeaponPlayer>().radiance++;
            if (player.GetModPlayer<WeaponPlayer>().radiance >= 5)
            {
                Dust dust;
                for (int d = 0; d < 40; d++)
                {
                    dust = Main.dust[Dust.NewDust(player.Center, 1, 1, 269, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 0, new Color(255, 255, 255), 1f)];
                }
            }

            // Set the target's hit direction to away from the player so the knockback is in the correct direction.
            hit.HitDirection = Main.player[Projectile.owner].Center.X < target.Center.X ? 1 : -1;
        }
        public override void OnKill(int timeLeft)
        {


        }

    }
}
