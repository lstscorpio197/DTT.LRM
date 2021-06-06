using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.BookFields
{
    public class BookFieldsController : LRMControllerBase
    {
        [AbpAuthorize]
        // GET: BookField
        public ActionResult Index()
        {
            return View();
        }
    }
}