using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TemplateDDD.Shared.Constants;

namespace TemplateDDD.Shared.Common
{
    public static class Utils
    {
        public static Guid UniqueId()
        {
            return Guid.NewGuid();
        }

        public static string PlainGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static DateTime ServerDate()
        {
            return DateTime.UtcNow;
        }

        public static string GetFormattedDate(string format = DateFormatConstants.FULL)
        {
            return ServerDate().ToString(format);
        }

        public static bool IsInt(string value, out int result)
        {
            return int.TryParse(value, out result);
        }

        public static bool GetBooleanValue(string value, bool defaultValue = false)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            int result = 0;

            int.TryParse(value, out result);

            return result == 1;
        }

        public static int GetIntValue(string value, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            int result = 0;

            int.TryParse(value, out result);

            return result;
        }

        public static long GetLongValue(string value, long defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            long result = 0;

            long.TryParse(value, out result);

            return result;
        }

        public static double GetDoubleValue(string value, long defaultValue = 0)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            double result = 0;

            double.TryParse(value, out result);

            return result;
        }

        public static string GenerateRandomCharacters(int length = 8)
        {
            Random random = new Random();

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz#@&!";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        public static bool IsUsernameEmailAddress(string username)
        {
            return username.Trim().IndexOf('@') >= 1;
        }

        public static bool IsUsernameAPhoneNumber(string username)
        {
            return (Regex.IsMatch(username.Trim(), @"^\+?\d+$"));
        }

        public static string NormalizePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return null;
            }

            var result = phoneNumber.StartsWith("+");
            if (result)
            {
                return phoneNumber;
            }

            result = phoneNumber.StartsWith("0");
            var newPhoneNumber = string.Empty;
            if (result)
            {
                newPhoneNumber = phoneNumber.Substring(1);
            }
            else
            {
                newPhoneNumber = phoneNumber;
            }

            return GeneralConstants.DEFAULT_COUNTRY_CODE + newPhoneNumber;
        }

        public static bool IsUnsignedInteger(string username)
        {
            return (Regex.IsMatch(username.Trim(), @"^\+?\d+$"));
        }

        public static string GenerateRandomDigits(string[] allowedDigits, int numberOfDigits = 6)
        {
            string randomDigits = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < numberOfDigits; i++)
            {
                int p = rand.Next(0, allowedDigits.Length);

                sTempChars = allowedDigits[rand.Next(0, allowedDigits.Length)];

                randomDigits += sTempChars;
            }

            return randomDigits;
        }

        public static string ComputeHMACSHA512(string text, string secretKey)
        {
            var hash = new StringBuilder(); ;
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}