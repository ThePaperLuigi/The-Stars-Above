
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Subworlds;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove
{
	public class StarsAboveEnterSubworldTestCommand : ModCommand
	{
		public override string Command => "tsatestenter";

		public override CommandType Type => CommandType.Chat;

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			
			SubworldSystem.Enter<Observatory>();
			
		}



	}
	public class StarsAboveEnterSubworldExitCommand : ModCommand
	{
		public override string Command => "tsatestexit";

		public override CommandType Type => CommandType.Chat;

		public override void Action(CommandCaller caller, string input, string[] args)
		{

			SubworldSystem.Exit();

		}



	}
}
