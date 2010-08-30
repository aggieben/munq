using System;

namespace Munq.Search.Documents
{
	[Flags]
	public enum IndexOptions
	{
		INDEXED = 1,
		TOKENIZED = 2,
		NORMS = 4,

		No = NORMS,
		Analyzed = INDEXED | TOKENIZED | NORMS,
		NotAnalyzed = INDEXED | NORMS,
		AnalyzedNoNorms = INDEXED | TOKENIZED,
		NotAnalysedNoNorms = INDEXED
	}
}
