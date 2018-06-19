using AutoMapper;
using Housing.Selection.Library.HousingModels;
using Housing.Selection.Library.ServiceHubModels;
using Housing.Selection.Library.ViewModels;

namespace Housing.Selection.Context.Selection
{
    public class MappingProfile : Profile
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

            CreateMap<Address, AddressViewModel>()
                .ForMember(d => d.id, o => o.MapFrom(s => s.AddressId))
                .ForMember(d => d.address1, o => o.MapFrom(s => s.Address1))
                .ForMember(d => d.address2, o => o.MapFrom(s => s.Address2))
                .ForMember(d => d.city, o => o.MapFrom(s => s.City))
                .ForMember(d => d.state, o => o.MapFrom(s => s.State))
                .ForMember(d => d.postalCode, o => o.MapFrom(s => s.PostalCode))
                .ForMember(d => d.country, o => o.MapFrom(s => s.Country));

            CreateMap<Batch, BatchViewModel>()
                .ForMember(d => d.id, o => o.MapFrom(s => s.BatchId))
                .ForMember(d => d.startDate, o => o.MapFrom(s => s.StartDate))
                .ForMember(d => d.endDate, o => o.MapFrom(s => s.EndDate))
                .ForMember(d => d.batchName, o => o.MapFrom(s => s.BatchName))
                .ForMember(d => d.batchOccupancy, o => o.MapFrom(s => s.BatchOccupancy))
                .ForMember(d => d.batchSkill, o => o.MapFrom(s => s.BatchSkill))
                .ForMember(d => d.users, o => o.MapFrom(s => s.Users));

            CreateMap<Name, NameViewModel>()
                .ForMember(d => d.id, o => o.MapFrom(s => s.NameId))
                .ForMember(d => d.first, o => o.MapFrom(s => s.First))
                .ForMember(d => d.middle, o => o.MapFrom(s => s.Middle))
                .ForMember(d => d.last, o => o.MapFrom(s => s.Last));

            CreateMap<Room, RoomViewModel>()
                .ForMember(d => d.id, o => o.MapFrom(s => s.RoomId))
                .ForMember(d => d.location, o => o.MapFrom(s => s.Location))
                .ForMember(d => d.vacancy, o => o.MapFrom(s => s.Vacancy))
                .ForMember(d => d.occupancy, o => o.MapFrom(s => s.Occupancy))
                .ForMember(d => d.gender, o => o.MapFrom(s => s.Gender))
                .ForMember(d => d.address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.users, o => o.MapFrom(s => s.Users));

            CreateMap<User, UserViewModel>()
                .ForMember(d => d.id, o => o.MapFrom(s => s.UserId))
                .ForMember(d => d.location, o => o.MapFrom(s => s.Location))
                .ForMember(d => d.email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.gender, o => o.MapFrom(s => s.Gender))
                .ForMember(d => d.type, o => o.MapFrom(s => s.Type))
                .ForMember(d => d.name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.address, o => o.MapFrom(s => s.Address));
        }
    }
}
