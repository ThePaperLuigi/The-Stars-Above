using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.StellarNovas
{
    public class UnlimitedBladeWorksBackground : ModProjectile
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Projectile.width = 1000;
			Projectile.height = 1000;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 240;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.hide = true;
			Projectile.hostile = false;
			Projectile.friendly = false;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.netUpdate = true;

		}
		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			// Add this projectile to the list of projectiles that will be drawn BEFORE tiles and NPC are drawn. This makes the projectile appear to be BEHIND the tiles and NPC.
			behindNPCsAndTiles.Add(index);
		}
		bool firstSpawn = true;
	
		public override void AI()
		{
			if(firstSpawn)
            {
				if(Main.netMode != NetmodeID.MultiplayerClient)
                {
					Projectile.NewProjectile(null, Projectile.Center, Vector2.Zero, ProjectileType<UnlimitedBladeWorksBorder>(), 0, 0, Main.player[Projectile.owner].whoAmI);

				}
				Projectile.scale = 0.001f;
				firstSpawn = false;
            }
			Projectile.ai[0] += 0.01f;//Time alive
			Projectile.ai[0] = MathHelper.Clamp(Projectile.ai[0], 0f, 1f);
			Projectile.scale = MathHelper.Lerp(0,1, EaseHelper.InOutQuad(Projectile.ai[0]));
			Projectile.scale = MathHelper.Clamp(Projectile.scale, 0, 1);

			Point z = Projectile.Center.ToTileCoordinates();
			int radius = (int)MathHelper.Lerp(0, 23, EaseHelper.InOutQuad(Projectile.ai[0]));
			for (int x = -radius; x <= radius; x++)
			{
				for (int y = -radius; y <= radius; y++)
				{
					if (x * x + y * y <= radius * radius)
					{
						int tileX = z.X + x;
						int tileY = z.Y + y;

						Tile tile = Main.tile[tileX, tileY];
						if (tile.HasTile)
						{
							
						}
					}
				}
			}

		}
        public override void Kill(int timeLeft)
        {
			

		}
        public static Texture2D texture;

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);

			
			var Texture = Mod.Assets.Request<Texture2D>("SceneEffects/CustomSkies/EdinSkyBG");
            GameShaders.Misc["CyclePass"].UseImage1(Texture);
            // Retrieve the shader registered in Mod.Load and pass in an additional image
            GameShaders.Misc["CyclePass"].UseImage1(Texture);

            GameShaders.Misc["CyclePass"].UseColor(Color.Aqua);

            // Any other parameters that might be needed but aren't supplied normally
            GameShaders.Misc["CyclePass"].Shader.Parameters["uUIPosition"].SetValue(new Vector2(-Main.LocalPlayer.Center.X, -Main.LocalPlayer.Center.Y));

            GameShaders.Misc["CyclePass"].Shader.Parameters["uDrawWithColor"].SetValue(false);
            GameShaders.Misc["CyclePass"].Shader.Parameters["uDrawInTooptipCoords"].SetValue(false);

            // Apply the shader
            GameShaders.Misc["CyclePass"].Apply();
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (texture == null || texture.IsDisposed)
            {
                texture = (Texture2D)ModContent.Request<Texture2D>(Projectile.ModProjectile.Texture);
            }
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, Color.White * 0.5f, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

			

			//This applies to everything, so we end the batch again so things go back to normal.
			Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);

			
			return false;
        }
        
	}

	public class UnlimitedBladeWorksTile : GlobalTile
    {
		public bool inEffect;
        public override void DrawEffects(int i, int j, int type, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
			/*for(int r = 0; r < Main.maxProjectiles; r++)
            {
				Projectile proj = Main.projectile[r];
				if (proj.type == ProjectileType<UnlimitedBladeWorksBackground>())
                {
					Point z = proj.Center.ToTileCoordinates();
					int radius = (int)MathHelper.Lerp(0, 23, EaseHelper.InOutQuad(proj.ai[0]));
					for (int x = -radius; x <= radius; x++)
					{
						for (int y = -radius; y <= radius; y++)
						{
							if (x * x + y * y <= radius * radius)
							{
								int tileX = z.X + x;
								int tileY = z.Y + y;

								Tile tile = Main.tile[tileX, tileY];
								if (tile.HasTile)
								{
									Tile tileAbove = Framing.GetTileSafely(i, j - 1);
									if(!tileAbove.HasTile)//If the tile above is air...
                                    {
										drawData.finalColor = Color.Black;

									}
									else
                                    {
										drawData.finalColor = Color.Red;
									}
									
								}
							}
						}
					}
				}
            }*/

            base.DrawEffects(i, j, type, spriteBatch, ref drawData);
        }
		
    }
}
