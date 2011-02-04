using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.Search.Index
{
	public class FieldIndex
	{
		private IDictionary<string, TermIndex> _fields;
		public FieldIndex()
		{
			_fields = new SortedDictionary<string, TermIndex>(StringComparer.CurrentCultureIgnoreCase).;
		}

		public void AddDocFieldTermCount(string fieldName, string term, uint docID, uint count)
		{
			TermIndex termIndex;
			if (_fields.ContainsKey(fieldName))
			{
				termIndex = _fields[fieldName];
			}
			else
			{
				termIndex = new TermIndex();
				_fields[fieldName] = termIndex;
			}

			termIndex.AddDocTermCount(term, docID, count);
		}

		public TermIndexEntry GetDocsWithTermInField(string term, string fieldName)
		{
			if (_fields.ContainsKey(fieldName))
				return _fields[fieldName].GetDocsWithTerm(term);
			else
				return new TermIndexEntry();
		}
	}
}
