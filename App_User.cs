using Microsoft.AspNet.Identity.EntityFramework;

namespace NakedIdentity.Mvc
{
    public class App_User : IdentityUser
    {
        public string Country { get; set; }

        public int Age { get; set; }
    }
}