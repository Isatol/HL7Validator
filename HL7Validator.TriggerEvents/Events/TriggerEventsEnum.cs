using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HL7Validator.TriggerEvents.Attributes;
using HL7Validator.TriggerEvents.Events.TriggerEventCollection;

namespace HL7Validator.TriggerEvents.Events
{
    public enum TriggerEventsEnum
    {
        [ValidationTriggerEvent(typeof(ADT_A08EventEnum))]
        ADT_A08,
        
        ADT_A01
    }
}
