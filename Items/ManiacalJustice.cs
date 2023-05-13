using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using StarsAbove.Projectiles.ManiacalJustice;
using StarsAbove.Utilities;
using StarsAbove.Buffs.ManiacalJustice;

namespace StarsAbove.Items
{
    public class ManiacalJustice : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("" +
				"Attacks with this weapon strikes foes multiple times with a close-ranged slash" +
				"\nStriking foes grant 1 [c/FC578E:LV] (Critical strikes grant 5 [c/FC578E:LV] instead)" +
				"\nRight click to unleash a [c/F4296D:SLAUGHTERING SLASH] at your cursor after a short delay, dealing 3x damage in a vertical slash (6 second cooldown)" +
				"\nAdditionally, the [c/F4296D:SLAUGHTERING SLASH] grants 2 [c/FC578E:LV] per hit" +
                "\n[c/FC578E:LV] caps at 100 and will deplete quickly out of combat" +
                "\nTaking damage resets [c/FC578E:LV] but reduces damage taken based on amount of [c/FC578E:LV]" +
				"\nAt 20 [c/FC578E:LV], attack speed is increased by 20% and the cooldown of [c/F4296D:SLAUGHTERING SLASH] is halved" +
				"\nAt 50 [c/FC578E:LV], attack speed is increased by an additional 10% and all attacks will now inflict [c/DA11B5:Karmic Retribution] for 10 seconds, dealing damage over time" +
				"\nAt 100 [c/FC578E:LV], all bonuses are removed, but pressing the Weapon Action Key will unleash a [c/C91515:SPECIAL ATTACK] for 15 seconds, consuming all [c/FC578E:LV]" +
				"\nDuring the [c/C91515:SPECIAL ATTACK] attack speed and damage dealt is doubled but damage taken is tripled" +
                "\nAdditionally, gain infinite flight time and a 25% chance to dodge any attack" +
				"\nOnce the [c/C91515:SPECIAL ATTACK] concludes, this weapon can not be used for 30 seconds" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 55;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;         //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 30;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 30;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Lime;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 0f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int currentSwing;
		int slashDuration;
		bool stackReminder20 = false;
		bool stackReminder50 = false;
		bool stackReminder100 = false;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.HasBuff(BuffType<ManiacalJusticeCooldown>()))
            {
				return false;
            }
			if (player.altFunctionUse == 2)
			{
				if (player.HasBuff(BuffType<SlaughteringSlashCooldown>()))
                {
					return false;
				}
				else
                {
					return true;
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
			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<WeaponPlayer>().LVStacks >= 20 && player.GetModPlayer<WeaponPlayer>().LVStacks < 100)
				{
					player.AddBuff(BuffType<SlaughteringSlashCooldown>(), 60 * 3);
				}
				else
				{
					player.AddBuff(BuffType<SlaughteringSlashCooldown>(), 60 * 6);

				}
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<ManiacalSlash>() , player.GetWeaponDamage(Item) * 3, 0, player.whoAmI, 0f);
				SoundEngine.PlaySound(StarsAboveAudio.SFX_ManiacalSlashWarning, Main.MouseWorld);

			}
			else
			{

				float launchSpeed3 = 70f;
				Vector2 mousePosition = Main.MouseWorld;
				Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
				Vector2 arrowVelocity3 = direction * launchSpeed3;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_ManiacalSlash, player.position);

				if (player.HasBuff(BuffType<SpecialAttackBuff>()))
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<ManiacalSwing2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

				}
				else
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<ManiacalSwing1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
				}

			}
			

			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			player.AddBuff(BuffType<LVBuff>(), 10);
			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
			{
				if(!player.HasBuff(BuffType<SpecialAttackBuff>()))
                {
					if (player.GetModPlayer<WeaponPlayer>().LVStacks >= 100)
					{
						SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, Main.MouseWorld);

						player.GetModPlayer<WeaponPlayer>().LVStacks = 0;
						player.AddBuff(BuffType<SpecialAttackBuff>(), 60 * 15);
						for (int d = 0; d < 38; d++)
						{
							Dust.NewDust(player.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-24, 24), 0f + Main.rand.Next(-24, 24), 150, default(Color), 1.2f);
						}
						for (int d = 0; d < 32; d++)
						{
							Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-17, 17), 0f + Main.rand.Next(-17, 17), 150, default(Color), 1.4f);
						}
						player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
					}
					else
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(255, 108, 108, 240), LangHelper.GetTextValue($"CombatText.ManiacalJustice.NotEnoughStacks", player.GetModPlayer<WeaponPlayer>().LVStacks), false, false);
					}
				}
				
			}
			if (player.GetModPlayer<WeaponPlayer>().LVStacks >= 20 && !stackReminder20)
            {
				Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
				CombatText.NewText(textPos, new Color(255, 108, 108, 240), LangHelper.GetTextValue($"CombatText.ManiacalJustice.Stack20"), false, false);
				stackReminder20 = true;
            }
			if(player.GetModPlayer<WeaponPlayer>().LVStacks < 20)
            {
				
				stackReminder20 = false;
            }
			if (player.GetModPlayer<WeaponPlayer>().LVStacks >= 50 && !stackReminder50)
			{
				Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
				CombatText.NewText(textPos, new Color(255, 45, 45, 240), LangHelper.GetTextValue($"CombatText.ManiacalJustice.Stack50"), false, false);
				stackReminder50 = true;
			}
			if (player.GetModPlayer<WeaponPlayer>().LVStacks < 50)
			{
				stackReminder50 = false;
			}
			if (player.GetModPlayer<WeaponPlayer>().LVStacks >= 100 && !stackReminder100)
			{
				Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
				CombatText.NewText(textPos, new Color(220, 0, 0, 240), LangHelper.GetTextValue($"CombatText.ManiacalJustice.Stack100"), false, false);
				stackReminder100 = true;
			}
			if (player.GetModPlayer<WeaponPlayer>().LVStacks < 100)
			{
				stackReminder100 = false;
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
				.AddIngredient(ItemID.PsychoKnife, 1)
				.AddIngredient(ItemID.Bone, 12)
				.AddIngredient(ItemID.Skull, 1)
				.AddIngredient(ItemID.BoneSword, 1)
				.AddIngredient(ItemType<EssenceOfMania>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
