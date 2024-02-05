
using Microsoft.Xna.Framework;
using Mono.Cecil;
using StarsAbove.Buffs.CandiedSugarball;
using StarsAbove.Buffs.Chronoclock;
using StarsAbove.Buffs.StarphoenixFunnel;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Prisms;
using StarsAbove.Projectiles.Summon.CandiedSugarball;
using StarsAbove.Projectiles.Summon.Chronoclock;
using StarsAbove.Projectiles.Summon.Kifrosse;
using StarsAbove.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Weapons.Summon
{
    public class CandiedSugarball : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chronoclock");
            /* Tooltip.SetDefault("" +
				"Summons a [c/EB9FDA:Fragment of Time] to aid you in combat, granting immunity to Slow (Only one can be summoned at a time)" +
				"\nThe [c/EB9FDA:Fragment of Time] will periodically nurture a [c/52BFDF:Time Bubble] centered on herself, which grows over time" +
				"\nThe [c/52BFDF:Time Bubble] will pop upon contact with an enemy, dealing damage and additionally exploding in a [c/9FD9EB:Time Pulse]" +
				"\nThe [c/9FD9EB:Time Pulse] deals damage in a large area, increasing in potency with the size of the [c/52BFDF:Time Bubble], and inflicts 18 summon tag damage" +
				"\nAfter 10 seconds at max size, the [c/52BFDF:Time Bubble] explodes automatically (Can be manually detonated by pressing the Weapon Action Key)" +
				"\nStanding within the [c/52BFDF:Time Bubble] will grant [c/E8579B:Alacrity], increasing attack speed by 20% and movement speed by 10%" +
				"\nAdditionally, taking damage within the [c/52BFDF:Time Bubble] will pop the [c/52BFDF:Time Bubble] prematurely, preventing that instance of damage (20 second cooldown)" +
				"\n'Well, that sounds like a waste of time'"
				+ $""); */

            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 60;
            Item.width = 21;
            Item.height = 21;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ProjectileType<Charsugar>();
            Item.buffType = BuffType<SugarballMinionBuff>(); //The buff added to player after used the item
            Item.value = Item.buyPrice(gold: 1);           //The value of the weapon
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {

            }

            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {

            }
            else
            {

            }

            return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.JustPressed)
            {
                if (player.HasBuff(BuffType<SugarballMinionBuff>()))
                {
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, Main.MouseWorld);

                    player.GetModPlayer<WeaponPlayer>().sugarballMinionType++;

                    if (player.GetModPlayer<WeaponPlayer>().sugarballMinionType > 2)
                    {
                        player.GetModPlayer<WeaponPlayer>().sugarballMinionType = 0;
                    }

                    for (int d = 0; d < 16; d++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                    }
                    for (int d = 0; d < 16; d++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.GemAmethyst, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                    }

                    for (int l = 0; l < Main.projectile.Length; l++)
                    {
                        Projectile proj = Main.projectile[l];
                        if (proj.active && (
                            proj.type == ProjectileType<Sugartle>() ||
                            proj.type == ProjectileType<Charsugar>() ||
                            proj.type == ProjectileType<Charsugar>()
                            ) && proj.owner == player.whoAmI)
                        {
                            proj.Kill();
                        }
                    }

                    ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = Main.MouseWorld },
                player.whoAmI);
                    player.SpawnMinionOnCursor(player.GetSource_FromThis(), player.whoAmI, ProjectileType<Sugartle>(), player.GetWeaponDamage(Item), 0);

                }

            }


        }

        public override void UpdateInventory(Player player)
        {

        }




        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {

            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            for (int l = 0; l < Main.projectile.Length; l++)
            {
                Projectile proj = Main.projectile[l];
                if (proj.active && proj.type == Item.shoot && proj.owner == player.whoAmI)
                {
                    proj.active = false;
                }
            }

            player.AddBuff(Item.buffType, 2);
            position = Main.MouseWorld;
            for (int l = 0; l < Main.projectile.Length; l++)
            {
                Projectile proj = Main.projectile[l];
                if (proj.active && (
                    proj.type == ProjectileType<Sugartle>() ||
                    proj.type == ProjectileType<Charsugar>() ||
                    proj.type == ProjectileType<Charsugar>()
                    ) && proj.owner == player.whoAmI)
                {
                    proj.Kill();
                }
            }
            switch(player.GetModPlayer<WeaponPlayer>().sugarballMinionType)
            {
                case 0:
                    ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
               new ParticleOrchestraSettings { PositionInWorld = Main.MouseWorld },
               player.whoAmI);
                    player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<Charsugar>(), damage, knockback);
                    return false;
                case 1:
                    ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
               new ParticleOrchestraSettings { PositionInWorld = Main.MouseWorld },
               player.whoAmI);
                    player.SpawnMinionOnCursor(source, player.whoAmI, ProjectileType<Sugartle>(), damage, knockback);
                    return false;

                case 2:
                    ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
               new ParticleOrchestraSettings { PositionInWorld = Main.MouseWorld },
               player.whoAmI);
                    player.SpawnMinionOnCursor(source, player.whoAmI, type, damage, knockback);
                    return false;
            }

            return false;
        }
        public override void AddRecipes()
        {
          
        }

    }
}
