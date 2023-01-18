using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using Terraria.Audio;

namespace StarsAbove.Items
{
    public class Xenoblade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("" +
                "[c/F7D76A:Hold left click to charge the weapon; if the weapon is fully charged, the attack varies]" +
                "\nUncharged attacks slash in a wide area and will restore 3 mana" +
                "\nCharged attacks deal double damage, have extraordinary knockback, and range is extended" +
                "\nPreparing charge attacks drain mana"+
                "\nAfter 2 uncharged attacks in quick succession, the next charged attack is prepared incredibly fast" +
                "\nRight click to cycle [c/E35D5D:Xenoblade Arts] between [c/C80000:Smash], [c/52BEF3:Speed], [c/E3D55D:Shield], [c/40EF28:Jump], and [c/9923CD:Buster]" +
                "\nThe current [c/E35D5D:Xenoblade Art] will grant a specific buff in the stance" +
                "\n[c/C80000:Smash] grants Titan when active, and charged attacks are upgraded into [c/C80000:Mechon's Bane] guaranteeing critical damage" +
                "\n[c/52BEF3:Speed] grants Swiftness when active and disables charged attacks, but normal attacks are stronger and faster" +
                "\n[c/E3D55D:Shield] grants Endurance when active, and you gain 18 defense when holding a readied charged attack and mana drain is stopped" +
                "\n[c/40EF28:Jump] grants Featherfall when active, and charged attacks launch you forward, costing 40 mana (Featherfall is lost below 100 mana)" +
                "\n[c/9923CD:Buster] grants Wrath when active and disables normal attacks, but charged attacks are faster" +
                "\n'THIS is the Monado's power!'" +
                $"");

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 76;
            Item.DamageType = DamageClass.Melee;
            Item.mana = 1;
            Item.width = 65;
            Item.height = 70;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.noUseGraphic = true;
            Item.knockBack = 4;
            Item.rare = ItemRarityID.LightPurple;
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
        int form;//0 = Buster | 1 = Speed | 2 = Shield
        int attackCooldown;
        int chargeCooldown;
        bool chargeFinishMsg;
         
