using System;
using Android.Content;
using Android.Text.Style;
using Android.Graphics.Drawables;

namespace com.vasundharareddy.emojicon
{
	public class EmojiconSpan : DynamicDrawableSpan
	{
		private readonly Context _context;
		private readonly int _resourceId;
		private readonly int _size;
		private Drawable _drawable;

		public EmojiconSpan(Context context, int resourceId, int size)
		{
			_context = context;
			_resourceId = resourceId;
			_size = size;
		}

		public override Drawable Drawable
		{
			get
			{
				if (_drawable != null)
					return _drawable;

				try
				{
					_drawable = _context.Resources.GetDrawable(_resourceId);
					_drawable.SetBounds(0, 0, _size, _size);
				}
				catch (Exception)
				{
					// ignored
				}

				return _drawable;
			}
		}
	}
}