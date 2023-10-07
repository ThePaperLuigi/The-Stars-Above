
using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Celestial.EverlastingPickaxe
{
    public class EverlastingPickaxeExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Everlasting Pickaxe");

        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 1;
            Projectile.penetrate = -1;
            Projectile.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;

        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }


        public override void AI()
        {
            //Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.Center);

            Projectile.ai[0] += 1f;

            Main.player[Projectile.owner].GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;

            for (int d = 0; d < 5; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 7, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default, 1.5f);
            }
            for (int d = 0; d < 6; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 269, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default, 1.5f);
            }
            for (int d = 0; d < 7; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 78, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 150, default, 1.5f);
            }
            // Smoke Dust spawn
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }


            // Fade in
            Projectile.alpha--;
            if (Projectile.alpha < 100)
            {
                Projectile.alpha = 100;
            }


        }
        public override void OnKill(int timeLeft)
        {
            {
                int explosionRadius = 9;

                int minTileX = (int)(Projectile.position.X / 16f - explosionRadius);
                int maxTileX = (int)(Projectile.position.X / 16f + explosionRadius);
                int minTileY = (int)(Projectile.position.Y / 16f - explosionRadius);
                int maxTileY = (int)(Projectile.position.Y / 16f + explosionRadius);
                if (minTileX < 0)
                {
                    minTileX = 0;
                }
                if (maxTileX > Main.maxTilesX)
                {
                    maxTileX = Main.maxTilesX;
                }
                if (minTileY < 0)
                {
                    minTileY = 0;
                }
                if (maxTileY > Main.maxTilesY)
                {
                    maxTileY = Main.maxTilesY;
                }
                bool canKillWalls = false;
                for (int x = minTileX; x <= maxTileX; x++)
                {
                    for (int y = minTileY; y <= maxTileY; y++)
                    {
                        float diffX = Math.Abs(x - Projectile.position.X / 16f);
                        float diffY = Math.Abs(y - Projectile.position.Y / 16f);
                        double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                        if (distance < explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].WallType == 0)
                        {
                            canKillWalls = true;
                            break;
                        }
                    }
                }
                AchievementsHelper.CurrentlyMining = true;
                for (int i = minTileX; i <= maxTileX; i++)
                {
                    for (int j = minTileY; j <= maxTileY; j++)
                    {
                        float diffX = Math.Abs(i - Projectile.position.X / 16f);
                        float diffY = Math.Abs(j - Projectile.position.Y / 16f);
                        double distanceToTile = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                        if (distanceToTile < explosionRadius)
                        {
                            bool canKillTile = true;
                            if (Main.tile[i, j] != null && Main.tile[i, j].HasTile)
                            {
                                canKillTile = true;
                                if (!TileLoader.CanExplode(i, j))
                                {
                                    canKillTile = false;
                                }
                                if (canKillTile)
                                {
                                    WorldGen.KillTile(i, j, false, false, false);
                                    if (!Main.tile[i, j].HasTile && Main.netMode != NetmodeID.SinglePlayer)
                                    {
                                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j, 0f, 0, 0, 0);
                                    }
                                }
                            }
                            if (canKillTile)
                            {
                                for (int x = i - 1; x <= i + 1; x++)
                                {
                                    for (int y = j - 1; y <= j + 1; y++)
                                    {
                                        if (Main.tile[x, y] != null && Main.tile[x, y].WallType > 0 && canKillWalls && WallLoader.CanExplode(x, y, Main.tile[x, y].WallType))
                                        {
                                            WorldGen.KillWall(x, y, false);
                                            if (Main.tile[x, y].WallType == 0 && Main.netMode != NetmodeID.SinglePlayer)
                                            {
                                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 2, x, y, 0f, 0, 0, 0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                AchievementsHelper.CurrentlyMining = false;
            }
            base.OnKill(timeLeft);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {



        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

            target.AddBuff(BuffID.OnFire, 240);

        }
    }
}
