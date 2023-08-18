using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Consumables;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using StarsAbove.Systems;

namespace StarsAbove.Items.Weapons.Celestial
{
    public class UltimaThule : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("" +
                "[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from all damage classes]" +
                "\n[c/F7D76A:Hold left click to charge the weapon; if the weapon is fully charged, the attack varies]" +
                "\nHolding this weapon will conjure 5 [c/14E6FF:Spatial Planets] to orbit you" +
                "\nUncharged attacks cleave a colossal area with powerful spatial damage" +
                "\nCharged attacks conjure stars and a [c/FF14A6:Vorpal Planet] from the cosmos to descend upon your foes" +
                "\nWhen a [c/14E6FF:Spatial Planet] strikes an enemy, it will burst into asteroids and additionally grant [c/145BFF:Universal Manipulation] for 12 seconds" +
                "\n[c/14E6FF:Spatial Planets] respawn 2 seconds after impact" +
                "\nWhen striking a foe with an uncharged attack during [c/145BFF:Universal Manipulation], the buff becomes [c/14FFB1:Celestial Cacophony] for 12 seconds" +
                "\nWhen using a charged attack during [c/14FFB1:Celestial Cacophony] the [c/FF14A6:Vorpal Planet] deals triple damage and more stars will descend, consuming [c/14FFB1:Celestial Cacophony]" +
                "\nRight click to activate [c/FF9F14:Cosmic Conception] which will converge the planets upon yourself, granting the buff [c/EBDD8D:Superimposed]" +
                "\nAdditionally, converging spatial bodies will deal damage to any foe caught in their path" +
                "\n[c/EBDD8D:Superimposed] grants incredible health regeneration and Invincibility" +
                "\nAfter 7 seconds, [c/FF9F14:Cosmic Conception] will resolve, bursting in an explosion of cosmic energy" +
                "\n[c/FF9F14:Cosmic Conception] has a 2 minute cooldown" +
                "\n'And yet you can walk on'" +
                $""); */

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


