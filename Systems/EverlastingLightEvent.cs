
using Microsoft.Xna.Framework;
using SubworldLibrary;
using System.IO;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Systems
{


    public class EverlastingLightEvent : ModSystem
    {
        public static bool isEverlastingLightActive = false;
        public static bool isEverlastingLightPreviewActive = false;
        public static int afterMoonLordDayTimer = 0;
        public static int daysAfterMoonLord = 0;

        public static int daysUntilEverlastingLightPreview = 2;
        public static int daysUntilEverlastingLight = 7;

        public override void OnWorldLoad()
        {
            isEverlastingLightActive = false;
            isEverlastingLightPreviewActive = false;
            afterMoonLordDayTimer = 0;
            daysAfterMoonLord = 0;

        }

        public override void OnWorldUnload()
        {
            isEverlastingLightActive = false;
            isEverlastingLightPreviewActive = false;
            afterMoonLordDayTimer = 0;
            daysAfterMoonLord = 0;

        }
        public override void SaveWorldData(TagCompound tag)
        {
            if (isEverlastingLightActive)
            {
                tag["isEverlastingLightActive"] = isEverlastingLightActive;
            }
            if (isEverlastingLightPreviewActive)
            {
                tag["isEverlastingLightPreviewActive"] = isEverlastingLightPreviewActive;
            }
            if (afterMoonLordDayTimer > 0)
            {
                tag["afterMoonLordDayTimer"] = afterMoonLordDayTimer;
            }
            if (daysAfterMoonLord > 0)
            {
                tag["daysAfterMoonLord"] = daysAfterMoonLord;
            }
            if (daysUntilEverlastingLight > 0)
            {
                tag["daysLight"] = daysUntilEverlastingLight;
            }
            if (daysUntilEverlastingLightPreview > 0)
            {
                tag["daysLightPreview"] = daysUntilEverlastingLightPreview;
            }

        }

        public override void LoadWorldData(TagCompound tag)
        {
            isEverlastingLightActive = tag.GetBool("isEverlastingLightActive");
            isEverlastingLightPreviewActive = tag.GetBool("isEverlastingLightPreviewActive");
            afterMoonLordDayTimer = tag.GetInt("afterMoonLordDayTimer");
            daysAfterMoonLord = tag.GetInt("daysAfterMoonLord");
            daysUntilEverlastingLight = tag.GetInt("daysLight");
            daysUntilEverlastingLightPreview = tag.GetInt("daysLightPreview");
        }

        public override void NetSend(BinaryWriter writer)
        {
            //Order of operations is important and has to match that of NetReceive
            var flags = new BitsByte();
            flags[0] = isEverlastingLightActive;
            flags[1] = isEverlastingLightPreviewActive;
            writer.Write(flags);

            writer.Write(daysAfterMoonLord);
            writer.Write(afterMoonLordDayTimer);
            writer.Write(daysUntilEverlastingLight);
            writer.Write(daysUntilEverlastingLightPreview);

        }

        public override void NetReceive(BinaryReader reader)
        {

            BitsByte flags = reader.ReadByte();
            isEverlastingLightActive = flags[0];
            isEverlastingLightPreviewActive = flags[1];

            daysAfterMoonLord = reader.ReadInt32();
            afterMoonLordDayTimer = reader.ReadInt32();
            daysUntilEverlastingLight = reader.ReadInt32();
            daysUntilEverlastingLightPreview = reader.ReadInt32();
        }

        public override void PostUpdateTime()
        {
            //Of course, no Everlasting Light after the Warrior of Light is defeated.
            if (DownedBossSystem.downedWarrior)
            {
                isEverlastingLightActive = false;
                isEverlastingLightPreviewActive = false;
                return;
            }
            if (NPC.downedMoonlord)
            {
                afterMoonLordDayTimer++;
                if (afterMoonLordDayTimer >= Main.dayLength)
                {
                    daysAfterMoonLord++;
                    afterMoonLordDayTimer = 0;
                }
                if (daysAfterMoonLord >= daysUntilEverlastingLightPreview && daysAfterMoonLord < daysUntilEverlastingLight)
                {
                    isEverlastingLightPreviewActive = true;
                }
                else
                {
                    isEverlastingLightPreviewActive = false;

                }
                if (daysAfterMoonLord >= daysUntilEverlastingLight)
                {
                    isEverlastingLightActive = true;
                }
                else
                {
                    isEverlastingLightActive = false;

                }

                //Main.NewText(Language.GetTextValue($"(Debug) Days after Moon Lord (Day Timer): {afterMoonLordDayTimer}"), 220, 100, 247);
                //Main.NewText(Language.GetTextValue($"(Debug) Days after Moon Lord: {daysAfterMoonLord}"), 220, 100, 247);

                if (!isEverlastingLightActive && !isEverlastingLightPreviewActive)
                {
                    //Main.NewText(Language.GetTextValue($"(Debug) Everlasting Light State: Off"), 220, 100, 247);
                }
                else if (isEverlastingLightPreviewActive)
                {
                    //Main.NewText(Language.GetTextValue($"(Debug) Everlasting Light State: Harsh Light"), 220, 100, 247);
                }
                else
                {
                    //Main.NewText(Language.GetTextValue($"(Debug) Everlasting Light State: Light Everlasting"), 220, 100, 247);
                }

            }
            else
            {

                isEverlastingLightActive = false;
                isEverlastingLightPreviewActive = false;
            }

        }

        public override void PreUpdateInvasions()
        {

            if (isEverlastingLightActive)
            {
                Main.eclipse = false;
            }
        }

        public override void ResetNearbyTileEffects()
        {
            StarsAbovePlayer modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            modPlayer.lightMonolith = false;


        }


    }

    public class EverlastingLightPlayer : ModPlayer
    {
        public override void PreUpdateBuffs()
        {
            if (EverlastingLightEvent.isEverlastingLightPreviewActive && SubworldSystem.Current == null)
            {

                Player.AddBuff(BuffType<Buffs.EverlastingLightPreview>(), 10);


            }
            if (EverlastingLightEvent.isEverlastingLightActive && SubworldSystem.Current == null)
            {

                Player.AddBuff(BuffType<Buffs.EverlastingLight>(), 10);


            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.EverlastingLight>()) || Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().lightMonolith)
            {

                Vector2 position = Main.LocalPlayer.position;

                if (Main.LocalPlayer.ZoneOverworldHeight)
                {
                    Dust.NewDust(new Vector2(position.X - 1200, position.Y - 550), 2200, 1, 64, 0f, 4f, 64, default, 1.5f);
                    Dust.NewDust(new Vector2(position.X - 1200, position.Y - 550), 2200, 1, 64, 0f, 4f, 64, default, 1.3f);
                    Dust.NewDust(new Vector2(position.X - 1200, position.Y - 550), 2200, 1, 64, 0f, 4f, 64, default, 0.9f);
                    Dust.NewDust(new Vector2(position.X - 1200, position.Y - 550), 2200, 1, 64, 0f, 4f, 64, default, 2f);
                }

            }
        }

    }
}
