using Application.Security.Entity;
using AutoMapper;
using Domain.MainModule.IUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Core;

public class BaseAppService
{
    protected readonly IConfiguration Configuration;
    public readonly AppUser CurrentUser;
    protected readonly IMapper Mapper;
    protected readonly IServiceProvider ServiceProvider;
    protected readonly IUnitOfWork UnitOfWork;

    public BaseAppService(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        UnitOfWork = serviceProvider.GetService<IUnitOfWork>();
        Mapper = serviceProvider.GetService<IMapper>();
        Configuration = serviceProvider.GetService<IConfiguration>();
        var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

        CurrentUser = new AppUser(httpContextAccessor?.HttpContext?.User);
    }

    public bool CheckPermission(string permissionCode)
    {
        return CurrentUser.PermissionList.Contains(permissionCode);
    }
}