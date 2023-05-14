using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Ozma;
using StarsAbove.Buffs.CatalystMemory;
using Terraria.Audio;
using System;

namespace StarsAbove.Projectiles.CatalystMemory
{
    public class Prismic : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Catalyst's Memory");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			//Main.projFrames[Projectile.type] = 1;
			
		}

		public override void SetDefaults() {
			Projectile.width = 34;               //The width of projectile hitbox
			Projectile.height = 48;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.minion = false;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			//DrawOriginOffsetY = -15;
			//DrawOffsetX = -26;
		}
		float rotationSpeed = 10f;
		int offsetVelocity;
		int idlePause;
		bool floatUpOrDown; //false is Up, true is Down
		public override void AI()
		{
			Color projcolor = new Color(174, 0, 255);
			Lighting.AddLight(Projectile.Center, projcolor.ToVector3());
			Projectile.scale = 1.1f;
			Projectile.timeLeft = 10;
			Player projOwner = Main.player[Projectile.owner];
			Player player = Main.player[Projectile.owner];
			if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<Bedazzled>()))
			{
				Projectile.Kill();
			}
			projOwner.GetModPlayer<WeaponPlayer>().CatalystPrismicPosition = Projectile.Center;
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			//Projectile.direction = projOwner.direction;
			//Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			//Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			//Projectile.spriteDirection = Projectile.direction;
			//Projectile.rotation = player.velocity.X * 0.05f;
			Projectile.ai[1]++;



			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}



			UpdateMovement();

			if (Projectile.ai[1] > 60)//Delay before appearing.
			{
				
				Projectile.alpha -= 10;
			}
			else
            {
				Vector2 vector = new Vector2(
						Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
						Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
				Dust d = Main.dust[Dust.NewDust(
					Projectile.Center + vector, 1, 1,
					DustID.FireworkFountain_Pink, 0, 0, 255,
					new Color(0.8f, 0.4f, 1f), 0.8f)];
				d.velocity = -vector / 12;
				d.velocity -= player.velocity / 8;
				d.noLight = true;
				d.noGravity = true;

			}


			// This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
			Vector2 direction = Main.rand.NextVector2CircularEdge(Projectile.width * 0.6f, Projectile.height * 0.6f);
			float distance = 0.3f + Main.rand.NextFloat() * 0.9f;
			Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustPerfect(Projectile.Center + direction * distance, DustID.SilverFlame, velocity);
				dust.scale = 0.5f;
				dust.fadeIn = 1.1f;
				dust.noGravity = true;
				dust.noLight = true;
				//dust.color = Color.Purple;
				dust.alpha = 0;
			}

			//If too far away from the projectile owner, shatter.
			float playerDistance = Vector2.Distance(projOwner.Center, Projectile.Center);

			if (playerDistance > 1000f)
			{
				projOwner.ClearBuff(BuffType<Bedazzled>());

				Projectile.Kill();
			}
			if (Main.myPlayer == player.whoAmI)
			{

				for (int i = 0; i < 40; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * (1000));
					offset.Y += (float)(Math.Cos(angle) * (1000));

					Dust d2 = Dust.NewDustPerfect(Projectile.Center + offset, DustID.FireworkFountain_Pink, Vector2.Zero, 200, default(Color), 0.5f);
					d2.fadeIn = 0.1f;
					d2.noGravity = true;
				}
				
			}
		}
        public override void Kill(int timeLeft)
        {
			SoundEngine.PlaySound(StarsAboveAudio.SFX_PrismicBreak, Projectile.Center);
			Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
			for (int i = 0; i < 5; i++)
			{

				Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3));
				Projectile.NewProjectile(null, Projectile.Center, vel, ProjectileType<PrismicShards>(), Projectile.damage/5, 3, Main.player[Projectile.owner].whoAmI, 0, 1);
			}
			for (int d = 0; d < 18; d++)
			{
				

				Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-15, 15), 150, default(Color), 0.7f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleCrystalShard, Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8), 150, default(Color), 1f);
			}

			base.Kill(timeLeft);
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

	}
}
