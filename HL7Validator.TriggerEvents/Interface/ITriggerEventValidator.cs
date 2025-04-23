using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HL7Validator.Model;
using NHapi.Base.Model;

namespace HL7Validator.TriggerEvents.Interface
{
    /// <summary>
    /// Interfaz que pretende implementar validar un evento HL7
    /// </summary>
    public interface ITriggerEventValidator
    {
        /// <summary>
        /// Valida el evento de un mensaje HL7
        /// </summary>
        /// <param name="hl7Message"></param>
        /// <returns></returns>
        HL7ValidationResult Validate(IMessage hl7Message);
    }
}
