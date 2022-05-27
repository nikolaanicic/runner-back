using AutoMapper;
using Contracts.Dtos;
using Contracts.Dtos.User.Get;
using Contracts.Exceptions;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Services;
using System.Threading.Tasks;

namespace Services.AdminService
{

    /// <summary>
    /// This class provides functionalities to the AdminController i.e. it provides 
    /// functionalities for the administrators of the application
    /// 
    /// </summary>
    public class AdminManager :ModelServiceBase, IAdminService
    {
        public AdminManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper) : base(logger, repository, mapper)
        {
        }


        /// <summary>
        /// This method approves the account of the wanted deliverer
        /// </summary>
        /// <param name="deliverer"></param>
        /// <returns></returns>
        public async Task ApproveAccountAsync(string deliverer)
        {
            await SetDelivrerState(deliverer, ProfileState.APPROVED);
        }


        /// <summary>
        /// This method rejects the account request of the wanted deliverer
        /// </summary>
        /// <param name="deliverer"></param>
        /// <returns></returns>
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
