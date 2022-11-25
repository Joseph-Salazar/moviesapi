namespace Application.Security.Entity;

public class AppUserToken
{
    public string Token { get; set; }
    public double ExpireIn { get; set; }
    public DateTime Expiration { get; set; }
}