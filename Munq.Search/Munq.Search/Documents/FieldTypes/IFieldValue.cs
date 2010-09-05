using System;

namespace Munq.Search.Documents.FieldTypes
{
	public interface IFieldValue
	{
		byte TypeID { get; }
		string ToString();
	}
}
