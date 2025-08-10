using System.Security.Cryptography;

namespace RealEstate.Shared.Constants;
public static class HashConstants
{
    public static int keySize = 64;
    public static int iterations = 350000;
    public static HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA512;
}
