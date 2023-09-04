
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Generics
{
	/* This class is a held projectile that animates a gun firing.
	 * 
	 * */
    public abstract class StarsAboveSword : ModProjectile
	{
		public override string Texture => "StarsAbove/Projectiles/Generics/StarsAboveGun";

        //This allows the gun to set its own muzzle flash.
        public abstract string TextureFlash { get; }

        public abstract bool UseRecoil { get; }
        public abstract int FlashDustID { get; }
        public abstract int SmokeDustID { get; }
        public abstract int MuzzleDistance { get; }
        public abstract int StartingState { get; }//0 is shooting, 1 is recoil, 2 is idle.
        public abstract bool KillOnIdle { get; }
        public abstract int ScreenShakeTime { get; }

        Vector2 MuzzlePosition;


        public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
		}
        public enum ActionState
        {
            //Recoil phase is optional and exists for posterity.
            Swinging,
            Recoil,
            Idle
        }
        public override void SetDefaults()
		{
			AIType = 0;

            //Adjust depending on projectile.
			Projectile.width = 90;
			Projectile.height = 90;

			Projectile.timeLeft = 10;
			Projectile.penetrate = -1;
			Projectile.hide = false;
			Projectile.alpha = 0;
			Projectile.netImportant = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}
        public ref float AI_State => ref Projectile.ai[0];
        public ref float Rotation => ref Projectile.ai[1];

        public abstract float BaseDistance { get; } //This changes depending on the size of the gun.
        
        float distance = 0;

        float swingAnimationProgress;
        float swingAnimationProgressMax = 10f;

        double deg;

        float flashAlpha;

        public override bool PreAI()
        {
            //For later, add a check: if player isn't holding this weapon, kill this projectile.
            Player projOwner = Main.player[Projectile.owner];
            if (projOwner.dead && !projOwner.active)
            {//Disappear when player dies
                Projectile.Kill();
            }
			return true;
		}
		public override void AI()
        {
            Player projOwner = Main.player[Projectile.owner];
            Projectile.scale = 1f;
            //Projectile.timeLeft = 10;

            projOwner.heldProj = Projectile.whoAmI;//The projectile draws in front of the player.

            
            RotateArms(projOwner);
            Projectile.alpha -= 10;

            switch (AI_State)
            {
                case (float)ActionState.Swinging:
                    SwingAnimation(projOwner);
                    break;
                case (float)ActionState.Recoil:
                    RecoilAnimation(projOwner);
                    break;
                case (float)ActionState.Idle:
                    Idle(projOwner);
                    break;
            }

            deg = Rotation;
            double rad = deg * (Math.PI / 180);
            double dist = distance;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            OrientSprite(projOwner);
            projOwner.GetModPlayer<WeaponPlayer>().MuzzlePosition = MuzzlePosition;
        }

        private void SwingAnimation(Player projOwner)
        {
            //Remember this is activating every tick.
            if (UseRecoil)
            {
                swingAnimationProgressMax = projOwner.itemTimeMax / 2; //Half of the time spent after using the item is this animation.
            }
            else
            {
                swingAnimationProgressMax = projOwner.itemTimeMax * 0.9f; //90% of the time spent after using this item is this animation.
            }
            //In order:
            //The blade spawns at a specificed offset from where the player is facing.
            //The blade then sweeps depending on animation progress, creating effects and the 1.4.4 swing trail
            //The blade disappears
            swingAnimationProgress = MathHelper.Lerp(0, 180, EaseHelper.InOutQuad(swingAnimationProgress));
            swingAnimationProgress = Math.Clamp(swingAnimationProgress, 0, 1f);
            if (swingAnimationProgress >= swingAnimationProgressMax && swingAnimationProgress != 0f)
            {
                //Return to the idle state. (Alternatively, switch to the recoil state but that's unused)
                AI_State = (float)ActionState.Idle;
            }
            swingAnimationProgress += 1f;
        }
        private void RecoilAnimation(Player projOwner)
        {
            //Pretty much the reverse of the shoot animation- the gun is lowered a little bit, pulled back again, and then reset to idle position.
        }
        private void Idle(Player projOwner)
        {
            if(projOwner.itemTime == projOwner.itemTimeMax && KillOnIdle)
            {
                Projectile.Kill();
            }
            Rotation = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.Center.Y, Main.MouseWorld.X - projOwner.Center.X)) - 180;
            Vector2 playerToMouse = Main.MouseWorld - projOwner.Center;
            playerToMouse = Vector2.Normalize(playerToMouse);
            MuzzlePosition = Vector2.Normalize(playerToMouse) * (MuzzleDistance);
        }
        private void OrientSprite(Player projOwner)
        {
            Projectile.rotation = Vector2.Normalize(Main.player[Projectile.owner].Center - Projectile.Center).ToRotation() + MathHelper.ToRadians(180f);

            //Main.NewText(MathHelper.ToDegrees(Projectile.rotation));
            if (Projectile.rotation >= MathHelper.ToRadians(90) && Projectile.rotation <= MathHelper.ToRadians(270))
            {
                Projectile.spriteDirection = 0;
                Projectile.rotation += MathHelper.Pi;
                projOwner.direction = 0;
            }
            else
            {
                Projectile.spriteDirection = 1;
                projOwner.direction = 1;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            // This is where we specify which way to flip the sprite. If the projectile is moving to the left, then flip it vertically.
            SpriteEffects spriteEffects = ((Projectile.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            // Getting texture of projectile
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            //Getting texture of the muzzle flash
            Texture2D textureFlash = (Texture2D)ModContent.Request<Texture2D>(TextureFlash);

            // Get the currently selected frame on the texture.
            Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // Applying lighting and draw our projectile
            Color drawColor = Projectile.GetAlpha(lightColor);

            if(swingAnimationProgress > 2 || AI_State == (float)ActionState.Idle)
            {
                Main.EntitySpriteDraw(texture,
                   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                   sourceRectangle, lightColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            }
           
            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }



        private void RotateArms(Player projOwner)
        {
            projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
                            new Vector2(Projectile.Center.X + (projOwner.velocity.X * 0.05f), Projectile.Center.Y + (projOwner.velocity.Y * 0.05f))
                            ).ToRotation() + MathHelper.PiOver2);
            Projectile.alpha -= 90;

            projOwner.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
                new Vector2(Projectile.Center.X + (projOwner.velocity.X * 0.05f), Projectile.Center.Y + (projOwner.velocity.Y * 0.05f))
                ).ToRotation() + MathHelper.PiOver2);
            Projectile.alpha -= 90;
        }

        public override void Kill(int timeLeft)
		{
			

		}

	}
}
