using System;

namespace Munq.Search.Documents
{
	[Serializable]
	public class FieldOptions
	{

		private StoreOptions		_store;
		private IndexOptions		_index;
		private TermVectorOptions	_termVector;

		public FieldOptions(StoreOptions store, IndexOptions index, TermVectorOptions termVector)
		{
			_store      = store;
			_index      = index;
			_termVector = termVector;
		}

		public bool IsStored		{ get { return (_store & StoreOptions.STORED) != 0; } }
		public bool IsCompressed	{ get { return (_store & StoreOptions.COMPRESSED) != 0; } }

		public bool IsIndexed		{ get { return (_index & IndexOptions.INDEXED) != 0; } }
		public bool IsTokenized		{ get { return (_index & IndexOptions.TOKENIZED) != 0; } }
		public bool HasNorms		{ get { return (_index & IndexOptions.NORMS) != 0; } }

		public bool HasTermVector	{ get { return (_termVector & TermVectorOptions.STORED) != 0; } }
		public bool HasPositions	{ get { return (_termVector & TermVectorOptions.POSITIONS) != 0; } }
		public bool HasOffsets		{ get { return (_termVector & TermVectorOptions.OFFSETS) != 0; } }
	}
}
