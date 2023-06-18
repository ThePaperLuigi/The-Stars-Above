using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Projectiles;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using Terraria.Audio;

namespace StarsAbove.Items
{
    public class Unforgotten : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("" +
                "[c/F7D76A:Hold left click to charge the weapon; if the weapon is fully charged, the attack varies]" +
                "\nUncharged attacks will alternate between [c/C2E6C1:Mortal Steel] and [c/F10079:Spirit Azakana]" +
                "\n[c/C2E6C1:Mortal Steel] has a 50% critical hit rate independent of other crit calculations" +
                "\n[c/F10079:Spirit Azakana] has no knockback and can not crit but deals half of the damage again as true damage" +
                "\nCharged attacks consume 30 mana and change depending on the preluding slash" +
                "\nIf the last attack was [c/C2E6C1:Mortal Steel], the charge attack will inflict Mortal Wounds, which deals damage over time for 10 seconds" +
                "\nIf the last attack was [c/F10079:Spirit Azakana], the charge attack will deal 20x critical damage to Mortally Wounded foes and purge the debuff" +
                "\nRight click to consume 40 mana to perform [c/8E60D1:Soul Unbound]" +
                "\n[c/8E60D1:Soul Unbound] will launch you forwards and grant movement speed for 8 seconds" +
                "\nOnce [c/8E60D1:Soul Unbound] ends, all nearby foes take damage equal to 1/3 of damage you inflicted with this weapon during [c/8E60D1:Soul Unbound]" +
                "\nOnce [c/8E60D1:Soul Unbound] ends, you will be forced to return to the position where [c/8E60D1:Soul Unbound] was casted, and attacks will be disabled" +
                "\n[c/8E60D1:Soul Unbound] has a 22 second cooldown" +
                "\n'One to cut, one to seal'" +
                $""); */

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 189;
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
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            

            for (int i = 0; i < player.CountBuffs(); i++)
                if (player.buffType[i] == BuffType<Buffs.SoulUnbound>())
                {
                    if (player.buffTime[i] <= 40)
                    {
                        return false;
                    }
                }


            if (player.altFunctionUse == 2)
            {
                if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.SoulUnbound>()) && !Main.LocalPlayer.HasBuff(BuffType<Buffs.SoulUnboundCooldown>()))
                {
                    Vector2 mousePosition = player.DirectionTo(player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                    Vector2 leap = Vector2.Normalize(mousePosition) * 15f;
                    player.velocity = leap;

                    player.AddBuff(BuffType<Buffs.SoulUnbound>(), 480);

                    //player.GetModPlayer<WeaponPlayer>().phantomTeleport = true;
                    //Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/TeleportPrep"));
                    player.GetModPlayer<WeaponPlayer>().soulUnboundLocation = new Vector2(player.Center.X, player.Center.Y - 5);
                    vector32 = new Vector2(player.Center.X, player.Center.Y - 5);
                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),vector32.X, vector32.Y, 0, 0, ProjectileType<Projectiles.SoulMarker>(), 0, 0, player.whoAmI, 0f);
                    player.GetModPlayer<WeaponPlayer>().soulUnboundActive = true;


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
            float launchSpeed = 59f;
            float launchSpeed2 = 12f;
            Vector2 mousePosition = Main.MouseWorld;
            Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
            Vector2 arrowVelocity = direction * launchSpeed;
            Vector2 arrowVelocity2 = direction * launchSpeed2;

            if (player.channel)
            {
                Item.useTime = 2;
                Item.useAnimation = 2;

                player.GetModPlayer<WeaponPlayer>().bowChargeActive = true;
                player.GetModPlayer<WeaponPlayer>().bowCharge += 3;
                if (player.statMana < 30)
                {
                    player.GetModPlayer<WeaponPlayer>().bowCharge = 1;
                }


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
                    if (currentSwing == 1)
                    {

                        for (int i = 0; i < 30; i++)
                        {//Circle
                            Vector2 offset = new Vector2();
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));
                            offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));

                            Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 266, player.velocity, 200, default(Color), 0.5f);
                            d2.fadeIn = 0.1f;
                            d2.noGravity = true;
                        }
                    }
                    else
                    {

                        for (int i = 0; i < 30; i++)
                        {//Circle
                            Vector2 offset = new Vector2();
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            offset.X += (float)(Math.Sin(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));
                            offset.Y += (float)(Math.Cos(angle) * (100 - player.GetModPlayer<WeaponPlayer>().bowCharge));

                            Dust d2 = Dust.NewDustPerfect(player.MountedCenter + offset, 57, player.velocity, 200, default(Color), 0.5f);
                            d2.fadeIn = 0.1f;
                            d2.noGravity = true;
                        }
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
                    Dust.NewDust(player.Center, 0, 0, 20, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
                }
            }
            else
            {
                Item.useTime = 20;
                Item.useAnimation = 20;

                if (player.GetModPlayer<WeaponPlayer>().bowCharge >= 100 && player.statMana >= 30)//Fully Charged
                {
                    player.statMana -= 30;
                    player.manaRegenDelay = 230;
                    SoundEngine.PlaySound(SoundID.Item1, player.position);
                    player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
                    player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
                    if (currentSwing == 1)
                    {

                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity2.X, arrowVelocity2.Y, ProjectileType<SteelTempestSwing4>(), Item.damage/2, 3, player.whoAmI, 0f);
                    }
                    else
                    {

                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity2.X, arrowVelocity2.Y, ProjectileType<SteelTempestSwing3>(), Item.damage/2, 3, player.whoAmI, 0f);
                    }



                }
                else
                {
                    if (player.GetModPlayer<WeaponPlayer>().bowCharge > 0)//Not Fully Charged
                    {//
                        SoundEngine.PlaySound(SoundID.Item1, player.position);

                        player.GetModPlayer<WeaponPlayer>().bowChargeActive = false;
                        player.GetModPlayer<WeaponPlayer>().bowCharge = 0;
                        if (currentSwing == 1)
                        {
                            currentSwing++;
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SteelTempestSwing>(), Item.damage/2, 3, player.whoAmI, 0f);
                        }
                        else
                        {
                            currentSwing = 1;
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem),player.MountedCenter.X, player.MountedCenter.Y, arrowVelocity.X, arrowVelocity.Y, ProjectileType<SteelTempestSwing2>(), Item.damage/2, 0, player.whoAmI, 0f);
                        }

                    }
                }
            }

            if (player.GetModPlayer<WeaponPlayer>().soulUnboundActive)
            {

                for (int i = 0; i < 50; i++)
                {
                    Vector2 position = Vector2.Lerp(player.Center, vector32, (float)i / 50);
                    Dust d = Dust.NewDustPerfect(position, 20, null, 240, default(Color), 0.3f);
                    d.fadeIn = 0.3f;
                    d.noLight = true;
                    d.noGravity = true;
                }
            }
           
            //item.shootSpeed = 8f + (int)Math.Round(player.GetModPlayer<WeaponPlayer>().bowCharge / 10);
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
                .AddIngredient(ItemType<PrismaticCore>(), 20)
                .AddIngredient(ItemID.FragmentSolar, 8)
                .AddIngredient(ItemType<EssenceOfAzakana>())
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
