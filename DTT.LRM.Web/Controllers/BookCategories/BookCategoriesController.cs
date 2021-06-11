using Abp.Authorization;
using DTT.LRM.BookCategories;
using DTT.LRM.BookCategories.Dto;
using DTT.LRM.BookFields;
using DTT.LRM.Share;
using DTT.LRM.Web.Models.BookCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.BookCategories
{
    [AbpAuthorize]
    public class BookCategoriesController : LRMControllerBase
    {
        private readonly IBookCategoryAppService _bookCategoryAppService;
        private readonly IBookFieldAppService _bookFieldAppService;
        public BookCategoriesController(IBookCategoryAppService bookCategoryAppService, IBookFieldAppService bookFieldAppService)
        {
            _bookCategoryAppService = bookCategoryAppService;
            _bookFieldAppService = bookFieldAppService;
        }
        // GET: BookCategories
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
            var data = await _bookCategoryAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            });
        }

        public async Task<ActionResult> CreateOrUpdate(int? bookCategoryId)
        {
            var model = new CreateOrUpdateModel();
            if (!bookCategoryId.HasValue)
            {
                model.BookCategory = new BookCategoryDto();
                model.ListBookField = await _bookFieldAppService.GetAllForSelect();
            }
            else
            {
                model.BookCategory = await _bookCategoryAppService.GetById(bookCategoryId.Value);
                model.ListBookField = await _bookFieldAppService.GetAllForSelect();
            }
            return View(model);
        }
    }
}