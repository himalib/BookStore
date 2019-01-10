using AuthnAuthz.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthnAuthz.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View(DapperORM.ReturnList<EmpModel>("EmployeeViewAll"));
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmployeeId", id);
                return View(DapperORM.ReturnList<EmpModel>("EmployeeViewById", param).FirstOrDefault<EmpModel>());
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(EmpModel emp)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId", emp.EmployeeId);
            param.Add("@Name", emp.Name);
            param.Add("@City", emp.City);
            param.Add("@Address", emp.Address);
            DapperORM.ExecuteWithoutReturn("EmployeeAddOrEdit", param);
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeID", id);
            DapperORM.ExecuteWithoutReturn("EmployeeDelete", param);
            return RedirectToAction("Index");
        }
    }
}