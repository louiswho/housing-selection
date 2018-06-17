using AutoMapper;
using Housing.Selection.Library.HousingModels;

namespace Housing.Selection.Library.ViewModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressViewModel>();
            CreateMap<Batch, BatchViewModel>();
            CreateMap<Name, NameViewModel>();
            CreateMap<Room, RoomViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}
