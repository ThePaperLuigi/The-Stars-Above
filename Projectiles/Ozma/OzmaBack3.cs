using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Ozma;

namespace StarsAbove.Projectiles.Ozma
{
    public class OzmaBack3 : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ozma Ascendant");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 2;
			
		}

		public override void SetDefaults() {
			Projectile.width = 200;               //The width of projectile hitbox
			Projectile.height = 200;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;          //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.minion = true;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			DrawOriginOffsetY = -15;
			DrawOffsetX = -26;
		}
		float rotationSpeed = 10f;
		int offsetVelocity;
		int idlePause;
		bool floatUpOrDown; //false is Up, true is Down
		public override void AI()
		{
			Projectile.scale = 0.9f;
			Projectile.timeLeft = 10;
			Player projOwner = Main.player[Projectile.owner];
			Player player = Main.player[Projectile.owner];
			if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<OzmaBuff>()))
			{
				Projectile.Kill();
			}
			
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2) + 12;
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) - 5;
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation = player.velocity.X * 0.05f;

			//This is 0 unless a auto attack has been initated, in which it'll tick up.
			

			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}


			if(player.HasBuff(BuffType<AnnihilationState>()))
            {
				Projectile.frame = 1;

            }
			else
            {
				Projectile.frame = 0;
            }

			UpdateMovement();
			DrawOriginOffsetY += -projOwner.GetModPlayer<WeaponPlayer>().OzmaSpikeVFXProgression*4;
			Projectile.alpha = projOwner.GetModPlayer<WeaponPlayer>().OzmaSpikeVFXProgression * 10;





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
