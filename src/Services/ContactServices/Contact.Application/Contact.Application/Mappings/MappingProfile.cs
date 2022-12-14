using Contact.Domain.Entities;
using AutoMapper;
using Contact.Application.Features.Users.UserCreate;
using Contact.Application.Features.Users.ContactCreate;

namespace Contact.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateCommand, User>().ForMember(p => p.company, o => o.MapFrom(src => src.company));
            CreateMap<UserUpdateCommand, User>().ForMember(p => p.company, o => o.MapFrom(src => src.company));
            CreateMap<UserUpdateCommand, User>().ForMember(p => p.id, o => o.MapFrom(src => src.uid));
            CreateMap<UserDeleteCommand, User>().ForMember(p => p.id, o => o.MapFrom(src => src.uid));

            CreateMap<ContactCreateCommand, Contact.Domain.Entities.Contact>().ForMember(p => p.email, o => o.MapFrom(src => src.email));
        }

    }
}
