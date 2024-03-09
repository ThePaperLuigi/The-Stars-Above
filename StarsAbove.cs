global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Terraria;
global using Terraria.Localization;
global using Terraria.ModLoader;

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
using StarsAbove.NPCs.Arbitration;
using StarsAbove.NPCs.Penthesilea;
using StarsAbove.NPCs.Thespian;
using StarsAbove.NPCs.Starfarers;
using MonoMod.Cil;
using Terraria.GameContent;
using StarsAbove.Biomes;
using System.Reflection;
using Terraria.GameContent.Skies;
using Terraria.Utilities;
using Terraria.Graphics.Light;

namespace StarsAbove
{

    public class StarsAbove : Mod
	{


		public static ModKeybind novaKey;
		public static ModKeybind weaponActionKey;
		public static ModKeybind showMemoryInfoKey;
		public static ModKeybind weaponMemoryKey;

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
				
				wikithis.Call("AddModURL", this, "https://terrariamods.wiki.gg/wiki/The_Stars_Above/{}");

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

				SkyManager.Instance["StarsAbove:DreamingCitySky"] = new DreamingCitySky();
				SkyManager.Instance["StarsAbove:EdinGenesisQuasarSky"] = new EdinGenesisQuasarSky();
                SkyManager.Instance["StarsAbove:ArbiterSky"] = new ArbiterSky();

                Filters.Scene["StarsAbove:MoonSky"] = new Filter(new ScreenShaderData("FilterTower").UseColor(0f, 0.5f, 1f).UseOpacity(0.5f), EffectPriority.High);
				SkyManager.Instance["StarsAbove:MoonSky"] = new MoonSky();

				Ref<Effect> blurRef = new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/GaussianBlur", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
				Filters.Scene["GaussianBlur"] = new Filter(new ScreenShaderData(blurRef, "Test"), EffectPriority.High);
				Filters.Scene["GaussianBlur"].Load();

                Ref<Effect> neonVeilRef = new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/NeonVeilReflectionEffect", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                Filters.Scene["NeonVeilReflectionEffect"] = new Filter(new ScreenShaderData(neonVeilRef, "Test"), EffectPriority.High);
                Filters.Scene["NeonVeilReflectionEffect"].Load();

                //Ref<Effect> neonVeilBlurRef = new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/NeonVeilBlurEffect", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                //Filters.Scene["NeonVeilBlurEffect"] = new Filter(new ScreenShaderData(neonVeilBlurRef, "Test"), EffectPriority.High);
                //Filters.Scene["NeonVeilBlurEffect"].Load();

                Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/ShockwaveEffect", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
				Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.High);
				Filters.Scene["Shockwave"].Load();

				var MiscEffect = new Ref<Effect>(Assets.Request<Effect>("Effects/MiscEffect", AssetRequestMode.ImmediateLoad).Value);
				GameShaders.Misc["CyclePass"] = new MiscShaderData(MiscEffect, "CyclePass");

				var AdditiveBlendEffect = new Ref<Effect>(Assets.Request<Effect>("Effects/AdditiveBlend", AssetRequestMode.ImmediateLoad).Value);
				GameShaders.Misc["BlendPass"] = new MiscShaderData(AdditiveBlendEffect, "BlendPass");

				GameShaders.Misc["StarsAbove:DeathAnimation"] = new MiscShaderData(
				  new Ref<Effect>(ModContent.Request<Effect>("StarsAbove/Effects/EffectDeath", AssetRequestMode.ImmediateLoad).Value),
				  "DeathAnimation"
				);

