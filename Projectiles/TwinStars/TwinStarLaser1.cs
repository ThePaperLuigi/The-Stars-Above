using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Enums;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.TwinStars
{
    // The following laser shows a channeled ability, after charging up the laser will be fired
    // Using custom drawing, dust effects, and custom collision checks for tiles
    public class TwinStarLaser1 : ModProjectile
	{
		// Use a different style for constant so it is very clear in code when a constant is used

		// The maximum charge value
		private const float MAX_CHARGE = 10f;
		//The distance charge particle from the player center
		private const float MOVE_DISTANCE = 72f;

		// The actual distance is stored in the ai0 field
		// By making a property to handle this it makes our life easier, and the accessibility more readable
		public float Distance {
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// The actual charge value is stored in the localAI0 field
		public float Charge {
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		// Are we at max charge? With c#6 you can simply use => which indicates this is a get only property
		public bool IsAtMaxCharge => Charge == MAX_CHARGE;

		public override void SetDefaults() {
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.hide = false;
			Projectile.timeLeft = 900;
		}

		public override bool PreDraw(ref Color lightColor) {
			// We start drawing the laser if we have charged up
			if (IsAtMaxCharge) {
				DrawLaser(Main.spriteBatch, (Texture2D)TextureAssets.Projectile[Projectile.type], Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().starPosition1,
					Projectile.velocity, 10, Projectile.damage, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
			}
			return false;
		}

		// The core function of drawing a laser
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50) {
			float r = unit.ToRotation() + rotation;

			// Draws the laser 'body'
			for (float i = transDist; i <= Distance; i += step) {
				Color c = Color.White;
				var origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition,
					new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r,
					new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			}

			// Draws the laser 'tail'
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

			// Draws the laser 'head'
			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
		}

		// Change the way of collision check of the projectile
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
			// We can only collide if we are at max charge, which is when the laser is actually fired
			if (!IsAtMaxCharge) return false;

			Player player = Main.player[Projectile.owner];
			Vector2 unit = Projectile.velocity;
			float point = 0f;
			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.GetModPlayer<WeaponPlayer>().starPosition1,
				player.GetModPlayer<WeaponPlayer>().starPosition1 + unit * Distance, 22, ref point);
		}

		// Set custom immunity time on hitting an NPC
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			target.immune[Projectile.owner] = 4;
		}

		// The AI of the projectile
		public override void AI() {
			Player player = Main.player[Projectile.owner];
			Projectile.position = player.GetModPlayer<WeaponPlayer>().starPosition1 + Projectile.velocity * MOVE_DISTANCE;
			Projectile.timeLeft = 2;

			// By separating large AI into methods it becomes very easy to see the flow of the AI in a broader sense
			// First we update player variables that are needed to channel the laser
			// Then we run our charging laser logic
			// If we are fully charged, we proceed to update the laser's position
			// Finally we spawn some effects like dusts and light

			UpdatePlayer(player);
			ChargeLaser(player);

			// If laser is not charged yet, stop the AI here.
			if (Charge < MAX_CHARGE) return;

			SetLaserPosition(player);
			SpawnDusts(player);
			CastLights();
			if (player.HasBuff(BuffType<BinaryMagnitude>()))
            {
				SpawnNapalm(player);
			}
			
		}
		private void SpawnNapalm(Player player)
		{
			Vector2 dustPos = player.GetModPlayer<WeaponPlayer>().starPosition1 + Projectile.velocity * Distance;
			Vector2 vel = new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-2, -5));

			Projectile.NewProjectile(Projectile.GetSource_FromThis(),dustPos, vel, ProjectileID.MolotovFire, Projectile.damage, 4, 0, 0, 1);
			
			
			
		}
		


		private void SpawnDusts(Player player)
		{
			Vector2 unit = Projectile.velocity * -1;
			Vector2 dustPos = player.GetModPlayer<WeaponPlayer>().starPosition1 + Projectile.velocity * Distance;

			
			for (int i = 0; i < 2; ++i)
			{
				float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 219, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.2f;
				dust = Dust.NewDustDirect(Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().starPosition1, 0, 0, 31,
					-unit.X * Distance, -unit.Y * Distance);
				dust.fadeIn = 0f;
				dust.noGravity = true;
				dust.scale = 0.88f;
				dust.color = Color.OrangeRed;
			}
			float rotation = (float)Math.Atan2(Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().starPosition1.Y - (player.GetModPlayer<WeaponPlayer>().starTarget.Y), Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().starPosition1.X - (player.GetModPlayer<WeaponPlayer>().starTarget.X));//Aim towards mouse

			for (int d = 0; d < 2; d++)
			{
				float Speed2 = Main.rand.NextFloat(10, 70);  //projectile speed
				Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(7)); // 30 degree spread.
				int dustIndex = Dust.NewDust(Projectile.Center - Vector2.One * 4f, 0, 0, 91, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
			}
			for (int b = 0; b < 2; b++)
			{
				float Speed3 = Main.rand.NextFloat(8, 40);  //projectile speed
				Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(46)); // 30 degree spread.
				int dustIndex = Dust.NewDust(Projectile.Center - Vector2.One * 4f, 0, 0, 158, perturbedSpeed.X, perturbedSpeed.Y, 50, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
			}
		}

		/*
		 * Sets the end of the laser position based on where it collides with something
		 */
		private void SetLaserPosition(Player player)
		{
			for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
			{
				var start = player.GetModPlayer<WeaponPlayer>().starPosition1 + Projectile.velocity * Distance;
				if (!Collision.CanHit(player.GetModPlayer<WeaponPlayer>().starPosition1, 1, 1, start, 1, 1)) {
					Distance -= 5f;
					break;
				}
			}
		}

		private void ChargeLaser(Player player)
		{
			// Kill the projectile if the player stops channeling
			if (!player.channel)
			{
				Projectile.Kill();
			}
			else
			{
				// Do we still have enough mana? If not, we kill the projectile because we cannot use it anymore
				if (player.statMana < 10)
				{
					Projectile.Kill();
				}
				Vector2 offset = Projectile.velocity;
				offset *= MOVE_DISTANCE;
				Vector2 pos = player.GetModPlayer<WeaponPlayer>().starPosition1 + offset - new Vector2(10, 10);
				if (Charge < MAX_CHARGE)
				{
					Charge++;
				}
				int chargeFact = (int)(Charge / 20f);
				Vector2 dustVelocity = Vector2.UnitX * 18f;
				dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
				Vector2 spawnPos = Projectile.Center + dustVelocity;
				/*for (int k = 0; k < chargeFact + 1; k++)
				{
					Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
					Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, 226, projectile.velocity.X / 2f, projectile.velocity.Y / 2f)];
					dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
					dust.noGravity = true;
					dust.scale = Main.rand.Next(10, 20) * 0.05f;
				}*/
			}
		}
		
		private void UpdatePlayer(Player player)
		{
			// Multiplayer support here, only run this code if the client running it is the owner of the projectile
			if (Projectile.owner == Main.myPlayer) {
				Vector2 diff = player.GetModPlayer<WeaponPlayer>().starTarget - player.GetModPlayer<WeaponPlayer>().starPosition1;
				diff.Normalize();
				Projectile.velocity = diff;
				Projectile.direction = player.GetModPlayer<WeaponPlayer>().starTarget.X > player.GetModPlayer<WeaponPlayer>().starPosition1.X ? 1 : -1;
				Projectile.netUpdate = true;
			}
			int dir = Projectile.direction;
			player.ChangeDir(dir); // Set player direction to where we are shooting
								   //player.heldProj = projectile.whoAmI; // Update player's held projectile]
			
			
			player.statMana--;
			player.itemTime = 2; // Set item time to 2 frames while we are used
			player.itemAnimation = 2; // Set item animation time to 2 frames while we are used
			player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir); // Set the item rotation to where we are shooting
		}

		private void CastLights()
		{
			// Cast a light along the line of the laser
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;

		/*
		 * Update CutTiles so the laser will cut tiles (like grass)
		 */
		public override void CutTiles() {
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
		}
	}
}
