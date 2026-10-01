using Microsoft.Xna.Framework;
using StarsAbove.NPCs.GoddessOfMemory;
using StarsAbove.Utilities;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class BossHammerOfRecognition : ModProjectile
    {
        public const int CastDuration = 24;
        public const int TravelDuration = 60;
        private const float HitRadius = 50f;

        private readonly bool[] hitPlayers = new bool[Main.maxPlayers];
        private Vector2 launchPosition;
        private Vector2 previousCenter;
        private int spinDirection = 1;
        private bool throwSoundPlayed;

        public int BossIndex => (int)Projectile.ai[0];
        private Vector2 TargetPosition => new Vector2(Projectile.ai[1], Projectile.ai[2]);
        private float Age => Projectile.localAI[0];

        public override string Texture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/HammerOfRecognition";

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 2;
            Projectile.netImportant = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            launchPosition = Projectile.Center;
            previousCenter = Projectile.Center;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                spinDirection = Main.rand.NextBool() ? 1 : -1;
            }

            UpdateRotation();
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Age);
            writer.Write(launchPosition.X);
            writer.Write(launchPosition.Y);
            writer.Write(spinDirection);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadSingle();
            launchPosition = new Vector2(reader.ReadSingle(), reader.ReadSingle());
            spinDirection = reader.ReadInt32();
            previousCenter = Projectile.Center;
            UpdateRotation();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        private bool IsBossActive()
        {
            return BossIndex >= 0 && BossIndex < Main.maxNPCs && Main.npc[BossIndex].active &&
                Main.npc[BossIndex].ModNPC is GoddessOfMemoryBoss && Main.npc[BossIndex].ai[0] != (float)ActionState.Dying;
        }

        public override bool? CanDamage()
        {
            return Age > 0f && Age <= TravelDuration && IsBossActive();
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.active && !target.dead && !target.ghost && !hitPlayers[target.whoAmI];
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (CanDamage() != true)
            {
                return false;
            }

            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), previousCenter, Projectile.Center,
                HitRadius * 2f, ref collisionPoint);
        }

        public override void AI()
        {
            if (!IsBossActive())
            {
                Projectile.Kill();
                return;
            }

            Projectile.timeLeft = 2;
            Projectile.localAI[0]++;
            if (Age > TravelDuration)
            {
                Projectile.Kill();
                return;
            }

            if (!Main.dedServ && !throwSoundPlayed)
            {
                SoundEngine.PlaySound(SoundID.Item1 with { MaxInstances = 0 }, launchPosition);
                throwSoundPlayed = true;
            }

            previousCenter = Projectile.Center;
            float halfDuration = TravelDuration * 0.5f;
            if (Age <= halfDuration)
            {
                float progress = Age / halfDuration;
                Projectile.Center = Vector2.Lerp(launchPosition, TargetPosition, EaseHelper.OutQuad(progress));
            }
            else
            {
                float progress = (Age - halfDuration) / halfDuration;
                Projectile.Center = Vector2.Lerp(TargetPosition, Main.npc[BossIndex].Center, EaseHelper.InQuad(progress));
            }

            Projectile.velocity = Vector2.Zero;
            UpdateRotation();
            Lighting.AddLight(Projectile.Center, Vector3.One);
        }

        private void UpdateRotation()
        {
            Projectile.rotation = (TargetPosition - launchPosition).ToRotation() - MathHelper.PiOver4 + MathHelper.Pi +
                spinDirection * Age * MathHelper.PiOver4;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (target.whoAmI != Main.myPlayer || info.Damage <= 0 || hitPlayers[target.whoAmI] || !IsBossActive())
            {
                return;
            }

            hitPlayers[target.whoAmI] = true;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)1);
                packet.Write((short)BossIndex);
                packet.Write(Projectile.identity);
                packet.Write(info.Damage);
                packet.Send();
                return;
            }

            ((GoddessOfMemoryBoss)Main.npc[BossIndex].ModNPC).HealFromHammerOfRecognition(Projectile.identity, target.whoAmI, info.Damage);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
