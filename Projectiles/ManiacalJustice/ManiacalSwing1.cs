using Microsoft.Xna.Framework;
using StarsAbove.Buffs.ManiacalJustice;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.ManiacalJustice
{
    //
    public class ManiacalSwing1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Maniacal Justice");
			Main.projFrames[Projectile.type] = 3;
			;
			//DrawOffsetX = -60;
		}
		public override void SetDefaults()
		{
			Projectile.width = 250;
			Projectile.height = 250;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 20;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			DrawOriginOffsetY = -30;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		bool firstSpawn = true;
		int itemAnimationSaved;
		
		public override void AI()
		{
			Player projOwner = Main.player[Projectile.owner];

			Lighting.AddLight(Projectile.Center, new Vector3(1f, 1f, 1f));
			if(firstSpawn)
            {
				firstSpawn = false;
				itemAnimationSaved = projOwner.itemTime/2;
				Projectile.ai[0] = itemAnimationSaved;
            }
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			
			if (projOwner.itemAnimation <= 1)
			{
				Projectile.Kill();
			}

			Projectile.frame = (int)MathHelper.Lerp(2, 0, Projectile.ai[0] / itemAnimationSaved);
			//Projectile.frame = (int)MathHelper.Lerp(0, 4, 0.5f);
			Projectile.ai[0]--;
			if(Projectile.ai[0] < 0)
            {
				Projectile.ai[0] = 0;
            }
			Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
			Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);

		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks++;
			if(crit)
            {
				Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks += 5;

			}
			if(Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks > 100)
            {
				Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks = 100;

			}
			if(Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks < 100 && Main.player[Projectile.owner].GetModPlayer<WeaponPlayer>().LVStacks >= 50)
            {
				target.AddBuff(BuffType<KarmicRetribution>(), 60 * 10);

			}
			OnHitDust(target);
			base.OnHitNPC(target, damage, knockback, crit);
		}

		private void OnHitDust(NPC target)
		{
			
			float dustAmount = 33f;
			float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(38f, 1f);
				spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
				int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemAmethyst);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = target.Center + spinningpoint5;
				Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
			}
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(38f, 1f);
				spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
				int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemAmethyst);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = target.Center + spinningpoint5;
				Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
			}
		}
	}
}