using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.Positions
{
    public class PositionsController : LRMControllerBase
    {
        [AbpAuthorize]
        // GET: Positions
        public ActionResult Index()
        {
            return View();
        }
    }
}