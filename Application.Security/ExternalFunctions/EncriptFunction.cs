using System.Security.Cryptography;
using System.Text;

namespace Application.Security.ExternalFunctions;

public static class EncriptFunction
{
    private const string PasswordHash = "pass75dc@@avz10";
    private const string SaltKey = "s@@lAvz10";
    private const string ViKey = "@1B2c3D4e5F6g7H8";
    private const int KeySize = 128;

    public static string Encriptar(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return default;

        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        var keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(KeySize / 8);
        var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC, Padding = PaddingMode.Zeros};
        var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(ViKey));

        byte[] cipherTextBytes;

        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
                cryptoStream.Close();
            }

            memoryStream.Close();
        }

        return Convert.ToBase64String(cipherTextBytes);
    }

    public static string Desencriptar(string encryptedText)
    {
        if (string.IsNullOrEmpty(encryptedText))
            return default;

        var cipherTextBytes = Convert.FromBase64String(encryptedText);
        var keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(KeySize / 8);
        var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC, Padding = PaddingMode.None};

        var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(ViKey));
        var memoryStream = new MemoryStream(cipherTextBytes);
        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        var plainTextBytes = new byte[cipherTextBytes.Length];

        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
    }
}