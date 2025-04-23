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
        /// Valida un solo evento HL7 basado en los enums expuestos de '<see cref="HL7Validator.TriggerEvents.Events"/>'
        /// </summary>
        /// <typeparam name="TEnum">El tipo de evento</typeparam>
        /// <param name="triggerEvent">El tipo de evento</param>
        /// <returns></returns>
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
        /// Valida un solo evento HL7
        /// </summary>
        /// <returns></returns>
        public HL7ValidationResult Validate()
        {
            return this.Validate(_triggerEvent);
        }


    }
}
