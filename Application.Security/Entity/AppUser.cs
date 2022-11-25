using System.Security.Claims;
using Infrastructure.CrossCutting.Enum;

namespace Application.Security.Entity;

public class AppUser
{
    public AppUser() {}
    public AppUser(ClaimsPrincipal claimsPrincipal)
    {
        PermissionList = new List<string>();

        if (claimsPrincipal != null)
        {
            Id = Convert.ToInt32(claimsPrincipal.FindFirst(ClaimType.Id)?.Value);
            FullName = claimsPrincipal.FindFirst(ClaimType.FullName)?.Value;
            UserName = claimsPrincipal.FindFirst(ClaimType.UserName)?.Value;
        }
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public IEnumerable<string> PermissionList { get; set; }
}