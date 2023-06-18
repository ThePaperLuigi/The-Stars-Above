using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using System;


namespace StarsAbove.Projectiles
{
    /// <summary>
    /// Draws a laser to the destination, then fires at high speed
    /// ai[0] is the timer
    /// ai[1] is the laser scale
    /// </summary>
    public class SkyBeam : ModProjectile
    {

        private const int chargeTime = 150;
        private const float muzzleDist = 1f;
        private const int hitboxSize = 4;
        private const int hitboxHalfSize = hitboxSize / 2;

        private Vector2 staPos;

        private Vector2 endPos;


        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sky Beam");//Thank you to WeaponOut

        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.scale = 1f;
            Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (Projectile.ai[0] < chargeTime)
            {
                AICharge();
            }
            else
            {
                AILaunch();
            }
            Projectile.ai[0]++;
        }

        private void AICharge()
        {
            Projectile.position -= Projectile.velocity;
            if (Projectile.ai[0] == 0)
            {
                //move forwards
                Projectile.position += Projectile.velocity * muzzleDist;
                Projectile.ai[1] = 0.1f;

                //set rotation direction scale
                if (Projectile.velocity.X > 0) Projectile.spriteDirection = 1;
                else Projectile.spriteDirection = -1;
                Projectile.scale = 1.5f;

                //get end point of laser
                staPos = Projectile.position + Projectile.velocity * muzzleDist;
                endPos = Projectile.position;
                for (int i = 0; i < Projectile.timeLeft; i++) //roughly 1024 ft.
                {
                    //custom collision to match laser size, once per frame
                    Vector2 halfVelo = Projectile.velocity * 0.5f;
                    Vector2 alteredVelo = Collision.TileCollision(new Vector2(endPos.X - hitboxHalfSize + Projectile.width / 2, endPos.Y - hitboxHalfSize + Projectile.height / 2), halfVelo, hitboxSize, hitboxSize, true, true);
                    if (halfVelo != alteredVelo)
                    {
                        endPos += alteredVelo;
                        break;
                    }
                    alteredVelo = Collision.TileCollision(new Vector2(endPos.X - hitboxHalfSize + Projectile.width / 2, endPos.Y - hitboxHalfSize + Projectile.height / 2) + halfVelo, halfVelo, hitboxSize, hitboxSize, true, true);
                    if (halfVelo != alteredVelo)
                    {
                        endPos += halfVelo + alteredVelo;
                        break;
                    }
                    endPos += Projectile.velocity;
                }

                //emit dust
                for (int i = 0; i < 5; i++)
                {
                    int d1 = Dust.NewDust(Projectile.position + new Vector2(Projectile.width / 2, Projectile.height / 2) + Projectile.velocity * muzzleDist, 1, 1, 66, 0f, 0f, 100, Color.Purple, 1.5f);
                    Main.dust[d1].noGravity = true;
                    Main.dust[d1].velocity *= 0.4f;
                    d1 = Dust.NewDust(endPos + new Vector2(Projectile.width / 2, Projectile.height / 2), 0, 0, 66, 0f, 0f, 100, Color.Purple, 1f);
                    Main.dust[d1].noGravity = true;
                    Main.dust[d1].velocity *= 0.4f;
                }
            }

            Projectile.scale -= 0.007f; //shrink
            Projectile.ai[1] *= 1.004f; //grow laser
            Projectile.rotation += 0.05f * Projectile.spriteDirection * (Projectile.ai[0] * 0.2f); //spin faster and faster
        }
        private void AILaunch()
        {
            //don't update actually, causes weird desync issues clientside...
            //if (Main.myPlayer == projectile.owner) projectile.netUpdate = true;

            if (Projectile.ai[0] == chargeTime)
            {
                Projectile.position -= Projectile.velocity * muzzleDist;

                Projectile.ai[1] = 1.7f;
                Projectile.scale = 1.2f;

            }
           
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
            Projectile.scale *= 0.992f;
            if (Projectile.ai[1] > 0.12f) Projectile.ai[1] *= 0.98f;

            int d1 = Dust.NewDust(Projectile.position + new Vector2(Projectile.width / 2f, Projectile.height / 2f), 1, 1, 66, 0f, 0f, 100, Color.Purple, 1f);
            Main.dust[d1].noGravity = true;
            Main.dust[d1].velocity *= 0.5f;

            Vector2 halfVelo = Projectile.velocity * 0.5f;
            Vector2 alteredVelo = Collision.TileCollision(new Vector2(Projectile.position.X - hitboxHalfSize + Projectile.width / 2, Projectile.position.Y - hitboxHalfSize + Projectile.height / 2), halfVelo, hitboxSize, hitboxSize, true, true);
            if (halfVelo != alteredVelo)
            {
                Projectile.Kill();
            }
            alteredVelo = Collision.TileCollision(new Vector2(Projectile.position.X - hitboxHalfSize + Projectile.width / 2, Projectile.position.Y - hitboxHalfSize + Projectile.height / 2) + halfVelo, halfVelo, hitboxSize, hitboxSize, true, true);
            if (halfVelo != alteredVelo)
            {
                Projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int j = 0; j < 40; j++)
            {
                int d2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 66, (j / 90f) * -Projectile.velocity.X, (j / 90f) * -Projectile.velocity.Y, 100, Color.Purple, 1.2f);
                Main.dust[d2].noGravity = true;
                Main.dust[d2].velocity *= 0.6f;
            }
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)TextureAssets.Projectile[Projectile.type];
            Vector2 centre = new Vector2(texture.Width / 2f, texture.Height / 2f);

            drawLaser(Main.spriteBatch, centre);

            Main.spriteBatch.Draw(texture,
                Projectile.position - Main.screenPosition + centre,
                new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)),
                Color.White,
                Projectile.rotation,
                centre,
                Projectile.scale,
                SpriteEffects.None,
                0
            );
            return false;
       
        }

        private void drawLaser(SpriteBatch spritebatch, Vector2 centre)
        {
            Vector2 projectileCentre = Projectile.position + new Vector2(Projectile.width / 2, Projectile.height / 2);
            Vector2 start, end;
            if (Projectile.ai[0] > 0)
            {
                if (Projectile.ai[0] < chargeTime) //charge
                {
                    start = projectileCentre;
                    end = endPos + centre;
                }
                else                               //fire!
                {
                    start = staPos + centre;
                    end = projectileCentre;
                }

                Utils.DrawLaser(
                    spritebatch,
                    Mod.Assets.Request<Texture2D>("Projectiles/SkyBeamBeam").Value,
                    start - Main.screenPosition,
                    end - Main.screenPosition,
                    new Vector2(Projectile.ai[1]),
                    new Utils.LaserLineFraming(Laser)); //uses delegate (see method below)
            }
            
            
        }
        //define which frames are used in each stage (0 = start, 1 = mid, 2 = end
        private void Laser(int stage, Vector2 currentPosition, float distanceLeft, Rectangle lastFrame, out float distCovered, out Rectangle frame, out Vector2 origin, out Color color)
        {
            color = Color.White;
			if (stage == 0)
			{
				distCovered = 33f;
				frame = new Rectangle(0, 0, 16, 16);
				origin = frame.Size() / 2f;
				return;
			}
			if (stage == 1)
			{
				frame = new Rectangle(0, 16, 16, 16);
				distCovered = (float)frame.Height;
				origin = new Vector2((float)(frame.Width / 2), 0f);
				return;
			}
			if (stage == 2)
			{
				distCovered = 22f;
				frame = new Rectangle(0, 24, 16, 16);
				origin = new Vector2((float)(frame.Width / 2), 1f);
				return;
			}
			distCovered = Projectile.velocity.Length() * 90;
			frame = Rectangle.Empty;
			origin = Vector2.Zero;
			color = Color.Transparent;
        }
        
    }
}
