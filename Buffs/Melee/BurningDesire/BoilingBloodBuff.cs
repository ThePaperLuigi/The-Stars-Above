using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Melee.BurningDesire
{
    public class BoilingBloodBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boiling Blood");
            /* Description.SetDefault("Blazing strength empowers you, granting 30% increased damage, 30 defense, attack speed based on missing HP, and follow-up attacks" +
                "\nHowever, health regeneration is disabled and you are slowly losing HP over time" +
                "\n'It's not hot enough here!'"); */
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        int lifeDrainTimer;
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.3f;
            player.statDefense += 30;
            player.GetAttackSpeed(DamageClass.Generic) += (float)(player.statLifeMax2 - player.statLife) / player.statLifeMax2;
            player.lifeRegenTime = 10;
            lifeDrainTimer++;
            if (lifeDrainTimer > 20)
            {
                lifeDrainTimer = 0;
                if (player.statLife > 1)
                {
                    player.statLife--;

                }
            }

            if (player.HeldItem.ModItem is Items.Weapons.Melee.BurningDesire)
            {

            }
            else
            {
                player.ClearBuff(BuffType<BoilingBloodBuff>());
                buffIndex--;
            }

        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {



        }
        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {


            Vector2 shake = new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-1, 2));

            drawParams.Position += shake;
            drawParams.TextPosition += shake;


            return true;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
