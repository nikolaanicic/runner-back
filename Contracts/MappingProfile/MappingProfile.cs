﻿using AutoMapper;
using Contracts.Dtos.User.Get;
using Contracts.Dtos.User.Post;
using Contracts.Models;

namespace Contracts.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<Admin, GetAdminDto>();
            CreateMap<PostUserDto, User>().ForMember(u => u.Role, op => op.MapFrom(dto => new Role { Rolename = dto.RoleName }));
            CreateMap<PostUserDto, Consumer>();
            CreateMap<PostUserDto, Deliverer>();
        }
    }
}
