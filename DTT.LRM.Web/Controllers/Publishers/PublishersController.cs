using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.Publishers
{
    public class PublishersController : LRMControllerBase
    {
        [AbpAuthorize]
        // GET: Publishers
        public ActionResult Index()
        {
            return View();
        }
    }
}