using Abp.Authorization;
using DTT.LRM.BookClassifies;
using DTT.LRM.PositionQuotas;
using DTT.LRM.PositionQuotas.Dto;
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
using System.Web.Script.Serialization;

namespace DTT.LRM.Web.Controllers.Positions
{
    [AbpAuthorize]
    public class PositionsController : LRMControllerBase
    {
        private IPositionAppService _positionAppService;
        private IPositionQuotaAppService _positionQuotaAppService;
        private readonly IBookClassifyAppService _bookClassifyAppService;
        public PositionsController(IPositionAppService positionAppService, IBookClassifyAppService bookClassifyAppService, IPositionQuotaAppService positionQuotaAppService)
        {
            _positionAppService = positionAppService;
            _bookClassifyAppService = bookClassifyAppService;
            _positionQuotaAppService = positionQuotaAppService;
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

        [HttpGet]
        public async Task<ActionResult> CreateOrUpdate(int? positionId)
        {
            var model = new CreateOrUpdateModel();
            if (!positionId.HasValue)
            {
                model.Position = new PositionDto();
                model.ListBookClassify = await _bookClassifyAppService.GetAllForSelect();
                model.ListPositionQuotas = new List<PositionQuotaDto>();
            }
            else
            {
                model.Position = await _positionAppService.GetById(positionId.Value);
                model.ListBookClassify = await _bookClassifyAppService.GetAllForSelect();
                model.ListPositionQuotas = await _positionQuotaAppService.GetAllByPositionId(positionId.Value);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrUpdate()
        {
            var generalInfo = System.Web.HttpContext.Current.Request.Form["generalInfo"];
            if (generalInfo != null)
            {
                var position = new JavaScriptSerializer().Deserialize<CreateOrUpdatePositionDto>(generalInfo);
                var positionId_Res = await _positionAppService.CreateOrUpdateAsync(position);
                if (positionId_Res > 0)
                {
                    var listQuotasResult = System.Web.HttpContext.Current.Request.Form["listQuotas"];
                    if (listQuotasResult != null)
                    {
                        var listQuotas = new JavaScriptSerializer().Deserialize<List<CreateOrUpdatePositionQuotaDto>>(listQuotasResult);
                        var qouta_Res = await _positionQuotaAppService.CreateOrUpdateAsync(listQuotas, positionId_Res);
                        return Json(qouta_Res);
                    }
                    else
                        return Json(1);
                }
                else
                    return Json(0);

            }
            return Json(-1);
        }
    }
}