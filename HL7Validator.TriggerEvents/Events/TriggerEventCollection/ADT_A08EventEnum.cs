using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.TriggerEvents.Events.TriggerEventCollection
{    
    public enum ADT_A08EventEnum
    {
        /// <summary>
        /// Segmento PID
        /// </summary>
        [Attributes.ValidationTarget(typeof(Segments.PID.PIDSegmentsEnum))]
        PID,

        /// <summary>
        /// Componentes de PID.3
        /// </summary>
        [Attributes.ValidationTarget(typeof(Segments.PID.Components.PID3ComponentsEnum))]
        PID_3,

        /// <summary>
        /// Componentes de PID.5
        /// </summary>
        [Attributes.ValidationTarget(typeof(Segments.PID.Components.PID5ComponentsEnum))]
        PID_5,

        /// <summary>
        /// Componentes de PID.7
        /// </summary>
        [Attributes.ValidationTarget(typeof(Segments.PID.Components.PID7ComponentsEnum))]
        PID_7,

        /// <summary>
        /// Segmento MSH
        /// </summary>
        [Attributes.ValidationTarget(typeof(Segments.MSH.MSHSegmentsEnum))]
        MSH,

        /// <summary>
        /// Componentes de MSH.7
        /// </summary>
        [Attributes.ValidationTarget(typeof(Segments.MSH.Components.MSH7ComponentsEnum))]
        MSH_7,

        /// <summary>
        /// Componentes de MSH.9
        /// </summary>
        [Attributes.ValidationTarget(typeof(Segments.MSH.Components.MSH9ComponentsEnum))]
        MSH_9,

        /// <summary>
        /// Componentes de MSH.11
        /// </summary>
        [Attributes.ValidationTarget(typeof(Segments.MSH.Components.MSH11ComponentsEnum))]
        MSH_11,

        /// <summary>
        /// Componentes de MSH.12
        /// </summary>
        [Attributes.ValidationTarget(typeof(Segments.MSH.Components.MSH12ComponentsEnum))]
        MSH_12
    }
}
