using Android.Widget;

namespace com.vasundharareddy.emojicon
{
	public class EmojiconRecentsGridFragment : EmojiconGridFragment, IEmojiconRecents
	{
		private EmojiAdapter _adapter;

		public static EmojiconRecentsGridFragment NewInstance()
		{
			return new EmojiconRecentsGridFragment();
		}

		public override void OnViewCreated(Android.Views.View view, Android.OS.Bundle savedInstanceState)
		{
			EmojiconRecentsManager.Context = view.Context;

			_adapter = new EmojiAdapter(view.Context, EmojiconRecentsManager.Recents);

			var gridView = (GridView) view.FindViewById(Resource.Id.Emoji_GridView);
			gridView.Adapter = _adapter;
			gridView.ItemClick += OnItemClick;
		}

		public override void OnDestroyView()
		{
			base.OnDestroyView();
			_adapter = null;
		}

		public void OnAddRecentEmoji(Emojicon emojicon)
		{
			EmojiconRecentsManager.Push(emojicon);
			_adapter?.NotifyDataSetChanged();
		}
	}
}