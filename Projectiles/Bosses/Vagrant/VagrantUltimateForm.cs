using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.NPCs.Vagrant;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Vagrant
{
    public class VagrantUltimateForm : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Vagrant of Space and Time");
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 582;
			Projectile.height = 308;
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
			
			return base.PreAI();
        }
        public override bool PreDraw(ref Color lightColor)
        {

			Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Items/Consumables/SpatialDisk");

			const float TwoPi = (float)Math.PI * 2f;
			float offset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 5f);

			SpriteEffects effects = SpriteEffects.None;

			float scale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 2f) * 0.3f + 0.7f;
			Color effectColor = Color.White;
			effectColor.A = 0;
			effectColor = effectColor * 0.06f * scale;
			for (float num5 = 0f; num5 < 1f; num5 += 355f / (678f * (float)Math.PI))
			{
				Main.spriteBatch.Draw(texture, Projectile.Center + (TwoPi * num5).ToRotationVector2() * (2f + offset), Projectile.getRect(), effectColor, 0f, Projectile.Center, 1f, effects, 0f);
			}

			return base.PreDraw(ref lightColor);
        }
        public override void AI() {

			for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
			{
				NPC other = Main.npc[i];

				if (other.active && other.type == ModContent.NPCType<VagrantBoss>())
				{
					Projectile.position = new Vector2(other.position.X, other.position.Y - 100);
					Projectile.direction = (Main.player[other.target].Center.X < Projectile.Center.X).ToDirectionInt();
					Projectile.spriteDirection = Projectile.direction;
					
				}
			}
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
			


			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}

		}

       
    }
}
