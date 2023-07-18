
using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.GossamerNeedle
{
    public class GossamerAOEAttack : ModProjectile
	{
		public override void SetStaticDefaults() {
			//DrawOriginOffsetY = -150;
			//DrawOffsetX = 20;
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults() {
			Projectile.width = 225;
			Projectile.height = 225;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 500;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			//Projectile.DamageType = DamageClass.Melee;
			Projectile.alpha = 0;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 5;
		}

		bool firstSpawn = true;
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
			hitbox.Width += 60;
			hitbox.Height += 60;
            base.ModifyDamageHitbox(ref hitbox);
        }
        public override void AI()
		{
			Projectile.ai[0] += 1f;
			Player projOwner = Main.player[Projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);

			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);

			Projectile.scale = 1.5f;
			if (firstSpawn)
            {
				float rotationValue = MathHelper.ToRadians(Main.rand.Next(0, 364));
				Projectile.rotation += rotationValue - MathHelper.ToRadians(90);
				firstSpawn = false;
            }
			
			if (++Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 3)
				{
					SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);

					Projectile.frame++;
				}
				else
				{
					Projectile.Kill();
				}

			}
			
			
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
			

		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if(hit.Crit)
            {
				Player projOwner = Main.player[Projectile.owner];
				projOwner.AddBuff(ModContent.BuffType<Alacrity>(), 60*5);
			}

			float dustAmount = 12f;
			float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 4f);
				spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
				int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemDiamond);
				Main.dust[dust].scale = 1.5f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = target.Center + spinningpoint5;
				Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 7f;
			}
			
			base.OnHitNPC(target, hit, damageDone);
		}
	}
}
