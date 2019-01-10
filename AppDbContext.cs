using Microsoft.AspNet.Identity.EntityFramework;
using NakedIdentity.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AuthnAuthz.Model;

namespace AuthnAuthz
{
    public class AppDbContext : IdentityDbContext<App_User>
    {
        public AppDbContext()
            : base("DefaultConnection")
        {
        }

        //public System.Data.Entity.DbSet<AuthnAuthz.Model.BookModel> BookModels { get; set; }
    }
}