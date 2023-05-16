using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    public class CrimsonSakuraAlpha : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("Striking foes cause [c/B08FFC:Skill Orbs] to drop" +
				"\nRight click after picking up a [c/B08FFC:Skill Orb] to unleash a special ability" +
				"\n[c/B08FFC:Skill Orbs] last for 20 seconds after picking them up and do not stack" +
				"\nPick up a [c/FC8F8F:Red Orb] to unleash [c/FC8F8F:Flickering Strike]: Slash the area around you in a wide attack for double damage" +
				"\nPick up a [c/8FBFFC:Blue Orb] to unleash [c/8FBFFC:Lightbreak Strike]: All melee attacks become critical strikes for 2 seconds" +
				"\nPick up a [c/FCF08F:Yellow Orb] to unleash [c/FCF08F:Dazzling Strike]: Pierce foes with a ranged burst" +
				"\nRight clicking with multiple orbs will use the orbs in the order you acquired them" +
				"\nAfter using all 3 orbs in quick succession, right click to enter [c/F04040:Blade Will]" +
				"\n[c/F04040:Blade Will] drains mana constantly, and has a 60 second cooldown upon conclusion" +
				"\nDuring [c/F04040:Blade Will], attack much faster and fire projectiles when you attack" +
				"\n[c/F04040:Blade Will] ends once Mana reaches 0, and Mana can not be regenerated" +
				"\nThe longer you stay in [c/F04040:Blade Will], the more Mana upkeep is required" +
				"\nInstead of [c/B08FFC:Skill Orbs], attacking foes will drop [c/A7A7A7:Blade Orbs] which prolong [c/F04040:Blade Will] through granting Mana" +
				"\n[c/F04040:Blade Will]'s projectiles will not spawn [c/A7A7A7:Blade Orbs]" +
				"\nYou are unable to use special abilities while under [c/F04040:Blade Will]" +
				"\n'This is the final journey'" +
				$""); */  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 120;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 15;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 15;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using

			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.crit = 10;
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.shoot = ProjectileType<Projectiles.bladeWillAttack>();
			Item.shootSpeed = 10f;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		bool bladeWill;
		Vector2 vector32;
		int bladeWillTriggerRed;
		int bladeWillTriggerBlue;
		int bladeWillTriggerYellow;
		int bladeWillTimer;
		public override bool CanUseItem(Player player)
		{
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * 9f;

			
			if (player.altFunctionUse == 2)
			{
				if(bladeWillTriggerBlue > 0 && bladeWillTriggerRed > 0 && bladeWillTriggerYellow > 0 && !player.GetModPlayer<WeaponPlayer>().Player.GetModPlayer<WeaponPlayer>().bladeWill && !player.HasBuff(BuffType<Buffs.BladeWillCooldown>()))
                {
					Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
					CombatText.NewText(textPos, new Color(255, 0, 125, 240), "Blade Will activated!", false, false);
					player.GetModPlayer<WeaponPlayer>().bladeWill = true;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, player.Center);
					return true;
					

				}
				if (!player.GetModPlayer<WeaponPlayer>().bladeWill)
				for (int i = 0; i < player.CountBuffs(); i++)
				{
					if (player.buffType[i] == BuffType<Buffs.YellowOrb>())
					{
							SoundEngine.PlaySound(StarsAboveAudio.SFX_HolyStab, player.Center);

							bladeWillTriggerYellow = 300;
						arrowVelocity = direction * 10f;
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<Firebolt>(), 130, 4, player.whoAmI, 0f);
						for (int d = 0; d < 20; d++)
						{
							Vector2 position = Main.LocalPlayer.position;
							int playerWidth = Main.LocalPlayer.width;
							int playerHeight = Main.LocalPlayer.height;
							Dust dust;
							dust = Main.dust[Terraria.Dust.NewDust(position, playerWidth, playerHeight, 87, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, new Color(255, 255, 255), 1f)];
						}
						player.DelBuff(i);
							return true;

					}
					if (player.buffType[i] == BuffType<Buffs.BlueOrb>())
					{
							SoundEngine.PlaySound(StarsAboveAudio.SFX_HolyStab, player.Center);

							bladeWillTriggerBlue = 300;
						player.AddBuff(BuffType<Buffs.LightbreakStrike>(), 120);

						player.DelBuff(i);
						for (int d = 0; d < 20; d++)
						{
							Vector2 position = Main.LocalPlayer.position;
							int playerWidth = Main.LocalPlayer.width;
							int playerHeight = Main.LocalPlayer.height;
							Dust dust;
							dust = Main.dust[Terraria.Dust.NewDust(position, playerWidth, playerHeight, 88, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, new Color(255, 255, 255), 1f)];
						}
							return true;

					}
					if (player.buffType[i] == BuffType<Buffs.RedOrb>())
					{
							SoundEngine.PlaySound(StarsAboveAudio.SFX_HolyStab, player.Center);

							bladeWillTriggerRed = 300;
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<sakuraSwing>(), 260, 4, player.whoAmI, 0f);

						player.DelBuff(i);
						for (int d = 0; d < 20; d++)
						{
							Vector2 position = Main.LocalPlayer.position;
							int playerWidth = Main.LocalPlayer.width;
							int playerHeight = Main.LocalPlayer.height;
							Dust dust;
							dust = Main.dust[Terraria.Dust.NewDust(position, playerWidth, playerHeight, 90, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, new Color(255, 255, 255), 1f)];
						}
							return true;

					}


				}

			}
			else
			{



			}
			return base.CanUseItem(player);
		}



		public override void HoldItem(Player player)
		{
			player.GetModPlayer<WeaponPlayer>().sakuraHeld = true;
			bladeWillTriggerRed--;
			bladeWillTriggerBlue--;
			bladeWillTriggerYellow--;
			if(player.statMana <= 0)
            {
				Main.LocalPlayer.AddBuff(BuffType<Buffs.BladeWillCooldown>(), 3600);
				player.GetModPlayer<WeaponPlayer>().bladeWill = false;
            }
			else
            {
				
            }
			if (player.GetModPlayer<WeaponPlayer>().bladeWill)
			{
				bladeWillTimer++;
				
				player.statMana -= 0 + (bladeWillTimer/240);
				player.manaRegenDelay = 30;
				Item.useTime = 9;
				Item.useAnimation = 9;

				Vector2 vector = new Vector2(
					Main.rand.Next(-48, 48) * (0.003f * 40 - 10),
					Main.rand.Next(-48, 48) * (0.003f * 40 - 10));
				Dust d = Main.dust[Dust.NewDust(
					player.MountedCenter + vector, 1, 1,
					115, 0, 0, 255,
					new Color(0.8f, 0.4f, 1f), 1.5f)];
				d.velocity = -vector / 16;
				d.velocity -= player.velocity / 8;
				d.noLight = true;
				d.noGravity = true;


			}
			else
			{
				bladeWillTimer = 0;
				Item.useTime = 15;  
				Item.useAnimation = 15;
			}


			base.HoldItem(player);
		}


		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 115);

			}

			Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 123);

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
				if (player.GetModPlayer<WeaponPlayer>().bladeWill)
				{
					Item.UseSound = StarsAboveAudio.SFX_YunlaiSwing1;
					return true;
				}
			}
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 12)
				.AddIngredient(ItemID.ShroomiteBar, 12)
				.AddIngredient(ItemType<EssenceOfAlpha>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
