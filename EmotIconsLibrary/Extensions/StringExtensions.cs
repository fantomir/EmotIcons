using System;
using System.Collections.Generic;

namespace com.vasundharareddy.emojicon.Extensions
{
	internal static class StringExtensions
	{
		public static IEnumerable<int> IndexesOf(this string haystack, string needle)
		{
			var lastIndex = 0;
			while (true)
			{
				var index = haystack.IndexOf(needle, lastIndex, StringComparison.Ordinal);
				if (index == -1)
				{
					yield break;
				}
				yield return index;
				lastIndex = index + needle.Length;
			}
		}
	}
}
