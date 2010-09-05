using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.Search.Index
{
	public class TermIndex
	{
		private IDictionary<string, TermIndexEntry> _terms;
		public TermIndex()
		{
			_terms = new SortedDictionary<string, TermIndexEntry>(StringComparer.CurrentCultureIgnoreCase);
		}

		public void AddDocTermCount(string term, uint docID, uint count)
		{
			TermIndexEntry termIndexEntry;
			if (_terms.ContainsKey(term))
			{
				termIndexEntry = _terms[term];
			}
			else
			{
				termIndexEntry = new TermIndexEntry();
				_terms[term] = termIndexEntry;
			}

			termIndexEntry[docID] = count;
		}

		public TermIndexEntry GetDocsWithTerm(string term)
		{
			if (_terms.ContainsKey(term))
				return _terms[term];
			else
				return new TermIndexEntry();
		}
	}
}
