using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Vagrant;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Vagrant
{
    public class VagrantSpearSprite : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Vagrant of Space and Time");
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 160;
			Projectile.height = 80;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1;
			Projectile.alpha = 0;
			Projectile.damage = 0;
			Projectile.hide = false;
			Projectile.timeLeft = 40;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			
		}
		int timer;
		int fadeIn = 0;

		bool firstTurn = true;
		float projectileVelocity = 15;

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

        // It appears that for this AI, only the ai0 field is used!
        public override bool PreAI()
        {
			if(firstTurn)
            {
				firstTurn = false;
				for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
				{
					NPC other = Main.npc[i];

					if (other.active && other.type == ModContent.NPCType<VagrantBoss>())
					{
						Projectile.position = new Vector2(other.position.X, other.position.Y + 80);
						Projectile.direction = (Main.player[other.target].Center.X < Projectile.Center.X).ToDirectionInt();
						Projectile.spriteDirection = Projectile.direction;
						return true;
					}
				}
			}
			else
            {

            }
			
			return base.PreAI();
        }
        public override void AI() {
			
			
			fadeIn += 5;
			
			timer++;
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
			
			
			
			

			if (Projectile.timeLeft < 10)
			{

				Projectile.alpha -= 5;
			}
			
			
			Projectile.position += Projectile.velocity * movementFactor;
			for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
			{
				NPC other = Main.npc[i];

				if (other.active && other.type == ModContent.NPCType<VagrantBoss>())
				{
					Projectile.position = new Vector2(other.position.X, other.position.Y + 80);
					
					return;
				}
			}


			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}

		}

       
    }
}
