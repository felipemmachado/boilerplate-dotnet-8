using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Users.Queries.GetUsers
{
    public record struct UserDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public DateTime? FirstAccess { get; set; }
        public DateTime? LastAccess { get; set; }
        public DateTime? DisabledAt { get; set; }
        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.FirstAccess, opt => opt.MapFrom(s => s.FirstAccess))
                .ForMember(d => d.LastAccess, opt => opt.MapFrom(s => s.LastAccess))
                .ForMember(d => d.DisabledAt, opt => opt.MapFrom(s => s.DisabledAt))
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.Roles))
                ;
        }
    }
}
