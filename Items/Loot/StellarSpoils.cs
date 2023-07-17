using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using StarsAbove.NPCs;
using StarsAbove.Items.Prisms;
using Terraria.GameContent.ItemDropRules;
using StarsAbove.Items.Placeable.StellarFoci;
using StarsAbove.Systems;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Pets;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Placeable;
using StarsAbove.Items.Accessories;
using StarsAbove.Items.Tools;
using StarsAbove.Items.Vanity;
using StarsAbove.Items.Vanity.Starpop;
using StarsAbove.Items.Vanity.QuantumButterfly;
using StarsAbove.Items.Vanity.BaselessBlade;
using StarsAbove.Items.Vanity.OceanHuedHunter;
using StarsAbove.Items.Armor.StarfarerArmor;

namespace StarsAbove.Items.Loot
{
    // Basic code for a boss treasure bag
    public class StellarSpoils : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stellar Foci Grab Bag (Tier 1)");
			/* Tooltip.SetDefault("10% chance to upgrade upon opening" +
                "\n{$CommonItemTooltip.RightClickToOpen}"); */ // References a language key that says "Right Click To Open" in the language of the game

			//ItemID.Sets.BossBag[Type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
																			//Item.expert = true; // This makes sure that "Expert" displays in the tooltip and the item name color changes
		}

		public override bool CanRightClick()
		{
			return true;
		}
		public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            //Bags only! 5% chance for a music box.
            itemLoot.Add(ItemDropRule.OneFromOptions(20,
                ItemType<CosmicWillMusicBox>(),
                ItemType<ElpisMusicBox>(),
                ItemType<FirstWarningMusicBox>(),
                ItemType<FleetingMomentMusicBox>(),
                ItemType<MageOfVioletMusicBox>(),
                ItemType<MightOfTheHellbladeMusicBox>(),
                ItemType<SecondWarningMusicBox>(),
                ItemType<ShadowsCastByTheMightyMusicBox>(),
                ItemType<SunsetStardustMusicBox>(),
                ItemType<TheExtremeMusicBox>(),
                ItemType<ToTheEdgeMusicBox>(),
                ItemType<UnmatchingPiecesMusicBox>(),
                ItemType<VoyageMusicBox>()
                ));

            SetupStellarSpoils(itemLoot);
        }

        public static void SetupStellarSpoils(ItemLoot itemLoot)
        {
            //1/100 chance to get 10 Remnants. If not, guarantee 1, and 30% chance for 1 more.
            itemLoot.Add(ItemDropRule.Common(ItemType<StellarRemnant>(), 100, 10, 10))
                .OnFailedRoll(itemLoot.Add(ItemDropRule.Common(ItemType<StellarRemnant>()))
                .OnSuccess(itemLoot.Add(ItemDropRule.Common(ItemType<StellarRemnant>(), 3))));

            //25% chance to give a random pet.
            itemLoot.Add(ItemDropRule.OneFromOptions(4,
                ItemType<AegisCrystal>(),
                ItemType<Astrobread>(),
                ItemType<BladeWolfPetItem>(),
                ItemType<BugHat>(),
                ItemType<CrystalTowerFigure>(),
                ItemType<DemonicRibbon>(),
                ItemType<DigiEgg>(),
                ItemType<Digivice>(),
                ItemType<DragonEgg>(),
                ItemType<DustyCartridge>(),
                ItemType<FerryLikeness>(),
                ItemType<FuneralPass>(),
                ItemType<GhostPet>(),
                ItemType<HolographicToken>(),
                ItemType<MonochromeKnife>(),
                ItemType<MysticalCore>(),
                ItemType<MysticPokeball>(),
                ItemType<NecoArcItem>(),
                ItemType<PetalsOfKur>(),
                ItemType<ProgenitorShard>(),
                ItemType<RockstarGuitar>(),
                ItemType<SlightlyParanormalCap>(),
                ItemType<StarstruckAxe>(),
                ItemType<SweetberryBranch>(),
                ItemType<TNTCarrot>(),
                ItemType<ToyDemonfireDagger>(),
                ItemType<TunaMicrophone>()
                ));

            //10% chance for a Stellar Spoil unique accessory/utility.
            itemLoot.Add(ItemDropRule.OneFromOptions(10,
                ItemType<GaleflameFeather>(),
                ItemType<Glitterglue>(),
                ItemType<LamentingPocketwatch>(),
                ItemType<Luciferium>(),
                ItemType<PerfectlyGenericAccessory>(),
                ItemType<ToMurder>(),
                ItemType<EmberFlask>(),
                ItemType<ShepherdSunstone>()
                ));

            IItemDropRule StarArmorRule = ItemDropRule.Common(ItemType<StarArmorHead>(), 1);
            StarArmorRule.OnSuccess(ItemDropRule.Common(ItemType<StarArmorTop>(), 1));
            StarArmorRule.OnSuccess(ItemDropRule.Common(ItemType<StarArmorLegs>(), 1));

            IItemDropRule StarpopRule = ItemDropRule.Common(ItemType<StarpopHead>(), 1);
            StarpopRule.OnSuccess(ItemDropRule.Common(ItemType<StarpopBody>(), 1));
            StarpopRule.OnSuccess(ItemDropRule.Common(ItemType<StarpopLegs>(), 1));

            IItemDropRule QuantumButterflyRule = ItemDropRule.Common(ItemType<QuantumButterflyBody>(), 1);
            QuantumButterflyRule.OnSuccess(ItemDropRule.Common(ItemType<QuantumButterflyLegs>(), 1));

            IItemDropRule BaselessBladeRule = ItemDropRule.Common(ItemType<BaselessBladeBody>(), 1);
            BaselessBladeRule.OnSuccess(ItemDropRule.Common(ItemType<BaselessBladeLegs>(), 1));

            IItemDropRule OceanHuedHunterRule = ItemDropRule.Common(ItemType<OceanHuedHunterHead>(), 1);
            OceanHuedHunterRule.OnSuccess(ItemDropRule.Common(ItemType<OceanHuedHunterBody>(), 1));
            OceanHuedHunterRule.OnSuccess(ItemDropRule.Common(ItemType<OceanHuedHunterLegs>(), 1));

            //10% chance for a vanity set
            itemLoot.Add(new OneFromRulesRule(10,
                StarArmorRule,
                StarpopRule,
                QuantumButterflyRule,
                OceanHuedHunterRule,
                BaselessBladeRule
                ));

            //1% chance for a Starfarer Vanity
            itemLoot.Add(ItemDropRule.OneFromOptions(100,
                ItemType<FamiliarLookingAttire>(),
                ItemType<StellarCasualAttire>(),
                ItemType<GarmentsOfWinterRainAttire>(),
                ItemType<SeventhSigilAutumnAttire>()
                ));

            //1% chance for another bag (Expert mode exclusive!)
            itemLoot.Add(ItemDropRule.Common(ItemType<StellarSpoils>(), 100, 1, 1));
        }
        public static void SetupBossStellarSpoils(NPCLoot itemLoot)
        {
            //All of this uses the Not Expert rule because boss bags use the above method.
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            //1/100 chance to get 10 Remnants. If not, guarantee 1, and 30% chance for 1 more.
            notExpertRule.OnSuccess(
            itemLoot.Add(ItemDropRule.Common(ItemType<StellarRemnant>(), 100, 10, 10))
                .OnFailedRoll(itemLoot.Add(ItemDropRule.Common(ItemType<StellarRemnant>()))
                .OnSuccess(itemLoot.Add(ItemDropRule.Common(ItemType<StellarRemnant>(), 3))))
                );

            //25% chance to give a random pet.
            notExpertRule.OnSuccess(
            itemLoot.Add(ItemDropRule.OneFromOptions(4,
                ItemType<AegisCrystal>(),
                ItemType<Astrobread>(),
                ItemType<BladeWolfPetItem>(),
                ItemType<BugHat>(),
                ItemType<CrystalTowerFigure>(),
                ItemType<DemonicRibbon>(),
                ItemType<DigiEgg>(),
                ItemType<Digivice>(),
                ItemType<DragonEgg>(),
                ItemType<DustyCartridge>(),
                ItemType<FerryLikeness>(),
                ItemType<FuneralPass>(),
                ItemType<GhostPet>(),
                ItemType<HolographicToken>(),
                ItemType<MonochromeKnife>(),
                ItemType<MysticalCore>(),
                ItemType<MysticPokeball>(),
                ItemType<NecoArcItem>(),
                ItemType<PetalsOfKur>(),
                ItemType<ProgenitorShard>(),
                ItemType<RockstarGuitar>(),
                ItemType<SlightlyParanormalCap>(),
                ItemType<StarstruckAxe>(),
                ItemType<SweetberryBranch>(),
                ItemType<TNTCarrot>(),
                ItemType<ToyDemonfireDagger>(),
                ItemType<TunaMicrophone>()
                ))
            );


            //10% chance for a Stellar Spoil unique accessory/utility.
            notExpertRule.OnSuccess(
            itemLoot.Add(ItemDropRule.OneFromOptions(10,
                ItemType<GaleflameFeather>(),
                ItemType<Glitterglue>(),
                ItemType<LamentingPocketwatch>(),
                ItemType<Luciferium>(),
                ItemType<PerfectlyGenericAccessory>(),
                ItemType<ToMurder>(),
                ItemType<EmberFlask>(),
                ItemType<ShepherdSunstone>()
                ))
            );

            IItemDropRule StarArmorRule = ItemDropRule.Common(ItemType<StarArmorHead>(), 1);
            StarArmorRule.OnSuccess(ItemDropRule.Common(ItemType<StarArmorTop>(), 1));
            StarArmorRule.OnSuccess(ItemDropRule.Common(ItemType<StarArmorLegs>(), 1));

            IItemDropRule StarpopRule = ItemDropRule.Common(ItemType<StarpopHead>(), 1);
            StarpopRule.OnSuccess(ItemDropRule.Common(ItemType<StarpopBody>(), 1));
            StarpopRule.OnSuccess(ItemDropRule.Common(ItemType<StarpopLegs>(), 1));

            IItemDropRule QuantumButterflyRule = ItemDropRule.Common(ItemType<QuantumButterflyBody>(), 1);
            QuantumButterflyRule.OnSuccess(ItemDropRule.Common(ItemType<QuantumButterflyLegs>(), 1));

            IItemDropRule BaselessBladeRule = ItemDropRule.Common(ItemType<BaselessBladeBody>(), 1);
            BaselessBladeRule.OnSuccess(ItemDropRule.Common(ItemType<BaselessBladeLegs>(), 1));

            IItemDropRule OceanHuedHunterRule = ItemDropRule.Common(ItemType<OceanHuedHunterHead>(), 1);
            OceanHuedHunterRule.OnSuccess(ItemDropRule.Common(ItemType<OceanHuedHunterBody>(), 1));
            OceanHuedHunterRule.OnSuccess(ItemDropRule.Common(ItemType<OceanHuedHunterLegs>(), 1));

            //10% chance for a vanity set
            notExpertRule.OnSuccess(
            itemLoot.Add(new OneFromRulesRule(10,
                StarArmorRule,
                StarpopRule,
                QuantumButterflyRule,
                OceanHuedHunterRule,
                BaselessBladeRule
                ))
            );

            //1% chance for a Starfarer Vanity
            notExpertRule.OnSuccess(
            itemLoot.Add(ItemDropRule.OneFromOptions(100,
                ItemType<FamiliarLookingAttire>(),
                ItemType<StellarCasualAttire>(),
                ItemType<GarmentsOfWinterRainAttire>(),
                ItemType<SeventhSigilAutumnAttire>()
                ))
            );
        }

        // Below is code for the visuals

        public override Color? GetAlpha(Color lightColor)
		{
			// Makes sure the dropped bag is always visible
			return Color.Lerp(lightColor, Color.White, 0.4f);
		}

		public override void PostUpdate()
		{
			// Spawn some light and dust when dropped in the world
			Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);

			if (Item.timeSinceItemSpawned % 12 == 0)
			{
				Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

				// This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
				Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
				float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
				Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

				Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
				dust.scale = 0.5f;
				dust.fadeIn = 1.1f;
				dust.noGravity = true;
				dust.noLight = true;
				dust.alpha = 0;
			}
		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			// Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
			Texture2D texture = TextureAssets.Item[Item.type].Value;

			Rectangle frame;

			if (Main.itemAnimations[Item.type] != null)
			{
				// In case this item is animated, this picks the correct frame
				frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
			}
			else
			{
				frame = texture.Frame();
			}

			Vector2 frameOrigin = frame.Size() / 2f;
			Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
			Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

			float time = Main.GlobalTimeWrappedHourly;
			float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

			time %= 4f;
			time /= 2f;

			if (time >= 1f)
			{
				time = 2f - time;
			}

			time = time * 0.5f + 0.5f;

			for (float i = 0f; i < 1f; i += 0.25f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;

				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			return true;
		}
	}
}