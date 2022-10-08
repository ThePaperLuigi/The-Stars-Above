using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs.Ozma;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ozma
{
    public class OzmaAttack4 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ozma Ascendant");     //The English name of the projectile
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			// This method right here is the backbone of what we're doing here; by using this method, we copy all of
			// the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
			// so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
			// if you're going to copy the stats of a projectile, use CloneDefaults().

			Projectile.CloneDefaults(ProjectileID.EmpressBlade);

			// To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
			// the projectile to essentially behave the same way as the vanilla projectile.
			AIType = ProjectileID.EmpressBlade;

			// Replacing the blade's Summon-type attributes
			Projectile.minion = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.minionSlots = 0f;
			Projectile.timeLeft = 80;
			Projectile.penetrate = -1;
			Projectile.hide = false;
			Projectile.alpha = 0;
			Projectile.width = 160;
			Projectile.height = 160;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
			Projectile.friendly = true;
		}
		bool onSpawn = true;
		bool finishedAttacking = false;

        public override bool PreAI()
        {
			Player projOwner = Main.player[Projectile.owner];
			projOwner.empressBlade = false;//Holdout from CloneDefaults
			return base.PreAI();
        }

        public override void AI()
		{
			Player projOwner = Main.player[Projectile.owner];
			

			if (onSpawn)
			{
				
				

				onSpawn = false;
			}

			if (projOwner.HasBuff(BuffType<AnnihilationState>()))
			{
				Projectile.frame = 1;

			}
			else
			{
				Projectile.frame = 0;
			}

			
			if (Projectile.alpha < 50)//A little transparent
			{
				Projectile.alpha = 50;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}



			base.AI();
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, 219, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.9f);

			}
			Player projOwner = Main.player[Projectile.owner];
			if (crit)
			{
				projOwner.AddBuff(BuffType<AnnihilationState>(), 180);
			}
			base.OnHitNPC(target, damage, knockback, crit);
		}
		public override void Kill(int timeLeft)
		{
			
		}
	}
}
