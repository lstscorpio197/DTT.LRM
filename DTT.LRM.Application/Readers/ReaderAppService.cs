using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using DTT.LRM.Readers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.AutoMapper;

namespace DTT.LRM.Readers
{
    public class ReaderAppService : LRMAppServiceBase, IReaderAppService
    {
        private readonly IRepository<Reader> _readerRepository;
        public ReaderAppService(IRepository<Reader> readerRepository)
        {
            _readerRepository = readerRepository;
        }
        public async Task<int> CreateOrUpdateAsync(CreateOrUpdateReaderDto input)
        {
            var check = await _readerRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<Reader>(input);
            return await _readerRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(int id)
        {
            var reader = await _readerRepository.GetAsync(id);
            await _readerRepository.DeleteAsync(reader);
        }

        public async Task<PagedResultDto<ReaderDto>> GetAll(PagedResultRequestDto input)
        {
            var listReaders = _readerRepository.GetAll();
            listReaders = listReaders.OrderBy("id DESC").PageBy(input);
            return new PagedResultDto<ReaderDto>
            {
                TotalCount = listReaders.Count(),
                Items = listReaders.MapTo<List<ReaderDto>>()
            };
        }

        public async Task<ReaderDto> GetById(int id)
        {
            var reader = await _readerRepository.GetAsync(id);
            return ObjectMapper.Map<ReaderDto>(reader);
        }
    }
}
