using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.GoddessOfMemory
{
    public abstract class GoddessOfMemoryBuff : ModBuff
    {
        protected abstract string BuffName { get; }
        protected virtual string BuffDescription => string.Empty;

        public override LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), () => BuffName);
        public override LocalizedText Description => this.GetLocalization(nameof(Description), () => BuffDescription);

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }
    }
}
