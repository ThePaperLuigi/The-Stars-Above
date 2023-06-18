
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.IrminsulDream;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.IrminsulDream
{
    public class VerdantSnapshot : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Irminsul's Dream");
			
		}

		public override void SetDefaults() {
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;

		}

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
			

			base.ModifyDamageHitbox(ref hitbox);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			int randomPetals = Main.rand.Next(3, 5);
			for (int i = 0; i < randomPetals; i++)
			{
				// Random upward vector.
				Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-1, -4));
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, vel, ProjectileType<IrminsulLeaf>(), 0, 0, Projectile.owner, 0, 1);
			}
			target.AddBuff(BuffType<VerdantEmbrace>(), 720);

			 
        }
        
        public override void AI() {
			//Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));

			
			for (int d = 0; d < 10; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.GreenMoss, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.7f);
			}
			for (int d = 0; d < 24; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Firework_Green, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.5f);
			}

			for (int d = 0; d < 10; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 1.0f);
			}
			for (int d = 0; d < 10; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.Firework_Green, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.6f);
			}
			
			// Fade in
			Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}
	}
}
