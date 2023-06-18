using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Ozma;

namespace StarsAbove.Projectiles.Ozma
{
    public class OzmaAttack1 : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ozma Ascendant");     //The English name of the projectile
			Main.projFrames[Projectile.type] = 2;

		}

		public override void SetDefaults() {
			Projectile.width = 160;               //The width of projectile hitbox
			Projectile.height = 160;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Magic;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
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
			Player projOwner = Main.player[Projectile.owner];

			float rotationsPerSecond = 3f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			
			if(Projectile.alpha < 0)
            {
				Projectile.alpha = 0;
            }
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}
			if (Projectile.timeLeft < 10)
            {
				Projectile.scale += 0.1f;
				Projectile.alpha += 20;
            }
			if (projOwner.HasBuff(BuffType<AnnihilationState>()))
			{
				Projectile.frame = 1;

			}
			else
			{
				Projectile.frame = 0;
			}
			base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, 219, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.9f);

			}
			Player projOwner = Main.player[Projectile.owner];
			if(hit.Crit)
            {
				projOwner.AddBuff(BuffType<AnnihilationState>(), 180);
            }				
			 
        }
       

		

		public override void Kill(int timeLeft)
		{
			Player projOwner = Main.player[Projectile.owner];
			if (projOwner.HasBuff(BuffType<AnnihilationState>()))
            {
				for (int i = 0; i < 22; i++)
				{
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 1.1f);
					//Main.dust[dustIndex].velocity *= 0.6f;
				}
				for (int i = 0; i < 22; i++)
				{
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.PlatinumCoin, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 1.1f);
					//Main.dust[dustIndex].velocity *= 0.6f;
				}
			}
			else
            {
				for (int i = 0; i < 42; i++)
				{
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.PlatinumCoin, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 1.1f);
					//Main.dust[dustIndex].velocity *= 0.6f;
				}
			}
			


		}
	}
}
