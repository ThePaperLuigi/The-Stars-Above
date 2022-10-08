
using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Ozma;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ozma
{
    public class OzmaDamageFinale : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ozma Ascendant");
			
		}

		public override void SetDefaults() {
			Projectile.width = 400;
			Projectile.height = 400;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;

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
			
			// Fade in
			Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, 219, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.9f);

			}
			target.AddBuff(BuffType<Stun>(), 60);
			Player projOwner = Main.player[Projectile.owner];
			if (crit)
			{
				projOwner.AddBuff(BuffType<AnnihilationState>(), 180);
			}
			base.OnHitNPC(target, damage, knockback, crit);
		}
	}
}
