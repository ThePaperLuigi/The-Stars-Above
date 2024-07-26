using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Projectiles.Melee.Skofnung;

namespace StarsAbove.Items.Weapons.Melee
{
    public class Skofnung : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("Attacks with this weapon stab rapidly and have high knockback" +
				"\nHolding this weapon conjures the [c/395A37:Blade of Grudges], which will target and attack foes" +
				"\nThe [c/395A37:Blade of Grudges] does half of your defense as damage to foes every 4 seconds (calculated seperately)" +
				"\nWhen the [c/395A37:Blade of Grudges] inflicts a critical strike, gain [c/7E2626:Bloodstained Belone] for 8 seconds" +
				"\n[c/7E2626:Bloodstained Belone] increases damage by 10%" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 22;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 18;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 18;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 3;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 14;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.LightRed;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
											//Item.reuseDelay = 20;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<SkofnungStab>();
			Item.shootSpeed = 7f;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true;
		}

		int followUpTimer;
        
        public override bool CanUseItem(Player player)
		{
			

			



			return true;
		}

		public override bool? UseItem(Player player)
		{

			
			



			return true;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(BuffType<Buffs.Melee.Skofnung.SkofnungBuff>(), 2);
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Melee.Skofnung.SkofnungSummon>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Melee.Skofnung.SkofnungSummon>(), 8, 4, player.whoAmI, 0f);


			}
			base.HoldItem(player);
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 20);

			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{


			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(15));
			velocity.X = perturbedSpeed.X;
			velocity.Y = perturbedSpeed.Y;
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<SkofnungStab>(), damage, knockback, player.whoAmI);

			return false;



			
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Topaz, 1)
				.AddIngredient(ItemID.CrimtaneOre, 18)
				.AddIngredient(ItemType<EssenceOfBitterfrost>())
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe(1)
				.AddIngredient(ItemID.Topaz, 1)
				.AddIngredient(ItemID.DemoniteOre, 18)
				.AddIngredient(ItemType<EssenceOfBitterfrost>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
