using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.Segments.MSH
{
    /// <summary>
    /// Enumera los campos requeridos del segmento MSH
    /// </summary>
    public enum MSHSegmentsEnum
    {
        MSH_1,   // Field Separator
        MSH_2,   // Encoding Characters
        MSH_3,   // Sending Application
        MSH_4,   // Sending Facility
        MSH_5,   // Receiving Application
        MSH_6,   // Receiving Facility
        MSH_7,   // Date/Time of Message
        MSH_9,   // Message Type (e.g., ADT^A08)
        MSH_10,  // Message Control ID
        MSH_11,  // Processing ID
        MSH_12   // Version ID
    }
}
