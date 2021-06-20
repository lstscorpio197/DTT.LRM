using Abp.Authorization;
using DTT.LRM.BookClassifies;
using DTT.LRM.BookClassifies.Dto;
using DTT.LRM.BookFields;
using DTT.LRM.BookFields.Dto;
using DTT.LRM.Share;
using DTT.LRM.Web.Models.BookFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.BookFields
{
    [AbpAuthorize]
    public class BookFieldsController : LRMControllerBase
    {
        private readonly IBookFieldAppService _bookFieldAppService;
        private readonly IBookClassifyAppService _bookClassifyAppService;
        public BookFieldsController(IBookFieldAppService bookFieldAppService, IBookClassifyAppService bookClassifyAppService)
        {
            _bookFieldAppService = bookFieldAppService;
            _bookClassifyAppService = bookClassifyAppService;
        }
        // GET: BookField
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
            var data = await _bookFieldAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            },JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CreateOrUpdate(int? bookFieldId)
        {
            var model = new CreateOrUpdateModel();
            if (!bookFieldId.HasValue)
            {
                model.BookField = new BookFieldDto();
                model.ListBookClassify = await _bookClassifyAppService.GetAllForSelect();
            }
            else
            {
                model.BookField = await _bookFieldAppService.GetById(bookFieldId.Value);
                model.ListBookClassify = await _bookClassifyAppService.GetAllForSelect();
            }
            return View(model);
        }
    }
}