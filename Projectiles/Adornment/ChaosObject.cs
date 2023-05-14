using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Adornment;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Adornment
{
    public class ChaosObject : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Adornment of the Chaotic God");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
			Main.projFrames[Projectile.type] = 4;

		}

		public override void SetDefaults() {
			Projectile.width = 22;               //The width of projectile hitbox
			Projectile.height = 22;              //The height of projectile hitbox
			Projectile.aiStyle = -1;//ProjAIStyleID.Arrow;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 3;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete Projectile if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.DamageType = DamageClass.Summon;
			AIType = ProjectileID.WoodenArrowFriendly;
			Projectile.tileCollide = true;          //Can the projectile collide with tiles?
		

		}

		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			// If collide with tile, reduce the penetrate.
			// So the projectile can reflect at most 5 times
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			else
			{
				for (int d = 0; d < 8; d++)
				{
					Dust.NewDust(Projectile.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.5f);
					Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.4f);

				}
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

				// If the projectile hits the left or right side of the tile, reverse the X velocity
				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}

				// If the projectile hits the top or bottom side of the tile, reverse the Y velocity
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}

			return false;
		}
		bool firstSpawn = true;
        public override bool PreAI()
        {
			if (firstSpawn)
			{
				Projectile.frame = Main.rand.Next(0, 5);
				firstSpawn = false;
			}
			Projectile.rotation += 0.1f;
            return base.PreAI();
        }
        public override void AI()
        {
			
			Projectile.alpha -= 10;

			Projectile.velocity.Y += 0.4f;
			/*
			Dust.NewDust(Projectile.Center, 0, 0, 110, 0f , 0f + Main.rand.Next(-1, 1), 150, default(Color), 1f);
			
			for (int d = 0; d < 20; d++)
			{
				
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.Center;
				Dust dust1 = Dust.NewDustPerfect(position, 220, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
				dust1.noGravity = true;
			}*/

			//Projectile.AI();
        }
        public override void Kill(int timeLeft)
        {
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.5f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.4f);

			}
			base.Kill(timeLeft);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Green, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.7f);
				Dust.NewDust(target.Center, 0, 0, DustID.GreenFairy, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.3f);

			}
			
			

        }
        public override bool PreDraw(ref Color lightColor)
		{
			if(Projectile.alpha < 100)
            {
				//default(Effects.WhiteTrail).Draw(Projectile);
			}
			

			return true;
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {

        }
        
	}
}
