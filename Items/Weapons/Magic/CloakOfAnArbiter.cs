using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using StarsAbove.Buffs.SanguineDespair;
using StarsAbove.Utilities;
using StarsAbove.Projectiles.Magic.SanguineDespair;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Magic.RedMage;
using StarsAbove.Projectiles.Magic.CloakOfAnArbiter;
using StarsAbove.Buffs.CloakOfAnArbiter;
using StarsAbove.Buffs.CandiedSugarball;
using StarsAbove.Buffs;

namespace StarsAbove.Items.Weapons.Magic
{
    public class CloakOfAnArbiter : ModItem
	{
		public override void SetStaticDefaults()
		{
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 140;           //The damage of your weapon
			Item.mana = 35;
			Item.DamageType = DamageClass.Magic;          //Is your weapon a melee weapon?
			Item.width = 30;            //Weapon's texture's width
			Item.height = 30;           //Weapon's texture's height
			Item.useTime = 40;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 40;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Shoot;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 5;         //The force of knockback of the weapon. Maximum is 20
			Item.rare = ItemRarityID.Red;              //The rarity of the weapon, from -1 to 13
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
			Item.shoot = ProjectileType<SanguineDespairBolt>();
			Item.shootSpeed = 20f;
			Item.noMelee = true;
			Item.noUseGraphic = true;
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
				if(player.HasBuff(BuffType<ComboCooldown>()))
                {
					return false;
                }
				else
                {
					return true;
                }


			}
			else
			{
				

			}
			return base.CanUseItem(player);
		}

