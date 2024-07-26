using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using StarsAbove.Utilities;
using StarsAbove.Projectiles.Magic.SanguineDespair;
using StarsAbove.Buffs.Magic.SanguineDespair;

namespace StarsAbove.Items.Weapons.Magic
{
    public class SanguineDespair : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sanguine Despair");

			/* Tooltip.SetDefault("Attacks with this weapon consume 10 health per cast and follow your cursor, piercing up to three times (Health drain can kill)" +
				"\nHolding this weapon grants the buff [c/EA6B69:Feral Despair], which increases damage by 0.5% per missing HP" +
				"\nRight click to unleash [c/DA0300:Surging Vampirism], an erratic bolt of energy that deals damage in a massive explosion upon striking an enemy (30 second cooldown)" +
				"\n[c/DA0300:Surging Vampirism] deals bonus damage based on missing HP (capping at 400%)" +
				"\nAdditionally, [c/DA0300:Surging Vampirism] heals based on 30% of damage dealt to the first foe struck" +
				"\nThe explosion of [c/DA0300:Surging Vampirism] heals an additional 5% of the damage dealt while inflicting Mortal Wounds for 8 seconds, dealing damage over time" +
                "\nAttacks against a foe with Mortal Wounds deal 10% increased damage" +
				"\nCritical strikes against a foe with Mortal Wounds deal 30% increased damage instead" +
				"" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 66;           //The damage of your weapon
			Item.mana = 25;
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 176;            //Weapon's texture's width
			Item.height = 30;           //Weapon's texture's height
			Item.useTime = 30;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 30;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.LightRed;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item43;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<SanguineDespairBolt>();
			Item.shootSpeed = 20f;
			Item.noMelee = true;
			Item.channel = true;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.channel)
            {
				return false;
            }
			if (player.altFunctionUse == 2)
			{
				if(player.HasBuff(BuffType<SurgingVampirismCooldown>()))
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
			return base.CanUseItem(player);
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-75, 0);
		}
		public override void HoldItem(Player player)
        {
			player.AddBuff(BuffType<FeralDespair>(), 10);

            base.HoldItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				player.AddBuff(BuffType<SurgingVampirismCooldown>(), 1800);

				float missingHealth = player.statLifeMax2 - player.statLife;
				float healthPercentage = missingHealth / player.statLifeMax2;

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X/2, velocity.Y/2, ProjectileType<SanguineExplosionBolt>(),
					(int)(damage * MathHelper.Lerp(1, 4, healthPercentage))
					, knockback, player.whoAmI, (Main.MouseWorld - player.Center).ToRotation(), Main.rand.Next(80));

				for (int d = 0; d < 21; d++)
				{
					Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(24));
					float scale = 1f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed1 = perturbedSpeed1 * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 90, perturbedSpeed1.X, perturbedSpeed1.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;

				}

				for (int d = 0; d < 11; d++)
				{
					Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(87));
					float scale = 0.7f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed1 = perturbedSpeed1 * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, DustID.LifeDrain, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;

				}
				return false;
			}
			else
			{
				if (player.statLife >= 10)
				{
					player.statLife -= 10;

					
				}
				else
                {
					player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " " + LangHelper.GetTextValue("DeathReason.SanguineDespair")), 10, 0);
                }
				Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
				CombatText.NewText(textPos, new Color(139, 3, 0, 240), $"-10", false, false);
				for (int d = 0; d < 11; d++)
				{
					Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(37));
					float scale = 0.7f - (Main.rand.NextFloat() * 1.9f);
					perturbedSpeed1 = perturbedSpeed1 * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, DustID.LifeDrain, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;

				}
				return true;
			}
			
			
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Ruby, 8)
				.AddIngredient(ItemID.Diamond, 2)
				.AddIngredient(ItemID.CrimsonSeeds, 4)
				.AddIngredient(ItemType<EssenceOfDespair>())
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe(1)
				.AddIngredient(ItemID.Ruby, 8)
				.AddIngredient(ItemID.Diamond, 2)
				.AddIngredient(ItemID.CorruptSeeds, 4)
				.AddIngredient(ItemType<EssenceOfDespair>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
