using StarsAbove.Buffs.GoddessOfMemory;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.SceneEffects
{
    public class GoddessOfMemorySceneEffect : ModSceneEffect
    {
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>(HasSeaOfStarsBuff(Main.LocalPlayer)
            ? "StarsAbove/SeaOfStarsBackgroundStyle"
            : "StarsAbove/ObservatoryBackgroundStyle");

        public override int Music => MusicLoader.GetMusicSlot(Mod, HasSeaOfStarsBuff(Main.LocalPlayer)
            ? "Sounds/Music/MareLamentorum"
            : "Sounds/Music/ElpisDay");

        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

        public override bool IsSceneEffectActive(Player player)
        {
            return HasBiomeBuff(player);
        }

        public static bool HasSeaOfStarsBuff(Player player)
        {
            if (GoddessOfMemoryArenaSystem.IsActive)
            {
                return GoddessOfMemoryArenaSystem.ActiveArena == (GoddessOfMemoryArenaSystem.ActiveIsPhase ? 4 : 5);
            }

            return player.HasBuff<GoddessOfMemoryPhase4Buff>() || player.HasBuff<GoddessOfMemoryFinalPhaseBuff>();
        }

        public static bool HasObservatoryBuff(Player player)
        {
            if (GoddessOfMemoryArenaSystem.IsActive)
            {
                return !GoddessOfMemoryArenaSystem.ActiveIsPhase && GoddessOfMemoryArenaSystem.ActiveArena == 1;
            }

            return player.HasBuff<MoiraesReflection>() && !HasSeaOfStarsBuff(player);
        }

        public static bool HasBiomeBuff(Player player)
        {
            return HasObservatoryBuff(player) || HasSeaOfStarsBuff(player);
        }
    }
}
