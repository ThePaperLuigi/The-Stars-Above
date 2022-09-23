
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Items;
using StarsAbove.Items.Essences;
using StarsAbove.Prefixes;
using SubworldLibrary;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using StarsAbove.Items.Prisms;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent;
using StarsAbove.Items.Armor.StarfarerArmor;
using StarsAbove.Items.Materials;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using Terraria.UI.Chat;

namespace StarsAbove
{
	
	public class StarsAboveGlobalItem : GlobalItem
	{
		public override bool InstancePerEntity => true;
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            return base.Clone(item, itemClone);
        }
       
        public int oldType;// 1 is melee, 2 is ranged, 3 is magic, 4 is summon
		public DamageClass oldDamageClass;
		//public int aspect; //0 is Astral, 1 is Umbral, 2 is Dual Aspected.
		public bool spatialWeapon;
		public bool loadItem = true;

		public List<int> AstralWeapons = new List<int>() { 
			ModContent.ItemType<CarianDarkMoon>(),
			ModContent.ItemType<NeoDealmaker>(),
			ModContent.ItemType<DerFreischutz>(),
			ModContent.ItemType<Persephone>(),
			ModContent.ItemType<Skofnung>(),
			ModContent.ItemType<AegisDriver>(),
			ModContent.ItemType<KarlanTruesilver>(),
			ModContent.ItemType<IzanagiEdge>(),
			ModContent.ItemType<VenerationOfButterflies>(),
			ModContent.ItemType<RideTheBull>(),
			ModContent.ItemType<CrimsonOutbreak>(),
			ModContent.ItemType<StygianNymph>(),
			ModContent.ItemType<CrimsonKey>(),
			ModContent.ItemType<PhantomInTheMirror>(),
			ModContent.ItemType<PleniluneGaze>(),
			ModContent.ItemType<VisionOfEuthymia>(),
			ModContent.ItemType<RexLapis>(),
			ModContent.ItemType<LiberationBlazing>(),
			ModContent.ItemType<Suistrume>(),
			ModContent.ItemType<KeyOfTheKingsLaw>(),
			ModContent.ItemType<HunterSymphony>(),

			//ModContent.ItemType<EssenceOf>(),
			ModContent.ItemType<EssenceOfTheDarkMoon>(),
			ModContent.ItemType<EssenceOfBitterfrost>(),
			ModContent.ItemType<EssenceOfButterflies>(),
			ModContent.ItemType<EssenceOfDuality>(),
			ModContent.ItemType<EssenceOfEuthymia>(),
			ModContent.ItemType<EssenceOfIzanagi>(),
			ModContent.ItemType<EssenceOfLiberation>(),
			ModContent.ItemType<EssenceOfSilverAsh>(),
			ModContent.ItemType<EssenceOfSin>(),
			ModContent.ItemType<EssenceOfStarsong>(),
			ModContent.ItemType<EssenceOfTheAegis>(),
			ModContent.ItemType<EssenceOfTheAnomaly>(),
			ModContent.ItemType<EssenceOfTheBull>(),
			ModContent.ItemType<EssenceOfTheFreeshooter>(),
			ModContent.ItemType<EssenceOfTheMoonlitAdepti>(),
			ModContent.ItemType<EssenceOfThePhantom>(),
			ModContent.ItemType<EssenceOfTheSwarm>(),
			ModContent.ItemType<EssenceOfTheTreasury>(),
			ModContent.ItemType<EssenceOfTheUnderworldGoddess>(),
			ModContent.ItemType<EssenceOfTheUnyieldingEarth>(),
			ModContent.ItemType<EssenceOfTheHunt>(),
		};
		public List<int> UmbralWeapons = new List<int>() {
			ModContent.ItemType<KonpakuKatana>(),
			ModContent.ItemType<AshenAmbition>(),
			ModContent.ItemType<DeathInFourActs>(),
			ModContent.ItemType<KazimierzSeraphim>(),
			ModContent.ItemType<InugamiRipsaw>(),
			ModContent.ItemType<RadGun>(),
			ModContent.ItemType<EveryMomentMatters>(),
			ModContent.ItemType<HawkmoonMagic>(),ModContent.ItemType<HawkmoonRanged>(),
			ModContent.ItemType<MementoMuse>(),
			ModContent.ItemType<Drachenlance>(),
			ModContent.ItemType<VoiceOfTheFallen>(),
			ModContent.ItemType<CaesuraOfDespair>(),
			ModContent.ItemType<CrimsonSakuraAlpha>(),
			ModContent.ItemType<HollowheartAlbion>(),
			ModContent.ItemType<Tartaglia>(),
			ModContent.ItemType<KroniicAccelerator>(),
			ModContent.ItemType<YunlaiStiletto>(),
			ModContent.ItemType<Unforgotten>(),
			ModContent.ItemType<Naganadel>(),
			ModContent.ItemType<LightUnrelenting>(),
			ModContent.ItemType<SparkblossomBeacon>(),

			ModContent.ItemType<EssenceOfAlpha>(),
			ModContent.ItemType<EssenceOfAsh>(),
			ModContent.ItemType<EssenceOfAzakana>(),
			ModContent.ItemType<EssenceOfDeathsApprentice>(),
			ModContent.ItemType<EssenceOfDrivingThunder>(),
			ModContent.ItemType<EssenceOfFingers>(),
			ModContent.ItemType<EssenceOfIRyS>(),
			ModContent.ItemType<EssenceOfLunarDominion>(),
			ModContent.ItemType<EssenceOfPerfection>(),
			ModContent.ItemType<EssenceOfStyle>(),
			ModContent.ItemType<EssenceOfSurpassingLimits>(),
			ModContent.ItemType<EssenceOfTheDragonslayer>(),
			ModContent.ItemType<EssenceOfTheFallen>(),
			ModContent.ItemType<EssenceOfTheGardener>(),
			ModContent.ItemType<EssenceOfTheHarbinger>(),
			ModContent.ItemType<EssenceOfTheHawkmoon>(),
			ModContent.ItemType<EssenceOfTheHollowheart>(),
			ModContent.ItemType<EssenceOfThePegasus>(),
			ModContent.ItemType<EssenceOfTheSharpshooter>(),
			ModContent.ItemType<EssenceOfTime>(),
			ModContent.ItemType<EssenceOfStaticShock>(),

		};
		public List<int> SpatialWeapons = new List<int>(){

			ModContent.ItemType<Apalistik>(),
			ModContent.ItemType<MiserysCompany>(),
			ModContent.ItemType<AncientBook>(),
			ModContent.ItemType<LuminaryWand>(),
			ModContent.ItemType<DreadnoughtChemtank>(),
			ModContent.ItemType<ApalistikUpgraded>(),
			ModContent.ItemType<Xenoblade>(),
			ModContent.ItemType<SkyStrikerArms>(),
			ModContent.ItemType<ForceOfNature>(),
			ModContent.ItemType<Hullwrought>(),
			ModContent.ItemType<PenthesileaMuse>(),
			ModContent.ItemType<Kifrosse>(),
			ModContent.ItemType<Genocide>(),
			ModContent.ItemType<Mercy>(),
			ModContent.ItemType<ArachnidNeedlepoint>(),
			ModContent.ItemType<SakuraVengeance>(),
			ModContent.ItemType<TheOnlyThingIKnowForReal>(),
			ModContent.ItemType<TwinStars>(),
			ModContent.ItemType<Ozma>(),
			ModContent.ItemType<ClaimhSolais>(),
			ModContent.ItemType<MorningStar>(),

			ModContent.ItemType<EternalStar>(),
			ModContent.ItemType<VermillionDaemon>(),
			ModContent.ItemType<ShadowlessCerulean>(),
			ModContent.ItemType<HullwroughtMKII>(),
			ModContent.ItemType<IgnitionAstra>(),
			ModContent.ItemType<BuryTheLight>(),
			ModContent.ItemType<ArchitectLuminance>(),
			ModContent.ItemType<CosmicDestroyer>(),
			ModContent.ItemType<VirtuesEdge>(),
			ModContent.ItemType<UltimaThule>(),
			ModContent.ItemType<BloodBlade>(),
			ModContent.ItemType<RedMage>(),
			ModContent.ItemType<BurningDesire>(),

			ModContent.ItemType<EssenceOfAdagium>(),
			ModContent.ItemType<EssenceOfBloodshed>(),
			ModContent.ItemType<EssenceOfChemtech>(),
			ModContent.ItemType<EssenceOfEternity>(),
			ModContent.ItemType<EssenceOfFoxfire>(),
			ModContent.ItemType<EssenceOfInk>(),
			ModContent.ItemType<EssenceOfLuminance>(),
			ModContent.ItemType<EssenceOfMisery>(),
			ModContent.ItemType<EssenceOfOuterGods>(),
			ModContent.ItemType<EssenceOfRadiance>(),
			ModContent.ItemType<EssenceOfSakura>(),
			ModContent.ItemType<EssenceOfTechnology>(),
			ModContent.ItemType<EssenceOfTheAerialAce>(),
			ModContent.ItemType<EssenceOfTheAscendant>(),
			ModContent.ItemType<EssenceOfTheBeginningAndEnd>(),
			ModContent.ItemType<EssenceOfExplosions>(),
			ModContent.ItemType<EssenceOfTheBionis>(),
			ModContent.ItemType<EssenceOfTheChimera>(),
			ModContent.ItemType<EssenceOfTheCosmos>(),
			ModContent.ItemType<EssenceOfTheFuture>(),
			ModContent.ItemType<EssenceOfBlasting>(),
			ModContent.ItemType<EssenceOfTheGunlance>(),
			ModContent.ItemType<EssenceOfTheObservatory>(),
			ModContent.ItemType<EssenceOfTheOcean>(),
			ModContent.ItemType<EssenceOfTwinStars>(),
			ModContent.ItemType<EssenceOfVampirism>(),
			ModContent.ItemType<EssenceOfDestiny>(),
			ModContent.ItemType<EssenceOfBlood>(),
			ModContent.ItemType<EssenceOfLifethirsting>(),
			ModContent.ItemType<EssenceOfBalance>(),
			ModContent.ItemType<EssenceOfTheOverwhelmingBlaze>(),
		};

