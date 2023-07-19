using Microsoft.Xna.Framework;
using StarsAbove.Buffs.DraggedBelow;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles.DraggedBelow;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class DraggedBelow : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 24;          
			Item.DamageType = DamageClass.Magic;          
			Item.width = 40;            
			Item.mana = 2;
			Item.height = 40;        
			Item.useTime = 35;         
			Item.useAnimation = 35;       
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          
			Item.knockBack = 1;       
			Item.value = Item.buyPrice(gold: 1);          
			Item.rare = ItemRarityID.LightRed;           
			Item.UseSound = null;      
			//item.autoReuse = false;         
			Item.channel = true;
			Item.value = Item.buyPrice(gold: 1);          
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.shoot = ProjectileType<DraggedBelowLaser>();
			Item.shootSpeed = 14f;
		}
		 
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2)
            {
				if(player.HasBuff(BuffType<DraggedBelowCooldown>()))
                {
					return false;
                }
            }
			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		public override void HoldItem(Player player)
		{
			var modPlayer = player.GetModPlayer<WeaponPlayer>();
			modPlayer.DraggedBelowHeld = true;

			//Weapon Action Key
			if (Main.myPlayer == player.whoAmI)
			{
				if (StarsAbove.weaponActionKey.JustPressed && !player.HasBuff(BuffType<DraggedBelowSuppressCooldown>()))
				{
					if(player.HasBuff(BuffType<DraggedBelowCorruption>()))
                    {
						if(player.statLife >= 51)
                        {
							player.AddBuff(BuffType<DraggedBelowSuppressCooldown>(), 120);
							player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -95;

							Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
							CombatText.NewText(textPos, new Color(227, 68, 255, 240), $"25", false, false);
							player.GetModPlayer<WeaponPlayer>().gaugeChangeAlpha = 1f;
							SoundEngine.PlaySound(SoundID.Item125, player.Center);
							for (int d = 0; d < 30; d++)
							{
								Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1f);
							}
							player.statLife -= 50;
							player.ManaEffect((int)(player.statManaMax2 * 0.5));
							modPlayer.DraggedBelowCorruption += 25;
							player.statMana += (int)(player.statManaMax2 * 0.5);
							if (player.statMana > player.statManaMax2)
								player.statMana = player.statManaMax2;
						}
						
					}
					else if (modPlayer.DraggedBelowCorruption >= 25)
					{
						player.AddBuff(BuffType<DraggedBelowSuppressCooldown>(), 120);
						player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -95;
						SoundEngine.PlaySound(SoundID.Item125, player.Center);

						modPlayer.DraggedBelowCorruption -= 25;
						if(player.HasBuff(BuffType<DraggedBelowHealCooldown>()))
                        {
							player.Heal(5);

						}
						else
                        {
							player.Heal(25);

						}
						for (int d = 0; d < 30; d++)
						{
							Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1f);
						}
						player.AddBuff(BuffType<DraggedBelowHealCooldown>(), 60 * 5);
						player.ManaEffect((int)(player.statManaMax2 * 0.25));
						player.statMana += (int)(player.statManaMax2 * 0.25);
						if (player.statMana > player.statManaMax2)
							player.statMana = player.statManaMax2;
					}
				}
			}
			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if(player.HasBuff(BuffType<DraggedBelowCorruption>()))
            {
				if (player.altFunctionUse == 2)
				{

					player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -95;
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<DraggedBelowFloodCorrupted>(), damage*10, 0f, player.whoAmI);
					player.AddBuff(BuffType<DraggedBelowCooldown>(), 60 * 3);
					player.velocity -= velocity / 2;

					SoundEngine.PlaySound(SoundID.Item124, player.Center);

					return false;
				}
				else
				{
					SoundEngine.PlaySound(SoundID.Item15, player.Center);

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<DraggedBelowLaserCorrupt>(), damage, 0f, player.whoAmI);
					
					return false;
				}
			}
			else
            {
				if (player.altFunctionUse == 2)
				{
					player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -95;
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<DraggedBelowFlood>(), damage*4, 0f, player.whoAmI);
					player.AddBuff(BuffType<DraggedBelowCooldown>(), 60 * 8);
					SoundEngine.PlaySound(SoundID.Item124, player.Center);
					player.velocity -= velocity / 2;
					return false;
				}
				else
				{
					SoundEngine.PlaySound(SoundID.Item12, player.Center);

					player.AddBuff(BuffType<DraggedBelowDrownActive>(), 4);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<DraggedBelowLaser>(), damage, 0f, player.whoAmI);
					Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
					CombatText.NewText(textPos, new Color(227, 68, 255, 240), $"1", false, false);
					player.GetModPlayer<WeaponPlayer>().DraggedBelowCorruption++;
					player.GetModPlayer<WeaponPlayer>().gaugeChangeAlpha = 1f;
					return false;
				}
			}
			

            
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.SoulofNight, 5)
				.AddIngredient(ItemID.CursedFlame, 8)
				.AddIngredient(ItemID.ShadowScale, 5)
				.AddIngredient(ItemID.PotionOfReturn)
				.AddIngredient(ItemType<EssenceOfTheVoid>())
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe(1)
				.AddIngredient(ItemID.SoulofNight, 5)
				.AddIngredient(ItemID.Ichor, 8)
				.AddIngredient(ItemID.TissueSample, 5)
				.AddIngredient(ItemID.PotionOfReturn)
				.AddIngredient(ItemType<EssenceOfTheVoid>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
