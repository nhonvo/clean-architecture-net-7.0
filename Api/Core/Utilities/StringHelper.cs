namespace Api.Core.Utilities
{
    public static class StringHelper
    {
        public static string Hash(this string inputString)
                    => BCrypt.Net.BCrypt.HashPassword(inputString);
    }
}