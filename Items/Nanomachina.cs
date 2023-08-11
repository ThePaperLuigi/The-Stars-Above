
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
using StarsAbove.Buffs.Nanomachina;
using StarsAbove.Projectiles.Nanomachina;

namespace StarsAbove.Items
{
    public class Nanomachina : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Nanomachina Reactor");
			/* Tooltip.SetDefault("" +
				"Use this item when at max Mana to consume all Mana and gain the buff [c/FF0046:Realized Nanomachina] for 20 seconds (12 second cooldown)" +
				"\nWhile [c/FF0046:Realized Nanomachina] is active: gain a barrier proportional to 20% of Max HP (Max 100 HP), gain 10% damage reduction, and gain knockback immunity" +
                "\nAdditionally, for 2 seconds after activation, gain an additional 40% damage reduction" +
                "\nTaking damage will subtract from the barrier, negating oncoming damage and granting 1 second of immunity" +
				"\nWhile [c/FF0046:Realized Nanomachina] is active, striking foes will restore 5 HP and 5 Mana (6 second cooldown)" +
				"\nAdditionally, striking enemies will slowly fill the [c/A00F0F:Nanomachine Gauge]; once full, automatically restore the duration and barrier HP of [c/FF0046:Realized Nanomachina]" +
				"\nDefeating foes increases the [c/A00F0F:Nanomachine Gauge] by 5%" +
				"\nWhen [c/FF0046:Realized Nanomachina] ends, the [c/A00F0F:Nanomachine Gauge] is emptied (Will also empty out of combat)" +
				$"\n'Nanomachines, son!'"); */

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


		}

		public override void SetDefaults()
		{
			

			Item.width = 21;
			Item.height = 21;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noMelee = true;
			Item.knockBack = 6;
			Item.noUseGraphic = true;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item44;
			Item.shoot = ProjectileType<NanomachinaShieldProjectile>();
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		public override bool AltFunctionUse(Player player)
		{
			return false;
		}
        public override bool CanUseItem(Player player)
        {
			if (player.statMana >= player.statManaMax2 && !player.HasBuff(BuffType<RealizedNanomachinaCooldownBuff>()) && player.whoAmI == Main.myPlayer)
			{
				return true;
			}
			else
            {
				return false;
            }
        }

        public override bool? UseItem(Player player)
        {
			player.AddBuff(BuffType<RealizedNanomachinaBuff>(), 1200);
			player.AddBuff(BuffType<RealizedNanomachinaCooldownBuff>(), 720);
			player.AddBuff(BuffType<RealizedNanomachinaActivation>(), 120);


			SoundEngine.PlaySound(StarsAboveAudio.SFX_GuntriggerParryPrep, player.Center);

			Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<NanomachinaShieldProjectile>(), 0, 0, player.whoAmI);
			player.statMana = 0;
			player.manaRegenDelay = 600;
			player.GetModPlayer<WeaponPlayer>().nanomachinaShieldHPMax = (int)MathHelper.Min((float)(player.statLifeMax2 * 0.2), 50f);
			player.GetModPlayer<WeaponPlayer>().nanomachinaShieldHP = player.GetModPlayer<WeaponPlayer>().nanomachinaShieldHPMax;



			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(player.Center, 0, 0, DustID.Flare, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-2, 2), 150, default(Color), 0.5f);
			}
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(player.Center, 0, 0, DustID.Teleporter, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 150, default(Color), 0.8f);
			}
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.8f);
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

			
		}


		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			
			
			return false;

		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.MeteoriteBar, 15)
				.AddIngredient(ItemID.Solidifier, 1)
				.AddIngredient(ItemID.IronskinPotion, 1)
				.AddIngredient(ItemType<EssenceOfNanomachines>())
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}
