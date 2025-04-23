using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HL7Validator.Model;
using HL7Validator.TriggerEvents.Events;
using NHapi.Base.Model;

namespace HL7Validator
{
    public interface IValidator
    {
        /// <summary>
        /// Valida un solo evento HL7 basado en los enums expuestos de '<see cref="HL7Validator.TriggerEvents.Events"/>'
        /// </summary>
        /// <typeparam name="TEnum">El tipo de evento</typeparam>
        /// <param name="triggerEvent">El tipo de evento</param>
        /// <returns></returns>
        public HL7ValidationResult Validate();

        /// <summary>
        /// Valida múltiples valores Enum expuestos en '<see cref="HL7Validator.TriggerEvents.Events"/>' de diferentes eventos HL7.
        /// </summary>
        /// <param name="enumList"></param>
        /// <returns></returns>
        //public HL7ValidationResult Validate(IEnumerable<TriggerEventsEnum> enumList);

        /// <summary>
        /// Valida un solo evento 
        /// </summary>
        /// <returns></returns>
        //public HL7ValidationResult Validate();
    }
}
