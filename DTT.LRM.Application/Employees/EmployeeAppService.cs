using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using DTT.LRM.Employees.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using DTT.LRM.Share;

namespace DTT.LRM.Employees
{
    public class EmployeeAppService : LRMAppServiceBase, IEmployeeAppService
    {
        private readonly IRepository<Employee> _employeeRepository;
        public EmployeeAppService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<int> CreateOrUpdateAsync(CreateOrUpdateEmployeeDto input)
        {
            var obj = ObjectMapper.Map<Employee>(input);
            return await _employeeRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(int id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            await _employeeRepository.DeleteAsync(employee);
        }

        public async Task<PagedResultExtendDto<EmployeeDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var keyword = input.Keyword.ToLower();
            var listEmployees = _employeeRepository.GetAllIncluding(x => x.Position, x => x.OrganizationUnit).Where(x => x.Code.Contains(keyword) ||
                                                                                                                x.Name.ToLower().Contains(keyword) ||
                                                                                                                x.Email.ToLower().Contains(keyword));
            var items = listEmployees.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<EmployeeDto>>(items);
            listItems = listItems.Select(x => { x.OrganizationUnitName = x.OrganizationUnit?.DisplayName; x.OrganizationUnit = null; return x; }).ToList();
            return new PagedResultExtendDto<EmployeeDto>(totalCount: listEmployees.Count(), items: listItems, countStatus: null);
        }

        public async Task<EmployeeDto> GetById(int id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            return ObjectMapper.Map<EmployeeDto>(employee);
        }

        public async Task<string> EmailIsExist(string email, int id)
        {
            var check = await _employeeRepository.FirstOrDefaultAsync(x => x.Email == email && x.Id != id);
            if (check != null)
                return "Email đã tồn tại.";
            return string.Empty;
        }

        public async Task<string> CodeIsExist(string code, int id)
        {
            var check = await _employeeRepository.FirstOrDefaultAsync(x => x.Code == code && x.Id != id);
            if (check != null)
                return "Mã nhân viên đã tồn tại.";
            return string.Empty;
        }

        public async Task<EmployeeDto> GetByUserId(long userId)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(x => x.UserId == userId);
            if (employee == null)
                return new EmployeeDto();
            return ObjectMapper.Map<EmployeeDto>(employee);
        }
    }
}
