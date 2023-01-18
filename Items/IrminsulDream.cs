using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles.IrminsulDream;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items
{
    public class IrminsulDream : ModItem
	{//Umbral
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Irminsul's Dream");
			Tooltip.SetDefault("" +
				"" +
				"Click and drag to draw a box; releasing left click will execute [c/6CDA61:Verdant Snapshot] (Attack will end once maximum size is reached)" +
				"\n[c/6CDA61:Verdant Snapshot] inflicts damage to all foes in the box while inflicting [c/2EB24A:Verdant Embrace] for 12 seconds" +
				"\n[c/2EB24A:Verdant Embrace] deals minor damage over time" +
				"\nAdditionally, when a foe with [c/2EB24A:Verdant Embrace] is inflicted with On Fire, Frostburn, Cursed Inferno, or Shadowflame, it will trigger a [c/94EF58:Verdant Burst]" +
				"\n[c/94EF58:Verdant Burst] will inflict bonus damage equal to 3% of the foe's maximum HP (up to 120 HP per application, can not kill)" +
				"\nHowever, both [c/2EB24A:Verdant Embrace] and the debuff that triggered [c/94EF58:Verdant Burst] will be cleansed" +
				"\n'Let knowledge be yours'" +
				$"");  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 49;          
			Item.DamageType = DamageClass.Magic;          
			Item.width = 40;            
			Item.mana = 20;
			Item.height = 40;        
			Item.useTime = 20;         
			Item.useAnimation = 20;       
			Item.useStyle = ItemUseStyleID.HoldUp;          
			Item.knockBack = 0;       
			Item.value = Item.buyPrice(gold: 1);          
			Item.rare = ItemRarityID.Green;           
			Item.UseSound = SoundID.Item9;      
			//item.autoReuse = false;         
			//Item.channel = true;
			Item.value = Item.buyPrice(gold: 1);          
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.shoot = ProjectileType<Projectiles.TwinStars.TwinStarLaser1>();
			Item.shootSpeed = 14f;
		}
		 
		public override bool AltFunctionUse(Player player)
		{
			return false;
		}

		public override bool CanUseItem(Player player)
		{
			
			return base.CanUseItem(player);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}
		public override void HoldItem(Player player)
		{
			player.GetModPlayer<StarsAbovePlayer>().IrminsulHeld = true;

			
			//player.AddBuff(BuffType<Buffs.TwinStarsBuff>(), 2);
			if (player.ownedProjectileCounts[ProjectileType<IrminsulHeld>()] < 1)
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<IrminsulHeld>(), 0, 4, player.whoAmI, 0f);


			}
			
			if(player.GetModPlayer<StarsAbovePlayer>().IrminsulAttackActive)
            {
				player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd = Main.MouseWorld;

				//Draw the 4 lines of the box...
				for (int i = 0; i < 20; i++)//Bottom line
				{
					Vector2 position = Vector2.Lerp(
						new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.X, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.Y), 
						new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.X, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.Y),
						(float)i / 20);
					Dust d = Dust.NewDustPerfect(position, DustID.FireworkFountain_Green, null, 240, default(Color), 0.5f);
					d.fadeIn = 0.1f;
					d.velocity = Vector2.Zero;

					d.noLight = true;
					d.noGravity = true;
				}
				for (int i = 0; i < 20; i++)//Top line
				{
					Vector2 position = Vector2.Lerp(
						new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.X, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.Y),
						new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.X, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.Y),
						(float)i / 20);
					Dust d = Dust.NewDustPerfect(position, DustID.FireworkFountain_Green, null, 240, default(Color), 0.5f);
					d.fadeIn = 0.1f;
					d.velocity = Vector2.Zero;
					d.noLight = true;
					d.noGravity = true;
				}
				for (int i = 0; i < 20; i++)//Left line
				{
					Vector2 position = Vector2.Lerp(
						new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.X, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.Y),
						new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.X, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.Y),
						(float)i / 20);
					Dust d = Dust.NewDustPerfect(position, DustID.FireworkFountain_Green, null, 240, default(Color), 0.5f);
					d.fadeIn = 0.1f;
					d.velocity = Vector2.Zero;

					d.noLight = true;
					d.noGravity = true;
				}
				for (int i = 0; i < 20; i++)//Right line
				{
					Vector2 position = Vector2.Lerp(
						new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.X, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.Y),
						new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.X, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.Y),
						(float)i / 20);
					Dust d = Dust.NewDustPerfect(position, DustID.FireworkFountain_Green, null, 240, default(Color), 0.5f);
					d.fadeIn = 0.1f;
					d.velocity = Vector2.Zero;

					d.noLight = true;
					d.noGravity = true;
				}

				Vector2 centerOfBox = Vector2.Lerp(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd, 0.5f);

				int width = (int)Math.Abs(Vector2.Distance(//find the X difference
								new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.X, 0),
								new Vector2(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.X, 0))) / 2;
				int height = (int)Math.Abs(Vector2.Distance(//find the Y difference
								new Vector2(0, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart.Y),
								new Vector2(0, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd.Y))) / 2;

				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (
						//npc.active && npc.CanBeChasedBy() && 
						(npc.Distance(centerOfBox) < width || npc.Distance(centerOfBox) < height))
					{
						for (int r = 0; r < 10; r++)
						{//Circle
							Vector2 offset = new Vector2();
							double angle = Main.rand.NextDouble() * 2d * Math.PI;
							offset.X += (float)(Math.Sin(angle) * (20));
							offset.Y += (float)(Math.Cos(angle) * (20));

							Dust d2 = Dust.NewDustPerfect(npc.Center + offset, DustID.FireworkFountain_Green, Vector2.Zero, 200, default(Color), 0.4f);
							d2.fadeIn = 0.1f;
							d2.noGravity = true;
						}
					}
				}
				if(player.GetModPlayer<StarsAbovePlayer>().IrminsulAttackActive)
                {
					if (player.ownedProjectileCounts[ProjectileType<IrminsulMark1>()] < 1)
					{
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<IrminsulMark1>(), 0, 4, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<IrminsulMark2>(), 0, 4, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<IrminsulMark3>(), 0, 4, player.whoAmI, 0f);
						Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<IrminsulMark4>(), 0, 4, player.whoAmI, 0f);


					}
					Main.cursorAlpha = 1f;
					Main.cursorScale = 0f;
				}
				if (!Main.mouseLeft || Vector2.Distance(player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart, player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxEnd) > 400)
                {//If no longer holding left click or the box is too big... (The circle will appear above enemies even if the attack isn't used yet.
					
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC npc = Main.npc[i];

						if (
							//npc.active && npc.CanBeChasedBy() && 
							(npc.Distance(centerOfBox) < width || npc.Distance(centerOfBox) < height))
						{
							Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), npc.Center, Vector2.Zero,
							   ProjectileType<VerdantSnapshot>(), player.GetWeaponDamage(Item), 0, player.whoAmI);
						}
					}
					player.GetModPlayer<StarsAbovePlayer>().IrminsulAttackActive = false;

				}
			}				


			base.HoldItem(player);
		}
        public override bool? UseItem(Player player)
        {
			if(player.GetModPlayer<StarsAbovePlayer>().IrminsulAttackActive)
            {

            }
			else
            {//Upon activation
				player.GetModPlayer<StarsAbovePlayer>().IrminsulBoxStart = Main.MouseWorld;
				player.GetModPlayer<StarsAbovePlayer>().IrminsulAttackActive = true;
			}


            return base.UseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// 
			// 60 frames = 1 second
			
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			

			
			//player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;

			return false;
            
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Book, 20)
				.AddIngredient(ItemID.Vine, 3)
				.AddIngredient(ItemID.JungleSpores, 5)
				.AddIngredient(ItemType<EssenceOfNature>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
