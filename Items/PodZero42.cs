
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
    public class PodZero42 : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Pod Zero-42");
			Tooltip.SetDefault("Summons an [c/EBD484:Automaton Pod] (Consumes 2 minion slots; Only one can be summoned at a time)" +
				"\nThe [c/EBD484:Automaton Pod] will passively fire bullets at nearby foes" +
				"\nHold the Weapon Action Key to control the [c/E3DABA:Automaton Pod], activating [c/E9C64A:Gatling Salvo]" +
				"\n[c/E9C64A:Gatling Salvo] fires a stream of bullets directly towards your cursor" +
                "\nThese bullets fire faster, travel faster, and deal 10% more damage, but consume 2 Mana per shot" +
				"\nThis cost can not be negated through any means, but mana regen delay is shortened" +
				"\nGain the buff Night Owl outside of combat and Hunter during combat" +
				"\n'Glory to Mankind'"
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
			Item.rare = ItemRarityID.LightRed;
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
			CreateRecipe(1)
				.AddIngredient(ItemID.TinPlating, 10)
				.AddIngredient(ItemID.Wire, 8)
				.AddIngredient(ItemType<PrismaticCore>(), 2)
				.AddIngredient(ItemType<EssenceOfTheAutomaton>())
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe(1)
				.AddIngredient(ItemID.CopperPlating, 10)
				.AddIngredient(ItemID.Wire, 8)
				.AddIngredient(ItemType<PrismaticCore>(), 2)
				.AddIngredient(ItemType<EssenceOfTheAutomaton>())
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}
