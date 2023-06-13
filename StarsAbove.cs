using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using StarsAbove.SceneEffects.CustomSkies;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using Terraria.ID;
using StarsAbove.Items;
using ReLogic.Content.Sources;

using StarsAbove.Systems;

namespace StarsAbove
{

    public class StarsAbove : Mod
	{


		public static ModKeybind novaKey;
		public static ModKeybind weaponActionKey;

		public static bool sharedAudio;
		public static StarsAbove Instance { get; set; }
		
		public StarsAbove() => Instance = this;

		//Video player code sourced from Terraria Overhaul- I can't thank you enough!
		public override IContentSource CreateDefaultContentSource()
		{
			if(!Main.dedServ)
            {
				AddContent(new OgvReader());

			}

			return base.CreateDefaultContentSource();
		}

		public override void Load()
		{
			Logger.InfoFormat("'I've always wanted to write text in crash logs!' -A", Name);
			Logger.InfoFormat("'Eh? Doesn't that make you look like you caused it..?' -E", Name);

			ModLoader.TryGetMod("Wikithis", out Mod wikithis);
			if(wikithis != null && !Main.dedServ)
            {
				wikithis.Call("AddModURL", this, "terrariamods.wiki.gg$The_Stars_Above");
            }

			if (Main.netMode != NetmodeID.Server)
			{
				
				//GameShaders.Misc["StarsAbove:DeathAnimation"] = new MiscShaderData(new Ref<Effect>(ModContent.Request<Effect>("Effects/EffectDeath").Value), "DeathAnimation");
				Filters.Scene["StarsAbove:Void"] = new Filter(new ScreenShaderData("FilterMoonLord"), EffectPriority.Medium);
				SkyManager.Instance["StarsAbove:Void"] = new VoidSky();
				//Filters.Scene["StarsAbove:EverlastingLight"] = new Filter(new ScreenShaderData("FilterMoonLord"), EffectPriority.Low);
				SkyManager.Instance["StarsAbove:EverlastingLight"] = new EverlastingLightSky();
				SkyManager.Instance["StarsAbove:EverlastingLightPreview"] = new EverlastingLightSkyPreview();

				SkyManager.Instance["StarsAbove:CorvusSky"] = new CorvusSky();

				SkyManager.Instance["StarsAbove:ObservatorySkyDay"] = new ObservatorySkyDay();
				SkyManager.Instance["StarsAbove:EdinGenesisQuasarSky"] = new EdinGenesisQuasarSky();


				Filters.Scene["StarsAbove:MoonSky"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0f, 0.5f, 1f).UseOpacity(0.5f), EffectPriority.High);
				SkyManager.Instance["StarsAbove:MoonSky"] = new MoonSky();


				Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/ShockwaveEffect", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
				Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.High);
				Filters.Scene["Shockwave"].Load();

				//Ref<Effect> dyeRef = new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/Shader", AssetRequestMode.ImmediateLoad).Value);
				//GameShaders.Armor.BindShader(ModContent.ItemType<Spatial>(), new ArmorShaderData(dyeRef, "GalaxyPass")).UseImage("StarsAbove/Effects/GalaxyTest");

				GameShaders.Misc["StarsAbove:DeathAnimation"] = new MiscShaderData(
				  new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/EffectDeath", AssetRequestMode.ImmediateLoad).Value),
				  "DeathAnimation"
				);

			}

