using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Essences;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Other.Hawkmoon;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Other.Wolvesbane;
using StarsAbove.Buffs.Other.Wolvesbane;
using Mono.Cecil;
using StarsAbove.Projectiles.Melee.RebellionBloodArthur;
using StarsAbove.Projectiles.Ranged.KissOfDeath;
using StarsAbove.Projectiles.Other.Phasmasaber;
using StarsAbove.Buffs.Other.Phasmasaber;
using StarsAbove.Buffs.Boss;
using StarsAbove.Buffs.Magic.ParadiseLost;
using StarsAbove.Projectiles.Magic.ParadiseLost;

namespace StarsAbove.Items.Weapons.Other
{
    public class Phasmasaber : ModItem
	{
		public override void SetStaticDefaults() {
			

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 60;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 58;
			Item.height = 36;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;
			Item.shootSpeed = 36f;
			Item.crit = 10;
			Item.shoot = ProjectileType<PhasmasaberRangedSlash>();
			//item.UseSound = SoundID.Item11;
			Item.scale = 0.7f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
		}
		int rounds;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{

			return true;
        }
		public override void HoldItem(Player player)
        {
            player.GetModPlayer<WeaponPlayer>().phasmasaberHeld = true;
			if (player.HasBuff(BuffType<SpectralIllusionBuff>()))
            {
                Item.channel = true;

                Item.DamageType = ModContent.GetInstance<ChionicDamageClass>();
			}
			else
			{
				if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
                {
                    Item.channel = true;

                    Item.DamageType = DamageClass.Melee;


                }
                else if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
                {
                    Item.channel = false;

                    Item.DamageType = DamageClass.Ranged;

                }
                else
				{
                    Item.DamageType = DamageClass.Melee;

                }
            }
            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
            {
                if (player.HasBuff(BuffType<SpectralIllusionBuff>()))
                {
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, player.Center);

                    for (int d = 0; d < 40; d++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.Electric, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);
                        Dust.NewDust(player.Center, 0, 0, DustID.GemSapphire, Main.rand.NextFloat(-15, 15), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);

                    }
                    player.ClearBuff(BuffType<SpectralIllusionBuff>());

                    if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
                    {
                        Projectile.NewProjectile(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ProjectileType<PhasmasaberMeleeExplosion>(), player.GetWeaponDamage(Item) * 4, 0, player.whoAmI);

                    }
                    else if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
                    {
                        Projectile.NewProjectile(player.GetSource_FromThis(), new Vector2(Main.MouseWorld.X - 1200, Main.MouseWorld.Y), new Vector2(80, 0), ProjectileType<PhasmasaberHorizontalSlash>(), player.GetWeaponDamage(Item) * 8, 0, player.whoAmI);

                    }

                }
            }
        }
        public override bool? UseItem(Player player)
        {
			if(player.altFunctionUse == 2)
			{
				if(!player.HasBuff(BuffType<SpectralIllusionCooldown>()))
				{
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, player.Center);

                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.Electric, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);
                        Dust.NewDust(player.Center, 0, 0, DustID.GemSapphire, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.7f);

                    }
                    player.AddBuff(BuffType<SpectralIllusionBuff>(), 60 * 12);
                    player.AddBuff(BuffType<SpectralIllusionCooldown>(), 60 * 60);

                }
            }


            return base.UseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
                
			}
			else
			{				
				if(player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
				{
                    if (player.channel)
                    {
                        if (player.ownedProjectileCounts[ProjectileType<PhasmasaberMeleeOrbital>()] < 1)
                        {
                            Projectile.NewProjectile(source, position, Vector2.Zero, ProjectileType<PhasmasaberMeleeOrbital>(), damage * 3, knockback, player.whoAmI);
                        }
                    }
                }
				else if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
				{
                    if (player.HasBuff(BuffType<SpectralIllusionBuff>()))
                    {
                        if (player.ownedProjectileCounts[ProjectileType<PhasmasaberRangedOrbital>()] < 1)
                        {
                            Projectile.NewProjectile(source, position, Vector2.Zero, ProjectileType<PhasmasaberRangedOrbital>(), damage * 3, knockback, player.whoAmI);
                        }
                    }
					else
                    {
                        SoundEngine.PlaySound(SoundID.Item1, player.Center);

                        Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<PhasmasaberRangedSlash>(), damage, knockback, player.whoAmI, 0, 0, player.direction);
                        float numberProjectiles = 3;
                        float rotation = MathHelper.ToRadians(5);
                        position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 15f;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<PhasmasaberRangedCrystal>(), damage / 3, knockback, player.whoAmI);
                        }
                        //Swing the weapon, shooting piercing crystals
                    }
                }
            }

            return false;



		}

		public override void AddRecipes()
		{
            CreateRecipe(1)
                  .AddIngredient(ItemID.HallowedBar, 8)
                                    .AddIngredient(ItemID.GlowingMushroom,12)

                                                      .AddIngredient(ItemID.SoulofNight, 2)

                                                                        .AddIngredient(ItemID.CrystalShard, 12)

                  .AddIngredient(ItemType<EssenceOfChionicEnergy>())
                  .AddTile(TileID.Anvils)
                  .Register();
        }
	}
}
