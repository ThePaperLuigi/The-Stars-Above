using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs;
using StarsAbove.Items.Pets;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Projectiles.SunsetOfTheSunGod;

namespace StarsAbove.Items
{
    public class SunsetOfTheSunGod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sunset of the Sun God");
			Tooltip.SetDefault("WORK IN PROGRESS WEAPON!!\n" +
				"[c/F592BF:This weapon is unaffected by Aspected Damage Type penalty]" +
				"\nRight click to unleash [Vasavi Shakti], launching yourself into the air and becoming Invincible (2 minute cooldown)" +
                "\nAfter a short delay, channel an incredibly powerful beam of energy towards your cursor" +
                "\n[Vasavi Shakti] will conclude with a colossal explosion, dealing 4x guaranteed critical damage and inflicting Daybroken" +
				"\nThe current [c/F592BF:Aspected Damage Type] influences the weapon's attacks (Weapon can not be used without [c/F592BF:Aspected Damage Type])" +
				"\n[c/FFAB4D:Melee]: Rapidly stab in a close-ranged flurry; Charged attacks swing in a wide arc, burning foes for 8 seconds" +
				"\n[c/EF4DFF:Magic]: Spears of lightning converge on your cursor, piercing up to 3 times" +
				"\n[c/4DFF5B:Ranged]: Attacks launch the spear at high velocity, piercing infinitely" +
				"\n[c/00CDFF:Summon]: Attacks swing the spear while extending a burning whip, causing summons to inflict Daybroken" +
				"\n'With this single strike, I shall inflict extinction!'" +
				$"");  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 220;
			Item.DamageType = DamageClass.Melee;
			Item.useStyle = ItemUseStyleID.HiddenAnimation;
			Item.width = 46;
			Item.height = 216;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon

			Item.noUseGraphic = true;
			Item.noMelee = true;

			Item.shoot = ProjectileType<KarnaSpearAttack>();
			Item.shootSpeed = 20;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect != 2
				&& player.GetModPlayer<StarsAbovePlayer>().MagicAspect != 2
				&& player.GetModPlayer<StarsAbovePlayer>().RangedAspect != 2
				&& player.GetModPlayer<StarsAbovePlayer>().SummonAspect != 2)
			{

				return false;
			}
			
			if (player.altFunctionUse == 2)
			{
				/*if (!player.HasBuff(BuffType<ArtificeSirenBuff>()) && !player.HasBuff(BuffType<ArtificeSirenCooldown>()))
				{
					
					return false;
				}
				else
				{
					return false;
				}*/

			}
			
			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		public int stabAmount;
		public int stabTimer;
		public override void HoldItem(Player player)
		{
			float launchSpeed = 8f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
			if (stabAmount > 0)
			{
				stabTimer--;
				if (stabTimer <= 0)
				{
					stabAmount--;

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<KarnaSpearAttack>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);
					stabTimer = 10;
				}
			}
			/*
			if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
			{
				
				Item.useTime = 20;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 20;
				Item.UseSound = SoundID.Item15;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{
				Item.useStyle = 5;
				
				Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 35;
				Item.UseSound = SoundID.Item125;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
			{
				Item.useStyle = 5;
				
				Item.useTime = 12;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 12;
				Item.UseSound = SoundID.Item125;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
			{
				Item.useStyle = 1;
				
				Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 35;
				Item.UseSound = SoundID.Item15;
			}
			*/
			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second
			//player.GetModPlayer<WeaponPlayer>().radiance++;
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
			{
				stabAmount = 3;
				stabTimer = 0;
				
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
			{


			}
			if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
            {

				
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{

				
			}
			return false;
		}

		public override void AddRecipes()
		{
			/*
			CreateRecipe(1)
				.AddIngredient(ItemID.ChlorophyteBar, 8)
				.AddIngredient(ItemID.BrokenHeroSword, 1)
				.AddIngredient(ItemID.Ectoplasm, 10)
				.AddIngredient(ItemID.SoulofLight, 12)
				.AddIngredient(ItemID.LunarBar, 12)
				.AddIngredient(ItemType<AegisCrystal>())
				.AddIngredient(ItemType<EssenceOfLuminance>())
				.AddTile(TileID.Anvils)
				.Register();*/
		}
	}
}
