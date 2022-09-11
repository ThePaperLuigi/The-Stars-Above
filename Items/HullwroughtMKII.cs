using Microsoft.Xna.Framework;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using StarsAbove.Projectiles;
using System;
using Terraria.Localization;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace StarsAbove.Items
{
    public class HullwroughtMKII : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hullwrought MK. II"); // Add the Nebula Blaze as a ingredient for the recipie
            Tooltip.SetDefault("" +
                "[c/F7D76A:Hold left click to charge the weapon; if the weapon is fully charged, the attack varies]" +
                "\nRight click to swap between [c/FF7C00:Blasting Form], [c/00CDFF:Striking Form], and [c/BE5CC8:Mystic Form] with 1/3rd second cooldown" +
                "\nIn [c/00CDFF:Striking Form], attacks focus on close-ranged damage" +
                "\nUncharged attacks will slash in a huge area" +
                "\nCharged attacks propel you forward while swinging in a large circle around you, dealing double damage" +
                "\nIn [c/FF7C00:Blasting Form], attacks focus on slow, powerful ranged attacks" +
                "\nUncharged attacks are disabled" +
                "\nCharged attacks take much longer to perform, but instead fires a powerful projectile that stuns foes for 1/3rd second on a critical strike" +
                "\nHolding charge attack enhances the attack every 1/2 second, capping at 10 stacks" +
                "\nIf the charged attack has 5 stacks or more, it will always be a critical hit" +
                "\nIn [c/BE5CC8:Mystic Form], attacks focus on magical burst attacks, costing 20 Mana per shot" +
                "\nUncharged attacks fire weak homing projectiles" +
                "\nCharged attacks fire powerful blasts of energy that travel slowly and deal damage over time" +
                "\nCharged attacks require 80 Mana" +
                "\nGain Mana Regeneration when not in [c/BE5CC8:Mystic Form]" +
                $"");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                Item.damage = 455;
            }
            else
            {
                Item.damage = 155;
            }

            Item.DamageType = DamageClass.Melee;
            Item.width = 65;
            Item.height = 70;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.noUseGraphic = true;
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Red;
            //item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.channel = true;//Important for all bows
            Item.shoot = 10;
            Item.shootSpeed = 15f;
            Item.value = Item.buyPrice(gold: 1);
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
            if(attackCooldown > 0)
            {
                return false;
            }
            //if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1) // Asphodene
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

            if (player.altFunctionUse == 2)
            {
                if(form == 0 && swapCooldown <= 0)
                {
                    swapCooldown = 20;
                    form = 1;
                    if (player.whoAmI == Main.myPlayer)
                    {
                        Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                        CombatText.NewText(textPos, new Color(255, 125, 0, 240), "Blasting Form", false, false);
                    }
                    
                }
                if(form == 1 && swapCooldown <= 0)
                {
                    swapCooldown = 20;
                    form = 2;
                    if (player.whoAmI == Main.myPlayer)
                    {
                        Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                        CombatText.NewText(textPos, new Color(255, 125, 0, 240), "Mystic Form", false, false);
                    }
                    
                }
                if (form == 2 && swapCooldown <= 0)
                {
                    swapCooldown = 20;
                    form = 0;
                    if (player.whoAmI == Main.myPlayer)
                    {
                        Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                        CombatText.NewText(textPos, new Color(255, 125, 0, 240), "Striking Form", false, false);
                    }

                }
            }

            return true;

        }
        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                chargeCooldown--;
                attackCooldown--;
                swapCooldown--;
                float launchSpeed = 36f;
                
                float launchSpeed2 = 102f;

                float launchSpeed3 = 126f;

                float launchSpeed4 = 4f;
                Vector2 mousePosition = Main.MouseWorld;
                Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                Vector2 arrowVelocity = direction * launchSpeed;
                Vector2 arrowVelocity2 = direction * launchSpeed2;

                Vector2 arrowVelocity3 = direction * launchSpeed3;

                Vector2 arrowVelocity4 = direction * launchSpeed4;

                if(form!=2)
                {
                    player.AddBuff(BuffID.ManaRegeneration, 2);
                }

                if (player.channel && swapCooldown <= 0)
                {
                    if (form != 0)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<HullwroughtStab2>(), 0, 3, player.whoAmI, 0f);
                    }
                    else
                    {


                    }
                    Item.useTime = 2;
                    Item.useAnimation = 2;

                    player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = true;
                    if (form == 1)
                    {
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge += 1;

                    }
                    else
                    {
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge += 2;

                    }
                    


                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 1)
                    {

                        //Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/bowstring"), 0.5f);
                    }
                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge == 99)
                    {

                        for (int d = 0; d < 88; d++)
                        {
                            Dust.NewDust(player.Center, 0, 0, 43, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 0.8f);
                        }
                        //Main.PlaySound(SoundID.Item52, player.position);
                    }
                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge < 100)
                    {

                        for (int i = 0; i < 30; i++)
                        {//Circle
                            Vector2 offset = new Vector2();
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));
                            offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<StarsAbovePlayer>().bowCharge));

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
                    else
                    {
                        if (player.GetModPlayer<StarsAbovePlayer>().empoweredHullwroughtShot >= 5)
                        {
                            Dust.NewDust(player.Center, 0, 0, 159, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
                        }
                        else
                        {
                            Dust.NewDust(player.Center, 0, 0, 20, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
                        }

                        if (form == 1 && chargeCooldown <= 0)
                        {
                            chargeCooldown = 30;
                            if (player.GetModPlayer<StarsAbovePlayer>().empoweredHullwroughtShot < 10)
                            {
                                player.GetModPlayer<StarsAbovePlayer>().empoweredHullwroughtShot++;
                                if (player.whoAmI == Main.myPlayer)
                                {
                                    if(player.GetModPlayer<StarsAbovePlayer>().empoweredHullwroughtShot >= 5)
                                    {
                                        Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                                        CombatText.NewText(textPos, new Color(255, 255, 0, 240), $"{player.GetModPlayer<StarsAbovePlayer>().empoweredHullwroughtShot}", false, false);
                                    }
                                    else
                                    {
                                        Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                                        CombatText.NewText(textPos, new Color(122, 122, 0, 240), $"{player.GetModPlayer<StarsAbovePlayer>().empoweredHullwroughtShot}", false, false);
                                    }
                                    SoundEngine.PlaySound(StarsAboveAudio.SFX_HullwroughtLoad, player.Center);
                                    
                                }

                            }
                            else
                            {
                                if (!chargeFinishMsg)
                                {
                                    if (player.whoAmI == Main.myPlayer)
                                    {
                                        Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                                        CombatText.NewText(textPos, new Color(255, 124, 0, 240), "Fully charged!", false, false);
                                    }
                                    chargeFinishMsg = true;
                                }

                            }
                        }
                    }

                }
                else
                {

                    Item.useTime = 20;
                    Item.useAnimation = 20;

                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100 && form == 0)//Fully Charged Normal attack
                    {

                        SoundEngine.PlaySound(SoundID.Item1, player.position);
                        player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                        Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                        Vector2 leap = Vector2.Normalize(mousePosition2) * 15f;
                        player.velocity = leap;
                        attackCooldown = 60;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity2.X, arrowVelocity2.Y, ProjectileType<HullwroughtSpin2>(), player.GetWeaponDamage(Item) * 2, 6, player.whoAmI, 0f);
                        // Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<HullwroughtSpinDamage>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);



                    }
                    else
                    {
                        if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && form == 0)//Not Fully Charged normal
                        {//
                            SoundEngine.PlaySound(SoundID.Item1, player.position);

                            player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                            player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                            if(currentSwing == 0)
                            {
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<HullwroughtSwing12>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                                currentSwing++;
                            }
                            else
                            {
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<HullwroughtSwing22>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                                currentSwing = 0;
                            }
                            


                        }
                    }
                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100 && form == 1)//Fully Charged Right Click
                    {
                        player.GetModPlayer<StarsAbovePlayer>().savedHullwroughtShot = player.GetModPlayer<StarsAbovePlayer>().empoweredHullwroughtShot;
                        player.GetModPlayer<StarsAbovePlayer>().empoweredHullwroughtShot = 0;
                        Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                        Vector2 leap = Vector2.Normalize(mousePosition2) * -6f;
                        player.velocity += leap;
                        SoundEngine.PlaySound(SoundID.Item1, player.position);
                        player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;

                        SoundEngine.PlaySound(StarsAboveAudio.SFX_HullwroughtBlast, player.Center);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity2.X, arrowVelocity2.Y, ProjectileType<HullwroughtRound>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                        chargeFinishMsg = false;



                    }
                    else
                    {
                        if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && form == 1)//Not Fully Charged Right Click
                        {//
                         //Main.PlaySound(SoundID.Item1, player.position);


                            player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                            player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                            
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SteelTempestSwing>(), player.GetWeaponDamage(Item) / 2, 3, player.whoAmI, 0f);


                        }
                    }
                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100 && form == 2)//Fully Charged Mystic
                    {
                        player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                        if(player.statMana >= 80)
                        {
                            player.manaRegenDelay = 120;
                            player.statMana -= 80;
                            Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                            Vector2 leap = Vector2.Normalize(mousePosition2) * -6f;
                            player.velocity += leap;
                            SoundEngine.PlaySound(SoundID.Item1, player.position);


                            SoundEngine.PlaySound(StarsAboveAudio.SFX_HullwroughtBlast, player.Center);
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity4.X, arrowVelocity4.Y, ProjectileID.NebulaArcanum, player.GetWeaponDamage(Item), 13, player.whoAmI, 0f);
                        }
                      
                        //chargeFinishMsg = false;



                    }
                    else
                    {
                        if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && form == 2)//Not Fully Charged Mystic
                        {//
                         //Main.PlaySound(SoundID.Item1, player.position);
                            player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                            player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                            if (player.statMana >= 20)
                            {
                                player.manaRegenDelay = 120;
                                player.statMana -= 20;
                                SoundEngine.PlaySound(StarsAboveAudio.SFX_HullwroughtBlast, player.Center);
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity4.X, arrowVelocity4.Y, ProjectileID.NebulaBlaze2, player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                            }
                            
                            
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SteelTempestSwing>(), player.GetWeaponDamage(Item) / 2, 3, player.whoAmI, 0f);


                        }
                    }
                }


                //item.shootSpeed = 8f + (int)Math.Round(player.GetModPlayer<StarsAbovePlayer>().bowCharge / 10);
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
            
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
               
                CreateRecipe(1)
                   .AddIngredient(calamityMod.Find<ModItem>("DivineGeode").Type, 8)
                   .AddIngredient(ItemType<PrismaticCore>(), 12)
                   .AddIngredient(ItemID.NebulaArcanum, 1)
                   .AddIngredient(ItemID.NebulaBlaze, 1)
                   .AddIngredient(ItemID.LunarBar, 18)
                   .AddIngredient(ItemType<Hullwrought>())
                   .AddTile(TileID.Anvils)
                   .Register();
            }
            else
            {

            CreateRecipe(1)
               .AddIngredient(ItemType<PrismaticCore>(), 12)
               .AddIngredient(ItemID.NebulaArcanum, 1)
               .AddIngredient(ItemID.NebulaBlaze, 1)
               .AddIngredient(ItemID.LunarBar, 18)
               .AddIngredient(ItemType<Hullwrought>())
               .AddTile(TileID.Anvils)
               .Register();
            }



        }
    }
}
