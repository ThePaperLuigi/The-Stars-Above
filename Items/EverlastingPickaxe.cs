using Microsoft.Xna.Framework;
using StarsAbove.Buffs.EverlastingPickaxe;
using StarsAbove.Projectiles.EverlastingPickaxe;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class EverlastingPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			//WIP
			/*if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				Tooltip.SetDefault("" +
				"Can quickly mine Auric Ore" +
				"\nRight click to place a dusting of [Everlasting Gunpowder], which lasts for 20 seconds" +
				"\nSwinging the pickaxe causes consecutive explosions centered on the [Everlasting Gunpowder], destroying tiles " +
				"\nPress the Weapon Action Key to load [Everlasting Gunpowder] into the pickaxe" +
				"\nWhen [Everlasting Gunpowder] is loaded, mining is disabled and attacking capabilities are extended" +
				"\nAttacks swing in an alternating arc, firing a portion of [Everlasting Gunpowder] towards the cursor" +
				"\nOnce the [Everlasting Gunpowder] makes contact with a tile or an enemy, it will detonate multiple times before disappearing" +
				"\nPressing the Weapon Action Key when [Everlasting Gunpowder] is loaded will revert to the original state" +
				"\n'At the Netherworld's bottom... I'll be waiting'" +
				$"");
			}
			else
			{
				Tooltip.SetDefault("" +
				"Can quickly mine any ore" +
				"\nRight click to place a dusting of [Everlasting Gunpowder], which lasts for 20 seconds" +
				"\nSwinging the pickaxe causes consecutive explosions centered on the [Everlasting Gunpowder], destroying tiles " +
				"\nPress the Weapon Action Key to load [Everlasting Gunpowder] into the pickaxe" +
				"\nWhen [Everlasting Gunpowder] is loaded, mining is disabled and attacking capabilities are extended" +
				"\nAttacks swing in an alternating arc, firing a portion of [Everlasting Gunpowder] towards the cursor" +
				"\nOnce the [Everlasting Gunpowder] makes contact with a tile or an enemy, it will detonate multiple times before disappearing" +
				"\nPressing the Weapon Action Key when [Everlasting Gunpowder] is loaded will revert to the original state" +
				"\n'At the Netherworld's bottom... I'll be waiting'" +
				$"");
			}*/
			DisplayName.SetDefault("The Everlasting Pickaxe");
			Tooltip.SetDefault("" +
				"WORK IN PROGRESS" +
                "\nCan quickly mine any ore" +
				"\nRight click to place a dusting of [Everlasting Gunpowder], which lasts for 20 seconds" +
				"\nSwinging the pickaxe causes consecutive explosions centered on the [Everlasting Gunpowder], destroying tiles " +
				"\nPress the Weapon Action Key to load [Everlasting Gunpowder] into the pickaxe" +
				"\nWhen [Everlasting Gunpowder] is loaded, mining is disabled and attacking capabilities are extended" +
				"\nAttacks swing in an alternating arc, firing a portion of [Everlasting Gunpowder] towards the cursor" +
				"\nOnce the [Everlasting Gunpowder] makes contact with a tile or an enemy, it will detonate multiple times before disappearing" +
				"\nPressing the Weapon Action Key when [Everlasting Gunpowder] is loaded will revert to the original state" +
				"\n'At the Netherworld's bottom... I'll be waiting'" +
				$"");


			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 99;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 100;            //Weapon's texture's width
			Item.height = 100;           //Weapon's texture's height
			Item.useTime = 4;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 12;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Swing;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 3;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 4f;
			Item.pick = 250;
			Item.tileBoost += 5;
			Item.useTurn = true;


			

		}
		bool altSwing;
		int currentSwing;
		int slashDuration;
		int comboTimer;

		bool attackModeEnabled;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override void UpdateInventory(Player player)
        {

			
        }
        public override void HoldItem(Player player)
        {
			//When the Weapon Action Key is pressed...
			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
			{
				//1. Swap stance (This disables mining, increases use time, and hides the weapon when swung.)
				if (attackModeEnabled)
				{
					attackModeEnabled = false;

				}
				else
				{
					attackModeEnabled = true;

				}
				//2. Spawn the swap animation projectile. Probably just the sprite of the pickaxe spinning and dust + SFX. Maybe screen shake.

			}

			if(attackModeEnabled)
            {
				player.AddBuff(BuffType<EverlastingGunpowderLoaded>(), 10);
				Item.useTime = 25;
				Item.useAnimation = 25;
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.shootSpeed = 4f;
				Item.pick = 0;
			}
			else
            {
				Item.useTime = 4;
				Item.useAnimation = 12;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.shootSpeed = 4f;
				Item.pick = 250;
			}

        }



        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			
		}

		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse != 2 && attackModeEnabled)
			{
				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<EverlastingPickaxeSwing2>(), damage, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<EverlastingPickaxeSwing1>(), damage, knockback, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<EverlastingPickaxeSwing1>(), damage, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<EverlastingPickaxeSwing2>(), damage, knockback, player.whoAmI, 0f);

						altSwing = true;
					}
				}
			}

			return false;
		}

		public override void AddRecipes()
		{
			/*
			CreateRecipe(1)
				.AddIngredient(ItemID.ButchersChainsaw, 1)
				.AddIngredient(ItemID.Chain, 3)
				.AddIngredient(ItemID.LavaBucket, 1)
				.AddIngredient(ItemID.LunarBar, 8)
				.AddIngredient(ItemID.FragmentSolar, 18)
				.AddIngredient(ItemType<EssenceOfTheOverwhelmingBlaze>())
				.AddTile(TileID.Anvils)
				.Register();
			*/
		}
	}
}
