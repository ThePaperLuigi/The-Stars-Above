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

namespace StarsAbove.Items
{
    public class StygianNymph : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("" +
                "[c/F7D76A:Hold left click to charge the weapon; if the weapon is fully charged, the attack varies]" +
                "\nHolding the weapon will cause the [c/8BD1D1:Duality Gauge] to appear, beginning 100% [c/C7FFFA:Sanctified]" +
                "\nUncharged attacks sweep in a colossal area and influences the [c/8BD1D1:Duality Gauge] towards [c/C7FFFA:Sanctification]" +
                "\nCharged attacks deal damage over multiple hits to foes in front of you, and influences the [c/8BD1D1:Duality Gauge] towards [c/B60000:Volatility]" +
                "\nHolding charged attacks will drain mana rapidly" +
                "\nWhen the [c/8BD1D1:Duality Gauge] is more than 50% [c/C7FFFA:Sanctified], gain access to [c/C7FFFA:Flash of Eternity]" +
                "\nWhen the [c/8BD1D1:Duality Gauge] is more than 50% [c/B60000:Volatile], gain the buff [c/B60000:Claws of Nyx]" +
                "\nWhen conditions are met, right click to activate [c/C7FFFA:Flash of Eternity], dashing towards your cursor (16 second cooldown)" +
                "\nThe first instance of damage taken during [c/C7FFFA:Flash of Eternity] is negated" +
                "\n[c/B60000:Claws of Nyx] increases damage by 40% and crit chance by 20%" +
                "\n'Darkness comes from within'" +
                $""); */

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
            //if (player.GetModPlayer<WeaponPlayer>().chosenStarfarer == 1) // Asphodene
            //{
            //    if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The weapon fails to react to your Aspect, rendering it unusable."), 241, 255, 180);}
            //    return false;
            //}

            /*for (int i = 0; i < player.CountBuffs(); i++)
                if (player.buffType[i] == BuffType<Buffs.SoulUnbound>())
                {
                    if (player.buffTime[i] <= 40)
                    {
                        return false;
                    }
                }
            */
            if(player.altFunctionUse == 2)
            {
                if (player.GetModPlayer<WeaponPlayer>().duality >= 50 && !player.HasBuff(BuffType<Buffs.FlashOfEternityCooldown>()))
                {
                    Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                    Vector2 leap = Vector2.Normalize(mousePosition2) * 15f;
                    player.velocity = leap;
                    player.AddBuff(BuffType<Buffs.FlashOfEternity>(), 120);
                    player.AddBuff(BuffType<Buffs.FlashOfEternityCooldown>(), 960);
                }
                return false;
            }
            


