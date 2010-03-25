using System.Web.Security;
using Munq.DI;
using Munq.DI.Configuration;
using FinalApp.Interfaces;

namespace FinalApp.AccountMembership
{
    public class AccountMembershipRegistrar : IMunqConfig
    {
        #region IMunqConfig Members

        public void RegisterIn(Container container)
        {
            container.Register<MembershipProvider>(ioc => Membership.Provider);
            container.Register<IMembershipService>(
                ioc => new AccountMembershipService(ioc.Resolve<MembershipProvider>()));
        }

        #endregion
    }
}
