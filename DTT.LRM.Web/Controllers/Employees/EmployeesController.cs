using Abp.Application.Services.Dto;
using Abp.Authorization;
using DTT.LRM.Employees;
using DTT.LRM.Employees.Dto;
using DTT.LRM.OrganizationUnits;
using DTT.LRM.Positions;
using DTT.LRM.Share;
using DTT.LRM.Users;
using DTT.LRM.Users.Dto;
using DTT.LRM.Web.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DTT.LRM.Web.Controllers.Employees
{
    [AbpAuthorize]
    public class EmployeesController : LRMControllerBase
    {
        private readonly IEmployeeAppService _employeeAppService;
        private readonly IPositionAppService _positonAppService;
        private readonly IOrganizationUnitAppService _organizationUnitAppService;
        private readonly IUserAppService _userAppService;
        public EmployeesController(IEmployeeAppService employeeAppService, IPositionAppService positonAppService, IOrganizationUnitAppService organizationUnitAppService, IUserAppService userAppService)
        {
            _employeeAppService = employeeAppService;
            _positonAppService = positonAppService;
            _organizationUnitAppService = organizationUnitAppService;
            _userAppService = userAppService;
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
            var data = await _employeeAppService.GetAll(filter);
            return Json(new
            {
                data = data,
                startIndex = start + 1
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> CreateOrUpdate(int? employeeId)
        {
            var model = new CreateOrUpdateModel();
            model.Roles = (await _userAppService.GetRoles()).Items;
            if (!employeeId.HasValue)
            {
                model.Employee = new EmployeeDto();
                model.User = new UserDto();
                model.ListPositions = await _positonAppService.GetAllForSelect();
                model.ListOrganizationUnits = await _organizationUnitAppService.GetAllForSelect();
            }
            else
            {
                model.Employee = await _employeeAppService.GetById(employeeId.Value);
                model.User = await _userAppService.GetAsync(new EntityDto<long>(model.Employee.UserId));
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
                var employee = new JavaScriptSerializer().Deserialize<CreateOrUpdateEmployeeDto>(generalInfo);
                var userInfo = System.Web.HttpContext.Current.Request.Form["userInfo"];
                if (userInfo != null)
                {
                    var checkCodeRes = await _employeeAppService.CodeIsExist(employee.Code, employee.Id);
                    if (!string.IsNullOrEmpty(checkCodeRes))
                        return Json(-2);
                    var checkEmailRes = await _employeeAppService.EmailIsExist(employee.Code, employee.Id);
                    if (!string.IsNullOrEmpty(checkEmailRes))
                        return Json(-3);

                    if (employee.Id > 0)
                    {
                        var user = new JavaScriptSerializer().Deserialize<UpdateUserDto>(userInfo);
                        var checkUserNameRes = await _userAppService.UserNameIsExist(user.UserName, user.Id);
                        if (!string.IsNullOrEmpty(checkUserNameRes))
                            return Json(-4);
                        var userRes = await _userAppService.UpdateAsync(user);
                        var employeeRes = await _employeeAppService.CreateOrUpdateAsync(employee);
                        return Json(employeeRes);
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
                            employee.UserId = userRes.Id;
                            var employeeRes = await _employeeAppService.CreateOrUpdateAsync(employee);
                            return Json(employeeRes);
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