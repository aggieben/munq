using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.Search.Documents
{
	// The document class contains the indexed and stored information for arbitrary fields.
	// The document is added to an Index so that it can be included in searches.
	public interface IField
	{
		string Name			{ get; }
		FieldOptions FieldInfo { get; }
		float Boost			{ get; set;}
	}
}
