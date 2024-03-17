using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using Terraria.GameContent.Creative;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Ranged.ForceOfNature;
using StarsAbove.Projectiles.Summon.StarphoenixFunnel;
using StarsAbove.Utilities;
using Terraria.Audio;
using StarsAbove.Buffs.Summon.StarphoenixFunnel;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Summon
{
    public class StarphoenixFunnel : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Force-of-Nature");

			

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.DamageType = DamageClass.Summon;
			Item.width = 40;
			Item.height = 20;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.rare = ItemRarityID.Yellow;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<StarphoenixRound>();
			Item.shootSpeed = 80f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.UseSound = SoundID.Item11;
			Item.noUseGraphic = true;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void HoldItem(Player player)
		{
            
            if (player.HasBuff(BuffType<AlignmentBuff>()))
            {
				player.GetModPlayer<WeaponPlayer>().alignmentStacks = 0;
            }
            switch (player.GetModPlayer<WeaponPlayer>().alignmentStacks)
			{
				case 0:

					break;
				case 1:
					player.AddBuff(BuffType<AlignmentStack1>(), 2);
					break;
				case 2:
                    player.AddBuff(BuffType<AlignmentStack2>(), 2);

                    break;
				case 3:
                    player.AddBuff(BuffType<AlignmentStack3>(), 2);

                    break;

				case 4:
                    player.AddBuff(BuffType<AlignmentStack4>(), 2);

                    break;

				case 5:
                    player.AddBuff(BuffType<AlignmentStack5>(), 2);

                    break;

			}

            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
            {
                if (!player.HasBuff(BuffType<AlignmentBuff>()))
                {
                    if (player.GetModPlayer<WeaponPlayer>().alignmentStacks >= 5)
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, Main.MouseWorld);

                        player.GetModPlayer<WeaponPlayer>().alignmentStacks = 0;
                        player.AddBuff(BuffType<AlignmentBuff>(), 60 * 4);
                        for (int d = 0; d < 38; d++)
                        {
                            Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-24, 24), 0f + Main.rand.Next(-24, 24), 150, default(Color), 1.2f);
                        }
                        for (int d = 0; d < 32; d++)
                        {
                            Dust.NewDust(player.Center, 0, 0, DustID.GemAmethyst, 0f + Main.rand.Next(-17, 17), 0f + Main.rand.Next(-17, 17), 150, default(Color), 0.8f);
                        }
                        player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
                    }
                    else
                    {
                        
                    }
                }

            }

        }
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				
				return true;

			}

			return true;

			
		}

		

		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 5);
		}

		// How can I make the shots appear out of the muzzle exactly?
		// Also, when I do this, how do I prevent shooting through tiles?

			
		public override bool? UseItem(Player player)
		{
			

			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (player.altFunctionUse == 2)
			{
                player.AddBuff(BuffType<CatalystKeyBuff>(), 2);
                player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<CatalystKey>(), damage, knockback);
                SoundEngine.PlaySound(SoundID.Item15, Main.MouseWorld);

                for (int d = 0; d < 28; d++)
                {
                    Dust.NewDust(Main.MouseWorld, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default, 0.8f);
                    Dust.NewDust(Main.MouseWorld, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default, 0.4f);

                }
                return false;


			}
			else
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, 0, 0, ProjectileType<StarphoenixGun>(), 0, knockback, player.whoAmI);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<StarphoenixRound>(), damage, knockback, player.whoAmI);

                
                return false;
                
			}
			
			return true;
		}


		public override void AddRecipes()
		{
			
		}
	}
}
