using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.Employees
{
    public class EmployeesController : LRMControllerBase
    {
        [AbpAuthorize]
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }
    }
}