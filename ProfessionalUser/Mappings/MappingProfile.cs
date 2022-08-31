using AutoMapper;
using Entities.Concrete;
using Entities.Dtos.User;
namespace Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDetailDto>();
            CreateMap<UserDetailDto, User>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();


            CreateMap<User, UserAddDto>();
            CreateMap<UserAddDto, User>();

            CreateMap<UserDto, UserUpdateDto>().ReverseMap();

            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
