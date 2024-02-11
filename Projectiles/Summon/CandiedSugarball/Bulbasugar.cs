using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Summon.CandiedSugarball
{
    public class Bulbasugar : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
            Projectile.width = 36;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 280;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
			Projectile.tileCollide = true;
        }

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			return true;
		}
		bool invisible;
		public override void AI()
		{
			DrawOriginOffsetY = 2;
			Projectile.velocity.Y += 0.26f;
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			
			if(modPlayer.sugarballMinionType != 2)
			{
                
                Projectile.Kill();
			}
			Vector2 position = Vector2.Zero;
			
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