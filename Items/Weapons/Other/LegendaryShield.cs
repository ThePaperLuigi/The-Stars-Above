using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Celestial.BrilliantSpectrum;
using StarsAbove.Buffs.Other.Farewells;
using StarsAbove.Buffs.Other.LegendaryShield;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles.Other.LegendaryShield;
using StarsAbove.Projectiles.Summon.KeyOfTheKingsLaw;
using StarsAbove.Projectiles.Summon.Wavedancer;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Other
{
    public class LegendaryShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			//Item.mana = 80;
			//Item.DamageType = DamageClass.Summon;          //Is your weapon a melee weapon?
			Item.width = 40;            //Weapon's texture's width
			Item.height = 40;           //Weapon's texture's height
			Item.useTime = 120;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 120;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.HoldUp;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Green;              //The rarity of the weapon, from -1 to 13
			//Item.UseSound = StarsAboveAudio.SFX_AgnianTune;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<LegendaryShieldBash>();
			Item.shootSpeed = 15f;
			Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
            Item.accessory = true;
		}
        //SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            
            base.UpdateAccessory(player, hideVisual);
        }
        public override void UpdateEquip(Player player)
        {
            player.aggro += 5;
            player.GetModPlayer<WeaponPlayer>().LegendaryShieldEquippedAsAccessory = true;
            player.GetModPlayer<LegendaryShieldDashPlayer>().DashAccessoryEquipped = true;
            if (player.HasBuff(BuffType<HeroicCurse>()))
            {

            }
            else
            {
                player.GetDamage(DamageClass.Generic) -= 0.4f;

            }
            if (player.HasBuff(BuffID.OnFire) ||
                player.HasBuff(BuffID.OnFire3) ||
                player.HasBuff(BuffID.Burning) ||               
                player.HasBuff(BuffID.Venom) ||
                player.HasBuff(BuffID.Poisoned) ||
                player.HasBuff(BuffID.CursedInferno) ||
                player.HasBuff(BuffID.Frostburn) ||
                player.HasBuff(BuffID.Frostburn2) ||
                player.HasBuff(BuffID.Electrified))
            {

                player.AddBuff(BuffType<HeroicCurse>(), 2);
            }
            base.UpdateEquip(player);
        }
        int randomBuff;
        public override void HoldItem(Player player)
        {
            player.aggro += 5;
            player.statDefense += player.statLifeMax2 / 6;
            if(player.HasBuff(BuffType<LegendaryShieldRaised>()))
            {
                player.thorns = 6f;

            }
            else
            {
                player.thorns = 1f;

            }
            player.GetModPlayer<WeaponPlayer>().LegendaryShieldHeld = true;
            player.GetModPlayer<LegendaryShieldDashPlayer>().DashAccessoryEquipped = true;

            base.HoldItem(player);
        }
        public override bool CanUseItem(Player player)
		{
			if(player.HasBuff(BuffType<LegendaryShieldRaisedCooldown>()))
            {
                return false;
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
            Vector2 dustPosition = player.Center;
            SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing, player.Center);

            float dustAmount = 40f;
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 15f);
                spinningpoint5 = spinningpoint5.RotatedBy(player.velocity.ToRotation() + MathHelper.ToRadians(90));
                int dust = Dust.NewDust(dustPosition, 0, 0, DustID.GemEmerald);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = dustPosition + spinningpoint5;
                Main.dust[dust].velocity = player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 10f;
            }
            for (int d = 0; d < 16; d++)
            {
                Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
            }
            for (int d = 0; d < 16; d++)
            {
                Dust.NewDust(player.Center, 0, 0, DustID.GemEmerald, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
            }
            player.AddBuff(BuffType<LegendaryShieldRaised>(), 120);
            player.AddBuff(BuffType<LegendaryShieldRaisedCooldown>(), 10 * 60);
			return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            return false;
		}
		public override void AddRecipes()
		{

            CreateRecipe(1)
                  .AddIngredient(ItemID.HeroShield, 1)

                  .AddIngredient(ItemType<EssenceOfTheShield>())
                  .AddTile(TileID.Anvils)
                  .Register();
        }
	}

    public class LegendaryShieldDashPlayer : ModPlayer
    {
        // These indicate what direction is what in the timer arrays used
        public const int DashDown = 0;
        public const int DashUp = 1;
        public const int DashRight = 2;
        public const int DashLeft = 3;

        public const int DashCooldown = 50; // Time (frames) between starting dashes. If this is shorter than DashDuration you can start a new dash before an old one has finished
        public const int DashDuration = 35; // Duration of the dash afterimage effect in frames

        // The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
        public const float DashVelocity = 10f;

        // The direction the player has double tapped.  Defaults to -1 for no dash double tap
        public int DashDir = -1;

        // The fields related to the dash accessory
        public bool DashAccessoryEquipped;
        public int DashDelay = 0; // frames remaining till we can dash again
        public int DashTimer = 0; // frames remaining in the dash

        public override void ResetEffects()
        {
            // Reset our equipped flag. If the accessory is equipped somewhere, ExampleShield.UpdateAccessory will be called and set the flag before PreUpdateMovement
           // 

            // ResetEffects is called not long after player.doubleTapCardinalTimer's values have been set
            // When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
            // If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
            if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[DashDown] < 15)
            {
                DashDir = DashDown;
            }
            else if (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[DashUp] < 15)
            {
                DashDir = DashUp;
            }
            else if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
            {
                DashDir = DashRight;
            }
            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
            {
                DashDir = DashLeft;
            }
            else
            {
                DashDir = -1;
            }
            DashAccessoryEquipped = false;
        }

        // This is the perfect place to apply dash movement, it's after the vanilla movement code, and before the player's position is modified based on velocity.
        // If they double tapped this frame, they'll move fast this frame
        public override void PreUpdateMovement()
        {
            // if the player can use our dash, has double tapped in a direction, and our dash isn't currently on cooldown
            if (CanUseDash() && DashDir != -1 && DashDelay == 0)
            {
                Vector2 newVelocity = Player.velocity;

                switch (DashDir)
                {
                    // Only apply the dash velocity if our current speed in the wanted direction is less than DashVelocity
                    case DashUp when Player.velocity.Y > -DashVelocity:
                    case DashDown when Player.velocity.Y < DashVelocity:
                        {
                            // Y-velocity is set here
                            // If the direction requested was DashUp, then we adjust the velocity to make the dash appear "faster" due to gravity being immediately in effect
                            // This adjustment is roughly 1.3x the intended dash velocity
                            float dashDirection = DashDir == DashDown ? 1 : -1.3f;
                            newVelocity.Y = dashDirection * DashVelocity;
                            break;
                        }
                    case DashLeft when Player.velocity.X > -DashVelocity:
                    case DashRight when Player.velocity.X < DashVelocity:
                        {
                            // X-velocity is set here
                            float dashDirection = DashDir == DashRight ? 1 : -1;
                            newVelocity.X = dashDirection * DashVelocity;
                            break;
                        }
                    default:
                        return; // not moving fast enough, so don't start our dash
                }

                // start our dash
                DashDelay = DashCooldown;
                DashTimer = DashDuration;
                Player.velocity = newVelocity;

                // Here you'd be able to set an effect that happens when the dash first activates
                // Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
            }

            if (DashDelay > 0)
                DashDelay--;

            if (DashTimer > 0)
            { // dash is active
              // This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
              // Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
              // Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
                Player.eocDash = DashTimer;
                Player.armorEffectDrawShadowEOCShield = true;
                //Player.AddBuff(BuffType<Invincibility>(), 2);

                // count down frames remaining
                DashTimer--;
            }
        }

        private bool CanUseDash()
        {
            return DashAccessoryEquipped || Player.HeldItem.type == ModContent.ItemType<LegendaryShield>()
                && Player.dashType == DashID.None // player doesn't have Tabi or EoCShield equipped (give priority to those dashes)
                && !Player.setSolar // player isn't wearing solar armor
                && !Player.mount.Active; // player isn't mounted, since dashes on a mount look weird
        }
    }
}

