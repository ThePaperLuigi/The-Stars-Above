using Microsoft.Xna.Framework;
using StarsAbove.Buffs.DragaliaFound;
using StarsAbove.Items.Essences;
using StarsAbove.Mounts.DragaliaFound;
using StarsAbove.Projectiles.DragaliaFound;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Summon
{
    public class DragaliaFound : ModItem
	{
		public override void SetStaticDefaults()
		{
			

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 70;           //The damage of your weapon
			Item.DamageType = DamageClass.Summon;          //Is your weapon a melee weapon?
			Item.width = 38;            //Weapon's texture's width
			Item.height = 132;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 2;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.noMelee = true;
			Item.crit = 0;
			Item.noUseGraphic = true;
			Item.shoot = ProjectileType<Projectiles.PhantomInTheMirrorProjectile>();
			Item.shootSpeed = 3f;
			Item.value = Item.buyPrice(gold: 1);
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
			
			return base.CanUseItem(player);
		}


		internal static void Teleport(Player player)
		{
			
		}
		public override void HoldItem(Player player)
		{
			
			base.HoldItem(player);
		}

		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			player.GetModPlayer<StarsAbovePlayer>().WhiteFade = 20;
			Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<DragonArm>(), 0, 0, player.whoAmI);
			player.mount.SetMount(MountType<DragonshiftMount>(), player);
			player.AddBuff(BuffType<DragonshiftActiveBuff>(), 240);
			return true;
		}
		public override void AddRecipes()
		{
			
		}
	}
}
