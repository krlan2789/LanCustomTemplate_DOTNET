using System.Security.Cryptography;
using System.Text;

namespace CustomTemplate_CA_API.Core.Helper;

public static class HashingHelper
{
    public static string Hash(this string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }

    public static bool VerifyHashed(this string tohash, string hashed)
    {
        return tohash.Hash() == hashed;
    }
}