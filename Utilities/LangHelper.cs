using System;
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

    public static string Wrap(string v, int size)
    {
        v = v.TrimStart();
        if (v.Length <= size) return v;
        var nextspace = v.LastIndexOf(' ', size);
        if (-1 == nextspace) nextspace = Math.Min(v.Length, size);
        return v.Substring(0, nextspace) + ((nextspace >= v.Length) ?
        "" : "\n" + Wrap(v.Substring(nextspace), size));
    }
}