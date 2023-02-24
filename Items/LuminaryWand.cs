using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles.Starchild;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class LuminaryWand : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("" +
				"Holding this weapon will conjure a friendly [c/F1D671:Starchild]" +
				"\nYour defense is directly added to the [c/F1D671:Starchild]'s contact damage" +
				"\nWhen the [c/F1D671:Starchild] deals damage to foes, there is a small chance that a [c/71F1CA:Star Bit] will appear" +
				"\nIf you pick up a [c/71F1CA:Star Bit] while holding this weapon, you will immediately fire a [c/E4559E:Starblast] at your cursor" +
				"\nThe [c/E4559E:Starblast] counts as Minion damage" +
				"\nThe [c/E4559E:Starblast] has unique effects depending on the color of [c/71F1CA:Star Bit] acquired" +
				"\n[c/E45555:Red Star Bits] have higher base damage" +
				"\n[c/EC8B35:Orange Star Bits] inflict Burn for 4 seconds" +
				"\n[c/E4CE55:Yellow Star Bits] bind foes for 1 second" +
				"\n[c/35EC43:Green Star Bits] have a 30% chance to crit" +
				"\n[c/3596EC:Blue Star Bits] restore 10 Mana upon contact with an enemy" +
				"\n[c/9E35EC:Purple Star Bits] inflict Starblight for 4 seconds" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 50;           //The damage of your weapon
			Item.DamageType = DamageClass.Summon;          //Is your weapon a melee weapon?
			Item.width = 52;            //Weapon's texture's width
			Item.height = 52;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			//item.knockback = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Pink;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
		}

		public override bool AltFunctionUse(Player player)
		{
			return false;
		}

		public override bool CanUseItem(Player player)
		{
			return false;

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
			//player.GetModPlayer<WeaponPlayer>().seraphimHeld = 10;
			if (player.ownedProjectileCounts[ProjectileType<Starchild>()] < 1)
			{
				player.AddBuff(BuffType<StarchildBuff>(), 999);
				
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<Starchild>(), Item.damage, 4, player.whoAmI, 0f);


				Main.projectile[index].originalDamage = Item.damage;

			}
			base.HoldItem(player);
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			
			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<PrismaticCore>(), 1)
				.AddIngredient(ItemID.FallenStar, 15)
				.AddIngredient(ItemType<EssenceOfTheObservatory>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
