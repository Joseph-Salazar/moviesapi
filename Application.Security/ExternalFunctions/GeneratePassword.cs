using System.Text;

namespace Application.Security.ExternalFunctions;

public static class GeneratePassword
{
    public static string RandomPassword(int longitud)
    {
        var caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var res = new StringBuilder();
        var rnd = new Random();
        while (0 < longitud--) res.Append(caracteres[rnd.Next(caracteres.Length)]);
        return res.ToString();
    }
}