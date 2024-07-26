using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
    public class OakCakeRollPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			//DrawOffsetX = -20;
			// DisplayName.SetDefault("NecoArc"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 10;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.MiniMinotaur);
			AIType = ProjectileID.MiniMinotaur;
			//AnimationType = ProjectileID.BabyDino;
			Projectile.light = 1f;
			DrawOriginOffsetY = -8;
		}
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity.Y != 0)
            {
                default(Effects.GreenTrail).Draw(Projectile);
            }


            return true;
        }
        public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.miniMinotaur = false; // Relic from AIType
			return true;
		}
		bool invisible;
		public override void AI()
		{
            DrawOffsetX = -10;
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (Projectile.frame > 6)
			{		
				if (!invisible)
                {
                    Vector2 position = Projectile.Center;
                    float dustAmount = 40f;
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 1f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                        int dust = Dust.NewDust(position, 0, 0, DustID.Flare);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = position + spinningpoint5;
                        Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
                    }
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 1f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + MathHelper.ToRadians(90));
                        int dust = Dust.NewDust(position, 0, 0, DustID.Flare);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = position + spinningpoint5;
                        Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
                    }
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                        int dust = Dust.NewDust(position, 0, 0, DustID.Flare);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = position + spinningpoint5;
                        Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
                    }
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                        int dust = Dust.NewDust(position, 0, 0, DustID.Flare);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = position + spinningpoint5;
                        Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 4f;
                    }
                    invisible = true;
                }
				

				
			}
			else
			{
                if(Projectile.frame < 6)
                {
                    if(invisible)
                    {
                        Vector2 position = Projectile.Center;
                        float dustAmount = 40f;
                        for (int i = 0; i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 1f);
                            spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                            int dust = Dust.NewDust(position, 0, 0, DustID.Flare);
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = position + spinningpoint5;
                            Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
                        }
                        for (int i = 0; i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 1f);
                            spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + MathHelper.ToRadians(90));
                            int dust = Dust.NewDust(position, 0, 0, DustID.Flare);
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = position + spinningpoint5;
                            Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
                        }
                        for (int i = 0; i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                            spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                            int dust = Dust.NewDust(position, 0, 0, DustID.Flare);
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = position + spinningpoint5;
                            Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
                        }
                        for (int i = 0; i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                            spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                            int dust = Dust.NewDust(position, 0, 0, DustID.Flare);
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = position + spinningpoint5;
                            Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 4f;
                        }
                        invisible = false;

                    }
                }
				
				
			}
			
			if (player.dead)
			{
				modPlayer.FireflyPet = false;
			}
			if (modPlayer.FireflyPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}