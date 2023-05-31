using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class WarriorOfLightCastingSprite : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Vagrant of Space and Time");
			Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 300;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1;
			Projectile.alpha = 255;
			Projectile.damage = 0;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			
		}
		int timer;
		int fadeIn = 0;
		
		float projectileVelocity = 15;

		public override void AI() {
			timer++;
			
			fadeIn += 5;
			Projectile.ai[0]--;
			if(Projectile.ai[0] <= 0)
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
			if(timer < 20)
            {
				Projectile.frameCounter = 0;
			}
			
			Projectile.alpha--;
			if (++Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 4)
				{
					
					Projectile.frame = 0;

				}

			}
			if(Projectile.frame == 13)
            {
				Projectile.alpha += 46;
			}
			if(Projectile.alpha >= 250)
            {
				Projectile.Kill();
			}
			
			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}
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
			/*
			if (Main.rand.NextBool(3)) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Clentaminator_Green,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
				dust.noGravity = true;
			}
			if (Main.rand.NextBool(4)) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.FireworkFountain_Green,
					0, 0, 269, Scale: 0.3f);
				dust.velocity += Projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
			}*/
		}

       
    }
}
