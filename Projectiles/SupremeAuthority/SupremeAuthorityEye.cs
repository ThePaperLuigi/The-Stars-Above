using Microsoft.Xna.Framework;
using StarsAbove.Buffs.SupremeAuthority;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.SupremeAuthority
{
    public class SupremeAuthorityEye : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Supreme Authority");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 312;               //The width of projectile hitbox
			Projectile.height = 124;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.minion = true;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		bool firstSpawn = true;
		float exitY = 0f;
		double dist;
		public override void AI()
		{
			DrawOriginOffsetY = -15;
			DrawOffsetX = -4;
			Projectile.ai[1] = 90;
			Projectile.alpha -= 10;
			Projectile.ai[0] += 0.02f;
			Projectile.ai[0] = MathHelper.Clamp(Projectile.ai[0], 0, 1);
			if (firstSpawn)
			{
				

				
				
				firstSpawn = false;
			}
			
			Player player = Main.player[Projectile.owner];
			if (player.dead && !player.active)
			{
				Projectile.Kill();
			}
			
			//Factors for calculations
			double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
			double rad = deg * (Math.PI / 180); //Convert degrees to radians

			if(Projectile.timeLeft > 120)
            {
				dist = MathHelper.Lerp(600,300,EaseHelper.OutQuad(Projectile.ai[0]));

			}
			else
            {
				dist = 300 + exitY; //Distance away from the player

			}

			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
			Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
			Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

			//Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
			//projectile.ai[1] += 2f;

			//Marked targets are being prepared...
			if (Projectile.timeLeft > 120)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.active && npc.friendly && npc.HasBuff(BuffType<AuthoritySacrificeMark>()))
					{
						//Link target
						for (int ir = 0; ir < 30; ir++)
						{
							Vector2 position = Vector2.Lerp(Projectile.Center, npc.Center, (float)ir / 30);
							Dust da = Dust.NewDustPerfect(position, DustID.GemTopaz, null, 0, default(Color), 0.4f);
							da.noLight = false;
							da.noGravity = true;

						}
						npc.velocity = Vector2.Zero;
					}
				}
			}
			

			//Consume targets.
			if (Projectile.timeLeft == 120)
			{
				SoundEngine.PlaySound(StarsAboveAudio.SFX_Deify, player.Center);
				player.GetModPlayer<StarsAbovePlayer>().activateAuthorityShockwaveEffect = true;
				if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
				{
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC npc = Main.npc[i];
						if (npc.active && npc.friendly && npc.Distance(Projectile.Center) < 660)
						{
							player.AddBuff(BuffType<DeifiedBuff>(), 60 * 60);

							//Dust visuals.
							for (int ir = 0; ir < 50; ir++)
							{
								Vector2 position = Vector2.Lerp(Projectile.Center, npc.Center, (float)ir / 50);
								Dust da = Dust.NewDustPerfect(position, DustID.GemTopaz, null, 240, default(Color), 2.7f);
								da.fadeIn = 0.5f;
								da.noLight = false;
								da.noGravity = true;

							}
							for (int d1 = 0; d1 < 25; d1++)
							{
								Dust.NewDust(npc.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
							}

							//Give the projectile owner stats for each consumption.
							player.GetModPlayer<WeaponPlayer>().SupremeAuthorityConsumedNPCs++;
							player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -70;

							//Kill the npc.
							npc.life = 0;
							npc.HitEffect();
							npc.checkDead();
							npc.active = false;

							for (int d1 = 0; d1 < 15; d1++)
							{
								Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
							}

							// Smoke Dust spawn
							for (int i5 = 0; i5 < 10; i5++)
							{
								int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-26, 26), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
								Main.dust[dustIndex].velocity *= 1.4f;
							}


							for (int d1 = 0; d1 < 15; d1++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.GemAmethyst, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1f);
								Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1f);


							}

						}
					}
				}
				else
                {
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC npc = Main.npc[i];
						if (npc.active && npc.friendly && npc.HasBuff(BuffType<AuthoritySacrificeMark>()))
						{
							player.AddBuff(BuffType<DeifiedBuff>(), 60 * 60);

							//Dust visuals.
							for (int ir = 0; ir < 50; ir++)
							{
								Vector2 position = Vector2.Lerp(Projectile.Center, npc.Center, (float)ir / 50);
								Dust da = Dust.NewDustPerfect(position, DustID.GemTopaz, null, 240, default(Color), 2.7f);
								da.fadeIn = 0.5f;
								da.noLight = false;
								da.noGravity = true;

							}
							for (int d1 = 0; d1 < 25; d1++)
							{
								Dust.NewDust(npc.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
							}

							//Give the projectile owner stats for each consumption.
							player.GetModPlayer<WeaponPlayer>().SupremeAuthorityConsumedNPCs++;
							player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -70;

							//Kill the npc.
							npc.life = 0;
							npc.HitEffect();
							npc.checkDead();
							npc.active = false;

							for (int d1 = 0; d1 < 15; d1++)
							{
								Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
							}

							// Smoke Dust spawn
							for (int i5 = 0; i5 < 10; i5++)
							{
								int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-26, 26), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
								Main.dust[dustIndex].velocity *= 1.4f;
							}


							for (int d1 = 0; d1 < 15; d1++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.GemAmethyst, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1f);
								Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1f);


							}

						}
					}
				}
				
			}
			
			if(Projectile.timeLeft < 60)
            {
				if(exitY == 0)
                {
					exitY = 1;
                }
				exitY *= 1.1f;
            }

			Vector2 vector = new Vector2(
					Main.rand.Next(-50, 50) * (0.003f * 4 - 10),
					Main.rand.Next(-50, 50) * (0.003f * 4 - 10));
			Dust d = Main.dust[Dust.NewDust(
				Projectile.Center + vector, 1, 1,
				DustID.FireworkFountain_Yellow, 0, 0, 255,
				Color.White, 1f)];
			d.velocity = -vector / 14;
			d.velocity -= player.velocity / 8;
			d.noLight = true;
			d.noGravity = true;
			
		}

        public override void Kill(int timeLeft)
        {

			base.Kill(timeLeft);
        }

    }
}
