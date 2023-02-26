using System;
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

	//public static string Wrap(string v, int size)
	//{
	//    v = v.TrimStart();
	//    if (v.Length <= size) return v;
	//    var nextspace = v.LastIndexOf(' ', size);
	//    if (-1 == nextspace) nextspace = Math.Min(v.Length, size);
	//    return v.Substring(0, nextspace) + ((nextspace >= v.Length) ?
	//    "" : "\n" + Wrap(v.Substring(nextspace), size));
	//}

	/// <summary>
	/// Auto add newline according to <paramref name="limit" />
	/// </summary>
	/// <param name="text"> Input Text </param>
	/// <param name="limit"> Character count limit </param>
	/// <returns> </returns>
	internal static string Wrap(ReadOnlySpan<char> text, int limit)
	{
		const int MaxNewLine = 8;

		// Just try 2.2f and found it fits
		limit = (GameCulture.CultureName)Language.ActiveCulture.LegacyId switch
		{
			GameCulture.CultureName.Chinese => (int)(limit / 2.2f),
			_ => limit,
		};
		text = text.TrimStart();
		int start = 0;
		StringBuilder stringBuilder = new StringBuilder(text.Length + MaxNewLine);
		while (limit + start <= text.Length)
		{
			// Maybe English dont lack of ' ' and Chinese dont contains ' '
			int index = Math.Min(limit + start, text.Slice(start, limit).LastIndexOf(' ') & 0x7FFFFFFF);

			// AppendLine dont support ReadOnlySpan???
			stringBuilder.Append(text[start..index]).AppendLine();
			start = index + 1;
		}
		stringBuilder.Append(text[start..]);

		//Bad memory copy
		return stringBuilder.ToString();
	}
}