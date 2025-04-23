using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.Segments.Interface
{
    /// <summary>
    /// Interfaz que contiene métodos para obtener los segmentos requeridos
    /// </summary>
    /// <typeparam name="T">Un enumerados de los segmentos requeridos</typeparam>
    public interface IRequiredMetadata<T> where T : Enum
    {
        /// <summary>
        /// Obtiene el campo en formato Terser basado en <see cref="RequiredPidSegmentsEnum"/>
        /// </summary>
        /// <param name="requiredSegmentsEnum"></param>
        /// <returns>El segmento en formato Terser. Por ejemplo /PID-3</returns>
        public string GetLogicalPath(T requiredSegmentsEnum);
        /// <summary>
        /// Devuelve los enumeradores de los campos requeridos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetRequiredSegments();
    }
}
