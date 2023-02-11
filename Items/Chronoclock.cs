
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
using StarsAbove.Projectiles.Adornment;
using StarsAbove.Buffs.Adornment;
using StarsAbove.Utilities;
using StarsAbove.Buffs.Chronoclock;
using StarsAbove.Projectiles.Chronoclock;

namespace StarsAbove.Items
{
    public class Chronoclock : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Chronoclock");
			Tooltip.SetDefault("" +
				"Summons a [Fragment of Time] to aid you in combat, granting immunity to Slow (Only one can be summoned at a time)" +
                "\nThe [Fragment of Time] will periodically nurture a [Time Bubble] centered on herself, which grows over time" +
                "\nThe [Time Bubble] will pop upon contact with an enemy, dealing damage and additionally exploding in a [Time Pulse]" +
				"\nThe [Time Pulse] deals damage in a large area, increasing in potency with the size of the [Time Bubble] and inflicts 18 summon tag damage" +
                "\nAt max size, the [Time Pulse] explodes automatically" +
                "\nTaking damage within the [Time Bubble] will pop the [Time Bubble] preventing that instance of damage" +
                "\nThe [Time Bubble] will be recast five seconds after popping" +
				"\n'Well, that sounds like a waste of time'"
				+ $"");

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 60;
			Item.width = 21;
			Item.height = 21;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noMelee = true;
			Item.knockBack = 6;
			Item.noUseGraphic = true;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item44;
			Item.shoot = ProjectileType<FragmentOfTimeMinion>();
			Item.buffType = BuffType<ChronoclockMinionBuff>(); //The buff added to player after used the item
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
				
			}

			return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
				
			}
			else
			{
			
			}

			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			
			
		
		}

        public override void UpdateInventory(Player player)
        {
			
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
