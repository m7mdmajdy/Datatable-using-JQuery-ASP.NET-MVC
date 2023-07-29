using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTable.Models;

namespace DataTable.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList()
        {
            using(DBModels dbModel = new DBModels())
            {
                var empList = new List<Employee>();
                empList = dbModel.Employees.ToList<Employee>();
                return Json(new {data=empList},JsonRequestBehavior.AllowGet);
            }
        }
    }
}