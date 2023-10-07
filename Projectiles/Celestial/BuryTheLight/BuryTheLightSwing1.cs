using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Celestial.BuryTheLight
{
    //
    public class BuryTheLightSwing1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stygian Swing");
            Main.projFrames[Projectile.type] = 4;
            //DrawOriginOffsetY = 30;
            //DrawOffsetX = -60;
        }
        public override void SetDefaults()
        {
            Projectile.width = 600;
            Projectile.height = 600;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 50;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;

            Projectile.hide = false;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = GetInstance<Systems.CelestialDamageClass>();
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            DrawOriginOffsetY = 150;
        }
        bool firstSpawn = true;
        public static Texture2D texture;
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);



            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            if (texture == null || texture.IsDisposed)
            {
                texture = (Texture2D)Request<Texture2D>(Projectile.ModProjectile.Texture);
            }

            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, Color.Black, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            ArmorShaderData data = GameShaders.Armor.GetSecondaryShader((byte)GameShaders.Armor.GetShaderIdFromItemId(ItemID.StardustDye), Main.LocalPlayer);
            data.Apply(null);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            //return Color.White;
            return new Color(255, 255, 255, 0) * (1f - Projectile.alpha / 255f);
        }
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        // It appears that for this AI, only the ai0 field is used!
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, new Vector3(1f, 1f, 1f));
            if (firstSpawn)
            {
                Projectile.scale = Main.rand.NextFloat(0.7f, 1f);
                firstSpawn = false;
            }
            Player projOwner = Main.player[Projectile.owner];
            projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
                    new Vector2(Main.MouseWorld.X + projOwner.velocity.X * 0.05f, Main.MouseWorld.Y + projOwner.velocity.Y * 0.05f)
                    ).ToRotation() + MathHelper.PiOver2);
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            // As long as the player isn't frozen, the spear can move
            if (!projOwner.frozen)
            {
                if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
                {
                    movementFactor = 49f; // Make sure the spear moves forward when initially thrown out
                    Projectile.netUpdate = true; // Make sure to netUpdate this spear
                }
                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
                {
                    movementFactor -= 0f;
                }
                else // Otherwise, increase the movement factor
                {
                    movementFactor += 0.7f;
                }
            }
            if (Projectile.frame >= 3)
            {
                Projectile.alpha += 22;
            }

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 3)
                {
                    Projectile.frame++;
                }
                else
                {
                    Projectile.Kill();
                }

            }
            Projectile.position += Projectile.velocity * movementFactor;

            if (Projectile.alpha >= 250)
            {
                Projectile.Kill();
            }
            Projectile.scale += 0.001f;
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            // Adding Pi to rotation if facing left corrects the drawing
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);


            // These dusts are added later, for the 'ExampleMod' effect
            /*if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 60,
					projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 269, Scale: 1.2f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 60,
					0, 0, 269, Scale: 0.3f);
				dust.velocity += projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}*/
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player projOwner = Main.player[Projectile.owner];

            if (target.GetGlobalNPC<StarsAboveGlobalNPC>().JudgementStacks > 20)
            {
                Projectile.NewProjectile(projOwner.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileType<BuryTheLightSlashFollowUp>(), Projectile.damage, 0, projOwner.whoAmI);
                projOwner.GetModPlayer<WeaponPlayer>().gaugeChangeAlpha = 1f;
                projOwner.GetModPlayer<WeaponPlayer>().judgementGauge += 3;
                target.GetGlobalNPC<StarsAboveGlobalNPC>().JudgementStacks = 0;

            }
            else
            {
                if (hit.Crit)
                {
                    target.GetGlobalNPC<StarsAboveGlobalNPC>().JudgementStacks += 2;
                }
                else
                {
                    target.GetGlobalNPC<StarsAboveGlobalNPC>().JudgementStacks++;
                }
            }
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            float randomConstant2 = Main.rand.Next(0, 360);


            float dustAmount = 13f;

            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(25f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.Electric);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * (3f + randomConstant2 / 24);
            }
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}