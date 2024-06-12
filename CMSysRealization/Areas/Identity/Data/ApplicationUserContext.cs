using AutoMapper;
using CMSysRealization.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppContext = CMSys.Infrastructure.AppContext;

namespace CMSysRealization.Data;

public class ApplicationUserContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{

    private readonly IMapper _mapper; 
    private readonly AppContext _appContext;
    public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options, IMapper mapper, AppContext appContext)
        : base(options)
    {
        _mapper = mapper ?? throw new ArgumentNullException("AutoMapper is null in app's services");
        _appContext = appContext;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var applicationUsers = _mapper.Map<List<ApplicationUser>>(_appContext.Users.ToList());
        var applicationRoles = _mapper.Map<List<ApplicationRole>>(_appContext.Roles.ToList());
        var applicationUserRoles = _mapper.Map<List<ApplicationUserRole>>(_appContext.UserRoles.ToList());

        builder.Entity<ApplicationUser>().HasData(applicationUsers);
        builder.Entity<ApplicationRole>().HasData(applicationRoles);
        builder.Entity<ApplicationUserRole>().HasData(applicationUserRoles);
    }
}
