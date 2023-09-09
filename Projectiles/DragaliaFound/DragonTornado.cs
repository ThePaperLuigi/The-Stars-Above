using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent;

namespace StarsAbove.Projectiles.DragaliaFound
{
    public class DragonTornado : ModProjectile
	{
		public override void SetStaticDefaults() {

		}

		public override void SetDefaults() {
			Projectile.width = 40;               //The width of projectile hitbox
			Projectile.height = 40;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete Projectile if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.DamageType = DamageClass.SummonMeleeSpeed;

		}
		public override void AI()
        {
			
			float num27 = 900f;
			if (Projectile.type == 657)
			{
				num27 = 300f;
			}
			if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = -1;
				SoundEngine.PlaySound(SoundID.Item82, Projectile.Center);
			}
			Projectile.ai[0]++;
			if (Projectile.ai[0] >= num27)
			{
				Projectile.Kill();
			}
			if (Projectile.type == 656 && Projectile.localAI[0] >= 30f)
			{
				Projectile.damage = 0;
				if (Projectile.ai[0] < num27 - 120f)
				{
					float num28 = Projectile.ai[0] % 60f;
					Projectile.ai[0] = num27 - 120f + num28;
					Projectile.netUpdate = true;
				}
			}
			float num29 = 15f;
			float num30 = 15f;
			Point point5 = Projectile.Center.ToTileCoordinates();
			Collision.ExpandVertically(point5.X, point5.Y, out var topY, out var bottomY, (int)num29, (int)num30);
			topY++;
			bottomY--;
			Vector2 value66 = new Vector2(point5.X, topY) * 16f + new Vector2(8f);
			Vector2 value68 = new Vector2(point5.X, bottomY) * 16f + new Vector2(8f);
			Vector2 vector8 = Vector2.Lerp(value66, value68, 0.5f);
			Vector2 value69 = new Vector2(0f, value68.Y - value66.Y);
			value69.X = value69.Y * 0.2f;
			Projectile.width = (int)(value69.X * 0.65f);
			Projectile.height = (int)value69.Y;
			Projectile.Center = vector8;
			if (Projectile.type == 656 && Projectile.owner == Main.myPlayer)
			{
				bool flag59 = false;
				Vector2 center6 = Main.player[Projectile.owner].Center;
				Vector2 top = Main.player[Projectile.owner].Top;
				for (float num31 = 0f; num31 < 1f; num31 += 0.05f)
				{
					Vector2 position2 = Vector2.Lerp(value66, value68, num31);
					if (Collision.CanHitLine(position2, 0, 0, center6, 0, 0) || Collision.CanHitLine(position2, 0, 0, top, 0, 0))
					{
						flag59 = true;
						break;
					}
				}
				if (!flag59 && Projectile.ai[0] < num27 - 120f)
				{
					float num32 = Projectile.ai[0] % 60f;
					Projectile.ai[0] = num27 - 120f + num32;
					Projectile.netUpdate = true;
				}
			}
			if (!(Projectile.ai[0] < num27 - 120f))
			{
				return;
			}
			for (int num33 = 0; num33 < 1; num33++)
			{
				float value70 = -0.5f;
				float value71 = 0.9f;
				float amount3 = Main.rand.NextFloat();
				Vector2 value72 = new Vector2(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()), MathHelper.Lerp(value70, value71, amount3));
				value72.X *= MathHelper.Lerp(2.2f, 0.6f, amount3);
				value72.X *= -1f;
				Vector2 value73 = new Vector2(6f, 10f);
				Vector2 position3 = vector8 + value69 * value72 * 0.5f + value73;
				Dust dust185 = Main.dust[Dust.NewDust(position3, 0, 0, DustID.Sandnado)];
				dust185.color = Color.Green;
				dust185.position = position3;
				dust185.customData = vector8 + value73;
				dust185.fadeIn = 1f;
				dust185.scale = 0.3f;
				if (value72.X > -1.2f)
				{
					dust185.velocity.X = 1f + Main.rand.NextFloat();
				}
				dust185.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
			}
			
			
			Projectile.AI();

        }
		public NPC FindClosestNPC(float maxDetectDistance)
		{
			NPC closestNPC = null;

			// Using squared values in distance checks will let us skip square root calculations, drastically improving Projectile method's speed.
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			// Loop through all NPCs(max always 200)
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target = Main.npc[k];
				// Check if NPC able to be targeted. It means that NPC is
				// 1. active (alive)
				// 2. chaseable (e.g. not a cultist archer)
				// 3. max life bigger than 5 (e.g. not a critter)
				// 4. can take damage (e.g. moonlord core after all it's parts are downed)
				// 5. hostile (!friendly)
				// 6. not immortal (e.g. not a target dummy)
				if (target.CanBeChasedBy())
				{
					// The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

					// Check if it is within the radius
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}

			return closestNPC;
		}

		

		public override bool PreDraw(ref Color lightColor) {
			float num274 = 1400f;
			float num275 = 2f;//upper height
			float num276 = 32f;// number of tendrils?
			float num277 = Projectile.ai[0];
			float num278 = MathHelper.Clamp(num277 / 30f, 0f, 1f);
			if (num277 > num274 - 60f)
			{
				num278 = MathHelper.Lerp(1f, 0f, (num277 - (num274 - 60f)) / 60f);
			}
			Point point5 = Projectile.Center.ToTileCoordinates();
			Collision.ExpandVertically(point5.X, point5.Y, out var topY, out var bottomY, (int)num275, (int)num276);
			topY++;
			bottomY--;
			float num279 = 0.2f;
			Vector2 value103 = new Vector2(point5.X, topY) * 16f + new Vector2(8f);
			Vector2 value104 = new Vector2(point5.X, bottomY) * 16f + new Vector2(8f);
			Vector2.Lerp(value103, value104, 0.5f);
			Vector2 vector43 = new Vector2(0f, value104.Y - value103.Y);
			vector43.X = vector43.Y * num279;
			new Vector2(value103.X - vector43.X / 2f, value103.Y);
			Texture2D value105 = (Texture2D)TextureAssets.Projectile[Projectile.type];
			Rectangle rectangle18 = value105.Frame();
			Vector2 origin18 = rectangle18.Size() / 2f;
			float num280 = -(float)Math.PI / 150f * num277;
			Vector2 spinningpoint6 = Vector2.UnitY.RotatedBy(num277 * 0.1f);
			float num281 = 0f;
			float num282 = 5.1f;
			Color value106 = new Color(125, 212, 100);
			for (float num283 = (int)value104.Y; num283 > (float)(int)value103.Y; num283 -= num282)
			{
				num281 += num282;
				float num284 = num281 / vector43.Y;
				float num285 = num281 * ((float)Math.PI * 2f) / -20f;
				float num286 = num284 - 0.15f;
				Vector2 position18 = spinningpoint6.RotatedBy(num285);
				Vector2 value107 = new Vector2(0f, num284 + 1f);
				value107.X = value107.Y * num279;
				Color color67 = Color.Lerp(Color.Transparent, value106, num284 * 2f);
				if (num284 > 0.5f)
				{
					color67 = Color.Lerp(Color.Transparent, value106, 2f - num284 * 2f);
				}
				color67.A = (byte)((float)(int)color67.A * 0.5f);
				color67 *= num278;
				position18 *= value107 * 100f;
				position18.Y = 0f;
				position18.X = 0f;
				position18 += new Vector2(value104.X, num283) - Main.screenPosition;
				Main.EntitySpriteDraw(value105, position18, rectangle18, color67, num280 + num285, origin18, 1f + num286, SpriteEffects.None, 0);
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			//if (Projectile.owner == Main.myPlayer)
			//{
				//Projectile.NewProjectile(Projectile.GetSource_FromProjectile(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<NovaBombExplosion>(), Projectile.damage, 0, Main.player[Projectile.owner].whoAmI);
			//}
		}
	}
}
