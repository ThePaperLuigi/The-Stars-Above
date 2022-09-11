
using Microsoft.Xna.Framework;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using StarsAbove.Projectiles;
using System;
using StarsAbove.Items.Essences;
using Terraria.Localization;
using StarsAbove.Projectiles.Kifrosse;

namespace StarsAbove.Items
{
	public class Kifrosse : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Kifrosse");
			Tooltip.SetDefault("" +
				"" +
				"Upon use, consumes all remaining Minion Slots (up to 9) to conjure a [c/7AE1CE:Foxfrost Mystic], gaining 5% increased Summon Damage per slot" +
				"\nWhile the [c/7AE1CE:Foxfrost Mystic] is stationary, a protective aura will grant [c/B8F7FC:Stalwart Snow] to nearby allies in range, granting 16 Defense" +
				"\nWhen in motion, the [c/7AE1CE:Foxfrost Mystic] will shapeshift, granting Swiftness to the summoner" +
				"\nLess than 3 slots: The [c/7AE1CE:Foxfrost Mystic] will summon a [c/129BE4:Dancing Foxfire], periodically attacking nearby foes and inflicting Frostburn for 2 seconds" +
				"\n3 slots: Two [c/129BE4:Dancing Foxfires] are summoned instead of one" +
				"\nThe [c/7AE1CE:Foxfrost Mystic] will periodically apply [c/88CEFF:Amaterasu's Grace] to the summoner for 3 seconds, increasing damage to Frostburned foes by 50%" +
				"\n6 slots: Both [c/129BE4:Dancing Foxfires] are replaced with [c/12DAE4:Blizzard Foxfires], which attack faster and pierce foes" +
				"\nFrostburn is applied for 12 seconds instead of 2" +
				"\n[c/88CEFF:Amaterasu's Grace] becomes permanent" +
				"\n9 slots: Three [c/12DAE4:Blizzard Foxfires] are summoned instead of two" +
				"\nAllies within the protective aura additionally gain the effects of [c/88CEFF:Amaterasu's Grace]" +
				"\nGain the ability to cast [c/0464A7:Amaterasu's Winter] when double-tapping DOWN" +
				"\n[c/0464A7:Amaterasu's Winter] will cause all [c/12DAE4:Blizzard Foxfires] to attack incredibly fast" +
				"\n[c/0464A7:Amaterasu's Winter] lasts for 4 seconds and has a 40 second cooldown" +
				$"");

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


		}

		public override void SetDefaults()
		{
			Item.damage = 46;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 60;
			Item.width = 21;
			Item.height = 21;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = 1;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.knockBack = 6;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = StarsAboveAudio.SFX_summoning;
			Item.shoot = ProjectileType<Kifrosse1>();
			Item.buffType = BuffType<Buffs.Kifrosse.KifrosseBuff1>(); //The buff added to player after used the item
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

        public override void HoldItem(Player player)
        {
			

			base.HoldItem(player);
        }
        public override bool CanUseItem(Player player)
        {
			

			return base.CanUseItem(player);
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
		

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {

			//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{player.GetModPlayer<StarsAbovePlayer>().activeMinions} minions out of a max of {player.maxMinions}"), 250, 100, 247);}
			for (int l = 0; l < Main.projectile.Length; l++)
			{
				Projectile proj = Main.projectile[l];
				if (proj.active && (
					proj.type == ProjectileType<Kifrosse1>()|| //Despawns any active Foxfrost Mystics
					proj.type == ProjectileType<Kifrosse2>()|| 
					proj.type == ProjectileType<Kifrosse3>()||
					proj.type == ProjectileType<Kifrosse4>()|| 
					proj.type == ProjectileType<Kifrosse5>()||
					proj.type == ProjectileType<Kifrosse6>()||
					proj.type == ProjectileType<Kifrosse7>()||
					proj.type == ProjectileType<Kifrosse8>()||
					proj.type == ProjectileType<Kifrosse9>()
					) && proj.owner == player.whoAmI)
				{
					proj.active = false;
				}
			}
			//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/summoning"));

			
			position = Main.MouseWorld;

			if (player.GetModPlayer<StarsAbovePlayer>().activeMinions + 9 <= player.maxMinions)
			{
				//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"Tried 9"), 250, 100, 247);}

				player.AddBuff(BuffType<Buffs.Kifrosse.KifrosseBuff9>(), 2);
				
				type = ProjectileType<Kifrosse9>();
				int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = damage;
				return false;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().activeMinions + 8 == player.maxMinions)
			{
				player.AddBuff(BuffType<Buffs.Kifrosse.KifrosseBuff8>(), 2);
				
				type = ProjectileType<Kifrosse8>();
				int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = damage;
				return false;
			}
			if(player.GetModPlayer<StarsAbovePlayer>().activeMinions + 7 == player.maxMinions)
			{
				player.AddBuff(BuffType<Buffs.Kifrosse.KifrosseBuff7>(), 2);
				
				type = ProjectileType<Kifrosse7>();
				int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = damage;
				return false;
			}
			if(player.GetModPlayer<StarsAbovePlayer>().activeMinions + 6 == player.maxMinions)
			{
				player.AddBuff(BuffType<Buffs.Kifrosse.KifrosseBuff6>(), 2);
				
				type = ProjectileType<Kifrosse6>();
				int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = damage;
				return false;
			}
			if(player.GetModPlayer<StarsAbovePlayer>().activeMinions + 5 == player.maxMinions)
			{
				player.AddBuff(BuffType<Buffs.Kifrosse.KifrosseBuff5>(), 2);
				
				type = ProjectileType<Kifrosse5>();
				int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = damage;
				return false;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().activeMinions + 4 == player.maxMinions)
			{
				player.AddBuff(BuffType<Buffs.Kifrosse.KifrosseBuff4>(), 2);
				
				type = ProjectileType<Kifrosse4>();
				int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = damage;
				return false;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().activeMinions + 3 == player.maxMinions)
			{
				player.AddBuff(BuffType<Buffs.Kifrosse.KifrosseBuff3>(), 2);
				
				type = ProjectileType<Kifrosse3>();
				int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = damage;
				return false;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().activeMinions + 2 == player.maxMinions)
			{
				//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"Tried 2"), 250, 100, 247);}

				player.AddBuff(BuffType<Buffs.Kifrosse.KifrosseBuff2>(), 2);
				
				type = ProjectileType<Kifrosse2>();
				int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = damage;
				return false;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().activeMinions + 1 == player.maxMinions)
			{
				//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"Tried 1"), 250, 100, 247);}

				player.AddBuff(BuffType<Buffs.Kifrosse.KifrosseBuff1>(), 2);
				type = ProjectileType<Kifrosse1>();
				int index = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = damage;
				return false;
			}
			return false;



		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.SnowBlock, 18)
				.AddIngredient(ItemID.LivingFireBlock, 7)
				.AddIngredient(ItemID.Amarok, 1)
				.AddIngredient(ItemType<EssenceOfFoxfire>())
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}
