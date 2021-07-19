using Abp.Authorization;
using DTT.LRM.SearchInfos;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.SearchInfos
{
    [AbpAuthorize]
    public class SearchInfosController : LRMControllerBase
    {
        private readonly ISearchInfoAppService _searchInfoAppService;
        public SearchInfosController(ISearchInfoAppService searchInfoAppService)
        {
            _searchInfoAppService = searchInfoAppService;
        }
        // GET: SearchInfos
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetDataTable(string keyword = "")
        {
            int start = Convert.ToInt32(Request["start"]);
            var filter = new PagedResultRequestExtendDto
            {
                SkipCount = start,
                Keyword = keyword
            };
            var data = await _searchInfoAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }
    }
}