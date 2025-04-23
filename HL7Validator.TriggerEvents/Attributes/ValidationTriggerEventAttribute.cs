using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.TriggerEvents.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ValidationTriggerEventAttribute : Attribute
    {
        public Type Type { get; }
        public ValidationTriggerEventAttribute(Type validatorEnumType)
        {
            Type = validatorEnumType;
        }
    }
}
