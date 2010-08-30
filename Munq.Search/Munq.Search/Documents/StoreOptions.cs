using System;

namespace Munq.Search.Documents
{
	[Flags]
	public enum StoreOptions
	{
		STORED = 1,
		COMPRESSED = 2,

		No = 0,
		Yes = STORED,
		Compress = STORED | COMPRESSED
	}
}
