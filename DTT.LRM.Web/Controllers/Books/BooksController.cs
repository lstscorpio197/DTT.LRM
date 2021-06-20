using Abp.Authorization;
using DTT.LRM.BookCategories;
using DTT.LRM.BookCategories.Dto;
using DTT.LRM.BookClassifies;
using DTT.LRM.Books;
using DTT.LRM.Books.Dto;
using DTT.LRM.Publishers;
using DTT.LRM.Share;
using DTT.LRM.Web.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DTT.LRM.Web.Controllers.Books
{
    [AbpAuthorize]
    public class BooksController : LRMControllerBase
    {
        private readonly IBookAppService _bookAppService;
        private readonly IBookClassifyAppService _bookClassifyAppService;
        private readonly IBookCategoryAppService _bookCategoryAppService;
        private readonly IPublisherAppService _publisherService;
        public BooksController(IBookAppService bookAppService, IBookClassifyAppService bookClassifyAppService, IPublisherAppService publisherService, IBookCategoryAppService bookCategoryAppService)
        {
            _bookAppService = bookAppService;
            _bookClassifyAppService = bookClassifyAppService;
            _publisherService = publisherService;
            _bookCategoryAppService = bookCategoryAppService;
        }
        // GET: Books
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
            var data = await _bookAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            });
        }

        [HttpGet]
        public async Task<ActionResult> CreateOrUpdate(int? bookId)
        {
            var model = new CreateOrUpdateModel();
            model.ListBookClassifies = await _bookClassifyAppService.GetAllForSelect();
            model.ListPublishers = await _publisherService.GetAllForSelect();
            if (!bookId.HasValue)
            {
                model.Book = new BookDto();
            }
            else
            {
                model.Book = await _bookAppService.GetById(bookId.Value);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrUpdate()
        {
            try
            {
                var newCategoryResult = System.Web.HttpContext.Current.Request.Form["newBookCategory"];
                if (newCategoryResult != null)
                {
                    var newCategory = new JavaScriptSerializer().Deserialize<CreateOrUpdateBookCategoryDto>(newCategoryResult);
                    var category = await _bookCategoryAppService.CreateNewBookCategoryWithName(newCategory);
                    var bookResult = System.Web.HttpContext.Current.Request.Form["book"];
                    if(bookResult != null && category.Id > 0)
                    {
                        var book = new JavaScriptSerializer().Deserialize<CreateOrUpdateBookDto>(bookResult);
                        book.Code = book.Code.Replace("x", "") + category.Code.ToString("D3");
                        book.BookCategoryId = category.Id;
                        book.Note = string.IsNullOrEmpty(book.Note) ? "" : book.Note;
                        var res = await _bookAppService.CreateOrUpdateAsync(book);
                        return Json(res);
                    }
                }
                else
                {
                    var bookResult = System.Web.HttpContext.Current.Request.Form["book"];
                    if (bookResult != null)
                    {
                        var book = new JavaScriptSerializer().Deserialize<CreateOrUpdateBookDto>(bookResult);
                        book.Code = book.Code.Replace("x", "");
                        book.Note = string.IsNullOrEmpty(book.Note) ? "" : book.Note;
                        var res = await _bookAppService.CreateOrUpdateAsync(book);
                        return Json(res);
                    }
                }
                return Json(-1);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Json(-1);
            }
        }
    }
}