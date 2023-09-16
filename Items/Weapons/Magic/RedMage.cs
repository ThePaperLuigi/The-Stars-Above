using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.RedMage;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles.RedMage;
using StarsAbove.Systems;
using StarsAbove.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Magic
{
    public class RedMage : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vermilion Riposte");

			/* Tooltip.SetDefault("" +
				"This weapon attacks after a short delay and varies between left and right click" +
				"\nLeft click: [c/4E01FF:Verfire] inflicts On Fire for 4 seconds on impact and increases [c/4E01FF:Black Mana] by 5" +
				"\nRight click: [c/FFE7BA:Verstone] heals 12 HP on a critical strike and increases [c/FFE7BA:White Mana] by 5" +
				"\nAfter attacking, there is a 50% chance to gain the buff [c/FF74EC:Dualcast] for 4 seconds" +
				"\n[c/FF74EC:Dualcast] removes attack delay and strengthens attacks" +
				"\nLeft click ([c/FF74EC:Dualcast]): [c/4E01FF:Verthunder] is always a critical strike and increases [c/4E01FF:Black Mana] by 6" +
				"\nRight click ([c/FF74EC:Dualcast]): [c/FFE7BA:Veraero] pierces foes and increases [c/FFE7BA:White Mana] by 6" +
				"\nOnce both [c/4E01FF:Black Mana] and [c/FFE7BA:White Mana] are at 50, press the Weapon Action Key to gain a unique buff for 20 seconds and 3 [c/C36BD5:Mana Stacks]" +
				"\nGain [c/4E01FF:Black Enchantment] or [c/FFE7BA:White Enchantment] depending on which is higher (Gain a random enchantment and 1 less [c/C36BD5:Mana Stack] when balanced)" +
				"\nEnchanted attacks are changed, cast instantly, and consumes [c/C36BD5:Mana Stacks] instead of Mana (Both left and right click use enchanted attacks)" +
				"\n[c/4E01FF:Black Enchantment] uses the following attacks in succession: [c/FF1515:Redoublement], [c/FF1515:Verflare], [c/FF1515:Scorch]" +
				"\n[c/FF1515:Redoublement] deals damage multiple times in a frontal cone; [c/FF1515:Verflare] casts a large AoE centered on your cursor" +
				"\n[c/FF1515:Scorch] deals 5x base damage and increases [c/4E01FF:Black Mana] by 11" +
				"\n[c/FFE7BA:White Enchantment] uses the following attacks in succession: [c/FF1515:Reprise], [c/FF1515:Verholy], [c/FF1515:Resolution]" +
				"\n[c/FF1515:Reprise] charges towards the cursor, granting Invincibility for 2 seconds; [c/FF1515:Verholy] casts a large AoE centered on your cursor" +
				"\n[c/FF1515:Resolution] pierces, heals 5 HP per foe struck, and increases [c/FFE7BA:White Mana] by 11" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;

		}

		public override void SetDefaults()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				Item.damage = 725;
			}
			else
			{
				Item.damage = 175;
			}
			          //The damage of your weapon
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 52;            //Weapon's texture's width
			Item.height = 52;           //Weapon's texture's height
			Item.useTime = 10;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 10;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			//item.knockback = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = 1;
			Item.shootSpeed = 14;
			Item.mana = 18;
		}
		int stabTimer;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.HasBuff(BuffType<RedMageCastDelay>()))
            {
				return false;
            }
			return true;

			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		public override void HoldItem(Player player)
		{
			float launchSpeed = 12f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			Vector2 perturbedSpeed = new Vector2(arrowVelocity.X, arrowVelocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
			if (player.HasBuff(BuffType<RedMageStabbing>()))
            {
				stabTimer++;
				if(stabTimer > 10)
                {
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<RedMageStab>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);
					stabTimer = 0;
                }
            }
			player.AddBuff(BuffType<RedMageHeldBuff>(), 10);
			if (player.ownedProjectileCounts[ProjectileType<RedMageFocus>()] < 1)
			{
				int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageFocus>(), 0, 0, player.whoAmI, 0f);
			}
			if (player.ownedProjectileCounts[ProjectileType<RedMageRapier>()] < 1)
			{
				//When charging the attack, show the rapier.
				//int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapier>(), 0, 0, player.whoAmI, 0f);
			}
			if(player.GetModPlayer<WeaponPlayer>().blackMana > 100)
            {	
				
				player.GetModPlayer<WeaponPlayer>().blackMana = 100;
			}
			if (player.GetModPlayer<WeaponPlayer>().whiteMana > 100)
			{
				player.GetModPlayer<WeaponPlayer>().whiteMana = 100;
			}
			if (player.GetModPlayer<WeaponPlayer>().blackMana < 0)
			{

				player.GetModPlayer<WeaponPlayer>().blackMana = 0;
			}
			if (player.GetModPlayer<WeaponPlayer>().whiteMana < 0)
			{
				player.GetModPlayer<WeaponPlayer>().whiteMana = 0;
			}
			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
			{
				

				if (player.GetModPlayer<WeaponPlayer>().blackMana >= 50 && player.GetModPlayer<WeaponPlayer>().whiteMana >= 50)
				{
					for (int d = 0; d < 28; d++)
					{
						Dust.NewDust(player.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-9, 9), Main.rand.NextFloat(-5, 5), 150, default(Color), 1.2f);
						Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Red, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.3f);

					}
					player.GetModPlayer<WeaponPlayer>().blackManaDrain = 50;
					player.GetModPlayer<WeaponPlayer>().whiteManaDrain = 50;
					if (player.GetModPlayer<WeaponPlayer>().blackMana > player.GetModPlayer<WeaponPlayer>().whiteMana)
                    {//Black Enchantment
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(241, 105, 255, 240), $"{Language.GetTextValue("Black Enchantment!")}", false, false);
						player.AddBuff(BuffType<BlackEnchantment>(), 1200);
						player.GetModPlayer<WeaponPlayer>().manaStack = 3;

					}
					if (player.GetModPlayer<WeaponPlayer>().whiteMana > player.GetModPlayer<WeaponPlayer>().blackMana)
					{//White Enchantment
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(241, 105, 255, 240), $"{Language.GetTextValue("White Enchantment!")}", false, false);
						player.AddBuff(BuffType<WhiteEnchantment>(), 1200);
						player.GetModPlayer<WeaponPlayer>().manaStack = 3;

					}
					if (player.GetModPlayer<WeaponPlayer>().whiteMana == player.GetModPlayer<WeaponPlayer>().blackMana)
					{//Random Enchantment + 1 less Mana Stack
						if(Main.rand.NextBool(1))
                        {
							Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
							CombatText.NewText(textPos, new Color(241, 105, 255, 240), $"{Language.GetTextValue("Black Enchantment!")}", false, false);
							player.AddBuff(BuffType<BlackEnchantment>(), 1200);
						}
						else
                        {
							Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
							CombatText.NewText(textPos, new Color(241, 105, 255, 240), $"{Language.GetTextValue("White Enchantment!")}", false, false);
							player.AddBuff(BuffType<WhiteEnchantment>(), 1200);
						}
						player.GetModPlayer<WeaponPlayer>().manaStack = 2;

					}
				}
				

				//int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld, Vector2.Zero, Mod.Find<ModProjectile>("AlucardSword").Type, 0, 0, player.whoAmI);
				//Main.projectile[index].originalDamage = 0;
			}
			//player.GetModPlayer<WeaponPlayer>().blackMana--;
			//player.GetModPlayer<WeaponPlayer>().whiteMana--;
			base.HoldItem(player);
		}
        public override bool? UseItem(Player player)
        {
			
			
			return base.UseItem(player);
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
			if (player.HasBuff(BuffType<WhiteEnchantment>()))
			{
				mult = 0;
			}
			if (player.HasBuff(BuffType<BlackEnchantment>()))
			{
				mult = 0;
			}

			base.ModifyManaCost(player, ref reduce, ref mult);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
		
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if(player.HasBuff(BuffType<WhiteEnchantment>()))
            {
				if(player.GetModPlayer<WeaponPlayer>().manaStack == 3)
                {//Reprise
					SoundEngine.PlaySound(StarsAboveAudio.SFX_Reprise, player.Center);
					player.AddBuff(BuffType<Invincibility>(), 120);
					Vector2 Lunge = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 12f;
					player.velocity += Lunge;
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, velocity.X, velocity.Y, ProjectileType<RedMageStab>(), damage, 0, player.whoAmI, 0f);

				}
				if (player.GetModPlayer<WeaponPlayer>().manaStack == 2)
				{//Verholy
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapierFast>(), 0, 0, player.whoAmI, 0f);

					SoundEngine.PlaySound(StarsAboveAudio.SFX_Verholy, player.Center);
					Projectile.NewProjectile(source, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y, 0, 0, ProjectileType<Verholy>(), damage, 0, player.whoAmI, 0f);
				}
				if (player.GetModPlayer<WeaponPlayer>().manaStack == 1)
				{//Resolution
					SoundEngine.PlaySound(StarsAboveAudio.SFX_Resolution, player.Center);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<ResolutionMagicCircle>(), damage, 0, player.whoAmI, 0f);
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<ResolutionMagicCircle2>(), 0, 0, player.whoAmI, 0f);

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapierFast>(), 0, 0, player.whoAmI, 0f);
					player.GetModPlayer<WeaponPlayer>().whiteMana += 11;
					player.ClearBuff(BuffType<WhiteEnchantment>());
				}
				player.AddBuff(BuffType<RedMageCastDelay>(), 80);
				player.GetModPlayer<WeaponPlayer>().manaStack--;
				return false;
			}
			if (player.HasBuff(BuffType<BlackEnchantment>()))
			{
				if (player.GetModPlayer<WeaponPlayer>().manaStack == 3)
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_Redoublement, player.Center);

					player.AddBuff(BuffType<RedMageStabbing>(), 55);
					//Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapierFast>(), 0, 0, player.whoAmI, 0f);

				}
				if (player.GetModPlayer<WeaponPlayer>().manaStack == 2)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapierFast>(), 0, 0, player.whoAmI, 0f);

					SoundEngine.PlaySound(StarsAboveAudio.SFX_Verflare, player.Center);
					Projectile.NewProjectile(source, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y, 0, 0, ProjectileType<Verflare>(), damage, 0, player.whoAmI, 0f);

				}
				if (player.GetModPlayer<WeaponPlayer>().manaStack == 1)
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_Scorch, player.Center);
					Projectile.NewProjectile(source, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X, player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y, 0, 0, ProjectileType<ScorchPrep>(), damage, 0, player.whoAmI, 0f);

					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapierFast>(), 0, 0, player.whoAmI, 0f);
					player.GetModPlayer<WeaponPlayer>().blackMana += 11;
					player.ClearBuff(BuffType<BlackEnchantment>());
				}
				player.AddBuff(BuffType<RedMageCastDelay>(), 80);
				player.GetModPlayer<WeaponPlayer>().manaStack--;
				return false;
			}

			if (player.altFunctionUse == 2)
			{//Right click

				if (player.HasBuff(BuffType<Dualcast>()))//Veraero
				{
					player.ClearBuff(BuffType<Dualcast>());
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapierFast>(), 0, 0, player.whoAmI, 0f);
					player.GetModPlayer<WeaponPlayer>().whiteMana += 5;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_RDMCast, player.Center);

					Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<Veraero>(), damage, 0, player.whoAmI, 0f);

					return false;
				}
				else
                {//Verstone
					player.AddBuff(BuffType<VerstoneCasting>(), 60);
					player.GetModPlayer<WeaponPlayer>().whiteMana += 5;
					//Spawn the rapier
					int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapier>(), damage, 0, player.whoAmI, 0f);
					if (Main.rand.NextBool(2))
					{
						player.AddBuff(BuffType<Dualcast>(), 240);
						for (int d = 0; d < 8; d++)
						{
							Dust.NewDust(player.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 1.2f); 
							Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Red, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.3f);

						}
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(241, 105, 255, 240), $"{Language.GetTextValue("Dualcast!")}", false, false);
					}
					//Charge SFX
					SoundEngine.PlaySound(StarsAboveAudio.SFX_RDMCharge, player.Center);

					return false;
				}
				
			}
			else
			{
				if (player.HasBuff(BuffType<Dualcast>()))//Verthunder
				{
					player.ClearBuff(BuffType<Dualcast>());
					player.GetModPlayer<WeaponPlayer>().blackMana += 6;
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapierFast>(), 0, 0, player.whoAmI, 0f);

					SoundEngine.PlaySound(StarsAboveAudio.SFX_RDMCast, player.Center);
					Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<Verthunder>(), damage, 0, player.whoAmI, 0f);
					return false;
				}
				else
				{//Verfire
				 //Spawn the rapier
					int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RedMageRapier>(), damage, 0, player.whoAmI, 0f);
					player.AddBuff(BuffType<VerfireCasting>(), 60);
					player.GetModPlayer<WeaponPlayer>().blackMana += 5;
					if(Main.rand.NextBool(2))
                    {
						player.AddBuff(BuffType<Dualcast>(), 240);
						for (int d = 0; d < 8; d++)
						{
							Dust.NewDust(player.Center, 0, 0, DustID.LifeDrain, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 1.2f);
							Dust.NewDust(player.Center, 0, 0, DustID.FireworkFountain_Red, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.3f);

						}
						Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
						CombatText.NewText(textPos, new Color(241, 105, 255, 240), $"{Language.GetTextValue("Dualcast!")}", false, false);
					}
					//Charge SFX
					SoundEngine.PlaySound(StarsAboveAudio.SFX_RDMCharge, player.Center);
					return false;
				}
				
			}
			return false;
		}

		public override void AddRecipes()
		{
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{

				CreateRecipe(1)
					.AddIngredient(calamityMod.Find<ModItem>("UeliaceBar").Type, 10)
					.AddIngredient(ItemType<PrismaticCore>(), 5)
					.AddIngredient(ItemID.SoulofNight, 15)
					.AddIngredient(ItemID.SoulofLight, 15)
					.AddIngredient(ItemID.DarkShard, 1)
					.AddIngredient(ItemID.LightShard, 1)
					.AddIngredient(ItemID.Ruby, 7)
					.AddIngredient(ItemID.ManaCrystal, 3)
					.AddIngredient(ItemType<EssenceOfBalance>())
					.AddTile(TileID.Anvils)
					.Register();
			}
			else
			{


				CreateRecipe(1)
				.AddIngredient(ItemType<PrismaticCore>(), 5)
				.AddIngredient(ItemID.SoulofNight, 15)
				.AddIngredient(ItemID.SoulofLight, 15)
				.AddIngredient(ItemID.DarkShard, 1)
				.AddIngredient(ItemID.LightShard, 1)
				.AddIngredient(ItemID.Ruby, 7)
				.AddIngredient(ItemID.ManaCrystal, 3)
				.AddIngredient(ItemType<EssenceOfBalance>())
				.AddTile(TileID.Anvils)
				.Register();
			}
			
		}
	}
}
