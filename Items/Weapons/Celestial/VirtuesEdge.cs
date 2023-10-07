using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Buffs.VirtuesEdge;
using StarsAbove.Systems;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Celestial.VirtuesEdge;

namespace StarsAbove.Items.Weapons.Celestial
{
    public class VirtuesEdge : ModItem
	{
		// The texture doesn't have the same name as the item, so this property points to it.
		//public override string Texture => "ExampleMod/Content/Items/Weapons/ExampleWhip";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Virtue's Edge");
			/* Tooltip.SetDefault("" +
				"Attacks with this weapon swing in a wide arc" +
				"\nHolding this weapon will cause the [c/4C3BB6:Virtue Gauge] to appear" +
				"\nAttacks with this weapon will fill the [c/4C3BB6:Virtue Gauge] with [c/49C7EC:Astral Flux]" +
				"\nCritical strikes will fill the [c/4C3BB6:Virtue Gauge] faster" +
				"\nRight click when the [c/4C3BB6:Virtue Gauge] is full to cleave a tear in spacetime, spawning a [c/495DEC:Celestial Void] for 8 seconds" +
				"\nThe [c/495DEC:Celestial Void] will pulse powerful damage to all nearby foes, dragging them inwards" +
				"\nAttacks with this weapon during this time will fill the [c/4C3BB6:Virtue Gauge] with [c/6C38E3:Umbral Flux]" +
				"\nIf the [c/4C3BB6:Virtue Gauge] is full once the [c/495DEC:Celestial Void] ends, it will collapse in a massive explosion, dealing 5x base damage" +
				"\nThere is a 45 cooldown between the re-activation of [c/495DEC:Celestial Void]" +
				"\n'Pierce through all matter!'" +
				$""); */
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 12));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		}

		public override void SetDefaults()
		{
			// Call this method to quickly set some of the properties below.
			//Item.DefaultToWhip(ModContent.ProjectileType<ExampleWhipProjectileAdvanced>(), 20, 2, 4);
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				Item.damage = 700;
			}
			else
			{
				Item.damage = 225;
			}
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Item.width = 30;
			Item.height = 30;
			Item.knockBack = 7;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.buyPrice(gold: 1);
			Item.shoot = ModContent.ProjectileType<VirtueEdgeSlash1>();
			Item.shootSpeed = 4;
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item1;
			Item.channel = false; // This is used for the charging functionality. Remove it if your whip shouldn't be chargeable.
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int randomCast;
		bool altSwing;
		int lecture;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override void HoldItem(Player player)
        {
			//player.AddBuff(BuffType<MorningStarHeld>(), 10);
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();

			if(player.HasBuff(BuffType<CelestialVoidBuff>()))
            {
				modPlayer.VirtueMode = 1;
            }
			else
            {
				modPlayer.VirtueMode = 0;
            }
			
		}

        public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				

			}
			else
			{
				
			}

			return true;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse != 2)
			{
				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueEdgeSlash2>(),damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueVFX2>(), 0, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueEdgeSlash1>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueVFX1>(), 0, knockback, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueEdgeSlash1>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueVFX1>(), 0, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueEdgeSlash2>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueVFX2>(), 0, knockback, player.whoAmI, 0f);


						altSwing = true;
					}
				}
			}
			else
            {
				if(modPlayer.VirtueGauge >= 100 && modPlayer.VirtueMode == 0)//Add cooldown check as well 
                {
					modPlayer.VirtueGauge = 0;
					//Spawn a slower cleaving sword.
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueEdgeSlashVoid>(), damage, knockback, player.whoAmI, 0f);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<VirtueVFXVoid>(), damage, knockback, player.whoAmI, 0f);
					//Spawn the black hole inside the VFX so it's closer to the blade tip.
					SoundEngine.PlaySound(StarsAboveAudio.SFX_iceCracking, player.Center);//SFX

					//Activate the shockwave effect, for a distortion.

				}
			}

			return false;

			
			

			//return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}
		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				CreateRecipe(1)
					.AddIngredient(ItemID.FragmentSolar, 20)
					.AddIngredient(ItemID.FragmentVortex, 20)
					.AddIngredient(ItemID.LunarBar, 20)
					.AddIngredient(calamityMod.Find<ModItem>("CosmiliteBar").Type, 8)
					.AddIngredient(calamityMod.Find<ModItem>("DarkPlasma").Type, 8)
					.AddIngredient(ItemType<EssenceOfDestiny>())
					.AddTile(TileID.Anvils)
					.Register();


			}
			else
			{

				CreateRecipe(1)
				.AddIngredient(ItemID.FragmentSolar, 20)
				.AddIngredient(ItemID.FragmentVortex, 20)
				.AddIngredient(ItemID.LunarBar, 20)
				.AddIngredient(ItemType<EssenceOfDestiny>())
				.AddTile(TileID.Anvils)
				.Register();
			}
			
		}
	}
}