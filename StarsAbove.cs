
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using StarsAbove.UI;
using StarsAbove.UI.CosmicDestroyerGauge;
using StarsAbove.UI.Hawkmoon;
using StarsAbove.UI.JudgementGauge;
using StarsAbove.UI.StarfarerMenu;
using StarsAbove.UI.Starfarers;
using StarsAbove.UI.StellarNova;
using StarsAbove.UI.Undertale;
using StarsAbove.UI.VN;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using StarsAbove.SceneEffects.CustomSkies;
using ReLogic.Content;

namespace StarsAbove
{

	public class StarsAbove : Mod
	{


		public static ModKeybind novaKey;
		public static ModKeybind weaponActionKey;

		public static bool sharedAudio;

	


		public static StarsAbove Instance { get; set; }
		
		public StarsAbove() => Instance = this;

		
		public override void Load()
		{
			Logger.InfoFormat("'I've always wanted to write text in crash logs!' -A", Name);
			Logger.InfoFormat("'Eh? Doesn't that make you look like you caused it..?' -E", Name);

			ModLoader.TryGetMod("Wikithis", out Mod wikithis);
			if(wikithis != null && !Main.dedServ)
            {
				wikithis.Call("AddModURL", this, "terrariamods.fandom.com$The_Stars_Above");
            }

			if (!Main.dedServ)
			{
				
				//GameShaders.Misc["StarsAbove:DeathAnimation"] = new MiscShaderData(new Ref<Effect>(ModContent.Request<Effect>("Effects/EffectDeath").Value), "DeathAnimation");
				Filters.Scene["StarsAbove:Void"] = new Filter(new ScreenShaderData("FilterMoonLord"), EffectPriority.Medium);
				SkyManager.Instance["StarsAbove:Void"] = new VoidSky();
				//Filters.Scene["StarsAbove:EverlastingLight"] = new Filter(new ScreenShaderData("FilterMoonLord"), EffectPriority.Low);
				SkyManager.Instance["StarsAbove:EverlastingLight"] = new EverlastingLightSky();
				SkyManager.Instance["StarsAbove:ObservatorySkyDay"] = new ObservatorySkyDay();
				SkyManager.Instance["StarsAbove:ObservatorySkyNight"] = new ObservatorySkyNight();


				Filters.Scene["StarsAbove:MoonSky"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0f, 0.5f, 1f).UseOpacity(0.5f), EffectPriority.High);
				SkyManager.Instance["StarsAbove:MoonSky"] = new MoonSky();


				Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/ShockwaveEffect", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
				Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
				Filters.Scene["Shockwave"].Load();



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
			
			if (bossChecklist != null)
			{
				
				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"The Warrior of Light", //Boss Name
					ModContent.NPCType<NPCs.WarriorOfLight>(), //Boss ID
					18.5f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedWarrior), //Downed boolean
					() => true, //Availability
					new List<int> { ModContent.ItemType<Items.Prisms.PrismOfTheRuinedKing>(), ModContent.ItemType<Items.Prisms.PrismOfTheCosmicPhoenix>(), ModContent.ItemType<Items.Materials.DullTotemOfLight>(), ModContent.ItemType<Items.Materials.TotemOfLightEmpowered>() },//Collection
					ModContent.ItemType<Items.Consumables.ProgenitorWish>(),//Spawn Item
					"Dispel the unending Light through defeating this foe, summoned with your Starfarer's help from the Moon Lord's corpse. (The Vagrant of Space and Time must have been defeated.)", //Spawn Item
					"The Flood of Light has consumed all..." //Despawn Message
					); //Boss Portrait

				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"The Vagrant of Space and Time", //Boss Name
					ModContent.NPCType<NPCs.VagrantOfSpaceAndTime>(), //Boss ID
					7.1f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedVagrant), //Downed boolean
					() => true, //Availability
					new List<int> { ModContent.ItemType<Items.Materials.EnigmaticDust>(), ModContent.ItemType<Items.Prisms.SpatialPrism>() },//Collection
					ModContent.ItemType<Items.Consumables.ShatteredDisk>(),//Spawn Item
					"Defeat this strange foe through the Shattered Disk, obtained from the Wall of Flesh. This fight will be critical to progression later!", //Spawn Item
					"Space and time return to normal..." //Despawn Message
					); //Boss Portrait

				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"Nalhaun, the Burnished King", //Boss Name
					ModContent.NPCType<NPCs.Nalhaun>(), //Boss ID
					11.5f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedNalhaun), //Downed boolean
					() => true, //Availability
					new List<int> { ModContent.ItemType<Items.Prisms.BurnishedPrism>() },//Collection
					ModContent.ItemType<Items.Consumables.AncientShard>(),//Spawn Item
					"Confront this aged monarch with the Ancient Shard, given to you by your Starfarer after any Mechanical Boss is defeated. (The Vagrant of Space and Time must have been defeated.)", //Spawn Item
					"The king's crimson blade rings true yet again..." //Despawn Message
					); //Boss Portrait

				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"Penthesilea, the Witch of Ink", //Boss Name
					ModContent.NPCType<NPCs.Penthesilea>(), //Boss ID
					12.5f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedPenth), //Downed boolean
					() => true, //Availability
					new List<int> {ModContent.ItemType<Items.Prisms.PaintedPrism>() },//Collection
					ModContent.ItemType<Items.Consumables.UnsulliedCanvas>(),//Spawn Item
					"Face this fearsome foe with the Unsullied Canvas, given to you by your Starfarer after Plantera is bested. (The Vagrant of Space and Time must have been defeated.)", //Spawn Item
					"You have been splattered away..." //Despawn Message
					); //Boss Portrait

				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"Arbitration", //Boss Name
					ModContent.NPCType<NPCs.Arbitration>(), //Boss ID
					15.5f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedArbiter), //Downed boolean
					() => true, //Availability
					new List<int> { ModContent.ItemType<Items.Prisms.VoidsentPrism>() },//Collection
					ModContent.ItemType<Items.Consumables.DemonicCrux>(),//Spawn Item
					"This foe embodies true fear... Obtain the Crux from your Starfarer after Golem is defeated. (The Vagrant of Space and Time must have been defeated.)", //Spawn Item
					"Chaos and order intertwined hath consumed you whole." //Despawn Message
					); //Boss Portrait

				bossChecklist.Call(
					"AddBoss", //Entry Type
					this, //Mod Instance
					"Tsukiyomi, the First Starfarer", //Boss Name
					ModContent.NPCType<NPCs.Tsukiyomi2>(), //Boss ID
					18.9f, //Progression
					(Func<bool>)(() => DownedBossSystem.downedTsuki), //Downed boolean
					(Func<bool>)(() => DownedBossSystem.downedWarrior), //Availability
					new List<int> { ModContent.ItemType<Items.Consumables.SpatialMemoriam>() },//Collection
					ModContent.ItemType<Items.Consumables.MnemonicSigil>(),//Spawn Item
					"The time has finally come. Craft all the Mnemonic Traces together to create the Mnemonic Sigil, and use it within the Observatory Hyperborea to be taken to Tsukiyomi's boss arena.", //Spawn Item
					"..." //Despawn Message
					); //Boss Portrait

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