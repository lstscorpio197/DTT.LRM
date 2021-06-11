using Abp.Authorization;
using DTT.LRM.BookClassifies;
using DTT.LRM.Publishers;
using DTT.LRM.Publishers.Dto;
using DTT.LRM.Share;
using DTT.LRM.Web.Models.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.Publishers
{
    [AbpAuthorize]
    public class PublishersController : LRMControllerBase
    {
        private readonly IPublisherAppService _publisherAppService;
        public PublishersController(IPublisherAppService publisherAppService)
        {
            _publisherAppService = publisherAppService;
        }
        // GET: Publishers
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
            var data = await _publisherAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CreateOrUpdate(int? publisherId)
        {
            var model = new CreateOrUpdateModel();
            if (!publisherId.HasValue)
            {
                model.Publisher = new PublisherDto();
            }
            else
            {
                model.Publisher = await _publisherAppService.GetById(publisherId.Value);
            }
            return View(model);
        }
    }
}