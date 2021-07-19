using Abp.Application.Services.Dto;
using Abp.Authorization;
using DTT.LRM.BookCategories;
using DTT.LRM.BookLiquidations;
using DTT.LRM.BookLiquidations.Dto;
using DTT.LRM.BookReaderUsings;
using DTT.LRM.Books;
using DTT.LRM.Employees;
using DTT.LRM.Liquidations;
using DTT.LRM.Liquidations.Dto;
using DTT.LRM.Publishers;
using DTT.LRM.Share;
using DTT.LRM.Users;
using DTT.LRM.Web.Models.Liquidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DTT.LRM.Web.Controllers.Liquidations
{
    [AbpAuthorize]
    public class LiquidationsController : LRMControllerBase
    {
        private readonly ILiquidationAppService _liquidationAppService;
        private readonly IBookLiquidationAppService _bookLiquidationAppService;
        private readonly IBookCategoryAppService _bookCategoryAppService;
        private readonly IUserAppService _userAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IBookReaderUsingAppService _bookReaderUsingAppService;
        private readonly IPublisherAppService _publisherAppService;
        private readonly IBookAppService _bookAppService;
        public LiquidationsController(ILiquidationAppService liquidationAppService, IBookLiquidationAppService bookLiquidationAppService, IBookCategoryAppService bookCategoryAppService, IUserAppService userAppService, IEmployeeAppService employeeAppService, IBookReaderUsingAppService bookReaderUsingAppService, IPublisherAppService publisherAppService, IBookAppService bookAppService)
        {
            _liquidationAppService = liquidationAppService;
            _bookLiquidationAppService = bookLiquidationAppService;
            _bookCategoryAppService = bookCategoryAppService;
            _userAppService = userAppService;
            _employeeAppService = employeeAppService;
            _bookReaderUsingAppService = bookReaderUsingAppService;
            _publisherAppService = publisherAppService;
            _bookAppService = bookAppService;
        }
        // GET: Liquidations
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
            var data = await _liquidationAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> CreateOrUpdate(int? liquidationId)
        {
            var model = new CreateOrUpdateModel();
            var userId = AbpSession.UserId.Value;

            var user = await _userAppService.GetAsync(new EntityDto<long>(userId));
            model.ListBookCategories = await _bookCategoryAppService.GetAllForSelect();
            model.ListPublishers = await _publisherAppService.GetAllForSelect();
            ViewBag.UserLoginId = userId;
            if (!liquidationId.HasValue)
            {
                model.Liquidation = new LiquidationDto();
                model.Liquidation.Code = await _liquidationAppService.AutoGetCode();
                model.Liquidation.CreateDate = DateTime.Now;
                model.Liquidation.Creator = user.Surname;
            }
            else
            {
                model.Liquidation = await _liquidationAppService.GetById(liquidationId.Value);
                model.ListBookLiquidations = await _bookLiquidationAppService.GetAllByLiquidationId(liquidationId.Value);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrUpdate()
        {
            var generalInfo = System.Web.HttpContext.Current.Request.Form["generalInfo"];
            var listBookResult = System.Web.HttpContext.Current.Request.Form["listBooks"];
            var listBooks = new List<CreateBookLiquidationDto>();
            if (generalInfo != null)
            {
                var liquidation = new JavaScriptSerializer().Deserialize<CreateLiquidationDto>(generalInfo);

                if (listBookResult != null)
                {
                    listBooks = new JavaScriptSerializer().Deserialize<List<CreateBookLiquidationDto>>(listBookResult);
                }
                if (!listBooks.Any())
                {
                    return Json(-2);
                }
                var liquidationId_Res = await _liquidationAppService.CreateOrUpdateAsync(liquidation);
                if (liquidationId_Res > 0)
                {
                    var bookBorrow_Res = await _bookLiquidationAppService.CreateOrUpdateAsync(listBooks, liquidationId_Res);
                    if (bookBorrow_Res > 0)
                    {
                        foreach (var book in listBooks)
                        {
                            await _bookAppService.UpdateBookStatusLiquidation(book.BookCategorieId, book.Author, (book.ReleaseYear.HasValue ? book.ReleaseYear.Value : 0), (book.PublisherId.HasValue ? book.PublisherId.Value : 0));
                        }
                        return Json(1);
                    }
                    else
                        return Json(-1);
                }
                else
                    return Json(0);
            }
            else
                return Json(-1);
        }
    }
}