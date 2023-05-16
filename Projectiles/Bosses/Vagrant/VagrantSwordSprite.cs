using StarsAbove.NPCs.Vagrant;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Vagrant
{
    public class VagrantSwordSprite : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Vagrant of Space and Time");
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults() {
			Projectile.width = 260;
			Projectile.height = 160;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1;
			Projectile.alpha = 255;
			Projectile.damage = 0;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			DrawOriginOffsetY = 2;
		}
		int timer;
		int fadeIn = 0;
		
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
			timer++;
			
			fadeIn += 5;
			if (timer == 0)
            {

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

			if (timer > 120)
			{
				
				Projectile.alpha += 16;
			}
			if(timer > 25)
            {
				Projectile.frame = 0;
			}
			else
            {
				Projectile.frame = 1;
			}
			if(Projectile.alpha >= 250)
            {
				Projectile.Kill();
			}
			
			Projectile.position += Projectile.velocity * movementFactor;

			for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
			{
				NPC other = Main.npc[i];

				if (other.active && other.type == ModContent.NPCType<VagrantBoss>())
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
