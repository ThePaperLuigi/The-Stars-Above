using StarsAbove.Systems;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace StarsAbove.Subworlds
{
    public class GoddessOfMemorySubworld : Subworld
    {
        public static bool SupportsArenaStructures => ModLoader.TryGetMod("StructureHelper", out Mod structureHelper) && structureHelper.Version >= new Version(3, 1, 1);

        public override int Width => 6001;
        public override int Height => 1201;
        public override bool NormalUpdates => false;
        public override LocalizedText DisplayName => this.GetLocalization("DisplayName", () => "Goddess of Memory");

        public override List<GenPass> Tasks => new() { new PassLegacy("Subworld", SubworldGeneration) };

        private void SubworldGeneration(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "...";
            Main.worldSurface = Main.maxTilesY + 250;
            Main.rockLayer = Main.maxTilesY;
            SubworldSystem.hideUnderworld = false;

            if (!ModLoader.TryGetMod("StructureHelper", out Mod structureHelper) || structureHelper.Version < new Version(3, 1, 1))
            {
                throw new InvalidOperationException("The Goddess of Memory arenas require StructureHelper 3.1.1 or newer.");
            }

            Type generatorType = structureHelper.Code.GetType("StructureHelper.API.Generator");
            Type generationFlagsType = structureHelper.Code.GetType("StructureHelper.GenFlags");
            if (generatorType == null || generationFlagsType == null)
            {
                throw new InvalidOperationException("The Goddess of Memory arenas require StructureHelper 3.1.1 or newer.");
            }

            var generateStructure = generatorType.GetMethod("GenerateStructure", new[] { typeof(string), typeof(Point16), typeof(Mod), typeof(bool), typeof(bool), generationFlagsType });
            object generationFlags = Enum.ToObject(generationFlagsType, 0);
            var getDimensions = (Func<string, Mod, bool, Point16>)generatorType.GetMethod("GetStructureDimensions", new[] { typeof(string), typeof(Mod), typeof(bool) }).CreateDelegate(typeof(Func<string, Mod, bool, Point16>));

            for (int i = 1; i <= 10; i++)
            {
                bool isPhase = i > 6;
                int arena = isPhase ? i - 6 : i;
                string path = "Structures/GoddessOfMemory/" + (isPhase ? "GoMPhase" : "GoMIntermissionArena") + arena;
                Point16 dimensions = getDimensions(path, Mod, false);
                int expectedWidth = isPhase ? 299 : 99;
                if (dimensions.X != expectedWidth || dimensions.Y != 99)
                {
                    throw new InvalidOperationException($"{path} must be {expectedWidth} by 99 tiles.");
                }

                Point center = GoddessOfMemoryArenaSystem.GetArenaCenter(isPhase, arena).ToTileCoordinates();
                Point16 position = new Point16(center.X - dimensions.X / 2, center.Y - dimensions.Y / 2);
                generateStructure.Invoke(null, new object[] { path, position, Mod, false, false, generationFlags });
                progress.Set(i / 10f);
            }

            GoddessOfMemoryArenaSystem.ResetArena();
        }

        public override void OnEnter()
        {
            GoddessOfMemoryArenaSystem.ApplyArenaTime();
            Main.cloudAlpha = 0f;
            Main.resetClouds = true;
            Main.moonPhase = 4;
        }

        public override void OnLoad()
        {
            SubworldSystem.hideUnderworld = false;
            GoddessOfMemoryArenaSystem.ApplyArenaTime();
        }
    }
}
