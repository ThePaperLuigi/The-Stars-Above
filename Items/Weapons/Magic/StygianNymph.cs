using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Essences;
using Terraria.Audio;

namespace StarsAbove.Items.Weapons.Magic
{
    public class StygianNymph : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 167;
            Item.DamageType = DamageClass.Magic;
            Item.width = 65;
            Item.height = 70;
            Item.useTime = 30;
            Item.mana = 2;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.noUseGraphic = true;
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Cyan;
            //item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.channel = true;//Important for all bows
            Item.shoot = 10;
            Item.shootSpeed = 15f;
            Item.value = Item.buyPrice(gold: 1);
            Item.scale = 2f;
        }
        bool ammoConsumed;
        int currentSwing;
        Vector2 vector32;
        int rightClick;
        int swapCooldown;
        int form;//0 = Lance 1 = Blasting
        int attackCooldown;
        int chargeCooldown;
        bool chargeFinishMsg;
         
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            
            if (attackCooldown > 0)
            {
                return false;
            }
            

            return true;

        }
        public override bool? UseItem(Player player)
        {
            float launchSpeed = 36f;

            float launchSpeed2 = 182f;

            float launchSpeed3 = 126f;
            Vector2 mousePosition = Main.MouseWorld;
            Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
            Vector2 arrowVelocity = direction * launchSpeed;
            Vector2 arrowVelocity2 = direction * launchSpeed2;

            Vector2 arrowVelocity3 = direction * launchSpeed3;
            SoundEngine.PlaySound(SoundID.Item1, player.position);

            if (player.altFunctionUse == 2)
            {

                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity2.Y, ProjectileType<StygianSwing3>(), player.GetWeaponDamage(Item) / 6, 3, player.whoAmI, 0f);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity2.Y, ProjectileType<StygianSwing4>(), 0, 3, player.whoAmI, 0f);
                player.GetModPlayer<WeaponPlayer>().duality -= 10;
                
            }
            else
            {

                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<StygianSwing2>(), 0, 3, player.whoAmI, 0f);
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<StygianSwing1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                player.GetModPlayer<WeaponPlayer>().duality += 10;
            }
            return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.GetModPlayer<WeaponPlayer>().duality < 50)
                {
                    player.AddBuff(BuffType<Buffs.ClawsOfNyx>(), 10);
                }
                

                if (StarsAbove.weaponActionKey.JustPressed)
                {
                    if (player.GetModPlayer<WeaponPlayer>().duality >= 50 && !player.HasBuff(BuffType<Buffs.FlashOfEternityCooldown>()))
                    {
                        Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                        Vector2 leap = Vector2.Normalize(mousePosition2) * 15f;
                        player.velocity = leap;
                        player.AddBuff(BuffType<Buffs.FlashOfEternity>(), 120);
                        player.AddBuff(BuffType<Buffs.FlashOfEternityCooldown>(), 480);
                    }
                }
            }
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

           
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.PixieDust, 8)
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddIngredient(ItemID.SoulofLight, 2)
                .AddIngredient(ItemID.SoulofNight, 2)
                .AddIngredient(ItemType<EssenceOfDuality>())
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
