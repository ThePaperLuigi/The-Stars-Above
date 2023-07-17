using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;
using System;
using StarsAbove.Buffs.ShepherdSunstone;

namespace StarsAbove.Items.Tools
{

    public class ShepherdSunstone : ModItem
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			Item.ResearchUnlockCount = 1;

		}

	
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		
		public override bool CanUseItem(Player player) {

			if(player.HasBuff(BuffType<ShepherdSunstoneCooldown>()))
            {
				return false;
            }
			return true;
		}

		public override bool? UseItem(Player player) {

			float dustAmount = 40f;
			
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player other = Main.player[i];
				if (other.active && other.Distance(player.Center) > 300)
				{
					other.Teleport(player.Center, 1, 0);
					NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)other.whoAmI, player.Center.X, player.Center.Y, 1, 0, 0);
				}
			}
			//Dust
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
				spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
				int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = player.Center + spinningpoint5;
				Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
			}
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(284f, 4f);
				spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
				int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = player.Center + spinningpoint5;
				Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 23f;
			}
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(184f, 4f);
				spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation());
				int dust = Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = player.Center + spinningpoint5;
				Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 48f;
			}
			//add cooldown (de)buff
			player.AddBuff(BuffType<ShepherdSunstoneCooldown>(), 36000)
			return true;
		}
		public override void AddRecipes()
		{
			
		}
	}
}