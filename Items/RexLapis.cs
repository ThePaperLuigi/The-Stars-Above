using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using StarsAbove.Projectiles.RexLapis;
using StarsAbove.Buffs.RexLapis;

namespace StarsAbove.Items
{
    public class RexLapis : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Attacks with this weapon execute rapid stabs" +
				"\nEach attack grants [c/F8D496:Bulwark of Jade] for 2 seconds on hit, increasing defenses by 30" +
				"\nRight click to consume 150 mana, dropping a colossal earthen meteor from the heavens, dealing 2x base damage (20 second cooldown)" +
                "\nAdditionally, this attack will deal bonus damage based on 50% of your Max HP" +
				"\nThis meteor will apply [c/CB8952:Petrification] to foes struck on a critical strike, drastically crippling their movement speed for 3 seconds" +
				"\nAttacking [c/CB8952:Petrified] foes with this weapon will deal extra damage" +
				"\nCritical strikes will [c/F1D078:Shatter] foes that are [c/CB8952:Petrified]" +
				"\n[c/F1D078:Shatter] deals 5x base damage while removing [c/CB8952:Petrification]" +
				"\n'I will have order!'" +
				$"");  //The (English) text shown below your weapon's name
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 199;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.crit = 30;
			Item.width = 80;            //Weapon's texture's width
			Item.height = 80;           //Weapon's texture's height
			Item.useTime = 14;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 14;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 3;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<RexLapisAttack>();
			Item.shootSpeed = 10f;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.UseSound = SoundID.Item1;
		}
		 
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		int throwPrep = -1;
		public override bool CanUseItem(Player player)
		{
			
			if (player.GetModPlayer<StarsAbovePlayer>().comboCooldown > 0)
			{
				return false;
			}
			if(player.altFunctionUse == 2)
            {
				if(player.statMana >= 150 && !player.HasBuff(BuffType<RexLapisMeteorCooldown>()))
                {
					player.statMana -= 150;
					player.manaRegenDelay = 240;
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

		public override bool? UseItem(Player player)
		{
			

			
			
			return true;
		}

	
		public override void HoldItem(Player player)
		{
			
			
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 
			if (player.altFunctionUse == 2)
			{

				SoundEngine.PlaySound(StarsAboveAudio.SFX_spinConstant, player.Center);
				player.AddBuff(BuffType<RexLapisMeteorCooldown>(), 60 * 20);
				
					SoundEngine.PlaySound(StarsAboveAudio.SFX_swordStab, player.Center);//DROP
					Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
					float ceilingLimit = target.Y;
					if (ceilingLimit > player.Center.Y - 200f)
					{
						ceilingLimit = player.Center.Y - 200f;
					}
					for (int i = 0; i < 1; i++)
					{
						position = player.Center + new Vector2((-(float)0 * player.direction), -600f);
						position.Y -= (100 * i);
						Vector2 heading = target - position;
						if (heading.Y < 0f)
						{
							heading.Y *= -1f;
						}
						if (heading.Y < 20f)
						{
							heading.Y = 20f;
						}
						heading.Normalize();
						heading *= new Vector2(velocity.X, velocity.Y).Length();
						velocity.X = heading.X;
						velocity.Y = heading.Y;
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<RexLapisMeteor2>(), damage*2 + player.statLifeMax/2, knockback, player.whoAmI, 0f, ceilingLimit);
					}
				
			}
			else
            {
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(15));
				velocity.X = perturbedSpeed.X;
				velocity.Y = perturbedSpeed.Y;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<RexLapisAttack>(), damage, knockback, player.whoAmI);
				return false;
			}
			

			
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.FragmentSolar, 12)
				.AddIngredient(ItemID.FragmentVortex, 3)
				.AddIngredient(ItemType<EssenceOfTheUnyieldingEarth>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
