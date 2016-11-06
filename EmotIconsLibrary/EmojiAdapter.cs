using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace com.vasundharareddy.emojicon
{
	public class EmojiAdapter : ArrayAdapter<Emojicon>
	{
		public EmojiAdapter(Context context, List<Emojicon> data) : base(context, Resource.Layout.emojicon_item, data)
		{

		}

		public EmojiAdapter(Context context, Emojicon[] data) : base(context, Resource.Layout.emojicon_item, data)
		{

		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var hasConvertView = convertView != null;

			var resultView = hasConvertView
				? convertView
				: View.Inflate(Context, Resource.Layout.emojicon_item, null);

			if (!hasConvertView)
			{
				resultView.Tag = new ViewHolder
				{
					Icon = resultView.FindViewById<EmojiconTextView>(Resource.Id.emojicon_icon)
				};
			}

			var emoji = GetItem(position);

			var holder = (ViewHolder) resultView.Tag;
			holder.Icon.Text = emoji.Value;

			return resultView;
		}

		private class ViewHolder : Java.Lang.Object
		{
			public EmojiconTextView Icon;
		}
	}
}