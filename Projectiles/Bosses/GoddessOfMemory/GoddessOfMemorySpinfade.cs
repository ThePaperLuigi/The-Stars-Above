using Microsoft.Xna.Framework;
using StarsAbove.NPCs.GoddessOfMemory;
using System;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class GoddessOfMemorySpinfade : GoddessOfMemorySpriteProjectile
    {
        public const int TotalFrames = GoddessOfMemorySpin.TotalFrames;
        public const float SpeedMultiplier = GoddessOfMemorySpin.SpeedMultiplier * 1.2f;
        public static readonly int Duration = (int)Math.Ceiling(TotalFrames * GoddessOfMemoryBoss.IdleFrameDuration / SpeedMultiplier);
        public static readonly int Midpoint = (int)Math.Ceiling(Duration / 2f);

        protected override int FrameCount => TotalFrames;
        protected override float AnimationSpeed => SpeedMultiplier;
        protected override int AnimationDuration => Duration;

        protected override void UpdateOpacity(float elapsed)
        {
            float midpoint = Midpoint;
            float fade = elapsed <= midpoint
                ? elapsed / midpoint
                : (AnimationDuration - 1f - elapsed) / (AnimationDuration - 1f - midpoint);
            Projectile.alpha = (int)(255f * MathHelper.Clamp(fade, 0f, 1f));
        }
    }
}
