using AuthnAuthz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthnAuthz.ViewModel
{
    public class BookPageVM
    {
        public IEnumerable<BookCategory> BookCategories { get; set; }
    }
}