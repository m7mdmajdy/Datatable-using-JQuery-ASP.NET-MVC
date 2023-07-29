using ServerSideProcessing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace ServerSideProcessing.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetList()
        {
            var empList = new List<Employee>();
            using(var context = new DataContext())
            {
                empList = context.Employees.ToList();
                int start = int.Parse(Request["start"]);
                int length = int.Parse(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request[$"columns[{Request["order[0][column]"]}][name]"];
                string sortDirection = Request["order[0][dir]"];

                int totalRows = empList.Count;
                if (!string.IsNullOrEmpty(searchValue))
                {
                    empList = context.Employees
                    .Where(x => x.Name.ToLower().Contains(searchValue.ToLower()) || x.Position.ToLower().Contains(searchValue.ToLower())
                    || x.Office.ToLower().Contains(searchValue.ToLower()) || x.Age.ToString().Contains(searchValue.ToLower())
                    || x.Salary.ToString().Contains(searchValue.ToLower())
                    )
                    .ToList();
                }
                int filteredCount = empList.Count;
                //empList.OrderBy( sortColumnName+" "+sortDirection).ToList();
                empList.Skip(start).Take(length).ToList();
                return Json(new {
                    data=empList, 
                    draw = Request["draw"],
                    recordsTotal = totalRows,
                    recordsFiltered = filteredCount
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
    }
}