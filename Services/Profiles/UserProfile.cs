using AutoMapper;
using Core.Domain;
using Core.Models;

namespace Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDetails>();
            CreateMap<User, UserShortInfo>();
        }
    }
}
