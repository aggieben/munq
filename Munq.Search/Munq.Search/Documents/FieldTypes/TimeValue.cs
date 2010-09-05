using System;

namespace Munq.Search.Documents.FieldTypes
{
	public class TimeValue : IFieldValue
	{
		private TimeSpan _time;
		public TimeValue(DateTime time) : this(time.TimeOfDay)
		{
		}

		public TimeValue(TimeSpan time)
		{
			_time = time;
		}

		public override string ToString()
		{
			return _time.ToString("hhmmss");
		}

		public byte TypeID
		{
			get { return (byte)'T'; }
		}
	}
}
