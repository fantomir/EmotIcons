using System;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using com.vasundharareddy.emojicon.EmojiCategories;

namespace com.vasundharareddy.emojicon
{
	public delegate void OnEmojiconBackspaceClicked(View v);

	public delegate void OnAddRecentEmoji(Emojicon emojicon);

	public class EmojiconsFragment : Fragment, IEmojiconRecents
	{
		public static event OnEmojiconBackspaceClicked EmojiconBackspaceClicked;
		public static event OnEmojiClicked EmojiClicked;
		public static event OnAddRecentEmoji AddRecentEmoji;

		private int _emojiTabLastSelectedIndex = -1;
		private View[] _emojiTabs;
		private PagerAdapter _emojisAdapter;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Android.OS.Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.emojicons, container, false);
			var emojisPager = view.FindViewById<ViewPager>(Resource.Id.emojis_pager);

			emojisPager.PageSelected += OnPageSelected;
			AddRecentEmoji += OnAddRecentEmoji;

			var emojiFragments = new List<EmojiconGridFragment>
			{
				EmojiconRecentsGridFragment.NewInstance(),
				EmojiconGridFragment.NewInstance(People.Data, this),
				EmojiconGridFragment.NewInstance(Nature.Data, this),
				EmojiconGridFragment.NewInstance(Objects.Data, this),
				EmojiconGridFragment.NewInstance(Places.Data, this),
				EmojiconGridFragment.NewInstance(Symbols.Data, this)
			};

			foreach (var fragment in emojiFragments)
			{
				fragment.EmojiClicked += e => EmojiClicked?.Invoke(e);
			}

			_emojisAdapter = new EmojisPagerAdapter(FragmentManager, emojiFragments);
			emojisPager.Adapter = _emojisAdapter;
			//ToDo: Solve Recents Issue

			const int EMOJI_TABS_COUNT = 6;
			_emojiTabs = new View[EMOJI_TABS_COUNT];
			_emojiTabs[0] = view.FindViewById(Resource.Id.emojis_tab_0_recents);
			_emojiTabs[1] = view.FindViewById(Resource.Id.emojis_tab_1_people);
			_emojiTabs[2] = view.FindViewById(Resource.Id.emojis_tab_2_nature);
			_emojiTabs[3] = view.FindViewById(Resource.Id.emojis_tab_3_objects);
			_emojiTabs[4] = view.FindViewById(Resource.Id.emojis_tab_4_cars);
			_emojiTabs[5] = view.FindViewById(Resource.Id.emojis_tab_5_punctuation);

			for (var i = 0; i < _emojiTabs.Length; i++)
			{
				var position = i;
				_emojiTabs[i].Click += (sender, e) => emojisPager.CurrentItem = position;
			}

			view.FindViewById(Resource.Id.emojis_backspace).Click += (sender, e) =>
			{
				EmojiconBackspaceClicked?.Invoke((View) sender);
			};

			// get last selected page
			EmojiconRecentsManager.Context = view.Context;
			var page = EmojiconRecentsManager.RecentPage;
			// last page was recents, check if there are recents to use
			// if none was found, go to page 1
			if (page == 0 && EmojiconRecentsManager.Count == 0)
			{
				page = 1;
			}
			if (page == 0)
			{
				OnPageSelected(null, new ViewPager.PageSelectedEventArgs(page));
			}
			else
			{
				emojisPager.SetCurrentItem(page, false);
			}
			return view;
		}

		private void OnPageSelected(object sender, ViewPager.PageSelectedEventArgs e)
		{
			if (_emojiTabLastSelectedIndex == e.Position)
			{
				return;
			}
			switch (e.Position)
			{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
					if (_emojiTabLastSelectedIndex >= 0 && _emojiTabLastSelectedIndex < _emojiTabs.Length)
					{
						_emojiTabs[_emojiTabLastSelectedIndex].Selected = false;
					}
					_emojiTabs[e.Position].Selected = true;
					_emojiTabLastSelectedIndex = e.Position;
					EmojiconRecentsManager.RecentPage = e.Position;
					break;
			}
		}

		public override void OnDetach()
		{
			EmojiconBackspaceClicked = null;
			EmojiClicked = null;
			base.OnDetach();
		}

		public static void Input(EmojiconEditText editText, Emojicon emojicon)
		{
			AddRecentEmoji?.Invoke(emojicon);
			if (editText == null || emojicon == null)
			{
				return;
			}
			var start = Math.Max(editText.SelectionStart, 0);
			var end = Math.Max(editText.SelectionEnd, 0);
			if (start < 0)
			{
				editText.Text += emojicon.Value;
				editText.SetSelection(editText.Text.Length);
			}
			else
			{
				editText.Text = editText.Text.Substring(0, Math.Min(start, end)) + emojicon.Value +
				                editText.Text.Substring(Math.Max(start, end));
				editText.SetSelection(start + emojicon.Value.Length);
			}
		}

		public void OnAddRecentEmoji(Emojicon emojicon)
		{
			var emojisPager = View.FindViewById<ViewPager>(Resource.Id.emojis_pager);
			var fragment = (EmojiconRecentsGridFragment) _emojisAdapter.InstantiateItem(emojisPager, 0);
			fragment.OnAddRecentEmoji(emojicon);
		}

		public static void Backspace(EditText editText)
		{
			var keyEvent = new KeyEvent(0, 0, 0, Keycode.Del, 0, 0, 0, 0, KeyEventFlags.Canceled);
			editText.DispatchKeyEvent(keyEvent);
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			AddRecentEmoji -= OnAddRecentEmoji;
		}
	}
}