
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SubworldLibrary;
using Terraria.Audio;
using StarsAbove.Subworlds;
using StarsAbove.Utilities;
using System;
using StarsAbove.Systems;
using StarsAbove.Systems;

namespace StarsAbove.Items.Consumables
{

    public class SpatialDisk : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spatial Disk");
			/* Tooltip.SetDefault("This mysterious artifact allows you to make contact with your [c/F1AF42:Starfarer]" +
				"\nLeft click to initiate contact with your [c/F1AF42:Starfarer]" +
				"\nYour [c/F1AF42:Starfarer] will periodically grant you components to powerful Aspected Weapons" +
				"\nRight click to open the [c/EC356F:Starfarer Menu] and access special abilities" +
				"\nDefeating bosses will grant powerful passive abilities available in the [c/3599EC:Stellar Array]" +
				"\nAdditionally, the damage type of Aspected Weapons can be adjusted" +
				"\n[c/F1AFFF:Once they have been unlocked, Stellar Novas can be modified with the] [c/EC356F:Starfarer Menu]" +
				"\n[c/F1AFFF:Once it have been unlocked, Celestial Cartography can be accessed with the] [c/EC356F:Starfarer Menu]" +
				"\nYou can re-acquire lost items and read previous dialogue with the [c/9FEE5E:Archive]" +
				$"\nThe ability to wield Umbral [i:{ItemType<Umbral>()}] or Astral [i:{ItemType<Astral>()}] weapons depends on your chosen [c/F1AF42:Starfarer]" +
				""); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 12));
			//
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}
		int pingCooldown;
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ModContent.GetInstance<StellarRarity>().Type;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noUseGraphic = true;
			//item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{// Draw the periodic glow effect

			// Get the initial draw parameters
			Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Items/Consumables/SpatialDisk");

			const float TwoPi = (float)Math.PI * 2f;
			float offset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 5f);

			SpriteEffects effects = SpriteEffects.None;

			scale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 2f) * 0.3f + 0.7f;
			Color effectColor = Color.White;
			effectColor.A = 0;
			effectColor = effectColor * 0.06f * scale;
			for (float num5 = 0f; num5 < 1f; num5 += 355f / (678f * (float)Math.PI))
			{
				spriteBatch.Draw(texture, position + (TwoPi * num5).ToRotationVector2() * (2f + offset * 2f), frame, effectColor, 0f, new Vector2(origin.X + 3, origin.Y + 3), 1f, effects, 0f);
			}
			base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}

		private int randomDialogue;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
			var modPlayer = player.GetModPlayer<StarsAbovePlayer>();

			pingCooldown--;
			if (!modPlayer.seenIntroCutscene)
			{
				if (modPlayer.IntroDialogueTimer <= 0 && modPlayer.chosenStarfarer == 0 && player.GetModPlayer<BossPlayer>().VideoDuration <= 0 && modPlayer.StarfarerSelectionVisibility <= 0)
				{
					modPlayer.IntroDialogueTimer = 300;//20 seconds if you say no.
					modPlayer.sceneID = 0;
					modPlayer.VNDialogueActive = true;
				}


				
				return;
			}
			base.HoldItem(player);
		}
		public override bool CanUseItem(Player player)
		{
			var modPlayer = player.GetModPlayer<StarsAbovePlayer>();

			if (modPlayer.novaUIActive)
				return false;

			if (player.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return false;

			if (player.altFunctionUse == 2)
			{
				if (modPlayer.chosenStarfarer != 0 && modPlayer.starfarerDialogue == false && modPlayer.stellarArray == false && modPlayer.novaUIActive == false && modPlayer.starfarerMenuActive == false && modPlayer.VNDialogueActive == false)
				{

					return true;


				}
				else
				{
					return false;
				}
			}
			else
			if (modPlayer.chosenStarfarer != 0 && modPlayer.starfarerDialogue == false && modPlayer.stellarArray == false && modPlayer.novaUIActive == false && modPlayer.starfarerMenuActive == false && modPlayer.VNDialogueActive == false)
			{
				return true;
			}
			else
				return false;
		}
		public override bool? UseItem(Player player)
		{
			var modPlayer = player.GetModPlayer<StarsAbovePlayer>();

			if (player.altFunctionUse == 2 && !modPlayer.starfarerIntro)
			{
				modPlayer.starfarerMenuDialogueScrollNumber = 0;
				modPlayer.starfarerMenuDialogueScrollTimer = 0;
				int randomDialogue = Main.rand.Next(0, 5);
				if (randomDialogue == 0)
				{
					if (modPlayer.chosenStarfarer == 1)
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.1");//1
					}
					if (modPlayer.chosenStarfarer == 2)
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.1", player.name);
					}

				}
				if (randomDialogue == 1)
				{
					if (modPlayer.chosenStarfarer == 1)
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.2");
					}
					if (modPlayer.chosenStarfarer == 2)//Eridani
					{
						if (DownedBossSystem.downedVagrant)
						{
							modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.2", player.name);
						}
						else
						{
							modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.3", player.name);
						}

					}
				}
				if (randomDialogue == 2)
				{
					if (modPlayer.chosenStarfarer == 1)
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.3");
					}
					if (modPlayer.chosenStarfarer == 2)//Eridani
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.4", player.name);
					}
				}
				if (randomDialogue == 3)
				{
					if (modPlayer.chosenStarfarer == 1)
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.4", player.name);
					}
					if (modPlayer.chosenStarfarer == 2)//Eridani
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.5", player.name);
					}
				}
				if (randomDialogue == 4)
				{
					if (modPlayer.chosenStarfarer == 1)
					{
						if (DownedBossSystem.downedVagrant)
						{
							modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.5", player.name);
						}
						else
						{
							modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.6", player.name);
						}

					}
					if (modPlayer.chosenStarfarer == 2)//Eridani
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.6", player.name);
					}
				}
				if (modPlayer.NewDiskDialogue)
				{
					if (modPlayer.chosenStarfarer == 1)
					{

						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.7", player.name);


					}
					if (modPlayer.chosenStarfarer == 2)//Eridani
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.7", player.name);
					}
				}
				if (modPlayer.inCombat > 0)
				{
					if (modPlayer.chosenStarfarer == 1)
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.8", player.name);
					}
					if (modPlayer.chosenStarfarer == 2)//Eridani
					{
						modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.8", player.name);
					}

				}
				modPlayer.starfarerMenuActive = true;
				modPlayer.starfarerMenuUIOpacity = 0f;
				//modPlayer.stellarArray = true;
				//modPlayer.stellarArrayMoveIn = 15f;
				return true;

			}
			SoundEngine.PlaySound(SoundID.MenuOpen, player.position);
			
			if (modPlayer.starfarerIntro == true)
			{
				//modPlayer.chosenDialogue = 1;

				modPlayer.sceneProgression = 0;

				if (modPlayer.chosenStarfarer == 1)
				{
					modPlayer.sceneID = 3;
				}
				if (modPlayer.chosenStarfarer == 2)
				{
					modPlayer.sceneID = 6;
				}

				modPlayer.VNDialogueActive = true;

				modPlayer.starfarerIntro = false;
				//modPlayer.dialoguePrep = true;
				//modPlayer.starfarerDialogue = true;
				return true;

			}
			//Subworld dialogue tutorials come first.
			if (modPlayer.astrolabeIntroDialogue == 1)
			{

				if (modPlayer.chosenStarfarer == 1)
				{
					modPlayer.sceneID = 11;
				}
				if (modPlayer.chosenStarfarer == 2)
				{
					modPlayer.sceneID = 12;
				}

				activateVNDialogue(player);
				modPlayer.astrolabeIntroDialogue = 2;

				return true;
			}
			if (modPlayer.observatoryIntroDialogue == 1)
			{
				modPlayer.sceneID = 13;

				activateVNDialogue(player);
				modPlayer.observatoryIntroDialogue = 2;

				return true;
			}

			//End of Subworld dialogue.


			if (modPlayer.desertscourgeDialogue == 1)
			{
				modPlayer.chosenDialogue = 201;
				modPlayer.desertscourgeDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.slimeDialogue == 1)
			{
				modPlayer.chosenDialogue = 51;
				modPlayer.slimeDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.eyeDialogue == 1)
			{
				modPlayer.chosenDialogue = 52;
				modPlayer.eyeDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.EyeBossWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 136;
				modPlayer.EyeBossWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.crabulonDialogue == 1)
			{
				modPlayer.chosenDialogue = 202;
				modPlayer.crabulonDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.corruptBossDialogue == 1)
			{
				modPlayer.chosenDialogue = 53;
				modPlayer.corruptBossDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.CorruptBossWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 137;
				modPlayer.CorruptBossWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.Stellaglyph2WeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 160;
				modPlayer.Stellaglyph2WeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.hivemindDialogue == 1)
			{
				modPlayer.chosenDialogue = 203;
				modPlayer.hivemindDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.perforatorDialogue == 1)
			{
				modPlayer.chosenDialogue = 204;
				modPlayer.perforatorDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.BeeBossDialogue == 1)
			{
				modPlayer.chosenDialogue = 54;
				modPlayer.BeeBossDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.QueenSlimeWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 149;
				modPlayer.QueenSlimeWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.VirtueWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 150;
				modPlayer.VirtueWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SkeletonDialogue == 1)
			{
				modPlayer.chosenDialogue = 55;
				modPlayer.SkeletonDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.slimegodDialogue == 1)
			{
				modPlayer.chosenDialogue = 205;
				modPlayer.slimegodDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SkeletonWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 101;
				modPlayer.SkeletonWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.HellWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 102;
				modPlayer.HellWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.QueenBeeWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 103;
				modPlayer.QueenBeeWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.MiseryWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 120;
				modPlayer.MiseryWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.OceanWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 123;
				modPlayer.OceanWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.DeerclopsDialogue == 1)
			{
				modPlayer.chosenDialogue = 76;
				modPlayer.DeerclopsDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.KingSlimeWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 104;
				modPlayer.KingSlimeWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.WallOfFleshDialogue == 1)
			{
				modPlayer.chosenDialogue = 56;
				modPlayer.WallOfFleshDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.FarewellWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 159;
				modPlayer.FarewellWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.UmbraWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 161;
				modPlayer.UmbraWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SaltwaterWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 162;
				modPlayer.SaltwaterWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.ChaosWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 163;
				modPlayer.ChaosWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.ClockWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 164;
				modPlayer.ClockWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}

			if (modPlayer.NanomachineWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 167;
				modPlayer.NanomachineWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SanguineWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 168;
				modPlayer.SanguineWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.LevinstormWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 166;
				modPlayer.LevinstormWeaponDialogue = 2;

				activateDialogue(player);

				return true;
			}

			if (modPlayer.GoldlewisWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 165;
				modPlayer.GoldlewisWeaponDialogue = 2;

				activateDialogue(player);

				return true;
			}
			if (modPlayer.WallOfFleshWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 105;
				modPlayer.WallOfFleshWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.LumaWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 124;
				modPlayer.LumaWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.QueenSlimeDialogue == 1)
			{
				modPlayer.chosenDialogue = 74;
				modPlayer.QueenSlimeDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.UrgotWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 146;
				modPlayer.UrgotWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.MorningStarWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 148;
				modPlayer.MorningStarWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.cryogenDialogue == 1)
			{
				modPlayer.chosenDialogue = 206;
				modPlayer.cryogenDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.TwinsDialogue == 1)
			{
				modPlayer.chosenDialogue = 57;
				modPlayer.TwinsDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.aquaticscourgeDialogue == 1)
			{
				modPlayer.chosenDialogue = 207;
				modPlayer.aquaticscourgeDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.MechBossWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 106;
				modPlayer.MechBossWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.DestroyerDialogue == 1)
			{
				modPlayer.chosenDialogue = 58;
				modPlayer.DestroyerDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.vagrantBossItemDialogue == 1)
			{
				modPlayer.chosenDialogue = 77;
				modPlayer.vagrantBossItemDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.nalhaunBossItemDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				modPlayer.chosenDialogue = 301;
				modPlayer.nalhaunBossItemDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.dioskouroiDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				modPlayer.chosenDialogue = 69;
				modPlayer.dioskouroiDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.dioskouroiBossItemDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				modPlayer.chosenDialogue = 305;
				modPlayer.dioskouroiBossItemDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.brimstoneelementalDialogue == 1)
			{
				modPlayer.chosenDialogue = 208;
				modPlayer.brimstoneelementalDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SkeletronPrimeDialogue == 1)
			{
				modPlayer.chosenDialogue = 59;
				modPlayer.SkeletronPrimeDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.EmpressDialogue == 1)
			{
				modPlayer.chosenDialogue = 75;
				modPlayer.EmpressDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SkyStrikerWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 135;
				modPlayer.SkyStrikerWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.OzmaWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 145;
				modPlayer.OzmaWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.calamitasDialogue == 1)
			{
				modPlayer.chosenDialogue = 209;
				modPlayer.calamitasDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.AllMechsDefeatedDialogue == 1)
			{
				modPlayer.chosenDialogue = 60;
				modPlayer.AllMechsDefeatedDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.AllMechBossWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 107;
				modPlayer.AllMechBossWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.HullwroughtWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 121;
				modPlayer.HullwroughtWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
            if (modPlayer.MonadoWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 125;
				modPlayer.MonadoWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.PlanteraDialogue == 1)
			{
				modPlayer.chosenDialogue = 61;
				modPlayer.PlanteraDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.FrostMoonWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 126;
				modPlayer.FrostMoonWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.penthBossItemDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				modPlayer.chosenDialogue = 302;
				modPlayer.penthBossItemDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.leviathanDialogue == 1)
			{
				modPlayer.chosenDialogue = 210;
				modPlayer.leviathanDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.astrumaureusDialogue == 1)
			{
				modPlayer.chosenDialogue = 211;
				modPlayer.astrumaureusDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.PlanteraWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 108;
				modPlayer.PlanteraWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.GolemDialogue == 1)
			{
				modPlayer.chosenDialogue = 62;
				modPlayer.GolemDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.arbiterBossItemDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				modPlayer.chosenDialogue = 303;
				modPlayer.arbiterBossItemDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.plaguebringerDialogue == 1)
			{
				modPlayer.chosenDialogue = 212;
				modPlayer.plaguebringerDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.GolemWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 109;
				modPlayer.GolemWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.KarnaWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 169;
				modPlayer.KarnaWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.ManiacalWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 170;
				modPlayer.ManiacalWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.AuthorityWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 171;
				modPlayer.AuthorityWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.KineticWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 172;
				modPlayer.KineticWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SoldierWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 173;
				modPlayer.SoldierWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.DreamerWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 174;
				modPlayer.DreamerWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.TrickspinWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 175;
				modPlayer.TrickspinWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
            if (modPlayer.DragaliaWeaponDialogue == 1)
            {
                modPlayer.chosenDialogue = 176;
				modPlayer.DragaliaWeaponDialogue = 2;
                activateDialogue(player);

                return true;
            }
            if (modPlayer.GundbitWeaponDialogue == 1)
            {
                modPlayer.chosenDialogue = 177;
                modPlayer.GundbitWeaponDialogue = 2;
                activateDialogue(player);

                return true;
            }
            if (modPlayer.WavedancerWeaponDialogue == 1)
            {
                modPlayer.chosenDialogue = 178;
                modPlayer.WavedancerWeaponDialogue = 2;
                activateDialogue(player);

                return true;
            }
            if (modPlayer.ClarentWeaponDialogue == 1)
            {
                modPlayer.chosenDialogue = 179;
                modPlayer.ClarentWeaponDialogue = 2;
                activateDialogue(player);

                return true;
            }
            if (modPlayer.ThespianWeaponDialogue == 1)
            {
                modPlayer.chosenDialogue = 180;
                modPlayer.ThespianWeaponDialogue = 2;
                activateDialogue(player);

                return true;
            }
            if (modPlayer.BloodWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 147;
				modPlayer.BloodWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.DukeFishronDialogue == 1)
			{
				modPlayer.chosenDialogue = 63;
				modPlayer.DukeFishronDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.ravagerDialogue == 1)
			{
				modPlayer.chosenDialogue = 213;
				modPlayer.ravagerDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.DukeFishronWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 116;
				modPlayer.DukeFishronWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.CultistDialogue == 1)
			{
				modPlayer.chosenDialogue = 64;
				modPlayer.CultistDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.astrumdeusDialogue == 1)
			{
				modPlayer.chosenDialogue = 214;
				modPlayer.astrumdeusDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.LunaticCultistWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 110;
				modPlayer.LunaticCultistWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.MoonLordDialogue == 1)
			{
				modPlayer.chosenDialogue = 65;
				modPlayer.MoonLordDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.warriorBossItemDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				modPlayer.chosenDialogue = 304;
				modPlayer.warriorBossItemDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.tsukiyomiDialogue == 1)
			{
				modPlayer.chosenDialogue = 73;
				modPlayer.tsukiyomiDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.MoonLordWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 111;
				modPlayer.MoonLordWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.ShadowlessWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 122;
				modPlayer.ShadowlessWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.WarriorOfLightDialogue == 1)
			{
				modPlayer.chosenDialogue = 66;
				modPlayer.WarriorOfLightDialogue = 2;
				activateDialogue(player);

				return true;

			}
			if (modPlayer.vagrantDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				if (modPlayer.chosenStarfarer == 1)
				{//This should be condensed.
					modPlayer.dialogueScrollTimer = 0;
					modPlayer.dialogueScrollNumber = 0;
					modPlayer.sceneProgression = 0;
					modPlayer.sceneID = 9;
					modPlayer.VNDialogueActive = true;
					modPlayer.vagrantDialogue = 2;
				}
				if (modPlayer.chosenStarfarer == 2)
				{
					modPlayer.dialogueScrollTimer = 0;
					modPlayer.dialogueScrollNumber = 0;
					modPlayer.sceneProgression = 0;
					modPlayer.sceneID = 10;
					modPlayer.VNDialogueActive = true;
					modPlayer.vagrantDialogue = 2;
				}

				return true;

			}
			if (modPlayer.nalhaunDialogue == 1)
			{
				modPlayer.chosenDialogue = 70;
				modPlayer.nalhaunDialogue = 2;
				activateDialogue(player);

				return true;

			}
			if (modPlayer.penthDialogue == 1)
			{
				modPlayer.chosenDialogue = 71;
				modPlayer.penthDialogue = 2;
				activateDialogue(player);

				return true;

			}
			if (modPlayer.arbiterDialogue == 1)
			{
				modPlayer.chosenDialogue = 72;
				modPlayer.arbiterDialogue = 2;
				activateDialogue(player);

				return true;

			}
			if (modPlayer.WarriorWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 112;
				modPlayer.WarriorWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.CatalystWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 155;
				modPlayer.CatalystWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SilenceWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 156;
				modPlayer.SilenceWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.VagrantWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 115;
				modPlayer.VagrantWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SoulWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 157;
				modPlayer.SoulWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.GoldWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 158;
				modPlayer.GoldWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.NalhaunWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 117;
				modPlayer.NalhaunWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.PenthesileaWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 118;
				modPlayer.PenthesileaWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.ArbitrationWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 119;
				modPlayer.ArbitrationWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.ClaimhWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 127;
				modPlayer.ClaimhWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.MuseWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 128;
				modPlayer.MuseWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.KifrosseWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 129;
				modPlayer.KifrosseWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.ArchitectWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 130;
				modPlayer.ArchitectWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.MurasamaWeaponDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				modPlayer.chosenDialogue = 139;
				modPlayer.MurasamaWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.MercyWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 141;
				modPlayer.MercyWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.SakuraWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 142;
				modPlayer.SakuraWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.EternalWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 143;
				modPlayer.EternalWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.DaemonWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 144;
				modPlayer.DaemonWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.NeedlepointWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 140;
				modPlayer.NeedlepointWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.CosmicDestroyerWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 138;
				modPlayer.CosmicDestroyerWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.ForceWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 131;
				modPlayer.ForceWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.GenocideWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 132;
				modPlayer.GenocideWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.TakodachiWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 133;
				modPlayer.TakodachiWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.HardwareWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 154;
				modPlayer.HardwareWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.TwinStarsWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 134;
				modPlayer.TwinStarsWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.RedMageWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 151;
				modPlayer.RedMageWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.BlazeWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 152;
				modPlayer.BlazeWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.PickaxeWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 153;
				modPlayer.PickaxeWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.AllVanillaBossesDefeatedDialogue == 1)
			{
				modPlayer.chosenDialogue = 67;
				modPlayer.AllVanillaBossesDefeatedDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.EverythingDefeatedDialogue == 1)
			{
				modPlayer.chosenDialogue = 68;
				modPlayer.EverythingDefeatedDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.AllVanillaBossesDefeatedWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 113;
				modPlayer.AllVanillaBossesDefeatedWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (modPlayer.EverythingDefeatedWeaponDialogue == 1)
			{
				modPlayer.chosenDialogue = 114;
				modPlayer.EverythingDefeatedWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}



			if (modPlayer.uniqueDialogueTimer <= 0)
			{
				modPlayer.uniqueDialogueTimer = Main.rand.Next(1800, 3600);
				if (Main.hardMode)
				{
					randomDialogue = Main.rand.Next(1, 15);

					switch (randomDialogue)
					{
						case 1:
							modPlayer.chosenDialogue = 12;
							break;
						case 2:
							modPlayer.chosenDialogue = 13;
							break;
						case 3:
							modPlayer.chosenDialogue = 14;
							break;
						case 4:
							modPlayer.chosenDialogue = 15;
							break;
						case 5:
							modPlayer.chosenDialogue = 16;
							break;
						case 6:
							modPlayer.chosenDialogue = 17;
							break;
						case 7:
							modPlayer.chosenDialogue = 18;
							break;
						case 8:
							modPlayer.chosenDialogue = 19;
							break;
						case 9:
							modPlayer.chosenDialogue = 20;
							break;
						case 10:
							modPlayer.chosenDialogue = 400;
							break;
						case 11:
							modPlayer.chosenDialogue = 401;
							break;
						case 12:
							modPlayer.chosenDialogue = 402;
							break;
						case 13:
							modPlayer.chosenDialogue = 403;
							break;
						case 14:
							modPlayer.chosenDialogue = 404;
							break;
					}
				}
				else
				{
					randomDialogue = Main.rand.Next(1, 9); //1-20 are idle lines, 50+ are boss dialogue lines, and 100+ is items available to be crafted (1 less than max)

					switch (randomDialogue)
                    {
						case 1:
							modPlayer.chosenDialogue = 3;
							break;
						case 2:
							modPlayer.chosenDialogue = 4;
							break;
						case 3:
							modPlayer.chosenDialogue = 5;
							break;
						case 4:
							modPlayer.chosenDialogue = 6;
							break;
						case 5:
							modPlayer.chosenDialogue = 7;
							break;
						case 6:
							modPlayer.chosenDialogue = 8;
							break;
						case 7:
							modPlayer.chosenDialogue = 9;
							break;
						case 8:
							modPlayer.chosenDialogue = 10;
							break;
						case 9:
							modPlayer.chosenDialogue = 11;
							break;
					}
                }
			}
			else
			{

				modPlayer.chosenDialogue = 2; //Default idle line.

				/*
				if (SubworldSystem.Current == null)
				{


				}
				else
				{
					if (SubworldSystem.IsActive<Observatory>())
					{
						modPlayer.chosenDialogue = 23;
					}
					if (!SubworldSystem.IsActive<Observatory>())
					{
						modPlayer.chosenDialogue = 25;
					}
				}*/
			}
			if (EverlastingLightEvent.isEverlastingLightActive && SubworldSystem.Current == null)
			{
				modPlayer.chosenDialogue = 21;
			}

			//
			modPlayer.dialoguePrep = true;
			modPlayer.starfarerDialogue = true;
			return true;

		}
		private void activateDialogue(Player player)
		{
			var modPlayer = player.GetModPlayer<StarsAbovePlayer>();

			modPlayer.NewDiskDialogue = false;
			modPlayer.dialoguePrep = true;
			modPlayer.starfarerDialogue = true;
		}
		private void activateVNDialogue(Player player)
		{
			var modPlayer = player.GetModPlayer<StarsAbovePlayer>();

			modPlayer.dialogueScrollTimer = 0;
			modPlayer.dialogueScrollNumber = 0;
			modPlayer.sceneProgression = 0;
			modPlayer.VNDialogueActive = true;

		}


		public override void UpdateInventory(Player player)
		{
			var modPlayer = player.GetModPlayer<StarsAbovePlayer>();

			if (modPlayer.NewDiskDialogue)
			{
				//ItemID.Sets.ItemIconPulse[Item.type] = true;
			}
			else
			{
				//ItemID.Sets.ItemIconPulse[Item.type] = false;
			}


			base.UpdateInventory(player);
		}




		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.FallenStar, 1)
				.AddTile(TileID.Anvils)
				.Register();
		}



		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			if (modPlayer.starfarerDialogue || modPlayer.stellarArray || modPlayer.starfarerDialogue)
			{
				itemColor = Color.Black * 255;
			}

			return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
	}
}