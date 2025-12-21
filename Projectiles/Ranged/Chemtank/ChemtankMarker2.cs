using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ranged.Chemtank
{
    public class ChemtankMarker2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dreadnought Chemtank");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 2;

        }

        public override void SetDefaults()
        {
            Projectile.width = 38;               //The width of projectile hitbox
            Projectile.height = 16;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = false;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 999;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Ranged;

        }
        float rotationSpeed = 0f;
        NPC chosenTarget;
        float chosenTargetDistance;
        bool firstSpawn = true;

        public override void AI()
        {
            if (firstSpawn)
            {
                Projectile.ai[0] = 480;

                Projectile.ai[1] = 60;
                firstSpawn = false;
            }
            Projectile.timeLeft = 999;
            Player player = Main.player[Projectile.owner];
            if (player.dead && !player.active || !player.HasBuff(BuffType<Buffs.Ranged.Chemtank.ChemtankBuff>()))
            {
                Projectile.Kill();
            }

            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            Player p = Main.player[Projectile.owner];

            //Factors for calculations
            double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 78; //Distance away from the player
            Vector2 adjustedPosition = new Vector2(player.Center.X, player.Center.Y);

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            //Projectile.ai[1] += 0.9f;


            float rotationsPerSecond = rotationSpeed;
            rotationSpeed -= 0.4f;
            bool rotateClockwise = true;

            SearchForTargets(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);

            if (Projectile.ai[0] <= 0)
            {

                if (p.HasBuff(BuffType<Buffs.Ranged.Chemtank.Chemtank2>()))
                {
                    p.ClearBuff(BuffType<Buffs.Ranged.Chemtank.Chemtank2>());
                    Projectile.ai[0] = 480;
                    int type = ProjectileType<Projectiles.Ranged.Chemtank.ChemtankRound>();


                    Vector2 position = p.Center;
                    float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                    float launchSpeed = 16f;
                    Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                    Vector2 direction = Vector2.Normalize(Projectile.Center - p.Center);//Shoot at the marker from the player.
                    Vector2 velocity = direction * launchSpeed;
                    Vector2 velocitySlow = direction * 1;
                    Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocitySlow.X, velocitySlow.Y)) * 10f;
                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, muzzleOffset.X, muzzleOffset.Y, ProjectileType<ChemtankExplosion>(), 40, 2, player.whoAmI);

                    //New code.
                    int numberProjectiles = 12 + Main.rand.Next(2); //random shots
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(45)); // 30 degree spread.
                                                                                                                                // If you want to randomize the speed to stagger the projectiles
                        float scale = 1f - Main.rand.NextFloat() * .3f;
                        perturbedSpeed = perturbedSpeed * scale;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 0, 2, player.whoAmI);
                    }


                    for (int d = 0; d < 21; d++)
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(47));
                        float scale = 2f - Main.rand.NextFloat() * .9f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(position, 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 16; d++)
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X / 2, velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(47));
                        float scale = 2f - Main.rand.NextFloat() * .9f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(position, 0, 0, 31, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                        Main.dust[dustIndex].noGravity = true;
                    }

                }
            }
            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-90f);
            Projectile.ai[0]--;

            if (Projectile.ai[0] > 0)
            {
                Projectile.alpha = (int)Projectile.ai[0];
                if (Projectile.alpha > 230)
                {
                    Projectile.alpha = 230;
                }
                Projectile.frame = 0;
            }
            else
            {
                Projectile.frame = 1;
            }







        }
        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 50f;
            targetCenter = Projectile.position;
            foundTarget = false;



            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy() && Vector2.Distance(npc.Center, Projectile.Center) < 50)
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if ((closest && inRange || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            // friendly needs to be set to true so the minion can deal contact damage
            // friendly needs to be set to false so it doesn't damage things like target dummies while idling
            // Both things depend on if it has a target or not, so it's just one assignment here
            // You don't need this assignment if your minion is shooting things instead of dealing contact damage

            //Projectile.friendly = foundTarget;
        }


    }
}
