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
using DTT.LRM.Share;

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
            try
            {
                var obj = ObjectMapper.Map<Reader>(input);
                return await _readerRepository.InsertOrUpdateAndGetIdAsync(obj);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return -1;
            }
        }

        public async Task DeleteById(int id)
        {
            var reader = await _readerRepository.GetAsync(id);
            await _readerRepository.DeleteAsync(reader);
        }

        public async Task<string> EmailIsExist(string email, int id)
        {
            var check = await _readerRepository.FirstOrDefaultAsync(x => x.Email == email && x.Id != id);
            if (check != null)
                return "Email đã tồn tại.";
            return string.Empty;
        }

        public async Task<PagedResultExtendDto<ReaderDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var keyword = input.Keyword.ToLower();
            var listReaders = _readerRepository.GetAllIncluding(x => x.Position, x=> x.OrganizationUnit).Where(x=>x.Code.Contains(keyword)||
                                                                                                                x.Name.ToLower().Contains(keyword)||
                                                                                                                x.Email.ToLower().Contains(keyword));
            var items = listReaders.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<ReaderDto>>(items);
            listItems = listItems.Select(x => { x.OrganizationUnitName = x.OrganizationUnit?.DisplayName; x.OrganizationUnit = null; return x; }).ToList();
            return new PagedResultExtendDto<ReaderDto>(totalCount: listReaders.Count(), items: listItems, countStatus: null);
        }

        public async Task<ReaderDto> GetById(int id)
        {
            var reader = await _readerRepository.GetAsync(id);
            return ObjectMapper.Map<ReaderDto>(reader);
        }

        public async Task<string> CodeIsExist(string code, int id)
        {
            var check = await _readerRepository.FirstOrDefaultAsync(x => x.Code == code && x.Id != id);
            if (check != null)
                return "Mã độc giả đã tồn tại.";
            return string.Empty;
        }

        public async Task<List<ReaderDto>> GetAllForSelect()
        {
            var listReaders = await _readerRepository.GetAllListAsync(x=>x.Status == true);
            return ObjectMapper.Map<List<ReaderDto>>(listReaders);
        }

        public async Task<int> GetReaderIdByUserId(long userId)
        {
            var reader = await _readerRepository.FirstOrDefaultAsync(x => x.UserId == userId);
            if (reader == null)
                return 0;
            return reader.Id;
        }
    }
}
