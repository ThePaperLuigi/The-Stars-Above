using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Vagrant;
using StarsAbove.NPCs.WarriorOfLight;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class WarriorOfLightSwingingSprite : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Vagrant of Space and Time");
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 300;
			Projectile.height = 300;
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
		int fadeIn = 0;
		bool effectUsed = false;
		float projectileVelocity = 15;

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		
		public override void AI() {
			//DrawOriginOffsetY = -38;
			//DrawOffsetX = -20;
			timer++;
			
			fadeIn += 5;
			Projectile.ai[0]--;
			if (Projectile.ai[0] <= 0)
			{
				Projectile.Kill();
			}
			if (timer >= 60)
			{

			}
			else
			{
				Projectile.alpha -= 5;

				
			}
			if (projectileVelocity < 0)
            {
				projectileVelocity = 0;
            }
			
			Projectile.alpha--;
			if (++Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 2)
				{

					Projectile.frame = 2;

				}

			}
			
			if (Projectile.frame == 5)
			{
				Projectile.alpha += 50;
			}
			if (Projectile.alpha >= 250)
			{
				Projectile.Kill();
			}
			
			
			Projectile.position += Projectile.velocity * movementFactor;

			for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
			{
				NPC other = Main.npc[i];

				if (other.active && other.type == ModContent.NPCType<NPCs.WarriorOfLight.WarriorOfLightBoss>())
				{
					Projectile.position = other.position;
					Projectile.direction = (Main.player[other.target].Center.X < Projectile.Center.X).ToDirectionInt();
					Projectile.spriteDirection = Projectile.direction;
					return;
				}
			}

			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}

		}

       
    }
}
