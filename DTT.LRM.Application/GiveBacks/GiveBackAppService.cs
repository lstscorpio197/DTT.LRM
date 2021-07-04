using Abp.Domain.Repositories;
using DTT.LRM.GiveBacks.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace DTT.LRM.GiveBacks
{
    public class GiveBackAppService : LRMAppServiceBase, IGiveBackAppService
    {
        private readonly IRepository<GiveBack> _giveBackRepository;
        public GiveBackAppService(IRepository<GiveBack> giveBackRepository)
        {
            _giveBackRepository = giveBackRepository;
        }
        public async Task<string> AutoGetCode()
        {
            string codeTemp = "TS" + DateTime.Now.ToString("yy/MM/dd").Replace("/", string.Empty);
            var codeIndex = _giveBackRepository.GetAll().Where(x => x.Code.Substring(0, codeTemp.Length) == codeTemp).OrderByDescending(x => x.Code.Substring(codeTemp.Length)).Select(x => x.Code.Substring(codeTemp.Length)).FirstOrDefault();
            if (string.IsNullOrEmpty(codeIndex))
                return codeTemp + "001";
            else
                return codeTemp + (int.Parse(codeIndex) + 1).ToString("D3");
        }

        public async Task<int> CreateOrUpdateAsync(CreateOrUpdateGiveBackDto input)
        {
            var check = await _giveBackRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<GiveBack>(input);
            return await _giveBackRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(int id)
        {
            var giveBack = await _giveBackRepository.GetAsync(id);
            await _giveBackRepository.DeleteAsync(giveBack);
        }

        public async Task<PagedResultExtendDto<GiveBackDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var listBorrows = _giveBackRepository.GetAllIncluding(x => x.Reader, x=>x.Employee);
            var items = listBorrows.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<GiveBackDto>>(items);
            return new PagedResultExtendDto<GiveBackDto>(totalCount: listBorrows.Count(), items: listItems, countStatus: null);
        }

        public async Task<GiveBackDto> GetById(int id)
        {
            var borrow = await _giveBackRepository.GetAsync(id);
            return ObjectMapper.Map<GiveBackDto>(borrow);
        }
    }
}
