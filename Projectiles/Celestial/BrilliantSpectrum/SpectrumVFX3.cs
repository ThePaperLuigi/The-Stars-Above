using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Celestial.BrilliantSpectrum
{
    public class SpectrumVFX3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Warrior of Light's Aura");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 150;               //The width of projectile hitbox
            Projectile.height = 150;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.scale = 1f;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0f;            //How much light emit around the projectile
            Projectile.hide = true;

            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {

            behindNPCsAndTiles.Add(index);

        }
        float rotationSpeed = 10f;


        public override void AI()
        {
            Projectile.scale = 0.7f;
            Player projOwner = Main.player[Projectile.owner];
            var modPlayer = projOwner.GetModPlayer<WeaponPlayer>();
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            if (modPlayer.refractionGauge >= 90 && modPlayer.BrilliantSpectrumHeld)
            {
                Lighting.AddLight(Projectile.Center, TorchID.Blue);

            }
            else
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 10;

            float rotationsPerSecond = rotationSpeed;
            rotationSpeed = 0.4f;
            bool rotateClockwise = true;
            //The rotation is set here

            Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);

        }


    }
}
