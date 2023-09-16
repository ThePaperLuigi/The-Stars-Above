using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using StarsAbove.Systems;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Melee
{
    public class MementoMuse : ModItem
	{
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("Holding this weapon will cause the [c/D8305F:Rhythm Gauge] to appear" +
                "\nSwing in tandem with the beat to deal increased damage and fire a powerful projectile" +
                "\nKeeping up with with the rhythm will slowly increase critical hit ratio" +
                "\nThe combo has a limit of 12" +
				"\nOnce you have completed a combo, you will gain the ability to use a [c/FF749B:Rhythm Burst]" +
				"\nRight click with [c/FF749B:Rhythm Burst] ready to temporarily ignore timing for 2 seconds" +
				"\n[c/FF749B:Rhythm Burst] will reset your combo to 0" +
                "\n'I'm not asking for much- do me a favor and die?'" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 66;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 78;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 15;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 15;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 2;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.LightPurple;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<Projectiles.MementoMuseProjectile>();
			Item.shootSpeed = 2f;
		}

		bool rhythmBurstReady = false;
		int rhythmBurstDuration = 0;

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (rhythmBurstReady)
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_MuseFinish, player.Center);
					rhythmBurstReady = false;
					player.GetModPlayer<WeaponPlayer>().RhythmCombo = 0;
					rhythmBurstDuration = 120;
				}
				else
				{
					return false;
				}
			}
			else
			{


				if (player.GetModPlayer<WeaponPlayer>().RhythmTiming > 40 && player.GetModPlayer<WeaponPlayer>().RhythmTiming < 60)//Success!
				{

					return true;
				}
				else
				{//Fail
					player.GetModPlayer<WeaponPlayer>().RhythmTiming = 0;
					player.GetModPlayer<WeaponPlayer>().RhythmCombo = 0;
				}
			}  
			return true;
		}

		public override void HoldItem(Player player)
		{
			rhythmBurstDuration--;
			if (player.GetModPlayer<WeaponPlayer>().RhythmTiming == 40)//
			{
				SoundEngine.PlaySound(StarsAboveAudio.SFX_MusePing, player.Center);
			}
			if (rhythmBurstDuration <= 0)
			{
				player.GetModPlayer<WeaponPlayer>().RhythmTiming += 2;
			}
			else
			{
				player.GetModPlayer<WeaponPlayer>().RhythmTiming = 48;
			}
			if (player.GetModPlayer<WeaponPlayer>().RhythmTiming > 100)
			{
				player.GetModPlayer<WeaponPlayer>().RhythmCombo = 0;
				player.GetModPlayer<WeaponPlayer>().RhythmTiming = 0;
			}
			Item.crit = (player.GetModPlayer<WeaponPlayer>().RhythmCombo *2);
			
				
			
			base.HoldItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 179);
				
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (player.GetModPlayer<WeaponPlayer>().RhythmTiming > 40 && player.GetModPlayer<WeaponPlayer>().RhythmTiming < 60)//Success!
			{
				
				
				
			}
			//target.AddBuff(BuffID.Weak, 60);

		
			

		}

		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.GetModPlayer<WeaponPlayer>().RhythmTiming > 40 && player.GetModPlayer<WeaponPlayer>().RhythmTiming < 60)
			{
				
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y + 8, velocity.X, velocity.Y,type, damage * 2, knockback * 2, player.whoAmI, 0f);
				
				if (player.GetModPlayer<WeaponPlayer>().RhythmCombo < 12 && rhythmBurstDuration <= 0)
				{
					player.GetModPlayer<WeaponPlayer>().RhythmCombo++;
					if (player.GetModPlayer<WeaponPlayer>().RhythmCombo == 12)
					{
						rhythmBurstReady = true;
					}
						

				}
				player.GetModPlayer<WeaponPlayer>().RhythmTiming = 0;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, player.Center);
				for (int d = 0; d < 2; d++)
				{
					Dust.NewDust(player.position, player.width, player.height, 60, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 179, default(Color), 1.5f);
				}
				for (int d = 0; d < 2; d++)
				{
					Dust.NewDust(player.position, player.width, player.height, 112, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 191, default(Color), 1.5f);
				}
			}
			return false;
		}
		public override void AddRecipes()
		{
			
			CreateRecipe(1)
			  .AddIngredient(ItemID.HallowedBar, 7)
			  .AddIngredient(ItemID.SoulofNight, 14)
			  .AddIngredient(ItemType<EssenceOfDeathsApprentice>())
			  .AddTile(TileID.Anvils)
			  .Register();

		}
	}
}
