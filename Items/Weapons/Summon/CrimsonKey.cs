
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Summon.KeyOfTheSinner;

namespace StarsAbove.Items.Weapons.Summon
{
    public class CrimsonKey : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Key of the Sinner");
			/* Tooltip.SetDefault("Calls forth an instance of your rebellious heart" +
				"\nUnleashes incredibly powerful bullets" +
				"\nGain a sizeable amount of bonus damage relative to Max HP and MP" +
				"\nAdditonally, the highest critical chance percentage of all damage types is converted into bonus damage" +
				"\nOnly one can be summoned at a time" +
				"\nUses 5 minion slots" +
				"\n'Ravage them'"
				+ $""); */

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 150;
			Item.width = 21;
			Item.height = 21;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = 1;
			Item.noMelee = true;
			Item.knockBack = 6;
			Item.rare = ItemRarityID.Red;
			Item.UseSound = StarsAboveAudio.SFX_summoning;
			
			Item.shoot = ProjectileType<Satanael>();
			Item.buffType = BuffType<Buffs.Satanael>(); //The buff added to player after used the item
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

        public override void HoldItem(Player player)
        {
			Item.damage = (int)(40 + player.statLifeMax2 / 20 + player.statManaMax2 / 20 + (Math.Max(Math.Max(Math.Max(player.GetCritChance(DamageClass.Melee), player.GetCritChance(DamageClass.Magic)), player.GetCritChance(DamageClass.Ranged)), player.GetCritChance(DamageClass.Throwing))));
            base.HoldItem(player);
        }
        public override bool CanUseItem(Player player)
        {
			
			return base.CanUseItem(player);
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}


		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			
			for (int l = 0; l < Main.projectile.Length; l++)
			{
				Projectile proj = Main.projectile[l];
				if (proj.active && proj.type == Item.shoot && proj.owner == player.whoAmI)
				{
					proj.active = false;
				}
			}
			SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, player.Center);

			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
			player.SpawnMinionOnCursor(source, player.whoAmI, type, damage, knockback);

			return false;



		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 12)
				.AddIngredient(ItemID.ShroomiteBar, 12)
				.AddIngredient(ItemType<EssenceOfSin>())
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}
