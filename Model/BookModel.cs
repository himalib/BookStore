using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthnAuthz.Model
{
    public class BookModel
    {
        public BookCategory BookCategory { get; set; }
        public BookAuthor BookAuthor { get; set; }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string BookName { get; set; }
        public int ISBN { get; set; }
        public string Admin { get; set; }
        public string Authorstringid { get; set; }


        public IEnumerable<BookCategory> BookCategories { get; set; }
        public IEnumerable<BookAuthor> BookAuthors { get; set; }
    }
   }