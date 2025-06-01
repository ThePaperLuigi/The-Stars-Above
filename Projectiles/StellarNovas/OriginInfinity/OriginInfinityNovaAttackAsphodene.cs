
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.StellarNovas;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Projectiles.Melee.Umbra;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.StellarNovas.OriginInfinity
{
	public class OriginInfinityNovaAttackAsphodene : ModProjectile
	{
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 300;
			Projectile.height = 300;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.timeLeft = 280;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.light = 1f;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
		bool targetWasHit = false;
        Vector2 targetPosition = Vector2.Zero;
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {

            //hitbox.Width /= 2;
            //hitbox.Height /= 2;
            base.ModifyDamageHitbox(ref hitbox);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.frame >= 1)
            {
                default(Effects.FireflyVFX).Draw(Projectile);
            }

            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>();

            Projectile.friendly = false;
            Projectile.ai[1] = 3;
            target.AddBuff(BuffType<NovaChainAttackAsphodene>(), (int)((player.novaEffectDuration + player.novaEffectDurationMod) * 60));

            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
                float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(target.Center, 0, 0, DustID.GemDiamond, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
                float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(3));
                float scale = 0.3f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
                float scale = 0.4f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                Main.dust[dustIndex].noGravity = true;

            }
            float dustAmount = 40f;
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 15f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemDiamond);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 15f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(32f, 16f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + MathHelper.ToRadians(90));
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemAmethyst);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 25f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(128f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Yellow);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 12f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemDiamond);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 9f;
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.X < 0)
			{
                Projectile.spriteDirection = -1;
            }
            else
            {

            }
            if(Projectile.friendly && Projectile.frame >= 1)
            {
                TryDamageInvincibleNPC();
            }
            Projectile.alpha -= 20;
            Projectile.ai[1]--;
            if (Projectile.frame == 0 && Projectile.ai[0] > 10)
            {
                for (int i = 0; i < 2; i++)
                {
                    // pick an initial offset from the center
                    Vector2 vector = new Vector2(
                        Main.rand.Next(-548, 548) * (0.003f * 200) - 10,
                        Main.rand.Next(-548, 548) * (0.003f * 200) - 10);

                    // inward velocity (points toward the projectile center)
                    Vector2 inward = -vector / 16f;

                    // perpendicular (tangential) velocity to make it swirl
                    float swirlStrength = 0.3f; // tweak this to speed up/slow down the swirl
                    Vector2 tangent = new Vector2(-inward.Y, inward.X) * swirlStrength;

                    // spawn the dust
                    int idx = Dust.NewDust(
                        Projectile.Center + vector,
                        1, 1,
                        DustID.GemDiamond,
                        0f, 0f, 0,
                        new Color(1f, 1f, 1f),
                        1.5f
                    );
                    Dust d = Main.dust[idx];

                    // combine inward pull + swirl + compensate for projectile movement
                    d.velocity = inward + tangent - Projectile.velocity / 4f;
                    d.noGravity = true;
                }
                for (int i = 0; i < 1; i++)
                {
                    // pick an initial offset from the center
                    Vector2 vector = new Vector2(
                        Main.rand.Next(-848, 848) * (0.003f * 200) - 10,
                        Main.rand.Next(-848, 848) * (0.003f * 200) - 10);

                    // inward velocity (points toward the projectile center)
                    Vector2 inward = -vector / 16f;

                    // perpendicular (tangential) velocity to make it swirl
                    float swirlStrength = 0.3f; // tweak this to speed up/slow down the swirl
                    Vector2 tangent = new Vector2(-inward.Y, inward.X) * swirlStrength;

                    // spawn the dust
                    int idx = Dust.NewDust(
                        Projectile.Center + vector,
                        1, 1,
                        DustID.GemTopaz,
                        0f, 0f, 0,
                        new Color(1f, 1f, 1f),
                        1.5f
                    );
                    Dust d = Main.dust[idx];

                    // combine inward pull + swirl + compensate for projectile movement
                    d.velocity = inward + tangent - Projectile.velocity / 8f;
                    d.noGravity = true;
                }

            }
            if (Projectile.ai[1] > 0)
            {
                Projectile.velocity *= 0.3f;
                if (Projectile.ai[1] == 1)
                {
                    if (Main.myPlayer == player.whoAmI)
                    {
                            //Projectile.NewProjectile(Projectile.GetSource_FromAI(), targetPosition, Vector2.Zero, ModContent.ProjectileType<FireflyKickExplosion>(), Projectile.damage, 0f, Main.myPlayer);

                    }
                    int speed = 30;
                    //SoundEngine.PlaySound(StarsAboveAudio.SFX_ThundercrashEnd, Projectile.Center);
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_LegendarySlash, Projectile.Center);
                    if (Projectile.direction == 1)
                    {
                        Projectile.velocity = new Vector2(speed, speed);

                    }
                    else
                    {
                        Projectile.velocity = new Vector2(-speed, speed);

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(4));
                        float scale = -0.1f + Main.rand.NextFloat() * 0.9f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmber, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 90; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(19));
                        float scale = -0.1f + Main.rand.NextFloat() * 1.9f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemDiamond, perturbedSpeed.X, perturbedSpeed.Y, 100, default, 1.5f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(3));
                        float scale = -0.1f + Main.rand.NextFloat() * 0.9f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 70; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(3));
                        float scale = 0.3f + Main.rand.NextFloat() * 0.9f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, perturbedSpeed.X, perturbedSpeed.Y, 0, default, 2f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
                        float scale = 0.4f + Main.rand.NextFloat() * 0.9f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                }
            }
            else
            {
                Projectile.ai[0]++;
            }
			
            if (Projectile.ai[0] > 60)
            {
                if (Projectile.frame == 0)
                {
                    Projectile.frame++;
                    int speed = 30;
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, Projectile.Center);
                    //SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceDurandalHit, Projectile.Center);
                    if(Projectile.direction == 1)
                    {
                        Projectile.velocity = new Vector2(speed, speed);

                    }
                    else
                    {
                        Projectile.velocity = new Vector2(-speed, speed);

                    }
                    for (int g = 0; g < 4; g++)
                    {

                        int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), -Projectile.velocity/20, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), -Projectile.velocity/20, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), -Projectile.velocity/20, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), -Projectile.velocity/20, Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(24));
                        float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemDiamond, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(36));
                        float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(38));
                        float scale = 0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 20; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(37));
                        float scale = 0.1f + Main.rand.NextFloat() * 0.3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                }
                
            }
            else
            {
                Projectile.velocity *= 0.89f;

            }
            if (Projectile.ai[0] > 40)
            {
                //Projectile.velocity *= 0.8f;

            }
            if (Projectile.ai[0] > 125)
            {
                Projectile.Kill();
                Projectile.alpha += 80;

            }
            
        }
		
        void TryDamageInvincibleNPC()
        {
            var player = Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>();

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.Distance(Projectile.Center) < 150 && npc.dontTakeDamage)
                {
                    int damage = 0;
                    int uniqueCrit = Main.rand.Next(100);
                    if (uniqueCrit <= player.novaCritChance + player.novaCritChanceMod)
                    {
                        damage = (int)(player.novaCritDamage * (1 + player.novaCritDamageMod / 100));
                        npc.life -= damage;
                        if(npc.life <= 0)
                        {
                            npc.life = 1;
                        }
 
                    }
                    else
                    {
                        damage = (int)(player.novaDamage * (1 + player.novaDamageMod / 100));
                        npc.life -= damage;
                        if (npc.life <= 0)
                        {
                            npc.life = 1;
                        }
                    }
                    npc.AddBuff(BuffType<NovaChainAttackAsphodene>(), (int)((player.novaEffectDuration + player.novaEffectDurationMod) * 60));
                    OnHitNPC(npc, new NPC.HitInfo(),damage);
                    targetWasHit = true;
                    targetPosition = npc.Center;
                    Projectile.friendly = false;
                    Rectangle textPos = new Rectangle((int)npc.position.X, (int)npc.position.Y - 20, npc.width, npc.height);
                    CombatText.NewText(textPos, new Color(255, 58, 58, 255), $"{damage}", false, false);
                }
            }

            
        }
	}
}
