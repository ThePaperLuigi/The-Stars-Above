
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.Starfarers
{
    internal class StellarArray : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIText warning;

		private UIText passiveTitle;
		private UIText description;
		private UIText EridaniText;
		private UIElement area;
		private UIElement area2;
		private UIElement area3;
		private UIImage barFrame;
		private UIImage bg;

		private UIImageButton starshower;
		private UIImageButton butchersdozen;
		private UIImageButton ironskin;
		private UIImageButton evasionmastery;
		private UIImageButton bonus100hp;
		private UIImageButton beyondinfinity;
		private UIImageButton aquaaffinity;
		private UIImageButton bloomingflames;
		private UIImageButton astralmantle;
		private UIImageButton inneralchemy;
		private UIImageButton hikari;
		private UIImageButton avataroflight;
		private UIImageButton keyofchronology;
		private UIImageButton livingdead;
		private UIImageButton umbralentropy;
		private UIImageButton celestialevanesence;
		private UIImageButton afterburner;
		private UIImageButton weaknessexploit;
		private UIImageButton unbridledradiance;
		private UIImageButton mysticforging;
		private UIImageButton beyondtheboundary;
		private UIImageButton artofwar;
		private UIImageButton flashfreeze;
		private UIImageButton aprismatism;

		private UIImageButton MeleeAspect;
		private UIImageButton MagicAspect;
		private UIImageButton RangedAspect;
		private UIImageButton SummonAspect;
		private UIImageButton RogueAspect;

		private UIImageButton BardAspect;
		private UIImageButton HealerAspect;
		private UIImageButton ThrowerAspect;

		private UIImageButton Lock;

		private UIImageButton confirm;
		private UIImageButton reset;

		

		private UIImage bulletIndicatorOn;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;
		private Vector2 offset;
		public bool dragging = false;

		public override void OnInitialize() {
			
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement(); 
			area.Left.Set(30, 0f); // Place the resource bar to the left of the hearts.
			area.Top.Set(100, 0f); 
			area.Width.Set(700, 0f); 
			area.Height.Set(400, 0f);
			//area.OnMouseDown += new UIElement.MouseEvent(DragStart);
			//area.OnMouseUp += new UIElement.MouseEvent(DragEnd);
			area.HAlign = area.VAlign = 0.5f; // 1

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);

			confirm = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Confirm"));
			confirm.OnClick += Confirm;
			confirm.Width.Set(70, 0f);
			confirm.Height.Set(52, 0f);
			confirm.Left.Set(0, 0f);
			confirm.Top.Set(174, 0f);
			confirm.OnMouseOver += ConfirmHover;
			confirm.OnMouseOut += HoverOff;

			reset = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Reset"));
			reset.OnClick += ResetAll;
			reset.Width.Set(70, 0f);
			reset.Height.Set(52, 0f);
			reset.Left.Set(0, 0f);
			reset.Top.Set(74, 0f);
			reset.OnMouseOver += ResetHover;
			reset.OnMouseOut += HoverOff;

			butchersdozen = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/butchersdozen") );
			butchersdozen.OnClick += ButchersDozenClick;
			butchersdozen.Width.Set(56, 0f);
			butchersdozen.Height.Set(44, 0f);
			butchersdozen.Left.Set(78, 0f);
			butchersdozen.Top.Set(200, 0f);//Row 4
			butchersdozen.OnMouseOver += ButchersDozenHover;
			butchersdozen.OnMouseOut += HoverOff;

			starshower = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Starshower") );
			starshower.OnClick += StarshowerClick;
			starshower.Width.Set(56, 0f);
			starshower.Height.Set(44, 0f);
			starshower.Left.Set(78, 0f);
			starshower.Top.Set(56, 0f);
			starshower.OnMouseOver += StarshowerHover;
			starshower.OnMouseOut += HoverOff;

			MeleeAspect = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Melee") );
			MeleeAspect.OnClick += MeleeAspectClick;
			MeleeAspect.Width.Set(32, 0f);
			MeleeAspect.Height.Set(44, 0f);
			MeleeAspect.Left.Set(86, 0f);
			MeleeAspect.Top.Set(264, 0f);
			MeleeAspect.OnMouseOver += MeleeAspectHover;
			MeleeAspect.OnMouseOut += HoverOff;

			RogueAspect = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Rogue"));
			RogueAspect.OnClick += RogueAspectClick;
			RogueAspect.Width.Set(32, 0f);
			RogueAspect.Height.Set(44, 0f);
			RogueAspect.Left.Set(86, 0f);
			RogueAspect.Top.Set(314, 0f);
			RogueAspect.OnMouseOver += RogueAspectHover;
			RogueAspect.OnMouseOut += HoverOff;

			BardAspect = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Symphonic"));
			BardAspect.OnClick += BardAspectClick;
			BardAspect.Width.Set(32, 0f);
			BardAspect.Height.Set(44, 0f);
			BardAspect.Left.Set(128, 0f);
			BardAspect.Top.Set(314, 0f);
			BardAspect.OnMouseOver += BardAspectHover;
			BardAspect.OnMouseOut += HoverOff;

			HealerAspect = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Radiant"));
			HealerAspect.OnClick += HealerAspectClick;
			HealerAspect.Width.Set(32, 0f);
			HealerAspect.Height.Set(44, 0f);
			HealerAspect.Left.Set(212, 0f);
			HealerAspect.Top.Set(314, 0f);
			HealerAspect.OnMouseOver += HealerAspectHover;
			HealerAspect.OnMouseOut += HoverOff;

			ThrowerAspect = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Thrower"));
			ThrowerAspect.OnClick += ThrowerAspectClick;
			ThrowerAspect.Width.Set(32, 0f);
			ThrowerAspect.Height.Set(44, 0f);
			ThrowerAspect.Left.Set(170, 0f);
			ThrowerAspect.Top.Set(314, 0f);
			ThrowerAspect.OnMouseOver += ThrowerAspectHover;
			ThrowerAspect.OnMouseOut += HoverOff;

			MagicAspect = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Magic") );
			MagicAspect.OnClick += MagicAspectClick;
			MagicAspect.Width.Set(32, 0f);
			MagicAspect.Height.Set(44, 0f);
			MagicAspect.Left.Set(128, 0f);
			MagicAspect.Top.Set(264, 0f);
			MagicAspect.OnMouseOver += MagicAspectHover;
			MagicAspect.OnMouseOut += HoverOff;

			RangedAspect = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Ranged") );
			RangedAspect.OnClick += RangedAspectClick;
			RangedAspect.Width.Set(32, 0f);
			RangedAspect.Height.Set(44, 0f);
			RangedAspect.Left.Set(170, 0f);
			RangedAspect.Top.Set(264, 0f);
			RangedAspect.OnMouseOver += RangedAspectHover;
			RangedAspect.OnMouseOut += HoverOff;

			SummonAspect = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Summon") );
			SummonAspect.OnClick += SummonAspectClick;
			SummonAspect.Width.Set(32, 0f);
			SummonAspect.Height.Set(44, 0f);
			SummonAspect.Left.Set(212, 0f);
			SummonAspect.Top.Set(264, 0f);
			SummonAspect.OnMouseOver += SummonAspectHover;
			SummonAspect.OnMouseOut += HoverOff;

			Lock = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Lock") );
			Lock.OnClick += LockClick;
			Lock.Width.Set(16, 0f);
			Lock.Height.Set(22, 0f);
			Lock.Left.Set(59, 0f);
			Lock.Top.Set(284, 0f);
			Lock.OnMouseOver += LockHover;
			Lock.OnMouseOut += HoverOff;

			ironskin = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/ironskin") );
			ironskin.OnClick += IronskinClick;
			ironskin.Width.Set(56, 0f);
			ironskin.Height.Set(44, 0f);
			ironskin.Left.Set(136, 0f);
			ironskin.Top.Set(56, 0f);
			ironskin.OnMouseOver += IronskinHover;
			ironskin.OnMouseOut += HoverOff;

			evasionmastery = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/evasionmastery") );
			evasionmastery.OnClick += evasionmasteryClick;
			evasionmastery.Width.Set(56, 0f);
			evasionmastery.Height.Set(44, 0f);
			evasionmastery.Left.Set(194, 0f);
			evasionmastery.Top.Set(56, 0f);
			evasionmastery.OnMouseOver += EvasionMasteryHover;
			evasionmastery.OnMouseOut += HoverOff;

			aquaaffinity = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/aquaaffinity") );
			aquaaffinity.OnClick += aquaaffinityClick;
			aquaaffinity.Width.Set(56, 0f);
			aquaaffinity.Height.Set(44, 0f);
			aquaaffinity.Left.Set(78, 0f);
			aquaaffinity.Top.Set(104, 0f);
			aquaaffinity.OnMouseOver += aquaaffinityHover;
			aquaaffinity.OnMouseOut += HoverOff;
			
			inneralchemy = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/inneralchemy") );
			inneralchemy.OnClick += inneralchemyClick;
			inneralchemy.Width.Set(56, 0f);
			inneralchemy.Height.Set(44, 0f);
			inneralchemy.Left.Set(136, 0f);
			inneralchemy.Top.Set(104, 0f);
			inneralchemy.OnMouseOver += inneralchemyHover;
			inneralchemy.OnMouseOut += HoverOff;

			umbralentropy = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/umbralentropy") );
			umbralentropy.OnClick += umbralentropyClick;
			umbralentropy.Width.Set(56, 0f);
			umbralentropy.Height.Set(44, 0f);
			umbralentropy.Left.Set(136, 0f);
			umbralentropy.Top.Set(152, 0f);
			umbralentropy.OnMouseOver += umbralentropyHover;
			umbralentropy.OnMouseOut += HoverOff;

			flashfreeze = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/flashfreeze") );
			flashfreeze.OnClick += flashfreezeClick;
			flashfreeze.Width.Set(56, 0f);
			flashfreeze.Height.Set(44, 0f);
			flashfreeze.Left.Set(136, 0f);
			flashfreeze.Top.Set(200, 0f);
			flashfreeze.OnMouseOver += flashfreezeHover;
			flashfreeze.OnMouseOut += HoverOff;

			mysticforging = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/mysticforging") );
			mysticforging.OnClick += mysticforgingClick;
			mysticforging.Width.Set(56, 0f);
			mysticforging.Height.Set(44, 0f);
			mysticforging.Left.Set(194, 0f);
			mysticforging.Top.Set(200, 0f);
			mysticforging.OnMouseOver += mysticforgingHover;
			mysticforging.OnMouseOut += HoverOff;

			hikari = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/hikari") );
			hikari.OnClick += hikariClick;
			hikari.Width.Set(56, 0f);
			hikari.Height.Set(44, 0f);
			hikari.Left.Set(194, 0f);
			hikari.Top.Set(104, 0f);
			hikari.OnMouseOver += hikariHover;
			hikari.OnMouseOut += HoverOff;

			celestialevanesence = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/celestialevanesence") );
			celestialevanesence.OnClick += celestialevanesenceClick;
			celestialevanesence.Width.Set(56, 0f);
			celestialevanesence.Height.Set(44, 0f);
			celestialevanesence.Left.Set(194, 0f);
			celestialevanesence.Top.Set(152, 0f);
			celestialevanesence.OnMouseOver += celestialevanesenceHover;
			celestialevanesence.OnMouseOut += HoverOff;

			bonus100hp = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/bonus100hp") );
			bonus100hp.OnClick += bonus100hpClick;
			bonus100hp.Width.Set(56, 0f);
			bonus100hp.Height.Set(44, 0f);
			bonus100hp.Left.Set(276, 0f);
			bonus100hp.Top.Set(56, 0f);
			bonus100hp.OnMouseOver += Bonus100hpHover;
			bonus100hp.OnMouseOut += HoverOff;

			bloomingflames = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/bloomingflames") );
			bloomingflames.OnClick += bloomingflamesClick;
			bloomingflames.Width.Set(56, 0f);
			bloomingflames.Height.Set(44, 0f);
			bloomingflames.Left.Set(336, 0f);
			bloomingflames.Top.Set(56, 0f);
			bloomingflames.OnMouseOver += bloomingflamesHover;
			bloomingflames.OnMouseOut += HoverOff;

			livingdead = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/livingdead") );
			livingdead.OnClick += livingdeadClick;
			livingdead.Width.Set(56, 0f);
			livingdead.Height.Set(44, 0f);
			livingdead.Left.Set(78, 0f);
			livingdead.Top.Set(152, 0f);
			livingdead.OnMouseOver += livingdeadHover;
			livingdead.OnMouseOut += HoverOff;

			astralmantle = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/astralmantle") );
			astralmantle.OnClick += astralmantleClick;
			astralmantle.Width.Set(56, 0f);
			astralmantle.Height.Set(44, 0f);
			astralmantle.Left.Set(276, 0f);
			astralmantle.Top.Set(104, 0f);
			astralmantle.OnMouseOver += astralmantleHover;
			astralmantle.OnMouseOut += HoverOff;

			afterburner = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/afterburner") );
			afterburner.OnClick += afterburnerClick;
			afterburner.Width.Set(56, 0f);
			afterburner.Height.Set(44, 0f);
			afterburner.Left.Set(276, 0f);
			afterburner.Top.Set(152, 0f);
			afterburner.OnMouseOver += afterburnerHover;
			afterburner.OnMouseOut += HoverOff;

			artofwar = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/artofwar") );
			artofwar.OnClick += artofwarClick;
			artofwar.Width.Set(56, 0f);
			artofwar.Height.Set(44, 0f);
			artofwar.Left.Set(276, 0f);
			artofwar.Top.Set(200, 0f);
			artofwar.OnMouseOver += artofwarHover;
			artofwar.OnMouseOut += HoverOff;

			avataroflight = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/avataroflight") );
			avataroflight.OnClick += avataroflightClick;
			avataroflight.Width.Set(56, 0f);
			avataroflight.Height.Set(44, 0f);
			avataroflight.Left.Set(336, 0f);
			avataroflight.Top.Set(104, 0f);
			avataroflight.OnMouseOver += avataroflightHover;
			avataroflight.OnMouseOut += HoverOff;

			weaknessexploit = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/weaknessexploit") );
			weaknessexploit.OnClick += weaknessexploitClick;
			weaknessexploit.Width.Set(56, 0f);
			weaknessexploit.Height.Set(44, 0f);
			weaknessexploit.Left.Set(336, 0f);
			weaknessexploit.Top.Set(152, 0f);
			weaknessexploit.OnMouseOver += weaknessexploitHover;
			weaknessexploit.OnMouseOut += HoverOff;

			aprismatism = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/aprismatism") );
			aprismatism.OnClick += aprismatismClick;
			aprismatism.Width.Set(56, 0f);
			aprismatism.Height.Set(44, 0f);
			aprismatism.Left.Set(336, 0f);
			aprismatism.Top.Set(200, 0f);
			aprismatism.OnMouseOver += aprismatismHover;
			aprismatism.OnMouseOut += HoverOff;

			beyondinfinity = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/beyondinfinity") );
			beyondinfinity.OnClick += beyondinfinityClick;
			beyondinfinity.Width.Set(56, 0f);
			beyondinfinity.Height.Set(44, 0f);
			beyondinfinity.Left.Set(418, 0f);
			beyondinfinity.Top.Set(56, 0f);
			beyondinfinity.OnMouseOver += BeyondInfinityHover;
			beyondinfinity.OnMouseOut += HoverOff;

			keyofchronology = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/keyofchronology") );
			keyofchronology.OnClick += keyofchronologyClick;
			keyofchronology.Width.Set(56, 0f);
			keyofchronology.Height.Set(44, 0f);
			keyofchronology.Left.Set(418, 0f);
			keyofchronology.Top.Set(104, 0f);
			keyofchronology.OnMouseOver += keyofchronologyHover;
			keyofchronology.OnMouseOut += HoverOff;

			unbridledradiance = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/unbridledradiance") );
			unbridledradiance.OnClick += unbridledradianceClick;
			unbridledradiance.Width.Set(56, 0f);
			unbridledradiance.Height.Set(44, 0f);
			unbridledradiance.Left.Set(418, 0f);
			unbridledradiance.Top.Set(200, 0f);
			unbridledradiance.OnMouseOver += unbridledradianceHover;
			unbridledradiance.OnMouseOut += HoverOff;


			beyondtheboundary = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/beyondtheboundary") );
			beyondtheboundary.OnClick += beyondtheboundaryClick;
			beyondtheboundary.Width.Set(56, 0f);
			beyondtheboundary.Height.Set(44, 0f);
			beyondtheboundary.Left.Set(418, 0f);
			beyondtheboundary.Top.Set(152, 0f);
			beyondtheboundary.OnMouseOver += beyondtheboundaryHover;
			beyondtheboundary.OnMouseOut += HoverOff;

			/*Asphodene = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/Eridani"));
			Asphodene.OnMouseOver += MouseOverA;
			Asphodene.OnClick += MouseClickA;
			Asphodene.Top.Set(0, 0f);
			Asphodene.Left.Set(0, 0f);
			Asphodene.Width.Set(0, 0f);
			Asphodene.Height.Set(0, 0f);*/

			text = new UIText("", 1.2f); 
			text.Width.Set(0, 0f);
			text.Height.Set(0, 0f);
			text.Left.Set(611, 0f);
			text.Top.Set(174, 0f);

			passiveTitle = new UIText("", 0.9f);
			passiveTitle.Width.Set(0, 0f);
			passiveTitle.Height.Set(0, 0f);
			passiveTitle.Left.Set(54, 0f);
			passiveTitle.Top.Set(-54, 0f);

			description = new UIText("", 0.7f);
			description.Width.Set(0, 0f);
			description.Height.Set(0, 0f);
			description.Left.Set(54, 0f);
			description.Top.Set(-31, 0f);



			gradientA = new Color(249, 133, 36); // 
			gradientB = new Color(255, 166, 83); //
			//area3.Append(Asphodene);
			
			area.Append(text);


			area.Append(starshower);
			area.Append(ironskin);
			area.Append(evasionmastery);
			area.Append(aquaaffinity);
			area.Append(livingdead);
			area.Append(bonus100hp);
			area.Append(bloomingflames);
			area.Append(beyondinfinity);
			area.Append(astralmantle);
			area.Append(afterburner);
			area.Append(artofwar);
			area.Append(inneralchemy);
			area.Append(umbralentropy);
			area.Append(flashfreeze);
			area.Append(celestialevanesence);
			area.Append(hikari);
			area.Append(avataroflight);
			area.Append(keyofchronology);
			area.Append(unbridledradiance);
			area.Append(beyondtheboundary);
			area.Append(weaknessexploit);
			area.Append(aprismatism);
			area.Append(butchersdozen);
			area.Append(mysticforging);

			
			

			area.Append(MeleeAspect);
			area.Append(MagicAspect);
			area.Append(RangedAspect);
			area.Append(SummonAspect);
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				area.Append(RogueAspect);
			}
			if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
			{
				area.Append(BardAspect);
				area.Append(HealerAspect);
				area.Append(ThrowerAspect);


			}
			area.Append(Lock);

			area.Append(passiveTitle);
			area.Append(description);
			area.Append(confirm);
			area.Append(reset);
			area.Append(barFrame);

			
			Append(area);

			
		}
		public override void OnDeactivate()
		{
			
			
		}
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			offset = new Vector2(evt.MousePosition.X - area.Left.Pixels, evt.MousePosition.Y - area.Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			Vector2 end = evt.MousePosition;
			dragging = false;

			area.Left.Set(end.X - offset.X, 0f);
			area.Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}
		private void Confirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray = false;
			
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive = true;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayMoveIn = -15f;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
			int randomDialogue = Main.rand.Next(0, 3);
			if (randomDialogue == 0)
			{
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterArrayDialogue.Asphodene.1", Main.LocalPlayer);
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterArrayDialogue.Eridani.1", Main.LocalPlayer);
				}

			}
			if (randomDialogue == 1)
			{
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterArrayDialogue.Asphodene.2", Main.LocalPlayer);
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterArrayDialogue.Eridani.2", Main.LocalPlayer);
				}
			}
			if (randomDialogue == 2)
			{
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterArrayDialogue.Asphodene.3", Main.LocalPlayer);
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterArrayDialogue.Eridani.3", Main.LocalPlayer);
				}
			}
			

			// We can do stuff in here!
		}
		private void ResetAll (UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starshower == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starshower = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ironskin == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ironskin = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().evasionmastery == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().evasionmastery = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bonus100hp == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bonus100hp = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaaffinity == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaaffinity = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bloomingflames == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bloomingflames = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astralmantle == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astralmantle = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondinfinity == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondinfinity = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().keyofchronology == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().keyofchronology = 1;

			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().avataroflight == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().avataroflight = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hikari == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hikari = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inneralchemy == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inneralchemy = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().livingdead == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().livingdead = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().umbralentropy == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().umbralentropy = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().celestialevanesence == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().celestialevanesence = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().afterburner == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().afterburner = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().weaknessexploit == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().weaknessexploit = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aprismatism == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aprismatism = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().unbridledradiance == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().unbridledradiance = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondtheboundary == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondtheboundary = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().butchersdozen == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().butchersdozen = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().mysticforging == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().mysticforging = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().flashfreeze == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().flashfreeze = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().artofwar == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().artofwar = 1;
			}
			// Remember to add to here
		}

		private void ConfirmHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Misc.Confirm", Main.LocalPlayer);

			// We can do stuff in here!
		}
		
		private void ResetHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Misc.Reset", Main.LocalPlayer);
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void StarshowerHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starshower == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.Starshield.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.Starshield.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.Starshield.Description", Main.LocalPlayer);

			}
			
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void MeleeAspectHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "[c/9D9D9D:Locked: Obtain an Aspected Weapon]";

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Melee.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Description", Main.LocalPlayer);

			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void RogueAspectHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "[c/9D9D9D:Locked: Obtain an Aspected Weapon]";

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Rogue.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Description", Main.LocalPlayer);
			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}

		private void BardAspectHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "[c/9D9D9D:Locked: Obtain an Aspected Weapon]";

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Bard.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Description", Main.LocalPlayer);
			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void HealerAspectHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "[c/9D9D9D:Locked: Obtain an Aspected Weapon]";

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Healer.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Description", Main.LocalPlayer);
			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void ThrowerAspectHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "[c/9D9D9D:Locked: Obtain an Aspected Weapon]";

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Thrower.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Description", Main.LocalPlayer);
			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}

		private void MagicAspectHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "[c/9D9D9D:Locked: Obtain an Aspected Weapon]";

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Magic.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Description", Main.LocalPlayer);
			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void RangedAspectHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "[c/9D9D9D:Locked: Obtain an Aspected Weapon]";

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Ranged.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Description", Main.LocalPlayer);
			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void SummonAspectHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "[c/9D9D9D:Locked: Obtain an Aspected Weapon]";

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Summon.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.Description", Main.LocalPlayer);
			}


			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void LockHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.AspectedDamageType.LockHover", Main.LocalPlayer);

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void ButchersDozenHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().butchersdozen == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.ButchersDozen.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.ButchersDozen.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.ButchersDozen.Description", Main.LocalPlayer);

			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void EvasionMasteryHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().evasionmastery == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.EvasionMastery.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.EvasionMastery.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.EvasionMastery.Description", Main.LocalPlayer);

			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void IronskinHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ironskin == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.IronWill.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.IronWill.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.IronWill.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void umbralentropyHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().umbralentropy == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.UmbralEntropy.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.UmbralEntropy.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.UmbralEntropy.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void flashfreezeHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().flashfreeze == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.FlashFreeze.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.FlashFreeze.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.FlashFreeze.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void mysticforgingHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().mysticforging == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.MysticForging.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.MysticForging.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.MysticForging.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void inneralchemyHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inneralchemy == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.InnerAlchemy.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.InnerAlchemy.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.InnerAlchemy.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void hikariHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hikari == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.Hikari.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.Hikari.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.Hikari.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void celestialevanesenceHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().celestialevanesence == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.CelestialEvanesence.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.CelestialEvanesence.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.CelestialEvanesence.Description", Main.LocalPlayer);

			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void Bonus100hpHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bonus100hp == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.HealthyConfidence.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.HealthyConfidence.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.HealthyConfidence.Description", Main.LocalPlayer);

			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void bloomingflamesHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bloomingflames == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.InfernalEnd.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.InfernalEnd.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.InfernalEnd.Description", Main.LocalPlayer);

			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void avataroflightHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().avataroflight == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.AvatarOfLight.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.AvatarOfLight.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.AvatarOfLight.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void astralmantleHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astralmantle == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.AstralMantle.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.AstralMantle.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.AstralMantle.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void afterburnerHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().afterburner == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.Afterburner.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.Afterburner.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.Afterburner.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void artofwarHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().artofwar == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.SpatialStratagem.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.SpatialStratagem.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.SpatialStratagem.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void weaknessexploitHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().weaknessexploit == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.WeaknessExploit.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.WeaknessExploit.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.WeaknessExploit.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void aprismatismHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aprismatism == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.Aprismatism.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.Aprismatism.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.Aprismatism.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void unbridledradianceHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().unbridledradiance == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.UnbridledRadiance.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.UnbridledRadiance.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.UnbridledRadiance.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}

		private void beyondtheboundaryHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondtheboundary == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.BetweenTheBoundary.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.BetweenTheBoundary.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.BetweenTheBoundary.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void aquaaffinityHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaaffinity == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.CyclicHunter.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.CyclicHunter.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.CyclicHunter.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void livingdeadHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().livingdead == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.LivingDead.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.LivingDead.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.LivingDead.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}

		private void BeyondInfinityHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondinfinity == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.BeyondInfinity.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.BeyondInfinity.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.BeyondInfinity.Description", Main.LocalPlayer);

			}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}
		private void keyofchronologyHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().keyofchronology == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.KeyOfChronology.Unlock", Main.LocalPlayer);

			}
			else
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = LangHelper.GetTextValue($"StellarArray.Abilities.KeyOfChronology.Name", Main.LocalPlayer);

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StellarArray.Abilities.KeyOfChronology.Description", Main.LocalPlayer);

			}

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;


			// We can do stuff in here!
		}

		private void HoverOff(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().passiveTitle = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			// We can do stuff in here!
		}
		private void StarshowerClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return; 
			if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starshower == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starshower = 2;
			}
			else
			if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starshower == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starshower = 1;
			}




			// We can do stuff in here!
		}
		private void BardAspectClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect = 1;

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect = 1;

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect = 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect = 1;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect = 1;
				return;

			}





			// We can do stuff in here!
		}
		private void HealerAspectClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect = 1;

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect = 1;

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect = 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect = 1;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect = 1;
				return;

			}





			// We can do stuff in here!
		}
		private void ThrowerAspectClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect = 1;

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect = 1;

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect = 2;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect = 1;
				return;

			}





			// We can do stuff in here!
		}
		private void MeleeAspectClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect = 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect = 1;

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect = 1;

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect = 1;
				return;
			}
			if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
            {
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect = 1;
				return;

			}
			




			// We can do stuff in here!
		}
		private void RogueAspectClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect = 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect = 1;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect = 1;
				return;

			}





			// We can do stuff in here!
		}
		private void RangedAspectClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect = 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect = 1;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
			{

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect = 1;
				return;
			}




			// We can do stuff in here!
		}
		private void MagicAspectClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect = 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect = 1;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{
	
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect = 1;
				return;
			}




			// We can do stuff in here!
		}
		private void SummonAspectClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect = 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect = 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect = 1;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
			{

				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect = 1;
				return;
			}




			// We can do stuff in here!
		}
		private void LockClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;

			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked != 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked = 0;
				return;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked == 0 && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2 || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2 || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2 || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 2)
			{
				if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
                {
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked = 1;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked = 2;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked = 3;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked = 4;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked = 5;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked = 6;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked = 7;
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AspectLocked = 8;
				}
				return;

			}
			




			// We can do stuff in here!
		}
		private void ButchersDozenClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().butchersdozen == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().butchersdozen = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().butchersdozen == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().butchersdozen = 1;
			}




			// We can do stuff in here!
		}
		private void umbralentropyClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().umbralentropy == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().umbralentropy = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().umbralentropy == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().umbralentropy = 1;
			}




			// We can do stuff in here!
		}
		private void flashfreezeClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().flashfreeze == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().flashfreeze = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().flashfreeze == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().flashfreeze = 1;
			}




			// We can do stuff in here!
		}
		private void mysticforgingClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().mysticforging == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().mysticforging = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().mysticforging == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().mysticforging = 1;
			}




			// We can do stuff in here!
		}
		private void IronskinClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ironskin == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ironskin = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ironskin == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ironskin = 1;
			}




			// We can do stuff in here!
		}
		private void livingdeadClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if(Main.LocalPlayer.statLifeMax > 200)
            {
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().livingdead == 1)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().livingdead = 2;
				}
				else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().livingdead == 2)
				{
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().livingdead = 1;
				}
			}
            else
            {

            }
			




			// We can do stuff in here!
		}

		private void evasionmasteryClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().evasionmastery == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().evasionmastery = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().evasionmastery == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().evasionmastery = 1;
			}




			// We can do stuff in here!
		}
		private void inneralchemyClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inneralchemy == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inneralchemy = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inneralchemy == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inneralchemy = 1;
			}




			// We can do stuff in here!
		}
		private void hikariClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hikari == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hikari = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hikari == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hikari = 1;
			}




			// We can do stuff in here!
		}
		private void celestialevanesenceClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().celestialevanesence == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().celestialevanesence = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().celestialevanesence == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().celestialevanesence = 1;
			}




			// We can do stuff in here!
		}
		private void aquaaffinityClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 1 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaaffinity == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaaffinity = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaaffinity == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 1;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaaffinity = 1;
			}




			// We can do stuff in here!
		}

		private void bonus100hpClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true ))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 2 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bonus100hp == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bonus100hp = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bonus100hp == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bonus100hp = 1;
			}




			// We can do stuff in here!
		}
		private void afterburnerClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 2 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().afterburner == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().afterburner = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().afterburner == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().afterburner = 1;
			}




			// We can do stuff in here!
		}
		private void artofwarClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 2 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().artofwar == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().artofwar = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().artofwar == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().artofwar = 1;
			}




			// We can do stuff in here!
		}
		private void weaknessexploitClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 2 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().weaknessexploit == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().weaknessexploit = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().weaknessexploit == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().weaknessexploit = 1;
			}




			// We can do stuff in here!
		}
		private void aprismatismClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 2 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aprismatism == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aprismatism = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aprismatism == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aprismatism = 1;
			}




			// We can do stuff in here!
		}
		private void bloomingflamesClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 2 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bloomingflames == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bloomingflames = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bloomingflames == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bloomingflames = 1;
			}




			// We can do stuff in here!
		}
		private void astralmantleClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true ))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 2 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astralmantle == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astralmantle = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astralmantle == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astralmantle = 1;
			}




			// We can do stuff in here!
		}
		private void avataroflightClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 2 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().avataroflight == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().avataroflight = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().avataroflight == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 2;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().avataroflight = 1;
			}
			




			// We can do stuff in here!
		}
		private void beyondinfinityClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 3 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondinfinity == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondinfinity = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondinfinity == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondinfinity = 1;
			}




			// We can do stuff in here!
		}
		private void keyofchronologyClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 3 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().keyofchronology == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().keyofchronology = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().keyofchronology == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().keyofchronology = 1;
			}




			// We can do stuff in here!
		}
		private void unbridledradianceClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 3 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().unbridledradiance == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().unbridledradiance = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().unbridledradiance == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().unbridledradiance = 1;
			}




			// We can do stuff in here!
		}

		private void beyondtheboundaryClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArray == true))
				return;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge + 3 <= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondtheboundary == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge += 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondtheboundary = 2;
			}
			else
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondtheboundary == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge -= 3;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondtheboundary = 1;
			}




			// We can do stuff in here!
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility > 0))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			
				
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Calculate quotient
			base.DrawSelf(spriteBatch);
			area.Top.Set(area.Top.Pixels - modPlayer.stellarArrayMoveIn, 0);
			Rectangle hitbox = area.GetInnerDimensions().ToRectangle();
			Rectangle hitbox2 = area.GetInnerDimensions().ToRectangle();
			Rectangle starshowerArea = starshower.GetInnerDimensions().ToRectangle();
			Rectangle ironskinArea = ironskin.GetInnerDimensions().ToRectangle();
			Rectangle evasionmasteryArea = evasionmastery.GetInnerDimensions().ToRectangle();
			Rectangle bonus100hpArea = bonus100hp.GetInnerDimensions().ToRectangle();
			Rectangle beyondinfinityArea = beyondinfinity.GetInnerDimensions().ToRectangle();
			Rectangle aquaaffinityArea = aquaaffinity.GetInnerDimensions().ToRectangle();
			Rectangle bloomingflamesArea = bloomingflames.GetInnerDimensions().ToRectangle();
			Rectangle astralmantleArea = astralmantle.GetInnerDimensions().ToRectangle();
			Rectangle inneralchemyArea = inneralchemy.GetInnerDimensions().ToRectangle();
			Rectangle hikariArea = hikari.GetInnerDimensions().ToRectangle();
			Rectangle celestialevanesenceArea = celestialevanesence.GetInnerDimensions().ToRectangle();
			Rectangle avataroflightArea = avataroflight.GetInnerDimensions().ToRectangle();
			Rectangle keyofchronologyArea = keyofchronology.GetInnerDimensions().ToRectangle();
			Rectangle livingdeadArea = livingdead.GetInnerDimensions().ToRectangle();
			Rectangle umbralentropyArea = umbralentropy.GetInnerDimensions().ToRectangle();
			Rectangle afterburnerArea = afterburner.GetInnerDimensions().ToRectangle();
			Rectangle weaknessexploitArea = weaknessexploit.GetInnerDimensions().ToRectangle();
			Rectangle aprismatismArea = aprismatism.GetInnerDimensions().ToRectangle();
			Rectangle unbridledradianceArea = unbridledradiance.GetInnerDimensions().ToRectangle();
			Rectangle butchersdozenArea = butchersdozen.GetInnerDimensions().ToRectangle();
			Rectangle mysticforgingArea = mysticforging.GetInnerDimensions().ToRectangle();
			Rectangle artofwarArea = artofwar.GetInnerDimensions().ToRectangle();
			Rectangle beyondtheboundaryArea = beyondtheboundary.GetInnerDimensions().ToRectangle();
			Rectangle flashfreezeArea = flashfreeze.GetInnerDimensions().ToRectangle();

			Rectangle MeleeAspectArea = MeleeAspect.GetInnerDimensions().ToRectangle();
			Rectangle SummonAspectArea = SummonAspect.GetInnerDimensions().ToRectangle();
			Rectangle MagicAspectArea = MagicAspect.GetInnerDimensions().ToRectangle();
			Rectangle RangedAspectArea = RangedAspect.GetInnerDimensions().ToRectangle();
			Rectangle RogueAspectArea = RogueAspect.GetInnerDimensions().ToRectangle();

			Rectangle BardAspectArea = BardAspect.GetInnerDimensions().ToRectangle();
			Rectangle HealerAspectArea = HealerAspect.GetInnerDimensions().ToRectangle();
			Rectangle ThrowerAspectArea = ThrowerAspect.GetInnerDimensions().ToRectangle();

			Rectangle LockArea = Lock.GetInnerDimensions().ToRectangle();
			//Rectangle indicator = new Rectangle((600), (280), (700), (440));
			//indicator.X += 0;
			//indicator.Width -= 0;
			//indicator.Y += 0;
			//indicator.Height -= 0;

			//Rectangle dialogueBox = new Rectangle((50), (480), (700), (300));


			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/StellarArray"), hitbox, Color.White * modPlayer.stellarArrayVisibility);
			if(modPlayer.stellarGauge<=5)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/gauge" + modPlayer.stellarGauge), hitbox, Color.White * modPlayer.stellarArrayVisibility);

			}
			else
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/gauge6"), hitbox, Color.White * modPlayer.stellarArrayVisibility);

			}
			if (modPlayer.MeleeAspect == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Melee"), MeleeAspectArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.RangedAspect == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Ranged"), RangedAspectArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.RogueAspect == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Rogue"), RogueAspectArea, Color.White * modPlayer.stellarArrayVisibility);
			}

			if (modPlayer.BardAspect == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Symphonic"), BardAspectArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.HealerAspect == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Radiant"), HealerAspectArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.ThrowerAspect == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Thrower"), ThrowerAspectArea, Color.White * modPlayer.stellarArrayVisibility);
			}

			if (modPlayer.MagicAspect == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Magic"), MagicAspectArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.SummonAspect == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Summon"), SummonAspectArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.AspectLocked != 0)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Lock"), LockArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.starshower == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Starshower"), starshowerArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.ironskin == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/ironskin"), ironskinArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.evasionmastery == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/evasionmastery"), evasionmasteryArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.bonus100hp == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/bonus100hp"), bonus100hpArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.beyondinfinity == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/beyondinfinity"), beyondinfinityArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.aquaaffinity == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/aquaaffinity"), aquaaffinityArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.bloomingflames == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/bloomingflames"), bloomingflamesArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.astralmantle == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/astralmantle"), astralmantleArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.inneralchemy == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/inneralchemy"), inneralchemyArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.hikari == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/hikari"), hikariArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.avataroflight == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/avataroflight"), avataroflightArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.keyofchronology == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/keyofchronology"), keyofchronologyArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.livingdead == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/livingdead"), livingdeadArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.umbralentropy == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/umbralentropy"), umbralentropyArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.celestialevanesence == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/celestialevanesence"), celestialevanesenceArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.unbridledradiance == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/unbridledradiance"), unbridledradianceArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.afterburner == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/afterburner"), afterburnerArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.weaknessexploit == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/weaknessexploit"), weaknessexploitArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.butchersdozen == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/butchersdozen"), butchersdozenArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.mysticforging == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/mysticforging"), mysticforgingArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.artofwar == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/artofwar"), artofwarArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.beyondtheboundary == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/beyondtheboundary"), beyondtheboundaryArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.flashfreeze == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/flashfreeze"), flashfreezeArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (modPlayer.aprismatism == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/aprismatism"), aprismatismArea, Color.White * modPlayer.stellarArrayVisibility);
			}
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility > 0))
			{
				area.Remove();
				confirm.Remove();
				reset.Remove();
			}
			Recalculate();


		}
			
		
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayVisibility > 0))
			{
				area.Remove();
				return;

			}
			else
			{
				Append(area);
			}

			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				area.Append(RogueAspect);
			}
			else
            {
				RogueAspect.Remove();

			}
			if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
			{
				area.Append(BardAspect);
				area.Append(HealerAspect);
				area.Append(ThrowerAspect);

			}
			else
			{
				BardAspect.Remove();
				HealerAspect.Remove();
				ThrowerAspect.Remove();


			}
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			// Setting the text per tick to update and show our resource values.
			text.SetText($"{modPlayer.stellarGauge}/{modPlayer.stellarGaugeMax}");

			passiveTitle.SetText($"{modPlayer.passiveTitle}");
			description.SetText(LangHelper.Wrap($"{modPlayer.description}", 70));
			

			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
			base.Update(gameTime);
		}

	}
}
