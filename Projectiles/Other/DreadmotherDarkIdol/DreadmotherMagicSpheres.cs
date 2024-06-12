using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Summon.Kifrosse;
using StarsAbove.Effects;
using StarsAbove.Projectiles.Bosses.OldBossAttacks;
using StarsAbove.Projectiles.Melee.RebellionBloodArthur;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Other.DreadmotherDarkIdol
{
    public class DreadmotherMagicSpheres : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.minionSlots = 0f;
            Projectile.width = 38;               //The width of projectile hitbox
            Projectile.height = 44;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Magic;
            Projectile.minion = false;
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame


        }
        public override bool PreDraw(ref Color lightColor)
        {
            //default(SmallPurpleTrail).Draw(Projectile);

            return true;
        }
        float rotationSpeed = 0f;
        NPC chosenTarget;
        bool firstSpawn = true;
        float chosenTargetDistance;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();

            Projectile.velocity *= 0.93f;
            Projectile.alpha = 100;
            Projectile.scale = 0.6f;
            if (player.dead && !player.active)
            {

            }
            Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, null, 240, default, 1.5f);
            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
            Projectile.ai[0] += 1f;
            Player p = Main.player[Projectile.owner];

           
            if (firstSpawn)
            {
                for (int d = 0; d < 15; d++)
                {
                    Dust.NewDust(Projectile.Center, 0, 0, DustID.Shadowflame, Projectile.velocity.X, Projectile.velocity.Y, 150, default, 0.7f);

                }

                firstSpawn = false;
            }

            float maxDetectRadius = 800f; // The maximum radius at which a projectile can detect a target
            float projSpeed = 20f; // The speed at which the projectile moves towards the target

            // Trying to find NPC closest to the projectile
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            // If found, change the velocity of the projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero

            if (Projectile.ai[0] > 40)
            {
                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
            



            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame > 4)
                {
                    Projectile.frame = 0;

                }
            }
        }
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 15; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Shadowflame, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);

            }
            base.OnKill(timeLeft);
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int d = 0; d < 5; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Shadowflame, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);

            }
        }



    }
}
