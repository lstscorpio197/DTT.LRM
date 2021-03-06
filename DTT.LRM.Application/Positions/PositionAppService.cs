using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using DTT.LRM.Positions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using DTT.LRM.Share;

namespace DTT.LRM.Positions
{
    public class PositionAppService : LRMAppServiceBase, IPositionAppService
    {
        private readonly IRepository<Position> _positionRepository;
        public PositionAppService(IRepository<Position> positionRepository)
        {
            _positionRepository = positionRepository;
        }
        public async Task<int> CreateOrUpdateAsync(CreateOrUpdatePositionDto input)
        {
            var check = await _positionRepository.FirstOrDefaultAsync(x => x.Code == input.Code && x.Id != input.Id);
            if (check != null)
                return 0;
            var obj = ObjectMapper.Map<Position>(input);
            return await _positionRepository.InsertOrUpdateAndGetIdAsync(obj);
        }

        public async Task DeleteById(int id)
        {
            var position = await _positionRepository.GetAsync(id);
            await _positionRepository.DeleteAsync(position);
        }

        public async Task<PagedResultExtendDto<PositionDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var listPositions = _positionRepository.GetAll();
            var items = listPositions.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<PositionDto>>(items);
            return new PagedResultExtendDto<PositionDto>(totalCount: listPositions.Count(), items: listItems, countStatus: null);
        }

        public async Task<List<PositionDto>> GetAllForSelect()
        {
            var listPositions = await _positionRepository.GetAllListAsync();
            return ObjectMapper.Map<List<PositionDto>>(listPositions);
        }

        public async Task<PositionDto> GetById(int id)
        {
            var position = await _positionRepository.GetAsync(id);
            return ObjectMapper.Map<PositionDto>(position);
        }
    }
}
