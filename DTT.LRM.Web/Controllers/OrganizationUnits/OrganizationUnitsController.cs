﻿using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.OrganizationUnits
{
    public class OrganizationUnitsController : Controller
    {
        [AbpAuthorize]
        // GET: OrganizationUnits
        public ActionResult Index()
        {
            return View();
        }
    }
}