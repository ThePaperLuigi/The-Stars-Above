using Microsoft.Xna.Framework;
using StarsAbove.Buffs.CloakOfAnArbiter;
using StarsAbove.Projectiles.Summon.StarphoenixFunnel;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Magic.CloakOfAnArbiter
{
    public class FairyAttack : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Verthunder");     //The English name of the Projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;               //The width of Projectile hitbox
            Projectile.height = 4;              //The height of Projectile hitbox
                                                //Projectile.aiStyle = 1;             //The ai style of the Projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the Projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the Projectile deal damage to the player?
            Projectile.penetrate = 1;           //How many monsters the Projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60;          //The live time for the Projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the Projectile, 255 for completely transparent. (aiStyle 1 quickly fades the Projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your Projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the Projectile
            Projectile.ignoreWater = true;          //Does the Projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the Projectile collide with tiles?
            Projectile.extraUpdates = 100;            //Set to above 0 if you want the Projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Magic;

            //AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            return true; // return false because we are handling collision
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.AddBuff(ModContent.BuffType<FairyTagDamage>(), 180);
            //modifiers.SetCrit();
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 2f)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + Main.rand.Next(-15,15), Projectile.Center.Y + Main.rand.Next(-15, 15), 0, 0, ModContent.ProjectileType<FairyAttackEffect>(), 0, 0, player.whoAmI, 0f);

                }
            }
           

           
        }



        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Keybrand,
                     new ParticleOrchestraSettings { PositionInWorld = Projectile.Center },
                     player.whoAmI);
            for (int i = 0; i < 5; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + Main.rand.Next(-15, 15), Projectile.Center.Y + Main.rand.Next(-15, 15), 0, 0, ModContent.ProjectileType<FairyAttackEffect>(), 0, 0, player.whoAmI, 0f, 0f, 1f);

            }
            for (int d = 0; d < 5; d++)
            {
                ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.PaladinsHammer,
                                     new ParticleOrchestraSettings { PositionInWorld = Projectile.Center },
                                     player.whoAmI);
            }
            for (int d = 0; d < 27; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Enchanted_Gold, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 1f);;
            }
            for (int d = 0; d < 16; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GoldFlame, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default, 1f);
            }

        }
    }
}
