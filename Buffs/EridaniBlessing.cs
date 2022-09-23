using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class EridaniBlessing : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[c/9F5DDE:Umbral Resonance]");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }
        public static bool disableAspectPenalty;
        public override void Update(Player player, ref int buffIndex)
        {
            //Natural passive unlocks
            player.statManaMax2 += 20;
            if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
            {
                if (!disableAspectPenalty)
                {
                    //player.GetDamage(DamageClass.Generic) *= 0.9f;
                }
            }

            //Stellar Array
            if (player.GetModPlayer<StarsAbovePlayer>().stellarArray == false)
            {
                if (player.GetModPlayer<StarsAbovePlayer>().starshower == 2)
                {
                    //player.starCloak = true;
                }
                if (player.GetModPlayer<StarsAbovePlayer>().evasionmastery == 2)
                {
                    //player.blackBelt = true;
                    //player.accRunSpeed *= 1.4f;
                    //player.maxRunSpeed 
                }
                if (player.GetModPlayer<StarsAbovePlayer>().ironskin == 2)
                {
                    player.statDefense += 6;

                }
                if (player.GetModPlayer<StarsAbovePlayer>().aquaaffinity == 2)
                {
                   
                   
                }
                if (player.GetModPlayer<StarsAbovePlayer>().bonus100hp == 2)
                {
                    player.statLifeMax2 += 150;
                    player.lifeRegen += 2;
                    if (player.statLife <= 200)
                    {
                        player.witheredArmor = true;
                    }
                }
                if (player.GetModPlayer<StarsAbovePlayer>().bloomingflames == 2)
                {
                    if (player.statLife < 100 || player.HasBuff(BuffType<InfernalEnd>()))
                    {
                        player.GetDamage(DamageClass.Generic) += 0.5f;

                    }
                }
                if (player.GetModPlayer<StarsAbovePlayer>().astralmantle == 2)
                {

                    player.statDefense += player.statMana / 10;

                }
                if (player.GetModPlayer<StarsAbovePlayer>().beyondinfinity == 2)
                {
                    if (player.statLife >= 500)
                    {
                        player.GetDamage(DamageClass.Generic) += 1;
                    }
                    if (player.statLife < 500)
                    {
                        player.GetDamage(DamageClass.Generic) -= 0.1f;
                    }
                    if (player.statLife < 400)
                    {
                        player.GetDamage(DamageClass.Generic) -= 0.1f;
                    }
                    if (player.statLife < 300)
                    {
                        player.GetDamage(DamageClass.Generic) -= 0.1f;
                    }
                    if (player.statLife < 200)
                    {
                        player.GetDamage(DamageClass.Generic) -= 0.1f;
                    }
                    if (player.statLife < 100)
                    {
                        player.GetDamage(DamageClass.Generic) -= 0.1f;
                    }
                }
                if (player.GetModPlayer<StarsAbovePlayer>().inneralchemy == 2)
                {
                    
                }

                if (player.GetModPlayer<StarsAbovePlayer>().avataroflight == 2)
                {
                    player.statLifeMax2 += (player.statManaMax2 / 2);
                    if (player.statLife >= 500)
                    {
                        player.statDefense += 10;
                        player.GetDamage(DamageClass.Generic) += 0.05f;
                    }
                }
                if (player.GetModPlayer<StarsAbovePlayer>().hikari == 2)
                {
                    player.GetDamage(DamageClass.Generic) += (0.01f * (player.statLifeMax2 / 20));
                    player.statDefense += (player.statLifeMax2 / 20);
                    
                }
                if (player.GetModPlayer<StarsAbovePlayer>().celestialevanesence == 2)
                {
                    player.GetCritChance(DamageClass.Generic) += player.statMana / 20;
                }
                if (player.GetModPlayer<StarsAbovePlayer>().afterburner == 2)
                {

                    if (player.statMana <= 40 && !Main.LocalPlayer.HasBuff(BuffType<Buffs.AfterburnerCooldown>()) && !Main.LocalPlayer.HasBuff(BuffType<Buffs.Afterburner>()))
                    {
                       
                        player.statMana += 150;
                        player.AddBuff(BuffType<Buffs.Afterburner>(), 240);

                    }
                }
                if (player.GetModPlayer<StarsAbovePlayer>().unbridledradiance == 2)
                {
                    //player.GetDamage(DamageClass.Generic) += (player.GetModPlayer<StarsAbovePlayer>().unbridledRadianceStack / 100000);
                }
            }
        }

       /*public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            Vector2 Center = new Vector2(drawParams.SourceRectangle.Width, drawParams.SourceRectangle.Height) / 2;
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.Draw(
                drawParams.Texture,
                new Vector2(drawParams.Position.X + 16, drawParams.Position.Y + 16),
                null,
                Color.White,
                MathHelper.ToRadians(-modPlayer.GlobalRotation),
                Center,
                1f,
                SpriteEffects.None,
                1f);



            return false;
        }*/
    }
}
