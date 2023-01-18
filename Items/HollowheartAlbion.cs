using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    public class HollowheartAlbion : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Holding this weapon summons [c/D66AAB:Melusine] and [c/6AD6AF:Arondight] to circle you, aiming at your cursor" +
				"\nLeft click to fire from [c/D66AAB:Melusine]" +
				"\nAttacking with [c/D66AAB:Melusine] fires a missile that has a 50% chance to apply Burning to foes struck for 6 seconds" +
				"\nStriking Frostburned foes with [c/D66AAB:Melusine] will deal extra critical damage and expel Frostburn" +
				"\nRight click to fire from [c/6AD6AF:Arondight]" +
				"\nAttacking with [c/6AD6AF:Arondight] fires a missile that has a 50% chance to apply Frostburn to foes struck for 6 seconds" +
				"\nStriking Burning foes with [c/6AD6AF:Arondight] will deal extra critical damage and quench Burn" +
				"\nBullets will travel through walls" +
				"\nThis weapon can not crit unless criteria is met" +
				"\n'Even if it's just for a short dream, my wings will fly for your sake.'" +
				$"");  //The (English) text shown below your weapon's name
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;

		}

		public override void SetDefaults()
		{
			Item.damage = 190;           //The damage of your weapon
			Item.DamageType = DamageClass.Summon;
			Item.width = 220;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 35;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = StarsAboveAudio.SFX_AlbionBlast;
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.shoot = ProjectileType<Projectiles.MelusineBeam>();
			Item.shootSpeed = 90f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		 
		int type;
		Vector2 position;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 2 && player.whoAmI == Main.myPlayer)
			{
				
				return false;
				
			}


			int damage = player.GetWeaponDamage(Item);

			if (player.altFunctionUse == 2)
			{

				type = ProjectileType<Projectiles.ArondightBeam>();
				position = player.GetModPlayer<StarsAbovePlayer>().arondightPosition;

				float Speed = 28f;  //projectile speed

				Vector2 vector8 = player.GetModPlayer<StarsAbovePlayer>().arondightPosition;
				float launchSpeed = 36f;
				Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
				Vector2 direction = Vector2.Normalize(mousePosition - player.GetModPlayer<StarsAbovePlayer>().arondightPosition);
				Vector2 velocity = direction * launchSpeed;
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, type, damage, 0f, player.whoAmI);


				Main.projectile[index].originalDamage = Item.damage;
				float rotation = (float)Math.Atan2(vector8.Y - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y), vector8.X - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X));

				for (int d = 0; d < 25; d++)
				{
					float Speed2 = Main.rand.NextFloat(10, 18);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
					int dustIndex = Dust.NewDust(vector8, 0, 0, 221, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				for (int d = 0; d < 45; d++)
				{
					float Speed3 = Main.rand.NextFloat(8, 14);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
					int dustIndex = Dust.NewDust(vector8, 0, 0, 88, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}
				//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
				SoundEngine.PlaySound(StarsAboveAudio.SFX_AlbionBlast, player.Center);
				return true;
			}
			else
			{
				type = ProjectileType<Projectiles.MelusineBeam>();
				float Speed = 28f;

				position = player.GetModPlayer<StarsAbovePlayer>().melusinePosition;//Synced from the projectile
				float rotation = (float)Math.Atan2(position.Y - (Main.MouseWorld.Y), position.X - (Main.MouseWorld.X));//Aim towards mouse

				float launchSpeed = 36f;
				Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
				Vector2 direction = Vector2.Normalize(mousePosition - player.GetModPlayer<StarsAbovePlayer>().melusinePosition);
				Vector2 velocity = direction * launchSpeed;

				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, type, damage, 0f, player.whoAmI);


				Main.projectile[index].originalDamage = Item.damage;

				for (int d = 0; d < 25; d++)
				{
					float Speed2 = Main.rand.NextFloat(10, 18);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
					int dustIndex = Dust.NewDust(position, 0, 0, 219, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}//Dust
				for (int d = 0; d < 45; d++)
				{
					float Speed3 = Main.rand.NextFloat(8, 14);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(60)); // 30 degree spread.
					int dustIndex = Dust.NewDust(position, 0, 0, 86, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}//Dust

				SoundEngine.PlaySound(StarsAboveAudio.SFX_AlbionBlast, player.Center);

				return true;
			}



			return base.CanUseItem(player);

		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				//Emit dusts when swing the sword

				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 269);



			}
		}
		public override bool? UseItem(Player player)
		{



			return true;
		}
		public override void HoldItem(Player player)
		{
			
			player.GetModPlayer<StarsAbovePlayer>().albionHeld = 10;
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Melusine>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Melusine>(), 0, 4, player.whoAmI, 0f);


			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Arondight>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Arondight>(), 0, 4, player.whoAmI, 0f);


			}
			for (int i = 0; i < 50; i++)
			{
				Vector2 position = Vector2.Lerp(player.Center, player.GetModPlayer<StarsAbovePlayer>().arondightPosition, (float)i / 50);
				Dust d = Dust.NewDustPerfect(position, 92, null, 240, default(Color), 0.3f);
				d.fadeIn = 0.3f;
				d.noLight = true;
				d.noGravity = true;
				Vector2 position2 = Vector2.Lerp(player.Center, player.GetModPlayer<StarsAbovePlayer>().melusinePosition, (float)i / 50);
				Dust d2 = Dust.NewDustPerfect(position2, 71, null, 240, default(Color), 0.3f);
				d2.fadeIn = 0.3f;
				d2.noLight = true;
				d2.noGravity = true;
			}
			for (int i2 = 0; i2 < 50; i2++)
			{

			}
			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second

		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 
			position = player.GetModPlayer<StarsAbovePlayer>().melusinePosition;
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.ShroomiteBar, 2)
				.AddIngredient(ItemID.JackOLanternLauncher, 1)
				.AddIngredient(ItemID.SnowmanCannon, 1)
				.AddIngredient(ItemType<EssenceOfTheHollowheart>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
