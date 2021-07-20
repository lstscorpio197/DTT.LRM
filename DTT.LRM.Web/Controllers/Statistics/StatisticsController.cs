using Abp.Authorization;
using DTT.LRM.Share;
using DTT.LRM.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.Statistics
{
    [AbpAuthorize]
    public class StatisticsController : Controller
    {
        private readonly IStatisticAppService _statisticAppService;
        public StatisticsController(IStatisticAppService statisticAppService)
        {
            _statisticAppService = statisticAppService;
        }
        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetDataTable(string keyword = "", DateTime? startDate = null, DateTime? endDate = null, int? bookClassifyId = null, int? bookFieldId = null)
        {
            int start = Convert.ToInt32(Request["start"]);
            var filter = new PagedResultRequestExtendDto
            {
                SkipCount = start,
                Keyword = keyword,
                DateStart = startDate,
                DateEnd =endDate,
                BookClassifyId = bookClassifyId,
                BookFieldId =bookFieldId
            };
            var data = await _statisticAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }
    }
}