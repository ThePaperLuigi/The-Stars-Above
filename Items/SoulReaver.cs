using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.BloodBlade;
using StarsAbove.Projectiles.BloodBlade;

namespace StarsAbove.Items
{
    public class SoulReaver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Reaver");
			Tooltip.SetDefault("" +
				"[c/F7D76A:Hold left click to charge the weapon; if the weapon is fully charged, the attack varies]" +//Charge attack sentence
				"Holding this weapon enchants your cursor, causing nearby foes to become inflicted with Shadowflame for half a second" +
                "\nAttacks with this weapon swing in a wide arc and will strike multiple times" +
				"\nCharging this weapon will execute [Soul Harvest], teleporting you to your cursor and dealing potent damage to foes around you" +
                "\n[Soul Harvest] will deal 50% increased damage to foes inflicted with Shadowflame" +
                "\nGain Invincibility for 1 second after use of [Soul Harvest], but become inflicted with Vulnerability for 1 second after, halving defenses" +
                "\nAfter [Soul Harvest] you will be inflicted with [Soul Split] for 10 seconds, reducing the damage of [Soul Harvest] by 2/3" +
                "\nCritical strikes with [Soul Harvest] will instead reduce the duration of [Soul Split] to 2 seconds" +
				"\nDefeating foes will grant a stack of [Dark Soul], granting 2% per stack (capping at 10 stacks)" +
                "\nLose all stacks of [Dark Soul] after leaving combat" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 65;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;         //Is your weapon a melee weapon?
			Item.width = 108;            //Weapon's texture's width
			Item.height = 108;           //Weapon's texture's height
			Item.useTime = 18;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 18;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Yellow;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 0f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int currentSwing;
		int slashDuration;
		 
		
		bool altSwing;
		bool hasTarget = false;
		Vector2 enemyTarget = Vector2.Zero;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
			
			float launchSpeed = 110f;
			float launchSpeed2 = 10f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * 26f;
			

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			if (player.altFunctionUse == 2)
			{
				if(!player.HasBuff(BuffType<Buffs.BloodBlade.BladeArtDragonPrepBuff>()) && !player.HasBuff(BuffType<Buffs.BloodBlade.BladeArtDragonCooldown>()) && player.statLife > (int)(player.statLifeMax*0.2) + 1)
                {
					player.AddBuff(BuffType<BladeArtDragonCooldown>(), 1200);
					player.AddBuff(BuffType<Invincibility>(), 50);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<BloodArtPrep>(), player.GetWeaponDamage(Item)*4, 3, player.whoAmI, 0f);
					player.statLife -= (int)(player.statLifeMax * 0.2);

					
					return true;
				}
				else
                {
					return false;
                }
				
			}
			else
            {
				
            }
			
			return true;
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{

			
		}
        public override bool? UseItem(Player player)
        {


			


			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {

			player.AddBuff(BuffType<BloodBladeBuff>(), 2);

			base.HoldItem(player);
        }

       
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 
			if(player.altFunctionUse != 2)
            {
				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<BloodSlash2>(), player.GetWeaponDamage(Item), knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<BloodSlash1>(), player.GetWeaponDamage(Item), knockback, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<BloodSlash1>(), player.GetWeaponDamage(Item), knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<BloodSlash2>(), player.GetWeaponDamage(Item), knockback, player.whoAmI, 0f);

						altSwing = true;
					}
				}
			}
			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 6)
				.AddIngredient(ItemID.SoulofFright, 8)
				.AddIngredient(ItemID.BlackFairyDust, 2)
				.AddIngredient(ItemType<EssenceOfLifethirsting>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
