
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.Takodachi;
using StarsAbove.Buffs;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Projectiles.Pod;
using StarsAbove.Items.Prisms;

namespace StarsAbove.Items
{
    public class AdornmentOfTheChaoticGod : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Adornment of the Chaotic God");
			Tooltip.SetDefault("WORK IN PROGRESS WEAPON" +
                "\nSummons an [Adornment Manifest] to assail your foes (Only one can be summoned at a time)" +
                "\nThe [Adornment Manifest] will fire chaotic blasts of magic with unpredictable effects" +
                "\nAdditionally, other random attacks may occur with unique effects" +
                "\nRight click to roll the dice, activating a [Pure Chaos] effect for 6 seconds (12 second cooldown)" +
                "\n60% chance [Positive Chaos]: Double [Adornment Manifest] attack speed, guarantee critical strikes, or gain Invincibility" +
                "\n30% chance [Negative Chaos]: Inflict random debuffs on self" +
                "\n10% chance to apply all effects at once at doubled potency" +
				"\nFire rate increases by 5% based on the amount of minions summoned" +
				"\n'Chaos can giveth, and chaos can taketh'"
				+ $"");

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.width = 21;
			Item.height = 21;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 6;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item44;
			Item.shoot = ProjectileType<PodMinion>();
			Item.buffType = BuffType<Buffs.Pod.PodBuff>(); //The buff added to player after used the item
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool? UseItem(Player player)
        {
			


			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			
			
		
		}

        public override void UpdateInventory(Player player)
        {
			
        }
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-3, 4);
		}

		
       
        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{

			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}


		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			
			for (int l = 0; l < Main.projectile.Length; l++)
			{
				Projectile proj = Main.projectile[l];
				if (proj.active && proj.type == Item.shoot && proj.owner == player.whoAmI)
				{
					proj.active = false;
				}
			}

			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
			

			player.SpawnMinionOnCursor(source, player.whoAmI, type, damage, knockback);
			return false;

		}
		public override void AddRecipes()
		{
			
		}

	}
}
