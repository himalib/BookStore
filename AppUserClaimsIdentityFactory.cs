using Microsoft.AspNet.Identity;
using AuthnAuthz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using NakedIdentity.Mvc;

namespace AuthnAuthz
{
    public class AppUserClaimsIdentityFactory : ClaimsIdentityFactory<App_User, string>
    {
        public async override Task<ClaimsIdentity> CreateAsync(
             UserManager<App_User, string> manager,
             App_User user,
             string authenticationType)
        {
            var identity = await base.CreateAsync(manager, user, authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));

            return identity;
        }

    }
}