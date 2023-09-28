
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Buffs.Gundbits;
using StarsAbove.Projectiles.Generics;
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

namespace StarsAbove.Projectiles.Gundbit
{
    public class GundbitGunCharged : StarsAboveGun
	{
		public override string Texture => "StarsAbove/Projectiles/Gundbit/GundbitGunUncharged";
		public override string TextureFlash => "StarsAbove/Projectiles/Gundbit/GundbitGunUnchargedFlash";
		//Use the extra recoil/reload code.
		public override bool UseRecoil => false;
		//The dust that appears from the barrel after shooting.
		public override int SmokeDustID => Terraria.ID.DustID.Smoke;

		//The dust that fires from the barrel after shooting.
		public override int FlashDustID => Terraria.ID.DustID.Electric;
		//The distance the gun's muzzle is relative to the player. Remember this also is influenced by base distance.
		public override int MuzzleDistance => 70;
		//The distance the gun is relative to the player.
		public override float BaseDistance => 48;
		public override int StartingState => 2;
		public override bool KillOnIdle => false;
		public override int ScreenShakeTime => 95;
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
		}
		public override void SetDefaults()
		{
			Projectile.width = 180;
			Projectile.height = 64;

			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}
		float TransformationProgress = 0;
		float TransitionSpeed = 0.1f;

		float Gundbit1Transition = 0f;
		float Gundbit2Transition = 0f;
		float Gundbit3Transition = 0f;
		float Gundbit4Transition = 0f;
		float Gundbit5Transition = 0f;
		float Gundbit6Transition = 0f;
		float Gundbit7Transition = 0f;
		float Gundbit8Transition = 0f;
		float Gundbit9Transition = 0f;
		float Gundbit10Transition = 0f;
		float Gundbit11Transition = 0f;

        public override void PostAI()
        {
			Player projOwner = Main.player[Projectile.owner];

			TransformationProgress += 0.02f;

			if (TransformationProgress >= 1f && projOwner.whoAmI == Main.myPlayer && projOwner.ownedProjectileCounts[ProjectileType<GundbitLaser>()] < 1)
			{
				for (int d = 0; d < 15; d++)
				{
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.MinecartSpark, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<GundbitLaser>(), (int)(Projectile.damage), 0f, projOwner.whoAmI);
				projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_GundamLaser, Projectile.Center);

			}
			if (TransformationProgress >= 1f)
			{
				
			}

			if (!projOwner.HasBuff(BuffType<GundbitBeamAttack>()))
			{
				for (int d = 0; d < 15; d++)
				{
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}
				Projectile.Kill();
			}

