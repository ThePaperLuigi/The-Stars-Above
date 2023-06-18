using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.BurningDesire;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.BurningDesire;
using StarsAbove.Projectiles.CatalystMemory;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class CatalystMemory : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Catalyst's Memory");
			

			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				/* Tooltip.SetDefault("" +
				"[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from all damage classes]" +
				"\nHolding this weapon activates it, granting the buff [c/EAC0ED:Catalyzed Blade], increasing movement speed by 10%" +
				"\nAttacks with this weapon barrage foes with myriad stabs dealing 80% of base damage, with a 10% chance to fire a [c/F581FE:Shimmering Prism] that deals 50% of base damage" +
				"\nThe [c/F581FE:Shimmering Prism] pierces up to 3 foes and deals 30% extra damage to foes below 50% HP" +
				"\nRight-click to conjure a [c/7B48CA:Dazzling Prismic] on your cursor that lasts for 20 seconds (1 minute cooldown)" +
				"\nThe [c/7B48CA:Dazzling Prismic] will shatter if too far away" +
				"\nGain 10% increased attack, 10 defense, and 10% movement speed when the [c/7B48CA:Dazzling Prismic] is present" +//Buff: Bedazzled
				"\nTaking damage will redirect 80% of the damage to the [c/7B48CA:Dazzling Prismic], up to 600 HP" +
				"\nOnce the HP threshold is reached, the [c/7B48CA:Dazzling Prismic] will shatter, granting [c/484CCA:Dazzling Aria] for 2 seconds" +
				"\n[c/484CCA:Dazzling Aria] grants powerful health regeneration and increases defense by 50" +
				"\nPress the Weapon Action Key when the [c/7B48CA:Dazzling Prismic] is present to launch the weapon towards it, shattering the [c/7B48CA:Dazzling Prismic]" +
				"\nAfter shattering the [c/7B48CA:Dazzling Prismic] with this method, gain 70 HP instantly and additionally gain [c/FF61CA:Dazzling Bladedance] for 5 seconds" +
				"\n[c/FF61CA:Dazzling Bladedance] increases melee speed by 30%" +
				"\nThe [c/7B48CA:Dazzling Prismic] will deal minor damage to foes caught in its radius after shattering for a short duration" +
				$""); */  //The (English) text shown below your weapon's name
			}
			else
			{
				/* Tooltip.SetDefault("" +
				"[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from all damage classes]" +
				"\nHolding this weapon activates it, granting the buff [c/EAC0ED:Catalyzed Blade], increasing movement speed by 10%" +
				"\nAttacks with this weapon barrage foes with myriad stabs dealing 80% of base damage, with a 10% chance to fire a [c/F581FE:Shimmering Prism] that deals 50% of base damage" +
				"\nThe [c/F581FE:Shimmering Prism] pierces up to 3 foes and deals 30% extra damage to foes below 50% HP" +
				"\nRight-click to conjure a [c/7B48CA:Dazzling Prismic] on your cursor that lasts for 20 seconds (1 minute cooldown)" +
				"\nThe [c/7B48CA:Dazzling Prismic] will shatter if too far away" +
				"\nGain 10% increased attack, 10 defense, and 10% movement speed when the [c/7B48CA:Dazzling Prismic] is present" +//Buff: Bedazzled
				"\nTaking damage will redirect 80% of the damage to the [c/7B48CA:Dazzling Prismic], up to 250 HP" +
				"\nOnce the HP threshold is reached, the [c/7B48CA:Dazzling Prismic] will shatter, granting [c/484CCA:Dazzling Aria] for 2 seconds" +
				"\n[c/484CCA:Dazzling Aria] grants powerful health regeneration and increases defense by 50" +
				"\nPress the Weapon Action Key when the [c/7B48CA:Dazzling Prismic] is present to launch the weapon towards it, shattering the [c/7B48CA:Dazzling Prismic]" +
				"\nAfter shattering the [c/7B48CA:Dazzling Prismic] with this method, gain 70 HP instantly and additionally gain [c/FF61CA:Dazzling Bladedance] for 5 seconds" +
				"\n[c/FF61CA:Dazzling Bladedance] increases melee speed by 30%" +
				"\nThe [c/7B48CA:Dazzling Prismic] will deal minor damage to foes caught in its radius after shattering for a short duration" +
				$""); */  //The (English) text shown below your weapon's name
			}
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				Item.damage = 289;
			}
			else
			{
				Item.damage = 89;
			}
			           //The damage of your weapon
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 4;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 4;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Rapier;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 3;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton

			Item.shoot = 337;
			Item.shootSpeed = 8f;

			Item.noUseGraphic = true;
			Item.noMelee = true;
			
		}

		int currentSwing;
		int slashDuration;
		int comboTimer;
		public override bool AltFunctionUse(Player player)
		{

			return true;
		}
        public override void UpdateInventory(Player player)
        {

			
			base.UpdateInventory(player);
        }
        public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if(!player.HasBuff(BuffType<CatalystMemoryPrismicCooldown>()))
                {
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
				return base.CanUseItem(player);
		}
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<WeaponPlayer>().CatalystMemoryProgress += 2;//Increase animation progress when the weapon is held.
			if (player.GetModPlayer<WeaponPlayer>().CatalystMemoryProgress > 50)
			{
				player.GetModPlayer<WeaponPlayer>().CatalystMemoryProgress = 50;//Cap it to 50

			}
			if (player.GetModPlayer<WeaponPlayer>().CatalystMemoryProgress > 25)//The blade is drawn.
			{
				player.AddBuff(BuffType<CatalyzedBlade>(), 10);
			}
			if (player.ownedProjectileCounts[ProjectileType<CatalystHeldBlade>()] < 1)
			{//Equip animation.
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<CatalystHeldBlade>(), 0, 0, player.whoAmI, 0f);
			}

			//Weapon Action Key
			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed && player.itemTime <= 0)
			{
				if(player.HasBuff(BuffType<Bedazzled>()))
                {
					//Fire the blade towards the prismic's position.
					//Once the blade is in contact with the prismic it will shatter.
					SoundEngine.PlaySound(StarsAboveAudio.SFX_CatalystSwing, player.Center);
					Vector2 direction = Vector2.Normalize(player.GetModPlayer<WeaponPlayer>().CatalystPrismicPosition - player.Center);
					Vector2 velocity = direction * 1f;
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<CatalystThrow>(), 0, 0, player.whoAmI, 0f);
					//After that, grant a buff.
					//player.AddBuff(BuffType<DazzlingBladedance>(), 300);
				}
			}
		}

		public override bool? UseItem(Player player)
        {
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			/*if (player.altFunctionUse == 2 && !player.HasBuff(BuffType<BoilingBloodBuff>()))
			{
				
			}*/


			return base.UseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}

		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 130f;
			
			
			if (player.altFunctionUse == 2)
			{
				if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
				{
					player.GetModPlayer<WeaponPlayer>().CatalystPrismicHP = 250;
				}
				else
                {
					player.GetModPlayer<WeaponPlayer>().CatalystPrismicHP = 600;
				}
					
				player.AddBuff(BuffType<Bedazzled>(), 1800);//Determines if the Prismic is alive or not. Provides buffs.
				SoundEngine.PlaySound(StarsAboveAudio.SFX_PrismicSpawn, player.Center);
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld.X, Main.MouseWorld.Y - 20, 0, 0, ProjectileID.PrincessWeapon, 0, knockback, player.whoAmI);
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<Prismic>(), damage, knockback, player.whoAmI);

				player.AddBuff(BuffType<CatalystMemoryPrismicCooldown>(), 3600);//Temporary time
			}
			else
			{

				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(15));
				velocity.X = perturbedSpeed.X;
				velocity.Y = perturbedSpeed.Y;

				for (int d = 0; d < 5; d++)
				{
					Vector2 perturbedSpeedNew = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(15));
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeedNew.X, perturbedSpeedNew.Y, ProjectileType<CatalystStabEffect>(), 0, knockback, player.whoAmI);

				}
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<CatalystStabEffectMain>(), (int)(damage * 0.8), knockback, player.whoAmI);

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<CatalystStab>(), 0, knockback, player.whoAmI);
				SoundEngine.PlaySound(StarsAboveAudio.SFX_CatalystSwing, player.Center);
				if (Main.rand.Next(0,100) <= 10)
                {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<CatalystPrism>(), damage/2, knockback, player.whoAmI);

				}

			}
			return false;
		}

		public override void AddRecipes()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				CreateRecipe(1)
				.AddIngredient(ItemID.PiercingStarlight, 1)
				.AddIngredient(calamityMod.Find<ModItem>("AuricBar").Type, 5)
				.AddIngredient(calamityMod.Find<ModItem>("CosmiliteBar").Type, 3)
				.AddIngredient(ItemID.FragmentSolar, 16)
				.AddIngredient(ItemType<EssenceOfQuantum>())
				.AddTile(TileID.Anvils)
				.Register();
			}
			else
			{
				CreateRecipe(1)
				.AddIngredient(ItemID.PiercingStarlight, 1)
				.AddIngredient(ItemID.FragmentSolar, 16)
				.AddIngredient(ItemType<EssenceOfQuantum>())
				.AddTile(TileID.Anvils)
				.Register();
			}
			/**/

		}
	}
}
