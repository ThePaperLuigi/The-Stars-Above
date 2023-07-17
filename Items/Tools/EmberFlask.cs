using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;
using System;
using StarsAbove.Buffs.EmberFlask;

namespace StarsAbove.Items.Tools
{

    public class EmberFlask : ModItem
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 1;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			Item.ResearchUnlockCount = 1;

		}

	
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		
		public override bool CanUseItem(Player player) {
			if (!player.HasBuff(BuffType<EmberFlaskUsed>()) && !player.HasBuff(BuffID.PotionSickness))
			{
				return true;
			}
			else
            {
				return false;
            }
		}

		public override bool? UseItem(Player player) {

			float dustAmount = 20f;

			player.AddBuff(BuffID.PotionSickness, 60 * 20);

			player.potionDelayTime = 60 * 20;

			player.Heal((int)(player.statLifeMax2 * 0.3));
			player.AddBuff(BuffType<EmberFlaskUsed>(), 600);
			//Dust
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
				spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
				int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = player.Center + spinningpoint5;
				Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
			}
			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<Materials.StellarRemnant>(), 40)
				.AddCustomShimmerResult(ItemType<Materials.StellarRemnant>(), 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}