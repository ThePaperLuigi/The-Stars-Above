using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs;

namespace StarsAbove.Projectiles.Bosses.Penthesilea
{
    public class PenthCastingPage : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Penthesilea's Muse");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 15;
			
		}

		public override void SetDefaults() {
			Projectile.width = 36;               //The width of projectile hitbox
			Projectile.height = 36;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.minion = true;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.hide = true;
			DrawOriginOffsetY = -15;
			DrawOffsetX = -26;
		}
		float rotationSpeed = 10f;
		int offsetVelocity;
		int idlePause;
		bool floatUpOrDown; //false is Up, true is Down
		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{

			behindNPCsAndTiles.Add(index);

		}
		public override void AI()
		{
			
			Projectile.timeLeft = 10;

			if (!NPC.AnyNPCs(NPCType<NPCs.Penthesilea>()))
			{

				Projectile.Kill();
			}

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];



				if (npc.active && npc.type == NPCType<NPCs.Penthesilea>())
				{
					//Projectile.Center = npc.Center;
					Projectile.position.X = npc.Center.X - Projectile.width / 2 + 55;
					Projectile.position.Y = npc.Center.Y - Projectile.height / 2 + 5;
					
					if(npc.HasBuff(BuffType<CastFinished>()))
                    {
						Projectile.Kill();
                    }
				}




			}

			if (++Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 14)
				{

					Projectile.frame = 0;

				}

			}

			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}


			

			UpdateMovement();
			
			





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

        public override void OnKill(int timeLeft)
        {
			for (int d = 0; d < 16; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default(Color), 1f);
			}
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default(Color), 1f);
			}


		}
    }
}
