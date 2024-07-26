using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Celestial.BlackSilence;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles.Celestial.BlackSilence;
using StarsAbove.Systems;
using StarsAbove.Systems.WeaponSystems;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Celestial
{
    public class BlackSilenceWeapon : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gloves of the Black Silence");
            /* Tooltip.SetDefault("" +
				"[c/8AC1F1:This weapon has a unique damage type; inherits bonuses from all damage classes]" +
				"\nRight-click to choose from 3 random weapons from a pool of 9 different weapons, each with unique attack patterns (5 second cooldown)" +
				"\nAfter choosing a weapon, gain a unique buff based on the chosen weapon that lasts for 12 seconds" +
				"\n[c/6C6C6C:Durandal]: Attack in a wide arc with a high critical strike chance; gain 50% bonus critical strike damage against foes below 50% HP" + //Done.
				"\n[c/6C6C6C:Zelkova]: Alternate between axe and mace, healing for a portion of damage dealt; deal 30% bonus damage to foes below 50% HP" + //Done.
				"\n[c/6C6C6C:Ranga]: Dash a fixed distance towards your cursor, damaging foes for half base damage (Cooldown after attacking); deal 30% increased critical strike damage" +//Done
				"\n[c/6C6C6C:Old Boys]: Throw a returning hammer that hits twice at max range, dealing 2x base damage to foes below 50% HP; gain 20 defense" +//Done
				"\n[c/6C6C6C:Allas]: Attack with a piercing mid-ranged stab with wide range and no knockback; gain 30% increased Movement Speed" +//Done
				"\n[c/6C6C6C:Mook]: Execute a barrage of close-ranged slashes per attack (Cooldown after attacking); inflict Bleed on hit for 4 seconds" + //Done.
				"\n[c/6C6C6C:Atelier Logic]: Attack with ranged bullets (every third attack deals 40% increased damage); gain 20% Armor Penetration" + //Done.
				"\n[c/6C6C6C:Crystal Atelier]: Slash in a mid-ranged arc, dealing damage twice per hit; reflect 80% of damage taken" +//When hitting a foe with the first swing, spawn a projectile that's the same swing but with the left hand to mimic dualstrike
				"\n[c/6C6C6C:Wheels Industry]: Slam the weapon into the ground, dealing 25% increased damage to all nearby enemies; deal 10% increased damage" + //Easy swing code.
				"\nOnce all 9 weapons are chosen, use the Weapon Action Key to activate [c/525252:Furioso], gaining the buff [c/323232:Black Silence] for 12 seconds" +
				"\n[c/323232:Black Silence] causes all weapons to be cycled through upon attacking" +//All buffs already account for Furioso.
				"\nAn icon will display near weapons you have not used for the activation of [c/525252:Furioso] during selection" +
				"\n[c/323232:Black Silence] will additionally increase damage by 30% and activate all unique weapon buffs" +
				"\n'It can't be helped. That's that, and this is this'" +
				$""); */  //The (English) text shown below your weapon's name

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 100;           //The damage of your weapon
            Item.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();          //Is your weapon a melee weapon?
            Item.width = 40;            //Weapon's texture's width
            Item.height = 40;           //Weapon's texture's height
            Item.useTime = 25;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 25;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
            Item.useStyle = ItemUseStyleID.HiddenAnimation;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            Item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
            Item.rare = ItemRarityID.Yellow;              //The rarity of the weapon, from -1 to 13
                                                          //Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
            Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
            Item.shoot = ProjectileType<DurandalSlash1>();
            Item.shootSpeed = 15f;
            Item.value = Item.buyPrice(gold: 1);           //The value of the weapon

            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override bool AltFunctionUse(Player player)
        {

            return true;
        }

        public override void HoldItem(Player player)
        {
            var modPlayer = player.GetModPlayer<BlackSilencePlayer>();

            modPlayer.BlackSilenceHeld = true;
            if (modPlayer.chosenWeapon == 0)
            {//Durandal
                Item.useStyle = ItemUseStyleID.HiddenAnimation;
                player.GetCritChance(DamageClass.Generic) += 0.3f;
            }
            if (modPlayer.chosenWeapon == 1)
            {//
                Item.useStyle = ItemUseStyleID.HiddenAnimation;
            }
            if (modPlayer.chosenWeapon == 2)
            {//
                Item.useStyle = ItemUseStyleID.HiddenAnimation;
                if (player.ownedProjectileCounts[ProjectileType<RangaHeld>()] < 1)
                {//Equip animation.
                    int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<RangaHeld>(), 0, 0, player.whoAmI, 0f);
                }
            }
            if (modPlayer.chosenWeapon == 3)
            {//
                Item.useStyle = ItemUseStyleID.Rapier;
            }
            if (modPlayer.chosenWeapon == 4)
            {//
                Item.useStyle = ItemUseStyleID.Rapier;
            }
            if (modPlayer.chosenWeapon == 5)
            {//Mook
                Item.useStyle = ItemUseStyleID.HiddenAnimation;
                if (player.ownedProjectileCounts[ProjectileType<MookHeldSheathe>()] < 1)
                {//Equip animation.
                    int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<MookHeldSheathe>(), 0, 0, player.whoAmI, 0f);
                }

            }
            if (modPlayer.chosenWeapon == 6)
            {//
                Item.useStyle = ItemUseStyleID.HiddenAnimation;
            }
            if (modPlayer.chosenWeapon == 7)
            {//
                Item.useStyle = ItemUseStyleID.Swing;
            }
            if (modPlayer.chosenWeapon == 8)
            {//
                Item.useStyle = ItemUseStyleID.HiddenAnimation;
            }

            //If weapon action key pressed and all weapons have been used, activate Furioso
            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed && player.itemTime <= 0)
            {
                if (modPlayer.durandalUsed && modPlayer.zelkovaUsed && modPlayer.rangaUsed && modPlayer.oldBoysUsed && modPlayer.allasUsed && modPlayer.mookUsed && modPlayer.atelierLogicUsed && modPlayer.crystalAtelierUsed && modPlayer.wheelsIndustryUsed)
                {//Furioso use condition
                    player.AddBuff(BuffType<FuriosoBuff>(), 720);
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, Main.LocalPlayer.Center);
                    player.AddBuff(BuffType<BlackSilenceChoiceCooldown>(), 720);
                }
            }
            if (player.HasBuff(BuffType<FuriosoBuff>()))
            {
                int dustIndex = Dust.NewDust(new Vector2(player.position.X, player.Center.Y + player.height / 2), player.width, 0, DustID.Obsidian, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-6, -1), 100, default(Color), 1.3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                Dust.NewDust(new Vector2(player.position.X, player.Center.Y + player.height / 2), player.width, 0, DustID.Obsidian, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-6, -1), 100, default(Color), 1f);
                Dust.NewDust(new Vector2(player.position.X, player.Center.Y + player.height / 2), player.width, 0, DustID.SilverFlame, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-6, -1), 100, default(Color), 1f);

                Main.dust[dustIndex].velocity *= 3f; Dust.NewDust(new Vector2(player.Center.X, player.Center.Y + player.height / 2), player.width, 0, DustID.Smoke, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-5, -1), 150, default(Color), 0.8f);
                Vector2 direction = Main.rand.NextVector2CircularEdge(player.width * 0.6f, player.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.9f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);
                for (int d = 0; d < 6; d++)
                {
                    Dust dust = Dust.NewDustPerfect(player.Center + direction * distance, DustID.SilverFlame, velocity);
                    dust.scale = 0.5f;
                    dust.fadeIn = 1.1f;
                    dust.noGravity = true;
                    dust.noLight = true;
                    //dust.color = Color.Purple;
                    dust.alpha = 0;
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<BlackSilencePlayer>();

            if (player.altFunctionUse == 2)
            {
                //If no cooldown
                if (!player.HasBuff(BuffType<BlackSilenceChoiceCooldown>()))
                {

                    //Assign random weapons to the 3 options available. (Make sure there's no duplicates.)
                    List<int> listNumbers = new List<int>();
                    int number;
                    for (int i = 0; i < 3; i++)
                    {
                        do
                        {
                            number = Main.rand.Next(0, 9);
                        } while (listNumbers.Contains(number));
                        listNumbers.Add(number);
                    }
                    modPlayer.blackSilenceWeaponChoice1 = listNumbers[0];
                    modPlayer.blackSilenceWeaponChoice2 = listNumbers[1];
                    modPlayer.blackSilenceWeaponChoice3 = listNumbers[2];

                    //Make the UI appear.
                    //modPlayer.UIAnimateIn = 15f;
                    modPlayer.BlackSilenceUIVisible = true;
                }
            }
            else
            {
                if (player.HasBuff(BuffType<BlackSilenceAttackCooldown>()))
                {
                    return false;
                }
            }



            return true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {

        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {

        }

        public override bool? UseItem(Player player)
        {


            return base.UseItem(player);
        }
        int currentAttack;
        int comboTimer;


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var modPlayer = player.GetModPlayer<BlackSilencePlayer>();
            if (player.altFunctionUse != 2)
            {
                if (player.HasBuff(BuffType<FuriosoBuff>()))
                {
                    modPlayer.chosenWeapon++;
                    if (modPlayer.chosenWeapon > 8)
                    {
                        modPlayer.chosenWeapon = 0;
                    }
                }

                if (modPlayer.chosenWeapon == 0)
                {//Durandal swings
                    if (player.direction == 1)
                    {


                        if (currentAttack == 0)
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<DurandalSlash2>(), damage, knockback, player.whoAmI, 0f);
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX2>(), 0, knockback, player.whoAmI, 0f);
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Main.LocalPlayer.Center);
                            currentAttack = 1; return false;
                        }
                        else
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<DurandalSlash1>(), damage, knockback, player.whoAmI, 0f);
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX1>(), 0, knockback, player.whoAmI, 0f);
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, Main.LocalPlayer.Center);
                            currentAttack = 0; return false;
                        }

                    }
                    else
                    {

                        if (currentAttack == 0)
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<DurandalSlash1>(), damage, knockback, player.whoAmI, 0f);
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX1>(), 0, knockback, player.whoAmI, 0f);
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Main.LocalPlayer.Center);
                            currentAttack = 1; return false;
                        }
                        else
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<DurandalSlash2>(), damage, knockback, player.whoAmI, 0f);
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX2>(), 0, knockback, player.whoAmI, 0f);

                            SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, Main.LocalPlayer.Center);
                            currentAttack = 0; return false;
                        }
                    }
                }
                if (modPlayer.chosenWeapon == 1)
                {//Axe/Mace (Zelkova)

                    if (player.direction == 1)
                    {


                        if (currentAttack == 0)
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<ZelkovaSlash2>(), damage, knockback, player.whoAmI, 0f);
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX2>(), 0, knockback, player.whoAmI, 0f);
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, Main.LocalPlayer.Center);
                            currentAttack = 1; return false;
                        }
                        else
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<ZelkovaSlash1>(), (int)(damage), knockback, player.whoAmI, 0f);
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX1>(), 0, knockback, player.whoAmI, 0f);
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Main.LocalPlayer.Center);
                            currentAttack = 0; return false;
                        }

                    }
                    else
                    {

                        if (currentAttack == 0)
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<ZelkovaSlash3>(), (int)(damage), knockback, player.whoAmI, 0f);
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX1>(), 0, knockback, player.whoAmI, 0f);
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, Main.LocalPlayer.Center);
                            currentAttack = 1; return false;
                        }
                        else
                        {
                            Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<ZelkovaSlash4>(), damage, knockback, player.whoAmI, 0f);
                            //Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<KazVFX2>(), 0, knockback, player.whoAmI, 0f);

                            SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Main.LocalPlayer.Center);
                            currentAttack = 0; return false;
                        }
                    }
                }
                if (modPlayer.chosenWeapon == 2)
                {//Ranga attack
                    Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 400f;
                    position = new Vector2(position.X, position.Y + 7);
                    position += muzzleOffset;

                    if (!Collision.SolidCollision(new Vector2(position.X, position.Y - 10), player.width, player.height))
                    {
                        for (int ir = 0; ir < 50; ir++)
                        {
                            Vector2 positionNew = Vector2.Lerp(player.Center, position, (float)ir / 50);
                            Dust d = Dust.NewDustPerfect(positionNew, DustID.SilverFlame, null, 0, default(Color), 0.7f);
                            d.fadeIn = 0.3f;
                            d.noLight = true;
                            d.noGravity = true;
                            Dust d2 = Dust.NewDustPerfect(positionNew, DustID.Obsidian, null, 110, default(Color), 0.7f);
                            d2.fadeIn = 0.3f;
                            d2.noLight = true;
                            d2.noGravity = true;

                        }
                        for (int ix = 0; ix < 4; ix++)
                        {
                            Vector2 positionNew = Vector2.Lerp(player.Center, position, (float)ix / 4);
                            int index = Projectile.NewProjectile(source, positionNew.X, positionNew.Y, 0, 0, ProjectileType<RangaDamage>(), damage / 2, 0f, player.whoAmI);

                            Main.projectile[index].originalDamage = damage;

                        }
                        player.Teleport(new Vector2(position.X, position.Y - 10), 1, 0);
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)player.whoAmI, position.X, position.Y - 10, 1, 0, 0);


                        SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceSwing, Main.LocalPlayer.Center);

                        if (player.HasBuff(BuffType<ComboCooldown>()))
                        {
                            player.AddBuff(BuffType<BlackSilenceAttackCooldown>(), 120);

                        }
                        else
                        {
                            player.AddBuff(BuffType<BlackSilenceAttackCooldown>(), 30);
                            player.AddBuff(BuffType<ComboCooldown>(), 240);
                        }
                        
                    }


                }
                if (modPlayer.chosenWeapon == 3)
                {//Old Boys Hammer
                    Projectile.NewProjectile(source, position, velocity / 4, ProjectileType<OldBoys>(), (int)(damage), knockback, player.whoAmI);
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Main.LocalPlayer.Center);
                    for (int d = 0; d < 17; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(8));
                        float scale = 2f - (Main.rand.NextFloat() * 1f);
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(position, 0, 0, DustID.Obsidian, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 17; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(8));
                        float scale = 2f - (Main.rand.NextFloat() * 7f);
                        perturbedSpeed = -perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(position, 0, 0, DustID.SilverFlame, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 27; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
                        float scale = 1f - (Main.rand.NextFloat() * .6f);
                        perturbedSpeed = (perturbedSpeed * scale);
                        int dustIndex = Dust.NewDust(position, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                }
                if (modPlayer.chosenWeapon == 4)
                {//Allas spear
                    for (int d = 0; d < 17; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(8));
                        float scale = 2f - (Main.rand.NextFloat() * 1f);
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(position, 0, 0, DustID.Obsidian, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 17; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(8));
                        float scale = 2f - (Main.rand.NextFloat() * 7f);
                        perturbedSpeed = -perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(position, 0, 0, DustID.SilverFlame, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                    for (int d = 0; d < 27; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
                        float scale = 1f - (Main.rand.NextFloat() * .6f);
                        perturbedSpeed = (perturbedSpeed * scale);
                        int dustIndex = Dust.NewDust(position, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                        Main.dust[dustIndex].noGravity = true;

                    }

                    Vector2 perturbedSpeed2 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(4));
                    velocity.X = perturbedSpeed2.X;
                    velocity.Y = perturbedSpeed2.Y;
                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X / 2, velocity.Y / 2, ProjectileType<AllasStab>(), damage, 0, player.whoAmI);
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Main.LocalPlayer.Center);

                }
                if (modPlayer.chosenWeapon == 5)
                {//Mook attack
                 //SoundEngine.PlaySound(StarsAboveAudio.SFX_AmiyaSlash, player.Center);
                    Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 160f;
                    position = new Vector2(position.X, position.Y + 7);
                    if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                    {
                        position += muzzleOffset;
                    };
                    if (player.ownedProjectileCounts[ProjectileType<MookHeldBlade>()] < 1)
                    {//Equip animation.
                        int index = Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<MookHeldBlade>(), 0, 0, player.whoAmI, 0f);
                    }
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceKatana, Main.LocalPlayer.Center);

                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, 0, 0, ProjectileType<MookSlashes>(), damage, 0, player.whoAmI, 0f);
                    if (!player.HasBuff(BuffType<FuriosoBuff>()))
                    {
                        player.AddBuff(BuffType<BlackSilenceAttackCooldown>(), 120);
                    }
                }
                if (modPlayer.chosenWeapon == 6)
                {//Gun
                    if (currentAttack == 0)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AtelierLogicGun1>(), 0, knockback, player.whoAmI, 0f);

                        SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilencePistol, Main.LocalPlayer.Center);
                        Projectile.NewProjectile(source, position, velocity * 2, ProjectileType<BlackSilenceBullet>(), damage, knockback, player.whoAmI);

                        currentAttack = 1; return false;
                    }
                    if (currentAttack == 1)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AtelierLogicGun2>(), 0, knockback, player.whoAmI, 0f);

                        SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilencePistol, Main.LocalPlayer.Center);
                        Projectile.NewProjectile(source, position, velocity * 2, ProjectileType<BlackSilenceBullet>(), damage, knockback, player.whoAmI);

                        currentAttack = 2; return false;
                    }
                    if (currentAttack == 2)
                    {//Rifle
                        Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 60f;
                        position = new Vector2(position.X, position.Y);
                        if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                        {
                            position += muzzleOffset;
                        }
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<AtelierLogicGun3>(), 0, knockback, player.whoAmI, 0f);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceRifle, Main.LocalPlayer.Center);

                        for (int d = 0; d < 17; d++)//Visual effects
                        {
                            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(8));
                            float scale = 2f - (Main.rand.NextFloat() * 1f);
                            perturbedSpeed = perturbedSpeed * scale;
                            int dustIndex = Dust.NewDust(position, 0, 0, DustID.Obsidian, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                            Main.dust[dustIndex].noGravity = true;

                        }
                        for (int d = 0; d < 17; d++)//Visual effects
                        {
                            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(8));
                            float scale = 2f - (Main.rand.NextFloat() * 7f);
                            perturbedSpeed = -perturbedSpeed * scale;
                            int dustIndex = Dust.NewDust(position, 0, 0, DustID.SilverFlame, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                            Main.dust[dustIndex].noGravity = true;

                        }
                        for (int d = 0; d < 27; d++)//Visual effects
                        {
                            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
                            float scale = 1f - (Main.rand.NextFloat() * .6f);
                            perturbedSpeed = (perturbedSpeed * scale);
                            int dustIndex = Dust.NewDust(position, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                            Main.dust[dustIndex].noGravity = true;

                        }
                        Projectile.NewProjectile(source, position, velocity * 2, ProjectileType<BlackSilenceBullet>(), (int)(damage * 1.4), knockback, player.whoAmI);

                        currentAttack = 0; return false;
                    }
                }
                if (modPlayer.chosenWeapon == 7)
                {//Dualblades (Crystal Atelier)

                    Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 60f;
                    position = new Vector2(position.X, position.Y + 7);
                    if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                    {
                        position += muzzleOffset;
                    };
                    if (currentAttack == 0)
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<CrystalAtelierSwing1>(), damage, knockback, player.whoAmI, 0f);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, Main.LocalPlayer.Center);
                        currentAttack = 1; return false;
                    }
                    else
                    {
                        Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<CrystalAtelierSwing2>(), (int)(damage), knockback, player.whoAmI, 0f);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Main.LocalPlayer.Center);
                        currentAttack = 0; return false;
                    }

                }
                if (modPlayer.chosenWeapon == 8)
                {//Greatsword (Wheels Industry)



                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.Center.X, player.Center.Y, 0, 0, ProjectileType<WheelsIndustryAttack>(), damage, knockback, player.whoAmI, 0f);
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Main.LocalPlayer.Center);
                    if (!player.HasBuff(BuffType<FuriosoBuff>()))
                    {
                        player.AddBuff(BuffType<BlackSilenceAttackCooldown>(), 80);
                    }

                    return false;


                }


            }

            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.SoulofNight, 6)
                .AddIngredient(ItemID.AvengerEmblem, 1)
                .AddIngredient(ItemID.PowerGlove, 1)
                .AddIngredient(ItemID.VoidLens, 1)
                .AddIngredient(ItemType<EssenceOfSilence>())
                .AddTile(TileID.Anvils)
                .Register();
        }
    }


}
