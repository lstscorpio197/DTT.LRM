using Abp.Authorization;
using DTT.LRM.Share;
using DTT.LRM.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTT.LRM.Web.Controllers.UserList
{
    [AbpAuthorize]
    public class UserListController : LRMControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserListController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        // GET: UserList
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetDataTable(string keyword = "", int? status = null)
        {
            int start = Convert.ToInt32(Request["start"]);
            var filter = new PagedResultRequestExtendDto
            {
                SkipCount = start,
                Keyword = keyword,
                Status = status
            };
            var data = await _userAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }
    }
}