using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using StarsAbove.Systems;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.Mercy;
using Terraria.GameContent.Drawing;

namespace StarsAbove.Projectiles.Ranged.TwoCrownBow
{
    public class TwoCrownBowArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Umbra");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 140;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;               //The width of projectile hitbox
            Projectile.height = 18;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 3;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 125;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.SmallWhiteTrail).Draw(Projectile);

            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Ranged/TwoCrownBow/TwoCrownBowArrowWhite");
            Texture2D texturebase = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Projectiles/Ranged/TwoCrownBow/TwoCrownBowArrow");

            Rectangle frame = texture.Frame(1, 1, 0, 0);
            Vector2 origin = frame.Size() / 2f;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, Color.White, Projectile.rotation,origin, Projectile.scale, SpriteEffects.None);
            Main.EntitySpriteDraw(texturebase, Projectile.Center - Main.screenPosition, frame, Color.White * Projectile.ai[2], Projectile.rotation, origin, Projectile.scale, SpriteEffects.None);

            return false;
        }
        bool firstSpawn = true;
        public override void AI()
        {
            Projectile.scale = 0.8f;
            Projectile.ai[2] += 0.04f;
            base.AI();
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int d = 0; d < 13; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
                float scale = 1f + Main.rand.NextFloat() * 0.6f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.5f);
                Main.dust[dustIndex].noGravity = true;

            }
            Player player = Main.player[Projectile.owner];

            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PrincessWeapon,
               new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
               Projectile.owner);

            player.GetModPlayer<WeaponPlayer>().terminationGauge += 1;
            player.GetModPlayer<WeaponPlayer>().gaugeChangeAlpha = 1f;
            if (player.GetModPlayer<WeaponPlayer>().terminationGauge >= 100)
            {
                player.GetModPlayer<WeaponPlayer>().terminationGauge = 100;

            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            for (int i = 0; i < 12; i++)
            {
                if (target.buffType[i] > 0 && Main.debuff[target.buffType[i]])
                {
                    modifiers.SourceDamage += 0.04f;
                    continue;
                }

                
                //target.buffTime[i] += 20 * 60;
            }

            base.ModifyHitNPC(target, ref modifiers);
        }
        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 18; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 150, default, 0.5f);
                Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 150, default, 0.4f);

            }
            
        }
    }
}
