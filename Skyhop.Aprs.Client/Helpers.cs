using System;
using System.Text.RegularExpressions;

namespace Skyhop.Aprs.Client
{
    public static class Helpers
    {
        public static bool VerifyCallsign(this string callsign)
        {
            // The callsign is validated by the following regex see 
            // http://www.aprs-is.net/Connecting.aspx for more info

            var match = Regex.Match(callsign, "(?>[1-9][A-Z][A-Z]?[0-9]|[A-Z][2-9A-Z]?[0-9])[A-Z]{1,4}");

            return match.Success;
        }

        public static bool VerifyCallsignPasswordCombination(string callsign, string password)
        {
            return callsign.VerifyCallsign() && VerifyPassword(callsign, password);
        }

        public static bool VerifyPassword(string callsign, string password)
        {
            if (password == "-1") return true;

            return GeneratePassword(callsign).ToString() == password;
        }

        // See https://gist.github.com/tomfanning/29d9b1d8577c1393d8a8f1f313e6cf97 for the original source of the code below. (To generate a password for an APRS client)
        public static int GeneratePassword(string callsign)
        {
            string upper = callsign.ToUpper();
            string main = upper.Split('-')[0];

            int hash = 0x73e2;

            char[] chars = main.ToCharArray();

            while (chars.Length != 0)
            {
                char? one = Shift(ref chars);
                char? two = Shift(ref chars);
                hash = hash ^ one.Value << 8;

                if (two != null)
                {
                    hash = hash ^ two.Value;
                }
            }

            int result = hash & 0x7fff;

            return result;
        }

        static char? Shift(ref char[] chars)
        {
            if (chars.Length == 0)
                return null;

            char result = chars[0];

            char[] newarr = new char[chars.Length - 1];

            for (int i = 1; i < chars.Length; i++)
            {
                newarr[i - 1] = chars[i];
            }

            chars = newarr;

            return result;
        }
    }
}
