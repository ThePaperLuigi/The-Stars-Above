using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Items
{
    public class LiberationBlazing : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("" +
				"Swapping to this weapon grants the buff '[c/FF2B2B:Core of Flames]' for 20 seconds" +
				"\nThis buff has a 2 minute cooldown, beginning when [c/FF2B2B:Core of Flames] is applied" +
				"\nWhen you are under the influence of [c/FF2B2B:Core of Flames] attacks cause a massive explosion on contact, burning foes for 3 seconds" +
				"\nWhen the weapon is held without [c/FF2B2B:Core of Flames] active, you will be burned until the blade is stowed" +
				"\nRight click to unleash [c/AD2500:Scarlet Outburst], spraying powerful flames around you as well as granting Invincibility for 2 seconds" +
				"\n[c/AD2500:Scarlet Outburst] inflicts 50 unmitigatable self damage and burns you for 8 seconds" +
				"\n[c/AD2500:Scarlet Outburst] can not be used when you are below 50 HP or if you are currently burning" +
				"\nBelow 100 HP, you gain drastic Health Regeneration when burning and [c/FF2B2B:Core of Flames] is active" +
				"\nThis healing is only applicable with this weapon held" +
				"\n'Fight to emancipate this world'" +
				$"");  //The (English) text shown below your weapon's name


			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 399;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee;          //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 40;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 40;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 12;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 116;
			Item.shootSpeed = 30f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
		}
		int fireRegen;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			
			if (player.altFunctionUse == 2)
			{
				if (!player.HasBuff(BuffID.OnFire) && player.statLife >= 50)
				{
					
					Vector2 position = Main.LocalPlayer.position;
					int playerWidth = Main.LocalPlayer.width;
					int playerHeight = Main.LocalPlayer.height;
					Dust dust;
					for (int d = 0; d < 50; d++)
					{
						dust = Main.dust[Terraria.Dust.NewDust(position, playerWidth, playerHeight, 106, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, new Color(255, 255, 255), 1f)];
						dust.shader = GameShaders.Armor.GetSecondaryShader(81, Main.LocalPlayer);
					}

					return true;
						

					

				}
				return false;


			}
			else
			{
				

			}
			return base.CanUseItem(player);
		}
        public override void HoldItem(Player player)
        {
			
			if(!Main.LocalPlayer.HasBuff(BuffType<Buffs.CoreOfFlamesCooldown>()))
			{
				
				player.AddBuff(BuffType<Buffs.CoreOfFlames>(), 1200);
				player.AddBuff(BuffType<Buffs.CoreOfFlamesCooldown>(), 7200);
			}
			if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.CoreOfFlames>()))
			{

				player.AddBuff(BuffID.OnFire, 2);

				
			}
			if (Main.LocalPlayer.HasBuff(BuffType<Buffs.CoreOfFlames>()))
			{

				

				Vector2 vector = new Vector2(
					Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
					Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
				Dust d = Main.dust[Dust.NewDust(
					player.MountedCenter + vector, 1, 1,
					204, 0, 0, 255,
					new Color(0.8f, 0.4f, 1f), 0.8f)];
				d.shader = GameShaders.Armor.GetSecondaryShader(122, Main.LocalPlayer);
				d.velocity = -vector / 12;
				d.velocity -= player.velocity / 8;
				d.noLight = true;
				d.noGravity = true;
			}
			if (Main.LocalPlayer.HasBuff(BuffType<Buffs.CoreOfFlames>()) && player.HasBuff(BuffID.OnFire))
			{
				
				fireRegen++;
				if(fireRegen > 5)
                {
					if (player.statLife <= 100)
					{
						player.statLife++;
					}

					fireRegen = 0;
                }
				

			}


			base.HoldItem(player);
        }
		
        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				//Emit dusts when swing the sword
				
					Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 269);
				
				
				
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second
			if (player.statMana >= 50)
			{
				target.AddBuff(BuffID.OnFire, 120);
			}
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				player.statLife -= 50;
				player.AddBuff(BuffType<Buffs.Invincibility>(), 120);
				player.AddBuff(BuffID.OnFire, 480);
				SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, player.Center);
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center, Vector2.Zero, Mod.Find<ModProjectile>("ScarletOutburst").Type, 0, 4, 0, 0, 1);
				for (int i = 0; i < 8; i++)
				{
					// Random upward vector.
					Vector2 vel = new Vector2(Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-5, -9));
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center, vel, ProjectileID.MolotovFire, damage/3, 4, 0, 0, 1);
				}
				for (int i = 0; i < 8; i++)
				{
					// Random upward vector.
					Vector2 vel = new Vector2(Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-5, -9));
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center, vel, ProjectileID.MolotovFire2, damage/3, 4, 0, 0, 1);
					
				}
				for (int i = 0; i < 8; i++)
				{
					// Random upward vector.
					Vector2 vel = new Vector2(Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-5, -9));
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center, vel, ProjectileID.MolotovFire3, damage/3, 4, 0, 0, 1);
				}
				return false;


			}

			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<PrismaticCore>(), 20)
				.AddIngredient(ItemID.FragmentSolar, 8)
				.AddIngredient(ItemType<EssenceOfLiberation>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
