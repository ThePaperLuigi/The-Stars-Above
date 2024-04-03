using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Magic.ParadiseLost;
using StarsAbove.Utilities;
using Terraria.GameContent;

namespace StarsAbove.Projectiles.Magic.ParadiseLost
{
    public class ParadiseLostProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Irys");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 1;

        }

        public override void SetDefaults()
        {
            Projectile.width = 260;               //The width of projectile hitbox
            Projectile.height = 260;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = true;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 2f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            DrawOriginOffsetY = -15;
            DrawOffsetX = -26;
        }
        float rotationSpeed = 10f;
        public override void AI()
        {
            Projectile.alpha = 255;
            Projectile.timeLeft = 10;
            Player projOwner = Main.player[Projectile.owner];
            if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<ParadiseLostBuff>()))
            {
                Projectile.Kill();
            }
            //projectile.spriteDirection = player.direction;
            projOwner.heldProj = Projectile.whoAmI;
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            //Projectile.direction = projOwner.direction;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            // Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = projOwner.velocity.X * 0.03f;

        }
        public float quadraticFloatTimer;
        public float quadraticFloat;
        public override void PostDraw(Color lightColor)
        {

            // This is where we specify which way to flip the sprite. If the projectile is moving to the left, then flip it vertically.
            SpriteEffects spriteEffects = Projectile.spriteDirection <= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            // Getting texture of projectile
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Texture2D bodyTexture = (Texture2D)Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/ParadiseLostProjectile");
            Texture2D BackWingsLeft = (Texture2D)Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/BackWingsLeft");
            Texture2D BackWingsRight = (Texture2D)Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/BackWingsRight");
            Texture2D MiddleWingsLeft = (Texture2D)Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/MiddleWingsLeft");
            Texture2D MiddleWingsRight = (Texture2D)Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/MiddleWingsRight");
            Texture2D FrontWingsLeft = (Texture2D)Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/FrontWingsLeft");
            Texture2D FrontWingsRight = (Texture2D)Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/FrontWingsRight");

            quadraticFloatTimer += 0.001f;
            quadraticFloat = EaseHelper.Pulse(quadraticFloatTimer);

            // Get the currently selected frame on the texture.
            Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;
            var drawPlayer = Main.player[Projectile.owner];
            Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0));
            int r1 = (int)color1.R;
            //drawOrigin.Y += 34f;
            //drawOrigin.Y += 8f;
            //--drawOrigin.X;
            Vector2 position1 = drawPlayer.Bottom - Main.screenPosition;
            Texture2D texture2D2 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Effects/ParadiseLostVFX");

            float num11 = (float)((double)Main.GlobalTimeWrappedHourly / 7.0);
            float timeFloatAlt = (float)((double)Main.GlobalTimeWrappedHourly / 5.0);

            //These control fade out (unused)
            float num12 = num11;
            if ((double)num12 > 0.5)
                num12 = 1f - num11;
            if ((double)num12 < 0.0)
                num12 = 0.0f;
            float num13 = (float)(((double)num11 + 0.5) % 1.0);
            float num14 = num13;
            if ((double)num14 > 0.5)
                num14 = 1f - num13;
            if ((double)num14 < 0.0)
                num14 = 0.0f;
            Microsoft.Xna.Framework.Rectangle r2 = texture2D2.Frame(1, 1, 0, 0);
            //drawOrigin = r2.Size() / 2f;
            Vector2 position3 = position1 + new Vector2(0f, -45f);

            /*if (drawPlayer.direction == 1)
            {
                position3 = position1 + new Vector2(-10f, -100f);
            }
            else
            {
                position3 = position1 + new Vector2(10f, -100f);
            }*/
            Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color(255, 255, 190); //This is the color of the pulse!
                                                                                                     //Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, NPC.rotation, drawOrigin, NPC.scale * 0.5f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            float num15 = 2f; //+ num11 * 2.75f; //Scale?
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, drawPlayer.fullRotation + num11, origin, 1 * 0.8f * num15 + MathHelper.Lerp(-0.2f, 0.2f, quadraticFloat), SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            float num16 = 2f; //+ num13 * 2.75f; //Scale?
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, drawPlayer.fullRotation - timeFloatAlt, origin, 1 * 0.8f * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

            // Applying lighting and draw our projectile
            Color drawColor = Projectile.GetAlpha(lightColor);

            //Back Wings
            Main.EntitySpriteDraw(BackWingsLeft,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation + MathHelper.ToRadians(MathHelper.Lerp(-2,10,quadraticFloat)), origin, Projectile.scale, spriteEffects, 0);
            Main.EntitySpriteDraw(BackWingsRight,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation - MathHelper.ToRadians(MathHelper.Lerp(-2, 10, quadraticFloat)), origin, Projectile.scale, spriteEffects, 0);

            //Middle Wings
            Main.EntitySpriteDraw(MiddleWingsLeft,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation - MathHelper.ToRadians(MathHelper.Lerp(-4, 4, quadraticFloat)), origin, Projectile.scale, spriteEffects, 0);
            Main.EntitySpriteDraw(MiddleWingsRight,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation + MathHelper.ToRadians(MathHelper.Lerp(-4, 4, quadraticFloat)), origin, Projectile.scale, spriteEffects, 0);

            //Front Wings
            Main.EntitySpriteDraw(FrontWingsLeft,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation + MathHelper.ToRadians(MathHelper.Lerp(-3, 3, quadraticFloat)), origin, Projectile.scale, spriteEffects, 0);
            Main.EntitySpriteDraw(FrontWingsRight,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation - MathHelper.ToRadians(MathHelper.Lerp(-3, 3, quadraticFloat)), origin, Projectile.scale, spriteEffects, 0);


            Main.EntitySpriteDraw(bodyTexture,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),//+ MathHelper.Lerp(25, 0, EaseHelper.InOutQuad(Gundbit1Transition))),
                    sourceRectangle, lightColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

           
            base.PostDraw(lightColor);
        }

    }
}
