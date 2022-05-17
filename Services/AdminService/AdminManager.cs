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

        public async Task<GetAdminDto> GetByUsername(string username)
        {
            Admin admin = await _repository.Admins.GetByUsernameAsync(username, false);
            if (admin == null)
                throw new NotFoundException($"Admin with the username:{username} is not found.");

            return _mapper.Map<GetAdminDto>(admin);
        }
    }
}
