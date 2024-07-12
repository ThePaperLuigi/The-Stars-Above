
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
using StarsAbove.Dialogue;
using System.Linq;
using System.Linq.Expressions;

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
        public static bool voicesDisabled;

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
                modPlayer.starfarerMenuActive = true;
                modPlayer.starfarerMenuUIOpacity = 0f;
                modPlayer.starfarerMenuDialogueScrollNumber = 0;
				modPlayer.starfarerMenuDialogueScrollTimer = 0;
				int randomDialogue = Main.rand.Next(0, 6);
                if (modPlayer.inCombat > 0)
                {
                    if (modPlayer.chosenStarfarer == 1)
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneMenuCombat0);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.8", player.name);
                    }
                    if (modPlayer.chosenStarfarer == 2)//Eridani
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniMenuCombat0);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.8", player.name);
                    }
                    return true;
                }
                if (randomDialogue == 0)
				{
					if (modPlayer.chosenStarfarer == 1)
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneMenu0);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.1");//1
					}
					if (modPlayer.chosenStarfarer == 2)
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniMenu0);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.1", player.name);
					}

				}
				if (randomDialogue == 1)
				{
					if (modPlayer.chosenStarfarer == 1)
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneMenu1);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.2");
					}
					if (modPlayer.chosenStarfarer == 2)//Eridani
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniMenu1);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.2", player.name);


                    }
                }
				if (randomDialogue == 2)
				{
					if (modPlayer.chosenStarfarer == 1)
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneMenu2);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.3");
					}
					if (modPlayer.chosenStarfarer == 2)//Eridani
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniMenu3);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.4", player.name);
					}
				}
				if (randomDialogue == 3)
				{
					if (modPlayer.chosenStarfarer == 1)
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneMenu3);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.4", player.name);
					}
					if (modPlayer.chosenStarfarer == 2)//Eridani
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniMenu4);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.5", player.name);
					}
				}
				if (randomDialogue == 4)
				{
					if (modPlayer.chosenStarfarer == 1)
					{
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneMenu5);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.5", player.name);

                    }
					if (modPlayer.chosenStarfarer == 2)//Eridani
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniMenu5);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.6", player.name);
					}
				}
                if (randomDialogue == 5)
                {
                    if (modPlayer.chosenStarfarer == 1)
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneMenu4);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Asphodene.6", player.name);

                    }
                    if (modPlayer.chosenStarfarer == 2)//Eridani
                    {
                        if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniMenu2);}

                        modPlayer.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.EnterStarfarerMenu.Eridani.3", player.name);
                    }
                }

                
				
				//modPlayer.stellarArray = true;
				//modPlayer.stellarArrayMoveIn = 15f;
				return true;

			}
			SoundEngine.PlaySound(SoundID.MenuOpen, player.position);
			//DEBUG
			//Main.NewText(LangHelper.GetCategorySize("Dialogue.IdleDialogueHardmode.Asphodene"));
            //modPlayer.chosenDialogue = 3;
            //activateDialogue(player);


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
            if (modPlayer.vagrantDialogue == 1 && DownedBossSystem.downedVagrant)
            {
                if (modPlayer.chosenStarfarer == 1)
                {
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
            if (modPlayer.starfarerBossItemDialogue == 1)
            {
                modPlayer.dialogueScrollTimer = 0;
                modPlayer.dialogueScrollNumber = 0;
                modPlayer.sceneProgression = 0;
                modPlayer.sceneID = 23;
                modPlayer.VNDialogueActive = true;
                modPlayer.starfarerBossItemDialogue = 2;


                //Spawn item here.
                player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemType<StarfarerEssence>());
                return true;

            }
            if (modPlayer.starfarerPostBattleDialogue == 1)
            {
                modPlayer.dialogueScrollTimer = 0;
                modPlayer.dialogueScrollNumber = 0;
                modPlayer.sceneProgression = 0;
                modPlayer.sceneID = 24;
                modPlayer.VNDialogueActive = true;
                modPlayer.starfarerPostBattleDialogue = 2;

                return true;

            }
            if (EverlastingLightEvent.isEverlastingLightActive && SubworldSystem.Current == null)
            {
                modPlayer.chosenDialogue = 21;
                activateDialogue(player);
				return true;
            }
            if (modPlayer.ActiveDialogues.Count > 0)
            {
                for (int i = 0; i < modPlayer.ActiveDialogues.Count; i++)
                {
                    if (modPlayer.ActiveDialogues.Values.ElementAt(i) == 1)
                    {
                        //Activate the dialogue's ID.
                        modPlayer.chosenDialogue = modPlayer.ActiveDialogues.Keys.ElementAt(i);
						//Set the active dialogue to "read"
						modPlayer.ActiveDialogues[modPlayer.ActiveDialogues.Keys.ElementAt(i)] = 2;
                        activateDialogue(player);
                        return true;
                    }
                }
                
                
            }
            //If there aren't any active dialogues, play an idle dialogue instead.
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
                    }
                }
            }
            else
            {
				if(modPlayer.globalVoiceDelayTimer <= 0)
				{
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().globalVoiceDelayTimer = StarsAbovePlayer.globalVoiceDelayMax * 60;
                    if (Main.hardMode)
                    {
                        if (modPlayer.chosenStarfarer == 1)
                        {
                            if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneIdle1);}

                        }
                        else if (modPlayer.chosenStarfarer == 2)
                        {
                            if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniIdle0);}

                        }
                    }
                    else
                    {
                        if (modPlayer.chosenStarfarer == 1)
                        {
                            if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneIdle0);}

                        }
                        else if (modPlayer.chosenStarfarer == 2)
                        {
                            if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniIdle1);}

                        }
                    }
                }
                
                modPlayer.chosenDialogue = 2;
            }
            activateDialogue(player);
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