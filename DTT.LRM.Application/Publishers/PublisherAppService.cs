using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using DTT.LRM.Publishers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace DTT.LRM.Publishers
{
    public class PublisherAppService : LRMAppServiceBase, IPublisherAppService
    {
        private readonly IRepository<Publisher> _publisherAppService;
        public PublisherAppService(IRepository<Publisher> publisherAppService)
        {
            _publisherAppService = publisherAppService;
        }
        public async Task<int> CreateOrUpdateAsync(CreateOrUpdatePublisherDto input)
        {
            var check = await _publisherAppService.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<Publisher>(input);
            return await _publisherAppService.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(int id)
        {
            var publisher = await _publisherAppService.GetAsync(id);
            await _publisherAppService.DeleteAsync(publisher);
        }

        public async Task<PagedResultDto<PublisherDto>> GetAll(PagedResultRequestDto input)
        {
            var listPublishers = _publisherAppService.GetAll();
            listPublishers = listPublishers.OrderBy("id DESC").PageBy(input);
            return new PagedResultDto<PublisherDto>
            {
                TotalCount = listPublishers.Count(),
                Items = listPublishers.MapTo<List<PublisherDto>>()
            };
        }

        public async Task<PublisherDto> GetById(int id)
        {
            var publisher = await _publisherAppService.GetAsync(id);
            return ObjectMapper.Map<PublisherDto>(publisher);
        }
    }
}