		public List<int> Prisms = new List<int>() {

			ModContent.ItemType<PrismaticCore>(),

			ModContent.ItemType<AlchemicPrism>(),
			ModContent.ItemType<ApocryphicPrism>(),
			ModContent.ItemType<CastellicPrism>(),
			ModContent.ItemType<CrystallinePrism>(),
			ModContent.ItemType<EmpressPrism>(),
			ModContent.ItemType<EverflamePrism>(),
			ModContent.ItemType<LightswornPrism>(),
			ModContent.ItemType<LihzahrdPrism>(),
			ModContent.ItemType<LucentPrism>(),
			ModContent.ItemType<LuminitePrism>(),
			ModContent.ItemType<MechanicalPrism>(),
			ModContent.ItemType<OvergrownPrism>(),
			ModContent.ItemType<PaintedPrism>(),
			ModContent.ItemType<PhylacticPrism>(),	
			ModContent.ItemType<RadiantPrism>(),
			ModContent.ItemType<RefulgentPrism>(),
			ModContent.ItemType<RoyalSlimePrism>(),
			ModContent.ItemType<SpatialPrism>(),
			ModContent.ItemType<TyphoonPrism>(),
			ModContent.ItemType<VerdantPrism>(),
			ModContent.ItemType<VoidsentPrism>(),
			ModContent.ItemType<PrismOfTheCosmicPhoenix>(),
			ModContent.ItemType<PrismOfTheRuinedKing>(),


		};

