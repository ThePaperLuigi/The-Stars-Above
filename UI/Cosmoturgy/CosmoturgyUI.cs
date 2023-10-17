
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
using Terraria.ModLoader;
using StarsAbove.Items.Memories;

namespace StarsAbove.UI.Cosmoturgy
{
    internal class CosmoturgyUI : UIState
	{
		private UIText text;
		private UIText hoverText;
		private UIElement area;
		private UIElement areaCrystal;
		private UIElement areaGlow;

		private Vector2 offset;

		private UIText description;

		static public VanillaItemSlotWrapper _affixSlot1;
		static public VanillaItemSlotWrapper _affixSlot2;
		static public VanillaItemSlotWrapper _affixSlot3;
		private VanillaItemSlotWrapper _weaponSlot;

		private UIImageButton confirm;
		private UIImageButton reset;

		public override void OnInitialize()
		{
			area = new UIElement();
			//area.Left.Set(0, 0f); // Place the resource bar to the left of the hearts.
			//area.Top.Set(0, 0f);
			area.Width.Set(600, 0f);
			area.Height.Set(600, 0f);
			//area.OnMouseDown += new UIElement.MouseEvent(DragStart);
			//area.OnMouseUp += new UIElement.MouseEvent(DragEnd);
			area.HAlign = area.VAlign = 0.5f; // 1

			areaCrystal = new UIElement();
			areaCrystal.Width.Set(226, 0f);
			areaCrystal.Height.Set(186, 0f);

			areaGlow = new UIElement();
			areaGlow.Width.Set(300, 0f);
			areaGlow.Height.Set(300, 0f);

			confirm = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Confirm"));
			confirm.OnLeftClick += Confirm;
			confirm.Width.Set(70, 0f);
			confirm.Height.Set(52, 0f);
			confirm.Left.Set(40, 0f);
			confirm.Top.Set(298, 0f);
			confirm.OnMouseOver += ConfirmHover;
			confirm.OnMouseOut += HoverOff;

			text = new UIText("", 1f);
			text.Width.Set(0, 0f);
			text.Height.Set(0, 0f);
			text.Top.Set(355, 0f);
			text.Left.Set(90, 0f);

			hoverText = new UIText("", 1f);
			hoverText.Width.Set(10, 0f);
			hoverText.Height.Set(10, 0f);
			hoverText.Top.Set(20, 0f);
			hoverText.Left.Set(20, 0f);
		

			_affixSlot1 = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 162 },
				Top = { Pixels = 604 },
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
				Top = { Pixels = 604 },
				Width = { Pixels = 70 },
				Height = { Pixels = 70 },
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
				Top = { Pixels = 604 },
				Width = { Pixels = 70 },
				Height = { Pixels = 70 },
				MaxWidth = { Pixels = 70 },
				MaxHeight = { Pixels = 70 },

				ValidItemFunc = item => item.IsAir || !item.IsAir,
				IgnoresMouseInteraction = false
			};
			_affixSlot3.OnMouseOver += AffixHover;
			_affixSlot3.OnMouseOut += HoverOff;

