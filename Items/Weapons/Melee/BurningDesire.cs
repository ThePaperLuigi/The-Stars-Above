using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.BurningDesire;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.BurningDesire;
using StarsAbove.Systems;
using StarsAbove.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Melee
{
    public class BurningDesire : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				/* Tooltip.SetDefault("" +
				"Attacks stab in a rapid combo, striking multiple times and finishing with a downward swing" +
				"\nThe third strike only strikes once, but deals 20% bonus damage and grants a stack of [c/FFC84A:Power Strike]" +
				"\n[c/FFC84A:Power Strike] increases damage by 5% and defense by 5 per stack (Up to 5)" +
				"\nAt maximum stacks of [c/FFC84A:Power Strike], right click to consume all stacks, entering [c/FF3E00:Boiling Blood] for 15 seconds" +
				"\nDuring [c/FF3E00:Boiling Blood], gain 30% increased damage and 30 defense, but health regeneration is disabled and HP will slowly drain over time (Will not drop below 1)" +
				"\nAdditionally, gain attack speed based on missing HP, and attacks become empowered with explosive follow-up attacks" +
				"\nRight click during [c/FF3E00:Boiling Blood] to activate [c/DC0000:Boiling Burst], ending [c/FF3E00:Boiling Blood]" +
				"\n[c/DC0000:Boiling Burst] deals damage in a colossal area around you based on missing health and 1/4th damage dealt during [c/FF3E00:Boiling Blood]" +
				"\nAdditionally, become [c/DA9090:Extinguished] for 20 seconds, preventing the activation of [c/FF3E00:Boiling Blood]" +
                "\nDamage scales with world progression" +
				"\n'My blood's only going to make this place hotter!'" +
				$""); */ //With Calamity enabled, add an extra tooltip.
			}
			else
            {
				/* Tooltip.SetDefault("" +
				"Attacks stab in a rapid combo, striking multiple times and finishing with a downward swing" +
				"\nThe third strike only strikes once, but deals 20% bonus damage and grants a stack of [c/FFC84A:Power Strike]" +
				"\n[c/FFC84A:Power Strike] increases damage by 5% and defense by 5 per stack (Up to 5)" +
				"\nAt maximum stacks of [c/FFC84A:Power Strike], right click to consume all stacks, entering [c/FF3E00:Boiling Blood] for 15 seconds" +
				"\nDuring [c/FF3E00:Boiling Blood], gain 30% increased damage and 30 defense, but health regeneration is disabled and HP will slowly drain over time (Will not drop below 1)" +
				"\nAdditionally, gain attack speed based on missing HP, and attacks become empowered with explosive follow-up attacks" +
				"\nRight click during [c/FF3E00:Boiling Blood] to activate [c/DC0000:Boiling Burst], ending [c/FF3E00:Boiling Blood]" +
				"\n[c/DC0000:Boiling Burst] deals damage in a colossal area around you based on missing health and 1/4th damage dealt during [c/FF3E00:Boiling Blood]" +
				"\nAdditionally, become [c/DA9090:Extinguished] for 20 seconds, preventing the activation of [c/FF3E00:Boiling Blood]" +
				"\n'My blood's only going to make this place hotter!'" +
				$""); */
			}
			

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 77;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 35;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 35;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 3;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton

			Item.shoot = 337;
			Item.shootSpeed = 4f;

			Item.noUseGraphic = true;
			Item.noMelee = true;
			
		}

		int currentSwing;
		int slashDuration;
		int comboTimer;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override void UpdateInventory(Player player)
        {

			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				if ((bool)calamityMod.Call("GetBossDowned", "providence"))
				{
					Item.damage = 222;
				}
				if ((bool)calamityMod.Call("GetBossDowned", "allsentinel"))
				{
					Item.damage = 592;
				}
				if ((bool)calamityMod.Call("GetBossDowned", "devourerofgods"))
				{

					Item.damage = 890;
					
				}
				if ((bool)calamityMod.Call("GetBossDowned", "yharon"))
				{
					Item.damage = 1333;
				}
				if ((bool)calamityMod.Call("GetBossDowned", "supremecalamitas"))
				{
					Item.damage = 3100;
				}
			}
			base.UpdateInventory(player);
        }
        public override bool CanUseItem(Player player)
		{
			if (player.HasBuff(BuffType<ComboCooldown>()))
			{
				return false;
			}

			if (player.altFunctionUse == 2)
			{
				if ((player.GetModPlayer<WeaponPlayer>().powerStrikeStacks == 5 && !player.HasBuff(BuffType<BoilingBloodCooldown>()))|| player.HasBuff(BuffType<BoilingBloodBuff>()))
				{
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
				return base.CanUseItem(player);
		}
        public override void HoldItem(Player player)
        {
			player.GetModPlayer<WeaponPlayer>().BurningDesireHeld = true;
			if (player.ownedProjectileCounts[ProjectileType<BurningDesireHeld>()] < 1)
			{//Equip animation.
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<BurningDesireHeld>(), 0, 0, player.whoAmI, 0f);
			}
			if(player.GetModPlayer<WeaponPlayer>().powerStrikeStacks > 0)
            {
				player.AddBuff(BuffType<PowerStrikeBuff>(), 2);
            }
			if(player.GetModPlayer<WeaponPlayer>().powerStrikeStacks > 5)
            {
				player.GetModPlayer<WeaponPlayer>().powerStrikeStacks = 5;

			}
			if (currentSwing != 0)
			{
				comboTimer--;
			}
			if (comboTimer <= 0)
			{
				currentSwing = 0;
			}
			//player center + half of player's height, have their feet burn during Boiling Blood
			if(player.HasBuff(BuffType<BoilingBloodBuff>()))
            {
				int dustIndex = Dust.NewDust(new Vector2(player.position.X, player.Center.Y + player.height / 2), player.width, 0, 6, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-6, -1), 100, default(Color), 1.3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				Dust.NewDust(new Vector2(player.position.X, player.Center.Y + player.height / 2), player.width, 0, 6, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-6, -1), 100, default(Color), 1f);
				Dust.NewDust(new Vector2(player.position.X, player.Center.Y + player.height / 2), player.width, 0, DustID.LifeDrain, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-6, -1), 100, default(Color), 1f);

				Main.dust[dustIndex].velocity *= 3f; Dust.NewDust(new Vector2(player.Center.X, player.Center.Y + player.height / 2), player.width, 0, DustID.Smoke, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-5, -1), 150, default(Color), 0.8f);

			}
		}

		public override bool? UseItem(Player player)
        {
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			if (player.altFunctionUse == 2 && !player.HasBuff(BuffType<BoilingBloodBuff>()))
			{
				modPlayer.powerStrikeStacks = 0;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_BoilingBloodStart, player.position);
				for (int d = 0; d < 14; d++)
				{
					int dustIndex = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 5f;
					dustIndex = Dust.NewDust(new Vector2(player.Center.X, player.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 3f;
					Dust.NewDust(player.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-9, 9), Main.rand.NextFloat(-5, 5), 150, default(Color), 1f);
					Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Red, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-5, 5), 150, default(Color), 1f);

				}
			}


			return base.UseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}

		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 130f;
			
			
			if (player.altFunctionUse == 2)
			{
				if(player.HasBuff(BuffType<BoilingBloodBuff>()))
                {
					
					SoundEngine.PlaySound(StarsAboveAudio.SFX_BoilingBloodEnd, player.Center);

					Projectile.NewProjectile(source, position.X, position.Y, 0, 0, ProjectileType<BoilingBurstPrep1>(),
						damage + player.GetModPlayer<WeaponPlayer>().boilingBloodDamage + (int)((player.statLifeMax2 - player.statLife) / player.statLifeMax2),
						0, player.whoAmI, 0f);
					Projectile.NewProjectile(source, position.X, position.Y, 0, 0, ProjectileType<BoilingBurstPrep2>(),0,0, player.whoAmI, 0f);
					player.AddBuff(BuffType<BoilingBloodCooldown>(), 1200);
					player.GetModPlayer<WeaponPlayer>().boilingBloodDamage = 0;
					player.ClearBuff(BuffType<BoilingBloodBuff>());
					return false;
				}
				else
                {
					player.AddBuff(BuffType<BoilingBloodBuff>(), 900);
					return false;

				}
			}
			else
			{


				if (currentSwing == 0)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
					velocity.X = perturbedSpeed.X;
					velocity.Y = perturbedSpeed.Y;
					Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<BurningDesireStab>(), (int)(damage * 0.3), knockback, player.whoAmI);
					if(player.HasBuff(BuffType<BoilingBloodBuff>()))
                    {
						position += muzzleOffset;
						//Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<ChainsawFollowUp>(), damage, knockback, player.whoAmI);

					}
					SoundEngine.PlaySound(StarsAboveAudio.SFX_BlazeAttack, player.position);
					comboTimer = 60;
					currentSwing++;
					return false;
				}
				if (currentSwing == 1)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
					velocity.X = perturbedSpeed.X;
					velocity.Y = perturbedSpeed.Y;
					Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<BurningDesireStab>(), (int)(damage * 0.3), knockback, player.whoAmI);
					if (player.HasBuff(BuffType<BoilingBloodBuff>()))
					{
						position += muzzleOffset;
						//Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<ChainsawFollowUp>(), damage, knockback, player.whoAmI);

					}
					SoundEngine.PlaySound(StarsAboveAudio.SFX_BlazeAttack, player.position);

					comboTimer = 60;
					currentSwing++;
					return false;
				}
				if (currentSwing > 1)
				{
					if (player.direction == 1)
					{
						Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<BurningDesireSlash1>(), (int)(damage * 1.2), knockback, player.whoAmI, 0f);

					}
					else
                    {
						Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileType<BurningDesireSlash2>(), (int)(damage * 1.2), knockback, player.whoAmI, 0f);

					}

					SoundEngine.PlaySound(StarsAboveAudio.SFX_BlazeAttack, player.position);
					if (player.HasBuff(BuffType<BoilingBloodBuff>()))
                    {
						
					}
					else
                    {
						player.GetModPlayer<WeaponPlayer>().powerStrikeStacks++;
					}
					
					player.AddBuff(BuffType<ComboCooldown>(), 50);
					currentSwing = 0;
					return false;
				}
				return true;
			}
			return false;
		}

		public override void AddRecipes()
		{
			
			CreateRecipe(1)
				.AddIngredient(ItemID.ButchersChainsaw, 1)
				.AddIngredient(ItemID.Chain, 3)
				.AddIngredient(ItemID.LavaBucket, 1)
				.AddIngredient(ItemID.LunarBar, 8)
				.AddIngredient(ItemID.FragmentSolar, 18)
				.AddIngredient(ItemType<EssenceOfTheOverwhelmingBlaze>())
				.AddTile(TileID.Anvils)
				.Register();
			
		}
	}
}
