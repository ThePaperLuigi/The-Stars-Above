using StarsAbove.Buffs.GoddessOfMemory;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryPlayer : ModPlayer
    {
        private bool pendingArenaSpawn;

        public override void Load()
        {
            On_Player.Spawn += SpawnInArena;
        }

        public override void Unload()
        {
            On_Player.Spawn -= SpawnInArena;
        }

        private static void SpawnInArena(On_Player.orig_Spawn orig, Player player, PlayerSpawnContext context)
        {
            orig(player, context);

            if (GoddessOfMemoryArenaSystem.IsActive && !player.dead)
            {
                if (Main.netMode == NetmodeID.Server)
                {
                    player.GetModPlayer<GoddessOfMemoryPlayer>().pendingArenaSpawn = true;
                }
                else if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    GoddessOfMemoryArenaSystem.TeleportPlayerToArena(player);
                }
            }
        }

        internal void CompleteArenaSpawn()
        {
            if (!pendingArenaSpawn)
            {
                return;
            }

            pendingArenaSpawn = false;
            if (GoddessOfMemoryArenaSystem.IsActive && Player.active && !Player.dead)
            {
                GoddessOfMemoryArenaSystem.TeleportPlayerToArena(Player);
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (GoddessOfMemoryArenaSystem.IsActive && Main.netMode != NetmodeID.SinglePlayer)
            {
                Player.respawnTimer = 0;
            }
        }

        public override void UpdateDead()
        {
            if (GoddessOfMemoryArenaSystem.IsActive && Main.netMode != NetmodeID.SinglePlayer)
            {
                Player.respawnTimer = 0;
            }
        }

        public override void PreUpdateBuffs()
        {
            if (!GoddessOfMemoryArenaSystem.IsActive)
            {
                return;
            }

            int arenaBuff = GoddessOfMemoryArenaSystem.ActiveIsPhase
                ? GoddessOfMemoryArenaSystem.ActiveArena switch
                {
                    1 => ModContent.BuffType<GoddessOfMemoryPhase1Buff>(),
                    2 => ModContent.BuffType<GoddessOfMemoryPhase2Buff>(),
                    3 => ModContent.BuffType<GoddessOfMemoryPhase3Buff>(),
                    4 => ModContent.BuffType<GoddessOfMemoryPhase4Buff>(),
                    _ => 0
                }
                : GoddessOfMemoryArenaSystem.ActiveArena == 1 ? ModContent.BuffType<MoiraesReflection>() : 0;

            for (int i = Player.buffType.Length - 1; i >= 0; i--)
            {
                int buffType = Player.buffType[i];
                if (buffType != arenaBuff && (buffType == ModContent.BuffType<MoiraesReflection>()
                    || buffType == ModContent.BuffType<GoddessOfMemoryPhase1Buff>()
                    || buffType == ModContent.BuffType<GoddessOfMemoryPhase2Buff>()
                    || buffType == ModContent.BuffType<GoddessOfMemoryPhase3Buff>()
                    || buffType == ModContent.BuffType<GoddessOfMemoryPhase4Buff>()
                    || buffType == ModContent.BuffType<GoddessOfMemoryFinalPhaseBuff>()))
                {
                    Player.DelBuff(i);
                }
            }

            if (arenaBuff != 0)
            {
                Player.AddBuff(arenaBuff, 2, quiet: true);
            }
        }

        public override void PostUpdateEquips()
        {
            if (Player.HasBuff<CelestialAugury>())
            {
                Player.wingTime = Player.wingTimeMax;
                Player.rocketTime = Player.rocketTimeMax;
            }
        }
    }

    public class GoddessOfMemoryPlayerSystem : ModSystem
    {
        public override void PostUpdatePlayers()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                return;
            }

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Main.player[i].GetModPlayer<GoddessOfMemoryPlayer>().CompleteArenaSpawn();
            }
        }
    }
}
