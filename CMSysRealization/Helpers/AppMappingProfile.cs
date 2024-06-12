using AutoMapper;
using CMSys.Core.Entities.Membership;
using CMSysRealization.Areas.Identity.Data;

namespace CMSysRealization.Helpers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() 
        {
            CreateMap<User, ApplicationUser>();
            CreateMap<Role, ApplicationRole>();
            CreateMap<UserRole, ApplicationUserRole>();
        }
    }
}
