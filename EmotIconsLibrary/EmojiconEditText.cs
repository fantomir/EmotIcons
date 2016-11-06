using Android.Content;
using Android.Widget;
using Android.Util;
using Android.Content.Res;
using Android.Text;

namespace com.vasundharareddy.emojicon
{
	public class EmojiconEditText : EditText
	{
		public int EmojiconSize { get; set; }

		public EmojiconEditText(Context context) : base(context)
		{
			//ReSharper disable once DoNotCallOverridableMethodsInConstructor
			EmojiconSize = (int) TextSize;
			TextChanged += OnTextChanged;
		}

		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			EmojiconHandler.AddEmojis(Context, new SpannableStringBuilder(TextFormatted), EmojiconSize);
		}

		public EmojiconEditText(Context context, IAttributeSet attrs) : base(context, attrs)
		{
			Init(attrs);
		}

		public EmojiconEditText(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle)
		{
			Init(attrs);
		}

		private void Init(IAttributeSet attrs)
		{
			TextChanged += OnTextChanged;

			if (attrs == null)
			{
				EmojiconSize = (int) TextSize;
			}
			else
			{
				var typedArray = Context.ObtainStyledAttributes(attrs, Resource.Styleable.Emojicon);
				EmojiconSize = (int)typedArray.GetDimension(Resource.Styleable.Emojicon_emojiconSize, TextSize);
				typedArray.Recycle();
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

		protected override void Dispose(bool disposing)
		{

			TextChanged -= OnTextChanged;
			base.Dispose(disposing);
		}
	}
}