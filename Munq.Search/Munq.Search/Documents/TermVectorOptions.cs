using System;

namespace Munq.Search.Documents
{
	[Flags]
	public enum TermVectorOptions
	{
		STORED = 1,
		POSITIONS = 2,
		OFFSETS = 4,

		No = 0,
		Yes = STORED,
		WithPositions = STORED | POSITIONS,
		WithOffsets = STORED | OFFSETS,
		WithPositionsAndOffsets = STORED | POSITIONS | OFFSETS
	}
}
