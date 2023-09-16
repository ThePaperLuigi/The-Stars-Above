using Microsoft.Xna.Framework;
using StarsAbove.Buffs.CarianDarkMoon;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.CarianDarkMoon;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Magic
{
    public class CarianDarkMoon : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("" +
				"Attacks with this weapon will sweep in a wide arc" +
                "\nCritical strikes will inflict Frostburn for 3 seconds" +
				"\nRight click to consume 100 Mana, gaining the buff [c/92D7FF:Moonlit Greatblade] for 18 seconds after a short delay, during which the weapon is unusable" +
				"\n[c/92D7FF:Moonlit Greatblade] empowers this weapon with lunar energy, granting 10 extra damage and 12 armor penetration" +
                "\nDuring this time, empowered attacks will fire piercing ranged projectiles, and melee strikes are guaranteed to inflict Frostburn" +
                "\nProjectiles can pierce infinitely, but damage is continually halved per hit" +
				"\n[c/92D7FF:Moonlit Greatblade] prevents natural mana regeneration when active, and has an 8 second cooldown" +
				"\n'Now cometh the age of stars...'" +
				$""); */
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}
		public int aspect = 0; // Astral = 0, Umbral = 1, Spatial = 2
		public override void SetDefaults()
		{
			
			Item.damage = 19;          
			Item.DamageType = DamageClass.Magic;
			Item.width = 108;           
			Item.height = 108;          
			Item.useTime = 45;        
			Item.useAnimation = 45;      
			Item.useStyle = 5;         
			Item.knockBack = 12; 
			Item.value = Item.buyPrice(gold: 1);   
			Item.rare = 2; 
			Item.UseSound = SoundID.Item1;  
			Item.autoReuse = true;
			Item.shoot = 337;
			Item.shootSpeed = 0f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			
		}
		int currentSwing;
		int slashDuration;
		 
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
			if (player.HasBuff(BuffType<PrepDarkmoon>()))
            {
				return false;
            }
			
			

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse == 2 && !player.HasBuff(BuffType<MoonlitGreatblade>()) && !player.HasBuff(BuffType<MoonlitCooldown>()))
			{
				if (player.statMana >= 100)
                {

					player.statMana -= 100;
					player.manaRegenDelay = 600;
					
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, 0, -30, ProjectileType<DarkmoonSpawn>(), 0, 0, player.whoAmI, 0f);//Spawn the sword.
					//The sword will be thrown above the player, spinning quickly but then slowing down, and then explode in a bunch of particles as it becomes empowered.
					return true;
				}
				else
                {
					return false;
                }
				
			}
			else
            {
				

				
				return true;
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
			float launchSpeed = 110f;
			float launchSpeed2 = 10f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity3 = direction * launchSpeed;
			Vector2 projectile = direction * launchSpeed2;

			Vector2 muzzleOffset = Vector2.Normalize(projectile) * 50f;

			if (player.altFunctionUse != 2 && Main.myPlayer == player.whoAmI)
			{
				if (currentSwing == 0)
				{
					if (player.HasBuff(BuffType<MoonlitGreatblade>()))
					{
						
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<CarianSwingE1>(), player.GetWeaponDamage(Item) + 10, 3, player.whoAmI, 0f);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter + muzzleOffset, projectile, ProjectileType<MoonlightAttack>(), player.GetWeaponDamage(Item) + 10, 3, player.whoAmI, 0f);

						

						SoundEngine.PlaySound(SoundID.Item1, player.position);

					}
					else
					{
						
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<CarianSwing1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
						
						SoundEngine.PlaySound(SoundID.Item1, player.position);
					}

					currentSwing++;
				}
				else
				{
					if (player.HasBuff(BuffType<MoonlitGreatblade>()))
					{
						
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<CarianSwingE2>(), player.GetWeaponDamage(Item) + 10, 3, player.whoAmI, 0f);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter + muzzleOffset, projectile, ProjectileType<MoonlightAttack>(), player.GetWeaponDamage(Item) + 10, 3, player.whoAmI, 0f);
						
						SoundEngine.PlaySound(SoundID.Item1, player.position);

					}
					else
					{
						
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<CarianSwing2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
						
						SoundEngine.PlaySound(SoundID.Item1, player.position);
					}
					currentSwing = 0;
				}
			}


			return true;
        }
        public override void HoldItem(Player player)
        {
			

			if(player.HasBuff(BuffType<MoonlitGreatblade>()))
            {
				player.GetArmorPenetration(DamageClass.Generic) = 12;
			}
			
            base.HoldItem(player);
        }

       
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 
			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.GoldBar, 7)
				.AddIngredient(ItemID.Sapphire, 1)
				.AddIngredient(ItemType<EssenceOfTheDarkMoon>())
				.AddTile(TileID.Anvils)
				.Register();
			CreateRecipe(1)
				.AddIngredient(ItemID.PlatinumBar, 7)
				.AddIngredient(ItemID.Sapphire, 1)
				.AddIngredient(ItemType<EssenceOfTheDarkMoon>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
