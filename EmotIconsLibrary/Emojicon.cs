namespace com.vasundharareddy.emojicon
{
	public class Emojicon : Java.Lang.Object
	{
		public string Value { get; }

		public Emojicon(string value)
		{
			const string SEPARATOR = ":";

			var valueWithPrefix = value.StartsWith(SEPARATOR)
				? value
				: SEPARATOR + value;

			var valueWithPrefixAndPostfix = valueWithPrefix.EndsWith(SEPARATOR)
				? valueWithPrefix
				: valueWithPrefix + SEPARATOR;

			Value = valueWithPrefixAndPostfix;
		}

		public override bool Equals(object obj)
			=> obj != null && ((obj.GetType() == typeof (Emojicon)) && (Value.Equals(((Emojicon) obj).Value)));

		public override int GetHashCode()
			=> Value.GetHashCode();

		public override string ToString()
			=> Value;
	}
}