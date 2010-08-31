using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.Search.Documents.FieldSources
{
    public class StringSource: IFieldSource
	{
		List<Token> _tokens = new List<Token>();

		public StringSource(string value)
		{
			_tokens.Add(new Token { Offset = 0, Value = value });
		}

		public StringSource(IEnumerable<string> values)
		{
			if(values != null)
			{
				int i = 0;
				foreach (var value in values)
					_tokens.Add(new Token { Offset = i++, Value = value });
			}
		}

		public IEnumerable<Token> Tokens
		{
			get { return _tokens; }
		}
	}
}
