using System;
namespace Munq.Search.Documents
{
	public interface IBoostable
	{
		float Boost { get; set; }
	}
}
