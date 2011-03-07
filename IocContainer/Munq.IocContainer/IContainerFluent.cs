using System;

namespace Munq
{
	/// <include file='XmlDocumentation/IContainerFluent.xml' path='IContainerFluent/SummaryDocumentation/*' />
	public interface IContainerFluent
	{
		/// <include file='XmlDocumentation/IContainerFluent.xml'
		/// path='IContainerFluent/Members[@name="UsesDefaultLifetimeManagerOf"]/*' />
		IContainerFluent UsesDefaultLifetimeManagerOf(ILifetimeManager lifetimeManager);
	}
}
