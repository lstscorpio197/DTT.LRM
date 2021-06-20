using Abp.Domain.Repositories;
using DTT.LRM.PositionQuotas.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.PositionQuotas
{
    public class PositionQuotaAppService : LRMAppServiceBase, IPositionQuotaAppService
    {
        private readonly IRepository<PositionQuota> _positionQuotaRepository;
        public PositionQuotaAppService(IRepository<PositionQuota> positionQuotaRepository)
        {
            _positionQuotaRepository = positionQuotaRepository;
        }
        public async Task<int> CreateOrUpdateAsync(List<CreateOrUpdatePositionQuotaDto> input, int positionId)
        {
            try
            {
                var listIds = input.Select(x => x.Id);
                await _positionQuotaRepository.DeleteAsync(x => x.PositionId == positionId && listIds.Contains(x.Id) == false);
                foreach(var item in input)
                {
                    item.PositionId = positionId;
                    var obj = ObjectMapper.Map<PositionQuota>(item);
                    await _positionQuotaRepository.InsertOrUpdateAndGetIdAsync(obj);
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public async Task<List<PositionQuotaDto>> GetAllByPositionId(int positionId)
        {
            var listEntities = await _positionQuotaRepository.GetAllListAsync(x => x.PositionId == positionId);
            return ObjectMapper.Map<List<PositionQuotaDto>>(listEntities);
        }
    }
}
