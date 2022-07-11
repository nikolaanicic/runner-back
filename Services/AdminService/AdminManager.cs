using AutoMapper;
using Contracts.Dtos;
using Contracts.Dtos.User.Get;
using Contracts.Exceptions;
using Contracts.Logger;
using Contracts.Models;
using Contracts.Repository;
using Contracts.Services;
using System.Collections.Generic;
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

        private IEmailService _emailManager;

        public AdminManager(ILoggerManager logger, IRepositoryManager repository, IMapper mapper,IEmailService email) : base(logger, repository, mapper)
        {
            _emailManager = email;
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

        public async Task<IEnumerable<GetDelivererDto>> GetPendingDeliverers()
        {
            return _mapper.Map<IEnumerable<GetDelivererDto>>(await _repository.Deliverers.GetPendingDeliverers(false));
        }

        private async Task SetDelivrerState(string deliverer, ProfileState state)
        {

            var user = await _repository.Deliverers.GetByUsernameAsync(deliverer, true);

            if (user == null)
                throw new NotFoundException($"Deliverer {deliverer} doesn't exist.");

            user.State = state;

            await _repository.SaveAsync();

            await _emailManager.SendEmail(new Contracts.Dtos.Email.Message(user.Email, $"{state.ToString().ToUpper()} REQUEST", state == ProfileState.APPROVED ?
                $"Dear {user.Name} {user.LastName},\n\t\twe are happy to inform you that your deliverer registration request has been successfully approved by the admin team at runner.\n\t\t" +
                $"Welcome aboard." :
                $"Dear {user.Name} {user.LastName},\n\t\twe are sorry to inform you that your deliverer registration request has been denied by our admin team at runner.\n\t\t"
                ));
        }
    }
}
