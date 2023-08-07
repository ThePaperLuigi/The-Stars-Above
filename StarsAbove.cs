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
using StarsAbove.NPCs.WarriorOfLight;
using StarsAbove.NPCs.Vagrant;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.NPCs.Dioskouroi;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using StarsAbove.UI.Starfarers;
using System.IO;
using SubworldLibrary;

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

				var MiscEffect = new Ref<Effect>(Assets.Request<Effect>("Effects/MiscEffect", AssetRequestMode.ImmediateLoad).Value);
				GameShaders.Misc["CyclePass"] = new MiscShaderData(MiscEffect, "CyclePass");

				GameShaders.Misc["StarsAbove:DeathAnimation"] = new MiscShaderData(
				  new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/EffectDeath", AssetRequestMode.ImmediateLoad).Value),
				  "DeathAnimation"
				);

				//Shelved for later. Doesn't work yet.
				//Terraria.GameContent.UI.Elements.On_UICharacterListItem.ctor += Hook_UICharacterList;
			}

			novaKey = KeybindLoader.RegisterKeybind(this, "Stellar Nova", "Z");
			weaponActionKey = KeybindLoader.RegisterKeybind(this, "Weapon Action", "X");
		}
		private void Hook_UICharacterList(On_UICharacterListItem.orig_ctor orig, UICharacterListItem self, PlayerFileData data, int snapPointIndex)
		{        //Thank you to tMod discord member pure_epic

			orig(self, data, snapPointIndex);
			if (data.Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
			{
				MainMenuIcon Icon = new MainMenuIcon();
				self.Append(Icon);
			}
			else if (data.Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
			{
				self.Append(MainMenuIcon.umbral);
			}
		}
		public override void Unload()
		{
			
			novaKey = null;
			weaponActionKey = null;

			base.Unload();
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			SubworldSystem.MovePlayerToSubworld("Observatory", whoAmI);

			byte msgType = reader.ReadByte();
			switch (msgType)
			{
				case 0://Attempting to enter a suwbworld

					string id = reader.ReadString();
					SubworldSystem.MovePlayerToSubworld("StarsAbove/" + id, whoAmI);

					/*
					switch (id)
					{
						case "Observatory":
							break;
						case "Test":
							break;
						default:
							Logger.WarnFormat("StarsAbove: Unknown Subworld");
							break;
					}*/

					break;
				default:
					Logger.WarnFormat("StarsAbove: Unknown Message type: {0}", msgType);
					break;
			}
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
			Mod recipeBrowser;
			ModLoader.TryGetMod("RecipeBrowser", out recipeBrowser);
			Mod musicDisplay;
			ModLoader.TryGetMod("MusicDisplay", out musicDisplay);

			Func<bool> CanSeeTsuki = () =>
			{
				bool WarriorDowned = DownedBossSystem.downedWarrior;

				bool canSeeBoss = WarriorDowned;

				return canSeeBoss;
			};
			/*
			if (recipeBrowser != null && !Main.dedServ)
			{
				recipeBrowser.Call(new object[5]
				{
				"AddItemCategory",
				"Astral",
				"Weapons",
				ModContent.Request<Texture2D>("StarsAbove/Items/Astral"), // 24x24 icon
				(Predicate<Item>)((Item item) =>
				{
				if (item.damage > 0)
				{
					return item.ModItem is Astral;
				}
				return false;
				})
				});
			}*/
			if (bossChecklist != null)
			{
				//Vagrant of Space and Time
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(VagrantBoss),
					1.9f,
					() => DownedBossSystem.downedVagrant,
					ModContent.NPCType<VagrantBoss>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.ShatteredDisk>(),
						["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) =>
						{
							Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/PerseusBossChecklist").Value;
							Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
							sb.Draw(texture, centered, color);
						}

					}
				);
				//Dioskouroi
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(PolluxBoss),
					9.5f,
					() => DownedBossSystem.downedDioskouroi,
					ModContent.NPCType<PolluxBoss>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.ShatteredDisk>(),
						["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) =>
						{
							Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/Dioskouroi_Bestiary").Value;
							Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
							sb.Draw(texture, centered, color);
						}
					}

				);
				//Penthesilea
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(Penthesilea),
					13.5f,
					() => DownedBossSystem.downedPenth,
					ModContent.NPCType<Penthesilea>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.UnsulliedCanvas>(),
					}
				);
				//Nalhaun
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(NalhaunBossPhase2),
					14.5f,
					() => DownedBossSystem.downedNalhaun,
					ModContent.NPCType<NalhaunBossPhase2>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.AncientShard>(),
						["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) =>
						{
							Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/Nalhaun_Bestiary").Value;
							Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
							sb.Draw(texture, centered, color);
						}
					}
				);
				//Arbitration
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(Arbitration),
					15.1f,
					() => DownedBossSystem.downedArbiter,
					ModContent.NPCType<Arbitration>(),
					new Dictionary<string, object>()
					{

					}
				);
				//The Warrior of Light
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(WarriorOfLightBoss),
					18.1f,
					() => DownedBossSystem.downedWarrior,
					ModContent.NPCType<WarriorOfLightBossFinalPhase>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.ProgenitorWish>(),
						["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) =>
						{
							Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/WarriorOfLight_Bestiary").Value;
							Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
							sb.Draw(texture, centered, color);
						}
					}
				);
				//Tsukiyomi
				bossChecklist.Call(
					"LogBoss",
					this,
					nameof(TsukiyomiBoss),
					18.3f,
					() => DownedBossSystem.downedTsuki,
					ModContent.NPCType<TsukiyomiBoss>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.MnemonicSigil>(),
						["availability"] = CanSeeTsuki,

					}
				);
			}
			if (musicDisplay != null)
            {
				/*
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
				*/
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