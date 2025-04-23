using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.Segments.PV1
{
    /// <summary>
    /// Enumera los campos requeridos del segmento PV1
    /// </summary>
    public enum PV1SegmentsEnum
    {
        PV1_2,    // Patient Class
        PV1_3,    // Assigned Patient Location
        PV1_7,    // Attending Doctor
        //PV1_10,   // Hospital Service
        PV1_19    // Visit Number
    }
}
