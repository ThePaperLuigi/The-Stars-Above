using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.Pigment;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Items
{
    public class PenthesileaMuse : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Penthesilea's Muse");
			Tooltip.SetDefault("" +
				"Attacks with this weapon will sweep in a wide arc" +
				"\nEvery 15 seconds, the nearest boss enemy will be [c/E83D71:Inked] and will drip paint according to a specific color" +
				"\nRight click to view all [c/889FCA:Mystic Pigments]" +
				"\nSelecting a [c/889FCA:Mystic Pigment] will change your attacks to the specified color" +
				"\nStriking [c/E83D71:Inked] foes with the correct [c/889FCA:Mystic Pigment] will always crit" +
				"\nStriking [c/E83D71:Inked] foes with the wrong color will deal half damage, and the opposite color will deal 1/3rd damage" +
				"\nAdditionally, each [c/889FCA:Mystic Pigment] has a special debuff when striking foes that lasts for 4 seconds" +
				"\n[c/E45555:Red Pigment] inflicts On Fire / [c/35EC43:Green Pigment] inflicts Cursed Flame" +
				"\n[c/EC8B35:Orange Pigment] inflicts Ichor / [c/3596EC:Blue Pigment] inflicts Frostburn" +
				"\n[c/E4CE55:Yellow Pigment] inflicts Midas / [c/9E35EC:Purple Pigment] inflicts Acid Venom" +
				//"\nSwapping [c/889FCA:Mystic Pigments] has a 1/3rd second cooldown" +
				"" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				Item.damage = 1195;
			}
			else
			{
				Item.damage = 255;
			}
			
			//The damage of your weapon
			Item.DamageType = DamageClass.Melee;         //Is your weapon a melee weapon?
			Item.width = 68;            //Weapon's texture's width
			Item.height = 68;           //Weapon's texture's height
			Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 3;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.rare = 10;              //The rarity of the weapon, from -1 to 13
			//item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = 337;
			Item.shootSpeed = 0f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}
		int currentSwing;
		int slashDuration;
		 
		int inkEnemyCooldown;
		int splatterCooldown;
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			float launchSpeed = 36f;
			float launchSpeed2 = 102f;
			float launchSpeed3 = 120f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			Vector2 arrowVelocity2 = direction * launchSpeed2;
			Vector2 arrowVelocity3 = direction * launchSpeed3;

			var modPlayer = player.GetModPlayer<StarsAbovePlayer>();
			if (player.altFunctionUse == 2)
			{
				if (modPlayer.paintVisible)
				{
					modPlayer.paintVisible = false;
				}
				else
				{
					if (!player.HasBuff(BuffType<InkCooldown>()))
					{
						
						modPlayer.paintVisible = true;
						player.AddBuff(BuffType<InkCooldown>(), 20);
						

					}
					else
					{
						return false;
					}
				}
				
				
				
			}
			else
            {

				
				
				
				return true;
			}
			return true;
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{

			
		}
        public override bool? UseItem(Player player)
        {
			float launchSpeed = 36f;
			float launchSpeed2 = 102f;
			float launchSpeed3 = 120f;
			Vector2 mousePosition = Main.MouseWorld;
			Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
			Vector2 arrowVelocity = direction * launchSpeed;
			Vector2 arrowVelocity2 = direction * launchSpeed2;
			Vector2 arrowVelocity3 = direction * launchSpeed3;
			var modPlayer = player.GetModPlayer<StarsAbovePlayer>();

			if (modPlayer.chosenColor == 0) //Red
			{

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<PaintSwingR>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
				SoundEngine.PlaySound(SoundID.Item1, player.position);
			}
			if (modPlayer.chosenColor == 1) //Orange
			{

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<PaintSwingO>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
				SoundEngine.PlaySound(SoundID.Item1, player.position);
			}
			if (modPlayer.chosenColor == 2) //Yellow
			{

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<PaintSwingY>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
				SoundEngine.PlaySound(SoundID.Item1, player.position);
			}
			if (modPlayer.chosenColor == 3) //Green
			{

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<PaintSwingG>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
				SoundEngine.PlaySound(SoundID.Item1, player.position);
			}
			if (modPlayer.chosenColor == 4) //Blue
			{

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<PaintSwingB>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
				SoundEngine.PlaySound(SoundID.Item1, player.position);
			}
			if (modPlayer.chosenColor == 5) //Purple
			{

				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<PaintSwingP>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
				SoundEngine.PlaySound(SoundID.Item1, player.position);
			}




			return base.UseItem(player);
        }
        public override void HoldItem(Player player)
		{
			var modPlayer = player.GetModPlayer<StarsAbovePlayer>();
			
			inkEnemyCooldown--;
			splatterCooldown--;
			if(inkEnemyCooldown < 0)
            {
				modPlayer.targetPaintColor = Main.rand.Next(0, 6);
				inkEnemyCooldown = 900;
            }
			NPC closest = null;
			float closestDistance = 9999999;
			int highestHP = 1;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				float distance = Vector2.Distance(npc.Center, player.Center);


				if (npc.active && npc.Distance(player.position) < closestDistance && npc.boss)
				{
					closest = npc;
					
					closestDistance = npc.Distance(player.position);
				}




			}
			if(closest != null)
            {
				if (closest.CanBeChasedBy() && closestDistance < 1200f)
				{
					if (modPlayer.targetPaintColor == 0)
					{
						if (splatterCooldown < 0)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),closest.Center.X, closest.Center.Y, 0, 0, ProjectileType<SplatterRed>(), 0, 0, player.whoAmI, 0f);

							splatterCooldown = 10;

						}
						closest.AddBuff(BuffType<Buffs.RedPaint>(), 30);
					}
					if (modPlayer.targetPaintColor == 1)
					{
						if (splatterCooldown < 0)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),closest.Center.X, closest.Center.Y, 0, 0, ProjectileType<SplatterOrange>(), 0, 0, player.whoAmI, 0f);

							splatterCooldown = 10;

						}
						closest.AddBuff(BuffType<Buffs.OrangePaint>(), 30);
					}
					if (modPlayer.targetPaintColor == 2)
					{
						if (splatterCooldown < 0)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),closest.Center.X, closest.Center.Y, 0, 0, ProjectileType<SplatterYellow>(), 0, 0, player.whoAmI, 0f);

							splatterCooldown = 10;

						}
						closest.AddBuff(BuffType<Buffs.YellowPaint>(), 30);
					}
					if (modPlayer.targetPaintColor == 3)
					{
						if (splatterCooldown < 0)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),closest.Center.X, closest.Center.Y, 0, 0, ProjectileType<SplatterGreen>(), 0, 0, player.whoAmI, 0f);

							splatterCooldown = 10;

						}
						closest.AddBuff(BuffType<Buffs.GreenPaint>(), 30);
					}
					if (modPlayer.targetPaintColor == 4)
					{
						if (splatterCooldown < 0)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),closest.Center.X, closest.Center.Y, 0, 0, ProjectileType<SplatterBlue>(), 0, 0, player.whoAmI, 0f);

							splatterCooldown = 10;

						}
						closest.AddBuff(BuffType<Buffs.BluePaint>(), 30);
					}
					if (modPlayer.targetPaintColor == 5)
					{
						if (splatterCooldown < 0)
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),closest.Center.X, closest.Center.Y, 0, 0, ProjectileType<SplatterPurple>(), 0, 0, player.whoAmI, 0f);

							splatterCooldown = 10;

						}
						closest.AddBuff(BuffType<Buffs.PurplePaint>(), 30);
					}


					/*for (int i3 = 0; i3 < 50; i3++)
					{
						Vector2 position2 = Vector2.Lerp(player.Center, closest.Center, (float)i3 / 50);
						Dust d = Dust.NewDustPerfect(position2, 219, null, 240, default(Color), 0.3f);
						d.fadeIn = 0.01f;
						d.noLight = true;
						d.noGravity = true;
					}*/
				}
			}
			

			base.HoldItem(player);
        }

        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			 
			
			return false;
		}

		public override void AddRecipes()
		{
			
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				
				CreateRecipe(1)
					.AddIngredient(calamityMod.Find<ModItem>("UeliaceBar").Type, 14)
					.AddIngredient(ItemType<EssenceOfInk>())
					.AddTile(TileID.Anvils)
					.Register();
			}
			else
			{


				CreateRecipe(1)
			   .AddIngredient(ItemID.Paintbrush, 1)
			   .AddIngredient(ItemID.ChlorophyteBar, 12)
			   .AddIngredient(ItemType<EssenceOfInk>())
			   .AddTile(TileID.Anvils)
			   .Register();
			}
			
		}
	}
}
