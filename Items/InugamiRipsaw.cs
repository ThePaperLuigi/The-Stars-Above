using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    //ported from my tAPI mod because I don't want to make more artwork
    public class InugamiRipsaw : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Right click to activate Berserker Mode for 5 seconds, increasing damage dealt but dealing damage to yourself (20 second cooldown)" +
				"\n'It preys on stray fingers'" +
				$"");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.DamageType = DamageClass.Melee;
			Item.width = 68;
			Item.height = 26;
			Item.useTime = 7;
			Item.useAnimation = 25;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 6;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<Projectiles.InugamiRipsaw>();
			Item.shootSpeed = 40f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
			
			
			
			
			base.HoldItem(player);
		}
		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.BerserkerModeCooldown>()) && !Main.LocalPlayer.HasBuff(BuffType<Buffs.BerserkerMode>()))
				{
					
					

					SoundEngine.PlaySound(StarsAboveAudio.SFX_InugamiCharge, player.Center);//
					player.AddBuff(BuffType<Buffs.BerserkerMode>(), 300);
					for (int d = 0; d < 25; d++)
					{
						Dust.NewDust(player.position, player.width, player.height, 45, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
					}

				}
				else
				{
					return false;
				}


			}
			else
			{
				

			}
			return base.CanUseItem(player);
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				


			}
			return true;

		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.FlowerofFire, 1)
				.AddIngredient(ItemID.BottledWater, 1)
				.AddIngredient(ItemType<EssenceOfFingers>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}