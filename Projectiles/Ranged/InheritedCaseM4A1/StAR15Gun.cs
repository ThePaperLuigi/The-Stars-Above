
using Microsoft.Xna.Framework;
 
using StarsAbove.Buffs.Ranged.InheritedCaseM4A1;
using StarsAbove.Projectiles.Generics;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ranged.InheritedCaseM4A1
{
    public class StAR15Gun : InheritedCaseOrbitalsGun
    {
        public override string Texture => "StarsAbove/Projectiles/Ranged/InheritedCaseM4A1/StAR15Gun";
        public override string TextureFlash => "StarsAbove/Projectiles/Ranged/InheritedCaseM4A1/M4A1GunFlash";
        //Use the extra recoil/reload code.
        public override bool UseRecoil => false;
        //The dust that appears from the barrel after shooting.
        public override int SmokeDustID => Terraria.ID.DustID.Smoke;

        //The dust that fires from the barrel after shooting.
        public override int FlashDustID => Terraria.ID.DustID.GemTopaz; 
        //The distance the gun's muzzle is relative to the player. Remember this also is influenced by base distance.
        public override int MuzzleDistance => 30;
        //The distance the gun is relative to the player.
        public override float BaseDistance => 100;
        public override int StartingState => 0;
        public override bool KillOnIdle => false;
        public override bool ReActivateAfterIdle => true;
        public override int ReActivateAfterIdleTimer => 3;
        public override int ScreenShakeTime => 100; //100 is disabled
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 140;
            Projectile.height = 64;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }
        double deg;

        public override bool PreAI()
        {
            Player projOwner = Main.player[Projectile.owner];
            var modPlayer = projOwner.GetModPlayer<WeaponPlayer>();
            DrawOriginOffsetY = 0;
            if(projOwner.HasBuff(BuffType<AuxiliaryArmamentBuff>()))
            {
                Projectile.timeLeft = 10;
            
            }
            if(!modPlayer.M4A1Held)
            {
                Projectile.Kill();
            }
            float degAdjustment = 0f;

            if(modPlayer.ActiveGuns.Contains(Projectile.type))
            {
                switch(modPlayer.ActiveGuns.Count)
                {
                    case 0:

                        break;
                    case 1://doesn't matter

                        break;
                    case 2://2 active guns
                        degAdjustment = 180 * modPlayer.ActiveGuns.IndexOf(Projectile.type);
                        break;
                    case 3://3 active guns
                        degAdjustment = 120 * modPlayer.ActiveGuns.IndexOf(Projectile.type);
                        break;
                    case 4:
                        degAdjustment = 90 * modPlayer.ActiveGuns.IndexOf(Projectile.type);
                        break;
                    case 5:
                        degAdjustment = 72 * modPlayer.ActiveGuns.IndexOf(Projectile.type);
                        break;
                }
            }

            Projectile.ai[2] = (float)modPlayer.M4A1deg + degAdjustment;
            deg = Projectile.ai[2];
            double rad = deg * (Math.PI / 180);
            double dist = 50 + MathHelper.Lerp(0, 50f, EaseHelper.InOutQuad((float)(modPlayer.M4A1UseTimer / 100f)));

            Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.ai[2] += 1f;
            return true;
        }
        public override void PostAI()
        {
            Player projOwner = Main.player[Projectile.owner];
            var modPlayer = projOwner.GetModPlayer<WeaponPlayer>();

            float launchSpeed = 10f;
            Vector2 mousePosition = Main.MouseWorld;
            Vector2 direction = Vector2.Normalize(mousePosition - Projectile.Center);
            Vector2 arrowVelocity = direction * launchSpeed;

            if (Projectile.ai[0] == 0 && Projectile.localAI[1] >= 1)
            {
                Projectile.localAI[1] = 0;

                Vector2 position = Projectile.Center;
                SoundEngine.PlaySound(SoundID.Item11, Projectile.position);
                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(arrowVelocity.X, arrowVelocity.Y)) * 35f;
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
                //Fire bullets on a timer
                Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y);
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(projOwner.GetSource_ItemUse(projOwner.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<StAR15Round>(), projOwner.GetWeaponDamage(projOwner.HeldItem)/3, 1, projOwner.whoAmI);

            }

            base.PostAI();
        }
        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 20; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 150, default(Color), 0.8f);
            }
        }

    }
}
