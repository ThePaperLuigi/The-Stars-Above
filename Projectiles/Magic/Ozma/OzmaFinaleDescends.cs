using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Ozma;
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Magic.Ozma
{
    public class OzmaFinaleDescends : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ozma Ascendant");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 500;               //The width of projectile hitbox
            Projectile.height = 500;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.scale = 1f;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 2f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.hide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;
        }
        float rotationSpeed = 10f;
        float expandStrength = 0f;


        public override void AI()
        {
            Player projOwner = Main.player[Projectile.owner];
            if (Projectile.ai[0] > 30)
            {


                projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("OzmaDamageFinale").Type, Projectile.damage, 0f, 0, 0);
                for (int d = 0; d < 38; d++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.FireworkFountain_Red,
                    Main.rand.Next(-1, 1), 0, 0, Scale: 1f);
                    dust.noGravity = true;

                }

                for (int d = 0; d < 16; d++)
                {
                    Dust.NewDust(Projectile.Center, 0, 0, DustID.Blood, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default, 0.5f);
                }
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 0.5f);
                }
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 0.5f);
                }

                Projectile.ai[0] = 0;

            }
            Projectile.ai[0]++;
            for (int d = 0; d < 4; d++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.LifeDrain,
                                    Main.rand.Next(-12, 0), 0, 0, Scale: 1.2f);
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.LifeDrain,
                                    Main.rand.Next(0, 12), 0, 0, Scale: 1.2f);
                dust.noGravity = true;
                dust.velocity.X = 0;
                dust2.noGravity = true;
                dust2.velocity.X = 0;
            }


            Projectile.alpha -= 10;
            expandStrength += 0.001f;
            Projectile.scale += 0.001f;
            if (Projectile.timeLeft < 10)
            {
                Projectile.alpha += 20;
                Projectile.scale -= 0.05f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            //Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            Player projOwner = Main.player[Projectile.owner];

            projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
            // Play explosion sound
            for (int i = 0; i < 100; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-15, 15), 100, default, 1f);
                Main.dust[dustIndex].noGravity = true;

                dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 91, Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10), 100, default, 0.6f);
                Main.dust[dustIndex].velocity *= 3f;
            }



        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            Player projOwner = Main.player[Projectile.owner];
            Player player = Main.player[Projectile.owner];


            if (hit.Crit)
            {
                projOwner.AddBuff(BuffType<AnnihilationState>(), 180);
            }

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
    }
}
