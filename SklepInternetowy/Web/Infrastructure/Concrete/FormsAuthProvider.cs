using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Web.Infrastructure.Abstract;

namespace Web.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string n, string p)
        {
            bool result = FormsAuthentication.Authenticate(n, p);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(n, false);
            }
            return result;
        }
    }
}