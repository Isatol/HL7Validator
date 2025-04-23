using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HL7Validator.Segments.Interface;
using HL7Validator.Segments.Validator;
using HL7Validator.TriggerEvents.Attributes;

namespace HL7Validator.TriggerEvents.Validator
{
    public static class ValidatorResolver
    {
        public static IEnumerable<IHL7Validator> ResolveAll<TENum>() where TENum : Enum
        {
            foreach (TENum val in Enum.GetValues(typeof(TENum)))
            {
                var enumMember = typeof(TENum).GetMember(val.ToString()).FirstOrDefault();
                ValidationTargetAttribute attr = enumMember?.GetCustomAttributes(typeof(ValidationTargetAttribute), false).FirstOrDefault() as ValidationTargetAttribute;
                if(attr != null)
                {
                    Type validatorType = typeof(GenericValidator<>).MakeGenericType(attr.EnumType);
                    if(Activator.CreateInstance(validatorType) is IHL7Validator validator)
                    {
                        yield return validator;
                    }
                }
            }
        }
    }
}
