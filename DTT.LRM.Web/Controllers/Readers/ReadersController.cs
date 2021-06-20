using Abp.Authorization;
using DTT.LRM.OrganizationUnits;
using DTT.LRM.Positions;
using DTT.LRM.Readers;
using DTT.LRM.Readers.Dto;
using DTT.LRM.Share;
using DTT.LRM.Web.Models.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DTT.LRM.Web.Controllers.Readers
{
    [AbpAuthorize]
    public class ReadersController : LRMControllerBase
    {
        private readonly IReaderAppService _readerAppService;
        private readonly IPositionAppService _positonAppService;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        public ReadersController(IReaderAppService readerAppService, IPositionAppService positonAppService, IOrganizationUnitAppService organizationUnitAppService)
        {
            _readerAppService = readerAppService;
            _positonAppService = positonAppService;
            _organizationUnitAppService = organizationUnitAppService;
        }
        // GET: Readers
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
            var data = await _readerAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> CreateOrUpdate(int? readerId)
        {
            var model = new CreateOrUpdateModel();
            if (!readerId.HasValue)
            {
                model.Reader = new ReaderDto();
                model.ListPositions = await _positonAppService.GetAllForSelect();
                model.ListOrganizationUnits = await _organizationUnitAppService.GetAllForSelect();
            }
            else
            {
                model.Reader = await _readerAppService.GetById(readerId.Value);
                model.ListPositions = await _positonAppService.GetAllForSelect();
                model.ListOrganizationUnits = await _organizationUnitAppService.GetAllForSelect();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrUpdate()
        {
            //var generalInfo = System.Web.HttpContext.Current.Request.Form["generalInfo"];
            //if (generalInfo != null)
            //{
            //    var position = new JavaScriptSerializer().Deserialize<CreateOrUpdatePositionDto>(generalInfo);
            //    var positionId_Res = await _positionAppService.CreateOrUpdateAsync(position);
            //    if (positionId_Res > 0)
            //    {
            //        var listQuotasResult = System.Web.HttpContext.Current.Request.Form["listQuotas"];
            //        if (listQuotasResult != null)
            //        {
            //            var listQuotas = new JavaScriptSerializer().Deserialize<List<CreateOrUpdatePositionQuotaDto>>(listQuotasResult);
            //            var qouta_Res = await _positionQuotaAppService.CreateOrUpdateAsync(listQuotas, positionId_Res);
            //            return Json(qouta_Res);
            //        }
            //        else
            //            return Json(1);
            //    }
            //    else
            //        return Json(0);

            //}
            return Json(-1);
        }
    }
}