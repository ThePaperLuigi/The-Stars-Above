
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

namespace StarsAbove.Items
{
    public class Chronoclock : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Work in progress weapon");
			Tooltip.SetDefault("" +
				"???" +
				"\n''"
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
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noMelee = true;
			Item.knockBack = 6;
			Item.noUseGraphic = true;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item44;
			Item.shoot = ProjectileType<AdornmentMinion>();
			Item.buffType = BuffType<AdornmentMinionBuff>(); //The buff added to player after used the item
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
				if (!player.HasBuff(BuffType<PureChaosCooldown>()))
                {
					for (int i = 0; i < 30; i++)
					{
						int dustIndex = Dust.NewDust(player.Center, 0, 0, 220, Main.rand.NextFloat(-25, 25), Main.rand.NextFloat(-25, 25), 100, default(Color), 1f);
						Main.dust[dustIndex].noGravity = true;

						dustIndex = Dust.NewDust(player.Center, 0, 0, 220, Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-20, 20), 100, default(Color), 2f);
						Main.dust[dustIndex].velocity *= 3f;
					}
					player.AddBuff(BuffType<PureChaosCooldown>(), 12 * 60);
					int chaos = Main.rand.Next(0, 10);
					if (chaos == 0)//10% bonus, all effects at once
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(114, 237, 119, 240), $"{LangHelper.GetTextValue($"CombatText.Adornment.Positive")}", false, false);

						player.AddBuff(BuffType<AdornmentCritBuff>(), 180);

						player.AddBuff(BuffType<AdornmentAttackSpeedBuff>(), 180);

						player.AddBuff(BuffType<Invincibility>(), 180);

					}
					else if (chaos >= 1 && chaos < 4)//1,2,3 30%
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(237, 114, 114, 240), $"{LangHelper.GetTextValue($"CombatText.Adornment.Negative")}", false, false);

						if (Main.rand.NextBool())
						{//Random debuffs
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.Poisoned, 60);
							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.Darkness, 60);

							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.Cursed, 60);

							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.OnFire, 60);

							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.Bleeding, 60);

							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.Slow, 60);

							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.Confused, 60);

							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.Weak, 60);

							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.BrokenArmor, 60);

							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.Chilled, 60);

							}
							if (Main.rand.NextBool())
							{
								player.AddBuff(BuffID.Frozen, 60);

							}
						}
						else
						{//Random objects
							player.AddBuff(BuffType<AdornmentRandomObjectsBuff>(), 360);
						}
					}
					else if (chaos >= 4 && chaos <= 9)//4,5,6,7,8,9 60%
					{
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(114, 237, 119, 240), $"{LangHelper.GetTextValue($"CombatText.Adornment.Positive")}", false, false);

						int goodChaos = (Main.rand.Next(3));
						if (goodChaos == 0)
						{
							player.AddBuff(BuffType<AdornmentCritBuff>(), 360);
						}
						else if (goodChaos == 1)
						{
							player.AddBuff(BuffType<AdornmentAttackSpeedBuff>(), 360);
						}
						else if (goodChaos == 2)
						{
							player.AddBuff(BuffType<Invincibility>(), 360);
						}
					}
					return true;
                }
				else
                {
					return false;
                }

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
