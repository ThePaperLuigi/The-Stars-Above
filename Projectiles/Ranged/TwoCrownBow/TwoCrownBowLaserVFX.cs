using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour.HookGen;
using StarsAbove.Systems;
using System;

using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ranged.TwoCrownBow
{
    public class TwoCrownBowLaserVFX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Armament");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 1;

        }

        public override void SetDefaults()
        {
            Projectile.width = 20;               //The width of projectile hitbox
            Projectile.height = 20;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = false;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 240;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 2f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        float rotationSpeed = 10f;
        double deg;

        public override void AI()
        {
            Projectile.timeLeft = 10;
            Player projOwner = Main.player[Projectile.owner];
            Player player = Main.player[Projectile.owner];
            if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<Buffs.Ranged.TwoCrownBow.StellarTerminationBuff>()))
            {
                Projectile.alpha += 50;
            }
            else
            {
                Projectile.alpha -= 15;
            }
            if(Projectile.alpha > 250)
            {
                Projectile.Kill();
            }
            Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha, 0, 255);
            //projectile.spriteDirection = player.direction;
            //projOwner.heldProj = projectile.whoAmI;
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            deg = Projectile.ai[1];
            double rad = deg * (Math.PI / 180);
            double dist = 30;
            Projectile.ai[1] = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.Center.Y, Main.MouseWorld.X - projOwner.Center.X)) - 180;
            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.spriteDirection = Projectile.direction;
            //Projectile.rotation += Projectile.velocity.X / 20f;

            Projectile.ai[2]++;
            if (Projectile.ai[2] > 360)
            {
                Projectile.ai[2] = 0;
            }

            if (Projectile.ai[0] > 3)
            {
                Projectile.ai[0] = 0;
                int type = ProjectileType<TwoCrownBowEmpoweredArrow>();

                SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);

                Vector2 position = Projectile.Center;
                float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                float launchSpeed = 35 + Main.rand.Next(0, 10);
                Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                Vector2 direction = Vector2.Normalize(mousePosition - Projectile.Center);
                Vector2 velocity = new Vector2(direction.X, direction.Y).RotatedByRandom(MathHelper.ToRadians(10)) * launchSpeed;

                int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, player.whoAmI);

                Main.projectile[index].originalDamage = Projectile.damage;

            }
            Projectile.ai[0]++;

        }
        public RenderTarget2D rt { get; set; }
        int frameY = 0;
        int frameTimer = 0;
        public override bool PreDraw(ref Color lightColor)
        {                      
            // Get the initial draw parameters
            Texture2D texture = (Texture2D)Request<Texture2D>("StarsAbove/Effects/TwoCrownedBowVortex");
            Texture2D textureBow = (Texture2D)Request<Texture2D>("StarsAbove/Items/Weapons/Ranged/TwoCrownBow");

            frameTimer++;
            if(frameTimer > 2)
            {
                frameY++;
                if(frameY > 9)
                {
                    frameY = 0;
                }
                frameTimer = 0;
            }
            
            Rectangle frame = texture.Frame(1, 10, 0, frameY);
            Rectangle frameBow = textureBow.Frame(1, 1, 0, 0);

            Vector2 origin = frame.Size() / 2f;
            Vector2 originBow = frameBow.Size() / 2f;

            Color color = Color.White * MathHelper.Lerp(1f, 0f, (float)((float)Projectile.alpha / 255f));
            SpriteEffects effects = SpriteEffects.FlipHorizontally;

            if (Main.player[Projectile.owner].direction == 1)
            {
                effects = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

            }
            else
            {

            }


            // Some math magic to make it smoothly move up and down over time
            const float TwoPi = (float)Math.PI * 2f;
            float offset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 5f);
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            Vector2 drawPosBow = Vector2.Lerp(Main.player[Projectile.owner].Center, Projectile.Center, 0.5f) - Main.screenPosition;

            float rotation = Vector2.Normalize(Vector2.Lerp(Main.player[Projectile.owner].Center,Projectile.Center,0.5f) - Projectile.Center).ToRotation();
            // Draw the main texture
            Main.EntitySpriteDraw(textureBow, drawPosBow, frameBow, color, rotation, originBow, 1f, effects, 0f);

            Main.EntitySpriteDraw(texture, drawPos, frame, color, rotation, origin, 0.7f, SpriteEffects.None, 0f);


            // Draw the periodic glow effect
            float scale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 2f) * 0.3f + 0.7f;
            Color effectColor = color;
            effectColor.A = 0;
            effectColor = effectColor * 0.1f * scale;
            for (float num5 = 0f; num5 < 1f; num5 += 355f / (678f * (float)Math.PI))
            {
                Main.EntitySpriteDraw(texture, drawPos + (TwoPi * num5).ToRotationVector2() * (6f + offset * 2f), frame, effectColor, rotation, origin, 0.8f, SpriteEffects.None, 0f);
            }
            

            return false;
        }
    }

    
}
