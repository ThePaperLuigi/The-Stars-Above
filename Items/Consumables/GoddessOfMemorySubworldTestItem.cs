using StarsAbove.NPCs.GoddessOfMemory;
using StarsAbove.Subworlds;
using StarsAbove.Systems;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarsAbove.Items.Consumables
{
    public class GoddessOfMemorySubworldTestItem : ModItem
    {
        public override string Texture => "StarsAbove/Items/Consumables/MnemonicSigil";
        
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 13;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = false;
            Item.ResearchUnlockCount = 0;
        }

        public override bool CanUseItem(Player player)
        {
            return GoddessOfMemoryArenaSystem.IsActive
                ? !NPC.AnyNPCs(ModContent.NPCType<GoddessOfMemoryBoss>())
                : GoddessOfMemorySubworld.SupportsArenaStructures;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI != Main.myPlayer)
            {
                return true;
            }

            if (!GoddessOfMemoryArenaSystem.IsActive)
            {
                SubworldSystem.Enter<GoddessOfMemorySubworld>();
            }
            else
            {
                int type = ModContent.NPCType<GoddessOfMemoryBoss>();
                if (!NPC.AnyNPCs(type))
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                    }
                    else
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, type);
                    }
                }
            }

            return true;
        }
    }
}
