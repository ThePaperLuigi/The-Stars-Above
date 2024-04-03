
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Summon.Takodachi;
using StarsAbove.Buffs.Summon.Takonomicon;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Summon
{
    public class AncientBook : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Takonomicon");
			/* Tooltip.SetDefault("Calls forth [c/B875F0:Void Octopi] to assail foes with homing lasers" +
				"\nEach [c/B875F0:Void Octopus] summoned increases damage of all minions and summon weapons by 1%" +
				"\nRight click to fire [c/E752B3:Eldritch Blast] towards your cursor for 3 seconds, melting foes in a line before you (1 minute cooldown)" +
				"\nWith at least one [c/B875F0:Void Octopus] summoned, the [c/5652E7:Abyssal Gauge] will appear" +
				"\nWhen [c/B875F0:Void Octopi] or [c/E752B3:Eldritch Blast] deals damage the [c/5652E7:Abyssal Gauge] will increase, even when this weapon is not held" +
				"\nWhen the [c/5652E7:Abyssal Gauge] is full, [c/AC49F5:Ancient One's Summoning] will activate when this weapon is held" +
				"\n[c/AC49F5:Ancient One's Summoning] will cause magical portals to surround the nearest foe" +
				"\nAfter a short delay, tentacles will be summoned to strike, inflicting Shadowflame for 7 seconds" +
				"\nFor each void octopi summoned, the amount of [c/AC49F5:Ancient One's Summoning]'s portals will increase and [c/E752B3:Eldritch Blast] does more damage" +
                "\nDamage scales with world progression" +
				"\n'We are happy. We are here. We are hype. We are...'"
				+ $""); */

			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


		}

		public override void SetDefaults()
		{
			Item.damage = 3;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.width = 21;
			Item.height = 21;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 6;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item44;
			Item.shoot = ProjectileType<TakodachiMinion>();
			Item.buffType = BuffType<TakodachiBuff>(); //The buff added to player after used the item
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            bool slimeKing = NPC.downedSlimeKing;
            bool eye = NPC.downedBoss1;
            bool evilboss = NPC.downedBoss2;
            bool queenBee = NPC.downedQueenBee;
            bool skeletron = NPC.downedBoss3;
            bool hardmode = Main.hardMode;
            bool anyMech = NPC.downedMechBossAny;
            bool allMechs = NPC.downedMechBoss3 && NPC.downedMechBoss2 && NPC.downedMechBoss1;
            bool plantera = NPC.downedPlantBoss;
            bool golem = NPC.downedGolemBoss;
            bool cultist = NPC.downedAncientCultist;
            bool moonLord = NPC.downedMoonlord;

            float damageMult = 1f +
                (slimeKing ? 0.1f : 0f) +
                (eye ? 0.12f : 0f) +
                (evilboss ? 0.14f : 0f) +
                (queenBee ? 0.36f : 0f) +
                (skeletron ? 0.58f : 0f) +
                (hardmode ? 1.2f : 0f) +
                (anyMech ? 1.23f : 0f) +
                (allMechs ? 1.3f : 0f) +
                (plantera ? 2.4f : 0f) +
                (golem ? 3.45f : 0f) +
                (cultist ? 4f : 0f) +
                (moonLord ? 5f : 0f);

            damage *= damageMult;
        }
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool? UseItem(Player player)
        {
			


			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
			//damage += 120 + player.statLifeMax2 / 20 + player.statManaMax2 / 20 + (Math.Max(Math.Max(Math.Max(player.meleeCrit, player.magicCrit), player.rangedCrit), player.thrownCrit));

			//player.GetModPlayer<WeaponPlayer>().takodachiGauge++;//Debug.
			Vector2 position = player.Center;

			float rotation = (float)Math.Atan2(position.Y - (player.GetModPlayer<WeaponPlayer>().takoTarget.Y), position.X - (player.GetModPlayer<WeaponPlayer>().takoTarget.X));//Aim towards mouse


			//If gauge is full, attack nearby foes with tentacles!
			//Player projOwner = Main.player[projectile.owner];

			NPC closest = null;
			float closestDistance = 9999999;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				float distance = Vector2.Distance(npc.Center, player.Center);


				if (npc.active && npc.Distance(player.position) < closestDistance)
				{
					closest = npc;
					closestDistance = npc.Distance(player.position);
				}




			}
			player.GetModPlayer<WeaponPlayer>().takoMinionTarget = closest.Center;
			if (player.GetModPlayer<WeaponPlayer>().takodachiGauge >= 100)
			{

				if (closest.CanBeChasedBy() && closestDistance < 1200f)//If the enemy is a reasonable distance away and is hostile (projectile.ai[0] is the amount of bounces left (Should start at 3.)
				{



					//closest.AddBuff(BuffType<Stun>(), 30);


				



					float launchSpeed = 1f;
					Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
					Vector2 direction = Vector2.Normalize(closest.position - player.Center);
					Vector2 velocity = direction * launchSpeed;
					Vector2 targetPosition = new Vector2(player.GetModPlayer<WeaponPlayer>().takoMinionTarget.X , player.GetModPlayer<WeaponPlayer>().takoMinionTarget.Y + Main.rand.Next(-70, 70));

					for (int l = 0; l < player.ownedProjectileCounts[Item.shoot]; l++)
					{
						
						
						int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), targetPosition.X + Main.rand.Next(-170, 170), targetPosition.Y + Main.rand.Next(-170, 170), velocity.X, velocity.Y, ProjectileType<TentacleCircle>(), (Item.damage), 2f, player.whoAmI);
						Main.projectile[index].originalDamage = (Item.damage);
					}
					SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, player.Center);

					


					player.GetModPlayer<WeaponPlayer>().takodachiGauge = 0;
				}
			}



			//float rotation = (float)Math.Atan2(player.Center.Y - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y), player.Center.X - (player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X));
			if (player.HasBuff(BuffType<TakodachiLaserBuff>()))
			{
				for (int d = 0; d < 2; d++)
				{
					float Speed2 = Main.rand.NextFloat(10, 70);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(7)); // 30 degree spread.
					int dustIndex = Dust.NewDust(player.Center, 0, 0, 134, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				for (int b = 0; b < 2; b++)
				{
					float Speed3 = Main.rand.NextFloat(8, 40);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(46)); // 30 degree spread.
					int dustIndex = Dust.NewDust(player.Center, 0, 0, 173, perturbedSpeed.X, perturbedSpeed.Y, 50, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
			}
			
		
		}

        public override void UpdateInventory(Player player)
        {
			
			
        }
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-3, 4);
		}

		public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffType<TakodachiLaserBuff>()) && !player.HasBuff(BuffType<TakodachiLaserBuffCooldown>()))
				{
					player.AddBuff(BuffType<TakodachiLaserBuff>(), 180);
					player.GetModPlayer<WeaponPlayer>().takoTarget = Main.MouseWorld;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, player.Center);
					
					
					int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center, Vector2.Zero, Mod.Find<ModProjectile>("TakonomiconLaser").Type, player.GetWeaponDamage(Item), 0, player.whoAmI);
					Main.projectile[index].originalDamage = player.GetWeaponDamage(Item);
					int index2 = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center, Vector2.Zero, Mod.Find<ModProjectile>("MagicCircle").Type, 0, 0, player.whoAmI);
					Main.projectile[index2].originalDamage = player.GetWeaponDamage(Item);
					player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;

					return false;
				}
				else
				{
					return false;
				}

			}
			return base.CanUseItem(player);
        }
       
        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{

			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}


		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			
			/*for (int l = 0; l < Main.projectile.Length; l++)
			{
				Projectile proj = Main.projectile[l];
				if (proj.active && proj.type == item.shoot && proj.owner == player.whoAmI)
				{
					proj.active = false;
				}
			}*/

			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
			

			player.SpawnMinionOnCursor(source, player.whoAmI, type, damage, knockback);
			return false;

		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.BlackInk, 3)
				.AddIngredient(ItemID.Amethyst, 12)
				.AddIngredient(ItemID.Book, 1)
				.AddIngredient(ItemID.SuspiciousLookingEye, 1)
				.AddIngredient(ItemType<EssenceOfOuterGods>())
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}
