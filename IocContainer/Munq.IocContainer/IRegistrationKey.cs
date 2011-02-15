using System;

namespace Munq
{
	public interface IRegistrationKey
	{
	    Type GetInstanceType();
		bool Equals(object obj);
		int  GetHashCode();
	}
}