		public List<int> Outfits = new List<int>() {

			ModContent.ItemType<FaerieVoyagerAttire>(),
			ModContent.ItemType<StellarCasualAttire>(),
			ModContent.ItemType<AegisOfHopesLegacy>(),
			ModContent.ItemType<CelestialPrincessGenesis>(),
			ModContent.ItemType<FamiliarLookingAttire>(),
		}; 

		public List<int> GlowingItems = new List<int>() {

			ModContent.ItemType<TotemOfLightEmpowered>(),
			//ModContent.ItemType<VirtuesEdge>(),


		};
		public List<int> Essences = new List<int>() {

			ModContent.ItemType<EssenceOfLifethirsting>(),
			ModContent.ItemType<EssenceOfTheDarkMoon>(),
			ModContent.ItemType<EssenceOfAdagium>(),
			ModContent.ItemType<EssenceOfBloodshed>(),
			ModContent.ItemType<EssenceOfChemtech>(),
			ModContent.ItemType<EssenceOfEternity>(),
			ModContent.ItemType<EssenceOfFoxfire>(),
			ModContent.ItemType<EssenceOfInk>(),
			ModContent.ItemType<EssenceOfLuminance>(),
			ModContent.ItemType<EssenceOfMisery>(),
			ModContent.ItemType<EssenceOfOuterGods>(),
			ModContent.ItemType<EssenceOfRadiance>(),
			ModContent.ItemType<EssenceOfSakura>(),
			ModContent.ItemType<EssenceOfTechnology>(),
			ModContent.ItemType<EssenceOfTheAerialAce>(),
			ModContent.ItemType<EssenceOfTheAscendant>(),
			ModContent.ItemType<EssenceOfTheBeginningAndEnd>(),
			ModContent.ItemType<EssenceOfExplosions>(),
			ModContent.ItemType<EssenceOfTheBionis>(),
			ModContent.ItemType<EssenceOfTheChimera>(),
			ModContent.ItemType<EssenceOfTheCosmos>(),
			ModContent.ItemType<EssenceOfTheFuture>(),
			ModContent.ItemType<EssenceOfBlasting>(),
			ModContent.ItemType<EssenceOfTheGunlance>(),
			ModContent.ItemType<EssenceOfTheObservatory>(),
			ModContent.ItemType<EssenceOfTheOcean>(),
			ModContent.ItemType<EssenceOfTwinStars>(),
			ModContent.ItemType<EssenceOfVampirism>(),
			ModContent.ItemType<EssenceOfDestiny>(),
			ModContent.ItemType<EssenceOfAlpha>(),
			ModContent.ItemType<EssenceOfAsh>(),
			ModContent.ItemType<EssenceOfAzakana>(),
			ModContent.ItemType<EssenceOfDeathsApprentice>(),
			ModContent.ItemType<EssenceOfDrivingThunder>(),
			ModContent.ItemType<EssenceOfFingers>(),
			ModContent.ItemType<EssenceOfIRyS>(),
			ModContent.ItemType<EssenceOfLunarDominion>(),
			ModContent.ItemType<EssenceOfPerfection>(),
			ModContent.ItemType<EssenceOfStyle>(),
			ModContent.ItemType<EssenceOfSurpassingLimits>(),
			ModContent.ItemType<EssenceOfTheDragonslayer>(),
			ModContent.ItemType<EssenceOfTheFallen>(),
			ModContent.ItemType<EssenceOfTheGardener>(),
			ModContent.ItemType<EssenceOfTheHarbinger>(),
			ModContent.ItemType<EssenceOfTheHawkmoon>(),
			ModContent.ItemType<EssenceOfTheHollowheart>(),
			ModContent.ItemType<EssenceOfThePegasus>(),
			ModContent.ItemType<EssenceOfTheSharpshooter>(),
			ModContent.ItemType<EssenceOfTime>(),
			ModContent.ItemType<EssenceOfStaticShock>(),
			ModContent.ItemType<EssenceOfBitterfrost>(),
			ModContent.ItemType<EssenceOfButterflies>(),
			ModContent.ItemType<EssenceOfDuality>(),
			ModContent.ItemType<EssenceOfEuthymia>(),
			ModContent.ItemType<EssenceOfIzanagi>(),
			ModContent.ItemType<EssenceOfLiberation>(),
			ModContent.ItemType<EssenceOfSilverAsh>(),
			ModContent.ItemType<EssenceOfSin>(),
			ModContent.ItemType<EssenceOfStarsong>(),
			ModContent.ItemType<EssenceOfTheAegis>(),
			ModContent.ItemType<EssenceOfTheAnomaly>(),
			ModContent.ItemType<EssenceOfTheBull>(),
			ModContent.ItemType<EssenceOfTheFreeshooter>(),
			ModContent.ItemType<EssenceOfTheMoonlitAdepti>(),
			ModContent.ItemType<EssenceOfThePhantom>(),
			ModContent.ItemType<EssenceOfTheSwarm>(),
			ModContent.ItemType<EssenceOfTheTreasury>(),
			ModContent.ItemType<EssenceOfTheUnderworldGoddess>(),
			ModContent.ItemType<EssenceOfTheUnyieldingEarth>(),
			ModContent.ItemType<EssenceOfTheHunt>(),
			ModContent.ItemType<EssenceOfBlood>(),
			ModContent.ItemType<EssenceOfBalance>(),
			ModContent.ItemType<EssenceOfTheOverwhelmingBlaze>(),
		};
		public static bool disableAspectPenalty;
		public static bool disableCalamityWeaponBuffs;
		public static bool disableWeaponRestriction = false;
		
