using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Projectiles.Other.SoliloquyOfSovereignSeas;
using StarsAbove.Mounts.DragaliaFound;
using StarsAbove.Projectiles.Extra;
using StarsAbove.Projectiles.Summon.DragaliaFound;
using System;
using StarsAbove.Items.Prisms;
using StarsAbove.Buffs.Other.SoliloquyOfSovereignSeas;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Other
{
    public class SoliloquyOfSovereignSeas : ModItem
	{
		public override void SetStaticDefaults() {
			

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.DamageType = ModContent.GetInstance<ArkheDamageClass>();
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Orange;
			Item.autoReuse = true;
			Item.shoot = 10;
			Item.shootSpeed = 10f;
			Item.value = Item.buyPrice(gold: 1);
			Item.noUseGraphic = true;
		}
		int curse = 0;
		
        public override bool AltFunctionUse(Player player)
        {
			if (player.ownedProjectileCounts[ProjectileType<SoliloquyJellySummon>()] <= 0 && player.ownedProjectileCounts[ProjectileType<SoliloquySinger>()] <= 0)
			{
				return true;
			}

			return base.AltFunctionUse(player);
        }
        public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            float percentage = (float)((player.statLife/player.statLifeMax2)-0.6);
            damage.Flat += MathHelper.Lerp(0, 30, percentage);

            base.ModifyWeaponDamage(player, ref damage);
        }
        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                
                if (StarsAbove.weaponActionKey.JustPressed)
                {
                    SoundEngine.PlaySound(SoundID.Item30, player.Center);
                    if (player.GetModPlayer<WeaponPlayer>().ousiaAligned)
					{
                        player.GetModPlayer<WeaponPlayer>().ousiaAligned = false;
                        int dustEffect = DustID.GemTopaz;
                        for (int d = 0; d < 20; d++)
                        {
                            Dust.NewDust(player.MountedCenter, 0, 0, dustEffect, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
                        }
                        float dustAmount = 120f;
                        for (int i = 0; (float)i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                            spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                            int dust = Dust.NewDust(player.MountedCenter, 0, 0, dustEffect);
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = player.MountedCenter + spinningpoint5;
                            Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 10f;
                        }
                    }
					else
					{
                        player.GetModPlayer<WeaponPlayer>().ousiaAligned = true;
                        int dustEffect = DustID.GemAmethyst;
                        for (int d = 0; d < 20; d++)
                        {
                            Dust.NewDust(player.MountedCenter, 0, 0, dustEffect, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
                        }
                        float dustAmount = 120f;
                        for (int i = 0; (float)i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                            spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                            int dust = Dust.NewDust(player.MountedCenter, 0, 0, dustEffect);
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = player.MountedCenter + spinningpoint5;
                            Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 10f;
                        }
                    }
					
                }

            }
        }
		bool altSwing;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if(player.altFunctionUse == 2)
            {
                SoundEngine.PlaySound(SoundID.Item8, player.Center);

                for (int d = 0; d < 20; d++)
                {
                    Dust.NewDust(player.MountedCenter, 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
                }
                if (!player.GetModPlayer<WeaponPlayer>().ousiaAligned)
				{
                    //Pneuma
                    player.AddBuff(BuffType<SoliloquyMinionBuff>(), 2);
                    if (player.ownedProjectileCounts[ProjectileType<SoliloquySinger>()] <= 0)
                    {
                        player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<SoliloquySinger>(), damage, knockback);
                    }
                    int dustEffect = DustID.GemSapphire;
                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(Main.MouseWorld, 0, 0, dustEffect, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
                    }
                    float dustAmount = 120f;
                    for (int i = 0; (float)i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                        int dust = Dust.NewDust(Main.MouseWorld, 0, 0, dustEffect);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Main.MouseWorld + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 10f;
                    }
                }
				else
                {

                    int dustEffect = DustID.GemSapphire;
                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(Main.MouseWorld, 0, 0, dustEffect, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
                    }
                    float dustAmount = 120f;
                    for (int i = 0; (float)i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
                        int dust = Dust.NewDust(Main.MouseWorld, 0, 0, dustEffect);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Main.MouseWorld + spinningpoint5;
                        Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 10f;
                    }
                    player.AddBuff(BuffType<SoliloquyMinionBuff>(), 2);
                    if (player.ownedProjectileCounts[ProjectileType<SoliloquyJellySummon>()] <= 0)
                    {
                        player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<SoliloquyJellySummon>(), damage/3, knockback);

                        player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<SoliloquyCrabSummon>(), damage/3, knockback);
                        player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<SoliloquySeahorseSummon>(), damage/3, knockback);
                    }
                }
				
			}
            else
            {
                SoundEngine.PlaySound(SoundID.Item1, player.Center);

                if (player.GetModPlayer<WeaponPlayer>().ousiaAligned)
                {
                    int dustEffect = DustID.GemAmethyst;
                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(player.MountedCenter, 0, 0, dustEffect, 0f + Main.rand.Next(-1, 1), 0f + Main.rand.Next(-1, 1), 150, default(Color), 0.6f);
                    }
                }
                else
                {
                    int dustEffect = DustID.GemTopaz;
                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(player.MountedCenter, 0, 0, dustEffect, 0f + Main.rand.Next(-1, 1), 0f + Main.rand.Next(-1, 1), 150, default(Color), 0.6f);
                    }
                }
                if (altSwing)
                {
                    Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<SoliloquySword>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
                    altSwing = false;
                }
                else
                {
                    Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<SoliloquySword>(), damage, knockback, player.whoAmI, 0, 1, player.direction);
                    altSwing = true;
                }
            }
			
			
			

			return false;	
		}
		
		
		public override void AddRecipes()
		{
            CreateRecipe(1)
                .AddIngredient(ItemID.Sapphire, 10)
                .AddIngredient(ItemType<PrismaticCore>(), 12)
                .AddIngredient(ItemType<EssenceOfHydro>())
                .AddTile(TileID.Anvils)
                .Register();
        }
	}

}
