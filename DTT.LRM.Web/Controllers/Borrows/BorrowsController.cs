using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Web.Mvc.Authorization;
using DTT.LRM.Authorization;
using DTT.LRM.BookBorrows;
using DTT.LRM.BookBorrows.Dto;
using DTT.LRM.BookCategories;
using DTT.LRM.BookReaderUsings;
using DTT.LRM.Books;
using DTT.LRM.Borrows;
using DTT.LRM.Borrows.Dto;
using DTT.LRM.Employees;
using DTT.LRM.Readers;
using DTT.LRM.Share;
using DTT.LRM.Users;
using DTT.LRM.Web.Models.Borrows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DTT.LRM.Web.Controllers.Borrows
{
    [AbpAuthorize]
    public class BorrowsController : LRMControllerBase
    {
        private readonly IBorrowAppService _borrowAppService;
        private readonly IReaderAppService _readerAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IBookAppService _bookAppService;
        private readonly IBookCategoryAppService _bookCategoryAppService;
        private readonly IBookBorrowAppService _bookBorrowAppService;
        private readonly IUserAppService _userAppService;
        private readonly IBookReaderUsingAppService _bookReaderUsingAppService;
        public BorrowsController(IBorrowAppService borrowAppService, IReaderAppService readerAppService, IBookAppService bookAppService, IBookCategoryAppService bookCategoryAppService, IBookBorrowAppService bookBorrowAppService, IUserAppService userAppService, IEmployeeAppService employeeAppService, IBookReaderUsingAppService bookReaderUsingAppService)
        {
            _borrowAppService = borrowAppService;
            _readerAppService = readerAppService;
            _bookAppService = bookAppService;
            _bookCategoryAppService = bookCategoryAppService;
            _bookBorrowAppService = bookBorrowAppService;
            _userAppService = userAppService;
            _employeeAppService = employeeAppService;
            _bookReaderUsingAppService = bookReaderUsingAppService;
        }
        // GET: Borrows
        [AbpAuthorize(PermissionNames.Pages_Borrows)]
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
            var data = await _borrowAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> CreateOrUpdate(int? borrowId)
        {
            var model = new CreateOrUpdateModel();
            var userId = AbpSession.UserId.Value;

            var user = await _userAppService.GetAsync(new EntityDto<long>(userId));
            model.ListBookCategory = await _bookCategoryAppService.GetAllForSelect();
            model.ListReader = await _readerAppService.GetAllForSelect();

            ViewBag.IsReader = model.ListReader.Select(x => x.UserId).Contains(userId);
            ViewBag.UserLoginId = userId;
            if (!borrowId.HasValue)
            {
                model.Borrow = new BorrowDto();
                model.Borrow.Code = await _borrowAppService.AutoGetCode();
                model.Borrow.BorrowDate = DateTime.Now;
                model.Borrow.Creator = user.Surname;
                model.ListBook = new List<BookBorrowDto>();
            }
            else
            {
                model.Borrow = await _borrowAppService.GetById(borrowId.Value);
                model.ListBook = await _bookBorrowAppService.GetAllByBorrowId(borrowId.Value);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrUpdate()
        {
            var generalInfo = System.Web.HttpContext.Current.Request.Form["generalInfo"];
            var listBookResult = System.Web.HttpContext.Current.Request.Form["listBooks"];
            var listBooks = new List<CreateOrUpdateBookBorrowDto>();
            if (generalInfo != null)
            {
                var borrow = new JavaScriptSerializer().Deserialize<CreateOrUpdateBorrowDto>(generalInfo);

                if (listBookResult != null)
                {
                    listBooks = new JavaScriptSerializer().Deserialize<List<CreateOrUpdateBookBorrowDto>>(listBookResult);
                }

                if (borrow.Status == 1)
                {
                    var userId = AbpSession.UserId.Value;
                    var employee = await _employeeAppService.GetByUserId(userId);
                    if (employee != null)
                    {
                        borrow.EmployeeId = employee.Id;
                    }
                    if (listBooks.Where(x => x.BookId == null || x.BookId == 0).Count() > 0)
                    {
                        return Json(-2);
                    }
                }

                var borrowId_Res = await _borrowAppService.CreateOrUpdateAsync(borrow);
                if (borrowId_Res > 0 && listBooks.Any())
                {
                    var bookBorrow_Res = await _bookBorrowAppService.CreateOrUpdateAsync(listBooks, borrowId_Res);
                    if (bookBorrow_Res > 0)
                    {
                        if (borrow.Status == 1)
                        {
                            var listBookIds = listBooks.Select(x => x.BookId.Value).ToList();
                            await _bookAppService.UpdateBookStatus(listBookIds, (int)LRMEnum.BookStatus.Using);
                            await _bookReaderUsingAppService.CreateByListBookIdAndReaderId(listBooks, borrow.ReaderId);
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