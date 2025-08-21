
using System.Security.Cryptography;
using System.Text;

namespace RealEstate.Shared.Utils;

public static class EncryptionService
{
    public const string EncyptionKeyConfig = "EncryptionKey";
    public static string key = string.Empty;
    private static SHA256 sha = SHA256.Create();

    public static string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = GetKeyHash();

        aes.GenerateIV();

        using var msEncrypt = new MemoryStream();
        msEncrypt.Write(aes.IV, 0, aes.IV.Length);

        using var encryptor = aes.CreateEncryptor();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        csEncrypt.Write(plainBytes, 0, plainBytes.Length);
        csEncrypt.FlushFinalBlock();

        return Convert.ToBase64String(msEncrypt.ToArray());
    }



    public static string Decrypt(string cipherText)
    {
        var combinedBytes = Convert.FromBase64String(cipherText);

        if (combinedBytes.Length < 16)
            throw new ArgumentException("Invalid cipher text.");

        using var aes = Aes.Create();
        aes.Key = GetKeyHash();

        var iv = new byte[16];
        Array.Copy(combinedBytes, 0, iv, 0, 16);
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();

        using var msDecrypt = new MemoryStream(combinedBytes, 16, combinedBytes.Length - 16);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();

    }

    private static byte[] GetKeyHash()
    {
        return sha.ComputeHash(Encoding.UTF8.GetBytes(key));
    }

}