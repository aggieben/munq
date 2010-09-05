using System;

namespace Munq.Search.Documents.FieldTypes
{
    public class DateValue : IFieldValue 
	{
		private DateTime _date;
		public DateValue(DateTime date)
		{
			_date = date.Date;
		}

		public byte TypeID
		{
			get { return (byte)'D'; }
		}

		public override string ToString()
		{
			return _date.ToString("yyyyMMdd");
		}
	}

}
