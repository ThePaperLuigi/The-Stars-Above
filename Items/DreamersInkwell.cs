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
    public class DreamersInkwell : ModItem
	{//Umbral
		public override void SetStaticDefaults()
		{
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
			player.GetModPlayer<WeaponPlayer>().InkwellHeld = true;



			if (player.GetModPlayer<WeaponPlayer>().IrminsulAttackActive && player.whoAmI == Main.myPlayer)
			{
				player.GetModPlayer<WeaponPlayer>().IrminsulBoxEnd = Main.MouseWorld;


				if (!Main.mouseLeft || player.statMana <= 0)
                {
					
					
					//player.GetModPlayer<WeaponPlayer>().IrminsulAttackActive = false;

				}
			}				


			base.HoldItem(player);
		}
        public override bool? UseItem(Player player)
        {
			if(player.GetModPlayer<WeaponPlayer>().IrminsulAttackActive)
            {

            }
			else
            {//Upon activation
				//player.GetModPlayer<WeaponPlayer>().IrminsulBoxStart = Main.MouseWorld;
				//player.GetModPlayer<WeaponPlayer>().IrminsulAttackActive = true;
			}


            return base.UseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
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
			
		}
	}
}
