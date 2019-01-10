using AuthnAuthz.Infrastructure;
using AuthnAuthz.Model;
using AuthnAuthz.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthnAuthz.Controllers
{
    public class BookStoreController : Controller
    {
        ConnectionFactory _connectionFactory;

        public BookStoreController()
        {
            _connectionFactory = new ConnectionFactory();
        }
        // GET: BookStore
        public ActionResult Index()
        {
            //return View();
            return View(DapperORM.ReturnList<BookModel>("usp_Book_SelectAll"));
        }

        //[Route("addbookstore")]
        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            BookModel bm = new BookModel(); 
            bm.BookCategories = GetAllCategory();
            return View(bm);
        }

        [HttpPost]
        public ActionResult Add(BookModel bk)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var query = "usp_Book_Author";
                var param = new DynamicParameters();
                param.Add("@BookName", bk.BookName);
                param.Add("@ISBN", bk.ISBN);
                param.Add("@Admin", User.Identity.Name);
                param.Add("@CategoryId", bk.CategoryId);
                param.Add("@AuthorstringId", bk.Authorstringid);
                var data = conn.Query<BookModel>(query, param, commandType: CommandType.StoredProcedure);
                return RedirectToAction("SelectBook", "BookStore");
            }

        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            BookModel pagevm = new BookModel();
            try
            {
                pagevm = GetById(id);
                pagevm.BookAuthors = GetAutherById(id);
                return View(pagevm);
            }
            catch (Exception ex)
            {
                return View();
            }

        }
        public IEnumerable<BookAuthor> GetAutherById(int id)
        {
            using (var con = _connectionFactory.GetConnection)
            {
                var query = "usp_SelectAutherById";
                var param = new DynamicParameters();
                param.Add("@BookId", id);
                var list = con.Query<BookAuthor>(query,param, commandType: CommandType.StoredProcedure);
                return list;
            }
        }
        public BookModel GetById(int id)
        {
            using (var con = _connectionFactory.GetConnection)
            {
                var query = "usp_Book_SelectById";
                var param = new DynamicParameters();
                param.Add("@Id", id);
                var list = con.Query<BookModel, BookCategory, BookModel>(query, (bookmodel, bookcategory) =>
                {
                    bookmodel.BookCategory = bookcategory;
                    return bookmodel;
                }, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                list.BookCategories = GetAllCategory();
                return list;
            }
        }

        [HttpPost]
        public ActionResult Update(BookModel bmu)
        {
            using (var con = _connectionFactory.GetConnection)
            {
                var query = "usp_Book_Update";
                var param = new DynamicParameters();
                param.Add("@BookId", bmu.Id);
                param.Add("@BookName", bmu.BookName);
                param.Add("@ISBN", bmu.ISBN);
                param.Add("@Admin", User.Identity.Name);
                param.Add("@CategoryId", bmu.CategoryId);
                param.Add("@AuthorstringId", bmu.Authorstringid);
                con.Query<BookModel>(query, param, commandType: CommandType.StoredProcedure);
                return RedirectToAction("SelectBook", "BookStore");
            }
        }

        public ActionResult Delete(int id)
        {
            using (var con = _connectionFactory.GetConnection)
            {
                var query = "usp_Book_DeleteById";
                var param = new DynamicParameters();
                param.Add("@Id", id);
                con.Query(query, param, commandType: CommandType.StoredProcedure);
                return RedirectToAction("SelectBook", "BookStore");
            }
        }

        [HttpGet]
        public ActionResult SelectBook()
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                var query = "usp_Book_SelectAll";
                var param = new DynamicParameters();
                var data = conn.Query<BookModel, BookCategory, BookModel>(query, (BookModel, BookCategory) => {
                    BookModel.BookCategory = BookCategory;
                    return BookModel;
                }, param, commandType: CommandType.StoredProcedure);

                return View(data);
            }
        }

        public IEnumerable<BookCategory> GetAllCategory()
        {
            using (var con = _connectionFactory.GetConnection)
            {
                var query = "usp_Book_SelectCatagoryById";
                var param = new DynamicParameters();
                var list = con.Query<BookCategory>(query, param, commandType: CommandType.StoredProcedure);
                return list;
            }
        }
    }
}