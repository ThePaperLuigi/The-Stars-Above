using StarsAbove.NPCs.GoddessOfMemory;
using System;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class GoddessOfMemorySpin : GoddessOfMemorySpriteProjectile
    {
        public const int TotalFrames = 12;
        public const float SpeedMultiplier = 1.3f;
        public static readonly int Duration = (int)Math.Ceiling(TotalFrames * GoddessOfMemoryBoss.IdleFrameDuration / SpeedMultiplier);
        public static readonly int Midpoint = (int)Math.Ceiling(Duration / 2f);

        protected override int FrameCount => TotalFrames;
        protected override float AnimationSpeed => SpeedMultiplier;
        protected override int AnimationDuration => Duration;
    }
}
