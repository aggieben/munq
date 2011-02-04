using System;

namespace Munq.Search.Documents
{
	// The document class contains the indexed and stored information for arbitrary fields.
	// The document is added to an Index so that it can be included in searches.
	public interface IField : IBoostable
	{
		string Name					{ get; }
		FieldOptions FieldOptions	{ get; }
	}
}
