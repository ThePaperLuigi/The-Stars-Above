
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.DreamersInkwell;

namespace StarsAbove.Projectiles.DreamersInkwell
{
    public class InkwellFireInk : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ice Lotus");
			
		}

		public override void SetDefaults() {
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 18000;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.friendly = true;

			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 60;
		}

		public override void AI() {
			Player projOwner = Main.player[Projectile.owner];

			if(!projOwner.GetModPlayer<WeaponPlayer>().InkwellHeld)
            {
				//Projectile.Kill();
            }
			


			if (Main.rand.NextBool(5))
			{
				
				Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemRuby, Vector2.Zero, 0, default, 2f);
				dust.noGravity = true;
				dust.velocity *= 0.9f;
			}
			if (Main.rand.NextBool(15))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.5f);
				dust.velocity += -Projectile.velocity * 0.3f;
				dust.velocity.Y -= 0.8f;
			}
			if (Main.rand.NextBool(1000))
			{
				if (Main.myPlayer == Projectile.owner)
				{
					Projectile.NewProjectile(null, Projectile.Center, Vector2.Zero, ProjectileID.MolotovFire, Projectile.damage, 0, projOwner.whoAmI);
				}
			}
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

			base.ModifyHitNPC(target, ref modifiers);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.OnFire, 60 * 8);

		}
		public override void Kill(int timeLeft)
		{
			
		}
	}
}
