using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.Search.Documents.FieldSources
{
	public class FieldSource : IFieldSource
	{
		protected IEnumerable<Token> _tokens;

		public IEnumerable<Token> Tokens
		{
			get { return _tokens; }
		}
	}
	
	public class FieldSource<T> : FieldSource
	{

		public FieldSource(T tokenValue)
		{
			_tokens = new List<Token>(new Token[] 
			{
				new Token { 
					Offset = 0, 
					Value = tokenValue.ToString()
			}});
		}

		public FieldSource(IEnumerable<T> tokenValues)
		{
			if (tokenValues != null)
			{
				_tokens = tokenValues.Select(tv => new Token
				{
					Offset = 0,
					Value = tv.ToString()
				});
			}
			else
				_tokens = new List<Token>();
		}
	}

}
