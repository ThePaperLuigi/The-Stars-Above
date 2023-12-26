using StarsAbove.Utilities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    // This class handles everything for our custom damage class
    // Any class that we wish to be using our custom damage class will derive from this class, instead of ModItem
    public class GadgetDamageClass : DamageClass
    {
        public override void SetStaticDefaults()
        {
        }

        //public string "Spatial Damage" => DisplayName;

        public new string DisplayName => "Gadget Strength";

        // This is an example damage class designed to demonstrate all the current functionality of the feature and explain how to create one of your own, should you need one.
        // For information about how to apply stat bonuses to specific damage classes, please instead refer to ExampleMod/Content/Items/Accessories/ExampleStatBonusAccessory.
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            // This method lets you make your damage class benefit from other classes' stat bonuses by default, as well as universal stat bonuses.
            // To briefly summarize the two nonstandard damage class names used by DamageClass:
            // Default is, you guessed it, the default damage class. It doesn't scale off of any class-specific stat bonuses or universal stat bonuses.
            // There are a number of items and projectiles that use this, such as thrown waters and the Bone Glove's bones.
            // Generic, on the other hand, scales off of all universal stat bonuses and nothing else; it's the base damage class upon which all others that aren't Default are built.
            if (damageClass == DamageClass.Generic)
                return StatInheritanceData.None;

            return new StatInheritanceData(
                damageInheritance: 0f,
                critChanceInheritance: 0f,
                attackSpeedInheritance: 0f,
                armorPenInheritance: 0f,
                knockbackInheritance: 0f
            );

        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {

            return false;
        }

        public override void SetDefaultStats(Player player)
        {
            // This method lets you set default statistical modifiers for your example damage class.
            // Here, we'll make our example damage class have more critical strike chance and armor penetration than normal.
            //player.GetCritChance<CelestialDamageClass>() += 4;
            //player.GetArmorPenetration<CelestialDamageClass>() += 10;
            // These sorts of modifiers also exist for damage (GetDamage), knockback (GetKnockback), and attack speed (GetAttackSpeed).
            // You'll see these used all around in referencce to vanilla classes and our example class here. Familiarize yourself with them.
        }

        // This property lets you decide whether or not your damage class can use standard critical strike calculations.
        // Note that setting it to false will also prevent the critical strike chance tooltip line from being shown.
        // This prevention will overrule anything set by ShowStatTooltipLine, so be careful!
        public override bool UseStandardCritCalcs => false;

        public override bool ShowStatTooltipLine(Player player, string lineName)
        {
            // This method lets you prevent certain common statistical tooltip lines from appearing on items associated with this DamageClass.
            // The four line names you can use are "Damage", "CritChance", "Speed", and "Knockback". All four cases default to true, and thus will be shown. For example...
            //if (lineName == "Speed")
            //	return false;
            if (lineName == "CritChance")
            {
                return false;
            }
            if (lineName == "Speed")
            {
                return false;
            }
            if (lineName == "Knockback")
            {
                return false;
            }
            return true;
            // PLEASE BE AWARE that this hook will NOT be here forever; only until an upcoming revamp to tooltips as a whole comes around.
            // Once this happens, a better, more versatile explanation of how to pull this off will be showcased, and this hook will be removed.
        }

    }
}
