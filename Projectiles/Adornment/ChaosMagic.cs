using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Adornment;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Adornment
{
    public class ChaosMagic : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Adornment of the Chaotic God");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 42;               //The width of projectile hitbox
			Projectile.height = 42;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 5;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete Projectile if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.DamageType = DamageClass.Summon;
			AIType = ProjectileID.ZapinatorLaser;
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
		

		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			
			return false;
		}
        public override void AI()
        {
			Projectile.scale = 0.4f;
			Projectile.alpha -= 10;
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Green, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.7f);
				Dust.NewDust(target.Center, 0, 0, DustID.GreenFairy, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.3f);

			}
			//Transplanted from vanilla Zapinator code.
			Vector2 position = Projectile.position;
			Projectile.alpha = 255;
			if (Main.rand.NextBool(20))
			{
				Projectile.tileCollide = false;
				Projectile.position.X += Main.rand.Next(-256, 257);
			}
			if (Main.rand.NextBool(20))
			{
				Projectile.tileCollide = false;
				Projectile.position.Y += Main.rand.Next(-256, 257);
			}
			if (Main.rand.NextBool(2))
			{
				Projectile.tileCollide = false;
			}
			if (!Main.rand.NextBool(3))
			{
				position = Projectile.position;
				Projectile.position -= Projectile.velocity * Main.rand.Next(0, 40);
				if (Projectile.tileCollide && Collision.SolidTiles(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.position = position;
					Projectile.position -= Projectile.velocity * Main.rand.Next(0, 40);
					if (Projectile.tileCollide && Collision.SolidTiles(Projectile.position, Projectile.width, Projectile.height))
					{
						Projectile.position = position;
					}
				}
			}
			Projectile.velocity *= 0.6f;
			if (Main.rand.NextBool(7))
			{
				Projectile.velocity.X += (float)Main.rand.Next(30, 31) * 0.01f;
			}
			if (Main.rand.NextBool(7))
			{
				Projectile.velocity.Y += (float)Main.rand.Next(30, 31) * 0.01f;
			}
			Projectile.damage = (int)((double)Projectile.damage * 0.9);
			Projectile.knockBack *= 0.9f;
			if (Main.rand.NextBool(20))
			{
				Projectile.knockBack *= 10f;
			}
			if (Main.rand.NextBool(50))
			{
				Projectile.damage *= 10;
			}
			if (Main.rand.NextBool(7))
			{
				position = Projectile.position;
				Projectile.position.X += Main.rand.Next(-64, 65);
				if (Projectile.tileCollide && Collision.SolidTiles(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.position = position;
				}
			}
			if (Main.rand.NextBool(7))
			{
				position = Projectile.position;
				Projectile.position.Y += Main.rand.Next(-64, 65);
				if (Projectile.tileCollide && Collision.SolidTiles(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.position = position;
				}
			}
			if (Main.rand.NextBool(14))
			{
				Projectile.velocity.X *= -1f;
			}
			if (Main.rand.NextBool(14))
			{
				Projectile.velocity.Y *= -1f;
			}
			if (Main.rand.NextBool(10))
			{
				Projectile.velocity *= (float)Main.rand.Next(1, 201) * 0.0005f;
			}
			if (Projectile.tileCollide)
			{
				Projectile.ai[1] = 0f;
			}
			else
			{
				Projectile.ai[1] = 1f;
			}
			Projectile.netUpdate = true;

        }
        public override bool PreDraw(ref Color lightColor)
		{
			if(Projectile.alpha < 100)
            {
				default(Effects.GreenTrail).Draw(Projectile);
			}
			

			return true;
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			Player player = Main.player[Projectile.owner];
			//target.AddBuff(BuffID.OnFire, 240);
			if (player.HasBuff(BuffType<AdornmentCritBuff>()))
            {
				modifiers.SetCrit();
            }
        }
        
	}
}
