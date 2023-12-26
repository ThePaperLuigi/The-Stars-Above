using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Thespian
{
    public class ThespianPotion : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Apostate");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 30;               //The width of projectile hitbox
			Projectile.height = 30;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.YellowTrail).Draw(Projectile);

			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			
			
			return false;
		}
		int type;
		bool firstSpawn = true;
		public override void AI()
		{
			Projectile.timeLeft = 10;
			Projectile.velocity *= 0.98f;
			if(firstSpawn)
			{
				Projectile.alpha = 255;
				Projectile.rotation = MathHelper.ToRadians(Main.rand.NextFloat(0, 360));
				type = Main.rand.Next(0, 3);
				firstSpawn = false;
			}

            if (Projectile.ai[1] > 0)
            {
				
            }
			else
            {
                Projectile.alpha -= 10;

                Lighting.AddLight(Projectile.Center, TorchID.Purple);

                switch (type)
                {
                    case 0:
                        Projectile.rotation += MathHelper.ToRadians(4f);
                        break;
                    case 1:
                        Projectile.rotation += MathHelper.ToRadians(2f);
                        break;
                    case 2:
                        Projectile.rotation -= MathHelper.ToRadians(3f);
                        break;
                }
                if (Projectile.ai[0] == 0)
				{
					SoundEngine.PlaySound(SoundID.Shatter, Projectile.Center);
                    SoundEngine.PlaySound(SoundID.DD2_CrystalCartImpact, Projectile.Center);

                    int type = ProjectileType<ThespianPotionExplosion>();
                    int damage = 20;

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y), Vector2.Zero, type, damage, 0f, Main.myPlayer, 0f, 0f);
                    
					int type2 = ProjectileType<ThespianLingeringDamageField>();

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y), Vector2.Zero, type2, damage/3, 0f, Main.myPlayer, 0f, 0f);
					Projectile.Kill();
                }
                Projectile.ai[0]--;
			}
			Projectile.ai[1]--;
		}

		public override void OnKill(int timeLeft)
		{
			

		}
	}
}
