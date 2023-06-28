using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.HunterSymphony;
using StarsAbove.Projectiles.HunterSymphony;

namespace StarsAbove.Items
{
    public class HunterSymphony : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hunter's Symphony");
			/* Tooltip.SetDefault("" +
				"Attacks with this weapon will sweep in an arc" +
                "\nMana regeneration is increased when not actively attacking" +
				"\nRight click while holding directional keys to play [c/72D16A:Hunter's Melodies] (20 second cooldown)" +
				"\n[c/72D16A:Hunter's Melodies] grant unique buffs based on the direction pressed to yourself and nearby allies for 20 seconds" +
				"\nUp: [c/49C0F0:Bracing Melody] grants 12 defense and Knockback Resistance" +
				"\nDown: [c/F04949:Challenger Melody] grants 6% increased damage" +
				"\nLeft: [c/49F059:Vitalty Melody] grants 15% increased Movement Speed and heals 40 HP on activation" +
				"\nRight: [c/AC49F0:Expertise Melody] grants 12% increased critical strike chance" +
				"\nAfter 3 [c/72D16A:Hunter's Melodies], right click to unleash [c/941818:Infernal Melody]" +
				"\n[c/941818:Infernal Melody] grants 20% increased damage to yourself and all nearby allies for 6 seconds" +
				"" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 75;           //The damage of your weapon
			Item.DamageType = DamageClass.Magic;         //Is your weapon a melee weapon?
			Item.width = 108;            //Weapon's texture's width
			Item.height = 108;           //Weapon's texture's height
			Item.useTime = 38;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 38;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.LightRed;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 0f;
			Item.mana = 15;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int currentSwing;
		int slashDuration;
		 
		
		bool altSwing;
		bool hasTarget = false;
		Vector2 enemyTarget = Vector2.Zero;

		int weaponInUseTimer;
		int songsPlayed;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse == 2)
			{
				if (modPlayer.SymphonySongsPlayed >= 3)
				{
					

					//



					return true;
				}
				else if (!player.HasBuff(BuffType<HunterSymphonyCooldown>()))
				{

					return true;
				}
				else
                {
					return false;
				}

			}

			return true;
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{

			
		}
        public override bool? UseItem(Player player)
        {
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse == 2)
			{
				
			}
			return true;
        }
        public override void HoldItem(Player player)
        {
			//weaponInUseTimer = 180;
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			modPlayer.HunterSymphonyHeld = true;
			weaponInUseTimer--;
			if(weaponInUseTimer < 0)
            {
				player.manaRegen += 80;
            }
			if(modPlayer.SymphonySongsPlayed == 1)
            {
				player.AddBuff(BuffType<InfernalMelody1>(), 2);  //
			}
			if (modPlayer.SymphonySongsPlayed == 2)
			{
				player.AddBuff(BuffType<InfernalMelody2>(), 2);  //
			}
			if (modPlayer.SymphonySongsPlayed >= 3)
			{
				player.AddBuff(BuffType<InfernalMelody3>(), 2);  //
			}
			//player.AddBuff(BuffType<BloodBladeBuff>(), 2);
			
			
			base.HoldItem(player);
        }

		

       
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse != 2)
            {
				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<HornSlash2>(), player.GetWeaponDamage(Item), knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<HornSlash1>(), player.GetWeaponDamage(Item), knockback, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<HornSlash1>(), player.GetWeaponDamage(Item), knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<HornSlash2>(), player.GetWeaponDamage(Item), knockback, player.whoAmI, 0f);

						altSwing = true;
					}
				}
			}
			else
            {
				if (modPlayer.SymphonySongsPlayed >= 3)
				{
					modPlayer.SymphonySongsPlayed = 0; //Reset stacks upon activation

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MusicRadiate>(), 0, 3, player.whoAmI, 0f, 4);

					return false;
				}
				if (player.controlUp && !player.HasBuff(BuffType<HunterSymphonyCooldown>()))
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MusicRadiate>(), 0, 3, player.whoAmI, 0f, 0);
					return false;
				}
				if (player.controlDown && !player.HasBuff(BuffType<HunterSymphonyCooldown>()))
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MusicRadiate>(), 0, 3, player.whoAmI, 0f, 1);
					return false;
				}
				if (player.controlLeft && !player.HasBuff(BuffType<HunterSymphonyCooldown>()))
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MusicRadiate>(), 0, 3, player.whoAmI, 0f, 2);
					return false;
				}
				if (player.controlRight && !player.HasBuff(BuffType<HunterSymphonyCooldown>()))
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MusicRadiate>(), 0, 3, player.whoAmI, 0f, 3);
					return false;
				}
				
			}
			
			return false;
		}

		public override void AddRecipes()
		{
		 
			CreateRecipe(1)
				.AddIngredient(ItemID.FlinxFur, 3)
				.AddIngredient(ItemID.Lens, 2)
				.AddIngredient(ItemID.TatteredCloth, 2)
				.AddIngredient(ItemID.Feather, 4)
				.AddIngredient(ItemID.AntlionMandible, 4)
				.AddIngredient(ItemID.Stinger, 2)
				.AddIngredient(ItemType<EssenceOfTheHunt>())
				.AddTile(TileID.Anvils)
				.Register();

			
		}
	}
	
}
