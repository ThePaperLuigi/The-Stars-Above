using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Other.DreadmotherDarkIdol
{
    public class DreadmotherMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Satanael");
            // Description.SetDefault("'You would slay gods to protect the justice you believe in...'");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.Other.DreadmotherDarkIdol.DreadmotherFlyingMinion>()] > 0)
            {
                modPlayer.dreadmotherMinion = true;
                player.statDefense += (int)(player.maxMinions - player.slotsMinions);
                player.GetDamage(DamageClass.Generic) += (int)(player.maxMinions - player.slotsMinions)/100;
            }
            if (!modPlayer.dreadmotherMinion)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;

            }
        }
    }
}