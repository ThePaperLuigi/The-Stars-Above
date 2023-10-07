
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Projectiles.Generics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.DragaliaFound
{
    public class DragaliaFoundSwordRecoil : StarsAboveSword
    {
        public override string Texture => "StarsAbove/Projectiles/Summon/DragaliaFound/DragaliaFoundSword";
        public override bool UseRecoil => true;
        public override bool DoSpin => false;
        public override float BaseDistance => 50;
        public override Color BackDarkColor => new Color(184, 236, 104);
        public override Color MiddleMediumColor => new Color(255, 255, 80);
        public override Color FrontLightColor => new Color(150, 240, 255);
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 132;
            Projectile.height = 132;
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

            // You could also spawn dusts at the enemy position. Here is simple an example:
            // Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, ModContent.DustType<Content.Dusts.Sparkle>());

            // Set the target's hit direction to away from the player so the knockback is in the correct direction.
            hit.HitDirection = Main.player[Projectile.owner].Center.X < target.Center.X ? 1 : -1;
        }
        public override void OnKill(int timeLeft)
        {


        }

    }
}
