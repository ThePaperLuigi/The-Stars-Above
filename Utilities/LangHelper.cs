using Terraria.Localization;
using Terraria.ModLoader;

namespace StarsAbove.Utilities;

internal static class LangHelper
{
    internal static string GetTextValue(string key, params object[] args)
    {
        return GetModTextValue(StarsAbove.Instance, key, args);
    }
    
    private static string GetModTextValue(Mod mod, string key, params object[] args)
    {
        return Language.GetTextValue($"Mods.{mod.Name}.{key}", args);
    }
}