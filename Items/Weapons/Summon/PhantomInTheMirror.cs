using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Summon
{
    public class PhantomInTheMirror : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("8 summon tag damage" +
                "\nThis weapon inflicts Frostburn for 2 seconds" +
				"\nRight click to summon the [c/67B6D3:Phantom Dagger] with 100 Mana available; this will not consume Mana" +
				"\nWhen the [c/67B6D3:Phantom Dagger] is present in the world, your Mana will drain and will not regenerate" +
				"\nRight click with the [c/67B6D3:Phantom Dagger] present to swap positions with your [c/67B6D3:Phantom Dagger] (1/2 second cooldown)" +
				"\nWhen you swap with your [c/67B6D3:Phantom Dagger], activate [c/AC5454:Bloodstained Crescent]" +
				"\n[c/AC5454:Bloodstained Crescent] sweeps in a small AoE at both your position and the position of the [c/67B6D3:Phantom Dagger]" +
				"\n[c/AC5454:Bloodstained Crescent] deals 200 extra damage to Frostburned foes while removing the debuff and granting 90 mana" +
				"\n'Yes, lift your voices! Sing for me! Sing for me!!'" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 42;           //The damage of your weapon
			Item.DamageType = DamageClass.Summon;          //Is your weapon a melee weapon?
			Item.width = 74;            //Weapon's texture's width
			Item.height = 74;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 2;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.crit = 3;
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.shoot = ProjectileType<Projectiles.Summon.PhantomInTheMirror.PhantomInTheMirrorProjectile>();
			Item.shootSpeed = 3f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		int teleportCooldown;
		bool powerSwingReady;
		Vector2 vector32;
		int manaDrain;
		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (teleportCooldown <= 0)
				{
					if (player.GetModPlayer<WeaponPlayer>().phantomTeleport == false)
					{
						if (player.statMana > 100)
						{


							player.GetModPlayer<WeaponPlayer>().phantomTeleport = true;
							//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TeleportPrep"));
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
							teleportCooldown = 30;
							vector32.X -= (float)(player.width / 2);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),vector32.X, vector32.Y, 0, 0, ProjectileType<Projectiles.Summon.PhantomInTheMirror.PhantomMarker>(), 0, 0, player.whoAmI, 0f);
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
							player.GetModPlayer<WeaponPlayer>().phantomTeleport = false;
							//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TeleportPrep"));
							player.GetModPlayer<WeaponPlayer>().phantomKill = true;
							player.GetModPlayer<WeaponPlayer>().phantomSavedPosition = new Vector2(player.Center.X, player.Center.Y - 5);
							Vector2 teleportPosition = new Vector2(player.Center.X, player.Center.Y - 5);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, 0, 0, ProjectileType<Projectiles.Summon.PhantomInTheMirror.BloodstainedCrescent>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);


							
							player.Teleport(vector32, 1, 0);
							NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X, player.Center.Y, 0, 0, ProjectileType<Projectiles.Summon.PhantomInTheMirror.BloodstainedCrescent>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);

							teleportCooldown = 30;
							vector32 = teleportPosition;
							player.GetModPlayer<WeaponPlayer>().phantomTeleport = true;
							//player.GetModPlayer<WeaponPlayer>().phantomTeleport = false;


						}
					}
				}
				else
                {
					return false;
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
			teleportCooldown--;
			
			
			/*if (powerSwingReady)
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
			*/
			if(player.GetModPlayer<WeaponPlayer>().phantomTeleport)
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
				//Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 21);
				
			}
			
					//Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 112);
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			
			//player.AddBuff(BuffID.Rage, 240);
			
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{

			}
			else
			{
				return true;
			}
			return false;
		}
		public override void AddRecipes()
		{//Change me
			CreateRecipe(1)
				.AddIngredient(ItemID.HellstoneBar, 6)
				.AddIngredient(ItemID.ThrowingKnife, 1)
				.AddIngredient(ItemID.MimeMask, 1)
				.AddIngredient(ItemType<EssenceOfThePhantom>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
