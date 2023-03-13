using System;
<<<<<<< HEAD
using System.Drawing;
using System.Text;
=======
>>>>>>> parent of beab749 (wrap method fix)
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

<<<<<<< HEAD

	/// <summary>
	/// Auto add newline according to <paramref name="limit"/>
	/// </summary>
	/// <param name="text">Input Text</param>
	/// <param name="limit">Character count limit</param>
	/// <returns></returns>
	internal static string Wrap(ReadOnlySpan<char> text, int limit)
    {
		const int MaxNewLine = 8;
<<<<<<< HEAD
		//Just try 2.2f and found it fits
		limit = (GameCulture.CultureName)Language.ActiveCulture.LegacyId switch
		{
			GameCulture.CultureName.Chinese => ((int)(limit / 2.2f)),
			_ => limit,
		};
		text = text.TrimStart();
		int index = 0, start = 0;
=======

		// Just try 2.2f and found it fits
		limit = (GameCulture.CultureName)Language.ActiveCulture.LegacyId switch
		{
			GameCulture.CultureName.Chinese => (int)(limit / 2.2f),
			_ => limit,
		};
		text = text.TrimStart();
		int start = 0;
>>>>>>> parent of 9d7020d (fix)
		StringBuilder stringBuilder = new StringBuilder(text.Length + MaxNewLine);
<<<<<<< HEAD
		while(index < text.Length)
=======
		while (limit + start <= text.Length)
>>>>>>> parent of 15de04d (small wrap fix)
		{
<<<<<<< HEAD
			//Maybe English dont lack of ' ' and Chinese dont contains ' '
			index = Math.Min(limit + start, text.Slice(start, limit).LastIndexOf(' ') & 0x7FFFFFFF);	//I'm lazy
			//AppendLine dont support ReadOnlySpan???
			stringBuilder.Append(text.Slice(start, index)).Append('\n');
=======
			// Maybe English dont lack of ' ' and Chinese dont contains ' '
			int index = Math.Min(limit + start, text.Slice(start, limit).LastIndexOf(' ') & 0x7FFFFFFF);

			// AppendLine dont support ReadOnlySpan???
			stringBuilder.Append(text[start..index]).AppendLine();
>>>>>>> parent of 9d7020d (fix)
			start = index + 1;
		}
		//Bad memory copy
		return stringBuilder.ToString();
	}
=======
    public static string Wrap(string v, int size)
    {
        v = v.TrimStart();
        if (v.Length <= size) return v;
        var nextspace = v.LastIndexOf(' ', size);
        if (-1 == nextspace) nextspace = Math.Min(v.Length, size);
        return v.Substring(0, nextspace) + ((nextspace >= v.Length) ?
        "" : "\n" + Wrap(v.Substring(nextspace), size));
    }
>>>>>>> parent of beab749 (wrap method fix)
}