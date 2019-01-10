using AuthnAuthz.Infrastructure;
using AuthnAuthz.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthnAuthz.Controllers
{
    [RoutePrefix("api")]
    public class AutherAPIController : ApiController
    {
        ConnectionFactory _connectionFactory;

        public AutherAPIController()
        {
            _connectionFactory = new ConnectionFactory();
        }

        [Route("general/author/search")]
        [HttpGet]
        public IHttpActionResult Author(string search)
        {
            BookModel bm = new BookModel ();
            bm.BookAuthors = GetAllAuthor(search);
            return Ok(bm);
        }

        public IEnumerable<BookAuthor> GetAllAuthor(string search)
        {
            using (var con = _connectionFactory.GetConnection)
            {
                var query = "usp_Book_SelectAuthorById";
                var param = new DynamicParameters();
                param.Add("@search", search);
                var list = con.Query<BookAuthor>(query, param, commandType: CommandType.StoredProcedure);
                return list;
            }
        }
    }
}
