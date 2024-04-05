using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Magic.ParadiseLost;
using StarsAbove.Utilities;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.CodeAnalysis.Differencing;
using System.Collections.Generic;
using StarsAbove.Systems;
using Terraria.Graphics.Shaders;
using Terraria.Audio;
using StarsAbove.Projectiles.Extra;

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
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
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
        double dist;

        float rotationSpeed = 10f;
        bool attackEffects = false;
        float glowScale = 0f;
        public override void AI()
        {
            Projectile.alpha = 255;
            Projectile.timeLeft = 10;
            Player projOwner = Main.player[Projectile.owner];
            if ((projOwner.dead && !projOwner.active) || !projOwner.HasBuff(BuffType<ParadiseLostBuff>()))
            {
                Projectile.Kill();
            }
            //projectile.spriteDirection = player.direction;
            projOwner.heldProj = Projectile.whoAmI;
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            var modPlayer = projOwner.GetModPlayer<WeaponPlayer>();
            //Factors for calculations
            double deg = 90; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians

            Projectile.ai[0] += 0.005f;
            Projectile.ai[0] = MathHelper.Clamp(Projectile.ai[0], 0, 1);
            if (modPlayer.paradiseLostAnimationTimer > 0.7f)
            {
                for (int i = 0; i < 10; i++)
                {
                    // Charging dust
                    Vector2 vector = new Vector2(
                        Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
                        Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
                    Dust d = Main.dust[Dust.NewDust(
                        Projectile.Center + vector, 1, 1,
                        DustID.LifeDrain, 0, 0, 255,
                        new Color(1f, 1f, 1f), 1.5f)];
                    d.velocity = -vector / 16;
                    d.velocity -= Projectile.velocity / 8;
                    d.noLight = true;
                    d.noGravity = true;
                }
                dist = MathHelper.Lerp(600, 0, EaseHelper.OutQuad(Projectile.ai[0])) - MathHelper.Lerp(0, 30, EaseHelper.InOutQuad(modPlayer.paradiseLostAnimationFloat1));
                if (!attackEffects)
                {
                    attackEffects = true;
                }
            }
            else if (modPlayer.paradiseLostAnimationTimer > 0f)
            {
                if(attackEffects)
                {
                    glowScale = 1f;
                    if(projOwner.whoAmI == Main.myPlayer)
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_TitanCast, Projectile.Center);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<ParadiseLostBurst>(), 0, 0, projOwner.whoAmI);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<fastLargeRadiate>(), 0, 0, projOwner.whoAmI);

                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<ParadiseLostExplosion>(), Projectile.damage * 2, 0, projOwner.whoAmI);
                    }
                    projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
                    attackEffects = false;
                }
                dist = MathHelper.Lerp(600, 0, EaseHelper.OutQuad(Projectile.ai[0])) - MathHelper.Lerp(-30, 0, EaseHelper.InOutQuad(modPlayer.paradiseLostAnimationFloat1));

            }
            else
            {
                dist = MathHelper.Lerp(600, 0, EaseHelper.OutQuad(Projectile.ai[0]));


            }
            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.rotation = projOwner.velocity.X * 0.03f;
            //Main.NewText("Animation Timer:" + modPlayer.paradiseLostAnimationTimer);
            //Main.NewText("Animation Float 1:" + modPlayer.paradiseLostAnimationFloat1);
            //Main.NewText("Animation Float 2:" + modPlayer.paradiseLostAnimationFloat2);

            //.NewText("Wing Animation:" + globalWingAnimation);

            //Part 1: draw wings in
            
            if(modPlayer.paradiseLostAnimationTimer > 0.7f)
            {
                globalWingAnimation = MathHelper.Lerp(0,15,EaseHelper.InOutQuad(modPlayer.paradiseLostAnimationFloat1));

            }
            //Part 2: unleash power animation
            else if (modPlayer.paradiseLostAnimationTimer > 0.6f)
            {
                globalWingAnimation = MathHelper.Lerp(-20, 20, EaseHelper.InOutQuad(modPlayer.paradiseLostAnimationFloat1));

            }
            //Part 3: recovery
            else if (modPlayer.paradiseLostAnimationTimer > 0f)
            {
                globalWingAnimation = MathHelper.Lerp(-20, 0, EaseHelper.InOutQuad(modPlayer.paradiseLostAnimationFloat1));

                globalWingAnimation = (float)Math.Round(globalWingAnimation, 3);
            }
            glowScale -= 0.01f;
            glowScale = MathHelper.Clamp(glowScale, 0f, 1f);
        }
        public float quadraticFloatTimer;
        public float quadraticFloat;

        public override bool PreDraw(ref Color lightColor)
        {
            // This is where we specify which way to flip the sprite. If the projectile is moving to the left, then flip it vertically.
            SpriteEffects spriteEffects = Projectile.spriteDirection <= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            // Getting texture of projectile
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle sourceRectangle;
            Vector2 origin;
            sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);
            origin = sourceRectangle.Size() / 2f;
            DrawGlowEffects(texture, out sourceRectangle, out origin);

            Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
            Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
            int r1 = (int)color1.R;
            //drawOrigin.Y += 34f;
            //drawOrigin.Y += 8f;
            --drawOrigin.X;
            Vector2 position1 = Projectile.Center - Main.screenPosition;
            Texture2D texture2D2 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/ParadiseLostVFX2");
            Texture2D VFX1 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/ParadiseLostVFX1");

            float num11 = (float)((double)Main.GlobalTimeWrappedHourly % 1.0 / 1.0);
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
            drawOrigin = r2.Size() / 2f;
            Vector2 position3 = position1 + new Vector2(0.0f, -10f);
            Microsoft.Xna.Framework.Color color3 = new Color(255, 0, 0, 100);
            float magicFade = 1f + num11 * 0.25f;

            Main.spriteBatch.Draw(VFX1, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, 0, drawOrigin, 1f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

            float num15 = 1f + num11 * 0.45f;
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, 0, drawOrigin, 1f * num15, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            Main.spriteBatch.Draw(VFX1, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, num15, drawOrigin, 1f * num15, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

            float original16 = 1f + num13 * 0.45f;

            float num16 = -1f + num13 * -0.15f;
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num14, 0, drawOrigin, 1f * original16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            Main.spriteBatch.Draw(VFX1, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num14, num16, drawOrigin, 1f * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num14, 0, drawOrigin, 8f * original16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);


            return base.PreDraw(ref lightColor);
        }
        float globalWingAnimation = 0;
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

            quadraticFloatTimer += 0.003f;

            quadraticFloat = EaseHelper.Pulse(quadraticFloatTimer);

            Rectangle sourceRectangle;
            Vector2 origin;
            sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);
            origin = sourceRectangle.Size() / 2f;
            // Applying lighting and draw our projectile
            Color drawColor = Projectile.GetAlpha(lightColor);

            //Back Wings
            Main.EntitySpriteDraw(BackWingsLeft,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation + MathHelper.ToRadians(MathHelper.Lerp(-2, 10, quadraticFloat) - globalWingAnimation), origin, Projectile.scale, spriteEffects, 0);
            Main.EntitySpriteDraw(BackWingsRight,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation - MathHelper.ToRadians(MathHelper.Lerp(-2, 10, quadraticFloat) - globalWingAnimation), origin, Projectile.scale, spriteEffects, 0);

            //Middle Wings
            Main.EntitySpriteDraw(MiddleWingsLeft,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation - MathHelper.ToRadians(MathHelper.Lerp(-4, 4, quadraticFloat) + globalWingAnimation), origin, Projectile.scale, spriteEffects, 0);
            Main.EntitySpriteDraw(MiddleWingsRight,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation + MathHelper.ToRadians(MathHelper.Lerp(-4, 4, quadraticFloat) + globalWingAnimation), origin, Projectile.scale, spriteEffects, 0);

            //Front Wings
            Main.EntitySpriteDraw(FrontWingsLeft,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation + MathHelper.ToRadians(MathHelper.Lerp(-3, 3, quadraticFloat) - globalWingAnimation), origin, Projectile.scale, spriteEffects, 0);
            Main.EntitySpriteDraw(FrontWingsRight,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                    sourceRectangle, lightColor, Projectile.rotation - MathHelper.ToRadians(MathHelper.Lerp(-3, 3, quadraticFloat) - globalWingAnimation), origin, Projectile.scale, spriteEffects, 0);


            Main.EntitySpriteDraw(bodyTexture,
                    Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY - 10),//+ MathHelper.Lerp(25, 0, EaseHelper.InOutQuad(Gundbit1Transition))),
                    sourceRectangle, lightColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);


            base.PostDraw(lightColor);
        }

        private void DrawGlowEffects(Texture2D texture, out Rectangle sourceRectangle, out Vector2 origin)
        {
            // Get the currently selected frame on the texture.
            sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);
            origin = sourceRectangle.Size() / 2f;
            var drawPlayer = Main.player[Projectile.owner];
            Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0));
            int r1 = (int)color1.R;
            //drawOrigin.Y += 34f;
            //drawOrigin.Y += 8f;
            //--drawOrigin.X;
            Vector2 position1 = Projectile.Center - Main.screenPosition;
            Texture2D texture2D2 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Effects/ParadiseLostVFX");

            Texture2D VFX1 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/ParadiseLostVFX1");
            Texture2D VFX2 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Magic/ParadiseLost/ParadiseLostVFX2");


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
            Vector2 position3 = position1 + new Vector2(0f, 0f);

            /*if (drawPlayer.direction == 1)
            {
                position3 = position1 + new Vector2(-10f, -100f);
            }
            else
            {
                position3 = position1 + new Vector2(10f, -100f);
            }*/
            Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color(255, 0, 0, 50); //This is the color of the pulse!
                                                                                                     //Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, Projectile.rotation, drawOrigin, Projectile.scale * 0.5f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            float num15 = 2f; //+ num11 * 2.75f; //Scale?
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, 0 + num11, origin, 1 * 0.8f * num15 + MathHelper.Lerp(-0.2f, 0.2f, quadraticFloat) + glowScale*3, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            float num16 = 2f; //+ num13 * 2.75f; //Scale?
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, 0 - timeFloatAlt, origin, 1 * 0.8f * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

            color3 = new Color(255, 0, 0);
            //Main.spriteBatch.Draw(VFX1, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, 0, origin, 1 * 0.8f * num15 + MathHelper.Lerp(-0.4f, 0.4f, quadraticFloat), SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
        }
    }
}
