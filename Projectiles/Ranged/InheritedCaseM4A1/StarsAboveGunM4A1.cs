
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ranged.InheritedCaseM4A1
{
    /* This class is a held projectile that animates a gun firing.
	 * 
	 * */
    public abstract class StarsAboveGunM4A1 : ModProjectile
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
        public virtual float ScaleModifier { get; } = 1f;

        Vector2 MuzzlePosition;


        public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
		}
        public enum ActionState
        {
            //Recoil phase is optional and exists for posterity.
            Shooting,
            Recoil,
            Idle
        }
        public override void SetDefaults()
		{
			AIType = 0;

            //Adjust depending on projectile.
			Projectile.width = 90;
			Projectile.height = 90;
            Projectile.friendly = false;
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

        float shootAnimationProgress;
        float shootAnimationProgressMax = 10f;

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
                case (float)ActionState.Shooting:
                    ShootAnimation(projOwner);
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
            Projectile.position.X = projOwner.MountedCenter.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = projOwner.MountedCenter.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            OrientSprite(projOwner);
            projOwner.GetModPlayer<WeaponPlayer>().MuzzlePosition = MuzzlePosition;
        }

        private static float recoilRotationStart = 30f;
        private float recoilRotation = 0f;

        private static float recoilDistance;
        private void ShootAnimation(Player projOwner)
        {
            //Remember this is activating every tick.

            //In order:
            //1. The gun starts pointed up and the muzzle flash appears and quickly fades out along with very transparent dust gore. Screen shakes.
            //1a. The gun additionally moves slightly closer to the player (reducing the distance)
            //2. For the duration of the shoot animation, there are smoke particles being emitted from the muzzle.
            //3. The gun lerps towards its resting position. (downwards and back to its original distance.)
            //Depends on a percentage of the player's itemTime and itemTimeMax. The first half of the useTime is dedicated to the shoot animation.
            if (shootAnimationProgress == 0f)
            {
                if(StartingState == 2)
                {
                    shootAnimationProgress = 1f;
                    Projectile.alpha = 0;
                    flashAlpha = 0;
                    AI_State = (float)ActionState.Idle;
                    //return;
                }
                else
                {
                    flashAlpha = 1f;
                    projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -ScreenShakeTime;

                }

                recoilDistance = BaseDistance - 10f;
                //If the shoot animation has just begun...
                Projectile.alpha = 255;
                if(UseRecoil)
                {
                    shootAnimationProgressMax = projOwner.itemTimeMax / 2; //Half of the time spent after using the item is this animation.
                }
                else
                {
                    shootAnimationProgressMax = 5; //90% of the time spent after using this item is this animation.
                }

                int fakeRotation = (int)MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.MountedCenter.Y, Main.MouseWorld.X - projOwner.MountedCenter.X)) - 180;
                //Main.NewText(fakeRotation);
                if(fakeRotation < -235 && fakeRotation > -300)
                {
                    recoilRotationStart = 0;
                }
                else
                {
                    recoilRotationStart = 0f;
                }


            }

            Vector2 playerToMouse = Main.MouseWorld - projOwner.MountedCenter;
            playerToMouse = Vector2.Normalize(playerToMouse);

            float rotationX = 0;
            float rotationY = 0;


            distance = MathHelper.Lerp(recoilDistance, BaseDistance, EaseHelper.InOutQuad(shootAnimationProgress / shootAnimationProgressMax));
            recoilRotation = MathHelper.Lerp(recoilRotationStart, 0, EaseHelper.InOutQuad(shootAnimationProgress / shootAnimationProgressMax));

            if (Projectile.spriteDirection == 1)
            {
                float rotationOffset = MathHelper.ToRadians(-recoilRotation);
                rotationX = (float)(playerToMouse.X * Math.Cos(rotationOffset) - playerToMouse.Y * Math.Sin(rotationOffset));
                rotationY = (float)(playerToMouse.X * Math.Sin(rotationOffset) + playerToMouse.Y * Math.Cos(rotationOffset));
                MuzzlePosition = projOwner.MountedCenter + new Vector2(rotationX, rotationY) * MuzzleDistance;
                Rotation = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.MountedCenter.Y, Main.MouseWorld.X - projOwner.MountedCenter.X)) - 180 - recoilRotation;
            }
            else
            {
                float rotationOffset = MathHelper.ToRadians(recoilRotation);
                rotationX = (float)(playerToMouse.X * Math.Cos(rotationOffset) - playerToMouse.Y * Math.Sin(rotationOffset));
                rotationY = (float)(playerToMouse.X * Math.Sin(rotationOffset) + playerToMouse.Y * Math.Cos(rotationOffset));
                MuzzlePosition = projOwner.MountedCenter + new Vector2(rotationX, rotationY) * MuzzleDistance;
                Rotation = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.MountedCenter.Y, Main.MouseWorld.X - projOwner.MountedCenter.X)) - 180 + recoilRotation;
            }
            if (shootAnimationProgress == 2)
            {
                SpawnSmoke(projOwner, Projectile.Center + Vector2.Normalize(playerToMouse) * (MuzzleDistance - 14));
            }
            if (flashAlpha <= 0.3)
            {
                //Draw the residual smoke from the barrel.
                Dust d = Dust.NewDustPerfect(MuzzlePosition, SmokeDustID, new Vector2(Main.rand.NextFloat(-0.5f,0.5f), -2), 140, default, 1f);
                d.noGravity = true;
            }
            if (shootAnimationProgress >= shootAnimationProgressMax && shootAnimationProgress != 0f)
            {
                //Return to the idle state. (Alternatively, switch to the recoil state but that's unused)
                AI_State = (float)ActionState.Idle;
            }
            projOwner.GetModPlayer<WeaponPlayer>().MuzzlePosition = MuzzlePosition;
            flashAlpha -= 0.07f;
            shootAnimationProgress += 1f;
        }

        private void SpawnSmoke(Player projOwner, Vector2 MuzzlePos)
        {//
            for (int g = 0; g < 3; g++)
            {
                Gore goreIndex = Gore.NewGorePerfect(projOwner.GetSource_FromThis(),
                    new Vector2(MuzzlePos.X - 16 + Main.rand.Next(-15,15), MuzzlePos.Y - 16 + Main.rand.Next(-15, 15)),
                    Vector2.Zero, Main.rand.Next(61, 64), 1f);
                goreIndex.scale = 1.5f;
                goreIndex.alpha = 210;
                goreIndex.velocity = Vector2.Normalize(Main.MouseWorld - projOwner.MountedCenter);
            }
            for (int d = 0; d < 20; d++)
            {
                Vector2 perturbedSpeed = Vector2.Normalize(Main.MouseWorld - projOwner.MountedCenter).RotatedByRandom(MathHelper.ToRadians(6));
                float scale = 22f - (Main.rand.NextFloat() * 21f);
                perturbedSpeed *= scale;
                int dustIndex = Dust.NewDust(MuzzlePos, 0, 0, FlashDustID, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.7f);
                Main.dust[dustIndex].noGravity = true;
            }
            for (int d = 0; d < 10; d++)
            {
                Vector2 perturbedSpeed = Vector2.Normalize(Main.MouseWorld - projOwner.MountedCenter).RotatedBy(MathHelper.ToRadians(90));
                float scale = 6f - (Main.rand.NextFloat() * 1f);
                perturbedSpeed *= scale;
                int dustIndex = Dust.NewDust(MuzzlePos, 0, 0, FlashDustID, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.7f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 10; d++)
            {
                Vector2 perturbedSpeed = Vector2.Normalize(Main.MouseWorld - projOwner.MountedCenter).RotatedBy(MathHelper.ToRadians(-90));
                float scale = 6f - (Main.rand.NextFloat() * 1f);
                perturbedSpeed *= scale;
                int dustIndex = Dust.NewDust(MuzzlePos, 0, 0, FlashDustID, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.7f);
                Main.dust[dustIndex].noGravity = true;

            }
        }

        private void RecoilAnimation(Player projOwner)
        {
            //Pretty much the reverse of the shoot animation- the gun is lowered a little bit, pulled back again, and then reset to idle position.
        }
        private void Idle(Player projOwner)
        {
            if(KillOnIdle)
            {
                Projectile.Kill();
            }
            Rotation = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.MountedCenter.Y, Main.MouseWorld.X - projOwner.MountedCenter.X)) - 180;
            Vector2 playerToMouse = Main.MouseWorld - projOwner.MountedCenter;
            playerToMouse = Vector2.Normalize(playerToMouse);
            MuzzlePosition = Vector2.Normalize(playerToMouse) * (MuzzleDistance);
        }
        private void OrientSprite(Player projOwner)
        {
            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(180f);
            
            //Main.NewText(MathHelper.ToDegrees(Projectile.rotation));
            if (Projectile.rotation >= MathHelper.ToRadians(90) && Projectile.rotation <= MathHelper.ToRadians(270))
            {
                Projectile.spriteDirection = 0;
                Projectile.rotation += MathHelper.Pi;
                if (recoilRotation > 1)
                {
                    return;
                }
                projOwner.direction = 0;
            }
            else
            {
                Projectile.spriteDirection = 1;
                if (recoilRotation > 1)
                {
                    return;
                }
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
            Rectangle sourceRectangleFlash = textureFlash.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // Applying lighting and draw our projectile
            Color drawColor = Projectile.GetAlpha(lightColor);

            if(shootAnimationProgress > 2 || AI_State == (float)ActionState.Idle)
            {
                Main.EntitySpriteDraw(texture,
                   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                   sourceRectangle, lightColor, Projectile.rotation, origin, Projectile.scale * ScaleModifier, spriteEffects, 0);
                Main.EntitySpriteDraw(textureFlash,
                   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                   sourceRectangle, Color.White * flashAlpha, Projectile.rotation, origin, Projectile.scale * ScaleModifier, spriteEffects, 0);
            }
           
            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }
        private void RotateArms(Player projOwner)
        {
            projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.MountedCenter -
                            new Vector2(Projectile.Center.X + (projOwner.velocity.X * 0.05f), Projectile.Center.Y + (projOwner.velocity.Y * 0.05f))
                            ).ToRotation() + MathHelper.PiOver2);
            Projectile.alpha -= 90;

            projOwner.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (projOwner.MountedCenter -
                new Vector2(Projectile.Center.X + (projOwner.velocity.X * 0.05f), Projectile.Center.Y + (projOwner.velocity.Y * 0.05f))
                ).ToRotation() + MathHelper.PiOver2);
            Projectile.alpha -= 90;
        }

        public override void OnKill(int timeLeft)
		{
			

		}

	}
}
