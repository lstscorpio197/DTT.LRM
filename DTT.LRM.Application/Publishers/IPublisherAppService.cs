using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DTT.LRM.Publishers.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Publishers
{
    public interface IPublisherAppService : IApplicationService
    {
        Task<PagedResultExtendDto<PublisherDto>> GetAll(PagedResultRequestExtendDto input);
        Task<List<PublisherDto>> GetAllForSelect();
        Task<int> CreateOrUpdateAsync(CreateOrUpdatePublisherDto input);
        Task<PublisherDto> GetById(int id);
        Task DeleteById(int id);
    }
}
