using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.BurningDesire;
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
			DisplayName.SetDefault("Catalyst's Memory");
			Tooltip.SetDefault("" +
                "Holding this weapon activates it, granting the buff [Catalyzed Blade], increasing movement speed by 10%" +
                "\nAttacks with this weapon barrage foes with myriad stabs, with a 10% chance to fire a [Shimmering Prism] that deals 50% of base damage" +
                "\nThe [Shimmering Prism] pierces up to 3 foes and deals 30% extra damage to foes below 50% HP" +
                "\nRight-click to conjure a [Dazzling Prismic] that lasts for 40 seconds (2 minute cooldown)" +
                "\nGain 10% increased attack, 10 defense, and 10 % movement speed when the [Dazzling Prismic] is present" +//Buff: Bedazzled
                "\nTaking damage will redirect 50% of the damage to the [Dazzling Prismic], up to 400 HP" +
                "\nOnce the HP threshold is reached, the [Dazzling Prismic] will shatter, granting [Dazzling Aria] for 2 seconds" +
                "\n[Dazzling Aria] grants powerful health regeneration and increases defense by 50" +
                "\nAdditionally, the [Dazzling Prismic] will deal minor damage to foes caught in its radius after shattering for a short duration" +
                "\nPress the Weapon Action Key when the [Dazzling Prismic] is present to launch the weapon towards it, shattering the [Dazzling Prismic]" +
                "\nAfter shattering the [Dazzling Prismic] with this method, gain 70 HP instantly and additionally gain [Dazzling Bladedance] for 5 seconds" +
                "\n[Dazzling Bladedance] increases melee speed by 30%" +
				$"");  //The (English) text shown below your weapon's name


			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 99;           //The damage of your weapon
			Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 4;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 4;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Rapier;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 3;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
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
			
				return base.CanUseItem(player);
		}
        public override void HoldItem(Player player)
        {
			player.GetModPlayer<StarsAbovePlayer>().CatalystMemoryProgress += 2;
			if(player.GetModPlayer<StarsAbovePlayer>().CatalystMemoryProgress > 50)
            {
				player.GetModPlayer<StarsAbovePlayer>().CatalystMemoryProgress = 50;

			}
			if (player.ownedProjectileCounts[ProjectileType<CatalystHeldBlade>()] < 1)
			{//Equip animation.
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<CatalystHeldBlade>(), 0, 0, player.whoAmI, 0f);
			}
			
		}

		public override bool? UseItem(Player player)
        {
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			/*if (player.altFunctionUse == 2 && !player.HasBuff(BuffType<BoilingBloodBuff>()))
			{
				
			}*/


			return base.UseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			
		}

		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 130f;
			
			
			if (player.altFunctionUse == 2)
			{
				 
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
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<CatalystStabEffectMain>(), damage/3, knockback, player.whoAmI);

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<CatalystStab>(), 0, knockback, player.whoAmI);

				if (Main.rand.Next(0,100) <= 10)
                {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<CatalystPrism>(), damage/2, knockback, player.whoAmI);

				}

			}
			return false;
		}

		public override void AddRecipes()
		{
			
			/*CreateRecipe(1)
				.AddIngredient(ItemID.ButchersChainsaw, 1)
				.AddIngredient(ItemID.Chain, 3)
				.AddIngredient(ItemID.LavaBucket, 1)
				.AddIngredient(ItemID.LunarBar, 8)
				.AddIngredient(ItemID.FragmentSolar, 18)
				.AddIngredient(ItemType<EssenceOfTheOverwhelmingBlaze>())
				.AddTile(TileID.Anvils)
				.Register();*/
			
		}
	}
}
