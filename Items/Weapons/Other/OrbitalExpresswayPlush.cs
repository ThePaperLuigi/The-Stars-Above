using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Other.OrbitalExpresswayPlush;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using StarsAbove.Projectiles.Magic.CarianDarkMoon;
using StarsAbove.Projectiles.Other.OrbitalExpresswayPlush;
using StarsAbove.Projectiles.Summon.KeyOfTheKingsLaw;
using StarsAbove.Systems;
using StarsAbove.Systems;
using Steamworks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Other
{
    public class OrbitalExpresswayPlush : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Agnian Farewell");
			/* Tooltip.SetDefault("Playing this instrument grants the buff [c/FFE86A:Off-seer's Purpose] to yourself for 2 minutes" +
				"\n[c/FFE86A:Off-seer's Purpose] grants increased Luck and maximum Luck while increasing enemy spawn rates (8 minute cooldown) " +
				"\nAfter defeating an enemy with the Agnian Farewell in your inventory while [c/FFE86A:Off-seer's Purpose] is active, gain the buff [c/FC6969:Farewell of Flames] for 10 seconds" +
				"\n[c/FC6969:Farewell of Flames] grants an additional increase to Luck" +
				"\nIf used while wielding the strength of the Astral Aspect (through trading in Multiplayer), further increase the potency of [c/FFE86A:Off-seer's Purpose]'s Luck increase" +
				"" +
				$""); */  //The (English) text shown below your weapon's name

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 440;           //Not a weapon.
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;            //Weapon's texture's width
			Item.height = 40;           //Weapon's texture's height
			Item.useTime = 60;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 60;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Green;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = StarsAboveAudio.SFX_AgnianTune;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<KeyOfTheKingsLawProjectile>();
			Item.shootSpeed = 15f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
			Item.noUseGraphic = true;
		}
		//SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

		int randomBuff;
        public override void UpdateInventory(Player player)
        {
			//player.GetModPlayer<WeaponPlayer>().AgnianFarewellInInventory = true;


            base.UpdateInventory(player);
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
			damage.Flat += (int)(player.GetModPlayer<StarsAbovePlayer>().novaDamage * 0.1f);

            base.ModifyWeaponDamage(player, ref damage);
        }
        public override bool CanUseItem(Player player)
		{
			if (player.HasBuff(BuffType<OrbitalExpresswayPlushCooldown>()))
			{
				return false;
			}
			return base.CanUseItem(player);
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			
		}

        public override bool? UseItem(Player player)
        {
            //player.AddBuff(BuffType<OffSeersPurpose>(), 7200);
            //player.AddBuff(BuffType<FarewellCooldown>(), 28800);

            //Toss the plush into the air, and it disappears in a sparkle. Mark your cursor location with a pulsing orb and draw a line from the sky projectile (where the train will come from) to that location
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, 0, -5, ProjectileType<ExpresswayPlushActive>(), 0, 0, player.whoAmI, 0f);//Spawn the sword.
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<ExpresswayTarget>(), player.GetWeaponDamage(Item), 0, player.whoAmI, 0f);
				player.AddBuff(BuffType<OrbitalExpresswayPlushCooldown>(), 60 * 60);
            }
            return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			return false;
		}
		public override void AddRecipes()
		{
            CreateRecipe(1)
                  .AddIngredient(ItemID.ChocolateChipCookie, 1)
                  .AddIngredient(ItemID.Minecart, 1)
                  .AddIngredient(ItemID.FallenStar, 8)
                  .AddIngredient(ItemType<EssenceOfCookies>())
                  .AddTile(TileID.Anvils)
                  .Register();

        }
	}

	
}
