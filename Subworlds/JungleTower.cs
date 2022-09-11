using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using StarsAbove;
using StarsAbove.Items;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using StarsAbove.NPCs;
using Microsoft.Xna.Framework.Audio;

using StarsAbove.Dusts;
using StarsAbove.Items.Consumables;
using System.Diagnostics.Contracts;
using StarsAbove.Items.Prisms;
using StarsAbove.UI.StellarNova;
using SubworldLibrary;
using StarsAbove.Projectiles.UltimaThule;
using StarsAbove.Projectiles.Otherworld;
using Terraria.GameContent.Generation;

namespace StarsAbove
{
	public class JungleTower : Subworld
	{
		public override int Width => 1750;
		public override int Height => 750;

		//public override ModWorld modWorld => ModContent.GetInstance < your modworld here>();

		public override bool ShouldSave => false;
		public override bool NoPlayerSaving => false;
		public override bool NormalUpdates => false;

		public int variantWorld;

		public override List<GenPass> Tasks => new List<GenPass>()
		{
			new PassLegacy("The Jungle Tower", (progress, _) =>
			{
				progress.Message = "Loading"; //Sets the text above the worldgen progress bar
				Main.worldSurface = Main.maxTilesY + 250; //Hides the underground layer just out of bounds
				Main.rockLayer = Main.maxTilesY; //Hides the cavern layer way out of bounds

				//variantWorld = Main.rand.Next(3);

				

				


				for (int i = 0; i < Main.maxTilesX; i++)
				{
					for (int j = 0; j < Main.maxTilesY; j++)
					{

						
						progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY)); //Controls the progress bar, should only be set between 0f and 1f
						//Main.tile[i, j].active(true);
						//Main.tile[i, j].type = TileID.Air;
					}
					
					if(i == Main.maxTilesX/2)
					{
						StructureHelper.Generator.GenerateStructure("Structures/JungleTower", new Terraria.DataStructures.Point16((Main.maxTilesX/2) - 55, (Main.maxTilesY/2) - 110), StarsAbove.Instance);
					}
					
				}
			})
		};

		public override void Load()
		{
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					player.AddBuff(BuffType<Buffs.Wormhole>(), 20);  //Make sure to replace "buffType" and "timeInFrames" with actual values
					
					
				}


			}
			Main.dayTime = false;
			Main.time = 6000;
			Main.cloudAlpha = 0f;
			Main.resetClouds = true;
			Main.moonPhase = 4;
			SubworldSystem.noReturn = true;


		}
	}
}
