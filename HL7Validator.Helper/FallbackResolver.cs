using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HL7Validator.Helper
{
    public static class FallbackResolver
    {
        /// <summary>
        /// Intenta inferir el tipo del enum complementario basado en el nombre del segmento y la convención de nombres.
        /// </summary>
        /// <param name="customEnum">Enumerador personalizado como MSH_7, PID_5, etc.</param>
        /// <returns>El tipo del enum complementario, si existe; null en caso contrario.</returns>
        public static Type? Resolve(Enum customEnum)
        {
            string logicalName = customEnum.ToString().Replace("_", ""); // MSH_7 -> MSH7                                                                         

            var segment = customEnum.ToString().Split('_').FirstOrDefault();

            if (string.IsNullOrEmpty(segment)) return null;

            var fullTypeName = $"HL7Validator.Segments.{segment}.Components.{logicalName}ComponentsEnum";
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => SafeGetTypes(a)).FirstOrDefault(t => t.FullName == fullTypeName);
        }

        // Previene errores con ensamblados dinámicos o incompletos
        private static Type[] SafeGetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch
            {
                return Array.Empty<Type>();
            }
        }
    }
}
