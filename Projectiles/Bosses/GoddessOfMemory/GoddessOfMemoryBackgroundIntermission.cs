using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class GoddessOfMemoryBackgroundIntermission : ModProjectile
    {
        public virtual string BackgroundTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryBackground";
        public virtual string BorderTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaIntermissionBorder";
        public virtual float BorderScale => 0.95f;

        public Vector2 BackgroundScrollVelocity => new Vector2(Projectile.ai[0], Projectile.ai[1]);

        public override string Texture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaIntermission";

        public override void SetDefaults()
        {
            Projectile.width = 1000;
            Projectile.height = 1000;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 3600;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.hide = true;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Vector2 scrollVelocity = Vector2.UnitX.RotatedBy(Main.rand.NextFloat(MathHelper.TwoPi)) * Main.rand.NextFloat(0.01f, 0.1f);
            Projectile.ai[0] = scrollVelocity.X;
            Projectile.ai[1] = scrollVelocity.Y;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}
