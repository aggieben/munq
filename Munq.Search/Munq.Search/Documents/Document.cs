using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.Search.Documents
{
	public class Document 
	{
		private List<IField> fields = new List<IField>();

		public Document()
		{
			Boost = 1.0f;
		}

		public float Boost { get; set; }

		public void Add(IField field)
		{
			fields.Add(field);
		}

		public int Count { get { return fields.Count; } }

		public IEnumerable<IField> Fields { get { return fields; } }

		public IField FindByName(string name)
		{
			return fields.FirstOrDefault(f => f.Name == name);
		}

		public IEnumerable<IField> FindAllByName(string name)
		{
			return fields.FindAll(f => f.Name == name);
		}

	}
}