            //Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 20));
        }

        public override void SetDefaults()
        {
             
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                Item.damage = 2500;
            }
            else
            {
                Item.damage = 444;

            }
            
            Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>(); // Makes our item use our custom damage type.

            Item.width = 300;
            Item.height = 300;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 1;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.noUseGraphic = true;
            Item.knockBack = 4;
            Item.rare = ModContent.GetInstance<StellarRarity>().Type;
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
         

        int starSpawnTimer;

        int pulsingRings = 250;

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            
            if (attackCooldown > 0 || player.HasBuff(BuffType<Buffs.Voidform>()))
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
                if (!player.HasBuff(BuffType<Buffs.CosmicConceptionCooldown>()) && !player.HasBuff(BuffType<Buffs.CosmicConception>()))
                {
                    /*Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                    Vector2 leap = Vector2.Normalize(mousePosition2) * 15f;
                    player.velocity = leap;*/
                    Vector2 placement2 = new Vector2((player.Center.X), player.Center.Y);
                    //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),placement2.X, placement2.Y, 0, 0, mod.ProjectileType("radiate"), 0, 0f, 0);
                    player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
                    //player.GetModPlayer<WeaponPlayer>().activateUltimaShockwaveEffect = true;
                    #region vfx
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, 20, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
                    }

                    for (int d = 0; d < 36; d++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, 221, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, 7, 0f + Main.rand.Next(-33, 33), 0f + Main.rand.Next(-33, 33), 150, default(Color), 1.5f);
                    }
                    
                    

                    // Play explosion sound
                    
                    // Smoke Dust spawn
                    for (int i = 0; i < 70; i++)
                    {
                        int dustIndex = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                        Main.dust[dustIndex].velocity *= 1.4f;
                    }
                    // Fire Dust spawn
                   
                    // Large Smoke Gore spawn
                    for (int g = 0; g < 4; g++)
                    {
                        int goreIndex = Gore.NewGore(null,new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null,new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null,new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                        goreIndex = Gore.NewGore(null,new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    }
                    #endregion
                    player.AddBuff(BuffType<Buffs.CosmicConception>(), 480);
                    player.AddBuff(BuffType<Buffs.Voidform>(), 590);
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_CelestialConception, player.Center);

                    if (player.ownedProjectileCounts[ProjectileType<Projectiles.UltimaThule.UltimaStar>()] < 1)
                    {
                        //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.UltimaThule.UltimaVFX2>(), 0, 0, player.whoAmI, 0f);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.UltimaThule.UltimaBurstFX>(), player.GetWeaponDamage(Item) * 2, 0, player.whoAmI, 0f);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.UltimaThule.UltimaStar>(), player.GetWeaponDamage(Item)*2, 0, player.whoAmI, 0f);
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.UltimaThule.UltimaVFX1>(), 0, 0, player.whoAmI, 0f);

                    }
                }
                return false;
            }
            


            return true;

        }
        public override void HoldItem(Player player)
        {
            player.AddBuff(BuffType<Buffs.Ultima>(), 2);
            
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.UltimaThule.UltimaPlanet1>()] < 1)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.UltimaThule.UltimaPlanet1>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);


            }
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.UltimaThule.UltimaPlanet2>()] < 1)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.UltimaThule.UltimaPlanet2>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);


            }
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.UltimaThule.UltimaPlanet3>()] < 1)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.UltimaThule.UltimaPlanet3>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);


            }
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.UltimaThule.UltimaPlanet4>()] < 1)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.UltimaThule.UltimaPlanet4>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);


            }
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.UltimaThule.UltimaPlanet5>()] < 1)
            {
                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.UltimaThule.UltimaPlanet5>(), player.GetWeaponDamage(Item), 4, player.whoAmI, 0f);


            }
            if (player.whoAmI == Main.myPlayer)
            {
                
                if(starSpawnTimer > 5)
                {
                    int randomDirection = Main.rand.Next(0, 4);
                    float Speed = 4f;  //projectile speed
                                       //Vector2 above = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                    Vector2 above = new Vector2(player.position.X + Main.rand.Next(-1200, 1200), player.position.Y - 1000 + Main.rand.Next(-200, 200));
                    Vector2 below = new Vector2(player.position.X + Main.rand.Next(-1200, 1200), player.position.Y + 1000 + Main.rand.Next(-200, 200));
                    Vector2 left = new Vector2(player.position.X - 1000 + Main.rand.Next(-200, 200), player.position.Y + Main.rand.Next(-1200, 1200));
                    Vector2 right = new Vector2(player.position.X + 1000 + Main.rand.Next(-200, 200), player.position.Y + Main.rand.Next(-1200, 1200));
                    int damage = player.GetWeaponDamage(Item);  //projectile damage
                    int type = Main.rand.Next(new int[] { ProjectileType<Projectiles.UltimaThule.UltimaStarProjectile>(), ProjectileType<Projectiles.UltimaThule.RandomPlanets>(), ProjectileType<Projectiles.UltimaThule.UltimaStarProjectile>(), ProjectileType<Projectiles.UltimaThule.UltimaStarProjectile>(), ProjectileType<Projectiles.LongAsteroid>(), ProjectileID.FairyQueenMagicItemShot, ProjectileID.FairyQueenRangedItemShot });

                    float rotation1 = (float)Math.Atan2(above.Y - (player.position.Y + (player.height * 0.5f)), above.X - (player.position.X + (player.width * 0.5f)));
                    float rotation2 = (float)Math.Atan2(below.Y - (player.position.Y + (player.height * 0.5f)), below.X - (player.position.X + (player.width * 0.5f)));
                    float rotation3 = (float)Math.Atan2(left.Y - (player.position.Y + (player.height * 0.5f)), left.X - (player.position.X + (player.width * 0.5f)));
                    float rotation4 = (float)Math.Atan2(right.Y - (player.position.Y + (player.height * 0.5f)), right.X - (player.position.X + (player.width * 0.5f)));
                    //SoundEngine.PlaySound(SoundID.Item, above);
                    if (randomDirection == 0)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),above.X, above.Y, (float)((Math.Cos(rotation1) * Speed) * -1), (float)((Math.Sin(rotation1) * Speed) * -1), type, damage, 0f, 0);
                    }
                    if (randomDirection == 1)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),below.X, below.Y, (float)((Math.Cos(rotation2) * Speed) * -1), (float)((Math.Sin(rotation2) * Speed) * -1), type, damage, 0f, 0);
                    }
                    if (randomDirection == 2)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),left.X, left.Y, (float)((Math.Cos(rotation3) * Speed) * -1), (float)((Math.Sin(rotation3) * Speed) * -1), type, damage, 0f, 0);
                    }
                    if (randomDirection == 3)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),right.X, right.Y, (float)((Math.Cos(rotation4) * Speed) * -1), (float)((Math.Sin(rotation4) * Speed) * -1), type, damage, 0f, 0);
                    }
                    starSpawnTimer = 0;
                }
                if (player.HasBuff(BuffType<Buffs.CosmicConception>()))
                {
                    starSpawnTimer++;
                }
                if (!player.HasBuff(BuffType<Buffs.Voidform>()))
                {
                    Vector2 vector = new Vector2(
                   Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
                   Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
                    Dust d = Main.dust[Dust.NewDust(
                        player.MountedCenter + vector, 1, 1,
                        20, 0, 0, 255,
                        new Color(0.8f, 0.4f, 1f), 0.8f)];
                    d.velocity = -vector / 440;
                    d.velocity -= player.velocity / 2;
                    d.noLight = true;
                    d.noGravity = true;
                    
                    for (int i = 0; i < 20; i++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * (100 - 250));
                        offset.Y += (float)(Math.Cos(angle) * (100 - 250));

                        Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 20, player.velocity, 200, default(Color), 0.5f);
                        d2.fadeIn = 0.1f;
                        d2.noGravity = true;
                        
                    }
                    for (int i = 0; i < 20; i++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * (100 - 300));
                        offset.Y += (float)(Math.Cos(angle) * (100 - 300));

                        Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 20, player.velocity, 200, default(Color), 0.5f);
                        d2.fadeIn = 0.1f;
                        d2.noGravity = true;
                    }
                    for (int i = 0; i < 20; i++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * (100 - 350));
                        offset.Y += (float)(Math.Cos(angle) * (100 - 350));

                        Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 20, player.velocity, 200, default(Color), 0.5f);
                        d2.fadeIn = 0.1f;
                        d2.noGravity = true;
                    }
                    for (int i = 0; i < 20; i++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * (100 - 400));
                        offset.Y += (float)(Math.Cos(angle) * (100 - 400));

                        Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 20, player.velocity, 200, default(Color), 0.5f);
                        d2.fadeIn = 0.1f;
                        d2.noGravity = true;
                    }
                    for (int i = 0; i < 20; i++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * (100 - 450));
                        offset.Y += (float)(Math.Cos(angle) * (100 - 450));

                        Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 20, player.velocity, 200, default(Color), 0.5f);
                        d2.fadeIn = 0.1f;
                        d2.noGravity = true;

                    }
                    
                }
               

                chargeCooldown--;
                attackCooldown--;
                swapCooldown--;
                float launchSpeed = 5f;
                
                float launchSpeed2 = 182f;

                float launchSpeed3 = 126f;
                float launchSpeed4 = 25f;
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


                        //player.GetModPlayer<WeaponPlayer>().duality += 10;
                        SoundEngine.PlaySound(SoundID.Item1, player.position);
                        player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
                        player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
                        //Vector2 mousePosition2 = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                        //Vector2 leap = Vector2.Normalize(mousePosition2) * 15f;
                        //player.velocity = leap;
                        //attackCooldown = 60;
                        int numberProjectiles;
                        int numberProjectiles2;
                        int numberProjectiles3;
                        Vector2 position = new Vector2(player.Center.X, player.Center.Y - 600);
                        float rotation = (float)Math.Atan2(position.Y - (Main.MouseWorld.Y), position.X - (Main.MouseWorld.X));//Aim towards mouse
                        Vector2 skyDirection = Vector2.Normalize(mousePosition - new Vector2(player.Center.X, player.Center.Y - 600));
                        Vector2 meteorVelocity = skyDirection * launchSpeed4;
                        //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<UltimaStab>(), 0, 0, player.whoAmI);
                        if (player.HasBuff(BuffType<CelestialCacophony>()))
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X + Main.rand.Next(-100, 100), player.Center.Y - 600, meteorVelocity.X, meteorVelocity.Y, ProjectileType<Projectiles.UltimaThule.RandomPlanetProjectile>(), player.GetWeaponDamage(Item) * 3, 5, player.whoAmI, 0, 0);

                            numberProjectiles = 7 + Main.rand.Next(5); //random shots
                            numberProjectiles2 = 3 + Main.rand.Next(5); //random shots
                            numberProjectiles3 = 3 + Main.rand.Next(5); //random shots

                            int index = player.FindBuffIndex(BuffType<CelestialCacophony>());
                            if (index > -1)
                            {
                                player.DelBuff(index);
                            }
                        }
                        else
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.Center.X + Main.rand.Next(-100, 100), player.Center.Y - 600, meteorVelocity.X, meteorVelocity.Y, ProjectileType<Projectiles.UltimaThule.RandomPlanetProjectile>(), player.GetWeaponDamage(Item), 5, player.whoAmI, 0, 1);

                            numberProjectiles = 2 + Main.rand.Next(2); //random shots
                            numberProjectiles2 = 3 + Main.rand.Next(2); //random shots
                            numberProjectiles3 = 0; //random shots
                        }
                        
                        

                       


                        for (int d = 0; d < 25; d++)
                        {
                            float Speed2 = Main.rand.NextFloat(10, 98);  //projectile speed
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
                            int dustIndex = Dust.NewDust(position, 0, 0, 219, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                            Main.dust[dustIndex].noGravity = true;
                        }//Dust
                        for (int d = 0; d < 45; d++)
                        {
                            float Speed3 = Main.rand.NextFloat(8, 94);  //projectile speed
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(60)); // 30 degree spread.
                            int dustIndex = Dust.NewDust(position, 0, 0, 86, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
                            Main.dust[dustIndex].noGravity = true;
                        }//Dust
                        for (int d = 0; d < 25; d++)
                        {
                            float Speed2 = Main.rand.NextFloat(10, 98);  //projectile speed
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
                            int dustIndex = Dust.NewDust(position, 0, 0, 221, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                            Main.dust[dustIndex].noGravity = true;
                        }//Dust
                        for (int d = 0; d < 45; d++)
                        {
                            float Speed3 = Main.rand.NextFloat(8, 94);  //projectile speed
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed3) * -1), (float)((Math.Sin(rotation) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(60)); // 30 degree spread.
                            int dustIndex = Dust.NewDust(position, 0, 0, 88, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
                            Main.dust[dustIndex].noGravity = true;
                        }//Dust

                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = meteorVelocity.RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                                                                                                               // If you want to randomize the speed to stagger the projectiles
                            float scale = 1f - (Main.rand.NextFloat() * .9f);
                            perturbedSpeed = perturbedSpeed * scale;
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X + Main.rand.Next(-100, 100), player.MountedCenter.Y - 600, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<Projectiles.UltimaThule.UltimaStarProjectile2>(), player.GetWeaponDamage(Item) / 3, 5, player.whoAmI);
                        }
                        for (int i = 0; i < numberProjectiles2; i++)
                        {
                            Vector2 perturbedSpeed = meteorVelocity.RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                                                                                                               // If you want to randomize the speed to stagger the projectiles
                            float scale = 1f - (Main.rand.NextFloat() * .9f);
                            perturbedSpeed = perturbedSpeed * scale;
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X + Main.rand.Next(-100, 100), player.MountedCenter.Y - 600, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.NebulaBlaze1, player.GetWeaponDamage(Item) / 3, 5, player.whoAmI);
                        }
                        for (int i = 0; i < numberProjectiles3; i++)
                        {
                            Vector2 perturbedSpeed = meteorVelocity.RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                                                                                                               // If you want to randomize the speed to stagger the projectiles
                            float scale = 1f - (Main.rand.NextFloat() * .9f);
                            perturbedSpeed = perturbedSpeed * scale;
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X + Main.rand.Next(-100, 100), player.MountedCenter.Y - 600, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.NebulaBlaze2, player.GetWeaponDamage(Item) / 3, 5, player.whoAmI);
                        }
                        for (int i = 0; i < numberProjectiles2; i++)
                        {
                            Vector2 perturbedSpeed = meteorVelocity.RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                                                                                                               // If you want to randomize the speed to stagger the projectiles
                            float scale = 1f - (Main.rand.NextFloat() * .9f);
                            perturbedSpeed = perturbedSpeed * scale;
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X + Main.rand.Next(-100, 100), player.MountedCenter.Y - 600, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.FairyQueenMagicItemShot, player.GetWeaponDamage(Item) / 3, 5, player.whoAmI);
                        }
                        for (int i = 0; i < numberProjectiles3; i++)
                        {
                            Vector2 perturbedSpeed = meteorVelocity.RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
                                                                                                               // If you want to randomize the speed to stagger the projectiles
                            float scale = 1f - (Main.rand.NextFloat() * .9f);
                            perturbedSpeed = perturbedSpeed * scale;
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.MountedCenter.X + Main.rand.Next(-100, 100), player.MountedCenter.Y - 600, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.FairyQueenRangedItemShot, player.GetWeaponDamage(Item) / 3, 5, player.whoAmI);
                        }
                        //player.GetModPlayer<WeaponPlayer>().duality -= 10;

                    }
                    else
                    {
                        if (player.GetModPlayer<WeaponPlayer>().bowCharge > 0)//Not Fully Charged normal
                        {//
                            SoundEngine.PlaySound(SoundID.Item1, player.position);

                            player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
                            player.GetModPlayer<WeaponPlayer>().bowCharge = 0;


                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<UltimaSwing2>(), 0, 3, player.whoAmI, 0f);
                            if (player.HasBuff(BuffType<UniversalManipulation>()))
                            {
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<UltimaSwing1>(), player.GetWeaponDamage(Item), 3, player.whoAmI, 0f);

                            }
                            else
                            {
                                Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity3.X, arrowVelocity3.Y, ProjectileType<UltimaSwing1>(), player.GetWeaponDamage(Item) / 3, 3, player.whoAmI, 0f);

                            }
                            
                        }
                    }
                   
                }


                
            }
        }
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
        public override void AddRecipes()
        {
            

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                CreateRecipe(1)
                    .AddIngredient(calamityMod.Find<ModItem>("ShadowspecBar").Type, 5)
                    .AddIngredient(ItemType<SpatialMemoriam>())
                    .AddTile(TileID.Anvils)
                    .Register();
               
            }
            else
            {


                CreateRecipe(1)
                .AddIngredient(ItemType<SpatialMemoriam>())
                .AddTile(TileID.Anvils)
                .Register();
            }

            
        }

        public override void PostUpdate()
        {
            // Spawn some light and dust when dropped in the world
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                // This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
                dust.scale = 0.5f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            // Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
            {
                // In case this item is animated, this picks the correct frame
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            }
            else
            {
                frame = texture.Frame();
            }

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }
        
        

    }
}


