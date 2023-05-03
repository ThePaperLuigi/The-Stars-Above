using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.SupremeAuthority;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.EternalStar;
using StarsAbove.Projectiles.LevinstormAxe;
using StarsAbove.Projectiles.SupremeAuthority;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
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
                "\nRight click friendly NPCs to mark them as a [Sacrifice] for 5 minutes" +
                "\nPressing the Weapon Action Key will consume all [Sacrifices] and grant [Deified] for 60 seconds (Unable to mark or consume [Sacrifices] when [Deified])" +
                "\nWhile [Deified], maximum HP and damage taken are halved, and most debuffs are resisted" +
                "\nAdditionally, gain stacks of [Dark Aura] depending on the amount of [Sacrifices] consumed" +
                "\n[Deified] grants 10% increased attack speed and attacks grant up to two stacks of [Encroaching]" +
                "\nWith two stacks of [Encroaching], right click to unleash [Disappear], dealing bonus damage based on [Dark Aura]" +
                "\nAdditionally, [Disappear] deals 1% of the foe's Max HP in bonus damage, increased with [Dark Aura]" +
                "\nPresing the Weapon Action Key while [Deified] will extend the duration by 30 seconds, but will consume 50% of current HP (Can be activated multiple times)" +
                "\nAdditionally, you will be inflicted with [Atrophied Deity], preventing natural health regeneration" +
                "\nDying with [Atrophied Deity] will curse all allies with Potion Sickness for 2 minutes" +//[playername] returned to the void.
				"\nAdditionally, if [Deified] ends while a boss is active, inflict [Mortality] for 15 seconds, slowing movement speed and doubling damage recieved" +
				$"");  //The (English) text shown below your weapon's name

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 120;           //The damage of your weapon
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.mana = 0;
			Item.width = 40;            //Weapon's texture's width
			Item.height = 40;           //Weapon's texture's height
			Item.useTime = 20;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 20;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 0;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Purple;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.noUseGraphic = true;
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
				

			}
			return base.CanUseItem(player);
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			
		}
        public override void HoldItem(Player player)
        {
			if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
			{
				//Spawn the eye. After a short delay it consumes all marked NPCs and grants Deified. Logic goes there.
				if (!player.HasBuff(BuffType<DeifiedBuff>()))
				{
					if (player.ownedProjectileCounts[ProjectileType<SupremeAuthorityEye>()] < 1)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<SupremeAuthorityEye>(), 0, 0, player.whoAmI, 0f);


					}
				}

			}

			base.HoldItem(player);
        }

        public override bool? UseItem(Player player)
        {
			if(player.altFunctionUse == 2 && !player.HasBuff(BuffType<DeifiedBuff>()))
            {
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.active && npc.friendly && npc.townNPC && npc.Distance(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) < 30)
					{
						npc.AddBuff(BuffType<AuthoritySacrificeMark>(), 18000);
					}
				}
				float dustAmount = 24f;
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 14f);
					//spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
					int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = player.Center + spinningpoint5;
					Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 15f;
				}
			}
			return base.UseItem(player);
        }

		bool altSwing = false;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse != 2)
			{
				if (player.direction == 1)
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthoritySwing2>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthorityVFX2>(), 0, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthoritySwing1>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthorityVFX1>(), 0, knockback, player.whoAmI, 0f);

						altSwing = true;
					}

				}
				else
				{
					if (altSwing)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthoritySwing1>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthorityVFX1>(), 0, knockback, player.whoAmI, 0f);

						altSwing = false;
					}
					else
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthoritySwing2>(), damage, knockback, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AuthorityVFX2>(), 0, knockback, player.whoAmI, 0f);


						altSwing = true;
					}
				}

				return false;

			}
			else
			{
				return false;

			}
		}
		public override void AddRecipes()
		{
			
		}
	}

	
}
