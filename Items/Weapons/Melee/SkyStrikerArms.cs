using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Melee.SkyStrikerBuffs;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.Melee.SkyStriker;
using StarsAbove.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Melee
{
    public class SkyStrikerArms : ModItem
	{
		
		
	
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Armaments of the Sky Striker");

			/* Tooltip.SetDefault("" +
				"[c/F592BF:This weapon is unaffected by Aspected Damage Type penalty]" +
				"\nRight click to access the [c/90B1BE:Aerial Forme] Selection Menu" +
				"\nEach [c/90B1BE:Aerial Forme] grants unique buffs when this weapon is held and modifies weapon attacks" +
				"\n[c/EC5656:Annihilation]: Damage is increased by 50%, but defenses are reduced by 30" +
				"\nAttacks are changed into fast swings that inflict On Fire for 8 seconds" +
				"\nCritical hits deal increased damage to targets inflicted with On Fire as well as expelling the debuff" +
				"\n[c/56D1EC:Defender]: Gain 20 defense, enemies are more likely to target you, and 80% of damage taken is reflected to attackers" +
				"\nAttacks are changed into a held ability that raises a shield in front of you, destroying nearby projectiles (2 second cooldown)" +//Held shield projectile. Periodically spawn a big circle AOE that does damage.
				"\nAdditionally, all allies in the radius gain 10 defense while the shield is active" +
				"\n[c/ECCA56:Close Combat]: Gain Wrath for 2 seconds when attacking foes below 50% HP" +
				"\nAttacks are changed into a barrage of quick close-ranged strikes that deal 1.2x base damage" +
				"\nCritical strikes launch a claw that pierces enemies (8 second cooldown)" +
				"\n[c/66EC56:Railgunner]: Critical strike chance is increased by 20%" +
				"\nAttacks are changed into long range piercing bolts, dealing 1.5x base damage" +
				"\nCritical strikes deal 4x damage" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 70;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton`
			Item.channel = false;
			Item.shoot = 116;
			Item.shootSpeed = 30f;
			Item.noUseGraphic = true;
			
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override void HoldItem(Player player)
        {
			player.GetModPlayer<WeaponPlayer>().SkyStrikerHeld = true;
			if(player.GetModPlayer<WeaponPlayer>().SkyStrikerForm == 1) //Red Firey attacks
            {

				Item.useTime = 30;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 30;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
				Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
				Item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
				Item.channel = false;
				Item.shoot = ProjectileType<SkyStrikerSwing1>();
				Item.shootSpeed = 120f;
				Item.UseSound = SoundID.Item1;
				


				player.AddBuff(BuffType<StrikerAttackBuff>(), 2);
            }
			if (player.GetModPlayer<WeaponPlayer>().SkyStrikerForm == 2)//Blue Shield
			{

				Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
				Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
				Item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
				Item.channel = true;
				Item.shoot = ProjectileType<SkyStrikerShield>();
				Item.shootSpeed = 14f;
				player.AddBuff(BuffType<StrikerShieldBuff>(), 2);
				Item.UseSound = SoundID.Item1;
				player.GetModPlayer<WeaponPlayer>().SkyStrikerCombo = 0;

				

			}
			if (player.GetModPlayer<WeaponPlayer>().SkyStrikerForm == 3)//Close Combat melee
			{

				Item.useTime = 12;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 12;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
				Item.useStyle = ItemUseStyleID.Rapier;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
				Item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
				Item.channel = false;
				Item.shoot = ProjectileType<SkyStrikerMeleeClaw>();
				Item.shootSpeed = 3f;
				player.AddBuff(BuffType<StrikerMeleeBuff>(), 2);
				Item.UseSound = SoundID.Item1;
				player.GetModPlayer<WeaponPlayer>().SkyStrikerCombo = 0;

			}
			if (player.GetModPlayer<WeaponPlayer>().SkyStrikerForm == 4)//Railgun
			{

				Item.useTime = 75;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
				Item.useAnimation = 75;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
				Item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
				Item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
				Item.channel = false;
				Item.shoot = ProjectileType<SkyStrikerRailgunRound>();
				Item.shootSpeed = 34f;
				player.AddBuff(BuffType<StrikerSniperBuff>(), 2);
				Item.UseSound = SoundID.Item125;
				player.GetModPlayer<WeaponPlayer>().SkyStrikerCombo = 0;

			}



			base.HoldItem(player);
        }
        public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{

				if (!player.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible)
				{
					player.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible = true;

				}
				
				return false;
			}
			else
			{
				

			}
			if(player.GetModPlayer<WeaponPlayer>().SkyStrikerForm == 0)
            {
				return false;
            }
			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float launchSpeed = 120f;
			float launchSpeed2 = 34f;

			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 MeleeSwing = direction * launchSpeed;
			Vector2 Railgun = direction * launchSpeed2;


			if (player.GetModPlayer<WeaponPlayer>().SkyStrikerForm == 1) //Attacking
			{

				if (player.GetModPlayer<WeaponPlayer>().SkyStrikerCombo == 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, MeleeSwing.X, MeleeSwing.Y,ProjectileType<SkyStrikerSwing2>(), damage, knockback, player.whoAmI);
					player.GetModPlayer<WeaponPlayer>().SkyStrikerCombo = 0;
					return false;

				}
				if (player.GetModPlayer<WeaponPlayer>().SkyStrikerCombo == 0)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, MeleeSwing.X, MeleeSwing.Y,ProjectileType<SkyStrikerSwing1>(), damage, knockback, player.whoAmI);
					player.GetModPlayer<WeaponPlayer>().SkyStrikerCombo++;
					return false;
				}
				


				
			}


			if (player.GetModPlayer<WeaponPlayer>().SkyStrikerForm == 2) //Shield
			{
				
				return true;
			}
			if (player.GetModPlayer<WeaponPlayer>().SkyStrikerForm == 3) //Punches
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
				velocity.X = perturbedSpeed.X;
				velocity.Y = perturbedSpeed.Y;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<SkyStrikerMeleeClaw>(), (int)(damage * 1.2), knockback, player.whoAmI);

				return false;
			}
			if (player.GetModPlayer<WeaponPlayer>().SkyStrikerForm == 4) //Gun
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, Railgun.X, Railgun.Y,ProjectileType<SkyStrikerRailgun>(), 0, knockback, player.whoAmI);
				Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
				if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
				{
					position += muzzleOffset;
				}
				
				Vector2 Lunge = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * -4f;
				player.velocity += Lunge;
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileType<SkyStrikerRailgunRound>(), damage*2, knockback, player.whoAmI);

				return false;
			}
			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Ruby, 3)
				.AddIngredient(ItemID.Sapphire, 3)
				.AddIngredient(ItemID.Topaz, 3)
				.AddIngredient(ItemID.Emerald, 3)
				.AddIngredient(ItemID.HallowedBar, 12)
				.AddIngredient(ItemID.SoulofFlight, 16)
				.AddIngredient(ItemType<EssenceOfTheAerialAce>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
