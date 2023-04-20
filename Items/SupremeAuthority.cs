using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.EternalStar;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class SupremeAuthority : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Supreme Authority");
			Tooltip.SetDefault("" +
                "Attacks with this weapon swing in a close-ranged arc" +
                "\nRight click friendly NPCs to mark them as a [Sacrifice]" +
                "\nPressing the Weapon Action Key will consume all [Sacrifices] and grant [Deified] for 60 seconds (Unable to mark or consume [Sacrifices] when [Deified])" +
                "\nWhile [Deified], maximum HP and damage taken is halved while gaining immunity to most debuffs" +
                "\nAdditionally, gain stacks of [Dark Aura] depending on the amount of [Sacrifices] consumed" +
                "\n[Deified] grants 10% increased attack speed and attacks grant up to two stacks of [Encroaching]" +
                "\nWith two stacks of [Encroaching], right click to unleash [Disappear],  dealing bonus damage based on [Dark Aura]" +
                "\nAdditionally, [Disappear] deals 1% of the foe's Max HP in bonus damage, increased with [Dark Aura]" +
                "\nPresing the Weapon Action Key while [Deified] will extend the duration by 30 seconds, but will drain HP (Can be activated multiple times)" +
                "\nDying while [Deified] has been extended will curse the world, causing nearby to deal 20% more damage to all players for 10 minutes" +//[playername] returned to the void.
				"\nAdditionally, if [Deified] ends while a boss is active, inflict [Mortality], slowing movement speed and doubling damage recieved" +
				$"");  //The (English) text shown below your weapon's name

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
				if (!player.HasBuff(BuffType<Buffs.EternalStar.ImmemorialSupernova>()) && player.GetModPlayer<WeaponPlayer>().EternalGauge >= 100)
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
					player.AddBuff(BuffType<Buffs.EternalStar.ImmemorialSupernova>(), 300);
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

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
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
