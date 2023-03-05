using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Nalhaun
{
    public class LaevateinnSwingLeft : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ars Laevateinn");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			//DrawOffsetX = 40;
			//DrawOriginOffsetY = 81;
		}

		public override void SetDefaults() {
			Projectile.width = 280;               //The width of projectile hitbox
			Projectile.height = 280;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = true;         //Can the projectile deal damage to the player?
			//Projectile.DamageType = ModContent.GetInstance<Systems.AuricDamageClass>();
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 25;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 245;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

			//Projectile.usesLocalNPCImmunity = true;
			//Projectile.localNPCHitCooldown = -1;
		}
		int direction;//0 is right 1 is left
		float rotationProgress;
		bool firstSpawn = true;
		double deg;
		public override void AI()
		{
			
			Player player = Main.player[Projectile.owner];

			//Appear and wait for the attack to go off, then swing.
			
			if (firstSpawn)
            {
				Projectile.scale = 1.3f;
				Projectile.ai[1] = 90;
				
				firstSpawn = false;
            }
      if (!NPC.AnyNPCs(ModContent.NPCType<NalhaunBoss>()) && !NPC.AnyNPCs(ModContent.NPCType<NalhaunBossPhase2>()))
			{

				Projectile.Kill();
			}
			if(Projectile.ai[0] <= 0)
            {
				if(rotationProgress < 0.8)
                {
					rotationProgress += 0.05f;

				}
				else
                {
					rotationProgress += 0.01f;
					Projectile.alpha += 35;
				}
				rotationProgress = Math.Clamp(rotationProgress, 0, 1f);
			}
			else
            {
				Projectile.alpha -= 15;
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y + 15), 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-8, 8), 0f + Main.rand.Next(-8, 8), 150, default(Color), 0.7f);

			}
			if (Projectile.alpha > 250)
            {
				Projectile.Kill();
            }
			if (Projectile.ai[0] == 0)
			{
				SoundEngine.PlaySound(StarsAboveAudio.SFX_Laevateinn, Projectile.Center);

				int type = ProjectileType<NalhaunCleave>();
				int damage = 50;

				for (int ir = 0; ir < Main.maxNPCs; ir++)
				{
					NPC npc = Main.npc[ir];

					if (npc.active && (npc.type == ModContent.NPCType<NalhaunBoss>() || npc.type == ModContent.NPCType<NalhaunBossPhase2>()))
					{
						var entitySource = npc.GetSource_FromAI();

						Projectile.NewProjectile(entitySource, new Vector2(npc.Center.X - 300, npc.Center.Y), Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f, 0f);

						for (int i = 0; i < 60; i++)
						{
							Dust.NewDust(new Vector2(npc.Center.X - 60, npc.Center.Y), 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-38, 0), 0f + Main.rand.Next(-18, 18), 150, default(Color), 1.7f);

						}
						for (int i = 0; i < 50; i++)
						{
							Dust.NewDust(new Vector2(npc.Center.X - 60, npc.Center.Y), 0, 0, DustID.Flare, 0f + Main.rand.Next(-28, 0), 0f + Main.rand.Next(-18, 18), 150, default(Color), 3.7f);

						}
						for (int i = 0; i < 50; i++)
						{
							Dust.NewDust(new Vector2(npc.Center.X - 60, npc.Center.Y), 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-48, 0), 0f + Main.rand.Next(-18, 18), 150, default(Color), 2.7f);

						}
						break;

					}
				}

				Projectile.ai[0]--;
			}
			Projectile.ai[0]--;
			Projectile.timeLeft = 25;


			deg = MathHelper.Lerp(0, -180, EaseHelper.InOutQuad(rotationProgress)) + Projectile.ai[1];


			//deg = 10;
			
			double rad = deg * (Math.PI / 180);
			double dist = 300;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC other = Main.npc[i];

				if (other.active && (other.type == ModContent.NPCType<NalhaunBoss>() || other.type == ModContent.NPCType<NalhaunBossPhase2>()))
				{
					Projectile.position.X = other.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
					Projectile.position.Y = other.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
					Projectile.rotation = Vector2.Normalize(other.Center - Projectile.Center).ToRotation() + MathHelper.ToRadians(0f);
					return;
				}
			}
			
			

			
			//player.itemRotation = Projectile.rotation;


		}
		public override bool PreDraw(ref Color lightColor)
		{
			if(Projectile.ai[0] < 0.2)
            {
				//Redraw the projectile with the color not influenced by light
				Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
				for (int k = 0; k < Projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Color color = Color.White * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
					Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
				}
			}
			
			return true;
		}


	}
}
