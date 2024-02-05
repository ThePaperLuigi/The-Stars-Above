using StarsAbove.Systems;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Summon.CandiedSugarball
{
    public class Sugartle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			//DrawOffsetX = -20;
			// DisplayName.SetDefault("Luka"); // Automatic from .lang files
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

            if (modPlayer.sugarballMinionType != 1)
            {

                Projectile.Kill();
            }

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