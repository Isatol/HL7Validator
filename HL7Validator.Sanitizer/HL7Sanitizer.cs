using System.Text.RegularExpressions;

namespace HL7Validator.Sanitizer
{
    public static class HL7Sanitizer
    {
        public static string Sanitize(string hl7Message)
        {
            hl7Message = hl7Message.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
            hl7Message = Regex.Replace(hl7Message, @"(\s+)([a-zA-Z0-9]{3})[|]", $"$1\r\n$2|");

            return hl7Message;
        }
    }
}
