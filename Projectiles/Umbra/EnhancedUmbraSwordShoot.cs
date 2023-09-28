using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Umbra
{
    public class EnhancedUmbraSwordShoot : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Umbra");     //The English name of the projectile
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 140;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 170;               //The width of projectile hitbox
			Projectile.height = 170;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 5;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 125;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.DamageType = DamageClass.Melee;
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.WhiteTrail).Draw(Projectile);

			return true;
		}
		bool firstSpawn = true;
		public override void AI()
        {

			if(firstSpawn)
            {
				Projectile.timeLeft = 60;
				Projectile.scale = 0.8f;
				firstSpawn = false;
            }				

			if(Projectile.timeLeft < 30)
            {
				Projectile.scale *= 0.985f;
				Projectile.velocity *= 0.92f;

			}
			else
            {
				Projectile.scale *= 0.99f;
				Projectile.velocity *= 1.02f;

			}

			base.AI();

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Player player = Main.player[Projectile.owner];


			for (int d = 0; d < 23; d++)//Visual effects
			{
				Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
				float scale = 1f + (Main.rand.NextFloat() * 0.6f);
				perturbedSpeed = perturbedSpeed * scale;
				int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.6f);
				Main.dust[dustIndex].noGravity = true;

			}

			Vector2 direction = Vector2.Normalize(target.Center - Projectile.Center);
			Vector2 velocity = direction * 26f;


			if (hit.Crit && Projectile.penetrate > 0)
            {
				for (int i = 0; i < Projectile.penetrate; i++)
				{

					Vector2 position = target.Center + new Vector2(((float)Main.rand.Next(-100, 101)), Main.rand.Next(-100, 101));
					position.Y -= (100 * i);
					Vector2 heading = target.Center - position;
					heading.Normalize();
					heading *= new Vector2(velocity.X, velocity.Y).Length();
					velocity.X = heading.X;
					velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
					if(player.whoAmI == Main.myPlayer)
                    {
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<UmbraSwordShoot>(), damageDone, 0, player.whoAmI, 0f);

					}
				}
			}

			 
        }
		

		public override void OnKill(int timeLeft)
		{
			for (int d = 0; d < 18; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 150, default(Color), 0.5f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 150, default(Color), 0.4f);

			}
			if(Projectile.penetrate != 0)
            {
				for (int d = 0; d < 23; d++)//Visual effects
				{
					Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
					float scale = 3f + (Main.rand.NextFloat() * 0.6f);
					perturbedSpeed = perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.6f);
					Main.dust[dustIndex].noGravity = true;

				}
				for (int d = 0; d < 23; d++)//Visual effects
				{
					Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
					float scale = 3f + (Main.rand.NextFloat() * 0.6f);
					perturbedSpeed = -perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.6f);
					Main.dust[dustIndex].noGravity = true;

				}
			}
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
		}
	}
}
