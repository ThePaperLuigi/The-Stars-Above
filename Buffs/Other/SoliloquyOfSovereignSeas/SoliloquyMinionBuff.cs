using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Other.SoliloquyOfSovereignSeas
{
    public class SoliloquyMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Octopi");
            // Description.SetDefault("The void octopi surround you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
            if (player.ownedProjectileCounts[ProjectileType<Projectiles.Other.SoliloquyOfSovereignSeas.SoliloquyJellySummon>()] > 0 || player.ownedProjectileCounts[ProjectileType<Projectiles.Other.SoliloquyOfSovereignSeas.SoliloquySinger>()] > 0)
            {
                modPlayer.SoliloquyMinions = true;
            }
            if (!modPlayer.SoliloquyMinions)
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