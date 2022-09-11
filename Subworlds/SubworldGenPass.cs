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
using Terraria.IO;

namespace StarsAbove
{
	public class SubworldGenPass : GenPass
	{
		private Action<GenerationProgress> method;

		public SubworldGenPass(Action<GenerationProgress> method)
			: base("", 1f)
		{
			this.method = method;
		}

		public SubworldGenPass(float weight, Action<GenerationProgress> method)
			: base("", weight)
		{
			this.method = method;
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			method(progress);
		}
	}
}
