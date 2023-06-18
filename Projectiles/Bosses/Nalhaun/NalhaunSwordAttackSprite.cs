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
    public class NalhaunSwordAttackSprite : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Nalhaun, the Burnished King");
			Main.projFrames[Projectile.type] = 6;
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
			DrawOriginOffsetY = 24;
			DrawOffsetX = 0;
			if (!NPC.AnyNPCs(ModContent.NPCType<NalhaunBoss>()))
			{

				Projectile.Kill();
			}
			if (++Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				if(Projectile.ai[0] > 0)
                {
					if (++Projectile.frame >= 3)
					{

						Projectile.frame = 3;

					}
				}
				else
                {
					if (++Projectile.frame >= 5)
					{
						
						Projectile.frame = 5;

					}
				}
				

			}
			Projectile.ai[0]--;
			if (Projectile.frame == 5)
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
