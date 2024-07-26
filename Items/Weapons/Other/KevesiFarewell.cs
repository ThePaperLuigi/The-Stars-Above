using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Other.Farewells;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using StarsAbove.Projectiles.Summon.KeyOfTheKingsLaw;
using StarsAbove.Systems;
using StarsAbove.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Other
{
    public class KevesiFarewell : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Kevesi Farewell");
			/* Tooltip.SetDefault("Playing this instrument grants the buff [c/69B5FC:Off-seer's Journey] to yourself for 2 minutes" +
				"\n[c/69B5FC:Off-seer's Journey] grants increased Luck and maximum Luck while increasing enemy spawn rates (8 minute cooldown) " +
				"\nAfter defeating an enemy with the Kevesi Farewell in your inventory while [c/69B5FC:Off-seer's Journey] is active, gain the buff [c/FC6969:Farewell of Flames] for 10 seconds" +
				"\n[c/FC6969:Farewell of Flames] grants an additional increase to Luck" +
				"\nIf used while wielding the strength of the Umbral Aspect (through trading in Multiplayer), further increase the potency of [c/69B5FC:Off-seer's Journey]'s Luck increase" +
				"" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 0;           //Not a weapon.
			//Item.mana = 80;
			//Item.DamageType = DamageClass.Summon;          //Is your weapon a melee weapon?
			Item.width = 40;            //Weapon's texture's width
			Item.height = 40;           //Weapon's texture's height
			Item.useTime = 480;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 480;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.EatFood;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Green;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = StarsAboveAudio.SFX_KevesiTune;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<KeyOfTheKingsLawProjectile>();
			Item.shootSpeed = 15f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		//SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

		int randomBuff;
        public override void UpdateInventory(Player player)
        {
			player.GetModPlayer<WeaponPlayer>().KevesiFarewellInInventory = true;


            base.UpdateInventory(player);
        }
        public override bool CanUseItem(Player player)
		{
			if(player.HasBuff(BuffType<FarewellCooldown>()))
            {
				return false;
            }
			return base.CanUseItem(player);
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}

        public override bool? UseItem(Player player)
        {
			player.AddBuff(BuffType<OffSeersJourney>(), 7200);
			player.AddBuff(BuffType<FarewellCooldown>(), 28800);

			return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			return false;
		}
		public override void AddRecipes()
		{
			
				
			CreateRecipe(1)
				.AddIngredient(ItemID.Wood, 8)
				.AddIngredient(ItemID.Gravestone)
				.AddIngredient(ItemType<EssenceOfFarewells>())
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemID.Wood, 8)
				.AddIngredient(ItemID.Tombstone)
				.AddIngredient(ItemType<EssenceOfFarewells>())
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemID.Wood, 8)
				.AddIngredient(ItemID.CrossGraveMarker)
				.AddIngredient(ItemType<EssenceOfFarewells>())
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemID.Wood, 8)
				.AddIngredient(ItemID.Headstone)
				.AddIngredient(ItemType<EssenceOfFarewells>())
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemID.Wood, 8)
				.AddIngredient(ItemID.Obelisk)
				.AddIngredient(ItemType<EssenceOfFarewells>())
				.AddTile(TileID.Anvils)
				.Register();

		}
	}

	
}
