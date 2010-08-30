using System;
using System.Collections.Generic;

namespace Munq.Search.Documents
{
	public interface IFieldSource
	{
		IEnumerable<Token> GetTokenIterator();
	}
}
