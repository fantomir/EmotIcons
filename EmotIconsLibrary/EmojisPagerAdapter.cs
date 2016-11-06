using Android.Support.V4.App;
using System.Collections.Generic;

namespace com.vasundharareddy.emojicon
{
	public class EmojisPagerAdapter : FragmentStatePagerAdapter
	{
		private readonly List<EmojiconGridFragment> _fragments;

		public EmojisPagerAdapter(FragmentManager fm, List<EmojiconGridFragment> fragments) : base(fm)
		{
			_fragments = fragments;
		}

		public override Fragment GetItem(int position) => _fragments[position];

		public override int Count => _fragments.Count;
	}
}