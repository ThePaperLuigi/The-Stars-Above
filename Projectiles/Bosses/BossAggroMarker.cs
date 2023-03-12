using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;

namespace StarsAbove.Projectiles.Bosses
{
    public class BossAggroMarker : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Boss Aggro Marker");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
			
		}

		public override void SetDefaults() {
			Projectile.width = 30;               //The width of projectile hitbox
			Projectile.height = 30;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.minion = false;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 250;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.hide = false;
			//Projectile.netImportant = true;
			
		}


		int idlePause;
		bool floatUpOrDown; //false is Up, true is Down
		public override void AI()
		{
			Projectile.timeLeft = 10;
			Player projOwner = Main.player[Projectile.owner];
			Projectile.scale = 1f;
			if (projOwner.dead && !projOwner.active || projOwner.GetModPlayer<BossPlayer>().hasBossAggro <= 0)
			{
				Projectile.alpha += 10;
			}
			else
            {
				Projectile.alpha -= 10;
            }

			if(Projectile.alpha >= 255)
            {
				Projectile.Kill();
            }
			Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha, 0, 255);

			Vector2 ownerMountedCenter = projOwner.Center;

			Projectile.direction = projOwner.direction;
			UpdateMovement();
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) - projOwner.height/2;
			Projectile.rotation = projOwner.velocity.X * 0.02f;
			DrawOffsetX = (int)(projOwner.velocity.X * 1f);
			Projectile.netUpdate = true;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Player projOwner = Main.player[Projectile.owner];

			return true;
		}
		private void UpdateMovement()
		{
			if (floatUpOrDown)//Up
			{
				if (Projectile.ai[0] > 8)
				{
					DrawOriginOffsetY++;
					Projectile.ai[0] = 0;
				}
			}
			else
			{
				if (Projectile.ai[0] > 8)
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

	}
}
