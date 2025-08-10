using System.Text.RegularExpressions;

namespace RealEstate.Shared.Utils;

public static class PasswordValidator
{
    // Regex pattern that enforces:
    // - Minimum 8 characters
    // - At least 1 capital letter
    // - At least 1 numeric digit
    private const string PasswordPattern = @"^(?=.*[A-Z])(?=.*\d).{8,}$";

    private static readonly Regex PasswordRegex = new Regex(PasswordPattern, RegexOptions.Compiled);

    public static (bool isValid, string errorMessage) ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return (false, "Password cannot be empty");

        if (password.Length < 8)
            return (false, "Password must be at least 8 characters long");

        if (!PasswordRegex.IsMatch(password))
        {
            var errors = new List<string>();

            if (!password.Any(char.IsUpper))
                errors.Add("at least 1 capital letter");

            if (!password.Any(char.IsDigit))
                errors.Add("at least 1 numeric digit");

            return (false, $"Password must contain {string.Join(" and ", errors)}");
        }

        return (true, null);
    }
}
