using System.Reflection;
using System.Text.RegularExpressions;
using HL7Validator.Model;
using HL7Validator.Segments.Interface;
using HL7Validator.Segments.Validator;
using HL7Validator.TriggerEvents.Attributes;
using HL7Validator.TriggerEvents.Events;
using HL7Validator.TriggerEvents.Interface;
using NHapi.Base.Model;
using NHapi.Base.Parser;

namespace HL7Validator
{
    public class Validator : IValidator
    {
        private readonly IMessage _message;
        private readonly TriggerEvents.Events.TriggerEventsEnum _triggerEvent;

        public Validator(string hl7Message)
        {
            PipeParser parser = new PipeParser();
            _message = parser.Parse(hl7Message);
            string triggerEvent = HL7Validator.Helper.TriggerEvents.GetTriggerEvent(_message);
            _triggerEvent = (TriggerEventsEnum)Enum.Parse(typeof(TriggerEventsEnum), triggerEvent.Replace("^", "_"));
        }

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
        private HL7ValidationResult Validate(TriggerEventsEnum triggerEventEnum)
        {
            HL7ValidationResult result = new HL7ValidationResult();
            var attr = triggerEventEnum.GetType().GetMember(triggerEventEnum.ToString()).FirstOrDefault()?.GetCustomAttribute<ValidationTriggerEventAttribute>();
            if (attr == null) throw new InvalidOperationException($"TriggerEventEnum '{triggerEventEnum}' is missing [ValidationTriggerEvent]");

            var validatorEventType = typeof(TriggerEvents.Validator.ValidatorEvent<>).MakeGenericType(attr.Type);
            var validatorInstance = Activator.CreateInstance(validatorEventType) as HL7Validator.TriggerEvents.Interface.ITriggerEventValidator;

            if (validatorInstance == null) throw new NullReferenceException($"Failed to create validator instance for event '{triggerEventEnum}'");

            result = validatorInstance.Validate(_message);
            return result;
        }

        /// <summary>
        /// Valida el mens
        /// </summary>
        /// <typeparam name="TEnum">El tipo de evento</typeparam>
        /// <param name="triggerEvent">El tipo de evento</param>
        /// <returns></returns>
        public HL7ValidationResult Validate()
        {
            return this.Validate(_triggerEvent);
        }

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
        public HL7ValidationResult Validate(params Enum[] customEnums)
        {
            HL7ValidationResult result = new HL7ValidationResult();            
            foreach (var customEnum in customEnums)
            {
                var enumType = customEnum.GetType();
                var enumMember = enumType.GetMember(customEnum.ToString()).FirstOrDefault();

                // Se intenta obtener el atributo ValidationTarget                

                var attr = enumMember?.GetCustomAttribute<ValidationTargetAttribute>();

                Type? defaultComponents = null;

                if(attr == null)
                {
                    defaultComponents = HL7Validator.Helper.FallbackResolver.Resolve(customEnum);
                    if(defaultComponents == null) throw new InvalidOperationException($"Enum '{customEnum}' no tiene [ValidationTarget] ni fallback registrado.");
                }

                Type targetComponent = attr?.EnumType ?? defaultComponents!;

                var validatorType = typeof(GenericValidator<>).MakeGenericType(targetComponent);

                var validator = Activator.CreateInstance(validatorType) as IHL7Validator;

                if(validator == null) throw new Exception($"Validator for '{customEnum}' could not be created.");

                var partialResult = validator.Validate(_message);
                result.MissingFields.AddRange(partialResult.MissingFields);
            }

            return result;
        }
    }
}
