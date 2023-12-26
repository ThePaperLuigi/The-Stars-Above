using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Boss
{
    public class BindingLight : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Binding Light");
            // Description.SetDefault("Entrapped by coils of light");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity = Microsoft.Xna.Framework.Vector2.Zero;
            player.gravity = 0f;//Test me
            player.GetModPlayer<BossPlayer>().QTEActive = true;
            player.frozen = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            

            base.Update(npc, ref buffIndex);
        }
        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
