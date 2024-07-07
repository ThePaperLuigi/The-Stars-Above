using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.StellarNovas;
using StarsAbove.Projectiles.Extra;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ranged.QuisUtDeus
{
    public class CallOfTheStarsBackground : ModProjectile
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Projectile.width = 1000;
			Projectile.height = 1000;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 480;
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
			behindNPCs.Add(index);
		}
		bool firstSpawn = true;
        
		float maxScale = 0.4f;
        Vector2 originalPosition;
		public override void AI()
        {
            Projectile.ai[1] += 0.02f;
            if (firstSpawn)
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    originalPosition = Projectile.Center;
                    Projectile.timeLeft = (int)Projectile.ai[1] + 60;

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<CallOfTheStarsBorder>(), 0, 0, Main.player[Projectile.owner].whoAmI,0,Projectile.timeLeft);
                   
                }
                
                Projectile.scale = 0.001f;
                firstSpawn = false;
            }
            if(Main.rand.NextBool(15))
            {
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PaladinsHammer,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(Main.player[Projectile.owner].Hitbox) },
                Projectile.owner);
            }
            if (Main.rand.NextBool(35))
            {
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = new Vector2(Projectile.Center.X + Main.rand.Next(-40,40), Projectile.Center.Y + Main.rand.Next(-40,40))},
                Projectile.owner);
            }
            Projectile.Center = Main.player[Projectile.owner].Center;
            Projectile.ai[0] = MathHelper.Clamp(Projectile.ai[0], 0f, 1f);
            Projectile.scale = MathHelper.Lerp(0, maxScale, EaseHelper.InOutQuad(Projectile.ai[0]));
            if (Projectile.scale <= 0 && Projectile.timeLeft < 60)
            {
                Projectile.Kill();
            }

            if (Projectile.timeLeft < 60)
            {
                Projectile.ai[0] -= 0.03f;
            }
            else
            {
                Projectile.ai[0] += 0.01f;//Time alive

            }



            Point z = Projectile.Center.ToTileCoordinates();
            int radius = (int)MathHelper.Lerp(0, 24 * maxScale, EaseHelper.InOutQuad(Projectile.ai[0]));
            int realRadius = (int)(radius * 16);


            for (int i = 0; i < 2; i++)
            {
                // Charging dust
                Vector2 vector = new Vector2(
                    Main.rand.Next(-realRadius, realRadius),
                    Main.rand.Next(-realRadius, realRadius));
                Dust d = Main.dust[Dust.NewDust(
                    Projectile.Center + vector, 1, 1,
                    DustID.GemAmethyst, 0, 0, 255,
                    new Color(1f, 1f, 1f), 1f)];

                d.velocity = vector / 16;
                d.velocity -= Projectile.velocity / 8;
                d.noLight = true;
                d.noGravity = true;
            }
            

        }

       
        

       

        public override void OnKill(int timeLeft)
        {
			float dustAmount = 50f;
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
				spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
				int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.GemAmethyst);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = new Vector2(Projectile.Center.X, Projectile.Center.Y) + spinningpoint5;
				Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 9f;
			}
			
		}
        public static Texture2D texture;

        int frameTimer;
        int frameY;
        public override bool PreDraw(ref Color lightColor)
        {
            

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);

			
			var Texture = Mod.Assets.Request<Texture2D>("Effects/QuisUtDeusBackground");
            GameShaders.Misc["CyclePass"].UseImage1(Texture);
            // Retrieve the shader registered in Mod.Load and pass in an additional image
            GameShaders.Misc["CyclePass"].UseImage1(Texture);

            GameShaders.Misc["CyclePass"].UseColor(Color.Aqua);

            // Any other parameters that might be needed but aren't supplied normally
            GameShaders.Misc["CyclePass"].Shader.Parameters["uUIPosition"].SetValue(new Vector2(-Main.LocalPlayer.Center.X, -Main.LocalPlayer.Center.Y));

            GameShaders.Misc["CyclePass"].Shader.Parameters["uDrawWithColor"].SetValue(false);
            GameShaders.Misc["CyclePass"].Shader.Parameters["uDrawInTooptipCoords"].SetValue(true);

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
            Vector2 origin2 = sourceRectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY ),
                sourceRectangle, Color.White * 0.5f, Projectile.rotation, origin2, Projectile.scale, spriteEffects, 0);

            

            //This applies to everything, so we end the batch again so things go back to normal.
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.UIScaleMatrix);

            
            return false;
        }
        
	}
	

    public class CallOfTheStarsLighting : ModSystem
    {
        public override void Load()
        {
			Terraria.Graphics.Light.On_TileLightScanner.GetTileLight += Light;
        }
		//Modified version of Dragonlens light to fix a visual bug
		private void Light(Terraria.Graphics.Light.On_TileLightScanner.orig_GetTileLight originalLight, Terraria.Graphics.Light.TileLightScanner self, int x, int y, out Vector3 outputColor)
		{
			originalLight(self, x, y, out outputColor);
			if (Main.LocalPlayer.ownedProjectileCounts[ProjectileType<CallOfTheStarsBackground>()] >= 1)
            {
				outputColor += Vector3.One * 0.01f;

			}
			
		}
	}
	
		
    
}
