using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.TriggerEvents.Interface
{
    public interface IValidatorEvents<TEnum> : ITriggerEventValidator where TEnum : Enum
    {

    }
}
