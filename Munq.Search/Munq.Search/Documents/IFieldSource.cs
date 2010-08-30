using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Munq.Search.Documents
{
	public interface IFieldSource
	{
		public IEnumerable<string> GetTokens();
	}
}
