using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.TriggerEvents.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ValidationTargetAttribute : Attribute
    {
        public Type EnumType { get; }
        public ValidationTargetAttribute(Type enumType)
        {
            EnumType = enumType;
        }
    }
}
