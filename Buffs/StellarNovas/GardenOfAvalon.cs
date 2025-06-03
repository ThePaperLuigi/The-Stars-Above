using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;


namespace StarsAbove.Buffs.StellarNovas
{

    public class GardenOfAvalon : ModBuff
    {
        //public const float buffRadius = 600; // 100ft, same as shared accessory info

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Garden of Avalon");
            // Description.SetDefault("Periodically heal health and mana with the strength of the Stellar Nova");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }
        int tickHeal = 0;
        public override void Update(Player player, ref int buffIndex)
        {
            tickHeal++;
            if (tickHeal >= 240)
            {
                Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 40, player.width, player.height); //Heal
                CombatText.NewText(textPos, new Color(63, 221, 53, 240), $"{(int)(player.GetModPlayer<StarsAbovePlayer>().novaDamage * (1 + player.GetModPlayer<StarsAbovePlayer>().novaDamageMod / 100))}", false, false);

                Rectangle textPos2 = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height); //Mana Heal
                CombatText.NewText(textPos2, new Color(50, 64, 205, 240), $"{(int)(player.GetModPlayer<StarsAbovePlayer>().novaDamage * (1 + player.GetModPlayer<StarsAbovePlayer>().novaDamageMod / 100))}", false, false);

                player.statLife += (int)(player.GetModPlayer<StarsAbovePlayer>().novaDamage * (1 + player.GetModPlayer<StarsAbovePlayer>().novaDamageMod));
                player.statMana += (int)(player.GetModPlayer<StarsAbovePlayer>().novaDamage * (1 + player.GetModPlayer<StarsAbovePlayer>().novaDamageMod));
                tickHeal = 0;
            }




            //player.GetDamage(DamageClass.Generic) *= 1.05f;

        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {


            return base.ReApply(player, time, buffIndex);
        }
    }
}
