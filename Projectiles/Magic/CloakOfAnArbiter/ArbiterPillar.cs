using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using StarsAbove.Systems;
using Terraria.GameContent.Drawing;

namespace StarsAbove.Projectiles.Magic.CloakOfAnArbiter
{
    public class ArbiterPillar : ModProjectile
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
            Projectile.width = 80;               //The width of projectile hitbox
            Projectile.height = 80;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 125;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Magic;
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.YellowTrail).Draw(Projectile);

            return true;
        }
        bool firstSpawn = true;
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, TorchID.Ichor);

            if (firstSpawn)
            {
                Projectile.timeLeft = 60;
                Projectile.scale = 1.3f;
                firstSpawn = false;
            }

            if (Projectile.timeLeft < 30)
            {
                Projectile.scale *= 0.92f;
                Projectile.velocity *= 0.96f;

            }
            else
            {
                Projectile.scale *= 0.99f;
                Projectile.velocity *= 1.04f;

            }

            base.AI();

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {


            for (int d = 0; d < 23; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
                float scale = 1f + Main.rand.NextFloat() * 0.6f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.6f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 23; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
                float scale = 1f + Main.rand.NextFloat() * 0.6f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.9f);
                Main.dust[dustIndex].noGravity = true;

            }

            Player player = Main.player[Projectile.owner];
            player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Keybrand,
                     new ParticleOrchestraSettings { PositionInWorld = target.Center },
                     player.whoAmI);
            for (int i = 0; i < 5; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X + Main.rand.Next(-15, 15), target.Center.Y + Main.rand.Next(-15, 15), 0, 0, ModContent.ProjectileType<FairyAttackEffect>(), 0, 0, player.whoAmI, 0f, 0f, 1f);

            }
            for (int d = 0; d < 5; d++)
            {
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PaladinsHammer,
                                     new ParticleOrchestraSettings { PositionInWorld = target.Center },
                                     player.whoAmI);
            }
            for (int d = 0; d < 27; d++)
            {
                Dust.NewDust(target.Center, 0, 0, DustID.Enchanted_Gold, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 1f); ;
            }
            for (int d = 0; d < 16; d++)
            {
                Dust.NewDust(target.Center, 0, 0, DustID.GoldFlame, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default, 1f);
            }



        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SetCrit();
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 18; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Gold, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 150, default, 0.5f);

            }
            if (Projectile.penetrate != 0)
            {
                for (int d = 0; d < 23; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
                    float scale = 3f + Main.rand.NextFloat() * 3.6f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Gold, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.6f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 23; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
                    float scale = 3f + Main.rand.NextFloat() * 1.6f;
                    perturbedSpeed = -perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Gold, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.6f);
                    Main.dust[dustIndex].noGravity = true;

                }
            }
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            //SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        }
    }
}
