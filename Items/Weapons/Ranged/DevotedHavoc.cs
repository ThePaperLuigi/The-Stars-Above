using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using StarsAbove.Projectiles.Ranged.ElCapitansHardware;
using System;
using StarsAbove.Utilities;
using StarsAbove.Projectiles.Ranged.DevotedHavoc;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Ranged
{
    public class DevotedHavoc : ModItem
	{
		public override void SetStaticDefaults() {
			

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 21;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
			Item.shoot = ProjectileType<DevotedHavocShot>();
			Item.shootSpeed = 30f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
		}
		int chosenOrdinance = 0;//0 is frag, 1 is arc star, 2 is thermite
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		int isInUseTimer;
		float isInUseTime;
		public override void HoldItem(Player player)
		{
            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
            {
                int dustEffect = DustID.GemTopaz;
                float dustAmount = 20f;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                    spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                    int dust = Dust.NewDust(player.MountedCenter, 0, 0, dustEffect);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = player.MountedCenter + spinningpoint5;
                    Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 10f;
                }
                Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);

                switch (chosenOrdinance)
                {
                    case 0:
                        CombatText.NewText(textPos, new Color(255, 0, 125, 240), LangHelper.GetTextValue("CombatText.DevotedHavoc.Shuriken"), false, false);

                        chosenOrdinance = 1;
                        break;
                    case 1:
                        CombatText.NewText(textPos, new Color(255, 0, 125, 240), LangHelper.GetTextValue("CombatText.DevotedHavoc.Molotov"), false, false);

                        chosenOrdinance = 2;
                        break;
                    case 2:
                        CombatText.NewText(textPos, new Color(255, 0, 125, 240), LangHelper.GetTextValue("CombatText.DevotedHavoc.Grenade"), false, false);

                        chosenOrdinance = 0;
                        break;
                }
                

            }
            isInUseTime++;

            isInUseTimer--;
            if (isInUseTimer < 0)
            {
				isInUseTime = 0;
            }
			else
			{
			}
			isInUseTime = (int)MathHelper.Clamp(isInUseTime, 0, 600);
            base.HoldItem(player);
		}
		public override bool CanUseItem(Player player)
		{
			var modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse == 2)
			{
				switch (chosenOrdinance)
				{
					case 0:
						if(player.HasItem(ItemID.Grenade))
						{
                            return true;

                        }
                        else
						{
							return false;
						}
					case 1:
                        if (player.HasItem(ItemID.Shuriken))
                        {
                            return true;

                        }
                        else
                        {
                            return false;
                        }
                    case 2:
                        if (player.HasItem(ItemID.MolotovCocktail))
                        {
                            return true;

                        }
                        else
                        {
                            return false;
                        }
                    default:
						//how??
						return false;
				}				
			}
			else
			{



				return true;
			}
		}
        public override float UseSpeedMultiplier(Player player)
        {
			
			return MathHelper.Lerp(0.5f, 1.5f, (float)(isInUseTime / 600));


        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float launchSpeed = 120f;
			float launchSpeed2 = 20f;
			float launchSpeed3 = 10f;

			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 Gun = direction * launchSpeed2;
			Vector2 throwableVelocity = direction * launchSpeed3;
			
			
            if (player.altFunctionUse == 2)
			{
				//Throwing ordinance
                switch (chosenOrdinance)
                {
                    case 0:
                        player.ConsumeItem(ItemID.Grenade);
                        Projectile.NewProjectile(source, position.X, position.Y, throwableVelocity.X, throwableVelocity.Y, ProjectileType<FragmentationGrenade>(), damage * 3, knockback, player.whoAmI);

                        return false;
                    case 1:
                        player.ConsumeItem(ItemID.Shuriken);
                        Projectile.NewProjectile(source, position.X, position.Y, throwableVelocity.X, throwableVelocity.Y, ProjectileType<EnergyStar>(), damage, knockback, player.whoAmI);

                        return false;
                    case 2:
                        player.ConsumeItem(ItemID.MolotovCocktail);
                        Projectile.NewProjectile(source, position.X, position.Y, throwableVelocity.X, throwableVelocity.Y, ProjectileType<ThermalFlare>(), damage, knockback, player.whoAmI);

                        return false;

                }
            }
			else
            {
                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 80f;
                position = new Vector2(position.X, position.Y);
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, Gun.X, Gun.Y, ProjectileType<DevotedHavocGun>(), 0, knockback, player.whoAmI);

                //Normal shooting
                SoundEngine.PlaySound(SoundID.Item11, player.Center);

                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
                isInUseTimer = 30;
				

            }


            return false;
		}

		
		public override void AddRecipes()
		{
			CreateRecipe(1)
					.AddIngredient(ItemID.Shuriken)
					.AddIngredient(ItemID.Grenade)
					.AddIngredient(ItemID.MolotovCocktail)
					.AddIngredient(ItemID.Emerald, 3)
					.AddIngredient(ItemType<EssenceOfEnergy>())
					.AddTile(TileID.Anvils)
					.Register();
		}
	}

}
