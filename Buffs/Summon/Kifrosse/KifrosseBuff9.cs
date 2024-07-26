using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Summon.Kifrosse
{
    public class KifrosseBuff9 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kifrosse (9 Tails)");
            // Description.SetDefault("Mystic energy chills the air around you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.Summon.Kifrosse.Kifrosse9>()] > 0)
            {
                modPlayer.Kifrosse9 = true;
            }
            if (!modPlayer.Kifrosse9)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;

            }

            if (player.whoAmI == Main.myPlayer && player.controlDown && player.releaseDown
                && player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
            {
                if (!player.HasBuff(BuffType<AmaterasuWinterCooldown>()))
                {
                    player.AddBuff(BuffType<AmaterasuWinter>(), 240);
                    player.AddBuff(BuffType<AmaterasuWinterCooldown>(), 2400);
                }

            }
            player.GetDamage(DamageClass.Summon) += 0.45f;

        }
    }
}