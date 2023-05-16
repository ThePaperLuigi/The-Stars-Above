
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.SaltwaterScourge
{
    public class PowderKegExplosion : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Saltwater Scourge");
			
		}

		public override void SetDefaults() {
			Projectile.width = 450;
			Projectile.height = 450;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {
			//Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));

			Projectile.ai[0] += 1f;
			for (int d = 0; d < 15; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 7, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 16; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 269, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 17; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 78, 0f + Main.rand.Next(-14, 14), 0f + Main.rand.Next(-14, 14), 150, default(Color), 1.5f);
			}
			// Smoke Dust spawn
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Fire Dust spawn
			for (int i = 0; i < 40; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			// Fade in
			Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
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

			base.OnHitNPC(target, damage, knockback, crit);
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {


           
        }
    }
}
