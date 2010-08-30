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
		public Field(String name, FieldOptions fieldInfo, IFieldSource source)
		{
			Name = name;
			FieldInfo = fieldInfo;
			Source = source;
			Boost = 1.0f;
		}

		public string Name				{ get; private set; }
		public FieldOptions FieldInfo	{ get; private set; }
		public IFieldSource Source		{ get; private set; }
		public float Boost				{ get; set; }
	}
}
