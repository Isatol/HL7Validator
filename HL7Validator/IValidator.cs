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
        /// Valida un mensaje HL7 completo detectando automáticamente el evento HL7 desde el campo <c>MSH.9</c>.
        ///
        /// Este método analiza el valor del campo <c>MSH.9</c> (Message Type + Trigger Event),
        /// lo transforma a su correspondiente enumerador de evento (por ejemplo: <c>ADT_A08</c>),
        /// y ejecuta todas las validaciones de segmentos y componentes requeridos asociados a dicho evento.
        ///
        /// Es la forma estándar y recomendada para validar un mensaje HL7 completo según las reglas oficiales del evento.
        ///
        /// Ejemplo:
        /// - Si el mensaje contiene <c>MSH.9 = ADT^A08</c>, se ejecutarán todas las validaciones del evento <c>ADT_A08</c>.
        /// </summary>
        /// <returns>
        /// Objeto <see cref="HL7ValidationResult"/> con los campos faltantes encontrados durante la validación.
        /// </returns>
        public HL7ValidationResult Validate();

        /// <summary>
        /// Valida campos o componentes personalizados de un mensaje HL7 mediante enumeradores definidos por el usuario.
        ///
        /// Cada enumerador representa un punto lógico del mensaje HL7, como <c>PID_3</c> para referirse al componente <c>PID.3</c>.
        /// 
        /// 🔹 Es recomendable agregar el atributo <see cref="ValidationTargetAttribute"/> que indique el tipo complementario a validar.
        /// 🔹 Si el atributo no está presente, se intentará resolver el tipo automáticamente mediante una convención interna.
        ///
        /// 📌 Importante: los enumeradores deben estar nombrados con guiones bajos (por ejemplo: <c>PID_3</c>, <c>MSH_7</c>),
        /// ya que esta estructura es utilizada para inferir el validador correspondiente en caso de no tener atributo.
        ///
        /// Ejemplo con atributo:
        /// - Enum: <c>PID_3</c>
        /// - Atributo: <c>[ValidationTarget(typeof(PID3ComponentsEnum))]</c>
        /// - Complementario: <c>PID3ComponentsEnum</c> con subcomponentes como <c>PID_3_1</c>, <c>PID_3_5</c>.
        ///
        /// Ejemplo sin atributo:
        /// - Enum: <c>MSH_7</c>
        /// - El validador correspondiente se resolverá automáticamente si existe uno disponible.
        /// </summary>
        /// <param name="customValidators">Enumeradores personalizados a validar</param>
        /// <returns>Resultado de validación con los campos faltantes detectados</returns>

        public HL7ValidationResult Validate(params Enum[] customEnum);
    }
}
