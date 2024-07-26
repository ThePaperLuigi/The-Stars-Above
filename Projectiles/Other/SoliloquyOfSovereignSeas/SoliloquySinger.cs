using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Other.SoliloquyOfSovereignSeas;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Other.SoliloquyOfSovereignSeas
{
    public class SoliloquySinger : ModProjectile
	{
		public override void SetStaticDefaults()
		{

			// DisplayName.SetDefault("Bloop"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
			
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.DD2PetGhost);
			AIType = ProjectileID.DD2PetGhost;
			Projectile.light = 1f;
            Projectile.minionSlots = 1f;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.petFlagDD2Ghost = false; // Relic from AIType
			return true;
		}

		public override void AI()
		{
            Projectile.alpha = 100;
            DrawOffsetX = -75;
            DrawOriginOffsetY = -75;
            Player player = Main.player[Projectile.owner];
            player.AddBuff(BuffID.WaterWalking, 10);
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
            if (modPlayer.ousiaAligned)
            {
                for (int d = 0; d < 20; d++)
                {
                    Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
                }
                Projectile.Kill();
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 120)
            {
                Projectile.ai[0] = 0;
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && !p.dead && p.Distance(Projectile.Center) < 490f)
                    {
                        if(p.statLife < 200)
                        {
                            p.Heal((int)(Projectile.damage * 0.1f));

                        }
                        else
                        {
                            p.Heal((int)(Projectile.damage * 0.05f));

                        }
                    }

                }
                float dustAmount = 120f;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity + spinningpoint5.SafeNormalize(Vector2.UnitY) * 40f;
                }
            }

            for (int i = 0; i < 30; i++)
            {//Circle
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * 494);
                offset.Y += (float)(Math.Cos(angle) * 494);

                Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 20, Vector2.Zero, 200, default, 0.7f);
                d.fadeIn = 0.1f;
                d.noGravity = true;
            }
            if (player.dead || !player.active)
            {
                player.ClearBuff(BuffType<SoliloquyMinionBuff>());

            }
            if (player.HasBuff(BuffType<SoliloquyMinionBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            if (player.dead)
            {
                modPlayer.SoliloquyMinions = false;
            }
            if (modPlayer.SoliloquyMinions)
            {
                Projectile.timeLeft = 2;
            }
        }
	}
	
}