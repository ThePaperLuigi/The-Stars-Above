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
    public class NalhaunCastSprite : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Nalhaun, the Burnished King");
			//Main.projFrames[Projectile.type] = 10;
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
			Projectile.tileCollide = false;
			
		}
		int timer;
		

		public override void AI() {
			DrawOriginOffsetY = 26;
			DrawOffsetX = 0;
			if (!NPC.AnyNPCs(ModContent.NPCType<NalhaunBoss>()))
			{

				Projectile.Kill();
			}
			for (int d = 0; d < 2; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X + 70, Projectile.Center.Y - 15), 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 0.8f);
			}
			for (int i = 0; i < 2; i++)
			{
				// Charging dust
				Vector2 vector = new Vector2(
					Main.rand.Next(-2048, 2048) * (0.003f * 100) + 175,
					Main.rand.Next(-2048, 2048) * (0.003f * 100) - 175);
				Dust d = Main.dust[Dust.NewDust(
					Projectile.Center + vector, 1, 1,
					DustID.Firework_Blue, 0, 0, 155,
					new Color(1f, 1f, 1f), 0.7f)];
				d.velocity = -vector / 16;
				d.velocity -= Projectile.velocity / 8;
				d.noLight = true;
				d.noGravity = true;
			}

			timer++;
			if (timer >= Projectile.ai[0])
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