		public override void HoldItem(Player player)
        {
            Lighting.AddLight(player.Center, TorchID.Ichor);

            if (player.velocity.Y == 0)
            {
                for (int i3 = 0; i3 < 10; i3++)
                {

                    Dust d = Main.dust[Dust.NewDust(new Vector2(player.Center.X - player.width, player.Center.Y + player.height / 2 - 4), player.width * 2 - 3, 0, DustID.GoldFlame, 0, Main.rand.Next(-5, -2), 150, default, 0.3f)];
                    d.fadeIn = 0.3f;
                    d.noLight = true;
                    d.noGravity = true;
                }
            }
            if (!player.HasBuff(BuffType<EmbodiedSingularity>()))
            {
                player.AddBuff(BuffType<DegradedSingularity>(), 10);

                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    if (player.statLife < 100 && player.statLifeMax >= 400 && Main.CurrentFrameFlags.AnyActiveBossNPC)
                    {
                        player.AddBuff(BuffType<SingularityResonance>(), 10);
                    }
                }
                else if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    if(Main.CurrentFrameFlags.ActivePlayersCount <= 1)
                    {
                        return;
                    }
                    bool anyoneAlive = false;
                    //Check if there is a living player
                    for(int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player p = Main.player[i];
                        if(p.active)
                        {
                            if(!p.dead && p.whoAmI != player.whoAmI)
                            {
                                anyoneAlive = true;
                            }
                        }
                    }
                    if(!anyoneAlive && Main.CurrentFrameFlags.AnyActiveBossNPC)
                    {
                        player.AddBuff(BuffType<SingularityResonance>(), 10);

                    }

                }
            }
            else
            {
                if(player.GetModPlayer<StarsAbovePlayer>().inCombat < 0 || !player.active || player.dead)
                {
                    player.ClearBuff(BuffType<EmbodiedSingularity>());
                    player.GetModPlayer<BossPlayer>().WhiteAlpha = 1f;
                }
                else
                {
                    player.AddBuff(BuffType<EmbodiedSingularity>(), 10);

                }

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.boss && npc.Distance(player.Center) < 900)
                    {
                        
                        npc.AddBuff(BuffType<FairyTagDamage>(), 10);
                    }
                }
            }
            
            player.GetModPlayer<WeaponPlayer>().CloakOfAnArbiterHeld = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.HasBuff(BuffType<EmbodiedSingularity>()) && !player.HasBuff(BuffType<ArbiterSpecialCooldown>()))
                {
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_ArbiterShockwave, player.Center);
                    player.AddBuff(BuffType<ArbiterSpecialCooldown>(), 6 * 60);
                    //Shockwave
                    player.GetModPlayer<StarsAbovePlayer>().activateArbiterShockwaveEffect = true;
                    player.AddBuff(BuffType<Invincibility>(), 60);
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && !npc.boss && npc.Distance(player.Center) < 900)
                        { 
                            for (int d = 0; d < 30; d++)
                            {
                                Dust.NewDust(npc.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 1f);
                            }
                            for (int d = 0; d < 30; d++)
                            {
                                Dust.NewDust(npc.Center, 0, 0, DustID.Enchanted_Gold, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 1f);
                            }
                            npc.SimpleStrikeNPC(player.GetWeaponDamage(player.HeldItem) * 2, 0, true, 0, DamageClass.Generic, false, 0);
                            npc.AddBuff(BuffType<Ensnared>(), 60);
                        }
                    }
                    for (int d = 0; d < 16; d++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.GemAmber, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                    }
                    for (int d = 0; d < 16; d++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                    }
                    for (int d = 0; d < 16; d++)
                    {
                        Dust.NewDust(player.Center, 0, 0, DustID.Enchanted_Gold, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                    }
                }
                else
                {

                }
                if(StarsAbove.weaponActionKey.JustPressed)
                {
                    if (player.HasBuff(BuffType<SingularityResonance>()) && !player.HasBuff(BuffType<EmbodiedSingularity>()))
                    {
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, player.Center);

                        for (int d = 0; d < 16; d++)
                        {
                            Dust.NewDust(player.Center, 0, 0, DustID.GemAmber, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                        }
                        for (int d = 0; d < 16; d++)
                        {
                            Dust.NewDust(player.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                        }
                        for (int d = 0; d < 16; d++)
                        {
                            Dust.NewDust(player.Center, 0, 0, DustID.Enchanted_Gold, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                        }
                        player.GetModPlayer<BossPlayer>().WhiteAlpha = 1f;
                        player.AddBuff(BuffType<EmbodiedSingularity>(), 120);
                    }
                }
                

            }

            base.HoldItem(player);
        }
		public int CurrentAttack;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
            {
                if (player.HasBuff(BuffType<EmbodiedSingularity>()))
                {
                    player.AddBuff(BuffType<ComboCooldown>(), 2 * 60);

                }
                else
                {
                    player.AddBuff(BuffType<ComboCooldown>(), 6 * 60);

                }

                switch (CurrentAttack)
				{
					case 0:
                        //Degraded Lock
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceSwing, player.Center);

                        for (int d = 0; d < 40; d++)
                        {
                            Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(37));
                            float scale = 0f - (Main.rand.NextFloat() * 0.5f);
                            perturbedSpeed1 = perturbedSpeed1 * scale;
                            int dustIndex = Dust.NewDust(position, 0, 0, DustID.Enchanted_Gold, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 1f);
                            Main.dust[dustIndex].noGravity = true;

                        }

                        Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, ProjectileType<ArbiterLock>(), damage, 0, player.whoAmI, 0f);
						CurrentAttack++;

                        break;
					case 1:
                        //Degraded Chain
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_ArbiterChain, player.Center);
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            NPC npc = Main.npc[i];
                            if (npc.active && !npc.boss && npc.Distance(player.Center) < 900)
                            {
                                for (int ix = 0; ix < 30; ix++)
                                {
                                    Vector2 pos = Vector2.Lerp(player.Center, npc.Center, (float)ix / 30);
                                    Dust d = Dust.NewDustPerfect(pos, DustID.Enchanted_Gold, null, 240, default, 0.6f);
                                    d.fadeIn = 0.3f;
                                    d.noGravity = true;

                                }
                                for (int d = 0; d < 20; d++)
                                {
                                    Dust.NewDust(npc.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 1f);
                                }
                                for (int d = 0; d < 20; d++)
                                {
                                    Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-13, 13), 150, default, 1f);
                                }
                                for (int d = 0; d < 20; d++)
                                {
                                    Dust.NewDust(npc.Center, 0, 0, DustID.Enchanted_Gold, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 1f);
                                }
                                npc.SimpleStrikeNPC(player.GetWeaponDamage(player.HeldItem), 0, false, 0, DamageClass.Generic, false, 0);
								npc.AddBuff(BuffType<Ensnared>(), 120);
                            }
                        }
                        CurrentAttack++;

                        break;
					case 2:
                        //Degraded Pillar
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_ArbiterPillar, player.Center);
                        for (int d = 0; d < 40; d++)
                        {
                            Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(37));
                            float scale = 0f - (Main.rand.NextFloat() * 2.5f);
                            perturbedSpeed1 = perturbedSpeed1 * scale;
                            int dustIndex = Dust.NewDust(position, 0, 0, DustID.Enchanted_Gold, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 1f);
                            Main.dust[dustIndex].noGravity = true;

                        }
                        for (int d = 0; d < 70; d++)
                        {
                            Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(77));
                            float scale = 0f - (Main.rand.NextFloat() * 0.5f);
                            perturbedSpeed1 = perturbedSpeed1 * scale;
                            int dustIndex = Dust.NewDust(position, 0, 0, DustID.GemTopaz, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 1f);
                            Main.dust[dustIndex].noGravity = true;

                        }
                        for (int d = 0; d < 90; d++)
                        {
                            Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(17));
                            float scale = 0f - (Main.rand.NextFloat() * 4.5f);
                            perturbedSpeed1 = perturbedSpeed1 * scale;
                            int dustIndex = Dust.NewDust(position, 0, 0, DustID.GemTopaz, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 1f);
                            Main.dust[dustIndex].noGravity = true;

                        }
                        Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<ArbiterPillar>(), damage, 0, player.whoAmI, 0f);
                        CurrentAttack = 0;

                        break;
					default:
						CurrentAttack = 0;
						break;
				}


				return false;
			}
			else
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceSwing, Main.MouseWorld);

                //Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                //CombatText.NewText(textPos, new Color(139, 3, 0, 240), $"-10", false, false);
                for (int d = 0; d < 40; d++)
				{
					Vector2 perturbedSpeed1 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(37));
					float scale = 0f - (Main.rand.NextFloat() * 0.5f);
					perturbedSpeed1 = perturbedSpeed1 * scale;
					int dustIndex = Dust.NewDust(position, 0, 0, DustID.Enchanted_Gold, -perturbedSpeed1.X, -perturbedSpeed1.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;

				}
                Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, ProjectileType<FairyAttack>(), damage, 0, player.whoAmI, 0f);

                return false;

            }


            return false;
		}
		public override void AddRecipes()
		{
			
		}
	}
}
