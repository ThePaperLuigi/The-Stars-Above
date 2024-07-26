using Microsoft.Xna.Framework;
using StarsAbove.Effects;
using StarsAbove.Projectiles.Celestial.BuryTheLight;
using StarsAbove.Projectiles.Other.DreadmotherDarkIdol;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Other.Wolvesbane
{
    //
    public class WolvesbaneRearmedShockwave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
            Main.projFrames[Projectile.type] = 4;
            //DrawOriginOffsetY = 30;
            //DrawOffsetX = -60;
        }
        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 150;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;

            Projectile.hide = false;
            Projectile.ownerHitCheck = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
            DrawOriginOffsetY = 0;
        }
       
        public override bool PreDraw(ref Color lightColor)
        {
            
            return true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            //return Color.White;
            return new Color(255, 255, 255, 0) * (1f - Projectile.alpha / 255f);
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, new Vector3(1f, 1f, 1f));
            Projectile.velocity *= 0.95f;
            // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            // Sadly, Projectile/ModProjectile does not have its own
            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);

            if (Projectile.frame >= 3)
            {
                Projectile.alpha += 22;
            }

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 3)
                {
                    Projectile.frame++;
                }
                else
                {

                }

            }

            if (Projectile.alpha >= 250)
            {
                Projectile.Kill();
            }
            Projectile.scale += 0.001f;
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();//+ MathHelper.ToRadians(0f);
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(180f);
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SetCrit();

            if (Projectile.owner == Main.myPlayer)
            {
                for(int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<WolvesbaneRearmedSlash>(), 0, 0, Main.player[Projectile.owner].whoAmI);

                }
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player projOwner = Main.player[Projectile.owner];
            Projectile.damage /= 2;
            for (int d = 0; d < 18; d++)
            {
                Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-5, 5), 150, default, 0.4f);

            }
            

        }

    }
}