            return true;

        }
       
        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if(player.GetModPlayer<WeaponPlayer>().duality < 50)
                {
                    player.AddBuff(BuffType<Buffs.ClawsOfNyx>(), 2);
                }
                chargeCooldown--;
                attackCooldown--;
                swapCooldown--;
                float launchSpeed = 36f;
                
                float launchSpeed2 = 182f;

                float launchSpeed3 = 126f;
                Vector2 mousePosition = Main.MouseWorld;
                Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                Vector2 arrowVelocity = direction * launchSpeed;
                Vector2 arrowVelocity2 = direction * launchSpeed2;

                Vector2 arrowVelocity3 = direction * launchSpeed3;

                if (player.channel && swapCooldown <= 0)
                {

                    Item.useTime = 2;
                    Item.useAnimation = 2;

                    player.GetModPlayer<WeaponPlayer>().bowChargeActive = true;
                    
                    player.GetModPlayer<WeaponPlayer>().bowCharge += 6;

                    


                    if (player.GetModPlayer<WeaponPlayer>().bowCharge == 1)
                    {

                        //Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/bowstring"), 0.5f);
                    }
                    if (player.GetModPlayer<WeaponPlayer>().bowCharge == 99)
                    {

                        for (int d = 0; d < 88; d++)
                        {
                            Dust.NewDust(player.Center, 0, 0, 43, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
                        }
                        //Main.PlaySound(SoundID.Item52, player.position);
                    }
                    if (player.GetModPlayer<WeaponPlayer>().bowCharge < 100)
                    {

                        for (int i = 0; i < 30; i++)
                        {//Circle
                            Vector2 offset = new Vector2();
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));
                            offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));

                            Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 20, player.velocity, 200, default(Color), 0.5f);
                            d2.fadeIn = 0.1f;
                            d2.noGravity = true;
                        }
                        //Charge dust
                        Vector2 vector = new Vector2(
                            Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
                            Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
                        Dust d = Main.dust[Dust.NewDust(
                            player.MountedCenter + vector, 1, 1,
                            43, 0, 0, 255,
                            new Color(0.8f, 0.4f, 1f), 0.8f)];
                        d.velocity = -vector / 12;
                        d.velocity -= player.velocity / 8;
                        d.noLight = true;
                        d.noGravity = true;

                    }
                    

                }
                else
                {

                    Item.useTime = 30;
                    Item.useAnimation = 30;

                    if (player.GetModPlayer<WeaponPlayer>().bowCharge >= 100)//Fully Charged Normal attack
                    {

                        SoundEngine.PlaySound(SoundID.Item1, player.position);
                        player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
                        player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
                        //Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                        //Vector2 leap = Vector2.Normalize(mousePosition2) * 15f;
                        //player.velocity = leap;
                        //attackCooldown = 60;
                        
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity2.Y, ProjectileType<StygianSwing3>(), player.GetWeaponDamage(Item)/6, 3, player.whoAmI, 0f);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity2.Y, ProjectileType<StygianSwing4>(), 0, 3, player.whoAmI, 0f);
                        player.GetModPlayer<WeaponPlayer>().duality -= 10;

                    }
                    else
                    {
                        if (player.GetModPlayer<WeaponPlayer>().bowCharge > 0)//Not Fully Charged normal
                        {//
                            SoundEngine.PlaySound(SoundID.Item1, player.position);

                            player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
                            player.GetModPlayer<WeaponPlayer>().bowCharge = 0;

                            
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<StygianSwing2>(), 0, 3, player.whoAmI, 0f);
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<StygianSwing1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                            player.GetModPlayer<WeaponPlayer>().duality += 10;


                        }
                    }
                   
                }


                //item.shootSpeed = 8f + (int)Math.Round(player.GetModPlayer<WeaponPlayer>().bowCharge / 10);
            }
        }


        /*
		 * Feel free to uncomment any of the examples below to see what they do
		 */

        // What if I wanted this gun to have a 38% chance not to consume ammo?
        

        // What if I wanted it to work like Uzi, replacing regular bullets with High Velocity Bullets?
        // Uzi/Molten Fury style: Replace normal Bullets with Highvelocity
        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (type == ProjectileID.Bullet) // or ProjectileID.WoodenArrowFriendly
			{
				type = ProjectileID.BulletHighVelocity; // or ProjectileID.FireArrow;
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}*/

        // What if I wanted it to shoot like a shotgun?
        // Shotgun style: Multiple Projectiles, Random spread 
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
             
            if (player.channel)
            {

            }
            else
            {

            }
            return false;
        }

        // What if I wanted an inaccurate gun? (Chain Gun)
        // Inaccurate Gun style: Single Projectile, Random spread 
        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(30));
			velocity.X = perturbedSpeed.X;
			velocity.Y = perturbedSpeed.Y;
			return true;
		}*/

        // What if I wanted multiple projectiles in a even spread? (Vampire Knives) 
        // Even Arc style: Multiple Projectile, Even Spread 
        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(45);
			position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}*/


        // How can I make the shots appear out of the muzzle exactly?
        // Also, when I do this, how do I prevent shooting through tiles?

        // How can I get a "Clockwork Assault Rifle" effect?
        // 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
        /*	The following changes to SetDefaults()
		 	item.useAnimation = 12;
			item.useTime = 4;
			item.reuseDelay = 14;
		public override void OnConsumeAmmo(Player player)
		{
			// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (UseAmination - 1, then - useTime until less than 0.) 
			// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
			return !(player.itemAnimation < item.useAnimation - 2);
		}*/

        // How can I shoot 2 different projectiles at the same time?
        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
			Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),position.X, position.Y, velocity.X, velocity.Y,ProjectileID.GrenadeI, damage, knockback, player.whoAmI);
			// By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
			return true;
		}*/

        // How can I choose between several projectiles randomly?
        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
			type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ProjectileType<Projectiles.ExampleBullet>() });
			return true;
		}*/
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
