
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Other.OrbitalExpresswayPlush
{
    public class ExpresswayTarget : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Carian Dark Moon");
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 180;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hide = false;
            Projectile.ownerHitCheck = false;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
        }

        float startingSize = 1;
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        // It appears that for this AI, only the ai0 field is used!
        public override void AI()
        {
            float rotationsPerSecond = 0.3f;
            bool rotateClockwise = true;
            if (Projectile.ai[0] == 0)
            {
                startingSize = 1;
            }
            //The rotation is set here
            Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
            Projectile.ai[0] += 1f;
            
            
            startingSize -= 0.1f;
            startingSize = MathHelper.Clamp(startingSize, 0f, 1f);
            Projectile.scale = MathHelper.Lerp(2f,27f,startingSize);
            if (Projectile.timeLeft < 30)
            {
                Projectile.alpha+= 10;
            }
            else
            {
                if (Main.rand.NextBool(5))
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 20,
                        Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
                    dust.velocity += Projectile.velocity * 0.3f;
                    dust.velocity *= 0.2f;
                }
                Projectile.alpha = (int)MathHelper.Lerp(0, 250, startingSize);
            }
            
            
            if(Projectile.timeLeft == 120)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_OrbitalTrain, Projectile.Center);

            }

        }

        public override bool PreDraw(ref Color lightColor)
        {
            
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            Player projOwner = Main.player[Projectile.owner];
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                SpawnTrain(projOwner);
                SpawnVFX(projOwner);

            }

        }

        private void SpawnTrain(Player projOwner)
        {
            Vector2 position = new Vector2(projOwner.Center.X, Projectile.Center.Y - 1200);

            float launchSpeed = 20f;
            Vector2 direction = Vector2.Normalize(Projectile.Center - position);
            Vector2 velocity = direction * launchSpeed;

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<ExpresswayProjectile>(), Projectile.damage, 0f, projOwner.whoAmI);
        }
        private void SpawnVFX(Player projOwner)
        {
            Vector2 position = new Vector2(projOwner.Center.X, Projectile.Center.Y - 1200);
            Vector2 position2 = new Vector2(Projectile.Center.X, Projectile.Center.Y + 80);


            float launchSpeed = 20f;
            Vector2 direction = Vector2.Normalize(position2 - position);
            Vector2 velocity = direction * launchSpeed;

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y + 80, velocity.X, velocity.Y, ProjectileType<ExpresswayVFX>(), 0, 0f, projOwner.whoAmI);
        }
    }
}