				//Shelved for later. Doesn't work yet.
				//Terraria.GameContent.UI.Elements.On_UICharacterListItem.ctor += Hook_UICharacterList;
			}
            IL_Main.DrawBG += UWBGInsert;
            IL_Main.DrawCapture += UWBGInsertCapture;
            On_AmbientSky.HellBatsGoupSkyEntity.ctor += HellBatsGoupSkyEntity_ctor;
            Terraria.IL_Player.UpdateBiomes += HeatRemoval;
            Terraria.Graphics.Light.On_TileLightScanner.ApplyHellLight += TileLightScanner_ApplyHellLight;

            novaKey = KeybindLoader.RegisterKeybind(this, "Stellar Nova", "Z");
			weaponActionKey = KeybindLoader.RegisterKeybind(this, "Weapon Action", "X");
			weaponMemoryKey = KeybindLoader.RegisterKeybind(this, "Weapon Memory Action", "C");
			showMemoryInfoKey = KeybindLoader.RegisterKeybind(this, "Show Memory Info", "V");

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
            Terraria.IL_Player.UpdateBiomes -= HeatRemoval;

            IL_Main.DrawBG -= UWBGInsert;
            IL_Main.DrawCapture -= UWBGInsertCapture;

            On_AmbientSky.HellBatsGoupSkyEntity.ctor -= HellBatsGoupSkyEntity_ctor;
            Terraria.Graphics.Light.On_TileLightScanner.ApplyHellLight -= TileLightScanner_ApplyHellLight;

            novaKey = null;
			weaponActionKey = null;

			base.Unload();
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			//SubworldSystem.MovePlayerToSubworld("Observatory", whoAmI);

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

                    case "downedThespian":
                        return DownedBossSystem.downedThespian;

                    case "downedStarfarers":
                        return DownedBossSystem.downedStarfarers;

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
            Func<bool> CanSeeStarfarers = () =>
            {
                bool PenthDowned = DownedBossSystem.downedPenth;
                bool canSeeBoss = false;
                if (PenthDowned && NPC.downedPlantBoss)
				{
					canSeeBoss = true;
				}
                

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
					2.9f,
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
                //Thespian
                bossChecklist.Call(
                    "LogBoss",
                    this,
                    nameof(ThespianBoss),
                    3.5f,
                    () => DownedBossSystem.downedThespian,
                    ModContent.NPCType<ThespianBoss>(),
                    new Dictionary<string, object>()
                    {
                        ["spawnItems"] = ModContent.ItemType<Items.Consumables.MalsaineDraught>(),
                    }
                );
                //Dioskouroi
                bossChecklist.Call(
					"LogBoss",
					this,
					nameof(PolluxBoss),
					6.5f,
					() => DownedBossSystem.downedDioskouroi,
					ModContent.NPCType<PolluxBoss>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.TwincruxPendant>(),
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
					nameof(PenthesileaBoss),
					11.5f,
					() => DownedBossSystem.downedPenth,
					ModContent.NPCType<PenthesileaBoss>(),
					new Dictionary<string, object>()
					{
						["spawnItems"] = ModContent.ItemType<Items.Consumables.UnsulliedCanvas>(),
					}
				);
				//The Starfarers
                bossChecklist.Call(
                    "LogBoss",
                    this,
                    nameof(StarfarerBoss),
                    12.5f,
                    () => DownedBossSystem.downedStarfarers,
                    ModContent.NPCType<StarfarerBoss>(),
                    new Dictionary<string, object>()
                    {
                        ["spawnItems"] = ModContent.ItemType<Items.Consumables.StarfarerEssence>(),
                        ["availability"] = CanSeeStarfarers,
                        ["customPortrait"] = (SpriteBatch sb, Rectangle rect, Color color) =>
                        {
                            Texture2D texture = ModContent.Request<Texture2D>("StarsAbove/Bestiary/StarfarerBoss_Bestiary").Value;
                            Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                            sb.Draw(texture, centered, color);
                        }
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
					nameof(ArbitrationBoss),
					17.9f,
					() => DownedBossSystem.downedArbiter,
					ModContent.NPCType<ArbitrationBoss>(),
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
            if (!Main.dedServ)
            {
                UWBGTexture[0] = new Asset<Texture2D>[14];
                UWBGTexture[1] = new Asset<Texture2D>[14];
                for (int i = 0; i < 14; i++)
                {
                    UWBGTexture[0][i] = TextureAssets.Underworld[i];
                    UWBGTexture[1][i] = ModContent.Request<Texture2D>("StarsAbove/Backgrounds/NeonVeil/NeonVeilBG" + i);
                }
                UWBGBottomColor[0] = new Color(11, 3, 7);
                UWBGBottomColor[1] = new Color(0, 0, 0);
            }
        }

        //Code taken from the Depths mod, thank you!
        #region DepthsBackgroundILEdit
		//Put here for organizational purposes
        private static float[] UWBGAlpha = new float[2];
        private static int UWBGStyle;
        private Color[] UWBGBottomColor = new Color[2];
        private Asset<Texture2D>[][] UWBGTexture = new Asset<Texture2D>[2][];

        private void UWBGInsert(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(MoveType.After, i => i.MatchLdarg0(), i => i.MatchLdcI4(0), i => i.MatchCall<Main>("DrawUnderworldBackground"));
            c.EmitDelegate(() => {
                DrawUnderworldBackground(false);
            });
        }

        private void UWBGInsertCapture(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.GotoNext(MoveType.After, i => i.MatchLdarg0(), i => i.MatchLdcI4(1), i => i.MatchCall<Main>("DrawUnderworldBackground"));
            c.EmitDelegate(() => {
                DrawUnderworldBackground(true);
            });
        }

        public int UnderworldStyleCalc()
        {
            if (ModContent.GetInstance<NeonVeilTileCount>().tileCount >= 40)
            {
                return 1;
            }
            return 0;
        }

        protected void DrawUnderworldBackground(bool flat)
        {
            if (!(Main.screenPosition.Y + (float)Main.screenHeight < (float)(Main.maxTilesY - 220) * 16f))
            {
                UWBGStyle = UnderworldStyleCalc();
                for (var i = 0; i < 2; i++)
                {
                    if (UWBGStyle != i)
                    {
                        UWBGAlpha[i] = Math.Max(UWBGAlpha[i] - 0.05f, 0f);
                    }
                    else
                    {
                        UWBGAlpha[i] = Math.Min(UWBGAlpha[i] + 0.05f, 1f);
                    }
                }
                Vector2 screenOffset = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
                float pushUp = (Main.GameViewMatrix.Zoom.Y - 1f) * 0.5f * 200f;
                SkyManager.Instance.ResetDepthTracker();
                for (int num = 4; num >= 0; num--)
                {
                    bool flag = false;
                    for (int j = 0; j < 2; j++)
                    {
                        if (UWBGAlpha[j] > 0f && j != UWBGStyle)
                        {
                            DrawUnderworldBackgroudLayer(flat, screenOffset, pushUp, num, j, flat ? 1f : UWBGAlpha[j]);
                            flag = true;
                        }
                    }
                    DrawUnderworldBackgroudLayer(flat, screenOffset, pushUp, num, UWBGStyle, flag ? UWBGAlpha[UWBGStyle] : 1f);
                }
                if (!Main.mapFullscreen)
                {
                    SkyManager.Instance.DrawRemainingDepth(Main.spriteBatch);
                }
                //Main.DrawSurfaceBG_DrawChangeOverlay(12);
            }
        }

        private void DrawUnderworldBackgroudLayer(bool flat, Vector2 screenOffset, float pushUp, int layerTextureIndex, int Style, float Alpha)
        {
            if (Style == 0)
            {
                return;
            }
            int num = Main.underworldBG[layerTextureIndex];
            Asset<Texture2D> asset = UWBGTexture[Style][num];
            if (!asset.IsLoaded)
            {
                Main.Assets.Request<Texture2D>(asset.Name);
            }
            Texture2D value = asset.Value;
            Vector2 vec = new Vector2((float)value.Width, (float)value.Height) * 0.5f;
            float num7 = (flat ? 1f : ((float)(layerTextureIndex * 2) + 3f));
            Vector2 vector = new(1f / num7);
            Rectangle value2 = new(0, 0, value.Width, value.Height);
            float num8 = 1.3f;
            Vector2 zero = Vector2.Zero;
            int num9 = 0;
            switch (num)
            {
                case 1:
                    {
                        int num14 = (int)(Main.GlobalTimeWrappedHourly * 8f) % 4;
                        value2 = new((num14 >> 1) * (value.Width >> 1), num14 % 2 * (value.Height >> 1), value.Width >> 1, value.Height >> 1);
                        vec *= 0.5f;
                        zero.Y += 175f;
                        break;
                    }
                case 2:
                    zero.Y += 100f;
                    break;
                case 3:
                    zero.Y += 75f;
                    break;
                case 4:
                    num8 = 0.5f;
                    zero.Y -= 0f;
                    break;
                case 5:
                    zero.Y += num9;
                    break;
                case 6:
                    {
                        int num13 = (int)(Main.GlobalTimeWrappedHourly * 8f) % 4;
                        value2 = new(num13 % 2 * (value.Width >> 1), (num13 >> 1) * (value.Height >> 1), value.Width >> 1, value.Height >> 1);
                        vec *= 0.5f;
                        zero.Y += num9;
                        zero.Y += -60f;
                        break;
                    }
                case 7:
                    {
                        int num12 = (int)(Main.GlobalTimeWrappedHourly * 8f) % 4;
                        value2 = new(num12 % 2 * (value.Width >> 1), (num12 >> 1) * (value.Height >> 1), value.Width >> 1, value.Height >> 1);
                        vec *= 0.5f;
                        zero.Y += num9;
                        zero.X -= 400f;
                        zero.Y += 90f;
                        break;
                    }
                case 8:
                    {
                        int num11 = (int)(Main.GlobalTimeWrappedHourly * 8f) % 4;
                        value2 = new(num11 % 2 * (value.Width >> 1), (num11 >> 1) * (value.Height >> 1), value.Width >> 1, value.Height >> 1);
                        vec *= 0.5f;
                        zero.Y += num9;
                        zero.Y += 90f;
                        break;
                    }
                case 9:
                    zero.Y += num9;
                    zero.Y -= 30f;
                    break;
                case 10:
                    zero.Y += 250f * num7;
                    break;
                case 11:
                    zero.Y += 100f * num7;
                    break;
                case 12:
                    zero.Y += 20f * num7;
                    break;
                case 13:
                    {
                        zero.Y += 20f * num7;
                        int num10 = (int)(Main.GlobalTimeWrappedHourly * 8f) % 4;
                        value2 = new(num10 % 2 * (value.Width >> 1), (num10 >> 1) * (value.Height >> 1), value.Width >> 1, value.Height >> 1);
                        vec *= 0.5f;
                        break;
                    }
            }
            if (flat)
            {
                num8 *= 1.5f;
            }
            vec *= num8;
            SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1f / vector.X);
            if (flat)
            {
                zero.Y += (float)(UWBGTexture[Style][0].Height() >> 1) * 1.3f - vec.Y;
            }
            zero.Y -= pushUp;
            float num2 = num8 * (float)value2.Width;
            int num3 = (int)((float)(int)(screenOffset.X * vector.X - vec.X + zero.X - (float)(Main.screenWidth >> 1)) / num2);
            vec = vec.Floor();
            int num4 = (int)Math.Ceiling((float)Main.screenWidth / num2);
            int num5 = (int)(num8 * ((float)(value2.Width - 1) / vector.X));
            Vector2 vec2 = (new Vector2((float)((num3 - 2) * num5), (float)Main.UnderworldLayer * 16f) + vec - screenOffset) * vector + screenOffset - Main.screenPosition - vec + zero;
            vec2 = vec2.Floor();
            while (vec2.X + num2 < 0f)
            {
                num3++;
                vec2.X += num2;
            }
            for (int i = num3 - 2; i <= num3 + 4 + num4; i++)
            {
                Color color = Color.White;
                float num16 = (float)(int)color.R * Alpha;
                float num17 = (float)(int)color.G * Alpha;
                float num18 = (float)(int)color.B * Alpha;
                float num19 = (float)(int)color.A * Alpha;
                color = new((int)(byte)num16, (int)(byte)num17, (int)(byte)num18, (int)(byte)num19);

                Color color2 = UWBGBottomColor[Style];
                float num116 = (float)(int)color2.R * Alpha;
                float num117 = (float)(int)color2.G * Alpha;
                float num118 = (float)(int)color2.B * Alpha;
                float num119 = (float)(int)color2.A * Alpha;
                color2 = new((int)(byte)num116, (int)(byte)num117, (int)(byte)num118, (int)(byte)num119);

                Main.spriteBatch.Draw(value, vec2, (Rectangle?)value2, color, 0f, Vector2.Zero, num8, (SpriteEffects)0, 0f);
                if (layerTextureIndex == 0)
                {
                    int num6 = (int)(vec2.Y + (float)value2.Height * num8);
                    Main.spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle((int)vec2.X, num6, (int)((float)value2.Width * num8), Math.Max(0, Main.screenHeight - num6)), color2);
                }
                vec2.X += num2;
            }
        }
        private void TileLightScanner_ApplyHellLight(Terraria.Graphics.Light.On_TileLightScanner.orig_ApplyHellLight orig, TileLightScanner self, Tile tile, int x, int y, ref Vector3 lightColor)
        {
            orig.Invoke(self, tile, x, y, ref lightColor);
            float finalR = 0f;
            float finalG = 0f;
            float finalB = 0f;
            float num4 = 0.55f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.08f;
            
            if (lightColor.X < finalR)
            {
                lightColor.X = finalR;
            }
            if (lightColor.Y < finalG)
            {
                lightColor.Y = finalG;
            }
            if (lightColor.Z < finalB)
            {
                lightColor.Z = finalB;
            }
            Vector3 neutralLight = new Vector3(0f, 0.2f, 0.2f);
            if (ModContent.GetInstance<NeonVeilTileCount>().tileCount >= 40)
            {
                if ((!tile.HasTile || !Main.tileNoSunLight[tile.TileType] || ((tile.Slope != 0 || tile.IsHalfBlock) && Main.tile[x, y - 1].LiquidAmount == 0 && Main.tile[x, y + 1].LiquidAmount == 0 && Main.tile[x - 1, y].LiquidAmount == 0 && Main.tile[x + 1, y].LiquidAmount == 0)) && (Main.wallLight[tile.WallType] || tile.WallType == 73 || tile.WallType == 227) && tile.LiquidAmount < 200 && (!tile.IsHalfBlock || Main.tile[x, y - 1].LiquidAmount < 200))
                {
                    lightColor = neutralLight;
                }
                if ((!tile.HasTile || tile.IsHalfBlock || !Main.tileNoSunLight[tile.TileType]) && tile.LiquidAmount < byte.MaxValue)
                {
                    lightColor = neutralLight;
                }
                lightColor = neutralLight;
            }
        }
        private void HeatRemoval(ILContext il)
		{
			ILCursor c = new ILCursor(il); //Make a cursor
			c.GotoNext(MoveType.After,
				i => i.MatchLdloc0(),
				i => i.MatchLdfld<Point>("Y"),
				i => i.MatchLdsfld<Main>("maxTilesY"),
				i => i.MatchLdcI4(320),
				i => i.MatchSub(),
				i => i.MatchCgt());
			//Finds the Flag7 Bool that controles the heat Y level
			c.EmitDelegate<Func<bool, bool>>(currentBool => currentBool && ModContent.GetInstance<NeonVeilTileCount>().tileCount < 40); //Adds ontop of the bool with our own
		}

        private void HellBatsGoupSkyEntity_ctor(On_AmbientSky.HellBatsGoupSkyEntity.orig_ctor orig, object self, Player player, FastRandom random)
        {
            orig.Invoke(self, player, random);
            if (ModContent.GetInstance<NeonVeilTileCount>().tileCount >= 40)
            {
                var SkyEntity = typeof(AmbientSky).GetNestedType("SkyEntity", BindingFlags.NonPublic);
                SkyEntity.GetField("Texture",
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Static |
                    BindingFlags.Instance
                    ).SetValue(
                    self,
                    ModContent.Request<Texture2D>("StarsAbove/UI/blank"));
            }
        }
        #endregion
    }
}