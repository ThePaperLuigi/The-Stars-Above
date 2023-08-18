using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using StarsAbove.Projectiles.KeyOfTheKingsLaw;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Summon
{
    public class KeyOfTheKingsLaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Key of the King's Law");
			/* Tooltip.SetDefault("22 summon tag damage" +
                "\nRapidly summon powerful blades towards your cursor" +
                "\nWhen a minion deals tag damage, additionally inflict a random debuff on the foe for 2 seconds" +
				"\nUsing this weapon grants a random combat-oriented buff for 2 seconds" +
				"\n'I see your luck has run out, mongrel!'" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 129;           //The damage of your weapon
			Item.DamageType = DamageClass.Summon;          //Is your weapon a melee weapon?
			Item.width = 40;            //Weapon's texture's width
			Item.height = 40;           //Weapon's texture's height
			Item.useTime = 15;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 15;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<KeyOfTheKingsLawProjectile>();
			Item.shootSpeed = 15f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		int randomBuff;
		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				//Emit dusts when swing the sword
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GemTopaz);
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}

        public override bool? UseItem(Player player)
        {
			

			return base.UseItem(player);
        }

        // Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
        // See Source code for Star Wrath projectile to see how it passes through tiles.
        /*	The following changes to SetDefaults */

        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float behindTarget = target.Y + 40;
			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 101) * player.direction), 0f);
				position.Y -= (0 * i);
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
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y - 200, velocity.X, velocity.Y,type, damage * 2, knockback, player.whoAmI, 0f);
			}
			return false;*/
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), 50f);
				position.Y -= (100 * i);
				Vector2 heading = target - position;
				heading.Normalize();
				heading *= new Vector2(velocity.X, velocity.Y).Length();
				velocity.X = heading.X;
				velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,type, damage, knockback, player.whoAmI, 0f);
			}
			randomBuff = Main.rand.Next(0, 15);
			if (randomBuff == 0)
			{
				player.AddBuff(BuffID.Endurance, 120);
			}
			if (randomBuff == 1)
			{
				player.AddBuff(BuffID.Hunter, 120);
			}
			if (randomBuff == 2)
			{
				player.AddBuff(BuffID.Ironskin, 120);
			}
			if (randomBuff == 3)
			{
				player.AddBuff(BuffID.Lifeforce, 120);
			}
			if (randomBuff == 4)
			{
				player.AddBuff(BuffID.Inferno, 120);
			}
			if (randomBuff == 5)
			{
				player.AddBuff(BuffID.Invisibility, 120);
			}
			if (randomBuff == 6)
			{
				player.AddBuff(BuffID.Rage, 120);
			}
			if (randomBuff == 7)
			{
				player.AddBuff(BuffID.Regeneration, 120);
			}
			if (randomBuff == 8)
			{
				player.AddBuff(BuffID.ManaRegeneration, 120);
			}
			if (randomBuff == 9)
			{
				player.AddBuff(BuffID.Wrath, 120);
			}
			if (randomBuff == 10)
			{
				player.AddBuff(BuffID.Titan, 120);
			}
			if (randomBuff == 11)
			{
				player.AddBuff(BuffID.Thorns, 120);
			}
			if (randomBuff == 12)
			{
				player.AddBuff(BuffID.Panic, 120);
			}
			if (randomBuff == 13)
			{
				player.AddBuff(BuffID.IceBarrier, 120);
			}
			if (randomBuff == 14)
			{
				player.AddBuff(BuffID.Swiftness, 120);
			}
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<DullTotemOfLight>())
				.AddIngredient(ItemType<EssenceOfTheTreasury>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}

	
}