        int comboTimer;
        bool swing1;
        bool swing2;

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
            if (!player.inventory[58].IsAir)
            {
                return false;
            }
            if (player.altFunctionUse == 2)
            {
                if(swapCooldown <= 0)
                {
                    swapCooldown = 20;
                    if (form == 0)
                    {
                        form = 1;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("MonadoSpeed").Type, 0, 0, player.whoAmI, 0, 1);
                        return true;
                    }
                    if(form == 1)
                    {
                        form = 2;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("MonadoShield").Type, 0, 0, player.whoAmI, 0, 1);
                        return true;
                    }
                    if(form == 2)
                    {
                        form = 3;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("MonadoJump").Type, 0, 0, player.whoAmI, 0, 1);
                        return true;
                    }
                    if (form == 3)
                    {
                        form = 4;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("MonadoBuster").Type, 0, 0, player.whoAmI, 0, 1);
                        return true;

                    }
                    if (form == 4)
                    {
                        form = 0;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("MonadoSmash").Type, 0, 0, player.whoAmI, 0, 1);
                        return true;
                    }

                }
                else
                {
                    return false;
                }
            }

            return true;

        }
        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (form == 0)
                {
                    player.AddBuff(BuffID.Titan, 2);
                }
                if (form == 4)
                {
                    player.AddBuff(BuffID.Wrath, 2);
                }
                if(form == 1)
                {
                    player.AddBuff(BuffID.Swiftness, 2);
                }
                if(form == 2)
                {
                    player.AddBuff(BuffID.Endurance, 2);
                }
                if (form == 3 && player.statMana > 100)
                {
                    player.AddBuff(BuffID.Featherfall, 2);
                }
                comboTimer--;
                chargeCooldown--;
                attackCooldown--;
                swapCooldown--;
                //player.statMana++;
                if (comboTimer <= 0)
                {
                    swing1 = false;
                    swing2 = false;
                }
                float launchSpeed = 36f;
                
                float launchSpeed2 = 142f;

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
                    comboTimer++;
                    if(form != 1)
                    {
                        player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = true;
                    }
                    else
                    {
                        player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                    }
                   
                    if (form == 2 && player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100)
                    {
                        player.statDefense += 18;
                        player.statMana++;
                    }
                    if (form == 3)
                    {
                       
                    }
                    if (swing1 && swing2 && form != 1)
                    {
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge += 10;

                    }
                    else
                    {
                        if(form ==4)
                        {
                            player.GetModPlayer<StarsAbovePlayer>().bowCharge += 4;
                        }
                        else
                        {
                            if(form == 1)
                            {
                                player.GetModPlayer<StarsAbovePlayer>().bowCharge = 1;
                                player.statMana++;
                            }
                            else
                            {
                                player.GetModPlayer<StarsAbovePlayer>().bowCharge += 2;
                            }
                            
                        }
                       

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
                        if (form == 2)
                        {
                            Dust.NewDust(player.Center, 0, 0, 159, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
                        }
                        else
                        {
                            Dust.NewDust(player.Center, 0, 0, 20, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
                        }

                        
                        
                    }

                }
                else // ATTACKS BEGIN HERE.
                {
                    if(form != 1)
                    {
                        Item.useTime = 30;
                        Item.useAnimation = 30;
                    }
                    else
                    {
                        Item.useTime = 20;
                        Item.useAnimation = 20;
                    }
                  

                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100 && form == 0)//Smash
                    {

                        SoundEngine.PlaySound(SoundID.Item1, player.position);
                        player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity2.X, arrowVelocity2.Y, ProjectileType<MonadoEmpoweredCritSwing>(), player.GetWeaponDamage(Item)*2, 14, player.whoAmI, 0f);
                        comboTimer = 0;
                        attackCooldown = 20;
                        // Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, 0, 0, ProjectileType<HullwroughtSpinDamage>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);



                    }
                    else
                    {
                        if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && form == 0)//Smash
                        {//
                            SoundEngine.PlaySound(SoundID.Item1, player.position);

                            player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                            player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                            player.statMana += 3;
                            if(currentSwing == 0)
                            {
                                comboTimer = 60;
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<MonadoSwing1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                                swing1 = true;
                                currentSwing++;
                            }
                            else
                            {
                                comboTimer = 60;
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<MonadoSwing2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                                swing2 = true;
                                currentSwing = 0;
                            }
                            


                        }
                    }
                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100 && form == 3)//Jump Charged
                    {
                        Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                        Vector2 leap = Vector2.Normalize(mousePosition2) * 12f;
                        player.velocity = leap;
                        SoundEngine.PlaySound(SoundID.Item1, player.position);
                        player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                        player.statMana -= 40;
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity2.X, arrowVelocity2.Y, ProjectileType<MonadoEmpoweredSwing>(), player.GetWeaponDamage(Item) * 2, 14, player.whoAmI, 0f);
                        comboTimer = 0;
                        attackCooldown = 20;
                    }
                    else
                    {
                        if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && form == 3)//Jump Uncharged
                        {//
                         //Main.PlaySound(SoundID.Item1, player.position);


                            SoundEngine.PlaySound(SoundID.Item1, player.position);
                            player.statMana += 3;
                            player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                            player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                            if (currentSwing == 0)
                            {
                                comboTimer = 60;
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<MonadoSwing1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                                swing1 = true;
                                currentSwing++;
                            }
                            else
                            {
                                comboTimer = 60;
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<MonadoSwing2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                                swing2 = true;
                                currentSwing = 0;
                            }

                        }
                    }
                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100 && form == 2)//Shield Form Charged
                    {

                        SoundEngine.PlaySound(SoundID.Item1, player.position);
                        player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity2.X, arrowVelocity2.Y, ProjectileType<MonadoEmpoweredSwing>(), player.GetWeaponDamage(Item) * 2, 14, player.whoAmI, 0f);
                        comboTimer = 0;
                        attackCooldown = 20;
                    }
                    else
                    {
                        if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && form == 2)//Shield Form Uncharged
                        {//
                         //Main.PlaySound(SoundID.Item1, player.position);


                            SoundEngine.PlaySound(SoundID.Item1, player.position);
                            player.statMana += 3;
                            player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                            player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                            if (currentSwing == 0)
                            {
                                comboTimer = 60;
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<MonadoSwing1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                                swing1 = true;
                                currentSwing++;
                            }
                            else
                            {
                                comboTimer = 60;
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<MonadoSwing2>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);
                                swing2 = true;
                                currentSwing = 0;
                            }

                        }
                    }
                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100 && form == 1)//Speed
                    {

                        
                    }
                    else
                    {
                        if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && form == 1)//Speed
                        {//
                         //Main.PlaySound(SoundID.Item1, player.position);


                            SoundEngine.PlaySound(SoundID.Item1, player.position);
                            player.statMana += 3;
                            player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                            player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                            if (currentSwing == 0)
                            {
                                comboTimer = 60;
                                
                                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<MonadoSwing1>(), player.GetWeaponDamage(Item) + 24, 3, player.whoAmI, 0f);
                                
                                
                                
                                swing1 = true;
                                currentSwing++;
                            }
                            else
                            {
                                comboTimer = 60;
                                
                                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<MonadoSwing2>(), player.GetWeaponDamage(Item) + 24, 3, player.whoAmI, 0f);
                            
                                swing2 = true;
                                currentSwing = 0;
                            }

                        }

                    }
                    if (player.GetModPlayer<StarsAbovePlayer>().bowCharge >= 100 && form == 4)//Buster Charged
                    {

                        SoundEngine.PlaySound(SoundID.Item1, player.position);
                        player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                        player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity2.X, arrowVelocity2.Y, ProjectileType<MonadoEmpoweredSwing>(), player.GetWeaponDamage(Item) * 2, 14, player.whoAmI, 0f);
                        comboTimer = 0;
                        attackCooldown = 20;
                    }
                    else
                    {
                        if (player.GetModPlayer<StarsAbovePlayer>().bowCharge > 0 && form == 4)//Buster Uncharged
                        {//
                         //Main.PlaySound(SoundID.Item1, player.position);
                            player.GetModPlayer<StarsAbovePlayer>().bowChargeActive = false;
                            player.GetModPlayer<StarsAbovePlayer>().bowCharge = 0;



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
            CreateRecipe(1)
                .AddIngredient(ItemType<PrismaticCore>(), 3)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.Ruby, 5)
                .AddIngredient(ItemType<EssenceOfTheBionis>())
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
