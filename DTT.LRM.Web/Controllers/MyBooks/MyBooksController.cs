using Abp.Authorization;
using DTT.LRM.MyBooks;
using DTT.LRM.Readers;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.MyBooks
{
    [AbpAuthorize]
    public class MyBooksController : LRMControllerBase
    {
        private readonly IMyBookAppService _myBookAppService;
        private readonly IReaderAppService _readerAppService;
        public MyBooksController(IMyBookAppService myBookAppService, IReaderAppService readerAppService)
        {
            _myBookAppService = myBookAppService;
            _readerAppService = readerAppService;
        }
        // GET: MyBooks
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> GetDataTable()
        {
            var loginId = AbpSession.UserId.Value;
            var readerId = await _readerAppService.GetReaderIdByUserId(loginId);
            int start = Convert.ToInt32(Request["start"]);
            var filter = new PagedResultRequestExtendDto
            {
                SkipCount = start,
                Keyword = readerId.ToString()
            };
            var data = await _myBookAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }
    }
}