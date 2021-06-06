using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.BookCategories
{
    public class BookCategoriesController : LRMControllerBase
    {
        [AbpAuthorize]
        // GET: BookCategories
        public ActionResult Index()
        {
            return View();
        }
    }
}