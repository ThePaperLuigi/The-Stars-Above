using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.RedMage;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles.RedMage;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Magic
{
    public class GundbitStaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 55;
			//The damage of your weapon
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 52;            //Weapon's texture's width
			Item.height = 52;           //Weapon's texture's height
			Item.useTime = 10;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 10;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			//item.knockback = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = 1;
			Item.shootSpeed = 14;
			Item.mana = 18;
		}
		int stabTimer;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
		
			return true;

			
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		public override void HoldItem(Player player)
		{
			float launchSpeed = 12f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
			player.AddBuff(BuffType<RedMageHeldBuff>(), 10);
			if (player.ownedProjectileCounts[ProjectileType<RedMageFocus>()] < 1)
			{
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageFocus>(), 0, 0, player.whoAmI, 0f);
			}
			
			
			//player.GetModPlayer<WeaponPlayer>().blackMana--;
			//player.GetModPlayer<WeaponPlayer>().whiteMana--;
			base.HoldItem(player);
		}
        public override bool? UseItem(Player player)
        {
			
			
			return base.UseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
		
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			if (player.altFunctionUse == 2)
			{

				
			}
			else
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapierFast>(), 0, 0, player.whoAmI, 0f);

				SoundEngine.PlaySound(StarsAboveAudio.SFX_Verholy, player.Center);
				Projectile.NewProjectile(source, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y, 0, 0, ProjectileType<Verholy>(), damage, 0, player.whoAmI, 0f);


			}
			return false;
		}

		public override void AddRecipes()
		{
			/*
			CreateRecipe(1)
				.AddIngredient(ItemType<PrismaticCore>(), 5)
				.AddIngredient(ItemID.SoulofNight, 15)
				.AddIngredient(ItemID.SoulofLight, 15)
				.AddIngredient(ItemID.DarkShard, 1)
				.AddIngredient(ItemID.LightShard, 1)
				.AddIngredient(ItemID.Ruby, 7)
				.AddIngredient(ItemID.ManaCrystal, 3)
				.AddIngredient(ItemType<EssenceOfBalance>())
				.AddTile(TileID.Anvils)
				.Register();*/
		}
	}
}
