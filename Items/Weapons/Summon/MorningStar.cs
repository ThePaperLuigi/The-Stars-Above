using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Items.Essences;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using StarsAbove.Buffs.MorningStar;
using StarsAbove.Systems;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Summon.MorningStar;

namespace StarsAbove.Items.Weapons.Summon
{
    public class MorningStar : ModItem
	{
		// The texture doesn't have the same name as the item, so this property points to it.
		//public override string Texture => "ExampleMod/Content/Items/Weapons/ExampleWhip";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Morning Star");
			/* Tooltip.SetDefault("" +
				"Attacks with this weapon whip in a wide arc, exploding foes hit for 1/4 base damage and burning them for 4 seconds" +
				"\nPress the Weapon Action key to summon [c/CBA09A:Alucard's Sword], consuming minion slots (up to 3) to summon a blade to attack foes" +
				"\nAdditional minion slots empower [c/CBA09A:Alucard's Sword], and at 3 slots, the blade hits much faster and has a 10% chance to deal critical strikes" +
				"\n[c/CBA09A:Alucard's Sword] deals 10% more damage from after striking foes with this weapon for 4 seconds" +
				"\n[c/CBA09A:Alucard's Sword] deals 30% more damage instead at 3 minion slots consumed" +
				"\nRight click to consume 20 Mana, casting a random [c/3E7299:Arcane Art] (3 second cooldown)" +
				"\n[c/8EE7E4:Frozen Crystals]: Unleash a spread of icy shards, inflicting Frostburn on foes for 3 seconds" +
				"\n[c/B4E93A:Accursed Flames]: Unleash a burst of flame around you that inflicts Cursed Inferno for 3 seconds" +
				"\n[c/F3E63E:Chain Lightning]: Unleash a concentrated lightning jolt, inflicting Ichor for 3 seconds and chaining to 5 nearby foes" +
				"\n'A vampire, a hunter, and a scholar'" +
				$""); */
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			// Call this method to quickly set some of the properties below.
			//Item.DefaultToWhip(ModContent.ProjectileType<ExampleWhipProjectileAdvanced>(), 20, 2, 4);

			Item.DamageType = DamageClass.SummonMeleeSpeed;
			Item.damage = 25;
			Item.knockBack = 7;
			Item.rare = ItemRarityID.LightRed;
			Item.value = Item.buyPrice(gold: 1);
			Item.shoot = ModContent.ProjectileType<MorningStarWhip>();
			Item.shootSpeed = 4;
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item152;
			Item.channel = false; // This is used for the charging functionality. Remove it if your whip shouldn't be chargeable.
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int randomCast;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override void HoldItem(Player player)
        {
			player.AddBuff(BuffType<MorningStarHeld>(), 10);
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();

			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
			{
				for (int l = 0; l < Main.projectile.Length; l++)
				{
					Projectile proj = Main.projectile[l];
					if (proj.active && (
						proj.type == ProjectileType<AlucardSword1>() ||
						proj.type == ProjectileType<AlucardSword2>() ||
						proj.type == ProjectileType<AlucardSword3>()

						) && proj.owner == player.whoAmI)
					{
						proj.active = false;
					}
				}
				//Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/summoning"));


				
				

				if (player.GetModPlayer<WeaponPlayer>().activeMinions + 3 <= player.maxMinions)
				{
					player.AddBuff(BuffType<Buffs.MorningStar.AlucardSwordBuff3>(), 2);


					int index = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<AlucardSword3>(), player.GetWeaponDamage(player.HeldItem), 4, player.whoAmI, 0f);
					Main.projectile[index].originalDamage = player.GetWeaponDamage(player.HeldItem);

				}
				if (player.GetModPlayer<WeaponPlayer>().activeMinions + 2 == player.maxMinions)
				{
					//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"Tried 2"), 250, 100, 247);}

					player.AddBuff(BuffType<Buffs.MorningStar.AlucardSwordBuff2>(), 2);

					int index1 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<AlucardSword2>(), player.GetWeaponDamage(player.HeldItem), 4, player.whoAmI, 0f);
					Main.projectile[index1].originalDamage = player.GetWeaponDamage(player.HeldItem);

				}
				if (player.GetModPlayer<WeaponPlayer>().activeMinions + 1 == player.maxMinions)
				{
					//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"Tried 1"), 250, 100, 247);}

					player.AddBuff(BuffType<Buffs.MorningStar.AlucardSwordBuff1>(), 2);
					int index2 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<AlucardSword1>(), player.GetWeaponDamage(player.HeldItem), 4, player.whoAmI, 0f);
					Main.projectile[index2].originalDamage = player.GetWeaponDamage(player.HeldItem);

				}

				//int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("AlucardSword").Type, 0, 0, player.whoAmI);
				//Main.projectile[index].originalDamage = 0;
			}
		}

        public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<ArcaneArtCooldown>()) && player.statMana >= 20)
				{
					player.statMana -= 20;
					player.manaRegenDelay = 480;
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
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			randomCast = Main.rand.Next(0, 3);
			if (player.altFunctionUse == 2)
			{

				player.AddBuff(BuffType<ArcaneArtCooldown>(), 180, true);

				if (randomCast == 0)
				{

					float numberProjectiles = 5;
					float rotation = MathHelper.ToRadians(15);
					position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 15f;
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X*5, perturbedSpeed.Y*5, ProjectileType<IcyShards>(), damage/4, knockback, player.whoAmI);
					}
				}
				if (randomCast == 1)
				{


					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center, velocity, ProjectileType<AccursedFlame>(), damage, 0, player.whoAmI);
					
				}
				if (randomCast == 2)
                {
					

					NPC closest = null;
					float closestDistance = 9999999;
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC npc = Main.npc[i];
						float distance = Vector2.Distance(npc.Center, player.Center);


						if (npc.active && npc.Distance(player.position) < closestDistance && !npc.HasBuff(BuffType<MortalWounds>()))
						{
							closest = npc;
							closestDistance = npc.Distance(player.position);
						}




					}

					if (closest.CanBeChasedBy() && closestDistance < 1200f)//If the enemy is a reasonable distance away and is hostile (projectile.ai[0] is the amount of bounces left (Should start at 2, counting initial bounce)
					{

						float rotation = (float)Math.Atan2(position.Y - (Main.MouseWorld.Y), position.X - (Main.MouseWorld.X));//Aim towards mouse
						closest.AddBuff(BuffType<Stun>(), 30);
						float launchSpeed = 7f;
						Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
						Vector2 direction = Vector2.Normalize(closest.Center - player.Center);
						Vector2 velocity1 = direction * launchSpeed;

						Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity1.X, velocity1.Y, ProjectileType<LightningJolt>(), damage, 0f, player.whoAmI, 4);

					}
				}

				return false;
			}
			else
			{
				return true;
			}

			//return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.TungstenBar, 9)
				.AddIngredient(ItemID.Chain, 3)
				.AddIngredient(ItemID.Wood, 4)
				.AddIngredient(ItemType<EssenceOfVampirism>())
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe(1)
				.AddIngredient(ItemID.SilverBar, 9)
				.AddIngredient(ItemID.Chain, 3)
				.AddIngredient(ItemID.Wood, 4)
				.AddIngredient(ItemType<EssenceOfVampirism>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}