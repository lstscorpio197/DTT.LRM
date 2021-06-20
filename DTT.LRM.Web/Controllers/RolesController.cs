using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Authorization;
using DTT.LRM.Authorization;
using DTT.LRM.Roles;
using DTT.LRM.Roles.Dto;
using DTT.LRM.Share;
using DTT.LRM.Web.Models.Roles;

namespace DTT.LRM.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Roles)]
    public class RolesController : LRMControllerBase
    {
        private readonly IRoleAppService _roleAppService;

        public RolesController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

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
            var data = await _roleAppService.GetDataTable(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> CreateOrUpdate(int? roleId)
        {
            var model = new EditRoleModalViewModel();
            model.Permissions = (await _roleAppService.GetAllPermissions()).Items;
            if (!roleId.HasValue)
            {
                model.Role = new RoleDto();
            }
            else
            {
                var role = await _roleAppService.GetAsync(new EntityDto(roleId.Value));
                model.Role = role;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrUpdate()
        {
            try
            {
                var roleResult = System.Web.HttpContext.Current.Request.Form["role"];
                if (roleResult != null)
                {
                    var role = new JavaScriptSerializer().Deserialize<RoleDto>(roleResult);
                    if(role.Id > 0)
                    {
                        var isExist = await _roleAppService.IsExist(role.Name, role.Id);
                        if (isExist)
                            return Json(0);
                        await _roleAppService.UpdateAsync(role);
                        return Json(1);
                    }
                    else
                    {
                        var isExist = await _roleAppService.IsExist(role.Name, 0);
                        if (isExist)
                            return Json(0);
                        var newRole = new JavaScriptSerializer().Deserialize<CreateRoleDto>(roleResult);
                        await _roleAppService.CreateAsync(newRole);
                        return Json(1);
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