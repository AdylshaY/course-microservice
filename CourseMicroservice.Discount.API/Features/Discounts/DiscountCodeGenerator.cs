using System.Security.Cryptography;

namespace CourseMicroservice.Discount.API.Features.Discounts
{
    public class DiscountCodeGenerator
    {
        private const string Allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string Generate(int length = 10)
        {
            if (length <= 0) throw new ArgumentException("Length must be greater than zero.", nameof(length));

            char[] buffer = new char[length];

            for (int i = 0; i < length; i++)
            {
                var random = RandomNumberGenerator.GetInt32(Allowed.Length);
                buffer[i] = Allowed[random];
            }

            return new string(buffer);
        }
    }
}
