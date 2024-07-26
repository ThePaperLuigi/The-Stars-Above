
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Magic.CarianDarkMoon
{
    public class DarkmoonSpawn : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Carian Dark Moon");
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 108;
            Projectile.height = 108;
            Projectile.aiStyle = 1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 160;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hide = false;
            Projectile.ownerHitCheck = false;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            AIType = ProjectileID.Bullet;
        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        // It appears that for this AI, only the ai0 field is used!
        public override void AI()
        {
            // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            // Sadly, Projectile/ModProjectile does not have its own
            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            projOwner.itemTime = 10;
            projOwner.itemAnimation = 10;
            //projectile.position.X = projOwner.Center.X - (float)(projectile.width / 2);
            // Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
            // MathHelper.ToRadians(xx degrees here)
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
            // Offset by 90 degrees here
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(90f);
            }
            Projectile.velocity *= 0.90f;
            if (Projectile.timeLeft < 140 && Projectile.timeLeft > 120)
            {

                Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-9, 9), 150, default, 0.2f);

            }
            if (Projectile.timeLeft < 120 && Projectile.timeLeft > 100)
            {

                Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-9, 9), 150, default, 0.6f);

            }
            if (Projectile.timeLeft < 100 && Projectile.timeLeft > 80)
            {

                Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-9, 9), 150, default, 1.1f);

            }
            if (Projectile.timeLeft < 80 && Projectile.timeLeft > 60)
            {
                for (int d = 0; d < 3; d++)
                {
                    Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-14, 14), 150, default, 1.5f);
                }
            }

            if (Projectile.timeLeft == 60)
            {
                //Change the projectile to empowered version, add particle effects, etc.
               

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, -0.01f, ProjectileType<DarkmoonSpawnEmpowered>(), 0, 0, projOwner.whoAmI, 0f);//Spawn the sword.

                for (int d = 0; d < 14; d++)
                {
                    Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
                }
                for (int d = 0; d < 16; d++)
                {
                    Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-16, 16), 0f, 150, default, 1.5f);
                }
                for (int d = 0; d < 16; d++)
                {
                    Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f, 150, default, 1.5f);
                }
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(new Vector2(Projectile.Center.X - 3, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-23, 23), 150, default, 1.5f);
                }


                // Play explosion sound

                // Smoke Dust spawn
                for (int i = 0; i < 20; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }

                // Large Smoke Gore spawn

            }

            if (Projectile.timeLeft < 60)
            {
                Projectile.alpha += 20;
                Projectile.scale += 0.005f;
            }


        }

        public override bool PreDraw(ref Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
    }
}
