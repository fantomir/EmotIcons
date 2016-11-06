using System;
using Android.App;
using Android.Widget;
using Android.OS;
using com.vasundharareddy.emojicon;
using Android.Support.V4.App;

namespace EmotIcons
{
	[Activity(Label = "Launcher", MainLauncher = true)]
	public class MainActivity : FragmentActivity
	{
		private EmojiconEditText _editEmojicon;
		private EmojiconTextView _textEmojicon;
		private TextView _textPlain;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			_editEmojicon = FindViewById<EmojiconEditText>(Resource.Id.editEmojicon);
			_textEmojicon = FindViewById<EmojiconTextView>(Resource.Id.txtEmojicon);
			_textPlain = FindViewById<TextView>(Resource.Id.txtPlain);

			EmojiconsFragment.EmojiClicked += e => EmojiconsFragment.Input(_editEmojicon, e);
			EmojiconsFragment.EmojiconBackspaceClicked += v => EmojiconsFragment.Backspace(_editEmojicon);

			_editEmojicon.TextChanged += (sender, e) =>
			{
				_textEmojicon.Text = e.Text.ToString();
				_textPlain.Text = e.Text.ToString();
			};
		}
	}
}