using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.VermillionDaemon;

namespace StarsAbove.Projectiles.VermillionDaemon
{
    public class WarpStrike : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Vermillion Daemon");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 65;               //The width of projectile hitbox
			Projectile.height = 65;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Magic;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 26;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		bool firstSpawn = true;

		public override void AI()
        {
			if (firstSpawn)
			{
				Projectile.scale = 2f;
				firstSpawn = false;
			}
			float rotationsPerSecond = 5f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);

			if (Projectile.timeLeft > 24)
			{
				Projectile.tileCollide = false;
			}
			else
            {
				Projectile.tileCollide = true;
			}

			base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Player player = Main.player[Projectile.owner];
			player.AddBuff(BuffType<Invincibility>(), 20);
			if (target.HasBuff(BuffType<CrimsonMark>()))
			{

				player.GetModPlayer<WeaponPlayer>().SpectralArsenal++;
				target.DelBuff(target.FindBuffIndex(BuffType<CrimsonMark>()));
			}
			for (int i = 0; i < 100; i++)
			{
				Vector2 position = Vector2.Lerp(player.Center, Projectile.Center, (float)i / 100);
				Dust d = Dust.NewDustPerfect(position, 223, null, 240, default(Color), 0.9f);
				d.fadeIn = 0.3f;
				d.noLight = true;
				d.noGravity = true;

			}
			player.Teleport(new Vector2(Projectile.Center.X, Projectile.Center.Y - 10), 1, 0);
			
			NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, Projectile.Center.X, Projectile.Center.Y - 10, 1, 0, 0);
			Projectile.Kill();


			 
        }
        public override bool OnTileCollide(Vector2 oldVelocity) {
			
			
			Player player = Main.player[Projectile.owner];
			for (int i = 0; i < 100; i++)
			{
				Vector2 position = Vector2.Lerp(player.Center, Projectile.Center, (float)i / 100);
				Dust d = Dust.NewDustPerfect(position, 223, null, 240, default(Color), 0.9f);
				d.fadeIn = 0.3f;
				d.noLight = true;
				d.noGravity = true;

			}
			player.Teleport(new Vector2(Projectile.Center.X, Projectile.Center.Y - 10), 1, 0);
			NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, Projectile.Center.X, Projectile.Center.Y - 10, 1, 0, 0);
			Projectile.Kill();
			

			return true;
		}

		

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 22; i++)
			{
				int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 223, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 1.1f);
				//Main.dust[dustIndex].velocity *= 0.6f;
			}


		}
	}
}
