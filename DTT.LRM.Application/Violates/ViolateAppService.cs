using Abp.Domain.Repositories;
using DTT.LRM.Violates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Violates
{
    public class ViolateAppService : LRMAppServiceBase, IViolateAppService
    {
        private readonly IRepository<Violate> _violateRepository;
        public ViolateAppService(IRepository<Violate> violateRepository)
        {
            _violateRepository = violateRepository;
        }
        public async Task<int> CreateOrUpdate(CreateOrUpdateViolateDto input)
        {
            try
            {
                var entity = ObjectMapper.Map<Violate>(input);
                 return await _violateRepository.InsertOrUpdateAndGetIdAsync(entity);
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
        }

        public async Task<ViolateDto> GetById(int id)
        {
            var violate = _violateRepository.GetAllIncluding(x => x.Reader, x => x.Book).Where(x => x.Id == id).FirstOrDefault();
            if (violate == null)
                return new ViolateDto();
            return ObjectMapper.Map<ViolateDto>(violate);
        }
    }
}
