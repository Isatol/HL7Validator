using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHapi.Base.Model;
using NHapi.Base.Util;

namespace HL7Validator.Segments.Interface
{    
    public interface IHL7Validator
    {
        /// <summary>
        /// Valida que todos los segmentos Requeridos del mensaje HL7 estén incluídos en el mensaje original
        /// </summary>
        /// <param name="hl7Message"></param>
        /// <returns></returns>
        public HL7Validator.Model.HL7ValidationResult Validate(IMessage hl7Message);
    }
}
