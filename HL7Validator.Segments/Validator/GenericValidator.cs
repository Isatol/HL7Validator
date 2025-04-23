using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HL7Validator.Helper;
using HL7Validator.Model;
using HL7Validator.Segments.Interface;
using NHapi.Base.Model;
using NHapi.Base.Util;

namespace HL7Validator.Segments.Validator
{
    public class GenericValidator<TEnum> : IHL7Validator where TEnum : Enum
    {
        private readonly IRequiredMetadata<TEnum> _requiredMetadata;

        public GenericValidator() : this(new GenericMetadata<TEnum>()) { }

        public GenericValidator(IRequiredMetadata<TEnum> requiredMetadata)
        {
            _requiredMetadata = requiredMetadata;
        }

        public HL7ValidationResult Validate(IMessage hl7Message)
        {
            HL7ValidationResult result = new HL7ValidationResult();
            Terser terser = new Terser(hl7Message);
            FieldNames helper = new FieldNames(hl7Message);

            foreach (TEnum component in _requiredMetadata.GetRequiredSegments())
            {
                string logicalPath = _requiredMetadata.GetLogicalPath(component);
                string terserBuild = TerserBuilder.BuildTerserPath(logicalPath);
                //string terserBuild = "/" + logicalPath.Replace(".", "-");
                string terserValue = terser.Get(terserBuild);

                if (string.IsNullOrEmpty(terserValue))
                {
                    result.MissingFields.Add(logicalPath);
                }
            }
            return result;
        }
    }
}
