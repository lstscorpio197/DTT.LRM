using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.Books
{
    public class BooksController : LRMControllerBase
    {
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrUpdate()
        {
            return View();
        }
    }
}