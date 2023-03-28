using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Items.Prisms;

namespace StarsAbove.Items
{
    public class KarlanTruesilver : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Unleashes powerful slashes with unlimited piercing" +
                "\nCritical strikes deal bonus damage based on 10% of your current HP" +
                "\nDamage is halved per pierce" +
				"\n'When the aether darkens, 'tis an omen that snowfall draws dear...'" +
				$"");  //The (English) text shown below your weapon's name
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 90;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 82;            //Weapon's texture's width
			Item.height = 84;           //Weapon's texture's height
			Item.useTime = 70;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 70;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Pink;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = false;
			Item.scale = 2f;
			Item.shoot = ProjectileType<TruesilverSlash>();
			Item.shootSpeed = 10f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		public override bool CanUseItem(Player player)
		{
			
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
			SoundEngine.PlaySound(StarsAboveAudio.SFX_TruesilverSlash, player.Center);
			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<PrismaticCore>(), 3)
				.AddIngredient(ItemID.BreakerBlade, 1)
				.AddIngredient(ItemType<EssenceOfSilverAsh>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
