using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Items.Essences;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Mercy;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Melee.Mercy;

namespace StarsAbove.Items.Weapons.Melee
{
    public class Mercy : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("" +
				"Attacks with this weapon will rapidly sweep in a short-ranged arc" +
				"\nRight click to unleash [c/DC3961:Edge of Anguish] (12 second cooldown)" +
				"\n[c/DC3961:Edge of Anguish] will target the nearest enemy to your cursor and charge towards them" +
				"\nGain Invincibility for the duration of the dash" +
				"\nStriking foes deals 3x base damage" +
				"\nUpon contact, inflict [c/FF3200:Infernal Hemorrhage] for 3 seconds, dealing powerful damage over time" +
				"\nAdditionally, deal 10% of the foe's missing HP as bonus true damage (Up to 2000)" +
				"\nCritical strikes with this weapon's basic attacks instantly cleanse the cooldown of [c/DC3961:Edge of Anguish]" +
				"\nAllies striking foes under the influence of [c/FF3200:Infernal Hemorrhage] with projectiles grants Rage for 8 seconds" +
                "\nAdditionally, these attacks deal 2% of the foe's missing HP as bonus true damage (Up to 250)" +
				"\n'From the depths of Noxian streets'" +
				"" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			Item.damage = 40;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;         //Is your weapon a melee weapon?
			Item.width = 108;            //Weapon's texture's width
			Item.height = 108;           //Weapon's texture's height
			Item.useTime = 18;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 18;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = 8;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 0f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int currentSwing;
		int slashDuration;
		 
		bool altSwing;
		bool hasTarget = false;
		Vector2 enemyTarget = Vector2.Zero;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
			
			float launchSpeed = 110f;
			float launchSpeed2 = 10f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * 26f;
			

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();

			if (player.altFunctionUse == 2)
			{
				if(!player.HasBuff(BuffType<Buffs.Mercy.EdgeOfAnguish>()) && !player.HasBuff(BuffType<Buffs.Mercy.EdgeOfAnguishCooldown>()) && hasTarget)
                {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<MercyStab>(), player.GetWeaponDamage(Item)*3, 3, player.whoAmI, 0f);
					player.AddBuff(BuffType<Invincibility>(), 120);
					player.AddBuff(BuffType<EdgeOfAnguish>(), 120);
					player.AddBuff(BuffType<EdgeOfAnguishCooldown>(), 720);

					Vector2 direction2 = Vector2.Normalize(enemyTarget - player.Center);
					Vector2 arrowVelocity2 = direction2 * 15f;
					player.velocity = arrowVelocity2;
					return true;
				}
				else
                {
					return false;
                }
				
			}
			else
            {
				
            }
			
			return true;
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{

			
		}
        public override bool? UseItem(Player player)
        {


			


			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			
			
			NPC closestnpc = null;
			float closestnpcDistance = 9999999;
			float closestnpcDistanceToPlayer = 9999999;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				float distance = Vector2.Distance(npc.Center, Main.MouseWorld);


				if (npc.active && npc.Distance(Main.MouseWorld) < closestnpcDistance && npc.CanBeChasedBy())
				{
					closestnpc = npc;
					closestnpcDistance = npc.Distance(Main.MouseWorld);
					closestnpcDistanceToPlayer = npc.Distance(player.Center);
				}




			}
			if (closestnpcDistance < 88f && !player.HasBuff(BuffType<Buffs.Mercy.EdgeOfAnguishCooldown>()))
			{
				

				for (int i = 0; i < 30; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 40);
					offset.Y += (float)(Math.Cos(angle) * 40);

					Dust d = Dust.NewDustPerfect(closestnpc.Center + offset, 235, Vector2.Zero, 200, default(Color), 0.3f);
					d.fadeIn = 0.1f;
					d.noGravity = true;
				}
				enemyTarget = closestnpc.Center;
				hasTarget = true;
			}
			else
            {
				enemyTarget = Vector2.Zero;
				hasTarget = false;
            }
			if (closestnpc == null)
			{
				player.ClearBuff(BuffType<EdgeOfAnguish>());
				hasTarget = false;
			}
			
			
			if (player.HasBuff(BuffType<EdgeOfAnguish>()) && closestnpcDistanceToPlayer > 88f)
			{

				for (int d = 0; d < 3; d++)
				{
					Dust.NewDust(player.Center, 0, 0, 235, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 1.2f);

				}

				closestnpc.AddBuff(BuffType<Stun>(), 10);
				
			}
			
			if(player.HasBuff(BuffType<EdgeOfAnguish>()) && closestnpcDistanceToPlayer <= 88f)
            {
				
				player.ClearBuff(BuffType<EdgeOfAnguish>());
            }

			base.HoldItem(player);
        }

       
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 
			if(player.altFunctionUse != 2)
            {
				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MercySlash2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MercySlash>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MercySlash>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<MercySlash2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

						altSwing = true;
					}
				}
			}
			
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 15)
				.AddIngredient(ItemID.BloodMoonStarter, 1)
				.AddIngredient(ItemType<EssenceOfBlood>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
