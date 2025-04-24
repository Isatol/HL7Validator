# HL7Validator

🚑 **Validador modular para mensajes HL7 v2.5**  
Validación extensible por evento, segmento o componente, usando `nHapi` y `Terser`, diseñado para los que requieren control total del contenido HL7.

---

## 📦 Características principales

- 🔍 Soporte completo para HL7 v2.5
- ✅ Validación por evento HL7 (ej: `ADT_A08`, `ADT_A01`)
- 🧩 Validación específica por campo o componente
- 🛠️ Escalable mediante enums y atributos
- ♻️ Limpieza automática del HL7 para un parseo correcto
- 🔧 Validación dinámica usando reflexión y arquitectura basada en atributos

---

## ⚙️ Instalación

Requiere:

- .NET 8 o superior
- Paquete [`NHapi`](https://github.com/nHapiNET/nHapi)

---

## 🧼 Limpieza de mensajes HL7 (Opcional)

Antes de validar, asegúrate de sanitizar el mensaje para que cada segmento esté correctamente separado:

```csharp
hl7Message = HL7Validator.Sanitizer.HL7Sanitizer.Sanitize(hl7Message);
```

Esto normaliza los saltos de línea y reestructura el HL7 si viene en una sola línea.

---

## ✅ Validación automática por evento HL7 (modo estándar)

```csharp
string hl7 = ObtenerMensaje();
hl7 = HL7Sanitizer.Sanitize(hl7);

HL7Validator.IValidator validator = new HL7Validator.Validator(hl7);
HL7Validator.Model.HL7ValidationResult result = validator.Validate();

if (!result.IsValid)
{
    Console.WriteLine("❌ Campos faltantes:");
    result.MissingFields.ForEach(Console.WriteLine);
}
else
{
    Console.WriteLine("✅ Mensaje válido.");
}
```

> El evento HL7 se detecta automáticamente desde el campo `MSH.9`.  
> Por ejemplo: `ADT^A08` se transforma a `ADT_A08` y valida todo lo requerido para ese evento.

---

## 🧪 Modo avanzado: Validación personalizada

Puedes validar solo campos o componentes específicos sin depender del trigger completo:

```csharp
HL7Validator.Model.HL7ValidationResult result = validator.Validate(
    MyCustomEnum.PID_3,
    MyCustomEnum.MSH_7
);
```

Cada enumerador debe representar un punto lógico del mensaje HL7 (por ejemplo: `PID_3` para `PID.3`).

🔹 Es recomendable agregar el atributo `[HL7Validator.TriggerEvents.Attributes.ValidationTarget(typeof(...))]` que indique el tipo complementario que contendrá los componentes del enumerador principal.  
🔹 Si no se especifica, se intentará resolver automáticamente el tipo correspondiente basado en el nombre del enum.

> 📌 **Los nombres de los enums deben seguir el patrón `SEGMENT_CAMPO`, como `PID_3`, `MSH_7`, etc.**

### Ejemplo con atributo:

```csharp
public enum MyCustomEnum
{
    [ValidationTarget(typeof(PID3ComponentsEnum))]
    PID_3
}

public enum PID3ComponentsEnum
{
    PID_3_1,
    PID_3_5
}
```

### Ejemplo sin atributo:

```csharp
public enum MyCustomEnum
{
    MSH_7
}
```

> El validador resolverá automáticamente `MSH_7` y validará en base al estándar de campos requeridos.

---

## 🧩 Convención de nombres para enums

Los nombres de los enums personalizados deben seguir la siguiente estructura:

- `SEGMENT_CAMPO` → como `PID_3`, `PV1_7`, `MSH_9`
- Los subcomponentes deben tener forma `SEGMENT_CAMPO_SUBCAMPO` → como `PID_3_1`, `PID_3_5`
- Si no se especifica el atributo `[ValidationTarget]`, el validador intentará automáticamente asociar `PID_3` con sus componentes por defecto, por lo cual, componentes de PID_3 serán validados conforme a los requeridos del estándar HL7.

---

## 📁 Estructura del proyecto

```
HL7Validator/
├── HL7Validator                     --> namespace HL7Validator
│   ├── Validator.cs
│   └── IValidator.cs
│
├── HL7Validator.Helper             --> namespace HL7Validator.Helper
│   ├── FallbackResolver.cs
│   ├── FieldNames.cs
│   ├── TerserBuilder.cs
│   └── TriggerEvents.cs
│
├── HL7Validator.Model              --> namespace HL7Validator.Model
│   ├── HL7Path.cs
│   └── HL7ValidationResult.cs
│
├── HL7Validator.Program            --> namespace HL7Validator.Program
│   ├── Program.cs
│   └── CustomValidatorEnums/
│       ├── CustomSegments.cs
│       └── CustomSubComponents.cs
│
├── HL7Validator.Sanitizer          --> namespace HL7Validator.Sanitizer
│   └── HL7Sanitizer.cs
│
├── HL7Validator.Segments           --> namespace HL7Validator.Segments
│   ├── Interface/
│   │   ├── IHL7Validator.cs
│   │   └── IRequiredMetadata.cs
│   ├── MSH/
│   │   ├── MSHSegmentsEnum.cs
│   │   └── Components/
│   │       ├── MSH11ComponentsEnum.cs
│   │       ├── MSH12ComponentsEnum.cs
│   │       ├── MSH7ComponentsEnum.cs
│   │       └── MSH9ComponentsEnum.cs
│   ├── PID/
│   │   ├── PIDSegmentsEnum.cs
│   │   └── Components/
│   │       ├── PID3ComponentsEnum.cs
│   │       ├── PID5ComponentsEnum.cs
│   │       └── PID7ComponentsEnum.cs
│   ├── PV1/
│   │   ├── PV1SegmentsEnum.cs
│   │   └── Components/
│   │       ├── PV1_3ComponentsEnum.cs
│   │       ├── PV1_7ComponentsEnum.cs
│   │       └── PV1_19ComponentsEnum.cs
│   └── Validator/
│       ├── GenericValidator.cs
│       └── GenericMetadata.cs
│
└── HL7Validator.TriggerEvents      --> namespace HL7Validator.TriggerEvents
    ├── Attributes/
    │   ├── ValidationTargetAttribute.cs
    │   └── ValidationTriggerEventAttribute.cs
    ├── Events/
    │   ├── TriggerEventsEnum.cs
    │   └── TriggerEventCollection/
    │       ├── ADT_A01EventEnum.cs
    │       └── ADT_A08EventEnum.cs
    ├── Interface/
    │   ├── ITriggerEventValidator.cs
    │   └── IValidatorEvents.cs
    └── Validator/
        ├── ValidatorEvent.cs
        └── ValidatorResolver.cs

```

---

## 📘 Ejemplo de evento: `ADT_A08`

Valida:

- Segmentos requeridos: `PID`, `MSH`, `PV1`
- Componentes requeridos: `PID.3.1`, `PID.5.1`, `MSH.9.1`, `PV1.3.1`, etc.

---

## 🔭 Por hacer

- [ ] Exportar resultados en JSON
- [ ] Integración con logging o notificaciones