		public override void SetDefaults(Item item)
		{
			//if (item.type == ItemID.CopperShortsword)
			//{  Here we make sure to only change Copper Shortsword by checking item.type in an if statement
			//item.damage = 50;       // Changed original CopperShortsword's damage to 50!
			//}

			

			if(item.DamageType == ModContent.GetInstance<Systems.CelestialDamageClass>())
            {
				spatialWeapon = true;
            }

			oldDamageClass = item.DamageType;

			if (AstralWeapons.Contains(item.type))
            {

            }
			if (UmbralWeapons.Contains(item.type))
			{

			}
			if (SpatialWeapons.Contains(item.type))
			{

			}

			if (loadItem)
            {

				loadItem = false;
            }
			
		}
		
		public override bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
        {
			if(item.OriginalRarity == ModContent.GetInstance<StellarRarity>().Type)
            {
				if (!line.OneDropLogo)
				{
					// You are not allowed to change these, modders should use ModifyTooltips to modify them
					//line.text = "you shall not pass...";
					//line.oneDropLogo = false;
					//line.color = Color.AliceBlue;
					//line.overrideColor = Color.AliceBlue;
					//line.isModifier = false;
					//line.isModifierBad = false;
					//line.index = 1;

					// Let's draw the item name centered so it's in the middle, and let's add a form of separator
					string sepText = "✧ ✦ ✧"; // This is our separator, which will go between the item name and the rest
					float sepHeight = line.Font.MeasureString(sepText).Y; // Height of our separator

					// If our line text equals our item name, this is our tooltip line for the item name
					// if (line.text == item.HoverName)
					// What is more accurate to check is the layer name and mod
					if (line.Name == "ItemName" && line.Mod == "Terraria")
					// We check for Terraria so we modify the vanilla tooltip and not a modded one
					// This could be important, in case some mod does a lot of custom work and removes the standard tooltip
					// For tooltip layers, check the documentation for TooltipLine
					{
						// Our offset is half the width of our box, minus the padding of one side
						//float boxOffset = boxSize.X / 2 - paddingForBox;
						// The X coordinate where we draw is where the line would draw, plus the box offset,
						// which would place the START of the string at the center, so we subtract half of the line width to center it completely
						float drawX = line.X + (int)line.Font.MeasureString(line.Text).X / 2 - line.Font.MeasureString(sepText).X / 2;
						float drawY = line.Y + sepHeight / 2;

						// Note how our line object has many properties we can use for drawing
						// Here we draw the separator, note that it'd make more sense to use PostDraw for this, but either will work
						ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, sepText,
							new Vector2(drawX, drawY), line.Color, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);

						// Here we do the same thing as we did for drawX, which will center our ItemName tooltip
						//line.X += (int)boxOffset - (int)line.Font.MeasureString(line.Text).X / 2;
						// yOffset affects the offset that is added every next line, so this will cause the line to come after the separator to be drawn slightly lower
						yOffset = (int)sepHeight / 4;
					}
					else
					{
						// Reset the offset for other lines
						//float boxOffset = boxSize.X / 2 - paddingForBox;
						//line.X = 0;
						yOffset = 0;
						//line.X += (int)boxOffset - (int)line.Font.MeasureString(line.Text).X / 2;
					}
				}
			}
			
