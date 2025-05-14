
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Utilities;
using StarsAbove.Items.Prisms;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using StarsAbove.Systems;
using StarsAbove.Systems.Items;

namespace StarsAbove.UI.StellarNova
{
    internal class StellarNovaUI : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIText warning;

		private UIElement starfarerPicture;

		private UIText hoverText;
		private UIElement area;
		private UIElement area2;
		private UIElement area3;
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

		private UIImageButton prototokia;
		private UIImageButton laevateinn;
		private UIImageButton kiwamiryuken;
		private UIImageButton gardenofavalon;
		private UIImageButton edingenesisquasar;
		private UIImageButton unlimitedbladeworks;
		private UIImageButton guardianslight;
        private UIImageButton fireflytypeiv;
        private UIImageButton origininfinity;

        private UIText abilityName;
		private UIText abilitySubName;
		private UIText abilityDescription;
		private UIText starfarerBonus;
		private UIText baseStats;
		private UIText modStats;
        private UIText setBonusInfo;

        static public VanillaItemSlotWrapper _affixSlot1;
		static public VanillaItemSlotWrapper _affixSlot2;
		static public VanillaItemSlotWrapper _affixSlot3;
		private VanillaItemSlotWrapper _affixSlotSpecial;

		private UIImageButton confirm;
		private UIImageButton reset;

		public bool dragging = false;

		static public bool loadPrisms;

