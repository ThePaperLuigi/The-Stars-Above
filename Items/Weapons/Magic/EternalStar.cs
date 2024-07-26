using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.Magic.EternalStar;
using StarsAbove.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Magic
{
    public class EternalStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eternal Star");
			/* Tooltip.SetDefault("Holding this weapon causes the [c/C2A4AA:Eternal Gauge] to appear" +
                "\nConjure powerful random types of [c/FFB47A:Emblazoned Stars] from the sky to obliterate foes" +
				"\n[c/FFB47A:Emblazoned Stars] deal damage 5 times before shattering" +
				"\nShattered [c/FFB47A:Emblazoned Stars] have a chance to leave behind [c/FF6988:Emblazoned Fragments] based on the type of star" +
				"\nWhen picked up, these [c/FF6988:Emblazoned Fragments] grant powerful buffs for 8 seconds" +
				"\n[c/FFDC59:Citrine] grants 20 Defense and 5% increased damage" +
				"\n[c/FF59CD:Amethyst] grants Mana Regeneration and 5% increased damage" +
				"\n[c/59CFFF:Cerulean] grants 120 Mana when collected and 5% increased damage" +
				"\n[c/FF5959:Vermillion] grants 50 armor penetration and 5% increased damage" +
				"\n[c/59FF68:Malachite] grants 10% critical strike chance and 5% increased damage" +
				"\nAdditionally, collecting [c/FF6988:Emblazoned Fragments] charges the [c/C2A4AA:Eternal Gauge]" +
				"\nOnce the [c/C2A4AA:Eternal Gauge] is full, right click to conjure the [c/59FFCD:Immemorial Supernova] at your cursor's location, draining the [c/C2A4AA:Eternal Gauge]" +
				"\nThe [c/59FFCD:Immemorial Supernova] will persist for 5 seconds, dealing damage to all foes in its vicinity" +
				"\nOnce the [c/59FFCD:Immemorial Supernova] expires, it will explode, dealing 5x base damage damage to nearby foes" +
				$""); */  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 442;           //The damage of your weapon
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.mana = 90;
			Item.width = 40;            //Weapon's texture's width
			Item.height = 40;           //Weapon's texture's height
			Item.useTime = 60;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 60;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<EmblazonedCitrine>();
			Item.shootSpeed = 15f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		int randomBuff;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{

			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<Buffs.Magic.EternalStar.ImmemorialSupernova>()) && player.GetModPlayer<WeaponPlayer>().EternalGauge >= 100)
				{
					/*for (int d = 0; d < 10; d++)
					{
						Dust.NewDust(player.Center, 0, 0, 15, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 28; d++)
					{
						Dust.NewDust(player.Center, 0, 0, DustType<Dusts.bubble>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-15, 15), 0, default(Color), 1.5f);
					}*/
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("ImmemorialSupernovaProjectile").Type, player.GetWeaponDamage(Item), 0, player.whoAmI);
					player.GetModPlayer<WeaponPlayer>().EternalGauge = 0;
					player.AddBuff(BuffType<Buffs.Magic.EternalStar.ImmemorialSupernova>(), 300);
					return true;
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
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-18, -15);
		}
		public override bool? UseItem(Player player)
        {
			

			return base.UseItem(player);
        }

        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse != 2)
			{
				 
				Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
				float ceilingLimit = target.Y;
				type = Main.rand.Next(new int[] { ProjectileType<EmblazonedCerulean>(), ProjectileType<EmblazonedAmethyst>(), ProjectileType<EmblazonedCitrine>(), ProjectileType<EmblazonedVermillion>(), ProjectileType<EmblazonedMalachite>() });
				position = player.Center + new Vector2((-(float)Main.rand.Next(-100, 101) * player.direction), 50f);
				position.Y -= (700);
				Vector2 heading = target - position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}
				heading.Normalize();
				heading *= new Vector2(velocity.X, velocity.Y).Length();
				velocity.X = heading.X;
				velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, ceilingLimit);

			}
			
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.FragmentSolar, 15)
				.AddIngredient(ItemID.FragmentStardust, 15)
				.AddIngredient(ItemID.FragmentVortex, 15)
				.AddIngredient(ItemID.FragmentNebula, 15)
				.AddIngredient(ItemID.WandofSparking, 1)
				.AddIngredient(ItemType<EssenceOfEternity>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}

	
}
