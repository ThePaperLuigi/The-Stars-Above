
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

namespace StarsAbove.Items.Consumables
{

    public class SpatialDisk : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Spatial Disk");
			Tooltip.SetDefault("This mysterious artifact allows you to make contact with your [c/F1AF42:Starfarer]" +
				"\nLeft click to initiate contact with your [c/F1AF42:Starfarer]" +
				"\nYour [c/F1AF42:Starfarer] will periodically grant you components to powerful Aspected Weapons" +
				"\nRight click to open the [c/EC356F:Starfarer Menu] and access special abilities" +
				"\nDefeating bosses will grant powerful passive abilities available in the [c/3599EC:Stellar Array]" +
				"\nAbilities are sorted into 1 cost, 2 cost, or 3 cost categories" +
				"\nYou are unable to slot abilities that would total a cost higher than 5" +
				"\nAdditionally, the damage type of Aspected Weapons can be modified with a 10% damage penalty" +
				"\n[c/F1AFFF:Once they have been unlocked, Stellar Novas can be modified with the] [c/EC356F:Starfarer Menu]" +
				"\nYou can re-acquire lost items and read previous dialogue with the [c/9FEE5E:Archive]" +
				$"\nThe ability to wield Umbral [i:{ItemType<Umbral>()}] or Astral [i:{ItemType<Astral>()}] weapons depends on your chosen [c/F1AF42:Starfarer]" +
				"");
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 12));
			//
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}
		int pingCooldown;
		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noUseGraphic = true;
			//item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		private int randomDialogue;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
			pingCooldown--;
			if (player.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility < 2f)
			{
				player.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility += 0.03f;
			}
			base.HoldItem(player);
		}
		public override bool CanUseItem(Player player) {
			if (player.GetModPlayer<StarsAbovePlayer>().novaUIActive)
				return false;

			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 0 && player.GetModPlayer<StarsAbovePlayer>().starfarerDialogue == false && player.GetModPlayer<StarsAbovePlayer>().stellarArray == false && player.GetModPlayer<StarsAbovePlayer>().novaUIActive == false && player.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive == false && player.GetModPlayer<StarsAbovePlayer>().VNDialogueActive == false)
				{
					
						return true;
					
					
				}
				else
				{
					return false;
				}
			}
			else
			if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 0 && player.GetModPlayer<StarsAbovePlayer>().starfarerDialogue == false && player.GetModPlayer<StarsAbovePlayer>().stellarArray == false && player.GetModPlayer<StarsAbovePlayer>().novaUIActive == false && player.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive == false && player.GetModPlayer<StarsAbovePlayer>().VNDialogueActive == false)
			{
				return true;
			}
			else
			return false;
		}
		public override bool? UseItem(Player player) {
			if (player.altFunctionUse == 2 && !player.GetModPlayer<StarsAbovePlayer>().starfarerIntro)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
				int randomDialogue = Main.rand.Next(0, 5);
				if(randomDialogue == 0)
                {
					if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
                    {
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.1");//1
					}
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.1", player.name);
					}
					
				}
				if(randomDialogue == 1)
                {
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.2");
					}
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//Eridani
					{
						if (DownedBossSystem.downedVagrant)
                        {
							Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.2", player.name);
						}
						else
                        {
							Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.3", player.name);
						}
						
					}
				}
				if (randomDialogue == 2)
				{
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.3");
					}
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//Eridani
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.4", player.name);
					}
				}
				if (randomDialogue == 3)
				{
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.4", player.name);
					}
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//Eridani
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.5", player.name);
					}
				}
				if (randomDialogue == 4)
				{
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
					{
						if (DownedBossSystem.downedVagrant)
						{
							Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.5", player.name);
						}
						else
						{
							Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.6", player.name);
						}
						
					}
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//Eridani
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.6", player.name);
					}
				}
				if(player.GetModPlayer<StarsAbovePlayer>().NewDiskDialogue)
                {
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
					{
						
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.7", player.name);


					}
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//Eridani
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.7", player.name);
					}
				}
				if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inCombat > 0)
                {
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.8", player.name);
					}
					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//Eridani
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.8", player.name);
					}

				}
				player.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive = true;
				player.GetModPlayer<StarsAbovePlayer>().starfarerMenuUIOpacity = 0f;
				//player.GetModPlayer<StarsAbovePlayer>().stellarArray = true;
				//player.GetModPlayer<StarsAbovePlayer>().stellarArrayMoveIn = 15f;
				return true;

			}
			SoundEngine.PlaySound(SoundID.MenuOpen, player.position);
			if (player.GetModPlayer<StarsAbovePlayer>().starfarerIntro == true)
			{
				//player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 1;

				player.GetModPlayer<StarsAbovePlayer>().sceneProgression = 0;

				if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
                {
					player.GetModPlayer<StarsAbovePlayer>().sceneID = 3;
				}
				if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					player.GetModPlayer<StarsAbovePlayer>().sceneID = 6;
				}
				
				player.GetModPlayer<StarsAbovePlayer>().VNDialogueActive = true;

				player.GetModPlayer<StarsAbovePlayer>().starfarerIntro = false;
				//player.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
				//player.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
				return true;

			}
			//Subworld dialogue tutorials come first.
			if (player.GetModPlayer<StarsAbovePlayer>().observatoryDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 22;
				player.GetModPlayer<StarsAbovePlayer>().observatoryDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().cosmicVoyageDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 24;
				player.GetModPlayer<StarsAbovePlayer>().cosmicVoyageDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().desertscourgeDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 201;
				player.GetModPlayer<StarsAbovePlayer>().desertscourgeDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().slimeDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 51;
				player.GetModPlayer<StarsAbovePlayer>().slimeDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().eyeDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 52;
				player.GetModPlayer<StarsAbovePlayer>().eyeDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().EyeBossWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 136;
				player.GetModPlayer<StarsAbovePlayer>().EyeBossWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().crabulonDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 202;
				player.GetModPlayer<StarsAbovePlayer>().crabulonDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().corruptBossDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 53;
				player.GetModPlayer<StarsAbovePlayer>().corruptBossDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().CorruptBossWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 137;
				player.GetModPlayer<StarsAbovePlayer>().CorruptBossWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().hivemindDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 203;
				player.GetModPlayer<StarsAbovePlayer>().hivemindDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().perforatorDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 204;
				player.GetModPlayer<StarsAbovePlayer>().perforatorDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().BeeBossDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 54;
				player.GetModPlayer<StarsAbovePlayer>().BeeBossDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().QueenSlimeWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 149;
				player.GetModPlayer<StarsAbovePlayer>().QueenSlimeWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().VirtueWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 150;
				player.GetModPlayer<StarsAbovePlayer>().VirtueWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SkeletonDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 55;
				player.GetModPlayer<StarsAbovePlayer>().SkeletonDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().slimegodDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 205;
				player.GetModPlayer<StarsAbovePlayer>().slimegodDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SkeletonWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 101;
				player.GetModPlayer<StarsAbovePlayer>().SkeletonWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().HellWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 102;
				player.GetModPlayer<StarsAbovePlayer>().HellWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().QueenBeeWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 103;
				player.GetModPlayer<StarsAbovePlayer>().QueenBeeWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MiseryWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 120;
				player.GetModPlayer<StarsAbovePlayer>().MiseryWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().OceanWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 123;
				player.GetModPlayer<StarsAbovePlayer>().OceanWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().DeerclopsDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 76;
				player.GetModPlayer<StarsAbovePlayer>().DeerclopsDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().KingSlimeWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 104;
				player.GetModPlayer<StarsAbovePlayer>().KingSlimeWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().WallOfFleshDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 56;
				player.GetModPlayer<StarsAbovePlayer>().WallOfFleshDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().WallOfFleshWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 105;
				player.GetModPlayer<StarsAbovePlayer>().WallOfFleshWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().LumaWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 124;
				player.GetModPlayer<StarsAbovePlayer>().LumaWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().QueenSlimeDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 74;
				player.GetModPlayer<StarsAbovePlayer>().QueenSlimeDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().UrgotWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 146;
				player.GetModPlayer<StarsAbovePlayer>().UrgotWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MorningStarWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 148;
				player.GetModPlayer<StarsAbovePlayer>().MorningStarWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().cryogenDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 206;
				player.GetModPlayer<StarsAbovePlayer>().cryogenDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().TwinsDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 57;
				player.GetModPlayer<StarsAbovePlayer>().TwinsDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().aquaticscourgeDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 207;
				player.GetModPlayer<StarsAbovePlayer>().aquaticscourgeDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MechBossWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 106;
				player.GetModPlayer<StarsAbovePlayer>().MechBossWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().DestroyerDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 58;
				player.GetModPlayer<StarsAbovePlayer>().DestroyerDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().nalhaunBossItemDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 301;
				player.GetModPlayer<StarsAbovePlayer>().nalhaunBossItemDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().brimstoneelementalDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 208;
				player.GetModPlayer<StarsAbovePlayer>().brimstoneelementalDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SkeletronPrimeDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 59;
				player.GetModPlayer<StarsAbovePlayer>().SkeletronPrimeDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().EmpressDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 75;
				player.GetModPlayer<StarsAbovePlayer>().EmpressDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SkyStrikerWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 135;
				player.GetModPlayer<StarsAbovePlayer>().SkyStrikerWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().OzmaWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 145;
				player.GetModPlayer<StarsAbovePlayer>().OzmaWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().calamitasDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 209;
				player.GetModPlayer<StarsAbovePlayer>().calamitasDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().AllMechsDefeatedDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 60;
				player.GetModPlayer<StarsAbovePlayer>().AllMechsDefeatedDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().AllMechBossWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 107;
				player.GetModPlayer<StarsAbovePlayer>().AllMechBossWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().HullwroughtWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 121;
				player.GetModPlayer<StarsAbovePlayer>().HullwroughtWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MonadoWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 125;
				player.GetModPlayer<StarsAbovePlayer>().MonadoWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().PlanteraDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 61;
				player.GetModPlayer<StarsAbovePlayer>().PlanteraDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().FrostMoonWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 126;
				player.GetModPlayer<StarsAbovePlayer>().FrostMoonWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().penthBossItemDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 302;
				player.GetModPlayer<StarsAbovePlayer>().penthBossItemDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().leviathanDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 210;
				player.GetModPlayer<StarsAbovePlayer>().leviathanDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().astrumaureusDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 211;
				player.GetModPlayer<StarsAbovePlayer>().astrumaureusDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().PlanteraWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 108;
				player.GetModPlayer<StarsAbovePlayer>().PlanteraWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().GolemDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 62;
				player.GetModPlayer<StarsAbovePlayer>().GolemDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().arbiterBossItemDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 303;
				player.GetModPlayer<StarsAbovePlayer>().arbiterBossItemDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().plaguebringerDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 212;
				player.GetModPlayer<StarsAbovePlayer>().plaguebringerDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().GolemWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 109;
				player.GetModPlayer<StarsAbovePlayer>().GolemWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().BloodWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 147;
				player.GetModPlayer<StarsAbovePlayer>().BloodWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().DukeFishronDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 63;
				player.GetModPlayer<StarsAbovePlayer>().DukeFishronDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().ravagerDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 213;
				player.GetModPlayer<StarsAbovePlayer>().ravagerDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().DukeFishronWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 116;
				player.GetModPlayer<StarsAbovePlayer>().DukeFishronWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().CultistDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 64;
				player.GetModPlayer<StarsAbovePlayer>().CultistDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().astrumdeusDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 214;
				player.GetModPlayer<StarsAbovePlayer>().astrumdeusDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().LunaticCultistWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 110;
				player.GetModPlayer<StarsAbovePlayer>().LunaticCultistWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MoonLordDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 65;
				player.GetModPlayer<StarsAbovePlayer>().MoonLordDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().warriorBossItemDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 304;
				player.GetModPlayer<StarsAbovePlayer>().warriorBossItemDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().tsukiyomiDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 73;
				player.GetModPlayer<StarsAbovePlayer>().tsukiyomiDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MoonLordWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 111;
				player.GetModPlayer<StarsAbovePlayer>().MoonLordWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().ShadowlessWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 122;
				player.GetModPlayer<StarsAbovePlayer>().ShadowlessWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().WarriorOfLightDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 66;
				player.GetModPlayer<StarsAbovePlayer>().WarriorOfLightDialogue = 2;
				activateDialogue(player);
				
				return true;
				
			}
			if (player.GetModPlayer<StarsAbovePlayer>().vagrantDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				if(player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
                {//This should be condensed.
					player.GetModPlayer<StarsAbovePlayer>().dialogueScrollTimer = 0;
					player.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber = 0;
					player.GetModPlayer<StarsAbovePlayer>().sceneID = 9;
					player.GetModPlayer<StarsAbovePlayer>().sceneProgression = 0;
					player.GetModPlayer<StarsAbovePlayer>().VNDialogueActive = true;
					player.GetModPlayer<StarsAbovePlayer>().vagrantDialogue = 2;
				}
				if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					player.GetModPlayer<StarsAbovePlayer>().dialogueScrollTimer = 0;
					player.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber = 0;
					player.GetModPlayer<StarsAbovePlayer>().sceneID = 10;
					player.GetModPlayer<StarsAbovePlayer>().sceneProgression = 0;
					player.GetModPlayer<StarsAbovePlayer>().VNDialogueActive = true;
					player.GetModPlayer<StarsAbovePlayer>().vagrantDialogue = 2;
				}
				
				return true;

			}
			if (player.GetModPlayer<StarsAbovePlayer>().nalhaunDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 70;
				player.GetModPlayer<StarsAbovePlayer>().nalhaunDialogue = 2;
				activateDialogue(player);
				
				return true;

			}
			if (player.GetModPlayer<StarsAbovePlayer>().penthDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 71;
				player.GetModPlayer<StarsAbovePlayer>().penthDialogue = 2;
				activateDialogue(player);
				
				return true;

			}
			if (player.GetModPlayer<StarsAbovePlayer>().arbiterDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 72;
				player.GetModPlayer<StarsAbovePlayer>().arbiterDialogue = 2;
				activateDialogue(player);
				
				return true;

			}
			if (player.GetModPlayer<StarsAbovePlayer>().WarriorWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 112;
				player.GetModPlayer<StarsAbovePlayer>().WarriorWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().CatalystWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 155;
				player.GetModPlayer<StarsAbovePlayer>().CatalystWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SilenceWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 156;
				player.GetModPlayer<StarsAbovePlayer>().SilenceWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().VagrantWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 115;
				player.GetModPlayer<StarsAbovePlayer>().VagrantWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SoulWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 157;
				player.GetModPlayer<StarsAbovePlayer>().SoulWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().GoldWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 158;
				player.GetModPlayer<StarsAbovePlayer>().GoldWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().NalhaunWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 117;
				player.GetModPlayer<StarsAbovePlayer>().NalhaunWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().PenthesileaWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 118;
				player.GetModPlayer<StarsAbovePlayer>().PenthesileaWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().ArbitrationWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 119;
				player.GetModPlayer<StarsAbovePlayer>().ArbitrationWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().ClaimhWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 127;
				player.GetModPlayer<StarsAbovePlayer>().ClaimhWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MuseWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 128;
				player.GetModPlayer<StarsAbovePlayer>().MuseWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().KifrosseWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 129;
				player.GetModPlayer<StarsAbovePlayer>().KifrosseWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().ArchitectWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 130;
				player.GetModPlayer<StarsAbovePlayer>().ArchitectWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MurasamaWeaponDialogue == 1 && DownedBossSystem.downedVagrant)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 139;
				player.GetModPlayer<StarsAbovePlayer>().MurasamaWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MercyWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 141;
				player.GetModPlayer<StarsAbovePlayer>().MercyWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SakuraWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 142;
				player.GetModPlayer<StarsAbovePlayer>().SakuraWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().EternalWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 143;
				player.GetModPlayer<StarsAbovePlayer>().EternalWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().DaemonWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 144;
				player.GetModPlayer<StarsAbovePlayer>().DaemonWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().NeedlepointWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 140;
				player.GetModPlayer<StarsAbovePlayer>().NeedlepointWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().CosmicDestroyerWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 138;
				player.GetModPlayer<StarsAbovePlayer>().CosmicDestroyerWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().ForceWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 131;
				player.GetModPlayer<StarsAbovePlayer>().ForceWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().GenocideWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 132;
				player.GetModPlayer<StarsAbovePlayer>().GenocideWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().TakodachiWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 133;
				player.GetModPlayer<StarsAbovePlayer>().TakodachiWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().HardwareWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 154;
				player.GetModPlayer<StarsAbovePlayer>().HardwareWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().TwinStarsWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 134;
				player.GetModPlayer<StarsAbovePlayer>().TwinStarsWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().RedMageWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 151;
				player.GetModPlayer<StarsAbovePlayer>().RedMageWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().BlazeWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 152;
				player.GetModPlayer<StarsAbovePlayer>().BlazeWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().PickaxeWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 153;
				player.GetModPlayer<StarsAbovePlayer>().PickaxeWeaponDialogue = 2;
				activateDialogue(player);

				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().AllVanillaBossesDefeatedDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 67;
				player.GetModPlayer<StarsAbovePlayer>().AllVanillaBossesDefeatedDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().EverythingDefeatedDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 68;
				player.GetModPlayer<StarsAbovePlayer>().EverythingDefeatedDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>(). AllVanillaBossesDefeatedWeaponDialogue== 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 113;
				player.GetModPlayer<StarsAbovePlayer>().AllVanillaBossesDefeatedWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().EverythingDefeatedWeaponDialogue == 1)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 114;
				player.GetModPlayer<StarsAbovePlayer>().EverythingDefeatedWeaponDialogue = 2;
				activateDialogue(player);
				
				return true;
			}

			

			if (player.GetModPlayer<StarsAbovePlayer>().uniqueDialogueTimer <= 0)
			{
				player.GetModPlayer<StarsAbovePlayer>().uniqueDialogueTimer = Main.rand.Next(1800, 3600);
				randomDialogue = Main.rand.Next(1, 9); //1-20 are idle lines, 50+ are boss dialogue lines, and 100+ is items available to be crafted (1 less than max)
				if(Main.hardMode)
				{
					randomDialogue += 9;//New dialogue when Hardmode is reached
				}
				if (randomDialogue == 1)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 3;

				}
				if (randomDialogue == 2)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 4;

				}
				if (randomDialogue == 3)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 5;

				}
				if (randomDialogue == 4)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 6;

				}
				if (randomDialogue == 5)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 7;

				}
				if (randomDialogue == 6)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 8;

				}
				if (randomDialogue == 7)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 9;

				}
				if (randomDialogue == 8)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 10;

				}
				if (randomDialogue == 9)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 11;//Pre hardmode end

				}
				if (randomDialogue == 10)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 12;

				}
				if (randomDialogue == 11)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 13;

				}
				if (randomDialogue == 12)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 14;

				}
				if (randomDialogue == 13)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 15;

				}
				if (randomDialogue == 14)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 16;

				}
				if (randomDialogue == 15)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 17;

				}
				if (randomDialogue == 16)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 18;

				}
				if (randomDialogue == 17)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 19;

				}
				if (randomDialogue == 18)
				{
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 20;

				}

			}
			else
			{
				if(SubworldSystem.Current == null)
                {
					player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 2; //Default idle line.

				}
				else
                {
					if (SubworldSystem.IsActive<Observatory>())
					{
						player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 23;
					}
					if (!SubworldSystem.IsActive<Observatory>())
					{
						player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 25;
					}
				}
			}
			if (NPC.downedMoonlord && !DownedBossSystem.downedWarrior)
			{
				player.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 21;
			}
			
			//
			player.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
			player.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
			return true;
		}
		private void activateDialogue(Player player)
        {

			player.GetModPlayer<StarsAbovePlayer>().NewDiskDialogue = false;
			player.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
			player.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
		}

        public override void UpdateInventory(Player player)
        {
			if(player.GetModPlayer<StarsAbovePlayer>().NewDiskDialogue)
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