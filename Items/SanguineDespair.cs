using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Items
{
    public class SanguineDespair : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sanguine Despair");

			Tooltip.SetDefault("WORK IN PROGRESS WEAPON" +
                "\nAttacks with this weapon consume 10 health per cast, can be controlled, and can pierce up to three times" +
				"\nHolding this weapon grants the buff [Feral Despair], which increases damage by 2% per missing HP" +
				"\nRight click to unleash [Surging Vampirism], an erratic bolt of energy that explodes in a massive explosion upon striking an enemy (30 second cooldown)" +
				"\n[Surging Vampirism] deals bonus damage based on missing HP (10% per missing HP, capping at 400%)" +
                "\nAdditionally, [Surging Vampirism] heals based on 10% of damage dealt to the first foe struck" +
				"" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 60;           //The damage of your weapon
			Item.mana = 25;
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 30;            //Weapon's texture's width
			Item.height = 176;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HoldUp;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.LightRed;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 116;
			Item.shootSpeed = 30f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				


			}
			else
			{
				

			}
			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.statMana >= 50)
			{
				player.statMana -= 50;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,type, damage * 2, knockback, player.whoAmI);
			}
			
			return false;
		}
		public override void AddRecipes()
		{
			/*CreateRecipe(1)
				.AddIngredient(ItemType<DullTotemOfLight>())
				.AddIngredient(ItemType<EssenceOfSurpassingLimits>())
				.AddTile(TileID.Anvils)
				.Register();*/
		}
	}
}
