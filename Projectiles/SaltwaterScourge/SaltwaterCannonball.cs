
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.TagDamage;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.SaltwaterScourge
{
    public class SaltwaterCannonball : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Saltwater Scourge");
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			// This method right here is the backbone of what we're doing here; by using Projectile method, we copy all of
			// the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
			// so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
			// if you're going to copy the stats of a projectile, use CloneDefaults().

			Projectile.CloneDefaults(ProjectileID.CannonballFriendly);
			Projectile.width = 18;
			Projectile.height = 18;
			// To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
			// the projectile to essentially behave the same way as the vanilla projectile.
			AIType = ProjectileID.CannonballFriendly;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			Projectile.DamageType = DamageClass.Ranged;

		}
		public override bool PreDraw(ref Color lightColor)
		{
			//default(Effects.YellowTrail).Draw(Projectile);

			return true;
		}
		public override void AI()
		{
			
		}

        // While there are several different ways to change how our projectile could behave differently, lets make it so
        // when our projectile finally dies, it will explode into 4 regular Meowmere projectiles.
        
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			//target.AddBuff(BuffType<KingTagDamage>(), 240);
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, 219, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.4f);

			}

			if (!target.active)
			{
				int k = Item.NewItem(target.GetSource_DropAsItem(), (int)target.position.X, (int)target.position.Y, target.width, target.height, ItemID.SilverCoin, 15, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, null, k, 1f);
				}
			}

			 
		}
		public override void OnKill(int timeLeft)
		{
			for (int d = 0; d < 18; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Smoke, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.7f);

			}
			// Large Smoke Gore spawn
			for (int g = 0; g < 4; g++)
			{
				int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - Main.rand.NextFloat(-5, 5), Projectile.position.Y + (float)(Projectile.height / 2) - Main.rand.NextFloat(-5, 5)), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1f;
				//Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				//Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - Main.rand.NextFloat(-5, 5), Projectile.position.Y + (float)(Projectile.height / 2) - Main.rand.NextFloat(-5, 5)), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1f;
				//Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				//Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - Main.rand.NextFloat(-5, 5), Projectile.position.Y + (float)(Projectile.height / 2) - Main.rand.NextFloat(-5, 5)), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1f;
				//Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				//Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - Main.rand.NextFloat(-5, 5), Projectile.position.Y + (float)(Projectile.height / 2) - Main.rand.NextFloat(-5, 5)), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1f;
				//Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				//Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}

			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}

	}
}
