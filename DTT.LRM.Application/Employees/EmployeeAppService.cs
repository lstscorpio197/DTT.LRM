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
            var check = await _employeeRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
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
            var listEmployees = _employeeRepository.GetAll();
            var items = listEmployees.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<EmployeeDto>>(items);
            return new PagedResultExtendDto<EmployeeDto>(totalCount: listEmployees.Count(), items: listItems, countStatus: null);
        }

        public async Task<EmployeeDto> GetById(int id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            return ObjectMapper.Map<EmployeeDto>(employee);
        }
    }
}
