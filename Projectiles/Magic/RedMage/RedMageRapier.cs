using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Magic.RedMage;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Magic.RedMage
{
    public class RedMageRapier : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Red Mage's Rapier");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
                                                                        //DrawOffsetX = 40;
                                                                        //DrawOriginOffsetY = 81;
        }

        public override void SetDefaults()
        {
            Projectile.width = 240;               //The width of projectile hitbox
            Projectile.height = 240;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        int direction;//0 is right 1 is left
        float rotationStrength = 0.1f;
        bool firstSpawn = true;
        double deg;


        public override void AI()
        {

            Player player = Main.player[Projectile.owner];

            player.heldProj = Projectile.whoAmI;
            player.itemTime = 50;
            player.itemAnimation = 50;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);

            Projectile.scale = 0.7f;

            Projectile.ai[1] = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - player.Center.Y, Main.MouseWorld.X - player.Center.X) + MathHelper.ToRadians(180));

            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }
            if (player.HasBuff(BuffType<VerfireCasting>()) || player.HasBuff(BuffType<VerstoneCasting>()))
            {
                Vector2 vector = new Vector2(
                        Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
                        Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
                Dust d = Main.dust[Dust.NewDust(
                    player.MountedCenter + vector, 1, 1,
                    DustID.LifeDrain, 0, 0, 255,
                    Color.Red, 0.8f)];
                d.velocity = -vector / 12;
                d.velocity -= player.velocity / 8;
                d.noLight = true;
                d.noGravity = true;
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 10;
            Projectile.alpha -= 10;

            if (Projectile.ai[1] >= 240 || Projectile.ai[1] < 90)
            {
                player.direction = -1;
            }
            else
            {
                player.direction = 1;
            }
            deg = Projectile.ai[1];

            if (Projectile.ai[0] > 50)
            {


                if (player.HasBuff(BuffType<VerfireCasting>()))
                {
                    Projectile.ai[0] = 0;
                    int type = ProjectileType<Verfire>();


                    Vector2 position = Projectile.Center;
                    float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                    float launchSpeed = 36f;
                    Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                    Vector2 direction = Vector2.Normalize(mousePosition - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;

                    int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, player.whoAmI);
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_RDMCast, player.Center);
                    Main.projectile[index].originalDamage = Projectile.damage;
                    Projectile.Kill();

                }
                if (player.HasBuff(BuffType<VerstoneCasting>()))
                {
                    Projectile.ai[0] = 0;
                    int type = ProjectileType<Verstone>();


                    Vector2 position = Projectile.Center;
                    float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                    float launchSpeed = 36f;
                    Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                    Vector2 direction = Vector2.Normalize(mousePosition - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;

                    int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, player.whoAmI);
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_RDMCast, player.Center);
                    Main.projectile[index].originalDamage = Projectile.damage;


                    Projectile.Kill();
                }



            }
            Projectile.ai[0]++;
            double rad = deg * (Math.PI / 180);
            double dist = 78;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(0f);



        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int d = 0; d < 8; d++)
            {
                Dust.NewDust(target.Center, 0, 0, DustID.Electric, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 1.2f); Dust.NewDust(target.Center, 0, 0, DustID.Electric, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.5f);
                Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Blue, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.3f);

            }
            Player player = Main.player[Projectile.owner];

        }
        public override void OnKill(int timeLeft)
        {

            for (int d = 0; d < 12; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);

            }

            base.OnKill(timeLeft);
        }

    }
}
