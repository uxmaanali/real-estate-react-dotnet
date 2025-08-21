using System.Security.Cryptography;
using System.Text;

using RealEstate.Shared.Constants;

namespace RealEstate.Shared.Utils;
public class PasswordHasher
{
    public static string HashPassword(string password, string saltHex)
    {
        if (string.IsNullOrWhiteSpace(saltHex))
            throw new ArgumentException("Salt cannot be null or empty.", nameof(saltHex));

        var salt = Convert.FromHexString(saltHex);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                HashConstants.iterations,
                HashConstants.hashAlgorithmName,
                HashConstants.keySize
            );

        return Convert.ToHexString(hash);
    }

    public static bool VerifyPassword(string password, string storedHashHex, string saltHex)
    {
        var computedHashHex = HashPassword(password, saltHex);

        return CryptographicOperations.FixedTimeEquals(
            Convert.FromHexString(storedHashHex),
            Convert.FromHexString(computedHashHex)
        );
    }

    public static string GeneratePasswordSalt()
    {
        var guid = Guid.NewGuid();
        var guidBytes = guid.ToByteArray();
        var saltBytes = guidBytes.Take(8).ToArray(); // 64-bit = 8 bytes
        return Convert.ToHexString(saltBytes);
    }
}
