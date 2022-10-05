
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.HunterSymphony;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.HunterSymphony
{
    public class MusicRadiate : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Hunter's Symphony");
			
		}

		public override void SetDefaults() {
			Projectile.width = 250;
			Projectile.height = 250;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 300;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.netUpdate = true;


		}
		int slowfade;
		bool start = true;
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		

		
		public override void AI() {
			Projectile.netUpdate = true;
			NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, Projectile.whoAmI);
			Player player = Main.player[Projectile.owner];
			if(Projectile.ai[0] == 0)
            {
				if (Projectile.ai[1] == 0)//Down
				{
					//Down Challenger Song
					for (int i = 0; i < Main.maxPlayers; i++)
					{
						Player other = Main.player[i];
						if (other.active && !other.dead && other.team == player.team)
						{
							other.AddBuff(BuffType<ChallengerSong>(), 1200, quiet:false);  //
							for (int d = 0; d < 15; d++)
							{
								Dust.NewDust(other.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
							}
						}



					}
					player.GetModPlayer<StarsAbovePlayer>().SymphonySongsPlayed++;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HuntingHornBasic, player.Center);
					player.AddBuff(BuffType<HunterSymphonyCooldown>(), 1200);



				}
				if (Projectile.ai[1] == 1)//Up
				{
					//Up Bracing Song
					for (int i = 0; i < Main.maxPlayers; i++)
					{
						Player other = Main.player[i];
						if (other.active && !other.dead && other.team == player.team)
						{
							other.AddBuff(BuffType<BracingSong>(), 1200, quiet: false); ;  //
							for (int d = 0; d < 15; d++)
							{
								Dust.NewDust(other.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
							}
						}



					}
					player.GetModPlayer<StarsAbovePlayer>().SymphonySongsPlayed++;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HuntingHornBasic, player.Center);
					player.AddBuff(BuffType<HunterSymphonyCooldown>(), 1200);
				}
				if (Projectile.ai[1] == 2)//Left
				{
					//Left Vitality Song
					for (int i = 0; i < Main.maxPlayers; i++)
					{
						Player other = Main.player[i];
						if (other.active && !other.dead && other.team == player.team)
						{
							player.statLife += 40;
							other.statLife += 40;
							NetMessage.SendData(MessageID.PlayerHeal, -1, -1, null, i, 40);
							NetMessage.SendData(MessageID.AddPlayerBuff, -1, -1, null, i, BuffType<VitalitySong>(), 1200);
							other.AddBuff(BuffType<VitalitySong>(), 1200, quiet: false); ;  //
							for (int d = 0; d < 15; d++)
							{
								Dust.NewDust(other.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
							}
						}



					}
					player.GetModPlayer<StarsAbovePlayer>().SymphonySongsPlayed++;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HuntingHornBasic, player.Center);
					player.AddBuff(BuffType<HunterSymphonyCooldown>(), 1200);
				}
				if (Projectile.ai[1] == 3)//Right
				{
					//Right Expertise Song
					for (int i = 0; i < Main.maxPlayers; i++)
					{
						Player other = Main.player[i];
						if (other.active && !other.dead && other.team == player.team)
						{
							other.AddBuff(BuffType<ExpertiseSong>(), 1200, quiet: false); ;  //
																			//p.AddBuff(BuffType<ExpertiseSong>(), 1200);  //
							for (int d = 0; d < 15; d++)
							{
								Dust.NewDust(other.Center, 0, 0, DustID.FireworkFountain_Pink, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
							}
						}



					}
					player.GetModPlayer<StarsAbovePlayer>().SymphonySongsPlayed++;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HuntingHornBasic, player.Center);
					player.AddBuff(BuffType<HunterSymphonyCooldown>(), 1200);
				}
				if (Projectile.ai[1] == 4)//Infernal
				{
					//Infernal
					for (int i = 0; i < Main.maxPlayers; i++)
					{
						Player other = Main.player[i];
						if (other.active && !other.dead && other.team == player.team)
						{
							other.AddBuff(BuffType<InfernalMelody>(), 360, quiet: false); ;
							//p.AddBuff(BuffType<ExpertiseSong>(), 1200);  //
							for (int d = 0; d < 15; d++)
							{
								Dust.NewDust(other.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
								Dust.NewDust(other.Center, 0, 0, DustID.FireworkFountain_Pink, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
								Dust.NewDust(other.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
								Dust.NewDust(other.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
							}
						}



					}
					//player.GetModPlayer<StarsAbovePlayer>().SymphonySongsPlayed++;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HuntingHornFinal, player.Center);
					//player.AddBuff(BuffType<HunterSymphonyCooldown>(), 1200);
				}
			}

			Projectile.netUpdate = true;
			Projectile.damage = 0;
			if(start)
            {
				Projectile.scale = 0.1f;
				start = false;
            }
			Projectile.ai[0] += 1f;
			Projectile.scale += 0.1f;
			//projectile.rotation++;
			float rotationsPerSecond = 0.7f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			// Fade in
			if (Projectile.ai[0] <= 30)
            {
				Projectile.alpha -= 4;
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
			}
			else
            {
				Projectile.alpha += 8;
            }


			
		}
	}
}
