using AutoMapper;
using Contracts.Dtos.Order.Get;
using Contracts.Dtos.User.Get;
using Contracts.Exceptions;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.DelivererService
{
    public class DelivererManager : ModelServiceBase, IDelivererService
    {
        public DelivererManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper) : base(logger, repository, mapper)
        {
        }

        public async Task<GetUserDto> GetDelivererByUsername(string username)
        {
            return _mapper.Map<GetUserDto>(await _repository.Deliverers.GetByUsernameAsync(username,false));
        }

        public async Task<IEnumerable<GetUserDto>> GetDeliverersAsync()
        {
            return _mapper.Map<IEnumerable<GetUserDto>>(await _repository.Deliverers.GetAllAsync(false));
        }



    }
}