			return true;
        }
        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
			if(Prisms.Contains(item.type) || Outfits.Contains(item.type) || GlowingItems.Contains(item.type) || Essences.Contains(item.type))
            {
				Texture2D texture = TextureAssets.Item[item.type].Value;

				



				float time = Main.GlobalTimeWrappedHourly;
				float timer = item.timeSinceItemSpawned / 240f + time * 0.04f;

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

					Main.spriteBatch.Draw(texture, position + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), 0, origin, scale, SpriteEffects.None, 0);
				}

				for (float i = 0f; i < 1f; i += 0.34f)
				{
					float radians = (i + timer) * MathHelper.TwoPi;

					Main.spriteBatch.Draw(texture, position + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), 0, origin, scale, SpriteEffects.None, 0);

				}



				return true;
			}
			return true;
		}
        public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
			if (Prisms.Contains(item.type) || Outfits.Contains(item.type) || GlowingItems.Contains(item.type) || Essences.Contains(item.type))
			{
				// Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
				Texture2D texture = TextureAssets.Item[item.type].Value;

				Rectangle frame;

				if (Main.itemAnimations[item.type] != null)
				{
					// In case this item is animated, this picks the correct frame
					frame = Main.itemAnimations[item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
				}
				else
				{
					frame = texture.Frame();
				}

				Vector2 frameOrigin = frame.Size() / 2f;
				Vector2 offset = new Vector2(item.width / 2 - frameOrigin.X, item.height - frame.Height);
				Vector2 drawPos = item.position - Main.screenPosition + frameOrigin + offset;

				float time = Main.GlobalTimeWrappedHourly;
				float timer = item.timeSinceItemSpawned / 240f + time * 0.04f;

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

			return true;
		}

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (AstralWeapons.Contains(item.type))
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: AstralIdentifier", $"[i:{ItemType<Astral>()}]") { OverrideColor = Color.White };
				tooltips.Add(tooltip);
			}
			if (UmbralWeapons.Contains(item.type))
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: UmbralIdentifier", $"[i:{ItemType<Umbral>()}]") { OverrideColor = Color.White };
				tooltips.Add(tooltip);
			}
			if (SpatialWeapons.Contains(item.type))
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: SpatialIdentifier", $"[i:{ItemType<Spatial>()}]") { OverrideColor = Color.White };
				tooltips.Add(tooltip);
				//
			}

			if (item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") && item.damage > 0)
			{
				

			}
			if (item.prefix == ModContent.PrefixType<NovaPrefix1>())//Weakest one
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: NovaPrefix", LangHelper.GetTextValue("Prefix.NovaPrefix1.Tooltip")) { OverrideColor = Color.White };
				tooltips.Add(tooltip);
				
			}
			if (item.prefix == ModContent.PrefixType<NovaPrefix2>())//
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: NovaPrefix", LangHelper.GetTextValue("Prefix.NovaPrefix2.Tooltip")) { OverrideColor = Color.White };
				tooltips.Add(tooltip);

			}
			if (item.prefix == ModContent.PrefixType<NovaPrefix3>())//
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: NovaPrefix", LangHelper.GetTextValue("Prefix.NovaPrefix3.Tooltip")) { OverrideColor = Color.White };
				tooltips.Add(tooltip);

			}
			if (item.prefix == ModContent.PrefixType<NovaPrefix4>())//Strongest one
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: NovaPrefix", LangHelper.GetTextValue("Prefix.NovaPrefix4.Tooltip")) { OverrideColor = Color.White };
				tooltips.Add(tooltip);

			}
			if (item.prefix == ModContent.PrefixType<BadNovaPrefix1>())//Bad 1
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: NovaPrefix", LangHelper.GetTextValue("Prefix.BadNovaPrefix1.Tooltip")) { OverrideColor = Color.White };
				tooltips.Add(tooltip);

			}
			if (item.prefix == ModContent.PrefixType<BadNovaPrefix2>())//Bad 2
			{
				TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: NovaPrefix", LangHelper.GetTextValue("Prefix.BadNovaPrefix2.Tooltip")) { OverrideColor = Color.White };
				tooltips.Add(tooltip);

			}
			//if (tooltip.Name.Equals("Tooltip0"))

			

		}
		public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
			if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
			{
				if(!disableCalamityWeaponBuffs)
                {
					damage += 0.2f;
				}
				

			}
			if (item.type == ItemID.Zenith)//Zenith Balance Patch? Nope.
            {
				//damage -= 0.3f;
            }				
			if ((item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") || player.GetModPlayer<StarsAbovePlayer>().aprismatism == 2) && item.damage > 0)
			{ //
				if (player.GetModPlayer<StarsAbovePlayer>().RogueAspect == 2)
				{
					if (oldDamageClass != calamityMod.Find<DamageClass>("RogueDamageClass"))
					{
						if (ModLoader.TryGetMod("CalamityMod", out Mod calamityModx))
						{
							damage = player.GetTotalDamage(calamityMod.Find<DamageClass>("RogueDamageClass"));

							
						}
						
						if (!disableAspectPenalty && !spatialWeapon)
						{
							damage -= 0.1f;
						}

					}
					else
					{

					}

				}
				
				if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
				{
					if (oldDamageClass != DamageClass.Melee && oldDamageClass != DamageClass.MeleeNoSpeed)
					{
						damage = player.GetTotalDamage(DamageClass.Melee);
						
						if (!disableAspectPenalty && !spatialWeapon)
						{
							damage -= 0.1f;
						}

					}
					else
					{

					}

				}
				if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
				{
					if (oldDamageClass != DamageClass.Magic && oldDamageClass != DamageClass.MagicSummonHybrid)
					{
						damage = player.GetTotalDamage(DamageClass.Magic);
						if (!disableAspectPenalty && !spatialWeapon)
						{
							damage -= 0.1f;
						}
					}
					else
					{

					}


				}
				if (player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
				{
					if (oldDamageClass != DamageClass.Ranged)
					{
						player.GetTotalDamage(DamageClass.Ranged);
						if (!disableAspectPenalty && !spatialWeapon)
						{
							damage -= 0.1f;
						}
					}
					else
					{

					}
				}
				if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
				{
					if (oldDamageClass != DamageClass.Summon && oldDamageClass != DamageClass.MagicSummonHybrid && oldDamageClass != DamageClass.SummonMeleeSpeed)
					{
						damage = player.GetTotalDamage(DamageClass.Summon);
						
						if (!disableAspectPenalty && !spatialWeapon)
						{
							damage -= 0.1f;
						}
					}
					else
					{

					}
				}

			}
			if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
			{
				if (item.ModItem is ArchitectLuminance && !disableAspectPenalty) //Melee weapons
				{
					damage += 0.1f;
				}
				if (item.ModItem is SkyStrikerArms && !disableAspectPenalty)
				{
					damage += 0.1f;
				}
			}
			if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
			{
				if (item.ModItem is CosmicDestroyer && !disableAspectPenalty) //Ranged weapons
				{
					damage += 0.1f;
				}

			}
			if (player.GetModPlayer<StarsAbovePlayer>().PerfectlyGenericAccessory && item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") && item.damage > 0)
			{
				damage += 0.08f;
			}


		}
        public override void HoldItem(Item item, Player player)
        {
			if ((item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") || player.GetModPlayer<StarsAbovePlayer>().aprismatism == 2) && item.damage > 0 && item.DamageType != ModContent.GetInstance<Systems.CelestialDamageClass>())
			{ //

				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
				{
					item.DamageType = DamageClass.Melee;
					
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
				{
					
					item.DamageType = DamageClass.Magic;
					
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
				{
					
					item.DamageType = DamageClass.Ranged;
					
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
				{
					
					item.DamageType = DamageClass.Summon;
					
				}
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 2)
				{
					if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
					{
						
						item.DamageType = calamityMod.Find<DamageClass>("RogueDamageClass");

					}
					
				}

				


			}

			
			base.HoldItem(item, player);
        }
        public override void UpdateInventory(Item item, Player player)
        {
			if (item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") || player.GetModPlayer<StarsAbovePlayer>().aprismatism == 2)
			{
				
			}
			item.DamageType = oldDamageClass;
		}
		

        public override bool CanUseItem(Item item, Player player)
        {
			if(player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 1 && player.whoAmI == Main.myPlayer && item.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))
            {
				if(AstralWeapons.Contains(item.type) && !disableWeaponRestriction)
				{            
					if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The weapon fails to react to your Aspect, rendering it unusable."), 241, 255, 180);}

					return false;
                }
            }
			if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 2 && player.whoAmI == Main.myPlayer && item.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))
			{
				if (UmbralWeapons.Contains(item.type) && !disableWeaponRestriction)
				{
					if (Main.netMode != NetmodeID.Server) { Main.NewText(Language.GetTextValue("The weapon fails to react to your Aspect, rendering it unusable."), 241, 255, 180); }

					return false;
				}
			}
			if (item.type == ItemID.RodofDiscord
				||item.type == ItemID.DirtBomb
				||item.type == ItemID.RopeCoil
				|| item.type == ItemID.SilkRopeCoil
				|| item.type == ItemID.VineRopeCoil
				|| item.type == ItemID.WebRopeCoil
				|| item.type == ItemID.WetBomb
				|| item.type == ItemID.TeleportationPotion
				|| item.type == ItemID.Clentaminator
				|| item.type == ItemID.LavaBomb
				|| item.type == ItemID.HoneyBomb
				|| item.type == ItemID.DirtStickyBomb
				)
            {
				if(SubworldSystem.Current != null)
                {
					return false;
				}
				
            }
			if(player.HasBuff(BuffID.DrillMount) && item.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))
            {
				return false;
            }
            return base.CanUseItem(item, player);
        }
	
    }
}