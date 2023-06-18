using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Prisms
{
    public class PrismaticCore : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Prismatic Core");
			/* Tooltip.SetDefault("A shimmering catalyst from beyond the rift" +
				"\nUtilized in spatial crafting, or can be sold at a high price at shops" +
				"\n[c/FF0D5D:Used to craft Stellar Nova upgrades]" +
				""); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 15;

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Red;
			Item.maxStack = 999;
		}
		

		public override bool OnPickup(Player player)
		{
			bool pickupText = false;

			for (int i = 0; i < 50; i++)
			{
				if (player.inventory[i].type == ItemID.None && pickupText == false)
				{
					Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
					CombatText.NewText(textPos, new Color(255, 198, 125, 105), "Prismatic Core acquired!", false, false);
					pickupText = true;
				}
				else
				{

				}
			}
			return true;
		}
		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<ApocryphicPrism>())
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<AlchemicPrism>())
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<CastellicPrism>())
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<CrystallinePrism>())
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<EverflamePrism>())
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<LucentPrism>())
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<PhylacticPrism>())
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<RadiantPrism>())
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<RefulgentPrism>())
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemType<VerdantPrism>())
				.Register();

			/*ModRecipe recipe11 = new ModRecipe(mod);
			recipe11.AddIngredient(ItemType<SpatialPrism>());

			recipe11.SetResult(this);
			recipe11.AddRecipe();

			ModRecipe recipe12 = new ModRecipe(mod);
			recipe12.AddIngredient(ItemType<BurnishedPrism>());

			recipe12.SetResult(this);
			recipe12.AddRecipe();

			ModRecipe recipe13 = new ModRecipe(mod);
			recipe13.AddIngredient(ItemType<PaintedPrism>());

			recipe13.SetResult(this);
			recipe13.AddRecipe();

			ModRecipe recipe14 = new ModRecipe(mod);
			recipe14.AddIngredient(ItemType<VoidsentPrism>());

			recipe14.SetResult(this);
			recipe14.AddRecipe();

			ModRecipe recipe15 = new ModRecipe(mod);
			recipe15.AddIngredient(ItemType<LightswornPrism>());

			recipe15.SetResult(this);
			recipe15.AddRecipe();*/
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
		public override bool PreDrawInInventory(SpriteBatch sB, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			// Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
			Texture2D texture = TextureAssets.Item[Item.type].Value;

			Texture2D glow = ModContent.Request<Texture2D>("StarsAbove/Effects/GodRays").Value;



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

				Main.spriteBatch.Draw(texture, position + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), 0, origin, scale, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				Main.spriteBatch.Draw(texture, position + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), 0, origin, scale, SpriteEffects.None, 0);

			}

			float radians2 = (timer) * MathHelper.TwoPi;

			Main.spriteBatch.Draw(glow, position, frame, new Color(255,255, 255, 0), radians2, origin, 0.2f, SpriteEffects.None, 0);

			return true;
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