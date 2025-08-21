using RealEstate.Shared.Utils;

namespace RealEstate.Utils;

public static class EncryptionSetup
{
    public static IServiceCollection AddEncryption(this IServiceCollection services, IConfiguration configuration)
    {
        var encryptionKey = configuration.GetValue<string>(EncryptionService.EncyptionKeyConfig);
        if (string.IsNullOrEmpty(encryptionKey))
        {
            throw new Exception("Encryption key is not valid.");
        }
        EncryptionService.key = encryptionKey!;

        return services;
    }
}
