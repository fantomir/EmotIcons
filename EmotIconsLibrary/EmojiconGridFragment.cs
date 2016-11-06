using Android.Support.V4.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using com.vasundharareddy.emojicon.EmojiCategories;

namespace com.vasundharareddy.emojicon
{
	public delegate void OnEmojiClicked(Emojicon e);

	public class EmojiconGridFragment : Fragment
	{
		public IEmojiconRecents Recents { get; set; }
		private Emojicon[] _data;
		private GridView _gridView;

		public event OnEmojiClicked EmojiClicked;

		public static EmojiconGridFragment NewInstance(Emojicon[] emojicons, IEmojiconRecents recents)
		{
			var emojiGridFragment = new EmojiconGridFragment
			{
				_data = emojicons,
				Recents = recents
			};

			return emojiGridFragment;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.emojicon_grid, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			_data = _data ?? People.Data;

			_gridView = (GridView)view.FindViewById(Resource.Id.Emoji_GridView);
			_gridView.Adapter = new EmojiAdapter(view.Context, _data);
			_gridView.ItemClick += OnItemClick;
		}

		public void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			EmojiClicked?.Invoke((Emojicon)e.Parent.GetItemAtPosition(e.Position));
		}
	}
}