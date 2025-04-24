# 🧭 HL7Validator – Conventions & Guidelines

Esta guía establece las convenciones utilizadas dentro del proyecto `HL7Validator` para asegurar una estructura uniforme, mantenible y escalable.

---

## 📦 Estructura general de proyectos

Cada responsabilidad tiene su propio proyecto:

| Proyecto                  | Responsabilidad principal                         |
|--------------------------|----------------------------------------------------|
| `HL7Validator`           | Orquestador general y punto de entrada             |
| `HL7Validator.Segments`  | Segmentos (ej. `PID`, `MSH`, `PV1`) y validadores  |
| `HL7Validator.TriggerEvents` | Validadores por evento HL7 (`ADT_A08`, `OML_O21`) |
| `HL7Validator.Helper`    | Utilidades: `TerserBuilder`, `FieldNames`, etc.    |
| `HL7Validator.Model`     | Entidades comunes como `HL7Path` y `ValidationResult` |
| `HL7Validator.Sanitizer` | Limpieza del mensaje HL7                           |
| `HL7Validator.Program`   | Consola de pruebas o demo                          |

---

## 📘 Convenciones para enums

### ✔️ Formato de enums por segmento

```csharp
public enum PIDSegmentsEnum
{
    PID_3,
    PID_5,
    PID_7,
    PID_8
}
```

- Usar `_` en lugar de `.`
- Agrupar enums por segmento en su carpeta correspondiente:  
  `Segments/PID/PIDSegmentsEnum.cs`

### ✔️ Formato para subcomponentes

```csharp
public enum PID3ComponentsEnum
{
    PID_3_1,
    PID_3_5
}
```

- Deben ir en la subcarpeta `Segments/PID/Components`
- El nombre del enum debe coincidir con el segmento del que proviene (ej. `PID3ComponentsEnum`)

---

## 🏷️ Atributos obligatorios

### `[ValidationTarget]`

Usado para vincular un campo general con su enum complementario de componentes.

```csharp
[ValidationTarget(typeof(PID3ComponentsEnum))]
PID_3
```

### `[ValidationTriggerEvent]`

Apunta a un enum que agrupa los segmentos/componentes de un trigger (ej. ADT^A08)

```csharp
[ValidationTriggerEvent(typeof(ADT_A08EventEnum))]
public enum TriggerEventsEnum
{
    ADT_A08
}
```

---

## 🛠️ Estructura de carpetas

```
Segments/
├── PID/
│   ├── PIDSegmentsEnum.cs
│   └── Components/
│       └── PID3ComponentsEnum.cs
├── MSH/
│   ├── MSHSegmentsEnum.cs
│   └── Components/
│       └── MSH9ComponentsEnum.cs
```

---

## 🧪 Tests y pruebas manuales

El proyecto `HL7Validator.Program` debe contener:

- Mensajes de ejemplo (`ADT^A01`, `ADT^A08`)
- Casos de prueba para:
  - Validaciones completas
  - Validaciones personalizadas con `Validate(params Enum[])`

---

## 🧼 Sanitización del mensaje

Siempre aplicar `HL7Sanitizer.Sanitize(hl7Message)` antes de hacer `Parse`.  
Esto evita errores comunes por:

- Saltos de línea mal formateados
- HL7 en una sola línea

---

## 💡 Buenas prácticas

- ✅ Prefiere usar `GenericValidator<TEnum>` cuando sea posible
- ✅ Agrega `ValidationTarget` en los enums para mayor claridad
- ❌ No mezcles lógica de segmentos con lógica de eventos
- ✅ Expón solo lo necesario en `HL7Validator` (no referencias cruzadas innecesarias)

---

## ✍️ Autor

**Isaías Orozco**  