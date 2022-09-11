
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.MorningStar;
using StarsAbove.Buffs.RedMage;
using StarsAbove.Buffs.Skofnung;
using System;
using System.Security.Policy;
using Terraria;using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.RedMage
{
	public class RedMageFocus : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Mage's Foci");
		}

		public override void SetDefaults()
		{
			// This method right here is the backbone of what we're doing here; by using this method, we copy all of
			// the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
			// so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
			// if you're going to copy the stats of a projectile, use CloneDefaults().

			//Projectile.CloneDefaults(ProjectileID.SuspiciousTentacle);

			// To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
			// the projectile to essentially behave the same way as the vanilla projectile.
			//AIType = ProjectileID.SuspiciousTentacle;
			AIType = 0;
			// After CloneDefaults has been called, we can now modify the stats to our wishes, or keep them as they are.
			// For the sake of example, lets make our projectile penetrate enemies a few more times than the vanilla projectile.
			// This can be done by modifying projectile.penetrate
			Projectile.width = 62;
			Projectile.height = 62;
			Projectile.minion = false;
			//Projectile.DamageType = DamageClass.Summon;
			Projectile.minionSlots = 0f;
			Projectile.timeLeft = 240;
			Projectile.penetrate = 999;
			Projectile.hide = false;
			Projectile.alpha = 255;
			Projectile.netImportant = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			//DrawOffsetX = -14;

		}
		int idlePause;
		bool floatUpOrDown; //false is Up, true is Down
		public override bool PreAI()
        {
			Player player = Main.player[Projectile.owner];
			//player.suspiciouslookingTentacle = false;
			return true;
		}
        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Player projOwner = Main.player[Projectile.owner];

			Projectile.scale = 0.7f;
			if (!player.HasBuff(BuffType<RedMageHeldBuff>()))
            {
				Projectile.Kill();
            }
			Projectile.timeLeft = 10;
			Projectile.alpha -= 10;

			player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (player.Center - 
				new Vector2(Projectile.Center.X, (Projectile.Center.Y + DrawOriginOffsetY) + 20)
				).ToRotation() + MathHelper.PiOver2);

			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			
			
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation = player.velocity.X * 0.05f;

			//Projectile.position.X = player.Center.X;
			if (Projectile.spriteDirection == 1)
			{
				Projectile.position.X = player.Center.X;

			}
			else
			{
				Projectile.position.X = player.Center.X - 62;

			}
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) - 12;

			//This is 0 unless a auto attack has been initated, in which it'll tick up.


			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}



			UpdateMovement();
			


			
		}
		private void UpdateMovement()
		{
			if (floatUpOrDown)//Up
			{
				if (Projectile.ai[0] > 7)
				{
					DrawOriginOffsetY++;
					Projectile.ai[0] = 0;
				}
			}
			else
			{
				if (Projectile.ai[0] > 7)
				{
					DrawOriginOffsetY--;
					Projectile.ai[0] = 0;
				}
			}
			if (DrawOriginOffsetY > -10)
			{
				idlePause = 10;
				DrawOriginOffsetY = -10;
				floatUpOrDown = false;

			}
			if (DrawOriginOffsetY < -20)
			{
				idlePause = 10;
				DrawOriginOffsetY = -20;
				floatUpOrDown = true;

			}
			if (idlePause < 0)
			{
				if (DrawOriginOffsetY < -12 && DrawOriginOffsetY > -18)
				{
					Projectile.ai[0] += 2;
				}
				else
				{
					Projectile.ai[0]++;
				}
			}

			idlePause--;

		}

		public override void Kill(int timeLeft)
		{
			

		}

	}
}
