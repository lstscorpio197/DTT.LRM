using Abp.Authorization;
using DTT.LRM.OrganizationUnits;
using DTT.LRM.OrganizationUnits.Dto;
using DTT.LRM.Share;
using DTT.LRM.Web.Models.OrganizationUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.OrganizationUnits
{
    [AbpAuthorize]
    public class OrganizationUnitsController : LRMControllerBase
    {
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        public OrganizationUnitsController(IOrganizationUnitAppService organizationUnitAppService)
        {
            _organizationUnitAppService = organizationUnitAppService;
        }
        // GET: OrganizationUnits
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
            var data = await _organizationUnitAppService.GetAllForDatatable(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            });
        }

        public async Task<ActionResult> CreateOrUpdate(int? organizationUnitId)
        {
            var model = new CreateOrUpdateModel();
            if (!organizationUnitId.HasValue)
            {
                model.OrganizationUnit = new OrganizationUnitBasicDto();
                model.ListOrganizationUnits = await _organizationUnitAppService.GetAllForSelect();
            }
            else
            {
                model.OrganizationUnit = await _organizationUnitAppService.GetById(organizationUnitId.Value);
                model.ListOrganizationUnits = await _organizationUnitAppService.GetAllForSelect();
            }
            return View(model);
        }
    }
}