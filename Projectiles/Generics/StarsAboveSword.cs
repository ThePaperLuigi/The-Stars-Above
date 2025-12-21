
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
using StarsAbove.Projectiles.Summon.Wavedancer;
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

namespace StarsAbove.Projectiles.Generics
{
    /* This class is a held projectile that animates a sword swing.
	 * 
	 * */
    public abstract class StarsAboveSword : ModProjectile
	{
		public override string Texture => "StarsAbove/Projectiles/Generics/StarsAboveSword";//Replace in the implementation
        public abstract bool UseRecoil { get; }
        public abstract bool DoSpin { get; }
        public abstract Color BackDarkColor { get; }
        public abstract Color MiddleMediumColor { get; }
        public abstract Color FrontLightColor  { get; }
        public virtual bool CenterOnPlayer { get; }
        public virtual bool Rotate45Degrees { get; }
        public virtual float EffectScaleMulti { get; } = 0.6f;
        public virtual float EffectScaleAdder { get; } = 1f;
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
        public ref float SwingDirection => ref Projectile.ai[1];//0 is normal, 1 is reversed
        public ref float Rotation => ref Projectile.localAI[1];
        public ref float PlayerDirection => ref Projectile.ai[2];


        public abstract float BaseDistance { get; } //This changes depending on the size of the gun.
        
        float distance = 0;

        float swingAnimationProgress;
        float swingAnimationProgressMax = 10f;
        float recoilAnimationProgress;
        float recoilAnimationProgressMax = 10f;
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
            if(CenterOnPlayer)
            {
                Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            }
            else
            {
                //ew
                if (projOwner.ownedProjectileCounts[ProjectileType<WavedancerSummon>()] > 0)
                {
                    Projectile.position.X = projOwner.GetModPlayer<WeaponPlayer>().wavedancerPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                    Projectile.position.Y = projOwner.GetModPlayer<WeaponPlayer>().wavedancerPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                }
                else
                {
                    Projectile.position.X = Main.MouseWorld.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                    Projectile.position.Y = Main.MouseWorld.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                }
               
            }
            
            OrientSprite(projOwner);
            //projOwner.GetModPlayer<WeaponPlayer>().MuzzlePosition = MuzzlePosition;
            MathHelper.Clamp(Projectile.alpha, 0, 255);

        }
        float startingPosition = 0f;

        private void SwingAnimation(Player projOwner)
        {
            //Remember this is activating every tick.
            if (swingAnimationProgress == 0f)
            {
                startingPosition = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.Center.Y, Main.MouseWorld.X - projOwner.Center.X)) - 180;
                if (UseRecoil)
                {
                    swingAnimationProgressMax = projOwner.itemTimeMax / 2; //Half of the time spent after using the item is this animation.
                }
                else
                {
                    swingAnimationProgressMax = projOwner.itemTimeMax * 0.9f;
                }
                if (SwingDirection == 0)//0 is normal, 1 is reverse
                {
                    int projType = ProjectileType<StarsAboveSwordEffect>();

                    //Projectile.NewProjectile(projOwner.GetSource_FromThis(), projOwner.MountedCenter, new Vector2(projOwner.direction, 0f), projType, 0, 0, projOwner.whoAmI, projOwner.direction, swingAnimationProgressMax, 1);
                    int proj = Projectile.NewProjectile(projOwner.GetSource_FromThis(), projOwner.MountedCenter, new Vector2(projOwner.direction, 0f), projType, 0, 0, projOwner.whoAmI, projOwner.direction, swingAnimationProgressMax, Projectile.scale);
                    StarsAboveSwordEffect projectile = Main.projectile[proj].ModProjectile as StarsAboveSwordEffect;
                    projectile.backDarkColor = BackDarkColor;
                    projectile.middleMediumColor = MiddleMediumColor;
                    projectile.frontLightColor = FrontLightColor;
                    projectile.scaleMulti = EffectScaleMulti;
                    projectile.scaleAdder = EffectScaleAdder;
                    if(DoSpin)
                    {
                        projectile.spin = true;
                    }
                    if(!CenterOnPlayer)
                    {
                        projectile.centerOnMouse = true;
                    }
                }
                else
                {
                    int projType = ProjectileType<StarsAboveSwordEffect>();

                    if (PlayerDirection == 1)
                    {
                        int proj = Projectile.NewProjectile(projOwner.GetSource_FromThis(), projOwner.MountedCenter, new Vector2(projOwner.direction, 0f), projType, 0, 0, projOwner.whoAmI, -1, swingAnimationProgressMax, Projectile.scale);
                        StarsAboveSwordEffect projectile = Main.projectile[proj].ModProjectile as StarsAboveSwordEffect;
                        projectile.backDarkColor = BackDarkColor;
                        projectile.middleMediumColor = MiddleMediumColor;
                        projectile.frontLightColor = FrontLightColor;
                        projectile.scaleMulti = EffectScaleMulti;
                        projectile.scaleAdder = EffectScaleAdder;
                        if (DoSpin)
                        {
                            projectile.spin = true;
                        }
                    }
                    else
                    {
                        int proj = Projectile.NewProjectile(projOwner.GetSource_FromThis(), projOwner.MountedCenter, new Vector2(projOwner.direction, 0f), projType, 0, 0, projOwner.whoAmI, 1, swingAnimationProgressMax, Projectile.scale);
                        StarsAboveSwordEffect projectile = Main.projectile[proj].ModProjectile as StarsAboveSwordEffect;
                        projectile.backDarkColor = BackDarkColor;
                        projectile.middleMediumColor = MiddleMediumColor;
                        projectile.frontLightColor = FrontLightColor;
                        projectile.scaleMulti = EffectScaleMulti;
                        projectile.scaleAdder = EffectScaleAdder;
                        if (DoSpin)
                        {
                            projectile.spin = true;
                        }
                    }

                }

