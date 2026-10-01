using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.GoddessOfMemory;
using StarsAbove.Projectiles.Bosses.GoddessOfMemory;
using StarsAbove.Systems;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.NPCs.GoddessOfMemory
{
    [AutoloadBossHead]
    public class GoddessOfMemoryBoss : ModNPC
    {
        private readonly List<int> remainingPhases = new List<int>();
        private readonly List<int> remainingIntermissions = new List<int>();
        private readonly Dictionary<int, HashSet<int>> hammerHitPlayers = new Dictionary<int, HashSet<int>>();
        private ulong hammerHitDeadline;
        private int lastPhase;
        private int lastIntermission;
        private bool onlyOnePlayerAlive;

        public const int IdleFrameDuration = 10;
        public int AttackTimer = 120;
        public bool SuppressCastSprite;
        public bool IsSpearCharging;
        public Vector2 SpearChargeDestination;
        public int CandleOfPromiseSide;
        public readonly List<Vector2[]> ConstellationPaths = new List<Vector2[]>();

        private enum Frame
        {
            Casting,
            Idle1,
            Idle2,
            Idle3,
            Idle4,
            Idle5,
            Idle6,
            Idle7,
            Idle8
        }

        public override LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), () => "Goddess of Memory");

        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Timer => ref NPC.ai[1];
        public ref float AI_RotationNumber => ref NPC.ai[2];
        public ref float AI_CastTimerMax => ref NPC.ai[3];
        public ref float AI_CastTimer => ref NPC.localAI[3];
        public bool IsCastingSubspaceShift => AI_State == (float)ActionState.Casting && SuppressCastSprite;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 9;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            });
        }

        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.lifeMax = 330000;
            NPC.damage = 20;
            NPC.defense = 125;
            NPC.knockBackResist = 0f;
            NPC.width = 100;
            NPC.height = 100;
            NPC.scale = 1f;
            NPC.npcSlots = 1f;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.value = 0f;
            NPC.netAlways = true;
            DrawOffsetY = 0;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return IsSpearCharging && AI_State == (float)ActionState.PersistentCast;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            remainingPhases.Clear();
            remainingIntermissions.Clear();
            for (int i = 2; i <= 6; i++)
            {
                remainingIntermissions.Add(i);
            }

            lastPhase = 0;
            lastIntermission = 1;
            if (GoddessOfMemoryArenaSystem.IsActive)
            {
                GoddessOfMemoryArenaSystem.ChangeArena(NPC, false, 1);
            }

            NPC.netUpdate = true;
        }

        private static int DrawFromPool(List<int> pool, int count, int previous = 0)
        {
            if (pool.Count == 0)
            {
                for (int i = 1; i <= count; i++)
                {
                    pool.Add(i);
                }
            }

            int previousIndex = pool.IndexOf(previous);
            bool excludePrevious = previousIndex >= 0 && pool.Count > 1;
            int index = Main.rand.Next(pool.Count - (excludePrevious ? 1 : 0));
            if (excludePrevious && index >= previousIndex)
            {
                index++;
            }

            int selected = pool[index];
            pool.RemoveAt(index);
            return selected;
        }

        public void ChangeArena(bool isPhase, int arena = 0)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            if (isPhase)
            {
                if (arena == 0)
                {
                    arena = DrawFromPool(remainingPhases, 4, lastPhase);
                }

                remainingPhases.Remove(arena);
                lastPhase = arena;
            }
            else
            {
                if (arena == 0)
                {
                    arena = DrawFromPool(remainingIntermissions, 6, lastIntermission);
                }

                remainingIntermissions.Remove(arena);
                lastIntermission = arena;
            }

            GoddessOfMemoryArenaSystem.ChangeArena(NPC, isPhase, arena);
            NPC.netUpdate = true;
        }

        internal void BeginHammerOfRecognition()
        {
            hammerHitPlayers.Clear();
            hammerHitDeadline = Main.GameUpdateCount + 270UL;
        }

        internal void RegisterHammerOfRecognition(int identity)
        {
            hammerHitPlayers[identity] = new HashSet<int>();
        }

        internal void HealFromHammerOfRecognition(int identity, int playerIndex, int damage)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !NPC.active || NPC.life <= 0 || AI_State == (float)ActionState.Dying ||
                playerIndex < 0 || playerIndex >= Main.maxPlayers || !Main.player[playerIndex].active || damage <= 0 ||
                Main.GameUpdateCount > hammerHitDeadline || !hammerHitPlayers.TryGetValue(identity, out HashSet<int> hitPlayers) || !hitPlayers.Add(playerIndex))
            {
                return;
            }

            int heal = Math.Min(Math.Max(damage, 100), NPC.lifeMax - NPC.life);
            if (heal <= 0)
            {
                return;
            }

            NPC.life += heal;
            NPC.netUpdate = true;
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.CombatTextInt, number: unchecked((int)Color.LightGreen.PackedValue), number2: NPC.Center.X,
                    number3: NPC.Top.Y - 20f, number4: heal);
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)2);
                packet.Write(NPC.Center.X);
                packet.Write(NPC.Center.Y);
                packet.Send();
            }
            else
            {
                CombatText.NewText(new Rectangle((int)NPC.Center.X, (int)NPC.Top.Y - 20, 0, 0), Color.LightGreen, heal);
                SoundEngine.PlaySound(SoundID.Item29 with { MaxInstances = 0 }, NPC.Center);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * bossAdjustment * balance);
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.None;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.RemoveWhere(rule => true);
        }

        public override bool CheckDead()
        {
            if (AI_State != (float)ActionState.Dying)
            {
                AI_State = (float)ActionState.Dying;
                AI_Timer = 0f;
                AI_CastTimer = 0f;
                AI_CastTimerMax = 0f;
                NPC.damage = 0;
                NPC.life = 1;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }

            if (AI_Timer < 20f)
            {
                NPC.life = 1;
                return false;
            }

            return true;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(AI_CastTimer);
            writer.Write(onlyOnePlayerAlive);
            writer.Write(SpearChargeDestination.X);
            writer.Write(SpearChargeDestination.Y);
            writer.Write(CandleOfPromiseSide);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            AI_CastTimer = reader.ReadSingle();
            onlyOnePlayerAlive = reader.ReadBoolean();
            SpearChargeDestination = new Vector2(reader.ReadSingle(), reader.ReadSingle());
            CandleOfPromiseSide = reader.ReadInt32();
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, Vector3.One);

            if (IsSpearCharging)
            {
                NPC.noTileCollide = false;
            }

            IsSpearCharging = false;

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (player.active)
                {
                    player.AddBuff(ModContent.BuffType<CelestialAugury>(), 2, quiet: true);
                }
            }

            NPC.velocity *= 0.98f;

            if (AI_State == (float)ActionState.Dying)
            {
                DeathAnimation();
                return;
            }

            FindTargetPlayer();
            if (IfAllPlayersAreDead())
            {
                return;
            }

            AttackTimer = Main.expertMode ? 100 : 120;
            NPC.direction = Main.player[NPC.target].Center.X < NPC.Center.X ? -1 : 1;
            SuppressCastSprite = false;

            switch (AI_State)
            {
                case (float)ActionState.Spawning:
                    SpawnAnimation();
                    break;
                case (float)ActionState.Idle:
                    Idle();
                    break;
                case (float)ActionState.Casting:
                    Casting();
                    break;
                case (float)ActionState.PersistentCast:
                    PersistentCast();
                    break;
            }
            //Replace some mechanics if only one player is active.
            if (AI_State == (float)ActionState.Idle && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int alivePlayers = 0;
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (player.active && !player.dead && !player.ghost)
                    {
                        alivePlayers++;
                    }
                }

                onlyOnePlayerAlive = alivePlayers == 1;
            }
            if (AI_Timer >= AttackTimer)
            {
                switch (AI_RotationNumber)
                {             
                    case 0:
                        ArenaChangePhase1(Main.player[NPC.target], NPC);
                        break;
                    case 1:
                        EdictOfTheEnd(Main.player[NPC.target], NPC);
                        break;
                    case 2:
                        if (onlyOnePlayerAlive)
                        {
                            ConstellationTrailing(Main.player[NPC.target], NPC);
                        }
                        else
                        {
                            Expungence(Main.player[NPC.target], NPC);
                        }
                        break;
                    case 3:
                        CandleOfPromiseRandomSide(Main.player[NPC.target], NPC);
                        break;
                    case 4:
                        ConstellationCardinalFocusPlayers(Main.player[NPC.target], NPC);
                        break;
                    case 5:
                        SpearOfNavigation(Main.player[NPC.target], NPC);
                        break;
                    case 6:
                        SpearOfNavigation(Main.player[NPC.target], NPC);
                        break;
                    case 7:
                        GoddessOfMemoryTeleportToArenaCenter(Main.player[NPC.target], NPC);
                        break;
                    case 8:
                        ConstellationTrailing(Main.player[NPC.target], NPC);
                        break;
                    case 9:
                        HammerOfRecognition(Main.player[NPC.target], NPC);
                        break;
                    case 10:
                        CandleOfPromiseRandomSide(Main.player[NPC.target], NPC);
                        break;
                    case 11:
                        ConstellationMirroredTarget(Main.player[NPC.target], NPC);
                        break;
                    case 12:
                        ConstellationCardinalIntercardinal(Main.player[NPC.target], NPC);
                        break;
                    case 13:
                        SpearOfNavigation(Main.player[NPC.target], NPC);
                        break;
                    case 14:
                        HammerOfRecognition(Main.player[NPC.target], NPC);
                        break;
                    case 15:
                        HammerOfRecognitionFocusTarget(Main.player[NPC.target], NPC);
                        break;
                    case 16:
                        ArenaChangeIntermission(Main.player[NPC.target], NPC);
                        break;
                    default:
                        AI_RotationNumber = 0;
                        break;
                }
                /*
                switch (AI_RotationNumber)
                {
                    case 0:
                        ConstellationMirroredTarget(Main.player[NPC.target], NPC);
                        break;
                    case 1:
                        ConstellationCardinalFocusPlayers(Main.player[NPC.target], NPC);
                        break;
                    case 2:
                        ConstellationCardinalIntercardinal(Main.player[NPC.target], NPC);
                        break;
                    case 3:
                        ConstellationTrailing(Main.player[NPC.target], NPC);
                        break;
                    case 4:
                        if (onlyOnePlayerAlive)
                        {
                            ConstellationTrailing(Main.player[NPC.target], NPC);
                        }
                        else
                        {
                            Expungence(Main.player[NPC.target], NPC);
                        }
                        break;
                    case 5:
                        ArenaChangePhase1(Main.player[NPC.target], NPC);
                        break;
                    case 6:
                        EdictOfTheEnd(Main.player[NPC.target], NPC);   
                        break;
                    case 7:
                        if (onlyOnePlayerAlive)
                        {
                            ConstellationTrailing(Main.player[NPC.target], NPC);
                        }
                        else
                        {
                            Expungence(Main.player[NPC.target], NPC);
                        }
                        break;
                    case 8:
                        ConstellationMirroredTarget(Main.player[NPC.target], NPC);
                        break;
                    case 9:
                        ConstellationCardinalFocusPlayers(Main.player[NPC.target], NPC);
                        break;
                    case 10:
                        ConstellationCardinalIntercardinal(Main.player[NPC.target], NPC);
                        break;
                    case 11:
                        ConstellationTrailing(Main.player[NPC.target], NPC);
                        break;
                    case 12:
                        ArenaChangeIntermission(Main.player[NPC.target], NPC);
                        break;
                    default:
                        AI_RotationNumber = 0;
                        break;
                }
                */
            }

            MaintainCastSprite();
            EmitCastRing();
            UpdateCastBar();
        }

        private void MaintainCastSprite()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || AI_State != (float)ActionState.Casting || SuppressCastSprite || FindActiveSprite() != null)
            {
                return;
            }

            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<GoddessOfMemoryCast>(), 0, 0f, Main.myPlayer);
        }

        private void EmitCastRing()
        {
            if (Main.dedServ || AI_State != (float)ActionState.Casting || AI_CastTimerMax <= 0f || (int)AI_CastTimer % 3 != 0)
            {
                return;
            }

            float duration = AI_CastTimerMax;
            float progress = MathHelper.Clamp(AI_CastTimer / duration, 0f, 1f);
            if (progress >= 1f)
            {
                return;
            }

            float radius = 240f * (1f - progress);
            float inwardSpeed = 240f / duration;
            Vector2 center = NPC.Center;
            for (int i = 0; i < 48; i++)
            {
                Vector2 direction = (MathHelper.TwoPi * i / 48f).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(center + direction * radius, DustID.GemRuby, -direction * inwardSpeed, 0, default, 1.1f);
                dust.noGravity = true;
                dust.noLight = false;
            }
        }

        private static Projectile FindActiveSprite()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.ModProjectile is GoddessOfMemorySpriteProjectile)
                {
                    return projectile;
                }
            }

            return null;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Projectile sprite = FindActiveSprite();
            Texture2D godRays = ModContent.Request<Texture2D>("StarsAbove/Effects/GodRays").Value;
            Vector2 drawPosition = NPC.Center - screenPos;
            Vector2 origin = godRays.Size() * 0.5f;
            float rotation = Main.GlobalTimeWrappedHourly * 0.12f;
            float opacity = NPC.Opacity * (sprite?.Opacity ?? 1f);
            Color color = new Color(255, 255, 255, 0) * opacity;
            spriteBatch.Draw(godRays, drawPosition, null, color * 0.3f, rotation, origin, NPC.scale * 1.7f, SpriteEffects.None, 0f);
            spriteBatch.Draw(godRays, drawPosition, null, color * 0.45f, -rotation, origin, NPC.scale * 1.1f, SpriteEffects.None, 0f);
            if (sprite == null)
            {
                Texture2D texture = TextureAssets.Npc[Type].Value;
                SpriteEffects effects = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                spriteBatch.Draw(texture, drawPosition, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, effects, 0f);
            }

            return false;
        }

        private void UpdateCastBar()
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
            bossPlayer.GoddessOfMemoryBarActive = true;
            bossPlayer.CastTime = AI_State == (float)ActionState.Casting ? (int)AI_CastTimer : 0;
            bossPlayer.CastTimeMax = AI_State == (float)ActionState.Casting ? (int)AI_CastTimerMax : 0;

            if (AI_State == (float)ActionState.Spawning || AI_State == (float)ActionState.Idle)
            {
                bossPlayer.NextAttack = "";
            }
        }

        private void FindTargetPlayer()
        {
            if (!NPC.HasValidTarget)
            {
                NPC.TargetClosest(false);
            }
        }

        private bool IfAllPlayersAreDead()
        {
            if (NPC.HasValidTarget)
            {
                return false;
            }

            if (GoddessOfMemoryArenaSystem.IsActive && Main.netMode != NetmodeID.SinglePlayer)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    if (Main.player[i].active)
                    {
                        NPC.velocity = Vector2.Zero;
                        return true;
                    }
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.active = false;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
                }
            }

            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            bool isCasting = !SuppressCastSprite && (AI_State == (float)ActionState.Casting || AI_State == (float)ActionState.PersistentCast);

            if (isCasting)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (int)Frame.Casting * frameHeight;
                return;
            }

            NPC.frame.Y = ((int)Frame.Idle1 + (int)(NPC.frameCounter / IdleFrameDuration)) * frameHeight;
            NPC.frameCounter = (NPC.frameCounter + 1) % (8 * IdleFrameDuration);
        }

        public override bool? CanFallThroughPlatforms()
        {
            return false;
        }

        private void DeathAnimation()
        {
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.velocity *= 0.95f;
            AI_Timer++;
            NPC.alpha = (int)(255f * MathHelper.Clamp(AI_Timer / 20f, 0f, 1f));

            if (AI_Timer >= 20f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 0);
                NPC.checkDead();
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
                }
            }
        }

        private void SpawnAnimation()
        {
            AI_Timer = 0f;
            AI_RotationNumber = 0f;
            AI_CastTimer = 0f;
            AI_CastTimerMax = 0f;
            AI_State = (float)ActionState.Idle;
            NPC.netUpdate = true;
        }

        private void Idle()
        {
            NPC.noGravity = true;
            if (AI_Timer < AttackTimer)
            {
                AI_Timer++;
                if (AI_Timer == 0f)
                {
                    AI_Timer = AttackTimer;
                }
            }
        }

        private void Casting()
        {
            AI_CastTimer++;
        }

        private void PersistentCast()
        {
        }
    }
}
