using Android.Content;
using Android.Widget;
using Android.Util;
using Android.Content.Res;
using Android.Text;

namespace com.vasundharareddy.emojicon
{
	public class EmojiconTextView : TextView
	{
		public int EmojiconSize { get; set; }

		public EmojiconTextView(Context context) : base(context)
		{
			Init(null);
		}

		public EmojiconTextView(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			Init(attrs);
		}

		public EmojiconTextView(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle)
		{
			Init(attrs);
		}

		private void Init(IAttributeSet attrs)
		{
			if (attrs == null)
			{
				EmojiconSize = (int) TextSize;
			}
			else
			{
				TypedArray a = Context.ObtainStyledAttributes(attrs, Resource.Styleable.Emojicon);
				EmojiconSize = (int) a.GetDimension(Resource.Styleable.Emojicon_emojiconSize, TextSize);
				a.GetInteger(Resource.Styleable.Emojicon_emojiconTextStart, 0);
				a.GetInteger(Resource.Styleable.Emojicon_emojiconTextLength, -1);
				a.Recycle();
			}
			Text = base.Text;
		}

		public new string Text
		{
			set
			{
				var builder = new SpannableStringBuilder(value);
				EmojiconHandler.AddEmojis(Context, builder, EmojiconSize);
				TextFormatted = builder;
			}
			get { return base.Text; }
		}
	}
}