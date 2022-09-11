using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using Terraria.Utilities;
using Terraria.WorldBuilding;
using Terraria;using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using StarsAbove;
using StarsAbove.Items;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using StarsAbove.NPCs;
using Microsoft.Xna.Framework.Audio;

using StarsAbove.Dusts;
using StarsAbove.Items.Consumables;
using System.Diagnostics.Contracts;
using StarsAbove.Items.Prisms;
using StarsAbove.UI.StellarNova;


namespace StarsAbove.Items.Consumables
{
	
	public class StarBitGreen : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Star Bit");
			Tooltip.SetDefault("" +
				"\n[c/F1AF42:Shouldn't be in your inventory.]" +
				"\n");
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.White;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
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
			if (player.HeldItem.ModItem is LuminaryWand)
			{
				int type = ProjectileType<Projectiles.Starchild.GreenStarBit>();
				Vector2 position = player.GetModPlayer<StarsAbovePlayer>().lumaPosition;

				float Speed = 28f;  //projectile speed

				Vector2 vector8 = player.Center;
				float rotation = (float)Math.Atan2(vector8.Y - (Main.MouseWorld.Y), vector8.X - (Main.MouseWorld.X));

				for (int d = 0; d < 25; d++)
				{
					float Speed2 = Main.rand.NextFloat(10, 18);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
					int dustIndex = Dust.NewDust(vector8, 0, 0, 31, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}//Dust
				for (int d = 0; d < 45; d++)
				{
					float Speed3 = Main.rand.NextFloat(8, 14);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
					int dustIndex = Dust.NewDust(vector8, 0, 0, 31, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}//Dust

				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, 60, 0, player.whoAmI);


				Main.projectile[index].originalDamage = 60;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_StarbitShoot, player.Center);
			}
			return false;
		}
		public override bool CanUseItem(Player player) {

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