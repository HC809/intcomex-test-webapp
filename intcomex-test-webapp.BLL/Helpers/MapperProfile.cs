using AutoMapper;
using intcomex_test_webapp.BLL.DTOs;
using intcomex_test_webapp.DL.Entities;

namespace intcomex_test_webapp.BLL.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.ContactTypeName, opt => opt.MapFrom(src => src.ContactTypeNoNavigation.ContactTypeName));

            CreateMap<UserDTO, User>();

            CreateMap<ContactType, ContactTypeDTO>();
        }
    }
}
