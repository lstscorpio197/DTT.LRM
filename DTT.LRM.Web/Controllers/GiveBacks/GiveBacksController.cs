using Abp.Authorization;
using DTT.LRM.BookGiveBacks;
using DTT.LRM.BookGiveBacks.Dto;
using DTT.LRM.BookReaderUsings;
using DTT.LRM.Employees;
using DTT.LRM.GiveBacks;
using DTT.LRM.GiveBacks.Dto;
using DTT.LRM.Readers;
using DTT.LRM.Readers.Dto;
using DTT.LRM.Share;
using DTT.LRM.Violates;
using DTT.LRM.Web.Models.GiveBacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DTT.LRM.Web.Controllers.GiveBacks
{
    [AbpAuthorize]
    public class GiveBacksController : LRMControllerBase
    {
        private readonly IGiveBackAppService _giveBackAppService;
        private readonly IBookGiveBackAppService _bookGiveBackAppService;
        private readonly IViolateAppService _violateAppService;
        private readonly IReaderAppService _readerAppService;
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IBookReaderUsingAppService _bookReaderUsingAppService;
        public GiveBacksController(IGiveBackAppService giveBackAppService, IBookGiveBackAppService bookGiveBackAppService, IViolateAppService violateAppService, IReaderAppService readerAppService, IEmployeeAppService employeeAppService, IBookReaderUsingAppService bookReaderUsingAppService)
        {
            _giveBackAppService = giveBackAppService;
            _bookGiveBackAppService = bookGiveBackAppService;
            _violateAppService = violateAppService;
            _readerAppService = readerAppService;
            _employeeAppService = employeeAppService;
            _bookReaderUsingAppService = bookReaderUsingAppService;
        }
        // GET: GiveBacks
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetDataTable(string keyword = "")
        {
            int start = Convert.ToInt32(Request["start"]);
            var filter = new PagedResultRequestExtendDto
            {
                SkipCount = start,
                Keyword = keyword
            };
            var data = await _giveBackAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> CreateOrUpdate(int? giveBackId)
        {
            var model = new CreateOrUpdateModel();
            var userId = AbpSession.UserId.Value;
            var employee = await _employeeAppService.GetByUserId(userId);
            ViewBag.EmployeeName = employee.Name;
            model.ListReader = await _readerAppService.GetAllForSelect();
            if (!giveBackId.HasValue)
            {
                model.GiveBack = new GiveBackDto();
                model.GiveBack.Code = await _giveBackAppService.AutoGetCode();
                model.GiveBack.GiveBackDate = DateTime.Now;
                if (employee != null)
                {
                    model.GiveBack.EmployeeId = employee.Id;
                }
                model.ListBookGiveBacks = new List<BookGiveBackDto>();
            }
            else
            {
                model.GiveBack = await _giveBackAppService.GetById(giveBackId.Value);
                model.ListBookGiveBacks = await _bookGiveBackAppService.GetAllByGiveBackId(giveBackId.Value);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrUpdate()
        {
            var generalInfo = System.Web.HttpContext.Current.Request.Form["generalInfo"];
            var listBookGiveBacksResult = System.Web.HttpContext.Current.Request.Form["listBookGiveBacks"];
            var listBookGiveBackAndViolatesResult = System.Web.HttpContext.Current.Request.Form["listBookGiveBackAndViolates"];
            if (generalInfo != null)
            {
                var giveBack = new JavaScriptSerializer().Deserialize<CreateOrUpdateGiveBackDto>(generalInfo);
                var giveBackId = await _giveBackAppService.CreateOrUpdateAsync(giveBack);
                if (giveBackId > 0)
                {
                    if (listBookGiveBackAndViolatesResult != null)
                    {
                        var listBookGiveBackAndViolates = new JavaScriptSerializer().Deserialize<List<BookGiveBackAndViolateDto>>(listBookGiveBackAndViolatesResult);
                        await _bookGiveBackAppService.CreateAndViolateAsync(listBookGiveBackAndViolates, giveBackId);
                        var listBookIds = listBookGiveBackAndViolates.Select(x => x.BookId).ToList();
                        await _bookReaderUsingAppService.UpdateGiveBackBook(listBookIds, giveBack.ReaderId);
                    }
                    if (listBookGiveBacksResult != null)
                    {
                        var listBookGiveBacks = new JavaScriptSerializer().Deserialize<List<CreateOrUpdateBookGiveBackDto>>(listBookGiveBacksResult);
                        await _bookGiveBackAppService.CreateOrUpdateAsync(listBookGiveBacks, giveBackId);
                        var listBookIds = listBookGiveBacks.Select(x => x.BookId).ToList();
                        await _bookReaderUsingAppService.UpdateGiveBackBook(listBookIds, giveBack.ReaderId);
                    }
                    return Json(1);
                }
                return Json(0);
            }
            else
                return Json(-1);
        }
    }
}