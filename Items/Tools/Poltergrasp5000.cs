using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;
using System;
using StarsAbove.Buffs.ShepherdSunstone;
using Terraria.GameContent.Drawing;

namespace StarsAbove.Items.Tools
{

    public class Poltergrasp5000 : ModItem
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
			Item.useAnimation = 35;
			Item.useTime = 35;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item1;
			Item.consumable = false;
			Item.ResearchUnlockCount = 1;

		}
	
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
        public override void HoldItem(Player player)
        {
			int range = 400;
			for (int j = 0; j < Main.maxItems; j++)
			{
				Item item = Main.item[j];
				if (item.active && item.noGrabDelay == 0 && !ItemLoader.GrabStyle(item, player) && ItemLoader.CanPickup(item, player))
				{
					if (player.CanPullItem(item, player.ItemSpace(item)) && item.Distance(player.Center) < range)
					{
						ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
						particleOrchestraSettings.PositionInWorld = item.Center;
						ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, particleOrchestraSettings,player.whoAmI);
						for (int d = 0; d < 10; d++)
						{
							Dust.NewDust(player.position, Item.width, Item.height, DustID.GemDiamond, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
						}
						item.Center = player.Center;
					}
				}
			}
			for (int i = 0; i < 5; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * range);
				offset.Y += (float)(Math.Cos(angle) * range);

				Dust d = Dust.NewDustPerfect(player.Center + offset, DustID.GemDiamond, player.velocity, 120, default(Color), 0.7f);
				d.fadeIn = 0.0001f;
				d.noGravity = true;
			}
			
			base.HoldItem(player);
        }
        public override bool CanUseItem(Player player) {

			
			return true;
		}

		public override bool? UseItem(Player player) {

			for (int j = 0; j < Main.maxItems; j++)
			{
				Item item = Main.item[j];
				if (item.active && item.noGrabDelay == 0 && !ItemLoader.GrabStyle(item, player) && ItemLoader.CanPickup(item, player))
				{
					if (player.CanPullItem(item, player.ItemSpace(item)) && item.Distance(player.Center) < 800)
					{
						ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
						particleOrchestraSettings.PositionInWorld = item.Center;
						ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, particleOrchestraSettings, player.whoAmI);
						particleOrchestraSettings.PositionInWorld = player.Center;
						ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, particleOrchestraSettings, player.whoAmI);
						item.Center = player.Center;
					}
				}
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