		public override void OnInitialize()
		{


			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.

			starfarerPicture = new UIElement();
			starfarerPicture.Width.Set(400, 0f);
			starfarerPicture.Height.Set(400, 0f);

			area = new UIElement();
			//area.Left.Set(0, 0f); // Place the resource bar to the left of the hearts.
			//area.Top.Set(0, 0f);
			area.Width.Set(1000, 0f);
			area.Height.Set(700, 0f);
			//area.OnMouseDown += new UIElement.MouseEvent(DragStart);
			//area.OnMouseUp += new UIElement.MouseEvent(DragEnd);
			area.HAlign = 0.5f; // 1
			area.VAlign = 0.3f;

            barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			barFrame.Left.Set(318, 0f);
			barFrame.Top.Set(86, 0f);
			barFrame.Width.Set(514, 0f);
			barFrame.Height.Set(114, 0f);
			bg = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			bg.Left.Set(-250, 0f);
			bg.Top.Set(380, 0f);
			bg.Width.Set(840, 0f);
			bg.Height.Set(810, 0f);
			bg2 = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			bg2.Left.Set(-250, 0f);
			bg2.Top.Set(380, 0f);
			bg2.Width.Set(840, 0f);
			bg2.Height.Set(810, 0f);

			confirm = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Confirm"));
			confirm.OnLeftClick += Confirm;
			confirm.Width.Set(70, 0f);
			confirm.Height.Set(52, 0f);
			confirm.Left.Set(40, 0f);
			confirm.Top.Set(298, 0f);
			confirm.OnMouseOver += ConfirmHover;
			confirm.OnMouseOut += HoverOff;

			reset = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Reset"));
			reset.OnLeftClick += ResetAll;
			reset.Width.Set(70, 0f);
			reset.Height.Set(52, 0f);
			reset.Left.Set(40, 0f);
			reset.Top.Set(244, 0f);
			reset.OnMouseOver += ResetHover;
			reset.OnMouseOut += HoverOff;

			prototokia = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StellarNova/prototokia"));
			prototokia.OnLeftClick += prototokiaSelected;
			prototokia.Width.Set(98, 0f);
			prototokia.Height.Set(52, 0f);
			prototokia.Left.Set(864, 0f);
			prototokia.Top.Set(266, 0f);
			prototokia.OnMouseOver += prototokiaHover;
			prototokia.OnMouseOut += HoverOff;

			laevateinn = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StellarNova/laevateinn"));
			laevateinn.OnLeftClick += laevateinnSelected;
			laevateinn.Width.Set(98, 0f);
			laevateinn.Height.Set(52, 0f);
			laevateinn.Left.Set(864, 0f);
			laevateinn.Top.Set(266, 0f);
			laevateinn.OnMouseOver += LaevateinnHover;
			laevateinn.OnMouseOut += HoverOff;

			kiwamiryuken = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StellarNova/KiwamiRyuken"));
			kiwamiryuken.OnLeftClick += kiwamiryukenSelected;
			kiwamiryuken.Width.Set(98, 0f);
			kiwamiryuken.Height.Set(52, 0f);
			kiwamiryuken.Left.Set(864, 0f);
			kiwamiryuken.Top.Set(266, 0f);
			kiwamiryuken.OnMouseOver += kiwamiryukenHover;
			kiwamiryuken.OnMouseOut += HoverOff;

			gardenofavalon = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StellarNova/gardenofavalon"));
			gardenofavalon.OnLeftClick += gardenofavalonSelected;
			gardenofavalon.Width.Set(98, 0f);
			gardenofavalon.Height.Set(52, 0f);
			gardenofavalon.Left.Set(864, 0f);
			gardenofavalon.Top.Set(266, 0f);
			gardenofavalon.OnMouseOver += gardenofavalonHover;
			gardenofavalon.OnMouseOut += HoverOff;

			edingenesisquasar = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StellarNova/edingenesisquasar"));
			edingenesisquasar.OnLeftClick += edingenesisquasarSelected;
			edingenesisquasar.Width.Set(98, 0f);
			edingenesisquasar.Height.Set(52, 0f);
			edingenesisquasar.Left.Set(864, 0f);
			edingenesisquasar.Top.Set(266, 0f);
			edingenesisquasar.OnMouseOver += edingenesisquasarHover;
			edingenesisquasar.OnMouseOut += HoverOff;

			unlimitedbladeworks = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StellarNova/unlimitedbladeworks"));
			unlimitedbladeworks.OnLeftClick += unlimitedbladeworksSelected;
			unlimitedbladeworks.Width.Set(98, 0f);
			unlimitedbladeworks.Height.Set(52, 0f);
			unlimitedbladeworks.Left.Set(864, 0f);
			unlimitedbladeworks.Top.Set(266, 0f);
			unlimitedbladeworks.OnMouseOver += unlimitedbladeworksHover;
			unlimitedbladeworks.OnMouseOut += HoverOff;

			guardianslight = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StellarNova/guardianslight"));
			guardianslight.OnLeftClick += guardianslightSelected;
			guardianslight.Width.Set(98, 0f);
			guardianslight.Height.Set(52, 0f);
			guardianslight.Left.Set(864, 0f);
			guardianslight.Top.Set(266, 0f);
			guardianslight.OnMouseOver += guardianslightHover;
			guardianslight.OnMouseOut += HoverOff;

            fireflytypeiv = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StellarNova/fireflytypeIV"));
            fireflytypeiv.OnLeftClick += fireflytypeivSelected;
            fireflytypeiv.Width.Set(98, 0f);
            fireflytypeiv.Height.Set(52, 0f);
            fireflytypeiv.Left.Set(864, 0f);
            fireflytypeiv.Top.Set(266, 0f);
            fireflytypeiv.OnMouseOver += fireflytypeivHover;
            fireflytypeiv.OnMouseOut += HoverOff;

            origininfinity = new UIImageButton(Request<Texture2D>("StarsAbove/UI/StellarNova/origininfinity"));
            origininfinity.OnLeftClick += origininfinitySelected;
            origininfinity.Width.Set(98, 0f);
            origininfinity.Height.Set(52, 0f);
            origininfinity.Left.Set(864, 0f);
            origininfinity.Top.Set(266, 0f);
            origininfinity.OnMouseOver += origininfinityHover;
            origininfinity.OnMouseOut += HoverOff;
            /*Asphodene = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/Eridani"));
			Asphodene.OnMouseOver += MouseOverA;
			Asphodene.OnClick += MouseClickA;
			Asphodene.Top.Set(0, 0f);
			Asphodene.Left.Set(0, 0f);
			Asphodene.Width.Set(0, 0f);
			Asphodene.Height.Set(0, 0f);*/

            text = new UIText("", 2f);
			text.Width.Set(150, 0f);
			text.Height.Set(155, 0f);
			text.Top.Set(355, 0f);
			text.Left.Set(90, 0f);

			hoverText = new UIText("", 1f);
			hoverText.Width.Set(10, 0f);
			hoverText.Height.Set(10, 0f);
			hoverText.Top.Set(20, 0f);
			hoverText.Left.Set(20, 0f);

			description = new UIText("", 1f);
			description.Width.Set(10, 0f);
			description.Height.Set(10, 0f);
			description.Top.Set(10, 0f);
			description.Left.Set(20, 0f);

			abilityName = new UIText("", 2.5f);
			abilityName.Width.Set(10, 0f);
			abilityName.Height.Set(10, 0f);
			abilityName.Top.Set(270, 0f);
			abilityName.Left.Set(320, 0f);

			abilitySubName = new UIText("", 1f);
			abilitySubName.Width.Set(10, 0f);
			abilitySubName.Height.Set(10, 0f);
			abilitySubName.Top.Set(320, 0f);
			abilitySubName.Left.Set(320, 0f);

			abilityDescription = new UIText("", 0.7f);
			abilityDescription.Width.Set(10, 0f);
			abilityDescription.Height.Set(10, 0f);
			abilityDescription.Top.Set(385, 0f);
			abilityDescription.Left.Set(320, 0f);

			starfarerBonus = new UIText("", 0.65f);
			starfarerBonus.Width.Set(10, 0f);
			starfarerBonus.Height.Set(10, 0f);
			starfarerBonus.Top.Set(515, 0f);
			starfarerBonus.Left.Set(350, 0f);

			baseStats = new UIText("", 0.8f);
			baseStats.Width.Set(10, 0f);
			baseStats.Height.Set(10, 0f);
			baseStats.Top.Set(384, 0f);
			baseStats.Left.Set(128, 0f);

            modStats = new UIText("", 0.8f);
            modStats.Width.Set(10, 0f);
            modStats.Height.Set(10, 0f);
            modStats.Top.Set(10, 0f);
            modStats.Left.Set(128, 0f);

            setBonusInfo = new UIText("", 0.68f);
            setBonusInfo.Width.Set(10, 0f);
            setBonusInfo.Height.Set(10, 0f);
            setBonusInfo.Top.Set(10, 0f);
            setBonusInfo.Left.Set(128, 0f);

            gradientA = new Color(249, 133, 36); // 
			gradientB = new Color(255, 166, 83); //
												 //area3.Append(Asphodene);

			_affixSlot1 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 162 },
				Top = { Pixels = 620 },
				Width = { Pixels = 70 },
				Height = { Pixels = 70 },
				MaxWidth = { Pixels = 70 },
				MaxHeight = { Pixels = 70 },
				ValidItemFunc = item => item.IsAir || !item.IsAir,
				IgnoresMouseInteraction = false

            };
            _affixSlot1.OnMouseOver += AffixHover;
            _affixSlot1.OnMouseOut += HoverOff;

            _affixSlot2 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 264 },
				Top = { Pixels = 620 },
				MaxWidth = { Pixels = 70 },
				MaxHeight = { Pixels = 70 },
				
				ValidItemFunc = item => item.IsAir || !item.IsAir,
				IgnoresMouseInteraction = false
            };
            _affixSlot2.OnMouseOver += AffixHover;
            _affixSlot2.OnMouseOut += HoverOff;

            _affixSlot3 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 364 },
				Top = { Pixels = 620 },
				Width = { Pixels = 70 },
				Height = { Pixels = 70 },
				MaxWidth = { Pixels = 70 },
				MaxHeight = { Pixels = 70 },

				ValidItemFunc = item => item.IsAir || !item.IsAir,
				IgnoresMouseInteraction = false
			};
			_affixSlot3.OnMouseOver += AffixHover;
			_affixSlot3.OnMouseOut += HoverOff;
			
			
			
			//area.Append(_affixSlotSpecial);

			area.Append(starfarerPicture);
			area.Append(bg2);
			area.Append(bg);
			
			area.Append(barFrame);
			barFrame.Append(description);

			area.Append(prototokia);

			area.Append(abilitySubName);
			area.Append(abilityDescription);
			area.Append(starfarerBonus);
			area.Append(baseStats);
            area.Append(modStats);
            area.Append(setBonusInfo);
            area.Append(confirm);
			area.Append(reset);
			area.Append(_affixSlot1);
            area.Append(_affixSlot2);
            area.Append(_affixSlot3);
            Append(area);
			Append(hoverText);

		}

		private void ResetAll(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			player.chosenStellarNova = 0;
			if (!_affixSlot1.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _affixSlot1.Item, _affixSlot1.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				_affixSlot1.Item.TurnToAir();
			}
			if (!_affixSlot2.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _affixSlot2.Item, _affixSlot2.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				_affixSlot2.Item.TurnToAir();
			}
			if (!_affixSlot3.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _affixSlot3.Item, _affixSlot3.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				_affixSlot3.Item.TurnToAir();
			}
			// Remember to add to here
		}
		private void Confirm(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			player.novaUIActive = false;
			player.starfarerMenuActive = true;
			player.description = "";
			player.textVisible = false;
			player.novaDialogueScrollNumber = 0;
			player.novaDialogueScrollTimer = 0;
			player.starfarerMenuDialogueScrollNumber = 0;
			player.starfarerMenuDialogueScrollTimer = 0;
			player.stellarArrayMoveIn = -15f;

			
			int randomDialogue = Main.rand.Next(0, 3);
			if (randomDialogue == 0)
			{
				if (player.chosenStarfarer == 1)
				{
					player.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterNovaDialogue.Asphodene.1", Main.LocalPlayer);
				}
				if (player.chosenStarfarer == 2)
				{
					player.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterNovaDialogue.Eridani.1", Main.LocalPlayer);
				}

			}
			if (randomDialogue == 1)
			{
				if (player.chosenStarfarer == 1)
				{
					player.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterNovaDialogue.Asphodene.2", Main.LocalPlayer);
					
				}
				if (player.chosenStarfarer == 2)
				{
					player.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterNovaDialogue.Eridani.2", Main.LocalPlayer);
					
				}
			}
			if (randomDialogue == 2)
			{
				if (player.chosenStarfarer == 1)
				{
					player.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterNovaDialogue.Asphodene.3", Main.LocalPlayer);
				}
				if (player.chosenStarfarer == 2)
				{
					player.starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterNovaDialogue.Eridani.3", Main.LocalPlayer);
				}
			}
			//player.animatedDescription = "";
			

			// We can do stuff in here!
		}
		private void prototokiaSelected(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;


			player.description = "";
			player.textVisible = false;
			//player.animatedDescription = "";
			player.novaDialogueScrollNumber = 0;
			player.novaDialogueScrollTimer = 0;
			if (player.prototokia == 1)
			{
				player.chosenStellarNova = 1;
			}
			else
			{


			}


			// We can do stuff in here!
		}
		private void laevateinnSelected(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;


			player.description = "";
			player.textVisible = false;
			//player.animatedDescription = "";
			player.novaDialogueScrollNumber = 0;
			player.novaDialogueScrollTimer = 0;
			if (player.laevateinn == 1)
			{
				player.chosenStellarNova = 2;
			}
			else
			{


			}


			// We can do stuff in here!
		}
		private void kiwamiryukenSelected(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;


			player.description = "";
			player.textVisible = false;
			//player.animatedDescription = "";
			player.novaDialogueScrollNumber = 0;
			player.novaDialogueScrollTimer = 0;
			if (player.kiwamiryuken == 1)
			{
				player.chosenStellarNova = 3;
			}
			else
			{


			}


			// We can do stuff in here!
		}
		private void gardenofavalonSelected(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;


			player.description = "";
			player.textVisible = false;
			//player.animatedDescription = "";
			player.novaDialogueScrollNumber = 0;
			player.novaDialogueScrollTimer = 0;
			if (player.gardenofavalon == 1)
			{
				player.chosenStellarNova = 4;
			}
			else
			{


			}


			// We can do stuff in here!
		}
        private void fireflytypeivSelected(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
                return;


            player.description = "";
            player.textVisible = false;
            //player.animatedDescription = "";
            player.novaDialogueScrollNumber = 0;
            player.novaDialogueScrollTimer = 0;
            if (player.fireflytypeiv == 1)
            {
                player.chosenStellarNova = 8;
            }
            else
            {


            }


            // We can do stuff in here!
        }
        private void origininfinitySelected(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
                return;


            player.description = "";
            player.textVisible = false;
            //player.animatedDescription = "";
            player.novaDialogueScrollNumber = 0;
            player.novaDialogueScrollTimer = 0;
            if (player.origininfinity == 1)
            {
                player.chosenStellarNova = 9;
            }
            else
            {


            }


            // We can do stuff in here!
        }
        private void edingenesisquasarSelected(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;

			player.description = "";
			player.textVisible = false;
			//player.animatedDescription = "";
			player.novaDialogueScrollNumber = 0;
			player.novaDialogueScrollTimer = 0;
			if (player.edingenesisquasar == 1)
			{
				player.chosenStellarNova = 5;
			}
			else
			{


			}


			// We can do stuff in here!
		}
		private void unlimitedbladeworksSelected(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;

			player.description = "";
			player.textVisible = false;
			//player.animatedDescription = "";
			player.novaDialogueScrollNumber = 0;
			player.novaDialogueScrollTimer = 0;
			if (player.unlimitedbladeworks == 1)
			{
				player.chosenStellarNova = 6;
			}
			else
			{


			}


			// We can do stuff in here!
		}
		private void guardianslightSelected(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;

			player.description = "";
			player.textVisible = false;
			//player.animatedDescription = "";
			player.novaDialogueScrollNumber = 0;
			player.novaDialogueScrollTimer = 0;
			if (player.guardianslight == 1)
			{
				player.chosenStellarNova = 7;
			}
			else
			{


			}


			// We can do stuff in here!
		}
		private void AffixHover(UIMouseEvent evt, UIElement listeningElement)
		{
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
                player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.Prisms.Asphodene", Main.LocalPlayer);


            }
            if (player.chosenStarfarer == 2)
			{
                player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.Prisms.Eridani", Main.LocalPlayer);


            }
            player.hoverText = $"{player.affix1}";
			player.textVisible = true;
			// We can do stuff in here!
		}
		
		private void ConfirmHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
				player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.MiscInfo.Confirm.Asphodene", Main.LocalPlayer);

			}
			if (player.chosenStarfarer == 2)
			{
				player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.MiscInfo.Confirm.Eridani", Main.LocalPlayer);

			}
			player.textVisible = true;
			// We can do stuff in here!
		}

		private void ResetHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
				player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.MiscInfo.Reset.Asphodene", Main.LocalPlayer);

			}
			if (player.chosenStarfarer == 2)
			{
				player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.MiscInfo.Reset.Eridani", Main.LocalPlayer);

			}
			player.textVisible = true;


			// We can do stuff in here!
		}
		private void prototokiaHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
				if (player.prototokia != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.PrototokiaAster.Unlocked.Asphodene", Main.LocalPlayer);

				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.PrototokiaAster.Locked.Asphodene", Main.LocalPlayer);

				}

			}
			if (player.chosenStarfarer == 2)
			{
				if (player.prototokia != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.PrototokiaAster.Unlocked.Eridani", Main.LocalPlayer);

				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.PrototokiaAster.Locked.Eridani", Main.LocalPlayer);

				}

			}
			player.textVisible = true;


			// We can do stuff in here!
		}
		private void LaevateinnHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
				if (player.laevateinn != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.ArsLaevateinn.Unlocked.Asphodene", Main.LocalPlayer);

				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.ArsLaevateinn.Locked.Asphodene", Main.LocalPlayer);

				}

			}
			if (player.chosenStarfarer == 2)
			{
				if (player.laevateinn != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.ArsLaevateinn.Unlocked.Eridani", Main.LocalPlayer);

				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.ArsLaevateinn.Locked.Eridani", Main.LocalPlayer);

				}

			}
			player.textVisible = true;


			// We can do stuff in here!
		}
		private void kiwamiryukenHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
				if (player.kiwamiryuken != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.KiwamiRyuken.Unlocked.Asphodene", Main.LocalPlayer);
					
				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.KiwamiRyuken.Locked.Asphodene", Main.LocalPlayer);

				}

			}
			if (player.chosenStarfarer == 2)
			{
				if (player.kiwamiryuken != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.KiwamiRyuken.Unlocked.Eridani", Main.LocalPlayer);
					
				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.KiwamiRyuken.Locked.Eridani", Main.LocalPlayer);

				}

			}
			player.textVisible = true;


			// We can do stuff in here!
		}
		private void gardenofavalonHover(UIMouseEvent evt, UIElement listeningElement)
		{
			var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
				if (player.gardenofavalon != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.GardenOfAvalon.Unlocked.Asphodene", Main.LocalPlayer);
					//"With this Stellar Nova, you can imbue your attacks directly," +
					//	"\noverwriting their stats as well as gaining a new burst attack.";
				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.GardenOfAvalon.Locked.Asphodene", Main.LocalPlayer);

				}

			}
			if (player.chosenStarfarer == 2)
			{
				if (player.gardenofavalon != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.GardenOfAvalon.Unlocked.Eridani", Main.LocalPlayer);
				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.GardenOfAvalon.Locked.Eridani", Main.LocalPlayer);

				}

			}
			player.textVisible = true;


			// We can do stuff in here!
		}
        private void fireflytypeivHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
                return;
            if (player.chosenStarfarer == 1)
            {
                if (player.fireflytypeiv != 0)
                {
                    player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.FireflyTypeIV.Unlocked.Asphodene", Main.LocalPlayer);
                    
                }
                else
                {
                    player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.FireflyTypeIV.Locked.Asphodene", Main.LocalPlayer);

                }

            }
            if (player.chosenStarfarer == 2)
            {
                if (player.fireflytypeiv != 0)
                {
                    player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.FireflyTypeIV.Unlocked.Eridani", Main.LocalPlayer);
                }
                else
                {
                    player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.FireflyTypeIV.Locked.Eridani", Main.LocalPlayer);

                }

            }
            player.textVisible = true;


            // We can do stuff in here!
        }
        private void origininfinityHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
                return;
            if (player.chosenStarfarer == 1)
            {
                if (player.origininfinity != 0)
                {
                    player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.OriginInfinity.Unlocked.Asphodene", Main.LocalPlayer);

                }
                else
                {
                    player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.OriginInfinity.Locked.Asphodene", Main.LocalPlayer);

                }

            }
            if (player.chosenStarfarer == 2)
            {
                if (player.origininfinity != 0)
                {
                    player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.OriginInfinity.Unlocked.Eridani", Main.LocalPlayer);
                }
                else
                {
                    player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.OriginInfinity.Locked.Eridani", Main.LocalPlayer);

                }

            }
            player.textVisible = true;


            // We can do stuff in here!
        }
        private void edingenesisquasarHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
				if (player.edingenesisquasar != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.EdinGenesisQuasar.Unlocked.Asphodene", Main.LocalPlayer);
					//"With this Stellar Nova, you can imbue your attacks directly," +
					//	"\noverwriting their stats as well as gaining a new burst attack.";
				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.EdinGenesisQuasar.Locked.Asphodene", Main.LocalPlayer);

				}

			}
			if (player.chosenStarfarer == 2)
			{
				if (player.edingenesisquasar != 0)
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.EdinGenesisQuasar.Unlocked.Eridani", Main.LocalPlayer);
				}
				else
				{
					player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.EdinGenesisQuasar.Locked.Eridani", Main.LocalPlayer);

				}

			}
			player.textVisible = true;


			// We can do stuff in here!
		}
		private void unlimitedbladeworksHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
				player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.UnlimitedBladeWorks.Unlocked.Asphodene", Main.LocalPlayer);


			}
			if (player.chosenStarfarer == 2)
			{
				player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.UnlimitedBladeWorks.Unlocked.Eridani", Main.LocalPlayer);


			}
			player.textVisible = true;


			// We can do stuff in here!
		}
		private void guardianslightHover(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			if (player.chosenStarfarer == 1)
			{
				player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.GuardiansLight.Unlocked.Asphodene", Main.LocalPlayer);


			}
			if (player.chosenStarfarer == 2)
			{
				player.description = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.GuardiansLight.Unlocked.Eridani", Main.LocalPlayer);


			}
			player.textVisible = true;


			// We can do stuff in here!
		}
		private void HoverOff(UIMouseEvent evt, UIElement listeningElement)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || !player.novaUIActive)
				return;
			player.description = "";
			player.hoverText = " ";
			player.animatedDescription = "";
			player.novaDialogueScrollNumber = 0;
			player.novaDialogueScrollTimer = 0;
			player.textVisible = false;
			// We can do stuff in here!
		}
		public override void Draw(SpriteBatch spriteBatch)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            // This prevents drawing unless we are using an ExampleDamageItem
            if (player.chosenStarfarer == 0 || player.novaUIOpacity < 0.1f)
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            base.DrawSelf(spriteBatch);
            // Calculate quotient
            hoverText.Left.Set(Main.mouseX + 20, 0f); // Place the resource bar to the left of the hearts.
            hoverText.Top.Set(Main.mouseY, 0f); // Placing it just a bit below the top of the screen.

            UI.StarfarerMenu.StarfarerMenu.AdjustAreaBasedOnPlayerVelocity(ref area, 0, 0);

            Rectangle hitbox = area.GetInnerDimensions().ToRectangle();
            Rectangle starfarer = starfarerPicture.GetInnerDimensions().ToRectangle();
            starfarerPicture.Top.Set(-40, 0f);
            Rectangle starfarerBody = starfarerPicture.GetInnerDimensions().ToRectangle();
            starfarerBody.Y += 000;
            Rectangle trim = new Rectangle(0, 0, 400, 400);

            //Rectangle trim = new Rectangle(Main.screenWidth / 2, Main.screenHeight / 2, 600, 400);
            Rectangle dialogue = barFrame.GetInnerDimensions().ToRectangle();
            dialogue.Y -= modPlayer.descriptionY;
            description.Top.Set(35 - modPlayer.descriptionY, 0f);

            modStats.Top.Set(415, 0f);

            Rectangle prototokiaArea = prototokia.GetInnerDimensions().ToRectangle();
            Rectangle laevateinnArea = laevateinn.GetInnerDimensions().ToRectangle();
            Rectangle kiwamiryukenArea = kiwamiryuken.GetInnerDimensions().ToRectangle();
            Rectangle gardenofavalonArea = gardenofavalon.GetInnerDimensions().ToRectangle();
            Rectangle edingenesisquasarArea = edingenesisquasar.GetInnerDimensions().ToRectangle();
            Rectangle unlimitedbladeworksArea = unlimitedbladeworks.GetInnerDimensions().ToRectangle();
            Rectangle guardianslightArea = guardianslight.GetInnerDimensions().ToRectangle();
            Rectangle fireflytypeIVArea = fireflytypeiv.GetInnerDimensions().ToRectangle();
            Rectangle origininfinityArea = origininfinity.GetInnerDimensions().ToRectangle();


            Sprites(spriteBatch, modPlayer, starfarer, starfarerBody, trim);

            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/NovaUI"), hitbox, Color.White * (modPlayer.novaUIOpacity));

            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/description"), dialogue, Color.White * (modPlayer.descriptionOpacity));

            Localization(spriteBatch, modPlayer, hitbox, prototokiaArea, laevateinnArea, kiwamiryukenArea, gardenofavalonArea, edingenesisquasarArea, unlimitedbladeworksArea, guardianslightArea, fireflytypeIVArea, origininfinityArea);
          
            if (_affixSlot1.Item != null)
            {
                if (!_affixSlot1.Item.IsAir )
                {
					if(_affixSlot1.Item.GetGlobalItem<ItemPrismSystem>().isPrism)
					{
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/affix1"), hitbox, Color.White * (modPlayer.novaUIOpacity));

                    }
                }
            }
            if (_affixSlot2.Item != null)
            {
                if (!_affixSlot2.Item.IsAir)
                {
                    if (_affixSlot2.Item.GetGlobalItem<ItemPrismSystem>().isPrism)
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/affix2"), hitbox, Color.White * (modPlayer.novaUIOpacity));

                    }
                }
            }
            if (_affixSlot3.Item != null)
            {
                if (!_affixSlot3.Item.IsAir)
                {
                    if (_affixSlot3.Item.GetGlobalItem<ItemPrismSystem>().isPrism)
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/affix3"), hitbox, Color.White * (modPlayer.novaUIOpacity));

                    }
                }
            }

            Blinking(spriteBatch, modPlayer, starfarer);
            Recalculate();
        }

        private static void Sprites(SpriteBatch spriteBatch, StarsAbovePlayer modPlayer, Rectangle starfarer, Rectangle starfarerBody, Rectangle trim)
        {
            if (modPlayer.chosenStarfarer == 1)
            {

                if (modPlayer.starfarerHairstyle == 1)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As0HairBAlt1"), starfarer, Color.White * (modPlayer.novaUIOpacity));

                }
                else
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As0HairBH"), starfarer, Color.White * (modPlayer.novaUIOpacity));

                }
                //Draw the head, accounting for pose.
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As0Head"), starfarer, Color.White * (modPlayer.novaUIOpacity));//TODO: MAKE A BOX (HITBOX SHOULD BE OG SIZE, SOURCERECTANGLE SHOULD BE NEW SIZE)

                //Draw the body, accounting for outfits and pose.
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As0Body" + modPlayer.starfarerOutfitVisible), starfarerBody, trim, Color.White * (modPlayer.novaUIOpacity));

                //Draw the hair on top of the head. Same deal with color change.
                if (modPlayer.starfarerHairstyle == 1)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As0HairAlt1H"), starfarer, Color.White * (modPlayer.novaUIOpacity));

                }
                else
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As0HairH"), starfarer, Color.White * (modPlayer.novaUIOpacity));

                }
                //Draw the expression, accounting for pose.
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As00"), starfarer, Color.White * (modPlayer.novaUIOpacity));//Base character's expression


            }
            if (modPlayer.chosenStarfarer == 2)
            {
				if(modPlayer.starfarerHairstyle == 1)
				{

				}
				else
				{
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0HairBH"), starfarer, Color.White * (modPlayer.novaUIOpacity));

                }



                //Draw the head, accounting for pose.
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0Head"), starfarer, Color.White * (modPlayer.novaUIOpacity));//TODO: MAKE A BOX (HITBOX SHOULD BE OG SIZE, SOURCERECTANGLE SHOULD BE NEW SIZE)

                //Draw the body, accounting for outfits and pose.
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0Body" + modPlayer.starfarerOutfitVisible), starfarerBody, trim, Color.White * (modPlayer.novaUIOpacity));

                //Draw the hair on top of the head. Same deal with color change.
                if (modPlayer.starfarerHairstyle == 1)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0HairAlt1H"), starfarer, Color.White * (modPlayer.novaUIOpacity));

                }
                else
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0HairH"), starfarer, Color.White * (modPlayer.novaUIOpacity));

                }
                //Draw the expression, accounting for pose.
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er00"), starfarer, Color.White * (modPlayer.novaUIOpacity));//Base character's expression


            }
        }

        private static void Localization(SpriteBatch spriteBatch, StarsAbovePlayer modPlayer, Rectangle hitbox, Rectangle prototokiaArea, Rectangle laevateinnArea, Rectangle kiwamiryukenArea, Rectangle gardenofavalonArea, Rectangle edingenesisquasarArea, Rectangle unlimitedbladeworksArea, Rectangle guardianslightArea, Rectangle fireflytypeIVArea, Rectangle origininfinityArea)
        {
            if (Language.ActiveCulture.LegacyId == ((int)GameCulture.CultureName.Chinese))
            {
                switch (modPlayer.chosenStellarNova)
                {
                    case 1:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/prototokia"), prototokiaArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/CN/prototokiaIconCN"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 2:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/laevateinn"), laevateinnArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/CN/laevateinnIconCN"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 3:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/KiwamiRyuken"), kiwamiryukenArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/CN/KiwamiRyukenIconCN"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 4:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/gardenofavalon"), gardenofavalonArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/CN/gardenofavalonIconCN"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 5:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/edingenesisquasar"), edingenesisquasarArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/CN/edingenesisquasarIconCN"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 6:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/unlimitedbladeworks"), unlimitedbladeworksArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/CN/unlimitedbladeworksIconCN"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 7:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/guardianslight"), guardianslightArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/CN/guardianslightIconCN"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 8:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/fireflytypeIV"), fireflytypeIVArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/CN/fireflytypeIVIconCN"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 9:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/origininfinity"), origininfinityArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/CN/origininfinityCN"), hitbox, Color.White * (modPlayer.novaUIOpacity));//Temp
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (modPlayer.chosenStellarNova)
                {
                    case 1:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/prototokia"), prototokiaArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/prototokiaIcon"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 2:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/laevateinn"), laevateinnArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/laevateinnIcon"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 3:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/KiwamiRyuken"), kiwamiryukenArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/KiwamiRyukenIcon"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 4:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/gardenofavalon"), gardenofavalonArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/gardenofavalonIcon"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 5:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/edingenesisquasar"), edingenesisquasarArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/edingenesisquasarIcon"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 6:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/unlimitedbladeworks"), unlimitedbladeworksArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/unlimitedbladeworksIcon"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 7:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/guardianslight"), guardianslightArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/guardianslightIcon"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 8:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/fireflytypeIV"), fireflytypeIVArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/fireflytypeIVIcon"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    case 9:
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/origininfinity"), origininfinityArea, Color.White * (modPlayer.novaUIOpacity));
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/origininfinityIcon"), hitbox, Color.White * (modPlayer.novaUIOpacity));
                        break;
                    default:
                        break;
                }
            }
        }

        private static void Blinking(SpriteBatch spriteBatch, StarsAbovePlayer modPlayer, Rectangle starfarer)
        {
            if ((modPlayer.blinkTimer > 70 && modPlayer.blinkTimer < 75) || (modPlayer.blinkTimer > 320 && modPlayer.blinkTimer < 325) || (modPlayer.blinkTimer > 420 && modPlayer.blinkTimer < 425) || (modPlayer.blinkTimer > 428 && modPlayer.blinkTimer < 433))
            {

                if (modPlayer.chosenStarfarer == 1)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As0b"), starfarer, Color.White * (modPlayer.novaUIOpacity));

                }
                if (modPlayer.chosenStarfarer == 2)
                {
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0b"), starfarer, Color.White * (modPlayer.novaUIOpacity));

                }


            }
        }

        static int topStatic = 206;
		int availableNovas;

		public override void Update(GameTime gameTime)
        {
            var player = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

            if (player.chosenStarfarer == 0 || player.novaUIOpacity < 0.1f)
			{
				area.Remove();
				return;

			}
			else
			{
				Append(area);
			}


			var modPlayer = player;
			//prototokia is unlocked at base, so there's always 1 available Nova
			availableNovas = 0;
			topStatic = 240;
			int topAdjustment = 25;
			int multiplierAdjustment = 70;
			if (modPlayer.prototokia > 0)
			{
				topStatic -= topAdjustment;

				area.Append(prototokia);
				prototokia.Top.Set(topStatic + (availableNovas * multiplierAdjustment), 0f);
				availableNovas++;
			}
			if (modPlayer.unlimitedbladeworks > 0)
			{
				topStatic -= topAdjustment;

				area.Append(unlimitedbladeworks);
				unlimitedbladeworks.Top.Set(topStatic + (availableNovas * multiplierAdjustment), 0f);

				availableNovas++;
			}
			else
			{
				unlimitedbladeworks.Remove();
			}
			if (modPlayer.guardianslight > 0)
			{
				topStatic -= topAdjustment;

				area.Append(guardianslight);
				guardianslight.Top.Set(topStatic + (availableNovas * multiplierAdjustment), 0f);

				availableNovas++;
			}
			else
			{
				guardianslight.Remove();
            }
            if (modPlayer.kiwamiryuken > 0)
            {
                topStatic -= topAdjustment;

                area.Append(kiwamiryuken);
                kiwamiryuken.Top.Set(topStatic + (availableNovas * multiplierAdjustment), 0f);

                availableNovas++;
            }
            else
            {
                kiwamiryuken.Remove();
            }
			if (modPlayer.gardenofavalon > 0)
			{
				topStatic -= topAdjustment;

				area.Append(gardenofavalon);
				gardenofavalon.Top.Set(topStatic + (availableNovas * multiplierAdjustment), 0f);

				availableNovas++;
			}
			else
			{
				gardenofavalon.Remove();
			}
            if (modPlayer.fireflytypeiv > 0)
            {
                topStatic -= topAdjustment;

                area.Append(fireflytypeiv);
                fireflytypeiv.Top.Set(topStatic + (availableNovas * multiplierAdjustment), 0f);

                availableNovas++;
            }
            else
            {
                fireflytypeiv.Remove();
            }
            if (modPlayer.laevateinn > 0)
			{
				topStatic -= topAdjustment;

				area.Append(laevateinn);
				laevateinn.Top.Set(topStatic + (availableNovas * multiplierAdjustment), 0f);

				availableNovas++;
			}
			else
            {
				laevateinn.Remove();
				
			}
            if (modPlayer.origininfinity > 0)
            {
                topStatic -= topAdjustment;

                area.Append(origininfinity);
                origininfinity.Top.Set(topStatic + (availableNovas * multiplierAdjustment), 0f);

                availableNovas++;
            }
            else
            {
                edingenesisquasar.Remove();
            }
            if (modPlayer.edingenesisquasar > 0)
			{
				topStatic -= topAdjustment;

				area.Append(edingenesisquasar);
				edingenesisquasar.Top.Set(topStatic + (availableNovas * multiplierAdjustment), 0f);

				availableNovas++;
			}
			else
            {
				edingenesisquasar.Remove();
            }
			
			

			// Setting the text per tick to update and show our resource values.

			description.SetText($"{modPlayer.animatedDescription}");

			abilityName.SetText($"{modPlayer.abilityName}");
			abilitySubName.SetText($"{modPlayer.abilitySubName}");
			abilityDescription.SetText($"{modPlayer.abilityDescription}");
			starfarerBonus.SetText($"{modPlayer.starfarerBonus}");
			baseStats.SetText($"{modPlayer.baseStats}");
			modStats.SetText($"{modPlayer.modStats}");
			setBonusInfo.Top.Set(587,0f);
			setBonusInfo.Left.Set(452, 0f);
            setBonusInfo.SetText($"{modPlayer.setBonusInfo}");

            if (modPlayer.hoverText != "")
			{
				hoverText.SetText($"\n" +
					$"{player.prismDescription}");
			}
			else
			{
				modPlayer.prismDescription = "";
			}

            if (_affixSlot1.Item != null)
            {
				modPlayer.affix1 = _affixSlot1.Item.Name;

			}
			if (_affixSlot2.Item != null)
			{
				modPlayer.affix2 = _affixSlot2.Item.Name;

			}
			if (_affixSlot3.Item != null)
			{
				modPlayer.affix3 = _affixSlot3.Item.Name;

			}

			modPlayer.affixItem1 = _affixSlot1.Item;
			modPlayer.affixItem2 = _affixSlot2.Item;
			modPlayer.affixItem3 = _affixSlot3.Item;

			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
			base.Update(gameTime);
		}

		

	}

}
