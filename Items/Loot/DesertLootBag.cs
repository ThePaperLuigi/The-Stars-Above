using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using StarsAbove.NPCs;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Prisms;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace StarsAbove.Items.Loot
{
	// Basic code for a boss treasure bag
	public class DesertLootBag : ModItem
	{

		//This could probably be made into a list, populated in SetDefaults?

		// Item 1
		int item1ID = ItemID.SandBlock;
		// Item amount (max)
		int item1Max = 100;
		// Item 1 chance
		float item1Chance = 0.1f;

		// Item 2
		int item2ID = ItemID.Cactus;
		// Item amount (max)
		int item2Max = 100;
		// Item 2 chance
		float item2Chance = 0.1f;

		// Item 3
		int item3ID = 0;
		// Item amount (max)
		int item3Max = 100;
		// Item 3 chance
		float item3Chance = 0.2f;

		// Item 4
		int item4ID = 0;
		// Item amount (max)
		int item4Max = 100;
		// Item 4 chance
		float item4Chance = 0.1f;

		// Item 5
		int item5ID = 0;
		// Item amount (max)
		int item5Max = 100;
		// Item 5 chance
		float item5Chance = 0.3f;

		// Item 6
		int item6ID = 0;
		// Item amount (max)
		int item6Max = 100;
		// Ite
		float item6Chance = 0.1f;


		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stellar Spoils (Desert)");
			Tooltip.SetDefault("" +
                "[Tier 1]" +
				"\n{$CommonItemTooltip.RightClickToOpen}" +
				"\nContains the following items:" +
                $"\n"); // References a language key that says "Right Click To Open" in the language of the game

			//ItemID.Sets.BossBag[Type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Purple;
			Item.expert = true; // This makes sure that "Expert" displays in the tooltip and the item name color changes
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			if (item1ID != 0)
            {
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: SpoilsInfo", $"{Lang.GetItemNameValue(item1ID)} (1-{item1Max}) {item1Chance * 100}%") { OverrideColor = Color.White };
				tooltips.Add(tooltip);
			}
			if (item2ID != 0)
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: SpoilsInfo", $"{Lang.GetItemNameValue(item2ID)} (1-{item2Max}) {item2Chance * 100}%") { OverrideColor = Color.White };
				tooltips.Add(tooltip);
			}
			if (item3ID != 0)
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: SpoilsInfo", $"{Lang.GetItemNameValue(item3ID)} (1-{item3Max}) {item3Chance * 100}%") { OverrideColor = Color.White };
				tooltips.Add(tooltip);
			}
			if (item4ID != 0)
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: SpoilsInfo", $"{Lang.GetItemNameValue(item4ID)} (1-{item4Max}) {item4Chance * 100}%") { OverrideColor = Color.White };
				tooltips.Add(tooltip);
			}
			if (item5ID != 0)
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: SpoilsInfo", $"{Lang.GetItemNameValue(item5ID)} (1-{item5Max}) {item5Chance * 100}%") { OverrideColor = Color.White };
				tooltips.Add(tooltip);
			}
			if (item6ID != 0)
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: SpoilsInfo", $"{Lang.GetItemNameValue(item6ID)} (1-{item6Max}) {item6Chance * 100}%") { OverrideColor = Color.White };
				tooltips.Add(tooltip);
			}


			base.ModifyTooltips(tooltips);
        }

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			// We have to replicate the expert drops from MinionBossBody here via QuickSpawnItem

			var entitySource = player.GetSource_OpenItem(Type);

			if (Main.rand.NextBool(4))
			{
				player.QuickSpawnItem(entitySource, ModContent.ItemType<BurnishedPrism>());
			}
		}
		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			// We have to replicate the expert drops from MinionBossBody here via QuickSpawnItem

			//itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<MinionBossMask>(), 7));
			//itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ExampleItem>(), 1, 12, 16));
			//itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<MinionBossBody>()));
		}
		// Below is code for the visuals

		public override Color? GetAlpha(Color lightColor)
		{
			// Makes sure the dropped bag is always visible
			return Color.Lerp(lightColor, Color.White, 0.4f);
		}

		public override void PostUpdate()
		{
			// Spawn some light and dust when dropped in the world
			Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);

			if (Item.timeSinceItemSpawned % 12 == 0)
			{
				Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

				// This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
				Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
				float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
				Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

				Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
				dust.scale = 0.5f;
				dust.fadeIn = 1.1f;
				dust.noGravity = true;
				dust.noLight = true;
				dust.alpha = 0;
			}
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			// Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
			Texture2D texture = TextureAssets.Item[Item.type].Value;

			Rectangle frame;

			if (Main.itemAnimations[Item.type] != null)
			{
				// In case this item is animated, this picks the correct frame
				frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
			}
			else
			{
				frame = texture.Frame();
			}

			Vector2 frameOrigin = frame.Size() / 2f;
			Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
			Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

			float time = Main.GlobalTimeWrappedHourly;
			float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

			time %= 4f;
			time /= 2f;

			if (time >= 1f)
			{
				time = 2f - time;
			}

			time = time * 0.5f + 0.5f;

			for (float i = 0f; i < 1f; i += 0.25f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			return true;
		}
	}
}