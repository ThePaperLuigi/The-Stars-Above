using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using StarsAbove.Projectiles.Melee.MiserysCompany;
using StarsAbove.Projectiles.Melee.KarlanTruesilver;
using StarsAbove.Utilities;

namespace StarsAbove.Items.Weapons.Melee
{
    public class MiserysCompany : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Misery's Company");
			/* Tooltip.SetDefault("Right click to cycle between [c/CE4A3F:Blade Mode], [c/3FCCCE:Scythe Mode], and [c/CE3FAD:Shotgun Mode] with a 2 second cooldown" +
				"\n[c/CE4A3F:Blade Mode] allows for quick swings at close range" +
				"\n[c/3FCCCE:Scythe Mode] throws a scythe with very long reach that can bypass walls with a cooldown" +
				"\n[c/CE3FAD:Shotgun Mode] fires bullets in a spread pattern that does not use ammo, doing half damage per shot" +
				$""); */

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 25;          
			Item.DamageType = DamageClass.Melee;          
			Item.width = 108;          
			Item.height = 118;         
			Item.useTime = 30;          
			Item.useAnimation = 30;        
			Item.useStyle = 1;         
			Item.knockBack = 5;        
			Item.value = Item.buyPrice(gold: 1);          
			Item.rare = ItemRarityID.Orange;             
														 
			Item.autoReuse = true;
			Item.shoot = ProjectileType<TruesilverSlash>();
			Item.shootSpeed = 10f;
			Item.value = Item.buyPrice(gold: 1);

			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
		}
		int mode;//0 = sword | 1 = scythe | 2 = shotgun
		int swapCooldown;
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2 )//Did the player right click?
			{
				if (mode == 0 && swapCooldown <= 0)
				{
					SoundEngine.PlaySound(SoundID.Item23, player.position);
					swapCooldown = 120;
					mode = 1;
					if (player.whoAmI == Main.myPlayer)
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(0, 125, 250, 240), LangHelper.GetTextValue($"CombatText.MiserysCompany.Scythe"), false, false);
					}
				}
				if (mode == 1 && swapCooldown <= 0)
				{
					SoundEngine.PlaySound(SoundID.Item23, player.position);
					swapCooldown = 120;
					mode = 2;
					if (player.whoAmI == Main.myPlayer)
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(0, 125, 250, 240), LangHelper.GetTextValue($"CombatText.MiserysCompany.Shotgun"), false, false);
					}
				}
				if (mode == 2 && swapCooldown <= 0)
				{
					SoundEngine.PlaySound(SoundID.Item23, player.position);
					swapCooldown = 120;
					mode = 0;
					if (player.whoAmI == Main.myPlayer)
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(0, 125, 250, 240), LangHelper.GetTextValue($"CombatText.MiserysCompany.Blade"), false, false);
					}
				}
				return false;
			}
			return base.CanUseItem(player);
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
        public override bool? UseItem(Player player)
        {
			



				return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			swapCooldown--;
			if(mode == 0)
            {
				Item.useStyle = ItemUseStyleID.HiddenAnimation;
				Item.shootSpeed = 10f;
				Item.useTime = 20;
				Item.useAnimation = 20;

			}
			if (mode == 1)
			{
				Item.useStyle = ItemUseStyleID.Swing;
				Item.shootSpeed = 8f;
				Item.useTime = 85;
				Item.useAnimation = 85;

			}
			if (mode == 2)
			{
				Item.useStyle = ItemUseStyleID.Shoot;
				Item.shootSpeed = 15f;
				Item.useTime = 30;
				Item.useAnimation = 30;

			}

			base.HoldItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{

		}
		bool altSwing;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				//Don't use the weapon if the player is swapping forms.
			}
			else
            {
				if (mode == 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<MiseryScythe>(), damage, knockback, player.whoAmI);
					SoundEngine.PlaySound(SoundID.Item1, player.position);
				}
				if (mode == 2)
				{
					Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
					if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
					{
						position += muzzleOffset;
					}
					//Gun shoot graphic
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, 0, 0,ProjectileType<MiseryShotgun>(), 0, 0, player.whoAmI);
					int numberProjectiles = 3 + Main.rand.Next(4); //random shots
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
																														
																														float scale = 1f - (Main.rand.NextFloat() * .3f);
																														perturbedSpeed = perturbedSpeed * scale; 
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<MiseryRound>(), damage/2, knockback, player.whoAmI);
					}
					
					SoundEngine.PlaySound(SoundID.Item11, player.position);
				}
			}
			if(mode == 0)
			{
				if(altSwing)
				{
					Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<MiserySword>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
					altSwing = false;
                }
				else
				{
					Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<MiserySword>(), damage, knockback, player.whoAmI, 0, 1, player.direction);
					altSwing = true;
                }
				SoundEngine.PlaySound(SoundID.Item1, player.position);
			}
			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<EssenceOfMisery>())
				.AddIngredient(ItemID.Diamond, 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
