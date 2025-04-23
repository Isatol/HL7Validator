using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HL7Validator.Model;
using HL7Validator.TriggerEvents.Attributes;
using HL7Validator.TriggerEvents.Interface;
using NHapi.Base.Model;

namespace HL7Validator.TriggerEvents.Validator
{
    public class ValidatorEvent<TEnum> : IValidatorEvents<TEnum> where TEnum : Enum
    {
        private readonly List<HL7Validator.Segments.Interface.IHL7Validator> _validators;

        public ValidatorEvent()
        {
            _validators = Validator.ValidatorResolver.ResolveAll<TEnum>().ToList();
        }

        public HL7ValidationResult Validate(IMessage hl7Message)
        {
            HL7ValidationResult result = new HL7ValidationResult();
            foreach (var validator in _validators)
            {
                var partialValidation = validator.Validate(hl7Message);
                result.MissingFields.AddRange(partialValidation.MissingFields);
            }
            return result;
        }
    }
}
