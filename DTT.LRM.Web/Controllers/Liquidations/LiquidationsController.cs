using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.Liquidations
{
    public class LiquidationsController : LRMControllerBase
    {
        // GET: Liquidations
        public ActionResult Index()
        {
            return View();
        }
    }
}