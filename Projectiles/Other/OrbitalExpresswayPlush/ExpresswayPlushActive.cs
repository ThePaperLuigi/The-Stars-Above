
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.CarianDarkMoon;
using StarsAbove.Projectiles.Magic.CarianDarkMoon;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Other.OrbitalExpresswayPlush
{
    public class ExpresswayPlushActive : ModProjectile
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
            Projectile.timeLeft = 100;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hide = false;
            Projectile.ownerHitCheck = false;
            Projectile.tileCollide = false;
            Projectile.friendly = false;
        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        // It appears that for this AI, only the ai0 field is used!
        public override void AI()
        {
            // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            // Sadly, Projectile/ModProjectile does not have its own
            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            projOwner.itemTime = 10;
            projOwner.itemAnimation = 10;
            //projectile.position.X = projOwner.Center.X - (float)(projectile.width / 2);
            // Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
            // MathHelper.ToRadians(xx degrees here)
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(Projectile.ai[0]);

            Projectile.velocity *= 0.97f;


            Projectile.ai[0]++;


            if (Projectile.timeLeft < 60)
            {
                //Projectile.alpha += 20;
                //Projectile.scale += 0.005f;
            }


        }

        public override bool PreDraw(ref Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item6, Projectile.Center);

            float dustAmount = 12f;
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
            }
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
              new ParticleOrchestraSettings { PositionInWorld =Projectile.Center },
              Projectile.owner);
        }
    }
}
