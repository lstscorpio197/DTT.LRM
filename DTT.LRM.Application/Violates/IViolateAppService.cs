using Abp.Application.Services;
using DTT.LRM.Violates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Violates
{
    public interface IViolateAppService : IApplicationService
    {
        Task<int> CreateOrUpdate(CreateOrUpdateViolateDto input);
        Task<ViolateDto> GetById(int id);
    }
}
