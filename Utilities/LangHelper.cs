using System;
using System.Drawing;
using System.Text;
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


	/// <summary>
	/// Auto add newline according to <paramref name="limit"/>
	/// </summary>
	/// <param name="text">Input Text</param>
	/// <param name="limit">Character count limit</param>
	/// <returns></returns>
	internal static string Wrap(ReadOnlySpan<char> text, int limit)
    {
		const int MaxNewLine = 8;
		//Just try 2.2f and found it fits
		limit = (GameCulture.CultureName)Language.ActiveCulture.LegacyId switch
		{
			GameCulture.CultureName.Chinese => ((int)(limit / 2.2f)),
			_ => limit,
		};
		text = text.TrimStart();
		int index = 0, start = 0;
		StringBuilder stringBuilder = new StringBuilder(text.Length + MaxNewLine);
		while(index < text.Length)
		{
			//Maybe English dont lack of ' ' and Chinese dont contains ' '
			index = Math.Min(limit + start, text.Slice(start, limit).LastIndexOf(' ') & 0x7FFFFFFF);	//I'm lazy
			//AppendLine dont support ReadOnlySpan???
			stringBuilder.Append(text.Slice(start, index)).Append('\n');
			start = index + 1;
		}
		//Bad memory copy
		return stringBuilder.ToString();
	}
}
