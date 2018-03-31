using System;
using System.Text.RegularExpressions;

namespace Boerman.AprsClient
{
    public static class Helpers
    {
        public static bool CheckValiditiyAsCallsign(this string callsign) {
            // The callsign is validated by the following regex see 
            // http://www.aprs-is.net/Connecting.aspx for more info

            var match = Regex.Match(callsign, "(?>[1-9][A-Z][A-Z]?+[0-9]|[A-Z][2-9A-Z]?[0-9])[A-Z]{1,4}+");

            return match.Success;
        }
    }
}
