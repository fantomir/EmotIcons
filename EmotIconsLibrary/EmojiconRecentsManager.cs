using System;
using Android.Content;
using System.Collections.Generic;
using System.Linq;

namespace com.vasundharareddy.emojicon
{
	public static class EmojiconRecentsManager
	{
		private const string PREFERENCE_NAME = "emojicon";
		private const string PREF_RECENTS = "recent_emojis";
		private const string PREF_PAGE = "recent_page";
		private const int MAX_SAVE = 40;

		private static Context _context;
		public static int Count => Recents.Count;
		public static List<Emojicon> Recents { get; private set; }

		public static Context Context
		{
			set
			{
				_context = value.ApplicationContext;
				LoadRecents();
			}
		}

		private static ISharedPreferences Preferences
			=> _context.GetSharedPreferences(PREFERENCE_NAME, FileCreationMode.Private);

		public static int RecentPage
		{
			get { return Preferences.GetInt(PREF_PAGE, 0); }
			set { Preferences.Edit().PutInt(PREF_PAGE, value).Commit(); }
		}

		public static void Push(Emojicon emojicon)
		{
			if (Recents.Contains(emojicon))
				Recents.Remove(emojicon);

			Recents.Insert(0, emojicon);
			SaveRecents();
		}

		public static void Add(Emojicon emojicon)
		{
			Recents.Add(emojicon);
			SaveRecents();
		}

		public static void Add(int index, Emojicon emojicon)
		{
			Recents.Insert(index, emojicon);
		}

		public static void Remove(Emojicon emojicon)
		{
			Recents.Remove(emojicon);
			SaveRecents();
		}

		private static void LoadRecents()
		{
			var loadedPreferences = Preferences.GetString(PREF_RECENTS, string.Empty);
#if DEBUG
			Console.WriteLine("Loaded emojicon preferences: " + loadedPreferences);
#endif
			var emojicons = loadedPreferences.Split('#');
			Recents = new List<Emojicon>();
			foreach (var emojicon in emojicons.Where(x => x != string.Empty))
			{
				try
				{
					Add(new Emojicon(emojicon));
				}
				catch
				{
					// ignored
				}
			}
		}

		private static void SaveRecents()
		{
			if (Recents.Count == MAX_SAVE)
				Recents.RemoveRange(0, 1);

			var saveString = string.Empty;

			foreach (var emoji in Recents.Where(x => x.Value != string.Empty))
			{
				saveString += emoji.Value.Replace(":", string.Empty) + "#";
			}

			Preferences.Edit().PutString(PREF_RECENTS, saveString).Commit();
		}
	}
}