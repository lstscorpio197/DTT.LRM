using Abp.Domain.Repositories;
using DTT.LRM.Liquidations.Dto;
using DTT.LRM.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace DTT.LRM.Liquidations
{
    public class LiquidationAppService : LRMAppServiceBase, ILiquidationAppService
    {
        private readonly IRepository<Liquidation> _liquidationRepository;
        public LiquidationAppService(IRepository<Liquidation> liquidationRepository)
        {
            _liquidationRepository = liquidationRepository;
        }
        public async Task<string> AutoGetCode()
        {
            string codeTemp = "TL" + DateTime.Now.ToString("yy/MM/dd").Replace("/", string.Empty);
            var codeIndex = _liquidationRepository.GetAll().Where(x => x.Code.Substring(0, codeTemp.Length) == codeTemp).OrderByDescending(x => x.Code.Substring(codeTemp.Length)).Select(x => x.Code.Substring(codeTemp.Length)).FirstOrDefault();
            if (string.IsNullOrEmpty(codeIndex))
                return codeTemp + "001";
            else
                return codeTemp + (int.Parse(codeIndex) + 1).ToString("D3");
        }

        public async Task<int> CreateOrUpdateAsync(CreateLiquidationDto input)
        {
            var check = await _liquidationRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<Liquidation>(input);
            return await _liquidationRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task<PagedResultExtendDto<LiquidationDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var keyword = input.Keyword.ToLower();
            var listLiquidations = _liquidationRepository.GetAll().Where(x=>x.Code.Contains(keyword)||
                                                                            x.Creator.ToLower().Contains(keyword)||
                                                                            x.Description.ToLower().Contains(keyword));
            var items = listLiquidations.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<LiquidationDto>>(items);
            return new PagedResultExtendDto<LiquidationDto>(totalCount: listLiquidations.Count(), items: listItems, countStatus: null);
        }

        public async Task<LiquidationDto> GetById(int id)
        {
            var liquidation = await _liquidationRepository.GetAsync(id);
            return ObjectMapper.Map<LiquidationDto>(liquidation);
        }
    }
}
