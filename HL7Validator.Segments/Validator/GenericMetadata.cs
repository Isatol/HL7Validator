using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.Segments.Validator
{
    public class GenericMetadata<TEnum> : Interface.IRequiredMetadata<TEnum> where TEnum : Enum
    {
        private Dictionary<TEnum, string> _componentsPaths;

        public GenericMetadata()
        {
            var enums = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            _componentsPaths = enums.ToDictionary(e => e, e => e.ToString().Replace("_", "."));
        }

        public string GetLogicalPath(TEnum requiredSegmentsEnum)
        {
            return _componentsPaths.TryGetValue(requiredSegmentsEnum, out var path) ? path : string.Empty;
        }

        public IEnumerable<TEnum> GetRequiredSegments()
        {
            return _componentsPaths.Keys;
        }
    }
}
