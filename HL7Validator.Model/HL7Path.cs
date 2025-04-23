using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.Model
{
    public class HL7Path
    {
        public string TerserPath { get; set; }   // ej: /PID(0)-5(0)-1-2
        public string LegiblePath { get; set; }  // ej: PID.5.1.2
        public string Description { get; set; } // ej: PatientID
    }
}
