
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.DreamersInkwell;
using StarsAbove.Buffs;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.DreamersInkwell
{
    public class InkwellWaterInk : ModProjectile
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
			Projectile.idStaticNPCHitCooldown = 40;
		}

		public override void AI() {
			Player projOwner = Main.player[Projectile.owner];

			if(!projOwner.GetModPlayer<WeaponPlayer>().InkwellHeld)
            {
				//Projectile.Kill();
            }
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player p = Main.player[i];
				if (p.active && !p.dead && p.Distance(Projectile.Center) < 20f)
				{
					if(!p.HasBuff(BuffType<InkwellWaterDelay>()))
                    {
						p.AddBuff(BuffType<InkwellWaterDelay>(), 60 * 8);
						p.Heal((int)(p.statLifeMax2 * 0.02));

					}
					p.AddBuff(BuffID.Ironskin, 60 * 8);

					
				}

			}


			if (Main.rand.NextBool(5))
			{
				
				Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemSapphire, Vector2.Zero, 0, default, 2f);
				dust.noGravity = true;
				dust.velocity *= 0.9f;
			}
			if (Main.rand.NextBool(15))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Water,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			

            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }
        public override void Kill(int timeLeft)
		{
			
		}
	}
}
