# HL7Validator

🚑 Validación modular, dinámica y escalable de mensajes HL7 versión 2.5, compatible con nHapi y basado en Terser.

---

## 📦 Características principales

- ✅ Soporta validación de segmentos y componentes requeridos por evento (ej. ADT_A08)
- ✅ Modular: cada evento tiene su propia definición de campos obligatorios
- ✅ Escalable: fácilmente extensible con nuevos eventos y validadores
- ✅ Plug & Play: valida un mensaje HL7 completo con una sola línea
- ✅ Limpieza automática del mensaje para evitar errores de formato

---

## ⚙️ Cómo usar

```csharp
string hl7Message = ObtenerMensaje();
hl7Message = Hl7Sanitizer.Sanitize(hl7Message);

var validator = new HL7Validator.Validator(hl7Message);
var result = validator.Validate();

if (!result.IsValid)
{
    Console.WriteLine("❌ Faltan campos:");
    result.MissingFields.ForEach(Console.WriteLine);
}
else
{
    Console.WriteLine("✅ Mensaje HL7 válido.");
}