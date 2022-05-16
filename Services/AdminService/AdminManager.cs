using AutoMapper;
using Contracts.Dtos;
using Contracts.Dtos.User.Get;
using Contracts.Exceptions;
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
    public class AdminManager : IAdminService
    {

        private IRepositoryManager _repo;
        private IMapper _mapper;

        public AdminManager(IRepositoryManager repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }


        public async Task<GetAdminDto> GetByUsername(string username)
        {
            Admin admin = await _repo.Admins.GetAsync(username, false);
            if (admin == null)
                throw new NotFoundException($"Admin with the username:{username} is not found.");

            return _mapper.Map<GetAdminDto>(admin);
        }
    }
}
