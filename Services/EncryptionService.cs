using System.Security.Cryptography;
using System.Text;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class EncryptionService : IEncryptionService
{
    private readonly IRepositoryManager _repositoryManager;

    public EncryptionService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public string Decrypt(string cipherText)
    {
        var key = Convert.FromBase64String(_repositoryManager.Key.GetKeyById(1, false));
        if (key == null)
        {
            throw new InvalidOperationException("Encryption key not found.");
        }
        var keyBytes = Convert.FromBase64String(cipherText);
        var iv = keyBytes.Take(16).ToArray();
        var cipherBytes = keyBytes.Skip(16).ToArray();

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor();
        var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }

    public string Encrypt(string plainText)
    {
        var key = Convert.FromBase64String(_repositoryManager.Key.GetKeyById(1, false));
        using var aes = Aes.Create();
        aes.Key = key;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encryptedPassword = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        var combined = aes.IV.Concat(encryptedPassword).ToArray();
        return Convert.ToBase64String(combined);
    }
}