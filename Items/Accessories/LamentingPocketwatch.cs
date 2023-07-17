using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.LamentingPocketwatch;
using StarsAbove.Projectiles.LamentingPocketwatch;
using StarsAbove.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Accessories
{
    public class LamentingPocketwatch : ModItem
	{
		public override void SetStaticDefaults() {
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
		}

		public override void SetDefaults() {
			Item.width = 40;
			Item.height = 40;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<PocketwatchModPlayer>().pocketwatchEquipped = true;
		}



		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<Materials.StellarRemnant>(), 40)
				.AddCustomShimmerResult(ItemType<Materials.StellarRemnant>(), 3)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
	public class PocketwatchModPlayer : ModPlayer
    {
        public bool pocketwatchEquipped;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (pocketwatchEquipped)
			{

				//Determine if clash win
				if (hit.Crit && !Player.HasBuff(BuffType<LamentingPocketwatchCooldown>()))
                {
					Player.AddBuff(BuffType<LamentingPocketwatchCooldown>(), 180);
					if(Main.rand.NextBool())
                    {
						target.StrikeNPC(hit);
						NetMessage.SendStrikeNPC(target, hit);
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							Projectile.NewProjectile(null, new Vector2(target.Center.X, target.Center.Y - target.height - 10), Vector2.Zero, ProjectileType<LamentClashWin>(), 0, 0, Player.whoAmI);
						}
					}
					else
                    {
						Player.AddBuff(BuffType<Vulnerable>(), 180);
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							Projectile.NewProjectile(null, new Vector2(target.Center.X, target.Center.Y - target.height - 10), Vector2.Zero, ProjectileType<LamentClashLose>(), 0, 0, Player.whoAmI);
						}
					}
					
                }
				//Spawn coin on enemy to signify
				

				//Do effect
			}
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			
			if (pocketwatchEquipped)
			{
				modifiers.DamageVariationScale *= 0;

				//Determine if clash win
				if(Player.HasBuff(BuffType<Vulnerable>()))
                {
					modifiers.FinalDamage -= 0.4f;
					modifiers.DisableCrit();
				}

				//Do effect
			}
		}
        
        public override void ResetEffects()
        {
			pocketwatchEquipped = false;
        }
    }
}
