using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs.Vagrant;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Nalhaun
{
    public class NalhaunSwordSprite : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Nalhaun, the Burnished King");
			Main.projFrames[Projectile.type] = 10;
		}

		public override void SetDefaults() {
			Projectile.width = 360;
			Projectile.height = 360;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1;
			Projectile.alpha = 0;
			Projectile.damage = 0;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			
		}
		int timer;
		

		public override void AI() {
			DrawOriginOffsetY = 26;
			DrawOffsetX = 0;
			
			if (++Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 9)
				{

					Projectile.frame = 9;

				}

			}
			if(Projectile.frame >= 5)
            {
				for (int d = 0; d < 2; d++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y + 15), 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-1, 1), 150, default(Color), 1.5f);
				}
			}
			if (Projectile.frame == 9)
			{
				timer++;
				
			}
			if (timer >= 20)
			{
				Projectile.alpha += 1;
			}
			if (Projectile.alpha >= 3)
			{
				Projectile.Kill();
			}
			for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
			{
				NPC other = Main.npc[i];

				if (other.active && other.type == ModContent.NPCType<NalhaunBoss>())
				{
					Projectile.position = new Vector2(other.position.X - 100, other.position.Y - 180);
					//Projectile.direction = (Main.player[other.target].Center.X < Projectile.Center.X).ToDirectionInt();
					//Projectile.spriteDirection = Projectile.direction;
					return;
				}
			}

			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}

		}

		
	}
}
