using AutoMapper;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;

namespace Housing.Selection.Context.Selection
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ApiUser>()
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Gender, o=> o.MapFrom(s => s.Gender));

            CreateMap<Room, ApiRoom>()
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address));

            CreateMap<Address, ApiAddress>();

            CreateMap<Name, ApiName>();
        }
    }
}
