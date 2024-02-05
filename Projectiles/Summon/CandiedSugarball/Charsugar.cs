using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Summon.CandiedSugarball
{
    public class Charsugar : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 10;
			
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.MiniMinotaur);
			AIType = ProjectileID.MiniMinotaur;
			DrawOriginOffsetY = -8;
			//AnimationType = ProjectileID.BabyDino;
			Projectile.light = 1f;
			DrawOffsetX = -20;
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
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			
			if(modPlayer.sugarballMinionType != 0)
			{
                
                Projectile.Kill();
			}
			Vector2 position = Vector2.Zero;
			if(Projectile.direction == 1)
			{
				position = new Vector2(Projectile.Center.X - 28, Projectile.Center.Y);
			}
			else
			{
                position = new Vector2(Projectile.Center.X + 10, Projectile.Center.Y);

            }
            Dust.NewDust(position, 0, 0, DustID.Flare, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default, 0.7f);

            if (player.dead)
			{
				modPlayer.sugarballMinions = false;
			}
			if (modPlayer.sugarballMinions)
			{
				Projectile.timeLeft = 2;
			}
		}
        public override void OnKill(int timeLeft)
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = Projectile.Center },
                Projectile.owner);
        }
       
    }
	
	
}