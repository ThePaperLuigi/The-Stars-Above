using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace StarsAbove.Projectiles.StellarArray
{
    public class Starshield : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starshield");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 1;

        }

        public override void SetDefaults()
        {
            Projectile.width = 200;               //The width of projectile hitbox
            Projectile.height = 200;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = false;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 120;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 2f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            DrawOriginOffsetY = -15;
            DrawOffsetX = -26;
        }

        public override bool PreDraw(ref Color lightColor)
        {

            Texture2D texture = (Texture2D)Request<Texture2D>("StarsAbove/Projectiles/Starshield");

            const float TwoPi = (float)Math.PI * 2f;
            float offset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 5f);

            SpriteEffects effects = SpriteEffects.None;

            float scale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 2f) * 0.3f + 0.7f;
            Color effectColor = Color.White;
            effectColor.A = 0;
            effectColor = effectColor * 0.06f * scale;
            for (float num5 = 0f; num5 < 1f; num5 += 355f / (678f * (float)Math.PI))
            {
                Main.spriteBatch.Draw(texture, Projectile.Center + (TwoPi * num5).ToRotationVector2() * (2f + offset), Projectile.getRect(), effectColor, 0f, Projectile.Center, 1f, effects, 0f);
            }

            return base.PreDraw(ref lightColor);
        }
        public override void AI()
        {
            Projectile.timeLeft = 10;
            Player projOwner = Main.player[Projectile.owner];
            Player player = Main.player[Projectile.owner];
            if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<Buffs.StarshieldBuff>()))
            {
                Projectile.alpha++;
            }
            if (Projectile.alpha > 255)
            {
                Projectile.Kill();
            }
            //projectile.spriteDirection = player.direction;
            //projOwner.heldProj = Projectile.whoAmI;
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            //Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation += Projectile.velocity.X / 20f;



            // Adding Pi to rotation if facing left corrects the drawing
            //projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);

            if (projOwner.velocity == Vector2.Zero)
            {

            }
            else
            {

            }


        }



    }
}
