using Microsoft.Xna.Framework;

using Terraria.ID;

using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Magic.EternalStar;
using StarsAbove.Systems;

namespace StarsAbove.Items.Consumables.EternalStar
{

    public class AmethystFragment : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Amethyst Fragment");
			/* Tooltip.SetDefault("Shattered star fragments" +
				"\n[c/F1AF42:Should not be able to be read.]" +
				"\n"); */
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			ItemID.Sets.ItemNoGravity[Item.type] = true; Item.ResearchUnlockCount = 0;

		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool ItemSpace(Player player)
		{
			return true;
		}

		public override bool CanPickup(Player player)
		{
			return true;
		}

		public override bool OnPickup(Player player)
		{
			player.AddBuff(BuffType<AmethystBuff>(), 480);
			player.GetModPlayer<WeaponPlayer>().EternalGauge += 5;
			return false;
		}
		public override bool CanUseItem(Player player) {

			return true;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			Lighting.AddLight(new Vector2(Item.Center.X, Item.Center.Y), 81 * 0.001f, 194 * 0.001f, 58 * 0.001f);//Vanilla code adapted
			for (int i = 0; i < 1; i++)
			{
				int num7 = 16;
				float num9 = 6f;
				float num8 = (float)(Math.Cos((double)Main.GlobalTimeWrappedHourly % 2.40000009536743 / 2.40000009536743 * 6.28318548202515) / 5 + 0.5);
				float amount1 = 0.5f;
				float num10 = 0.0f;
				float addY = 0f;
				float addHeight = 0f;
				SpriteEffects spriteEffects = SpriteEffects.None;
				Texture2D texture = Terraria.GameContent.TextureAssets.Item[Item.type].Value;
				Vector2 vector2_3 = new Vector2((float)(Terraria.GameContent.TextureAssets.Item[Item.type].Width() / 2), (float)(Terraria.GameContent.TextureAssets.Item[Item.type].Height() / 1 / 2));
				Vector2 position1 = Item.Center - Main.screenPosition - new Vector2((float)texture.Width, (float)(texture.Height / 1)) * Item.scale / 2f + vector2_3 * Item.scale + new Vector2(0.0f, addY + addHeight + 0);
				Color color2 = new Color(255, 255, 255, 150);
				Rectangle r = Terraria.GameContent.TextureAssets.Item[Item.type].Frame(1, 1, 0, 0);
				for (int index2 = 0; index2 < num7; ++index2)
				{
					Color newColor2 = color2;
					Color color3 = Item.GetAlpha(newColor2) * (0.85f - num8);
					Vector2 position2 = new Vector2(Item.Center.X, Item.Center.Y) + ((float)((double)index2 / (double)num7 * 6.28318548202515) + rotation + num10).ToRotationVector2() * (float)(4.0 * (double)num8 + 2.0) - Main.screenPosition - new Vector2((float)texture.Width, (float)(texture.Height / 1)) * Item.scale / 2f + vector2_3 * Item.scale + new Vector2(0.0f, addY + addHeight + 0);
					Main.spriteBatch.Draw((Texture2D)Terraria.GameContent.TextureAssets.Item[Item.type], position2, new Microsoft.Xna.Framework.Rectangle?(r), color3, rotation, vector2_3, Item.scale * 1.35f, spriteEffects, 0.0f);
				}
			}
			return true;
		}
		public override bool? UseItem(Player player) {
			
			return true;
		}
		public override void AddRecipes()
		{
			//ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(ItemID.NightKey, 1);
			//recipe.AddIngredient(ItemID.DarkShard, 1);
			//recipe.AddTile(TileID.AdamantiteForge);
			//recipe.SetResult(this);
			//recipe.AddRecipe();
		}
	}
}