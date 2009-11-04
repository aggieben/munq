// Article3
using System;
using System.Web;
using System.Security.Principal;
using FinalApp.Interfaces;

namespace FinalApp.Tests.Mocks
{
    public class MockFormsAuthenticationService : IFormsAuthentication
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
        }

        public void SignOut()
        {
        }
    }

    public class MockIdentity : IIdentity
    {
        public string AuthenticationType
        {
            get
            {
                return "MockAuthentication";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "someUser";
            }
        }
    }

    public class MockPrincipal : IPrincipal
    {
        IIdentity _identity;

        public IIdentity Identity
        {
            get
            {
                if (_identity == null)
                {
                    _identity = new MockIdentity();
                }
                return _identity;
            }
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }

    public class MockHttpContext : HttpContextBase
    {
        private IPrincipal _user;

        public override IPrincipal User
        {
            get
            {
                if (_user == null)
                {
                    _user = new MockPrincipal();
                }
                return _user;
            }
            set
            {
                _user = value;
            }
        }
    }

}
