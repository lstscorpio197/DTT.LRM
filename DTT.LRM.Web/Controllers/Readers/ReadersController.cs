using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.Readers
{
    public class ReadersController : LRMControllerBase
    {
        [AbpAuthorize]
        // GET: Readers
        public ActionResult Index()
        {
            return View();
        }
    }
}