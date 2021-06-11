using Abp.Authorization;
using DTT.LRM.BookClassifies;
using DTT.LRM.BookClassifies.Dto;
using DTT.LRM.Share;
using DTT.LRM.Web.Models.BookClassifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.BookClassifies
{
    [AbpAuthorize]
    public class BookClassifiesController : LRMControllerBase
    {
        private readonly IBookClassifyAppService _bookClassifyAppService;
        public BookClassifiesController(IBookClassifyAppService bookClassifyAppService)
        {
            _bookClassifyAppService = bookClassifyAppService;
        }
        // GET: BookClassifies
        public async Task<ActionResult> Index()
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
            var data = await _bookClassifyAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet) ;
        }

        public async Task<ActionResult> CreateOrUpdate(int? bookClassifyId)
        {
            var model = new CreateOrUpdateModel();
            if (!bookClassifyId.HasValue)
            {
                model.BookClassify = new BookClassifyDto();
            }
            else
            {
                model.BookClassify = await _bookClassifyAppService.GetById(bookClassifyId.Value);
            }
            return View(model);
        }
    }
}