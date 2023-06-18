using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Items
{
    public class LightUnrelenting : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("Will fire a blade of light that will deal double damage and burn foes with 50 mana if mana is available" +
				"\nRight click to unleash your inner potential, gaining the unrivaled strength of [c/F1AF42:Limit Break] for 20 seconds (1 minute cooldown)" +
				"\nWhile under the influence of [c/F1AF42:Limit Break], health will be converted to mana without limit until mana is full" +
				"\nAdditonally, all damage is increased by 10% and all magic damage is increased by an additional 100%" +
				"\n'I shall transcend you!'" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 120;           //The damage of your weapon
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 116;
			Item.shootSpeed = 30f;
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
				if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.LimitBreakCooldown>()))
				{
					if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.LimitBreak>()))
					{
						Vector2 position = Main.LocalPlayer.position;
						int playerWidth = Main.LocalPlayer.width;
						int playerHeight = Main.LocalPlayer.height;
						Dust dust;
						for (int d = 0; d < 50; d++)
						{
							dust = Main.dust[Terraria.Dust.NewDust(position, playerWidth, playerHeight, 106, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, new Color(255, 255, 255), 1f)];
							dust.shader = GameShaders.Armor.GetSecondaryShader(81, Main.LocalPlayer);
						}
						player.AddBuff(BuffType<Buffs.LimitBreak>(), 1200);
						SoundEngine.PlaySound(StarsAboveAudio.SFX_Umbral, player.Center);

					}

				}


			}
			else
			{
				

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

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// 
			// 60 frames = 1 second
			if (player.statMana >= 50)
			{
				target.AddBuff(BuffID.OnFire, 120);
			}
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.statMana >= 50)
			{
				player.statMana -= 50;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,type, damage * 2, knockback, player.whoAmI);
			}
			
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<DullTotemOfLight>())
				.AddIngredient(ItemType<EssenceOfSurpassingLimits>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
