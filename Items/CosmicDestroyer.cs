using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.CosmicDestroyer;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    public class CosmicDestroyer : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("[c/F592BF:This weapon is unaffected by Aspected Damage Type penalty]" +
				"\nFires a barrage of hyperpowered energy bolts" +
				"\nStriking foes will increment the [c/00B7FF:Magiton Gauge]" +
				"\nOnce the [c/00B7FF:Magiton Gauge] is full, right click to activate [c/FE3727:Magiton Overheat] for 8 seconds" +
				"\nDuring [c/FE3727:Magiton Overheat], attacks will change the next eleven shots into empowered [c/CF1E1E:Magiton Shots]" +
				"\n[c/CF1E1E:Magiton Shots] deal 3x base damage and will be critical strikes against foes below 50% HP" +
				"\nOnce [c/FE3727:Magiton Overheat] is either fully consumed or ends, you are inflicted with [c/A56363:Overheated] for 1 second, causing this weapon to become unusable" +
				"\n'This isn't... something you see everyday'" +
				$"");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {

			
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				Item.damage = 642;
			}
			else
			{
				Item.damage = 338;
			}

			Item.DamageType = DamageClass.Ranged;
			Item.width = 136;
			Item.height = 56;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 2;
			Item.rare = ItemRarityID.Red;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<CosmicDestroyerRound>();
			Item.shootSpeed = 11f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (player.GetModPlayer<WeaponPlayer>().CosmicDestroyerGauge >= 100)
				{
					player.AddBuff(BuffType<Buffs.CosmicDestroyer.MagitonOverheat>(), 480);
					
					SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, player.Center);
					player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
					player.GetModPlayer<WeaponPlayer>().CosmicDestroyerRounds = 11;
					player.GetModPlayer<WeaponPlayer>().CosmicDestroyerGauge = 0;

					Vector2 position = Main.LocalPlayer.position;
					int playerWidth = Main.LocalPlayer.width;
					int playerHeight = Main.LocalPlayer.height;
					Dust dust;
					for (int d = 0; d < 30; d++)
					{
						dust = Main.dust[Terraria.Dust.NewDust(position, playerWidth, playerHeight, 127, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 0, new Color(255, 255, 255), 1f)];
						
					}
					return false;
				}
				else
				{
					return false;
				}

			}
			if(player.HasBuff(BuffType<Buffs.CosmicDestroyer.Overheated>()))
            {
				return false;
            }

			return base.CanUseItem(player);
		}
		public override void HoldItem(Player player)
		{


			base.HoldItem(player);
		}
        public override bool? UseItem(Player player)
        {


            return base.UseItem(player);

        }

        
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 8);
		}

		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 160f;
			position = new Vector2(position.X, position.Y + 7);
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<MiseryRound>(), 0, knockback, player.whoAmI);

			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
			velocity.X = perturbedSpeed.X;
			velocity.Y = perturbedSpeed.Y;

			
			SoundEngine.PlaySound(SoundID.Item11, player.position);
			if(player.HasBuff(BuffType<Buffs.CosmicDestroyer.MagitonOverheat>()) && player.GetModPlayer<WeaponPlayer>().CosmicDestroyerRounds > 0)
            {
				player.GetModPlayer<WeaponPlayer>().CosmicDestroyerRounds--;
				damage *= 3;
				type = ProjectileType<CosmicDestroyerRound2>();

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
					int dustIndex = Dust.NewDust(position, 0, 0, 90, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;

				}
				for (int d = 0; d < 31; d++)
				{
					Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(87));
					float scale = 1.7f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed1 = perturbedSpeed1 * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 127, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 1.6f);
					Main.dust[dustIndex].noGravity = true;

				}
				for (int d = 0; d < 16; d++)
				{
					Vector2 perturbedSpeed2 = new Vector2(velocity.X / 2, velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(17));
					float scale = 2f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed2 = perturbedSpeed2 * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 31, perturbedSpeed2.X, perturbedSpeed2.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
				return false;
			}
			else
            {
				player.GetModPlayer<WeaponPlayer>().CosmicDestroyerGaugeVisibility = 2f;

				for (int d = 0; d < 21; d++)
				{
					Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(17));
					float scale = 1f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed1 = perturbedSpeed1 * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 92, perturbedSpeed1.X, perturbedSpeed1.Y, 150, default(Color), 1.8f);
					Main.dust[dustIndex].noGravity = true;

				}
				for (int d = 0; d < 21; d++)
				{
					Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(87));
					float scale = 0.7f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed1 = perturbedSpeed1 * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 92, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;

				}
				for (int d = 0; d < 16; d++)
				{
					Vector2 perturbedSpeed2 = new Vector2(velocity.X / 2, velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(17));
					float scale = 2f - (Main.rand.NextFloat() * .9f);
					perturbedSpeed2 = perturbedSpeed2 * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, 31, perturbedSpeed2.X, perturbedSpeed2.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

			}
			return false;

			
		}

		public override void AddRecipes()
		{
			
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				
				CreateRecipe(1)
					.AddIngredient(calamityMod.Find<ModItem>("CosmiliteBar").Type, 4)
					.AddIngredient(ItemID.ShroomiteBar, 12)
					.AddIngredient(ItemID.LunarBar, 12)
					.AddIngredient(ItemID.ChainGun, 1)
					.AddIngredient(ItemID.Sapphire, 5)
					.AddIngredient(ItemID.SoulofMight, 30)
					.AddIngredient(ItemID.IllegalGunParts, 2)
					.AddIngredient(ItemType<EssenceOfTheFuture>())
					.AddTile(TileID.Anvils)
					.Register();
			}
            else
			{


				CreateRecipe(1)
				.AddIngredient(ItemID.ShroomiteBar, 12)
				.AddIngredient(ItemID.LunarBar, 12)
				.AddIngredient(ItemID.ChainGun, 1)
				.AddIngredient(ItemID.Sapphire, 5)
				.AddIngredient(ItemID.SoulofMight, 30)
				.AddIngredient(ItemID.IllegalGunParts, 2)
				.AddIngredient(ItemType<EssenceOfTheFuture>())
				.AddTile(TileID.Anvils)
				.Register();
			}

		}
	}
}
