namespace Infrastructure.Email;

public interface IEmailHelper
{
    Task Send(Email email);
}