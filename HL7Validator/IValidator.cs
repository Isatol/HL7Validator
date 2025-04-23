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
        /// Valida eventos personalizados de un mensaje HL7 mediante enumeradores definidos por el usuario.
        /// </summary>
        /// <param name="customValidators">
        /// Lista de enumeradores que representan campos o componentes específicos a validar.
        /// 
        /// Cada enumerador debe representar un punto del mensaje (por ejemplo: <c>PID_3</c> para el componente <c>PID.3</c>).
        /// 
        /// Es obligatorio que cada elemento tenga el atributo <see cref="HL7Validator.TriggerEvents.Attributes.ValidationTargetAttribute"/>,
        /// el cual debe apuntar a su enumerador complementario que contiene los subcomponentes a validar.
        /// 
        /// Ejemplo:
        /// - Enumerador principal: <c>PID_3</c>
        /// - Atributo: <c>[ValidationTarget(typeof(Segments.PID.Components.PID3ComponentsEnum))]</c>
        /// - Complementario: <c>PID3ComponentsEnum</c> conteniendo: <c>PID_3_1</c>, <c>PID_3_5</c>, etc.
        /// </param>
        /// <returns>Resultado de validación que incluye todos los campos requeridos faltantes encontrados.</returns>

        public HL7ValidationResult Validate(params Enum[] customValidators);
    }
}
