using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHapi.Base.Model;
using NHapi.Base.Util;

namespace HL7Validator.Helper
{
    public static class TriggerEvents
    {
        /// <summary>
        /// Obtiene el evento del mensaje HL7
        /// </summary>
        /// <param name="hl7Message">Mensaje HL7</param>
        /// <returns>Devuelve el evento HL7. Ejemplo: ADT^A08</returns>
        public static string GetTriggerEvent(IMessage hl7Message)
        {
            try
            {
                Terser terser = new Terser(hl7Message);
                string messageCode = terser.Get("/MSH-9-1"); // Ej. "ADT"
                string triggerEvent = terser.Get("/MSH-9-2"); // Ej "A08"

                if (!string.IsNullOrEmpty(messageCode) && !string.IsNullOrEmpty(triggerEvent))
                {
                    return $"{messageCode}^{triggerEvent}";
                }
                return "UNKNOWN";
            }
            catch (Exception)
            {
                return "UNKNOWN";
            }

        }
    }
}
