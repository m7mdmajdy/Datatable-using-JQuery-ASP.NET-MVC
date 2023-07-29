using CustomFilter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomFilter.Controllers
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
            using (var context = new DataContext())
            {
                var employees = context.Employees.ToList();
                return Json(new {data = employees}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}