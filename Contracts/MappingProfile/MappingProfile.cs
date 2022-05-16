using AutoMapper;
using Contracts.Dtos;
using Contracts.Dtos.User.Get;
using Contracts.Models;

namespace Contracts.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<Admin, GetAdminDto>();
        }

    }
}
