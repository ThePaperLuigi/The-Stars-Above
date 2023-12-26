using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.NPCs.Tsukiyomi;
using Terraria;using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
	public class TsukiStygianIndicator: ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Stygian Memento Indicator");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 30;               //The width of projectile hitbox
			Projectile.height = 30;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = true;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			
		}
		public override void AI()
		{

			//DrawOriginOffsetY = -90;
			Projectile.timeLeft = 10;
			Projectile.alpha -= 10;
			Projectile.ai[0]--;
			SearchForTargets(out bool foundTarget, out Vector2 targetCenter);
			Projectile.velocity.X = -35.5f;

			if (foundTarget)
			{
				//Projectile.position.Y = targetCenter.Y;


			}

			if (Projectile.ai[0] <= 0)
			{
				Projectile.Kill();
			}


		}
		private void SearchForTargets(out bool foundTarget, out Vector2 targetCenter)
		{


			targetCenter = Projectile.position;
			foundTarget = false;



			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.active && npc.type == ModContent.NPCType<TsukiyomiBoss>() || npc.type == ModContent.NPCType<TsukiyomiBoss>())//temp
					{
						foundTarget = true;
						targetCenter = Main.player[npc.target].Center;
					}
				}
			}

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			//Projectile.friendly = foundTarget;
		}
		public override bool PreDraw(ref Color lightColor) {
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Color.White * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void OnKill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			
			// Play explosion sound
			
			// Large Smoke Gore spawn
			

		}
	}
}
