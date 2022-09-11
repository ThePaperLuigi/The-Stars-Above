using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using Terraria;using Terraria.DataStructures;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
	public class AegisDriver : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("With 40 mana available, this weapon will be thrown with special effects, consuming 40 mana in the process" +
				"\nThe blade will ignite, dealing increased damage and will burn foes" +
				"\nBurning foes will take bonus damage when they are reignited" +
				"\nIf no mana is available, the weapon will be unpowered and deal half damage" +
				"\nAttacking with the powered version of this weapon charges the [c/FF5858:Aegis Gauge]" +
				"\nOnce the [c/FF5858:Aegis Gauge] is filled, the next attack will explode the foe, dealing massive critical damage while consuming the [c/FF5858:Aegis Gauge]" +
				"\nThe [c/FF5858:Aegis Gauge] will also slowly charge over time when this weapon is held" +
				"\n'This is the Aegis's power!'" +
				$"");  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 24;           //The damage of your weapon
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 72;            //Weapon's texture's width
			Item.height = 72;           //Weapon's texture's height
			Item.useTime = 50;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 50;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = 8;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			
			
			Item.shoot = ProjectileType<AegisDriverOff>();
			Item.shootSpeed = 4f;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true;
		}

		int cooldown = 0;

		public override bool CanUseItem(Player player)
		{
			

			return base.CanUseItem(player);
		}
		public override void HoldItem(Player player)
		{
			cooldown++;
			
			if (cooldown >= 20)
			{
				player.GetModPlayer<StarsAbovePlayer>().aegisGauge += 1;
				cooldown = 0;
			}
			
			
			
			base.HoldItem(player);
		}
		
		


		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		
		/*public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = mod.(Texture2D)Request<Texture2D>("Glow/AegisDriverOn_Glow");
			spriteBatch.Draw
			(
				texture,
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale,
				SpriteEffects.None,
				0f
			);
		}*/
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.statMana >= 40)
			{
				player.GetModPlayer<StarsAbovePlayer>().aegisGauge += 10;
				player.statMana -= 40;
				player.manaRegenDelay = 300;
				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y,ProjectileType<AegisDriverOn>(), damage, knockback, player.whoAmI);
			}
			else
			{
				
				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y,ProjectileType<AegisDriverOff>(), damage, knockback, player.whoAmI);
			}
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Ruby, 6)
				.AddIngredient(ItemID.EnchantedBoomerang, 1)
				.AddIngredient(ItemType<EssenceOfTheAegis>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
