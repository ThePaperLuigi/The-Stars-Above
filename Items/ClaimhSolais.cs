using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    public class ClaimhSolais : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("" +
				"Critical strikes with this weapon will grant a stack of [c/F1AF42:Radiance] (Up to 10)" +
				"\nRight click with 5 stacks of [c/F1AF42:Radiance] to unleash the [c/F35B43:Radiant Soul], consuming all [c/F1AF42:Radiance]" +
				"\nWhen the [c/F35B43:Radiant Soul] is unleashed, gain 2 seconds of Invincibility" +
				"\nThe [c/F35B43:Radiant Soul] will deal powerful critical damage to nearby foes in a large radius" +
				"\nThe [c/F35B43:Radiant Soul]'s damage is influenced by a portion of defense and current stacks of [c/F1AF42:Radiance]" +
				"\nGaining [c/F1AF42:Radiance] past 10 stacks will instead grant Ironskin for 4 seconds" +
				"\nThe weapon grows in scale based upon a portion of defense" +
				"\n'An indefatigable sword. Its blade glitters with light'" +
				$"");  //The (English) text shown below your weapon's name'
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 159;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 96;            //Weapon's texture's width
			Item.height = 96;           //Weapon's texture's height
			Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 35;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
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
				if (player.GetModPlayer<StarsAbovePlayer>().radiance >= 5)
				{
					player.AddBuff(BuffType<Buffs.Invincibility>(), 120);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("ErinysFX").Type, 0, 0, player.whoAmI, 0, 1);
					SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, player.Center);

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("Erinys").Type, 0, 0, player.whoAmI, Item.damage + (player.GetModPlayer<StarsAbovePlayer>().radiance * 10) + player.statDefense/2, 1);
					player.GetModPlayer<StarsAbovePlayer>().radiance = 0;
					return false;
				}
				else
				{
					return false;
				}

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
		public override void HoldItem(Player player)
		{
			Item.scale = 1f + (player.statDefense / 250f);
			if (player.GetModPlayer<StarsAbovePlayer>().radiance >= 5)
			{
				Dust dust;
				for (int d = 0; d < 2; d++)
				{
					dust = Main.dust[Terraria.Dust.NewDust(player.Center, 1, 1, 269, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 0, new Color(255, 255, 255), 1f)];
					dust.shader = GameShaders.Armor.GetSecondaryShader(81, Main.LocalPlayer);
				}
			}
			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second
			//player.GetModPlayer<StarsAbovePlayer>().radiance++;
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.LifeFruit, 3)
				.AddIngredient(ItemID.BrokenHeroSword, 1)
				.AddIngredient(ItemID.SoulofLight, 12)
				.AddIngredient(ItemID.EyeoftheGolem, 1)
				.AddIngredient(ItemType<EssenceOfRadiance>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