                distance = BaseDistance;
                Projectile.alpha = 0;

               
            }
            //In order:
            //The blade spawns at a specificed offset from where the player is facing.
            //The blade then sweeps depending on animation progress, creating effects and the 1.4.4 swing trail
            //The blade disappears
            int bonusMovement = 0;
            if (DoSpin)
            {
                bonusMovement = 360;
            }

            if (PlayerDirection == 1)
            {
                if(SwingDirection == 0)//0 is normal, 1 is reverse
                {
                    Rotation = MathHelper.Lerp(startingPosition - 90, startingPosition + 90 + bonusMovement, EaseHelper.InOutQuad(swingAnimationProgress / swingAnimationProgressMax));

                }
                else
                {
                    Rotation = MathHelper.Lerp(startingPosition + 90, startingPosition - 90 - bonusMovement, EaseHelper.InOutQuad(swingAnimationProgress / swingAnimationProgressMax));

                }
            }
            else
            {
                if (SwingDirection == 0)//0 is normal, 1 is reverse
                {
                    Rotation = MathHelper.Lerp(startingPosition + 90, startingPosition - 90 - bonusMovement, EaseHelper.InOutQuad(swingAnimationProgress / swingAnimationProgressMax));
                }
                else
                {
                    Rotation = MathHelper.Lerp(startingPosition - 90, startingPosition + 90 + bonusMovement, EaseHelper.InOutQuad(swingAnimationProgress / swingAnimationProgressMax));
                }
            }

            if (swingAnimationProgress / swingAnimationProgressMax > 0.4f && !UseRecoil)
            {
                Projectile.alpha += 40;
            }

            if (swingAnimationProgress >= swingAnimationProgressMax && swingAnimationProgress != 0f)
            {
                if(UseRecoil)
                {
                    AI_State = (float)ActionState.Recoil;
                }
                else
                {
                    //It's a sword, kill it.
                    Projectile.Kill();
                }

               


                //Return to the idle state. (Alternatively, switch to the recoil state but that's unused)
                //AI_State = (float)ActionState.Idle;
            }
            swingAnimationProgress += 1f;
        }
        private void RecoilAnimation(Player projOwner)
        {
            if (recoilAnimationProgress == 0f)
            {
                startingPosition = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.Center.Y, Main.MouseWorld.X - projOwner.Center.X)) - 180;
                
                recoilAnimationProgressMax = projOwner.itemTimeMax / 2; //Half of the time spent after using the item is this animation.

                distance = BaseDistance;
                //Projectile.alpha = 0;
            }
            //In order:
            //The blade spins to prep for the stab

            if (PlayerDirection == 1)
            {
                if (SwingDirection == 0)//0 is normal, 1 is reverse
                {
                    Rotation = MathHelper.Lerp(startingPosition + 90, startingPosition, EaseHelper.InOutQuad(recoilAnimationProgress / recoilAnimationProgressMax));

                }
                else
                {
                    Rotation = MathHelper.Lerp(startingPosition - 90, startingPosition, EaseHelper.InOutQuad(recoilAnimationProgress / recoilAnimationProgressMax));

                }
            }
            else
            {
                if (SwingDirection == 0)//0 is normal, 1 is reverse
                {
                    Rotation = MathHelper.Lerp(startingPosition - 90, startingPosition, EaseHelper.InOutQuad(recoilAnimationProgress / recoilAnimationProgressMax));
                }
                else
                {
                    Rotation = MathHelper.Lerp(startingPosition + 90, startingPosition, EaseHelper.InOutQuad(recoilAnimationProgress / recoilAnimationProgressMax));
                }
            }

            if (recoilAnimationProgress / recoilAnimationProgressMax > 0.4f)
            {
                Projectile.alpha += 40;
            }

            if (recoilAnimationProgress >= recoilAnimationProgressMax && recoilAnimationProgress != 0f)
            {
                Projectile.Kill();
            }
            recoilAnimationProgress += 1f;
        }
        private void Idle(Player projOwner)
        {
            if(projOwner.itemTime == projOwner.itemTimeMax)
            {

            }
            //Rotation = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.Center.Y, Main.MouseWorld.X - projOwner.Center.X)) - 180;
            Vector2 playerToMouse = Main.MouseWorld - projOwner.Center;
            playerToMouse = Vector2.Normalize(playerToMouse);
        }
        private void OrientSprite(Player projOwner)
        {
            if (CenterOnPlayer)
            {
                if(Rotate45Degrees)
                {
                    if (PlayerDirection == 1)
                    {
                        if (Projectile.ai[1] == 1)
                        {
                            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-45f);

                        }
                        else
                        {
                            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-135f);

                        }
                    }
                    else
                    {
                        if (Projectile.ai[1] == 1)
                        {
                            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-135f);

                        }
                        else
                        {
                            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-45f);

                        }
                    }
                    

                }
                else
                {
                    Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-90f);

                }

            }
            else
            {
                if (Rotate45Degrees)
                {
                    if (Projectile.ai[1] == 1)
                    {
                        Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-90f);


                    }
                    else
                    {
                        Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-135f);

                    }

                }
                else
                {
                    Projectile.rotation = Vector2.Normalize(Main.MouseWorld - Projectile.Center).ToRotation() + MathHelper.ToRadians(-90f);

                }

            }
            //Main.NewText(MathHelper.ToDegrees(Projectile.rotation));

        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = ((Projectile.spriteDirection > 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

            // This is where we specify which way to flip the sprite. If the projectile is moving to the left, then flip it vertically.
            if (PlayerDirection == 1)
            {
                if (Projectile.ai[1] == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    spriteEffects = SpriteEffects.None;

                }
            }
            else
            {
                if (Projectile.ai[1] == 1)
                {
                    spriteEffects = SpriteEffects.None;
                }
                else
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;


                }
            }
            

            // Getting texture of projectile
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            // Get the currently selected frame on the texture.
            Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // Applying lighting and draw our projectile
            Color drawColor = Projectile.GetAlpha(lightColor);

            if(swingAnimationProgress > 2 || AI_State == (float)ActionState.Idle)
            {
                Main.EntitySpriteDraw(texture,
                   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                   sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            }
           
            // It's important to return false, otherwise we also draw the original texture.
            return false;
        }



        private void RotateArms(Player projOwner)
        {
            projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
                            new Vector2(Projectile.Center.X + (projOwner.velocity.X * 0.05f), Projectile.Center.Y + (projOwner.velocity.Y * 0.05f))
                            ).ToRotation() + MathHelper.PiOver2);

            /*projOwner.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
                new Vector2(Projectile.Center.X + (projOwner.velocity.X * 0.05f), Projectile.Center.Y + (projOwner.velocity.Y * 0.05f))
                ).ToRotation() + MathHelper.PiOver2);
            Projectile.alpha -= 90;*/
        }

        public override void OnKill(int timeLeft)
		{
			

		}

	}
}
