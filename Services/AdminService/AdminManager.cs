using AutoMapper;
using Contracts.Dtos;
using Contracts.Dtos.User.Get;
using Contracts.Exceptions;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Services;
using Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AdminService
{
    public class AdminManager :ModelServiceBase, IAdminService
    {
        public AdminManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper) : base(logger, repository, mapper)
        {
        }

        public async Task ApproveAccountAsync(string deliverer)
        {
            await SetDelivrerState(deliverer, ProfileState.APPROVED);
        }

        public async Task DisapproveAccountAsync(string deliverer)
        {
            await SetDelivrerState(deliverer, ProfileState.DENIED);
        }

        private async Task SetDelivrerState(string deliverer, ProfileState state)
        {

            var user = await _repository.Deliverers.GetByUsernameAsync(deliverer, true);

            if (user == null)
                throw new NotFoundException($"Deliverer {deliverer} doesn't exist.");

            user.State = state;

            await _repository.SaveAsync();
        }
    }
}
