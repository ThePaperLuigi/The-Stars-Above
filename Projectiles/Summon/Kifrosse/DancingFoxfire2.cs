using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.Kifrosse
{
    public class DancingFoxfire2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("DancingFoxfire2");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.minionSlots = 0f;
            Projectile.width = 30;               //The width of projectile hitbox
            Projectile.height = 30;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = true;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 999;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 1f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame


        }
        float rotationSpeed = 0f;
        NPC chosenTarget;
        float chosenTargetDistance;
        public override void AI()
        {

            Projectile.timeLeft = 999;
            Player player = Main.player[Projectile.owner];
            WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
            if (player.dead && !player.active)
            {






            }
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.Kifrosse.Kifrosse3>()] >= 1 || player.ownedProjectileCounts[ProjectileType<Kifrosse4>()] >= 1 || player.ownedProjectileCounts[ProjectileType<Kifrosse5>()] >= 1)
            {



            }
            else
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
            double dist = 148; //Distance away from the player
            Vector2 adjustedPosition = new Vector2(modPlayer.KifrossePosition.X, modPlayer.KifrossePosition.Y - 20);

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            Projectile.ai[1] -= 0.4f;


            float rotationsPerSecond = rotationSpeed;
            rotationSpeed -= 0.4f;
            bool rotateClockwise = true;

            NPC closest = null;
            float closestDistance = 9999999;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                float distance = Vector2.Distance(npc.Center, Projectile.Center);


                if (npc.active && npc.Distance(Projectile.position) < closestDistance)
                {
                    closest = npc;
                    closestDistance = npc.Distance(Projectile.position);
                }




            }

            /*if (closest.CanBeChasedBy() && closestDistance < 1200f)
			{
				for (int i3 = 0; i3 < 50; i3++)
				{
					Vector2 position2 = Vector2.Lerp(projectile.Center, closest.Center, (float)i3 / 50);
					Dust d = Dust.NewDustPerfect(position2, 183, null, 240, default(Color), 0.3f);
					d.fadeIn = 0.4f;
					d.noLight = true;
					d.noGravity = true;
				}
			}*/

            SearchForTargets(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);

            if (Projectile.ai[0] > 80 && foundTarget)
            {

                if (closest.CanBeChasedBy() && closestDistance < 1200f)
                {
                    Projectile.ai[0] = 0;
                    int type = ProjectileType<Projectiles.Bosses.OldBossAttacks.Icebolt>();


                    Vector2 position = Projectile.Center;
                    float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                    float launchSpeed = 36f;
                    Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                    Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;
                    for (int d = 0; d < 6; d++)
                    {
                        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 221, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default, 1.5f);
                    }
                    int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, player.whoAmI);

                    Main.projectile[index].originalDamage = Projectile.damage;

                }
            }

            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame > 3)
                {
                    Projectile.frame = 0;

                }


            }
            //projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(-90f);
            Projectile.ai[0]++;







        }
        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 700f;
            targetCenter = Projectile.position;
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
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

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 120);

        }



    }
}
