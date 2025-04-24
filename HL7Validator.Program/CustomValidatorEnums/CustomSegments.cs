using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.Program.CustomValidatorEnums
{
    public enum CustomSegments
    {
        [HL7Validator.TriggerEvents.Attributes.ValidationTarget(typeof(CustomValidatorEnums.CustomSubComponents))]
        PID_3,
        MSH_7
    }
}
