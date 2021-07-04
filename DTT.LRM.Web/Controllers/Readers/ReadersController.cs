using Abp.Application.Services.Dto;
using Abp.Authorization;
using DTT.LRM.OrganizationUnits;
using DTT.LRM.Positions;
using DTT.LRM.Readers;
using DTT.LRM.Readers.Dto;
using DTT.LRM.Share;
using DTT.LRM.Users;
using DTT.LRM.Users.Dto;
using DTT.LRM.Web.Models.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DTT.LRM.Web.Controllers.Readers
{
    [AbpAuthorize]
    public class ReadersController : LRMControllerBase
    {
        private readonly IReaderAppService _readerAppService;
        private readonly IPositionAppService _positonAppService;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        private readonly IUserAppService _userAppService;
        public ReadersController(IReaderAppService readerAppService, IPositionAppService positonAppService, IOrganizationUnitAppService organizationUnitAppService, IUserAppService userAppService)
        {
            _readerAppService = readerAppService;
            _positonAppService = positonAppService;
            _organizationUnitAppService = organizationUnitAppService;
            _userAppService = userAppService;
        }
        // GET: Readers
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
            var data = await _readerAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> CreateOrUpdate(int? readerId)
        {
            var model = new CreateOrUpdateModel();
            model.Roles = (await _userAppService.GetRoles()).Items;
            if (!readerId.HasValue)
            {
                model.Reader = new ReaderDto();
                model.User = new UserDto();
                model.ListPositions = await _positonAppService.GetAllForSelect();
                model.ListOrganizationUnits = await _organizationUnitAppService.GetAllForSelect();
            }
            else
            {
                model.Reader = await _readerAppService.GetById(readerId.Value);
                model.User = await _userAppService.GetAsync(new EntityDto<long>(model.Reader.UserId));
                model.ListPositions = await _positonAppService.GetAllForSelect();
                model.ListOrganizationUnits = await _organizationUnitAppService.GetAllForSelect();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrUpdate()
        {
            var generalInfo = System.Web.HttpContext.Current.Request.Form["generalInfo"];
            if (generalInfo != null)
            {
                var reader = new JavaScriptSerializer().Deserialize<CreateOrUpdateReaderDto>(generalInfo);
                var userInfo = System.Web.HttpContext.Current.Request.Form["userInfo"];
                if (userInfo != null)
                {
                    var checkCodeRes = await _readerAppService.CodeIsExist(reader.Code, reader.Id);
                    if (!string.IsNullOrEmpty(checkCodeRes))
                        return Json(-2);
                    var checkEmailRes = await _readerAppService.EmailIsExist(reader.Code, reader.Id);
                    if (!string.IsNullOrEmpty(checkEmailRes))
                        return Json(-3);
                    
                    if (reader.Id > 0)
                    {
                        var user = new JavaScriptSerializer().Deserialize<UpdateUserDto>(userInfo);
                        var checkUserNameRes = await _userAppService.UserNameIsExist(user.UserName, user.Id);
                        if (!string.IsNullOrEmpty(checkUserNameRes))
                            return Json(-4);
                        var userRes = await _userAppService.UpdateAsync(user);
                        var readerRes = await _readerAppService.CreateOrUpdateAsync(reader);
                        return Json(readerRes);
                    }
                    else
                    {
                        var user = new JavaScriptSerializer().Deserialize<CreateUserDto>(userInfo);
                        var checkUserNameRes = await _userAppService.UserNameIsExist(user.UserName, 0);
                        if (!string.IsNullOrEmpty(checkUserNameRes))
                            return Json(-4);
                        var userRes = await _userAppService.CreateAsync(user);
                        if (userRes.Id > 0)
                        {
                            reader.UserId = userRes.Id;
                            var readerRes = await _readerAppService.CreateOrUpdateAsync(reader);
                            return Json(readerRes);
                        }
                        return Json(-1);
                    }
                }
                return Json(-1);
            }
            return Json(-1);
        }
    }
}