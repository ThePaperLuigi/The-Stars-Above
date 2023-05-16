using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.LevinstormAxe;
using StarsAbove.Effects;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.LevinstormAxe
{
    public class LevinstormLightning : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Levinstorm Axe");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = ProjectileID.Sets.TrailCacheLength[ProjectileID.VortexLightning];    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;        //The recording mode
			//DrawOffsetX = 40;
			//DrawOriginOffsetY = 81;
		}

		public override void SetDefaults() {
			Projectile.width = 14;               //The width of projectile hitbox
			Projectile.height = 14;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete Projectile if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 4;            //Set to above 0 if you want the projectile to update multiple time in a frame

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		
		
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (Projectile.frameCounter == 0 || Projectile.oldPos[0] == Vector2.Zero)
			{
				for (int num8 = Projectile.oldPos.Length - 1; num8 > 0; num8--)
				{
					Projectile.oldPos[num8] = Projectile.oldPos[num8 - 1];
				}
				Projectile.oldPos[0] = Projectile.position;
				if (Projectile.velocity == Vector2.Zero)
				{

				}
			}
			/*
			if (Projectile.localAI[1] < 1f)
			{
				Projectile.localAI[1] += 2f;
				Projectile.position += Projectile.velocity;
				Projectile.velocity = Vector2.Zero;
			}
			*/
			if (Projectile.localAI[1] == 0f && Projectile.ai[0] >= 900f)
			{
				Projectile.ai[0] -= 1000f;
				Projectile.localAI[1] = -1f;
			}
			Projectile.frameCounter++;
			Lighting.AddLight(Projectile.Center, 0.3f, 0.45f, 0.5f);
			if (Projectile.velocity == Vector2.Zero)
			{
				if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
				{
					Projectile.frameCounter = 0;
					bool flag26 = true;
					for (int num796 = 1; num796 < Projectile.oldPos.Length; num796++)
					{
						if (Projectile.oldPos[num796] != Projectile.oldPos[0])
						{
							flag26 = false;
						}
					}
					if (flag26)
					{
						Projectile.Kill();
						return;
					}
				}
				if (Main.rand.Next(Projectile.extraUpdates) == 0 && (Projectile.velocity != Vector2.Zero || Main.rand.Next((Projectile.localAI[1] == 2f) ? 2 : 6) == 0))
				{
					for (int num797 = 0; num797 < 2; num797++)
					{
						float num798 = Projectile.rotation + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * ((float)Math.PI / 2f);
						float num800 = (float)Main.rand.NextDouble() * 0.8f + 1f;
						Vector2 vector61 = new Vector2((float)Math.Cos(num798) * num800, (float)Math.Sin(num798) * num800);
						int num801 = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz, vector61.X, vector61.Y);
						Main.dust[num801].noGravity = true;
						Main.dust[num801].scale = 1.2f;
					}
					if (Main.rand.Next(5) == 0)
					{
						Vector2 value37 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
						int num802 = Dust.NewDust(Projectile.Center + value37 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
						Dust dust77 = Main.dust[num802];
						Dust dust195 = dust77;
						dust195.velocity *= 0.5f;
						Main.dust[num802].velocity.Y = 0f - Math.Abs(Main.dust[num802].velocity.Y);
					}
				}
			}
			else
			{
				if (Projectile.frameCounter < Projectile.extraUpdates * 2)
				{
					return;
				}
				Projectile.frameCounter = 0;
				float num803 = Projectile.velocity.Length();
				UnifiedRandom unifiedRandom2 = new UnifiedRandom((int)Projectile.ai[1]);
				int num804 = 0;
				Vector2 spinningpoint7 = -Vector2.UnitY;
				while (true)
				{
					int num805 = unifiedRandom2.Next();
					Projectile.ai[1] = num805;
					num805 %= 100;
					float f2 = (float)num805 / 100f * ((float)Math.PI * 2f);
					Vector2 vector62 = f2.ToRotationVector2();
					if (vector62.Y > 0f)
					{
						vector62.Y *= -1f;
					}
					bool flag27 = false;
					if (vector62.Y > -0.02f)
					{
						flag27 = true;
					}
					if (vector62.X * (float)(Projectile.extraUpdates + 1) * 2f * num803 + Projectile.localAI[0] > 40f)
					{
						flag27 = true;
					}
					if (vector62.X * (float)(Projectile.extraUpdates + 1) * 2f * num803 + Projectile.localAI[0] < -40f)
					{
						flag27 = true;
					}
					if (flag27)
					{
						if (num804++ >= 100)
						{
							Projectile.velocity = Vector2.Zero;
							if (Projectile.localAI[1] < 1f)
							{
								Projectile.localAI[1] += 2f;
							}
							break;
						}
						continue;
					}
					spinningpoint7 = vector62;
					break;
				}
				if (!(Projectile.velocity != Vector2.Zero))
				{
					return;
				}
				Projectile.localAI[0] += spinningpoint7.X * (float)(Projectile.extraUpdates + 1) * 2f * num803;
				Projectile.velocity = spinningpoint7.RotatedBy(Projectile.ai[0] + (float)Math.PI / 2f) * num803;
				Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
				if (Main.rand.Next(4) == 0 && Main.netMode != NetmodeID.MultiplayerClient && Projectile.localAI[1] == 0f)
				{
					float num806 = (float)Main.rand.Next(-3, 4) * ((float)Math.PI / 3f) / 3f;
					Vector2 vector63 = Projectile.ai[0].ToRotationVector2().RotatedBy(num806) * Projectile.velocity.Length();
					Projectile.NewProjectile(null, Projectile.Center.X - vector63.X, Projectile.Center.Y - vector63.Y, vector63.X, vector63.Y, Projectile.type, Projectile.damage/2, Projectile.knockBack, Projectile.owner, vector63.ToRotation() + 1000f, Projectile.ai[1]);

				}
			}
		}
		
		public override bool PreDraw(ref Color lightColor)
		{
			//default(MedBlueTrail).Draw(Projectile);
			

			Vector2 end3 = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
			Texture2D value145 = TextureAssets.Extra[33].Value;
			Projectile.GetAlpha(lightColor);
			Vector2 vector57 = new Vector2(Projectile.scale) / 2f;
			for (int num360 = 0; num360 < 2; num360++)
			{
				float num361 = ((Projectile.localAI[1] == -1f || Projectile.localAI[1] == 1f) ? (-0.2f) : 0f);
				if (num360 == 0)
				{
					vector57 = new Vector2(Projectile.scale) * (0.5f + num361);
					DelegateMethods.c_1 = new Color(250, 227, 115, 0) * 0.5f; //This is the color of the lightning!
				}
				else
				{
					vector57 = new Vector2(Projectile.scale) * (0.3f + num361);
					DelegateMethods.c_1 = new Color(255, 255, 255, 0) * 0.5f;
				}
				DelegateMethods.f_1 = 1f;
				for (int num362 = Projectile.oldPos.Length - 1; num362 > 0; num362--)
				{
					if (!(Projectile.oldPos[num362] == Vector2.Zero))
					{
						Vector2 start3 = Projectile.oldPos[num362] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
						Vector2 end4 = Projectile.oldPos[num362 - 1] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
						Utils.DrawLaser(Main.spriteBatch, value145, start3, end4, vector57, DelegateMethods.LightningLaserDraw);
					}
				}
				if (Projectile.oldPos[0] != Vector2.Zero)
				{
					DelegateMethods.f_1 = 1f;
					Vector2 start4 = Projectile.oldPos[0] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
					Utils.DrawLaser(Main.spriteBatch, value145, start4, end3, vector57, DelegateMethods.LightningLaserDraw);
				}
			}

			return true;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.7f);
				Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.3f);

			}
			SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, target.Center);
			if(Main.player[Projectile.owner].HasBuff(BuffType<GatheringLevinstorm>()) && crit)
            {
				Projectile.NewProjectile(null, Projectile.Center.X, Projectile.Center.Y - 600, 0, 7, Projectile.type, Projectile.damage / 4, Projectile.knockBack, Projectile.owner, MathHelper.ToRadians(90), 1);

			}
		}
	}
}