			Gundbit1Transition = MathHelper.Clamp(Gundbit1Transition, 0f, 1f);
			Gundbit2Transition = MathHelper.Clamp(Gundbit2Transition, 0f, 1f);
			Gundbit3Transition = MathHelper.Clamp(Gundbit3Transition, 0f, 1f);
			Gundbit4Transition = MathHelper.Clamp(Gundbit4Transition, 0f, 1f);
			Gundbit5Transition = MathHelper.Clamp(Gundbit5Transition, 0f, 1f);
			Gundbit6Transition = MathHelper.Clamp(Gundbit6Transition, 0f, 1f);
			Gundbit7Transition = MathHelper.Clamp(Gundbit7Transition, 0f, 1f);
			Gundbit8Transition = MathHelper.Clamp(Gundbit8Transition, 0f, 1f);
			Gundbit9Transition = MathHelper.Clamp(Gundbit9Transition, 0f, 1f);
			Gundbit10Transition = MathHelper.Clamp(Gundbit10Transition, 0f, 1f);
			Gundbit11Transition = MathHelper.Clamp(Gundbit11Transition, 0f, 1f);

		}
		public override bool PreAI()
        {
			return true;
		}
        public override void PostDraw(Color lightColor)
        {
			// This is where we specify which way to flip the sprite. If the projectile is moving to the left, then flip it vertically.
			SpriteEffects spriteEffects = ((Projectile.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

			// Getting texture of projectile
			Texture2D texture = TextureAssets.Projectile[Type].Value;

			Texture2D textureGundbit1 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit4");
			Texture2D textureGundbit2 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit5");
			Texture2D textureGundbit3 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit6");
			Texture2D textureGundbit4 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit1");
			Texture2D textureGundbit5 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit2");
			Texture2D textureGundbit6 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit3");
			Texture2D textureGundbit7 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit7");
			Texture2D textureGundbit8 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit8");
			Texture2D textureGundbit9 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit9");
			Texture2D textureGundbit10 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit10");
			Texture2D textureGundbit11 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Gundbit/Gundbit11");

			// Get the currently selected frame on the texture.
			Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);

			Vector2 origin = sourceRectangle.Size() / 2f;

			// Applying lighting and draw our projectile
			Color drawColor = Projectile.GetAlpha(lightColor);
			if(TransformationProgress >= 0.01)
            {
				Gundbit1Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit1,
					Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + MathHelper.Lerp(25,0,EaseHelper.InOutQuad(Gundbit1Transition))),
					sourceRectangle, lightColor * Gundbit1Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if(TransformationProgress >= 0.09)
            {
				Gundbit2Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit2,
				   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + MathHelper.Lerp(-25, 0, EaseHelper.InOutQuad(Gundbit2Transition))),
				   sourceRectangle, lightColor * Gundbit2Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if (TransformationProgress >= 0.19)
			{
				Gundbit3Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit3,
				   Projectile.Center - Main.screenPosition + new Vector2(0f + MathHelper.Lerp(25, 0, EaseHelper.InOutQuad(Gundbit3Transition)), Projectile.gfxOffY),
				   sourceRectangle, lightColor * Gundbit3Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if (TransformationProgress >= 0.29)
			{
				Gundbit4Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit4,
				   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + MathHelper.Lerp(25, 0, EaseHelper.InOutQuad(Gundbit4Transition))),
				   sourceRectangle, lightColor * Gundbit4Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if (TransformationProgress >= 0.39)
			{
				Gundbit5Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit5,
				   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + MathHelper.Lerp(-25, 0, EaseHelper.InOutQuad(Gundbit5Transition))),
				   sourceRectangle, lightColor * Gundbit5Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if (TransformationProgress >= 0.49)
			{
				Gundbit6Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit6,
				   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + MathHelper.Lerp(-25, 0, EaseHelper.InOutQuad(Gundbit6Transition))),
				   sourceRectangle, lightColor * Gundbit6Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if (TransformationProgress >= 0.59)
			{
				Gundbit7Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit7,
				   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + MathHelper.Lerp(-25, 0, EaseHelper.InOutQuad(Gundbit7Transition))),
				   sourceRectangle, lightColor * Gundbit7Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if (TransformationProgress >= 0.69)
			{
				Gundbit8Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit8,
				   Projectile.Center - Main.screenPosition + new Vector2(0f + MathHelper.Lerp(-25, 0, EaseHelper.InOutQuad(Gundbit8Transition)), Projectile.gfxOffY),
				   sourceRectangle, lightColor * Gundbit8Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if (TransformationProgress >= 0.79)
			{
				Gundbit9Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit9,
				   Projectile.Center - Main.screenPosition + new Vector2(0f + MathHelper.Lerp(-25, 0, EaseHelper.InOutQuad(Gundbit9Transition)), Projectile.gfxOffY),
				   sourceRectangle, lightColor * Gundbit9Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if (TransformationProgress >= 0.89)
			{
				Gundbit10Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit10,
				   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + MathHelper.Lerp(25, 0, EaseHelper.InOutQuad(Gundbit10Transition))),
				   sourceRectangle, lightColor * Gundbit10Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			if (TransformationProgress >= 0.99)
			{
				Gundbit11Transition += TransitionSpeed;
				Main.EntitySpriteDraw(textureGundbit11,
				   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + MathHelper.Lerp(25, 0, EaseHelper.InOutQuad(Gundbit11Transition))),
				   sourceRectangle, lightColor * Gundbit11Transition, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			}
			
			base.PostDraw(lightColor);
        }
        //For posterity, the draw code of this gun is going to have each part of the upgraded gun seperate and they will draw in with a white flash.
        public override void OnKill(int timeLeft)
		{
			

		}

	}
}
