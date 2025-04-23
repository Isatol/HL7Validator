using System.Text.RegularExpressions;
using HL7Validator.TriggerEvents.Events.TriggerEventCollection;

string hl7Message = "MSH|^~\\&|ECH||||20250423115252||ADT^A08|20035|P|2.5|||AL|||UTF-8    PID|1|1000000036^^^^PT|1000000036^^^^PT~111111111111111000^^^^NI||PIZANA^EVELYN^^^Señor|ROMERO|19791010010000|H|||PRUEBA^Agrícola Oriental^Iztacalco^CIUDAD DE MÉXICO^08500^MÉXICO|MX|5666669764^^CP~^NET^X.400^evepiro72@gmail.com||ESPAÑOL||||PIRE791010XXX|||||||||MEXICANA    PV1||O    AL1|1|MA  ";

hl7Message = HL7Validator.Sanitizer.HL7Sanitizer.Sanitize(hl7Message);

HL7Validator.IValidator validator = new HL7Validator.Validator(hl7Message);

HL7Validator.Model.HL7ValidationResult result = validator.Validate();