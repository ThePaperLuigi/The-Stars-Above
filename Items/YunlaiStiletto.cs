using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Items
{
    public class YunlaiStiletto : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Striking foes will grant Rage" +
				"\nRight click to expend 80 mana, placing a [c/CB8EE3:Lightning Stiletto] at your cursor's location" +
				"\nOnce a [c/CB8EE3:Lightning Stiletto] is present, using right click again will teleport you to the position of the [c/CB8EE3:Lightning Stiletto]" +
				"\nAdditionally, your next attack will become [c/B703FF:Driving Thunder]" +
				"\n[c/B703FF:Driving Thunder] will sweep in a wide arc, dealing high amounts of damage" +
				"\nIf [c/B703FF:Driving Thunder] defeats an opponent, the mana used for placing the [c/CB8EE3:Lightning Stiletto] will be refunded" +
				"\nIf you attempt to teleport to an unaccessable location, you will consume the [c/CB8EE3:Lightning Stiletto] without teleporting but still prepare [c/B703FF:Driving Thunder]" +
				"\n'Woven from lightning itself, from a land where gods roam free'" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 99;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using

			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 2;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.crit = 10;
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton

			Item.shoot = ProjectileType<Projectiles.yunlaiSwing>();
			Item.shootSpeed = 10f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		bool powerSwingReady;
		Vector2 vector32;
		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<WeaponPlayer>().yunlaiTeleport == false)
				{
					if (player.statMana >= 80)
					{
						player.statMana -= 80;
						player.manaRegenDelay = 220;
						player.GetModPlayer<WeaponPlayer>().yunlaiTeleport = true;
						SoundEngine.PlaySound(StarsAboveAudio.SFX_TeleportPrep, player.Center);
						Vector2 teleportPosition = Main.MouseWorld;
						
						vector32.X = (float)Main.mouseX + Main.screenPosition.X;
						if (player.gravDir == 1f)
						{
							vector32.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
						}
						else
						{
							vector32.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
						}
						vector32.X -= (float)(player.width / 2);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),vector32.X,vector32.Y, 0, 0, ProjectileType<Projectiles.StilettoMarker>(), 0, 0, player.whoAmI, 0f);
					}
					else
					{
						return false;
					}
				}
				else
				{
					if (Main.myPlayer == player.whoAmI)//weaponout code
					{
						
							if (!Collision.SolidCollision(vector32, player.width, player.height))
							{
								player.Teleport(vector32, 1, 0);
								NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float)player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);

							}
						player.GetModPlayer<WeaponPlayer>().yunlaiTeleport = false;
						powerSwingReady = true;
						
					}
				}
			}
			else
			{
				if (powerSwingReady)
				{
					
				}
					
				
			}
			return base.CanUseItem(player);
		}


		internal static void Teleport(Player player)
		{
			
		}
		public override void HoldItem(Player player)
		{
			if (powerSwingReady)
			{
				
				
					
					Vector2 vector = new Vector2(
						Main.rand.Next(-48, 48) * (0.003f * 40 - 10),
						Main.rand.Next(-48, 48) * (0.003f * 40 - 10));
					Dust d = Main.dust[Dust.NewDust(
						player.MountedCenter + vector, 1, 1,
						21, 0, 0, 255,
						new Color(0.8f, 0.4f, 1f), 1.5f)];
					d.velocity = -vector / 16;
					d.velocity -= player.velocity / 8;
					d.noLight = true;
					d.noGravity = true;

				
			}
			else
			{
				
			}

			if(player.GetModPlayer<WeaponPlayer>().yunlaiTeleport)
			{
				
				for (int i = 0; i < 50; i++)
				{
					Vector2 position = Vector2.Lerp(player.Center, vector32, (float)i / 50);
					Dust d = Dust.NewDustPerfect(position, 20, null, 240, default(Color), 0.3f);
					d.fadeIn = 0.3f;
					d.noLight = true;
					d.noGravity = true;
				}
			}
			base.HoldItem(player);
		}

		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 21);
				
			}
			
					Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 112);
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			
			player.AddBuff(BuffID.Rage, 240);
			
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{

			}
			else
			{
				if (powerSwingReady)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,type, damage * 3, knockback * 2, player.whoAmI, 0f);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_TeleportFinisher, player.Center);
					powerSwingReady = false;
				}
			}
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.FragmentNebula, 12)
				.AddIngredient(ItemID.FragmentStardust, 3)
				.AddIngredient(ItemType<EssenceOfDrivingThunder>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
