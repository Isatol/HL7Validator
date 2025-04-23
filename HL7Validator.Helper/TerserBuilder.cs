using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.Helper
{
    public class TerserBuilder
    {
        public static string BuildTerserPath(string logicalPath)
        {
            var parts = logicalPath.Split('.');
            if (parts.Length == 2)
                return $"/{parts[0]}(0)-{parts[1]}(0)";
            if (parts.Length == 3)
                return $"/{parts[0]}(0)-{parts[1]}(0)-{parts[2]}";
            return logicalPath;
        }
    }
}
