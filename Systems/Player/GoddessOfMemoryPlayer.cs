using StarsAbove.Buffs.GoddessOfMemory;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryPlayer : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            if (Player.HasBuff<CelestialAugury>())
            {
                Player.wingTime = Player.wingTimeMax;
                Player.rocketTime = Player.rocketTimeMax;
            }
        }
    }
}
