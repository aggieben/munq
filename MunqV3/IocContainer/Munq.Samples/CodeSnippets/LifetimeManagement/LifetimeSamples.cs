using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Munq;
using Munq.Samples.Models;
using System.Web.Security;

namespace Munq.Samples.CodeSnippets.LifetimeManagement
{
    public class LifetimeSamples
    {
        void SettingTheDefaultContainerLifetime()
        {
            // create the container.  Only done once in Application_Start
            IocContainer iocContainer = new IocContainer();

            // create a lifetime manager to use as default
            ILifetimeManager lifetimeManager = new LifetimeManagers.RequestLifetime();

            // set the default lifetime manager
            iocContainer.UsesDefaultLifetimeManagerOf(lifetimeManager);
            
        }

        void SettingTheLifetimeManagerForARegistration()
        {
            // create the container.  Only done once in Application_Start
            IocContainer iocContainer = new IocContainer();

            // create a Container lifetime manager to use for 'singelton' services
            // only one instance will be created and reused for each resolve request.
            ILifetimeManager containerLifetimeManager = new LifetimeManagers.ContainerLifetime();

            iocContainer.Register<IMembershipService>( ioc => new AccountMembershipService(Membership.Provider))
                         .WithLifetimeManager(containerLifetimeManager);

            iocContainer.Register<IFormsAuthenticationService>(ioc => new FormsAuthenticationService())
                        .WithLifetimeManager(containerLifetimeManager);
        }
    }
}