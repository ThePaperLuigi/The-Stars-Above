using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.Other.OrbitalExpresswayPlush
{
    public class ExpresswayVFX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hawkmoon");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;               //The width of projectile hitbox
            Projectile.height = 20;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?

            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 2;            //Set to above 0 if you want the projectile to update multiple time in a frame

        }
        public override void AI()
        {
            
            for (int d = 0; d < 12; d++)
            {
                Dust dustIndex = Dust.NewDustPerfect(new Vector2(Projectile.Center.X, Projectile.Center.Y), DustID.FireworkFountain_Yellow);
                dustIndex.noGravity = true;
                dustIndex.scale = Main.rand.NextFloat(0.5f, 1f);
                
            }
            base.AI();
        }
        public override Color? GetAlpha(Color lightColor)
        {
            //return Color.White;
            return Color.White;
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            
            return false;
        }
       
        public override void OnKill(int timeLeft)
        {
        }
    }
}
