using AutoMapper;
using Contracts.Dtos.Order.Get;
using Contracts.Dtos.Product.Get;
using Contracts.Dtos.Product.Post;
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
            CreateMap<PostProductDto, Product>();
            CreateMap<Product, GetProductDto>();
            CreateMap<Order, GetOrderDto>()
                .ForMember(dto => dto.Consumer, op => op.MapFrom(o => o.Consumer.Username))
                .ForMember(dto => dto.Deliverer, op => op.MapFrom(o => o.Deliverer.Username ?? string.Empty));
        
        }
    }
}