			_weaponSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f)
			{
				Left = { Pixels = 740 },
				Top = { Pixels = 604 },
				Width = { Pixels = 70 },
				Height = { Pixels = 70 },
				MaxWidth = { Pixels = 70 },
				MaxHeight = { Pixels = 70 },

				ValidItemFunc = item => (item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") && item.damage > 0) || item.IsAir, //Only weapons from this mod in this slot
				IgnoresMouseInteraction = false
			};
			_weaponSlot.OnMouseOver += SpecialAffixHover;
			_weaponSlot.OnMouseOut += HoverOff;
			area.Append(_affixSlot1);
			area.Append(_affixSlot2);
			area.Append(_affixSlot3);
			area.Append(_weaponSlot);
			area.Append(text);
			area.Append(areaCrystal);
			area.Append(areaGlow);
			area.Append(confirm);
			//area.Append(reset);
			Append(area);

		}
		private void ResetAll(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().cosmoturgyUIOpacity < 0.1f)
				return;
			if (!_affixSlot1.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				//Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _affixSlot1.Item, _affixSlot1.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				//_affixSlot1.Item.TurnToAir();
			}
			if (!_affixSlot2.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				//Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _affixSlot2.Item, _affixSlot2.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				//_affixSlot2.Item.TurnToAir();
			}
			if (!_affixSlot3.Item.IsAir)
			{
				// QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
				//Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _affixSlot3.Item, _affixSlot3.Item.stack);
				// Now that we've spawned the item back onto the player, we reset the item by turning it into air.
				//_affixSlot3.Item.TurnToAir();
			}
			// Remember to add to here
		}
		private void SpecialAffixHover(UIMouseEvent evt, UIElement listeningElement)
		{


			if (Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().cosmoturgyUIOpacity < 0.1f)
				return;
			Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.InsertWeaponHover", Main.LocalPlayer);//replace me later
			// We can do stuff in here!
		}
		private void AffixHover(UIMouseEvent evt, UIElement listeningElement)
		{


			if (Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().cosmoturgyUIOpacity < 0.1f)
				return;
			Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.MemorySlot", Main.LocalPlayer);
			// We can do stuff in here!
		}
		private void WeaponHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().cosmoturgyUIOpacity < 0.1f)
				return;

			Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.InsertWeapon", Main.LocalPlayer);

		}
		private void ConfirmHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().cosmoturgyUIOpacity < 0.1f)
				return;
			
			if(_weaponSlot.Item.IsAir)
            {
				Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.ConfirmImprintNoWeapon", Main.LocalPlayer);

			}
			else
            {
				if (_affixSlot1.Item.IsAir && _affixSlot2.Item.IsAir && _affixSlot3.Item.IsAir)
				{
					Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.ConfirmImprintNoMemories", Main.LocalPlayer);

				}
				else 
				{
					bool atleastOneMemory = false;

					if(!_affixSlot1.Item.IsAir)
                    {
						if(_affixSlot1.Item.GetGlobalItem<ItemMemorySystem>().isMemory)//If the thing in the slot is a memory
                        {
							atleastOneMemory = true;
						}
						

					}
					if (!_affixSlot2.Item.IsAir)
					{
						if (_affixSlot2.Item.GetGlobalItem<ItemMemorySystem>().isMemory)//If the thing in the slot is a memory
						{
							atleastOneMemory = true;
						}

					}
					if (!_affixSlot3.Item.IsAir)
					{
						if(_affixSlot3.Item.GetGlobalItem<ItemMemorySystem>().isMemory)//If the thing in the slot is a memory

						{
							atleastOneMemory = true;
						}

					}
					
					if(atleastOneMemory)
                    {
						if (_weaponSlot.Item.GetGlobalItem<ItemMemorySystem>().memoryCount > 0)//If the weapon already has a memory
						{
							Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.ConfirmImprintOverwrite", Main.LocalPlayer);
							return;
						}
						else
						{
							Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.ConfirmImprint", Main.LocalPlayer);
							return;
						}
					}
					
					Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.ConfirmImprintNoMemories", Main.LocalPlayer);


				}
			}

			

		}
		private void Confirm(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().cosmoturgyUIOpacity < 0.1f)
				return;

			//if all checks succeed
			if ((_affixSlot1.Item.IsAir && _affixSlot2.Item.IsAir && _affixSlot3.Item.IsAir) || _weaponSlot.Item.IsAir)
			{
				//Nothing
				return;
			}
			else
			{
				if (!_affixSlot1.Item.IsAir)
				{
					if(_affixSlot1.Item.GetGlobalItem<ItemMemorySystem>().isMemory)
                    {
						_weaponSlot.Item.GetGlobalItem<ItemMemorySystem>().itemMemorySlot1 = GetMemoryID(_affixSlot1.Item);
						_affixSlot1.Item.TurnToAir();
					}			
				}
				if (!_affixSlot2.Item.IsAir)
				{
					if(_affixSlot2.Item.GetGlobalItem<ItemMemorySystem>().isMemory)
                    {
						_weaponSlot.Item.GetGlobalItem<ItemMemorySystem>().itemMemorySlot2 = GetMemoryID(_affixSlot2.Item);
						_affixSlot2.Item.TurnToAir();
					}				
				}
				if (!_affixSlot3.Item.IsAir)
				{
					if(_affixSlot3.Item.GetGlobalItem<ItemMemorySystem>().isMemory)
                    {
						_weaponSlot.Item.GetGlobalItem<ItemMemorySystem>().itemMemorySlot3 = GetMemoryID(_affixSlot3.Item);
						_affixSlot3.Item.TurnToAir();
					}				
				}
				Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().imbueSuccessAnimationTimer = 1f;
				Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.MemoriesImprinted", Main.LocalPlayer);
			}
		}
		private int GetMemoryID(Item memory)
        {
			if (memory.type == ItemType<CapedFeather>())
			{
				return 5;
			}			
			if (memory.type == ItemType<ResonanceGem>())
			{
				return 22;
			}
			if (memory.type == ItemType<SigilOfHope>())
			{
				return 31;
			}
			if (memory.type == ItemType<KnightsShovelhead>())
			{
				return 33;
			}
			return 0;
        }
		private void HoverOff(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().cosmoturgyUIOpacity < 0.1f)
				return;

			if(_weaponSlot.Item.IsAir)
            {
				Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.InsertWeapon", Main.LocalPlayer);

			}
			else
            {
				if(_weaponSlot.Item.GetGlobalItem<ItemMemorySystem>().memoryCount != 1)
                {
					Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.CurrentWeapon", _weaponSlot.Item.Name);

				}
				else
                {
					Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().description = LangHelper.GetTextValue($"UIElements.Cosmoturgy.CurrentWeapon", _weaponSlot.Item.Name);

				}

			}

			// We can do stuff in here!
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().cosmoturgyUIOpacity < 0.1f)
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{

			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>();
			// Calculate quotient
			hoverText.Left.Set(Main.mouseX + 20, 0f); // Place the resource bar to the left of the hearts.
			hoverText.Top.Set(Main.mouseY, 0f); // Placing it just a bit below the top of the screen.

			UI.StarfarerMenu.StarfarerMenu.AdjustAreaBasedOnPlayerVelocity(ref area, 0,0);

			areaCrystal.Left.Set(340, 0f);
			areaCrystal.Top.Set(10 + MathHelper.Lerp(-5,5,modPlayer.quadraticFloat), 0f);

			Rectangle hitbox = area.GetInnerDimensions().ToRectangle();
			Rectangle crystal = areaCrystal.GetInnerDimensions().ToRectangle();
			Rectangle glow = areaGlow.GetInnerDimensions().ToRectangle();
			Vector2 glowCenter = new Vector2(areaGlow.GetInnerDimensions().Width/2, areaGlow.GetInnerDimensions().Height/2);
			areaGlow.Top.Set(230, 0f);
			areaGlow.Left.Set(300, 0f);

			confirm.Top.Set(470, 0f);
			confirm.Left.Set(44, 0f);
			//reset.Top.Set(450, 0f);
			//reset.Left.Set(44, 0f);
			Rectangle trim = new Rectangle(0, 0, 400, 400);
			Texture2D crystalTexture = (Texture2D)Request<Texture2D>("StarsAbove/UI/Cosmoturgy/CosmoturgyCrystal");

			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Cosmoturgy/CosmoturgyUI"), hitbox, Color.White * (modPlayer.cosmoturgyUIOpacity));
			spriteBatch.Draw(crystalTexture, crystal, crystalTexture.Frame(1,7,0,modPlayer.crystalFrame), Color.White * (modPlayer.cosmoturgyUIOpacity));
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Cosmoturgy/CosmoturgyUITop"), hitbox, Color.White * (modPlayer.cosmoturgyUIOpacity));

			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/Effects/GodRays"),
				glow,
				null,
				Color.White * modPlayer.cosmoturgyUIOpacity * modPlayer.weaponGlowOpacity,
				MathHelper.ToRadians(modPlayer.smoothRotation),//Very mechanical animation, ends on neutral
				glowCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/Effects/GodRays"),
				glow,
				null,
				Color.White * modPlayer.cosmoturgyUIOpacity * modPlayer.weaponGlowOpacity,
				MathHelper.ToRadians(-modPlayer.smoothRotation),//Very mechanical animation, ends on neutral
				glowCenter,
				SpriteEffects.None,
				1f);

			_affixSlot1.Top.Set(302, 0f);
			_affixSlot1.Left.Set(160, 0f);

			_affixSlot2.Top.Set(326, 0f);
			_affixSlot2.Left.Set(278, 0f);

			_affixSlot3.Top.Set(302, 0f);
			_affixSlot3.Left.Set(396, 0f);

			_weaponSlot.Top.Set(210, 0f);
			_weaponSlot.Left.Set(278, 0f);

			text.Top.Set(425, 0f);
			text.Left.Set(127, 0f);

			if (_weaponSlot.Item != null)
            {
				if (!_weaponSlot.Item.IsAir)
				{
					if (_weaponSlot.Item.GetGlobalItem<ItemMemorySystem>().itemMemorySlot1 != 0)
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Cosmoturgy/Slot1Filled"), hitbox, Color.White * (modPlayer.cosmoturgyUIOpacity));

					}
					
					if (_weaponSlot.Item.GetGlobalItem<ItemMemorySystem>().itemMemorySlot2 != 0)
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Cosmoturgy/Slot2Filled"), hitbox, Color.White * (modPlayer.cosmoturgyUIOpacity));

					}
					
					if (_weaponSlot.Item.GetGlobalItem<ItemMemorySystem>().itemMemorySlot3 != 0)
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Cosmoturgy/Slot3Filled"), hitbox, Color.White * (modPlayer.cosmoturgyUIOpacity));

					}
					
					
				}
			}
			

			Recalculate();
		}


		public override void Update(GameTime gameTime)
		{
			if (Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>().cosmoturgyUIOpacity < 0.1f)
			{
				area.Remove();
				return;

			}
			else
			{
				Append(area);
			}

			var modPlayer = Main.LocalPlayer.GetModPlayer<CosmoturgyPlayer>();

			// Setting the text per tick to update and show our resource values.
			if (_weaponSlot.Item == null)
			{
				modPlayer.description = "test";

			}
			text.SetText($"{LangHelper.Wrap(modPlayer.description, 38)}");

			if (!modPlayer.cosmoturgyUIActive)
            {
				if (!_weaponSlot.Item.IsAir)
				{
					Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _weaponSlot.Item, _weaponSlot.Item.stack);
					_weaponSlot.Item.TurnToAir();
				}
				if (!_affixSlot1.Item.IsAir)
				{
					Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _affixSlot1.Item, _affixSlot1.Item.stack);
					_affixSlot1.Item.TurnToAir();


				}
				if (!_affixSlot2.Item.IsAir)
				{
					Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _affixSlot2.Item, _affixSlot2.Item.stack);
					_affixSlot2.Item.TurnToAir();


				}
				if (!_affixSlot3.Item.IsAir)
				{
					Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), _affixSlot3.Item, _affixSlot3.Item.stack);
					_affixSlot3.Item.TurnToAir();


				}
			}
			

			//modPlayer.affixItem1 = _affixSlot1.Item;
			//modPlayer.affixItem2 = _affixSlot2.Item;
			//modPlayer.affixItem3 = _affixSlot3.Item;

			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
			base.Update(gameTime);
		}

		

	}

}
