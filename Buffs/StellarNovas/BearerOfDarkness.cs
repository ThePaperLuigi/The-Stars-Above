using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.StellarNovas
{
    public class BearerOfDarkness : ModBuff
    {
        public override void SetStaticDefaults()
        {
           
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        float modifier;
        public override void Update(Player player, ref int buffIndex)
        {
            if(player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
            {

            }
            else if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
            {

            }

        }
    }
    public class BearerOfDarknessPlayer : ModPlayer
    {
        public override void OnHurt(Player.HurtInfo info)
        {
            if(Player.HasBuff(BuffType<BearerOfDarkness>()) && Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
            {
                info.Damage = (int)(info.Damage * 0.9f);
            }
            
            base.OnHurt(info);
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Player.HasBuff(BuffType<BearerOfDarkness>()) || Player.HasBuff(BuffType<BearerOfDarkness>()) && Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
            {
                Player.Heal(30);
                Player.immune = true;
                Player.immuneTime = 60;
                Player.ClearBuff(BuffType<BearerOfDarkness>());
                Player.ClearBuff(BuffType<BearerOfLight>());
                return false;
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            

            base.OnHitNPC(target, hit, damageDone);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Player.HasBuff(BuffType<BearerOfDarkness>()) && Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
            {
                modifiers.CritDamage += 0.15f;
                
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}
