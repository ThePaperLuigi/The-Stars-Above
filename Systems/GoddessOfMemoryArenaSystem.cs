using Microsoft.Xna.Framework;
using StarsAbove.Subworlds;
using SubworldLibrary;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class GoddessOfMemoryArenaSystem : ModSystem
    {
        public static bool IsActive => SubworldSystem.IsActive<GoddessOfMemorySubworld>();
        public static bool ActiveIsPhase { get; private set; }
        public static int ActiveArena { get; private set; } = 1;
        public static Vector2 ActiveCenter => GetArenaCenter(ActiveIsPhase, ActiveArena);

        public static Vector2 GetArenaCenter(bool isPhase, int arena)
        {
            int maximum = isPhase ? 4 : 6;
            if (arena < 1 || arena > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(arena));
            }

            int centerX = Main.maxTilesX / 2;
            int centerY = Main.maxTilesY / 2;
            if (isPhase || arena > 2)
            {
                centerY += 50;
            }

            if (isPhase)
            {
                centerX += (arena * 2 - 5) * 600;
            }
            else
            {
                switch (arena)
                {
                    case 2:
                        centerY = Main.maxTilesY - 131;
                        break;
                    case 3:
                        centerX -= 2400;
                        break;
                    case 4:
                        centerX -= 1200;
                        break;
                    case 5:
                        centerX += 1200;
                        break;
                    case 6:
                        centerX += 2400;
                        break;
                }
            }

            return new Vector2(centerX * 16f + 8f, centerY * 16f + 8f);
        }

        public static void ResetArena()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            ActiveIsPhase = false;
            ActiveArena = 1;
            UpdateSpawnPoint();
            ApplyArenaTime();
            SynchronizeArena();
        }

        public static void ChangeArena(NPC boss, bool isPhase, int arena)
        {
            if (!IsActive || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            Vector2 center = GetArenaCenter(isPhase, arena);
            ActiveIsPhase = isPhase;
            ActiveArena = arena;
            UpdateSpawnPoint();
            ApplyArenaTime();
            SynchronizeArena();

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (player.active)
                {
                    TeleportPlayerToArena(player);
                }
            }

            boss.Center = FindLandingCenter(center, boss.width, boss.height);
            boss.velocity = Vector2.Zero;
            boss.netUpdate = true;
        }

        public static void TeleportPlayerToArena(Player player)
        {
            if (!IsActive || !player.active || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            Vector2 position = FindLandingCenter(ActiveCenter, player.width, player.height) - player.Size / 2f;
            if (Main.netMode == NetmodeID.Server)
            {
                RemoteClient.CheckSection(player.whoAmI, position);
            }

            player.RemoveAllGrapplingHooks();
            player.Teleport(position, TeleportationStyleID.RodOfDiscord);
            player.velocity = Vector2.Zero;
            player.fallStart = (int)(player.position.Y / 16f);
            player.fallStart2 = player.fallStart;

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendData(MessageID.TeleportEntity, number: 0, number2: player.whoAmI, number3: position.X, number4: position.Y, number5: TeleportationStyleID.RodOfDiscord);
            }
        }

        private static Vector2 FindLandingCenter(Vector2 center, int width, int height)
        {
            int maximumOffset = Math.Max(0, 49 * 16 - height / 2);
            for (int offset = 0; offset <= maximumOffset; offset++)
            {
                Vector2 position = center - new Vector2(width / 2f, height / 2f + offset);
                if (!Collision.SolidCollision(position, width, height))
                {
                    return center - new Vector2(0f, offset);
                }
            }

            return center;
        }

        public static void ApplyArenaTime()
        {
            if (!IsActive)
            {
                return;
            }

            bool midnight = ActiveIsPhase ? ActiveArena == 4 : ActiveArena == 5;
            bool dusk = ActiveIsPhase ? ActiveArena == 2 : ActiveArena == 4 || ActiveArena == 6;
            bool morning = !ActiveIsPhase && ActiveArena == 3;
            Main.dayTime = !midnight;
            Main.time = midnight ? 16200 : dusk ? Main.dayLength - 1800 : morning ? 5400 : 27000;
        }

        private static void UpdateSpawnPoint()
        {
            Point center = ActiveCenter.ToTileCoordinates();
            Main.spawnTileX = center.X;
            Main.spawnTileY = center.Y;
        }

        private static void SynchronizeArena()
        {
            if (IsActive && Main.netMode == NetmodeID.Server && !WorldGen.generatingWorld)
            {
                NetMessage.SendData(MessageID.WorldData);
            }
        }

        public override void OnWorldLoad()
        {
            ActiveIsPhase = false;
            ActiveArena = 1;
        }

        public override void OnWorldUnload()
        {
            ActiveIsPhase = false;
            ActiveArena = 1;
        }

        public override void PreUpdateTime()
        {
            ApplyArenaTime();
        }

        public override void ModifyTimeRate(ref double timeRate, ref double tileUpdateRate, ref double eventUpdateRate)
        {
            if (IsActive)
            {
                timeRate = 0;
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(ActiveIsPhase);
            writer.Write((byte)ActiveArena);
        }

        public override void NetReceive(BinaryReader reader)
        {
            ActiveIsPhase = reader.ReadBoolean();
            ActiveArena = Math.Clamp((int)reader.ReadByte(), 1, ActiveIsPhase ? 4 : 6);
            if (IsActive)
            {
                UpdateSpawnPoint();
                ApplyArenaTime();
            }
        }
    }
}
