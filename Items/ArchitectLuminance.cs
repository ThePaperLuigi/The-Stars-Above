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

namespace StarsAbove.Items
{
    public class ArchitectLuminance : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Architect's Luminance");
			Tooltip.SetDefault("" +
				"[c/F592BF:This weapon is unaffected by Aspected Damage Type penalty]" +
				"\nHolding this weapon will equip yourself with the [c/6EF367:Luminary Armament], increasing defense by 30" +
				"\nRight click to summon [c/70D38B:Artifice Siren] for 15 seconds at your cursor (1 minute cooldown)" +
				"\n[c/70D38B:Artifice Siren] will obliterate nearby foes with hyperpowered beams of light, even when this weapon is not held" +
				"\nThe current [c/F592BF:Aspected Damage Type] influences the weapon's attacks" +
				"\n[c/FFAB4D:Melee]: The weapon swings much faster and defense is added to critical strike damage" +
				"\n[c/EF4DFF:Magic]: Instead of swinging, the weapon will will conjure 6 explosive blasts in a fan pattern dealing 1/3 base damage" +
				"\n[c/4DFF5B:Ranged]: Instead of swinging, the weapon will quickly fire piercing shots without consuming ammo, dealing 1/2 base damage" +
				"\n[c/00CDFF:Summon]: The [c/6EF367:Luminary Armament] periodically attacks nearby foes independently, dealing half of your current HP as base damage and can crit" +
				"\n'For the future!'" +
				$"");  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 342;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 158;            //Weapon's texture's width
			Item.height = 158;           //Weapon's texture's height
			Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 35;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item15;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			
			Item.shoot = ProjectileType<Projectiles.ArchitectLuminance.ArchitectShoot>();
			Item.shootSpeed = 38;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<ArtificeSirenBuff>()) && !player.HasBuff(BuffType<ArtificeSirenCooldown>()))
				{
					player.AddBuff(BuffType<Buffs.ArtificeSirenBuff>(), 900);
					//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),Main.MouseWorld, Vector2.Zero, mod.ProjectileType("ErinysFX"), 0, 0, player.whoAmI, 0, 1);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, player.Center);

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("Siren").Type, 0, 0, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("SirenTurret1").Type, 0, 0, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("SirenLaser1").Type, Item.damage, 0, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("SirenTurret2").Type, 0, 0, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("SirenLaser2").Type, Item.damage, 0, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("SirenTurret3").Type, 0, 0, player.whoAmI);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("SirenLaser3").Type, Item.damage, 0, player.whoAmI);

					return false;
				}
				else
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
			Item.scale = 2f;
			player.AddBuff(BuffType<Buffs.ArchitectLuminanceBuff>(), 2);
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.ArchitectLuminance.Armament>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.ArchitectLuminance.Armament>(), 0, 4, player.whoAmI, 0f);


			}
			if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
			{
				Item.useStyle = 1;
				Item.noUseGraphic = false;
				Item.noMelee = false;
				Item.useTime = 20;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 20;
				Item.UseSound = SoundID.Item15;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{
				Item.useStyle = 5;
				Item.noUseGraphic = true;
				Item.noMelee = true;
				Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 35;
				Item.UseSound = SoundID.Item125;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
			{
				Item.useStyle = 5;
				Item.noUseGraphic = true;
				Item.noMelee = true;
				Item.useTime = 12;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 12;
				Item.UseSound = SoundID.Item125;
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
			{
				Item.useStyle = 1;
				Item.noUseGraphic = false;
				Item.noMelee = false;
				Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 35;
				Item.UseSound = SoundID.Item15;
			}

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
			if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
            {

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileID.MoonlordBullet, damage/2, knockback, player.whoAmI);
				
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, velocity.X/2, velocity.Y/2, ProjectileType<Projectiles.ArchitectLuminance.ArchitectShoot>(), 0, 3, player.whoAmI, 0f);


			}
			if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
			{

				float numberProjectiles = 6;
				float rotation = MathHelper.ToRadians(15);
				position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 15f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.LunarFlare, damage/3 , knockback, player.whoAmI);
				}
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, velocity.X/2, velocity.Y/2, ProjectileType<Projectiles.ArchitectLuminance.ArchitectShoot>(), 0, 3, player.whoAmI, 0f);


			}
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.ChlorophyteBar, 8)
				.AddIngredient(ItemID.BrokenHeroSword, 1)
				.AddIngredient(ItemID.Ectoplasm, 10)
				.AddIngredient(ItemID.SoulofLight, 12)
				.AddIngredient(ItemID.LunarBar, 12)
				.AddIngredient(ItemType<AegisCrystal>())
				.AddIngredient(ItemType<EssenceOfLuminance>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
