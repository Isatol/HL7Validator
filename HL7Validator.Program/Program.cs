using System.Text.RegularExpressions;
using HL7Validator.TriggerEvents.Events.TriggerEventCollection;

string hl7Message = "MSH|^~\\&|ECH|DHO0||DHO0|20250423115252||ADT^A01|20036|P|2.5|||AL|||UTF-8    EVN|A01|20250423115252    PID|1|1000000036^^^^PT|1000000036^^^^PT~111111111111111000^^^^NI~469775^^^^PI||PIZANA^EVELYN^^^Señor|ROMERO|19791010010000|H|||PRUEBA^Agrícola Oriental^Iztacalco^CIUDAD DE MÉXICO^08500^MÉXICO|MX|5666669764^^CP~^NET^X.400^evepiro72@gmail.com||ESPAÑOL||||PIRE791010XXX|||||||||MEXICANA    PV1||I|DHTHA10H^DHHA1001^DHHC1001||||||2600000000^ABUNDIS^ALBERTO|||||||||I|53^^^^^^^^^DHMHOADD|||||||||||||||||||||||||202504081259  ";

hl7Message = HL7Validator.Sanitizer.HL7Sanitizer.Sanitize(hl7Message);

HL7Validator.IValidator validator = new HL7Validator.Validator(hl7Message);

HL7Validator.Model.HL7ValidationResult result = validator.Validate();