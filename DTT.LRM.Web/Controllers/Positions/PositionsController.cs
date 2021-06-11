using Abp.Authorization;
using DTT.LRM.BookClassifies;
using DTT.LRM.Positions;
using DTT.LRM.Positions.Dto;
using DTT.LRM.Share;
using DTT.LRM.Web.Models.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.Positions
{
    [AbpAuthorize]
    public class PositionsController : LRMControllerBase
    {
        private IPositionAppService _positionAppService;
        private readonly IBookClassifyAppService _bookClassifyAppService;
        public PositionsController(IPositionAppService positionAppService, IBookClassifyAppService bookClassifyAppService)
        {
            _positionAppService = positionAppService;
            _bookClassifyAppService = bookClassifyAppService;
        }
        // GET: Positions
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetDataTable(string keyword)
        {
            int start = Convert.ToInt32(Request["start"]);
            var filter = new PagedResultRequestExtendDto
            {
                SkipCount = start,
                Keyword = keyword
            };
            var data = await _positionAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CreateOrUpdate(int? positionId)
        {
            var model = new CreateOrUpdateModel();
            if (!positionId.HasValue)
            {
                model.Position = new PositionDto();
                model.ListBookClassify = await _bookClassifyAppService.GetAllForSelect();
            }
            else
            {
                model.Position = await _positionAppService.GetById(positionId.Value);
                model.ListBookClassify = await _bookClassifyAppService.GetAllForSelect();
            }
            return View(model);
        }
    }
}