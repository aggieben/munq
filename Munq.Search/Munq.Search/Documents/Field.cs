using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Munq.Search.Documents
{
	[Serializable]
	public class Field : IField
	{
		private FieldOptions _fieldInfo;
		private IFieldSource _source;

		public Field(String name, FieldOptions fieldInfo, IFieldSource source)
		{
			Name = name;
			_fieldInfo = fieldInfo;
			_source = source;
		}

		public string Name				{ get; private set; }
		public FieldOptions FieldInfo	{ get; private set; }
		public IFieldSource Source		{ get; private set; }
		public float Boost				{ get; set; }
	}
}