			novaKey = KeybindLoader.RegisterKeybind(this, "Stellar Nova", "Z");
			weaponActionKey = KeybindLoader.RegisterKeybind(this, "Weapon Action", "X");
		}
		public override void Unload()
		{
			
			novaKey = null;
			weaponActionKey = null;

			base.Unload();
		}

		

		public override object Call(params object[] args)
		{
			// Make sure the call doesn't include anything that could potentially cause exceptions.
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args), "Arguments cannot be null!");
			}

			if (args.Length == 0)
			{
				throw new ArgumentException("Arguments cannot be empty!");
			}

			// This check makes sure that the argument is a string using pattern matching.
			// Since we only need one parameter, we'll take only the first item in the array..
			if (args[0] is string content)
			{
				// ..And treat it as a command type.
				switch (content)
				{
					case "downedVagrant":
						return DownedBossSystem.downedVagrant;

					case "downedNalhaun":
						return DownedBossSystem.downedNalhaun;

					case "downedPenthesilea":
						return DownedBossSystem.downedPenth;

					case "downedArbitration":
						return DownedBossSystem.downedArbiter;

					case "downedWarriorOfLight":
						return DownedBossSystem.downedWarrior;

					case "downedDioskouroi":
						return DownedBossSystem.downedDioskouroi;

					case "downedTsukiyomi":
						return DownedBossSystem.downedTsuki;

				}
			}

			

			// If the arguments provided don't match anything we wanted to return a value for, we'll return a 'false' boolean.
			// This value can be anything you would like to provide as a default value.
			return false;
		}
		public override void PostSetupContent()
		{
			Mod bossChecklist;
			ModLoader.TryGetMod("BossChecklist", out bossChecklist);
			Mod musicDisplay;
			ModLoader.TryGetMod("MusicDisplay", out musicDisplay);
			if (bossChecklist != null)
			{
				
				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"$Mods.StarsAbove.NPCName.WarriorOfLight", //Boss Name
					ModContent.NPCType<NPCs.WarriorOfLight>(), //Boss ID
					18.1f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedWarrior), //Downed boolean
					() => true, //Availability
					new List<int> { ModContent.ItemType<Items.Prisms.PrismOfTheRuinedKing>(), ModContent.ItemType<Items.Prisms.PrismOfTheCosmicPhoenix>(), ModContent.ItemType<Items.Materials.DullTotemOfLight>(), ModContent.ItemType<Items.Materials.TotemOfLightEmpowered>() },//Collection
					ModContent.ItemType<Items.Consumables.ProgenitorWish>(),//Spawn Item
					"$Mods.StarsAbove.BossChecklist.WarriorOfLight.SpawnInfo", //Spawn Item
					"$Mods.StarsAbove.BossChecklist.WarriorOfLight.DespawnMessage" //Despawn Message
					); //Boss Portrait

				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"$Mods.StarsAbove.NPCName.VagrantOfSpaceAndTime", //Boss Name
					ModContent.NPCType<NPCs.Vagrant.VagrantBoss>(), //Boss ID
					7.1f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedVagrant), //Downed boolean
					() => true, //Availability
					new List<int> { ModContent.ItemType<Items.Materials.EnigmaticDust>(), ModContent.ItemType<Items.Prisms.SpatialPrism>() },//Collection
					ModContent.ItemType<Items.Consumables.ShatteredDisk>(),//Spawn Item
					"$Mods.StarsAbove.BossChecklist.VagrantOfSpaceAndTime.SpawnInfo", //Spawn Item
					"$Mods.StarsAbove.BossChecklist.VagrantOfSpaceAndTime.DespawnMessage", //Despawn Message
					(SpriteBatch sb, Rectangle rect, Color color) => {
						Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/PerseusBossChecklist").Value;
						Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
						sb.Draw(texture, centered, color);
					}); //Boss Portrait
				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"$Mods.StarsAbove.NPCName.Dioskouroi", //Boss Name
					ModContent.NPCType<NPCs.Dioskouroi.PolluxBoss>(), //Boss ID
					11.9f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedDioskouroi), //Downed boolean
					() => true, //Availability
					new List<int> { ModContent.ItemType<Items.Prisms.GeminiPrism>() },//Collection
					ModContent.ItemType<Items.Consumables.TwincruxPendant>(),//Spawn Item
					"$Mods.StarsAbove.BossChecklist.Dioskouroi.SpawnInfo", //Spawn Item
					"$Mods.StarsAbove.BossChecklist.Dioskouroi.DespawnMessage", //Despawn Message
					(SpriteBatch sb, Rectangle rect, Color color) => {
						Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/Dioskouroi_Bestiary").Value;
						Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
						sb.Draw(texture, centered, color);
					}); //Boss Portrait //Boss Portrait

				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"$Mods.StarsAbove.NPCName.NalhaunBoss", //Boss Name
					ModContent.NPCType<NPCs.Nalhaun.NalhaunBossPhase2>(), //Boss ID
					17.1f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedNalhaun), //Downed boolean
					() => true, //Availability
					new List<int> { ModContent.ItemType<Items.Prisms.BurnishedPrism>() },//Collection
					ModContent.ItemType<Items.Consumables.AncientShard>(),//Spawn Item
					"$Mods.StarsAbove.BossChecklist.Nalhaun.SpawnInfo", //Spawn Item
					"$Mods.StarsAbove.BossChecklist.Nalhaun.DespawnMessage", //Despawn Message
					(SpriteBatch sb, Rectangle rect, Color color) => {
						Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/Nalhaun_Bestiary").Value;
						Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
						sb.Draw(texture, centered, color);
					}); //Boss Portrait //Boss Portrait

				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"$Mods.StarsAbove.NPCName.Penthesilea", //Boss Name
					ModContent.NPCType<NPCs.Penthesilea>(), //Boss ID
					14.1f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedPenth), //Downed boolean
					() => true, //Availability
					new List<int> {ModContent.ItemType<Items.Prisms.PaintedPrism>() },//Collection
					ModContent.ItemType<Items.Consumables.UnsulliedCanvas>(),//Spawn Item
					"$Mods.StarsAbove.BossChecklist.Penthesilea.SpawnInfo", //Spawn Item
					"$Mods.StarsAbove.BossChecklist.Penthesilea.DespawnMessage" //Despawn Message
					); //Boss Portrait

				/*bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"$Mods.StarsAbove.NPCName.Arbitration", //Boss Name
					ModContent.NPCType<NPCs.Arbitration>(), //Boss ID
					15.5f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedArbiter), //Downed boolean
					() => true, //Availability
					new List<int> { ModContent.ItemType<Items.Prisms.VoidsentPrism>() },//Collection
					ModContent.ItemType<Items.Consumables.DemonicCrux>(),//Spawn Item
					"$Mods.StarsAbove.BossChecklist.Arbitration.SpawnInfo", //Spawn Item
					"$Mods.StarsAbove.BossChecklist.Arbitration.DespawnMessage" //Despawn Message
					); //Boss Portrait
				*/
				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"$Mods.StarsAbove.NPCName.TsukiyomiBoss", //Boss Name
					ModContent.NPCType<NPCs.Tsukiyomi.TsukiyomiBoss>(), //Boss ID
					18.3f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedTsuki), //Downed boolean
					(Func<bool>)(() => DownedBossSystem.downedWarrior), //Availability
					new List<int> { ModContent.ItemType<Items.Consumables.SpatialMemoriam>() },//Collection
					ModContent.ItemType<Items.Consumables.MnemonicSigil>(),//Spawn Item
					"$Mods.StarsAbove.BossChecklist.Tsukiyomi2.SpawnInfo", //Spawn Item
					"$Mods.StarsAbove.BossChecklist.Tsukiyomi2.DespawnMessage" //Despawn Message
					); //Boss Portrait

			}
			if (musicDisplay != null)
            {
				void AddMusic(string path, string name) => musicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(this, path), name, "The Stars Above");

				AddMusic("Sounds/Music/CosmicWill", "PaperLuigi - Cosmic Will (Stars Above OST)");
				AddMusic("Sounds/Music/SunsetStardust", "PaperLuigi - Sunset, Stardust (Stars Above OST)");

				AddMusic("Sounds/Music/ElpisDay", "Masayoshi Soken - Sky Unsundered (FFXIV Endwalker OST)");
				AddMusic("Sounds/Music/MareLamentorum", "Masayoshi Soken - One Small Step (FFXIV Endwalker OST)");
				AddMusic("Sounds/Music/EverlastingLight", "Masayoshi Soken - Unmatching Pieces (FFXIV Endwalker OST)");
				AddMusic("Sounds/Music/ToTheEdge", "Masayoshi Soken - To The Edge (FFXIV Shadowbringers OST)");

				AddMusic("Sounds/Music/MageOfViolet", "Murasaki Shion - Mage of Violet (Instrumental)");

				AddMusic("Sounds/Music/ShadowsCastByTheMighty", "REVO - Shadows Cast By The Mighty: Swift Judgement (Bravely Default 2 OST)");
				AddMusic("Sounds/Music/TheMightOfTheHellblade", "REVO - The Might of The Hellblade (Bravely Default 2 OST)");

				AddMusic("Sounds/Music/FirstWarning", "Studio EIM - First Warning (LoR ver.) (Library of Ruina OST)");
				AddMusic("Sounds/Music/SecondWarning", "Studio EIM - Second Warning (LoR ver.) (Library of Ruina OST)");

			}
		}
		
		/*public override void UpdateMusic(ref int music, ref MusicPriority priority) Will be replaced by SceneEffects.
		{
			if (Main.myPlayer != -1 && !Main.gameMenu)
			{
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].GetModPlayer<StarsAbovePlayer>().stellarPerformanceActive == true)
				{
					
					

				}
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].HasBuff(ModContent.BuffType<Buffs.StellarListener>()) && !sharedAudio)
				{

					music = MusicLoader.GetMusicSlot(this, "Sounds/Music/NextColorPlanet");
					priority = MusicPriority.BossLow;

				}
				if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].HasBuff(ModContent.BuffType<Buffs.EverlastingLight>()))
				{

					music = MusicLoader.GetMusicSlot(this, "Sounds/Music/EverlastingLight");
					priority = MusicPriority.Event;

				}
				
				
				if (Main.player[Main.myPlayer].active && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SeaOfStars)
				{
					music = MusicLoader.GetMusicSlot(this, "Sounds/Music/MareLamentorum");
					priority = MusicPriority.BiomeHigh;
				}
				if (Main.player[Main.myPlayer].active && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().Observatory)
				{
					if (MusicMod != null)
					{

						music = MusicLoader.GetMusicSlot(this, "Sounds/Music/ElpisDay");
						priority = MusicPriority.BiomeHigh;
					}
					else
					{
						music = MusicLoader.GetMusicSlot(this, "Sounds/Music/MareLamentorum");
						priority = MusicPriority.BiomeHigh;
					}
					
				}
				if (Main.player[Main.myPlayer].active && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BleachedWorld)
				{

					music = MusicLoader.GetMusicSlot(this, "Sounds/Music/MareLamentorum");
					priority = MusicPriority.BiomeHigh;

				}
				if (Main.player[Main.myPlayer].active && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().City)
				{
					if (MusicMod != null)
					{

						music = MusicLoader.GetMusicSlot(this, "Sounds/Music/ACYBERSWORLD");
						priority = MusicPriority.BiomeHigh;
					}
					else
					{
						music = MusicLoader.GetMusicSlot(this, "Sounds/Music/MareLamentorum");
						priority = MusicPriority.BiomeHigh;
					}

				}
			}

			base.UpdateMusic(ref music, ref priority);
		}*/
		
		
	}
}