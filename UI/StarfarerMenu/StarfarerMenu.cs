
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items.Armor.StarfarerArmor;
using StarsAbove.Utilities;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.StarfarerMenu
{
    internal class StarfarerMenu : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIText warning;

		private UIText hoverText;
		private UIElement area;
		private UIElement area2;
		private UIElement area3;
		private UIElement headHitbox;

		private UIElement HoverArmorArea;
		private UIElement HoverVanityArea;

		private UIImage barFrame;
		private UIImage bg;
		private UIImage bg2;
		private UIImageButton imageButton;
		private UIImage bulletIndicatorOn;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;
		private Vector2 offset;

		private UIText description;

		private UIImageButton stellarNova;
		private UIImageButton stellarArray;
		private UIImageButton archive;
		private UIImageButton voyage;
		private UIImageButton confirmDialogue;

		private UIImageButton IdleDialogue;
		private UIImageButton BossDialogue;
		private UIImageButton WeaponDialogue;
		private UIImageButton VNDialogue;

		private UIImageButton leftButton;
		private UIImageButton rightButton;

		private UIText abilityName;
		private UIText abilitySubName;
		private UIText abilityDescription;
		private UIText starfarerBonus;
		private UIText baseStats;
		private UIText adjustedStats;

		static public VanillaItemSlotWrapper _starfarerArmorSlot;
		static public VanillaItemSlotWrapper _starfarerVanitySlot;

		public static bool ShadesVisible;

		private UIImageButton confirm;
		private UIImageButton reset;

		public bool dragging = false;

		static public bool loadPrisms;

		public override void OnInitialize()
		{


			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.



			area = new UIElement();
			area.Left.Set(200, 0f); // Place the resource bar to the left of the hearts.
			//area.Top.Set(0, 0f);
			area.Width.Set(1000, 0f);
			area.Height.Set(650, 0f);
			//area.OnMouseDown += new UIElement.MouseEvent(DragStart);
			//area.OnMouseUp += new UIElement.MouseEvent(DragEnd);
			area.HAlign = area.VAlign = 0.5f; // 1

			headHitbox = new UIElement();
			headHitbox.Left.Set(200, 0f);
			headHitbox.Width.Set(300, 0f);
			headHitbox.Height.Set(300, 0f);
			headHitbox.HAlign = area.VAlign = 0.5f;

			area2 = new UIElement();
			area2.Left.Set(0, 0f); // Place the resource bar to the left of the hearts.
									//area.Top.Set(0, 0f);
			area2.Width.Set(1000, 0f);
			area2.Height.Set(650, 0f);
			area2.HAlign = area.VAlign = 0.5f; // 1
											  //area2.OnMouseDown += new UIElement.MouseEvent(DragStart);
											  //area.OnMouseUp += new UIElement.MouseEvent(DragEnd);
											  //area2.HAlign = area.VAlign = 0.5f; // 1

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			barFrame.Left.Set(318, 0f);
			barFrame.Top.Set(86, 0f);
			barFrame.Width.Set(514, 0f);
			barFrame.Height.Set(114, 0f);
			/*bg = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			bg.Left.Set(-250, 0f);
			bg.Top.Set(380, 0f);
			bg.Width.Set(840, 0f);
			bg.Height.Set(810, 0f);
			bg2 = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			bg2.Left.Set(-250, 0f);
			bg2.Top.Set(380, 0f);
			bg2.Width.Set(840, 0f);
			bg2.Height.Set(810, 0f);*/

			confirm = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Confirm"));
			confirm.OnClick += Confirm;
			confirm.Width.Set(70, 0f);
			confirm.Height.Set(52, 0f);
			confirm.Left.Set(18, 0f);
			confirm.Top.Set(419, 0f);
			confirm.OnMouseOver += ConfirmHover;
			confirm.OnMouseOut += HoverOff;

			stellarNova = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/StellarNova"));
			stellarNova.OnClick += StellarNovaConfirm;
			stellarNova.Width.Set(74, 0f);
			stellarNova.Height.Set(50, 0f);
			stellarNova.Left.Set(404, 0f);
			stellarNova.Top.Set(189, 0f);
			stellarNova.OnMouseOver += StellarNovaHover;
			stellarNova.OnMouseOut += HoverOff;

			stellarArray = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/StellarArray"));
			stellarArray.OnClick += StellarArrayConfirm;
			stellarArray.Width.Set(74, 0f);
			stellarArray.Height.Set(50, 0f);
			stellarArray.Left.Set(404, 0f);
			stellarArray.Top.Set(131, 0f);
			stellarArray.OnMouseOver += StellarArrayHover;
			stellarArray.OnMouseOut += HoverOff;

			voyage = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Voyage"));
			voyage.OnClick += VoyageConfirm;
			voyage.Width.Set(74, 0f);
			voyage.Height.Set(50, 0f);
			voyage.Left.Set(404, 0f);
			voyage.Top.Set(247, 0f);
			voyage.OnMouseOver += voyageHover;
			voyage.OnMouseOut += HoverOff;
			

			archive = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Archive"));
			archive.OnClick += ArchiveConfirm;
			archive.Width.Set(74, 0f);
			archive.Height.Set(50, 0f);
			archive.Left.Set(404, 0f);
			archive.Top.Set(305, 0f);
			archive.OnMouseOver += ArchiveHover;
			archive.OnMouseOut += HoverOff;

			confirmDialogue = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/ConfirmArchive"));
			confirmDialogue.OnClick += DialogueConfirm;
			confirmDialogue.Width.Set(74, 0f);
			confirmDialogue.Height.Set(50, 0f);
			confirmDialogue.Left.Set(808, 0f);
			confirmDialogue.Top.Set(380, 0f);

			leftButton = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/LeftButton"));
			leftButton.OnClick += LeftButton;
			leftButton.Width.Set(28, 0f);
			leftButton.Height.Set(28, 0f);
			leftButton.Left.Set(780, 0f);
			leftButton.Top.Set(322, 0f);

			rightButton = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/RightButton"));
			rightButton.OnClick += RightButton;
			rightButton.Width.Set(28, 0f);
			rightButton.Height.Set(28, 0f);
			rightButton.Left.Set(878, 0f);
			rightButton.Top.Set(322, 0f);

			//
			IdleDialogue = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/IdleDialogue"));
			IdleDialogue.OnClick += IdleDialogueConfirm;
			IdleDialogue.Width.Set(74, 0f);
			IdleDialogue.Height.Set(50, 0f);
			IdleDialogue.Left.Set(774, 0f);
			IdleDialogue.Top.Set(182, 0f);

			BossDialogue = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/BossDialogue"));
			BossDialogue.OnClick += BossDialogueConfirm;
			BossDialogue.Width.Set(74, 0f);
			BossDialogue.Height.Set(50, 0f);
			BossDialogue.Left.Set(844, 0f);
			BossDialogue.Top.Set(182, 0f);

			WeaponDialogue = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/WeaponDialogue"));
			WeaponDialogue.OnClick += WeaponDialogueConfirm;
			WeaponDialogue.Width.Set(74, 0f);
			WeaponDialogue.Height.Set(50, 0f);
			WeaponDialogue.Left.Set(774, 0f);
			WeaponDialogue.Top.Set(236, 0f);

			VNDialogue = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/VNDialogue"));
			VNDialogue.OnClick += VNDialogueConfirm;
			VNDialogue.Width.Set(74, 0f);
			VNDialogue.Height.Set(50, 0f);
			VNDialogue.Left.Set(844, 0f);
			VNDialogue.Top.Set(236, 0f);

			HoverArmorArea = new UIElement();
			HoverArmorArea.Left.Set(323, 0f);
			HoverArmorArea.Top.Set(310, 0f);
			HoverArmorArea.Width.Set(50, 0f);
			HoverArmorArea.Height.Set(50, 0f);
			HoverArmorArea.OnMouseOver += ArmorSlotHover;
			HoverArmorArea.OnMouseOut += HoverOff;

			HoverVanityArea = new UIElement();
			HoverVanityArea.Left.Set(323, 0f);
			HoverVanityArea.Top.Set(260, 0f);
			HoverVanityArea.Width.Set(50, 0f);
			HoverVanityArea.Height.Set(50, 0f);
			HoverVanityArea.OnMouseOver += VanitySlotHover;
			HoverVanityArea.OnMouseOut += HoverOff;

			_starfarerArmorSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 323 },
				Top = { Pixels = 310 },

				MaxWidth = { Pixels = 50 },
				MaxHeight = { Pixels = 50 },
				ValidItemFunc = item => item.IsAir || !item.IsAir,
				IgnoresMouseInteraction = true

			};
			_starfarerArmorSlot.OnMouseOver += ArmorSlotHover;
			_starfarerArmorSlot.OnMouseOut += HoverOff;

			_starfarerVanitySlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 323 },
				Top = { Pixels = 260 },
				MaxWidth = { Pixels = 50 },
				MaxHeight = { Pixels = 50 },

				ValidItemFunc = item => item.IsAir || !item.IsAir,
				IgnoresMouseInteraction = true
			};
			_starfarerVanitySlot.OnMouseOver += VanitySlotHover;
			_starfarerVanitySlot.OnMouseOut += HoverOff;

			//confirmDialogue.OnMouseOver += DialogueHover;
			//confirmDialogue.OnMouseOut += HoverOff;

			/*reset = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Reset"));
			reset.OnClick += ResetAll;
			reset.Width.Set(70, 0f);
			reset.Height.Set(52, 0f);
			reset.Left.Set(40, 0f);
			reset.Top.Set(244, 0f);
			reset.OnMouseOver += ResetHover;
			reset.OnMouseOut += HoverOff;

			theofania = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StarfarerMenu/theofania"));
			theofania.OnClick += theofaniaSelected;
			theofania.Width.Set(98, 0f);
			theofania.Height.Set(52, 0f);
			theofania.Left.Set(864, 0f);
			theofania.Top.Set(266, 0f);
			theofania.OnMouseOver += TheofaniaHover;
			theofania.OnMouseOut += HoverOff;*/


			/*Asphodene = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/Eridani"));
			Asphodene.OnMouseOver += MouseOverA;
			Asphodene.OnClick += MouseClickA;
			Asphodene.Top.Set(0, 0f);
			Asphodene.Left.Set(0, 0f);
			Asphodene.Width.Set(0, 0f);
			Asphodene.Height.Set(0, 0f);*/

			text = new UIText("", 1f);
			text.Width.Set(11, 0f);
			text.Height.Set(11, 0f);
			text.Top.Set(382, 0f);
			text.Left.Set(104, 0f);

			hoverText = new UIText("", 1f);
			hoverText.Width.Set(10, 0f);
			hoverText.Height.Set(10, 0f);
			hoverText.Top.Set(325, 0f);
			hoverText.Left.Set(823, 0f);

			description = new UIText("", 1f);
			description.Width.Set(10, 0f);
			description.Height.Set(10, 0f);
			description.Top.Set(194, 0f);
			description.Left.Set(560, 0f);

			gradientA = new Color(249, 133, 36); // 
			gradientB = new Color(255, 166, 83); //
												 //area3.Append(Asphodene);

			//area.Append(_affixSlotSpecial);

			//area.Append(bg2);
			//area.Append(bg);
			//area.Append(theofania);
			//area.Append(laevateinn);
			//area.Append(kiwamiryuken);
			//area.Append(gardenofavalon);
			//area.Append(edingenesisquasar);
			//area.Append(barFrame);
			//barFrame.Append(description);

			//area.Append(abilitySubName);
			//area.Append(abilityDescription);
			//area.Append(area2);
			area.Append(hoverText);
			area.Append(description);

			area.Append(HoverArmorArea);
			area.Append(HoverVanityArea);

			area.Append(_starfarerArmorSlot);
			area.Append(_starfarerVanitySlot);

			area.Append(archive);
			area.Append(voyage);
			area.Append(stellarArray);
			area.Append(stellarNova);
			area.Append(confirm);
			area.Append(text);
			Append(area);
			Append(hoverText);

		}
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;
			offset = new Vector2(evt.MousePosition.X - area.Left.Pixels, evt.MousePosition.Y - area.Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;
			Vector2 end = evt.MousePosition;
			dragging = false;

			area.Left.Set(end.X - offset.X, 0f);
			area.Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}


		private void Confirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedStarfarerMenuDialogue = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedDescription = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;


			// We can do stuff in here!
		}
		private void IdleDialogueConfirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber = 1;
		
		}
		private void BossDialogueConfirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList = 1;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber = 1;

		}
		private void WeaponDialogueConfirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber = 1;

		}
		

		private void VNDialogueConfirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList = 3;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber = 1;

		}

		private void LeftButton(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;
			if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber > 1)
            {
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber--;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListMax;
			}




			// We can do stuff in here!
		}
		private void RightButton(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber < Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListMax)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber++;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber == Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListMax)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber = 1;
			}



			// We can do stuff in here!
		}
		private void StellarNovaConfirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeUnlocked || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaUIOpacity = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaUIActive = true;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaDialogueScrollTimer = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedStarfarerMenuDialogue = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedDescription = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;


			// We can do stuff in here!
		}
		private void VoyageConfirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astrolabeIntroDialogue != 2 || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaUIOpacity = 0;
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = true;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaDialogueScrollTimer = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedStarfarerMenuDialogue = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedDescription = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;


			// We can do stuff in here!
		}
		private void StellarArrayConfirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
				return;

			if(!Main.LocalPlayer.HasBuff(BuffType<Buffs.BossEnemySpawnMod>()))
            {
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive = false;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayMoveIn = 15f;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray = true;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive = false;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedStarfarerMenuDialogue = "";
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = "";
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			}



			



			// We can do stuff in here!
		}

		private void DialogueConfirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;
			#region dialogue
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList == 0)
            {
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().IdleArchiveList[Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber].DialogueID;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BossArchiveList[Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber].DialogueID;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;

			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().WeaponArchiveList[Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber].DialogueID;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialoguePrep = true;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = true;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList == 3)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollTimer = 0;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber = 0;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression = 0;


				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneID = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNArchiveList[Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber].DialogueID;


				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueActive = true;
			}

			#endregion

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedStarfarerMenuDialogue = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;



			// We can do stuff in here!
		}
		private void ArchiveConfirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;

			if (!Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive = true;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveChosenList = 0;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveListNumber = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;                                     //|
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Archive.Asphodene");

				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Archive.Eridani");

				}
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;
				
			}
			else
            {
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive = false;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;                                     //|
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.ExitArchive.Asphodene");

				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.ExitArchive.Eridani");

				}
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;
				return;
			}
			
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedDescription = "";




			// We can do stuff in here!
		}


		private void ConfirmHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
			int randomDialogue = Main.rand.Next(0, 3);
			if (randomDialogue == 0)
			{
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Confirm.Asphodene.1");
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Confirm.Eridani.1");
				}

			}
			if (randomDialogue == 1)
			{
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Confirm.Asphodene.2");
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Confirm.Eridani.2");
				}
			}
			if (randomDialogue == 2)
			{
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Confirm.Asphodene.3");
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Confirm.Eridani.3");
				}
			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;
			// We can do stuff in here!
		}


		private void ArchiveHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)														//|
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.ArchiveHover.Asphodene");

			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.ArchiveHover.Eridani");

			}


			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void voyageHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;

			if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astrolabeIntroDialogue != 2)
            {
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Locked.Asphodene");
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Locked.Eridani");

				}
			}
			else
            {
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)                                                     //|
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.VoyageHover.Asphodene");

				}
				else
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.VoyageHover.Eridani");

				}
			}

				


			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void StellarNovaHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
			if(!Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeUnlocked)
            {
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Locked.Asphodene");
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.Locked.Eridani");

				}
			}
			else
            {
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)//                                                   | Limit
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.NovaHover.Asphodene");

				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.NovaHover.Eridani");

				}
			}
			


			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void StellarArrayHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;

			if (Main.LocalPlayer.HasBuff(BuffType<Buffs.BossEnemySpawnMod>()))
			{
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.ArrayBoss.Asphodene");
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//        
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.ArrayBoss.Eridani");

				}
			}
			else
            {
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.ArrayHover.Asphodene");
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//        
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.ArrayHover.Eridani");

				}
			}
				




			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}

		private void ArmorSlotHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;

			
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)//                                                   | Limit
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.StarfarerAttire.Asphodene");
			}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//        
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.StarfarerAttire.Eridani");

			}
			





			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void VanitySlotHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;


			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)//                                                   | Limit
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.StarfarerVanity.Asphodene");
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)//        
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerMenuButtons.StarfarerVanity.Eridani");

			}






			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void HoverOff(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive)
				return;
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = "";
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hoverText = " ";
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedStarfarerMenuDialogue = "";
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaDialogueScrollNumber = 0;
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaDialogueScrollTimer = 0;
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			// We can do stuff in here!
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			// This prevents drawing unless we are using an ExampleDamageItem
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuUIOpacity < 0.1f)
				return;

			base.Draw(spriteBatch);
		}
		
		bool AnimateReverse = false;
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{

			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			var animationModPlayer = Main.LocalPlayer.GetModPlayer<StarfarerMenuAnimation>();

			Rectangle hitbox = area.GetInnerDimensions().ToRectangle();
			Rectangle bodyHitbox = area.GetInnerDimensions().ToRectangle();
			Rectangle dialogue = barFrame.GetInnerDimensions().ToRectangle();
			Rectangle archiveSelected = archive.GetInnerDimensions().ToRectangle();
			Rectangle headLocation = headHitbox.GetInnerDimensions().ToRectangle();

			//Rectangle indicator = new Rectangle((600), (280), (700), (440));
			//indicator.X += 0;
			//indicator.Width -= 0;
			//indicator.Y += 0;
			//indicator.Height -= 0;

			//Rectangle dialogueBox = new Rectangle((50), (480), (700), (300));
			//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/description"), dialogue, Color.White * (modPlayer.descriptionOpacity));

			//Step 1: draw Hair behind body
			//Step 2: draw Body (Starfarer initial, 'Body', costume number (0 for default)
			//Step 3: draw Head
			//Step 4: draw Hair in front of body
			//Step 5: draw Menu

			Texture2D AsphodeneHeadBase = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/AHeadBase");
			Texture2D AsphodeneHairBehindHead = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/AHairBehindHead");
			Texture2D AsphodeneHeadBaseH = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/AHeadBaseH");
			Texture2D AsphodeneHairBehindHeadH = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/AHairBehindHeadH");

			Texture2D AsphodeneNeck = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ANeck");

			Texture2D AsphodeneGrab = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/AGrab");
			Texture2D AsphodeneHandOpen = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/AHandOpen");


			Texture2D AsphodeneTheofania = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ATheofania");//300x500

			Texture2D AsphodeneRightLeg = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ARightLeg");
			Texture2D AsphodeneLeftLeg = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ALeftLeg");


			Texture2D AsphodeneEyeRight = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/AEyeRight");
			Texture2D AsphodeneEyeLeft = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/AEyeLeft");


			Texture2D AsphodeneTailRight = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ATailRight");
			Texture2D AsphodeneTailLeft = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ATailLeft");
			Texture2D AsphodeneTailRightH = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ATailRightH");
			Texture2D AsphodeneTailLeftH = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ATailLeftH");

			Texture2D AsphodeneRightArmLower = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ARightArmLower0");
			Texture2D AsphodeneRightArmUpper = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ARightArmUpper0");


			Vector2 AsphodeneHeadPosition = new Vector2(Main.screenWidth/2 - 96, Main.screenHeight/2 - 118 + animationModPlayer.StarfarerMenuIdleMovement);
			Vector2 AsphodeneUpperArmPosition = new Vector2(Main.screenWidth / 2 - 126, Main.screenHeight / 2 - 82 + animationModPlayer.StarfarerMenuIdleMovement);
			Vector2 AsphodeneLowerArmPosition = new Vector2(Main.screenWidth / 2 - 132, Main.screenHeight / 2 - 64 + animationModPlayer.StarfarerMenuIdleMovement);

			Vector2 GripPosition = new Vector2(Main.screenWidth / 2 - 132, Main.screenHeight / 2 - 64 + animationModPlayer.StarfarerMenuIdleMovement);
			Vector2 SwordPosition = new Vector2(Main.screenWidth / 2 - 114, Main.screenHeight / 2 + 10 + animationModPlayer.StarfarerMenuIdleMovement);
			

			Vector2 AsphodeneRightLegPosition = new Vector2(Main.screenWidth / 2 - 126, Main.screenHeight / 2 + 100 + animationModPlayer.StarfarerMenuIdleMovement/2);
			Vector2 AsphodeneLeftLegPosition = new Vector2(Main.screenWidth / 2 - 72, Main.screenHeight / 2  + 90 + animationModPlayer.StarfarerMenuIdleMovement/2);

			Vector2 AsphodeneBodyPosition = new Vector2(Main.screenWidth / 2 - 300, (Main.screenHeight / 2) - 325 + animationModPlayer.StarfarerMenuIdleMovement);

			Vector2 AsphodeneEyeRightPosition = new Vector2(Main.screenWidth / 2 - 96 + animationModPlayer.AsphodeneEyeMovementRX, Main.screenHeight / 2 - 118 + animationModPlayer.StarfarerMenuIdleMovement + animationModPlayer.AsphodeneEyeMovementY);
			Vector2 AsphodeneEyeLeftPosition = new Vector2(Main.screenWidth / 2 - 96 + animationModPlayer.AsphodeneEyeMovementLX, Main.screenHeight / 2 - 118 + animationModPlayer.StarfarerMenuIdleMovement + animationModPlayer.AsphodeneEyeMovementY);

			if (modPlayer.archiveActive)
            {
				AsphodeneHeadPosition.X -= 200;
				AsphodeneBodyPosition.X -= 200;
				AsphodeneEyeRightPosition.X -= 200;
				AsphodeneEyeLeftPosition.X -= 200;
				AsphodeneUpperArmPosition.X -= 200;
				AsphodeneLowerArmPosition.X -= 200;
				AsphodeneRightLegPosition.X -= 200;
				AsphodeneLeftLegPosition.X -= 200;
				SwordPosition.X -= 200;
			}

			//Vector2 AsphodeneBodyPosition = new Vector2(bodyHitbox.X, bodyHitbox.Y + modPlayer.StarfarerMenuIdleMovement);

			//Vector2 AsphodeneHeadPosition = new Vector2(headLocation.X, headLocation.Y - 166 + IdleMovement); +104, +26
			//-49X, -54Y
			Vector2 AsphodenePonytailPosition1 = new Vector2(AsphodeneHeadPosition.X - 49, AsphodeneHeadPosition.Y - 54 + animationModPlayer.StarfarerMenuIdleMovement/2);
			//+46X, -59Y
			Vector2 AsphodenePonytailPosition2 = new Vector2(AsphodeneHeadPosition.X + 46, AsphodeneHeadPosition.Y - 59 + animationModPlayer.StarfarerMenuIdleMovement/2);


			Texture2D vignette = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/vignette");
			float width = (float)Main.screenWidth / (float)vignette.Width;
			float height = (float)Main.screenHeight / (float)vignette.Height;
			Vector2 zero = Vector2.Zero;
			if (width != height)
			{
				if (height > width)
				{
					width = height;
					zero.X -= ((float)vignette.Width * width - (float)Main.screenWidth) * 0.5f;
				}
				else
				{
					zero.Y -= ((float)vignette.Height * width - (float)Main.screenHeight) * 0.5f;
				}
			}



			spriteBatch.Draw(vignette, Vector2.Zero, (Rectangle?)null, Color.White * (modPlayer.starfarerMenuUIOpacity), 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);


			if (modPlayer.chosenStarfarer == 1) // Asphodene's code.
			{
				if (modPlayer.vagrantDialogue == 2)
				{
					//Draw the hair that appears behind the head.
					spriteBatch.Draw(
					AsphodeneHairBehindHeadH, //The texture being drawn.
					new Vector2(AsphodeneHeadPosition.X, AsphodeneHeadPosition.Y + 2), //The position of the texture.
					new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					0f, // The rotation of the texture.
					AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);


					//Draw the ponytail(s)
					spriteBatch.Draw(
						AsphodeneTailLeftH, //The texture being drawn.
						AsphodenePonytailPosition2, //The position of the texture.
						new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 3), // The rotation of the texture.
						AsphodeneTailLeft.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
					spriteBatch.Draw(
						AsphodeneTailRightH, //The texture being drawn.
						AsphodenePonytailPosition1, //The position of the texture.
						new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(-animationModPlayer.StarfarerMenuHeadRotation / 3), // The rotation of the texture.
						AsphodeneTailRight.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
				}
				else
                {
					//Draw the hair that appears behind the head.
					spriteBatch.Draw(
					AsphodeneHairBehindHead, //The texture being drawn.
					new Vector2(AsphodeneHeadPosition.X, AsphodeneHeadPosition.Y + 2), //The position of the texture.
					new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					0f, // The rotation of the texture.
					AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);


					//Draw the ponytail(s)
					spriteBatch.Draw(
						AsphodeneTailLeft, //The texture being drawn.
						AsphodenePonytailPosition2, //The position of the texture.
						new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 3), // The rotation of the texture.
						AsphodeneTailLeft.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
					spriteBatch.Draw(
						AsphodeneTailRight, //The texture being drawn.
						AsphodenePonytailPosition1, //The position of the texture.
						new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(-animationModPlayer.StarfarerMenuHeadRotation / 3), // The rotation of the texture.
						AsphodeneTailRight.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
				}
				//Draw the neck.
				spriteBatch.Draw(
					AsphodeneNeck, //The texture being drawn.
					AsphodeneHeadPosition, //The position of the texture.
					new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					0f, // The rotation of the texture.
					AsphodeneTailLeft.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
				if (modPlayer.starfarerOutfitVisible == 0)//
				{
					//Draw the arm (lower part)
					if(!animationModPlayer.idleAnimationActive)
                    {
						spriteBatch.Draw(
						AsphodeneRightArmLower, //The texture being drawn.
						AsphodeneLowerArmPosition, //The position of the texture.
						new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
						AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
					}
					

					
					//Behind the skirt.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ASkirtBottom"), AsphodeneBodyPosition, Color.White * (modPlayer.starfarerMenuUIOpacity));

					//Draw the legs
					spriteBatch.Draw(
						AsphodeneRightLeg, //The texture being drawn.
						AsphodeneRightLegPosition, //The position of the texture.
						new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation/2), // The rotation of the texture.
						AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
					spriteBatch.Draw(
						AsphodeneLeftLeg, //The texture being drawn.
						AsphodeneLeftLegPosition, //The position of the texture.
						new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(-animationModPlayer.StarfarerMenuHeadRotation/2), // The rotation of the texture.
						AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);


					//Draw the body, accounting for costume.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/ABody" + modPlayer.starfarerOutfitVisible), AsphodeneBodyPosition, Color.White * (modPlayer.starfarerMenuUIOpacity));
					
					

					//If the outfit changed recently, draw a white flash to signify the effect.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/ABody" + modPlayer.starfarerOutfitVisible + "W"), AsphodeneBodyPosition, Color.White * (modPlayer.costumeChangeOpacity));
					
				}
				else
                {
					//Draw the body, accounting for costume.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/ABody" + modPlayer.starfarerOutfitVisible), AsphodeneBodyPosition, Color.White * (modPlayer.starfarerMenuUIOpacity));
					//If the outfit changed recently, draw a white flash to signify the effect.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/ABody" + modPlayer.starfarerOutfitVisible + "W"), AsphodeneBodyPosition, Color.White * (modPlayer.costumeChangeOpacity));

				}

				if (modPlayer.vagrantDialogue == 2)
				{
					//Draw the head.
					spriteBatch.Draw(
					AsphodeneHeadBaseH, //The texture being drawn.
					AsphodeneHeadPosition, //The position of the texture.
					new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
				}
				else
                {
					//Draw the head.
					spriteBatch.Draw(
					AsphodeneHeadBase, //The texture being drawn.
					AsphodeneHeadPosition, //The position of the texture.
					new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
				}
				//Draw the eyes.
				spriteBatch.Draw(
					AsphodeneEyeLeft, //The texture being drawn.
					AsphodeneEyeLeftPosition, //The position of the texture.
					new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
				spriteBatch.Draw(
					AsphodeneEyeRight, //The texture being drawn.
					AsphodeneEyeRightPosition, //The position of the texture.
					new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);


				//Idle animation.
				if (modPlayer.starfarerOutfitVisible == 0)//
				{
					spriteBatch.Draw(//Draw the sword.
						AsphodeneTheofania, //The texture being drawn.
						SwordPosition, //The position of the texture.
						new Rectangle(0, 0, AsphodeneTheofania.Width, AsphodeneTheofania.Height), //The source rectangle.
						Color.White * (animationModPlayer.idleAnimationAlpha), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation + animationModPlayer.StarfarerMenuIdleAnimationRotation + 40), // The rotation of the texture.
						AsphodeneTheofania.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
					if (animationModPlayer.idleAnimationActive)
					{
						if (animationModPlayer.idleAnimationProgress < 0.02f)
						{
							spriteBatch.Draw(//Draw the open hand
							AsphodeneHandOpen, //The texture being drawn.
							AsphodeneLowerArmPosition, //The position of the texture.
							new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
							Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture. (Uses normal opacity, because it instantly replaces the old arm.)
							MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation + animationModPlayer.StarfarerMenuIdleAnimationRotation), // The rotation of the texture.
							AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
							1f, //The scale of the texture.
							SpriteEffects.None,
							0f);
						}
						else
						{
							spriteBatch.Draw(//Draw the gripping arm.
							AsphodeneGrab, //The texture being drawn.
												AsphodeneLowerArmPosition, //The position of the texture.
												new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
												Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture. (Uses normal opacity, because it instantly replaces the old arm.)
												MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation + animationModPlayer.StarfarerMenuIdleAnimationRotation), // The rotation of the texture.
												AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
												1f, //The scale of the texture.
												SpriteEffects.None,
												0f);
						}


					}

					//Draw the arm (upper part)
					spriteBatch.Draw(
						AsphodeneRightArmUpper, //The texture being drawn.
						AsphodeneUpperArmPosition, //The position of the texture.
						new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 2), // The rotation of the texture.
						AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
					//Closed mouth.
					spriteBatch.Draw(
						(Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/AClosedMouth"), //The texture being drawn.
						AsphodeneHeadPosition, //The position of the texture.
						new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
						Color.White * (animationModPlayer.idleAnimationAlphaFast), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
						AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
				}
				/*
				//Draw the hair behind the body.
				if (modPlayer.vagrantDialogue == 2)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/AHairBH"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				}
				else
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/AHairB"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				}
				//Draw the head.
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/AHead"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				//Draw the hair.
				if (modPlayer.vagrantDialogue == 2)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/AHairH"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				}
				else
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/AHair"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
					
				}
				*/


			}

			//Eridani's code.
			Texture2D EridaniHeadBase = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EHeadBase");
			Texture2D EridaniMouthAlt = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EMouthVariant");

			Texture2D EridaniHeadBaseH = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EHeadBaseH");
			Texture2D EridaniHairBehindHeadH = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EHairBehindHeadH");
			Texture2D EridaniPonytailH = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EPonytailH");



			Texture2D EridaniHairBehindHead = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EHairBehindHead");
			Texture2D EridaniNeck = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ENeck");
			Texture2D EridaniRightLeg = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ERightLeg");
			Texture2D EridaniLeftLeg = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ELeftLeg");
			Texture2D EridaniPonytail = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EPonytail");
			Texture2D EridaniLeftArm = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ELeftArm");
			Texture2D EridaniBehindSkirtAndCape = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EBehindSkirtAndCape");
			Texture2D EridaniEyeRight = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EEyeRight");
			Texture2D EridaniEyeLeft = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EEyeLeft");

			Texture2D EridaniArmsCrossed = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EArmsCrossed");
			Texture2D EridaniBook = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EIdleBook");
			Texture2D EridaniBookWhite = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/EIdleBookWhite");


			Vector2 EridaniHeadPosition = new Vector2(Main.screenWidth / 2 - 96, Main.screenHeight / 2 - 112 + animationModPlayer.StarfarerMenuIdleMovement);
			
			Vector2 EridaniLowerArmPosition = new Vector2(Main.screenWidth / 2 - 22, Main.screenHeight / 2 - 14 + animationModPlayer.StarfarerMenuIdleMovement);

			Vector2 EridaniPonytailPosition = new Vector2(Main.screenWidth / 2 - 66, Main.screenHeight / 2 - 132 + animationModPlayer.StarfarerMenuIdleMovement);


			Vector2 EridaniRightLegPosition = new Vector2(Main.screenWidth / 2 - 106, Main.screenHeight / 2 + 94 + animationModPlayer.StarfarerMenuIdleMovement / 2);
			Vector2 EridaniLeftLegPosition = new Vector2(Main.screenWidth / 2 - 72, Main.screenHeight / 2 + 84 + animationModPlayer.StarfarerMenuIdleMovement / 2);

			Vector2 EridaniBodyPosition = new Vector2(Main.screenWidth / 2 - 308, (Main.screenHeight / 2) - 328 + animationModPlayer.StarfarerMenuIdleMovement);

			Vector2 EridaniEyeRightPosition = new Vector2(Main.screenWidth / 2 - 96 + animationModPlayer.EridaniEyeMovementRX, Main.screenHeight / 2 - 112 + animationModPlayer.StarfarerMenuIdleMovement + animationModPlayer.EridaniEyeMovementY);
			Vector2 EridaniEyeLeftPosition = new Vector2(Main.screenWidth / 2 - 97 + animationModPlayer.EridaniEyeMovementLX, Main.screenHeight / 2 - 112 + animationModPlayer.StarfarerMenuIdleMovement + animationModPlayer.EridaniEyeMovementY);

			Vector2 EridaniBookPosition = new Vector2(Main.screenWidth / 2 - 126, Main.screenHeight / 2 + 12 + animationModPlayer.StarfarerMenuHeadRotation / 2);
			Vector2 EridaniArmsCrossedPosition = new Vector2(Main.screenWidth / 2 - 102, Main.screenHeight / 2 - 42 + animationModPlayer.StarfarerMenuIdleMovement);

			if (modPlayer.archiveActive)
			{
				EridaniHeadPosition.X -= 200;
				EridaniBodyPosition.X -= 200;
				EridaniPonytailPosition.X -= 200;
				EridaniEyeRightPosition.X -= 200;
				EridaniEyeLeftPosition.X -= 200;
				EridaniLowerArmPosition.X -= 200;
				EridaniRightLegPosition.X -= 200;
				EridaniLeftLegPosition.X -= 200;
				EridaniBookPosition.X -= 200;
				EridaniArmsCrossedPosition.X -= 200;
			}

			if (modPlayer.chosenStarfarer == 2)
			{
				if (modPlayer.vagrantDialogue == 2)
				{
					//Draw the ponytail(s)
					spriteBatch.Draw(
						EridaniPonytailH, //The texture being drawn.
						EridaniPonytailPosition, //The position of the texture.
						new Rectangle(0, 0, EridaniPonytail.Width, EridaniPonytail.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 2), // The rotation of the texture.
						EridaniPonytail.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);

					//Draw the hair that appears behind the head.

					spriteBatch.Draw(
						EridaniHairBehindHeadH, //The texture being drawn.
						new Vector2(EridaniHeadPosition.X, EridaniHeadPosition.Y), //The position of the texture.
						new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						0f, // The rotation of the texture.
						EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
				}
				else
				{
					//Draw the ponytail(s)
					spriteBatch.Draw(
						EridaniPonytail, //The texture being drawn.
						EridaniPonytailPosition, //The position of the texture.
						new Rectangle(0, 0, EridaniPonytail.Width, EridaniPonytail.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 2), // The rotation of the texture.
						EridaniPonytail.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);

					//Draw the hair that appears behind the head.

					spriteBatch.Draw(
						EridaniHairBehindHead, //The texture being drawn.
						new Vector2(EridaniHeadPosition.X, EridaniHeadPosition.Y), //The position of the texture.
						new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						0f, // The rotation of the texture.
						EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
				}
				




				//Draw the neck.
				spriteBatch.Draw(
					EridaniNeck, //The texture being drawn.
					EridaniHeadPosition, //The position of the texture.
					new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					0f, // The rotation of the texture.
					EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
				if (modPlayer.starfarerOutfitVisible == 0)//
				{
					//Behind the skirt.
					spriteBatch.Draw(EridaniBehindSkirtAndCape, EridaniBodyPosition, Color.White * (modPlayer.starfarerMenuUIOpacity));

					//Draw the arm (lower part)
					if (!animationModPlayer.idleAnimationActive)
					{
						spriteBatch.Draw(
						EridaniLeftArm, //The texture being drawn.
						EridaniLowerArmPosition, //The position of the texture.
						new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(-animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
						EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
					}





					//Draw the legs
					spriteBatch.Draw(
						EridaniRightLeg, //The texture being drawn.
						EridaniRightLegPosition, //The position of the texture.
						new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 2), // The rotation of the texture.
						EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
					spriteBatch.Draw(
						EridaniLeftLeg, //The texture being drawn.
						EridaniLeftLegPosition, //The position of the texture.
						new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
						Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
						MathHelper.ToRadians(-animationModPlayer.StarfarerMenuHeadRotation / 2), // The rotation of the texture.
						EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);


					//Draw the body, accounting for costume.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EBody" + modPlayer.starfarerOutfitVisible), EridaniBodyPosition, Color.White * (modPlayer.starfarerMenuUIOpacity));
					
				}
				else
				{
					//Draw the body, accounting for costume.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EBody" + modPlayer.starfarerOutfitVisible), EridaniBodyPosition, Color.White * (modPlayer.starfarerMenuUIOpacity));
					
				}

				if (modPlayer.vagrantDialogue == 2)
				{
					//Draw the head.
					spriteBatch.Draw(
					EridaniHeadBaseH, //The texture being drawn.
					EridaniHeadPosition, //The position of the texture.
					new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
				}
				else
                {
					//Draw the head.
					spriteBatch.Draw(
					EridaniHeadBase, //The texture being drawn.
					EridaniHeadPosition, //The position of the texture.
					new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
				}
					
				//Draw the eyes.
				spriteBatch.Draw(
					EridaniEyeLeft, //The texture being drawn.
					EridaniEyeLeftPosition, //The position of the texture.
					new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
				spriteBatch.Draw(
					EridaniEyeRight, //The texture being drawn.
					EridaniEyeRightPosition, //The position of the texture.
					new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
					Color.White * (modPlayer.starfarerMenuUIOpacity), //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);


				//Idle animation.
				
				if (animationModPlayer.idleAnimationActive && animationModPlayer.idleAnimationProgress < 0.4)
				{
					spriteBatch.Draw(
						EridaniLeftArm, //The texture being drawn.
						EridaniLowerArmPosition, //The position of the texture.
						new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
						Color.White * (1 - animationModPlayer.idleAnimationProgress * 3), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuIdleAnimationRotation*10), // The rotation of the texture.
						EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
				}
				
				if (modPlayer.starfarerOutfitVisible == 0)//
				{
					spriteBatch.Draw(//Summon the book
						EridaniArmsCrossed, //The texture being drawn.
						EridaniArmsCrossedPosition, //The position of the texture.
						new Rectangle(0, 0, EridaniBook.Width, EridaniBook.Height), //The source rectangle.
						Color.White * (animationModPlayer.idleAnimationProgress * 3), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 5), // The rotation of the texture.
						EridaniBook.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);

					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Animation/ECapeTop"), EridaniBodyPosition, Color.White * (modPlayer.starfarerMenuUIOpacity));

					spriteBatch.Draw(//Summon the book
							EridaniBook, //The texture being drawn.
							EridaniBookPosition, //The position of the texture.
							new Rectangle(0, 0, EridaniBook.Width, EridaniBook.Height), //The source rectangle.
							Color.White * (animationModPlayer.idleAnimationAlpha), //The color of the texture.
							MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 5), // The rotation of the texture.
							EridaniBook.Size() * 0.5f, //The centerpoint of the texture.
							1f, //The scale of the texture.
							SpriteEffects.None,
							0f);
					if (animationModPlayer.idleAnimationActive)
					{
						if (animationModPlayer.idleAnimationProgress < 0.5)
						{
							spriteBatch.Draw(//Summon the book
								EridaniBook, //The texture being drawn.
								EridaniBookPosition, //The position of the texture.
								new Rectangle(0, 0, EridaniBook.Width, EridaniBook.Height), //The source rectangle.
								Color.White, //* (animationModPlayer.idleAnimationAlpha), //The color of the texture.
								MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 5), // The rotation of the texture.
								EridaniBook.Size() * 0.5f, //The centerpoint of the texture.
								1f, //The scale of the texture.
								SpriteEffects.None,
								0f);
						}
						else
                        {
							spriteBatch.Draw(//Summon the book
								EridaniBook, //The texture being drawn.
								EridaniBookPosition, //The position of the texture.
								new Rectangle(0, 0, EridaniBook.Width, EridaniBook.Height), //The source rectangle.
								Color.White * (animationModPlayer.idleAnimationAlpha), //The color of the texture.
								MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 5), // The rotation of the texture.
								EridaniBook.Size() * 0.5f, //The centerpoint of the texture.
								1f, //The scale of the texture.
								SpriteEffects.None,
								0f);
						}
						spriteBatch.Draw(//Summon the book
						EridaniBookWhite, //The texture being drawn.
						EridaniBookPosition, //The position of the texture.
						new Rectangle(0, 0, EridaniBook.Width, EridaniBook.Height), //The source rectangle.
						Color.White * (1 - animationModPlayer.idleAnimationAlpha), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation / 5), // The rotation of the texture.
						EridaniBook.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
					}

					//Draw the closed mouth
					spriteBatch.Draw(
						EridaniMouthAlt, //The texture being drawn.
						EridaniHeadPosition, //The position of the texture.
						new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
						Color.White * (animationModPlayer.idleAnimationAlpha), //The color of the texture.
						MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
						EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);

				}

				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EBody" + modPlayer.starfarerOutfitVisible + "W"), EridaniBodyPosition, Color.White * (modPlayer.costumeChangeOpacity));

				/*
				//OLD CODE
				//Draw the hair behind the body.
				if (modPlayer.vagrantDialogue == 2)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EHairBH"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
                }
                else
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EHairB"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));

                }
				//Draw the head.
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EHead"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));

				//Draw the hair.
				if (modPlayer.vagrantDialogue == 2)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EHairH"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				}
				else
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EHair"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));

				}
				//Draw the body, accounting for costume.
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EBody"+ modPlayer.starfarerOutfitVisible), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));

				//If the outfit changed recently, draw a white flash to signify the effect.
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EBody" + modPlayer.starfarerOutfitVisible + "W"), hitbox, Color.White * (modPlayer.costumeChangeOpacity));

				*/


			}
			//Draw the actual menu.
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/StarfarerMenuUI"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));



			if (modPlayer.archiveActive)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/ArchiveMenu"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/Archive"), archive.GetInnerDimensions().ToRectangle(), Color.White * (modPlayer.starfarerMenuUIOpacity));
				if (modPlayer.archiveChosenList == 0)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/IdleSelected"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				}
				if (modPlayer.archiveChosenList == 1)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/BossSelected"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				}
				if (modPlayer.archiveChosenList == 2)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/WeaponSelected"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				}
				if (modPlayer.archiveChosenList == 3)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/VNSelected"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));
				}
			}
			

			if ((modPlayer.blinkTimer > 70 && modPlayer.blinkTimer < 75) || (modPlayer.blinkTimer > 320 && modPlayer.blinkTimer < 325) || (modPlayer.blinkTimer > 420 && modPlayer.blinkTimer < 425) || (modPlayer.blinkTimer > 428 && modPlayer.blinkTimer < 433))
			{

				if (modPlayer.chosenStarfarer == 1 && !animationModPlayer.idleAnimationActive)
				{
					//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/ABlink"), headLocation, Color.White * (modPlayer.starfarerMenuUIOpacity));
					spriteBatch.Draw(
					(Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/ABlink"), //The texture being drawn.
					AsphodeneHeadPosition, //The position of the texture.
					new Rectangle(0, 0, AsphodeneHeadBase.Width, AsphodeneHeadBase.Height), //The source rectangle.
					Color.White, //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					AsphodeneHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
				}
				if (modPlayer.chosenStarfarer == 2)
				{
					//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/ABlink"), headLocation, Color.White * (modPlayer.starfarerMenuUIOpacity));
					spriteBatch.Draw(
					(Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EBlink"), //The texture being drawn.
					EridaniHeadPosition, //The position of the texture.
					new Rectangle(0, 0, EridaniHeadBase.Width, EridaniHeadBase.Height), //The source rectangle.
					Color.White, //The color of the texture.
					MathHelper.ToRadians(animationModPlayer.StarfarerMenuHeadRotation), // The rotation of the texture.
					EridaniHeadBase.Size() * 0.5f, //The centerpoint of the texture.
					1f, //The scale of the texture.
					SpriteEffects.None,
					0f);
					//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EBlink"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));

				}
			}
			

			if (ShadesVisible)//April fools stuff.
			{

				if (modPlayer.chosenStarfarer == 1)
				{
					//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/AShades"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));

				}
				if (modPlayer.chosenStarfarer == 2)
				{
					//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/EShades"), hitbox, Color.White * (modPlayer.starfarerMenuUIOpacity));

				}


			}

			Recalculate();


		}


		public override void Update(GameTime gameTime)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuUIOpacity < 0.1f)
			{
				area.Remove();
				return;

			}
			else
			{
				Append(area);
			}

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			if (!Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().archiveActive)
			{
				//area2.Remove();
				hoverText.Remove();
				description.Remove();
				confirmDialogue.Remove();
				rightButton.Remove();
				leftButton.Remove();

				IdleDialogue.Remove();
				BossDialogue.Remove();
				WeaponDialogue.Remove();
				VNDialogue.Remove();
			}
			else
			{
				//Append(area2);
				area.Append(IdleDialogue);
				area.Append(BossDialogue);
				area.Append(WeaponDialogue);
				area.Append(VNDialogue);
				if(modPlayer.canViewArchive)
                {
					area.Append(confirmDialogue);
				}
				else
                {
					confirmDialogue.Remove();
				}
				
				area.Append(hoverText);
				area.Append(description);
				area.Append(rightButton);
				area.Append(leftButton);
				
			}
			//area.HAlign = area.VAlign = 0.5f;
			
			if(voyage.IsMouseHovering)
            {
				//modPlayer.CelestialCartographyActive = true;
            }

			if(modPlayer.archiveActive)
            {
				area.Left.Set(0, 0f);
            }
			else
            {
				area.Left.Set(200, 0f);
			}
			// Setting the text per tick to update and show our resource values.

			hoverText.SetText($"{modPlayer.archiveListNumber}/{modPlayer.archiveListMax}");
			text.SetText($"{modPlayer.animatedStarfarerMenuDialogue}");
			description.SetText($"{modPlayer.archiveListInfo}");
			//abilityName.SetText($"{modPlayer.abilityName}");
			//abilitySubName.SetText($"{modPlayer.abilitySubName}");
			//abilityDescription.SetText($"{modPlayer.abilityDescription}");
			//starfarerBonus.SetText($"{modPlayer.starfarerBonus}");
			////baseStats.SetText($"{modPlayer.baseStats}");

			//if (modPlayer.hoverText != "")
			//{
			//	hoverText.SetText($"\n" +
			//		$"{Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().prismDescription}");
			//}
			//else
			////{
			//	modPlayer.prismDescription = "";
			//}

			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");

			//If the armor slot type corresponds to a certain item, treat it like the armor piece.
			modPlayer.starfarerOutfit = 0;
			modPlayer.starfarerOutfitVanity = 0;

			


			modPlayer.starfarerVanityEquipped = _starfarerVanitySlot.Item;
			modPlayer.starfarerArmorEquipped = _starfarerArmorSlot.Item;

			base.Update(gameTime);
		}

	}

}
