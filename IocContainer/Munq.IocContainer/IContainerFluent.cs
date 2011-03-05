using System;

namespace Munq
{
	public interface IContainerFluent
	{
		IContainerFluent UsesDefaultLifetimeManagerOf(ILifetimeManager lifetimeManager);
	}
}
