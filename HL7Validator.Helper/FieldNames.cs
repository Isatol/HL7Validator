using HL7Validator.Model;
using NHapi.Base.Model;

namespace HL7Validator.Helper
{
    public class FieldNames
    {
        private IMessage _message;        

        public FieldNames(IMessage message)
        {
            _message = message;
        }

        /// <summary>
        /// Obtiene las rutas del _message HL7
        /// </summary>
        /// <returns>Una lista con las rutas en formato Terser o Lejible. Junto a su Descripción</returns>
        private List<HL7Path> GetPaths()
        {
            var paths = new List<HL7Path>();
            var names = _message.Names;

            foreach (var segName in names)
            {
                var estructuras = _message.GetAll(segName);

                for (int rep = 0; rep < estructuras.Length; rep++)
                {
                    if (estructuras[rep] is ISegment segmento)
                    {
                        for (int campo = 1; campo <= segmento.NumFields(); campo++)
                        {
                            IType[] fields = segmento.GetField(campo);

                            for (int frep = 0; frep < fields.Length; frep++)
                            {
                                var field = fields[frep];

                                if (field is IComposite composite)
                                {
                                    paths.Add(new HL7Path
                                    {
                                        TerserPath = $"/{segName}({rep})-{campo}({frep})",
                                        LegiblePath = $"{segName}.{campo}",
                                        //Description = GetDescription(field) // puede ser vacío
                                    });
                                    for (int comp = 0; comp < composite.Components.Length; comp++)
                                    {
                                        var component = composite.Components[comp];
                                        if (component is IComposite subComposite)
                                        {
                                            for (int sub = 0; sub < subComposite.Components.Length; sub++)
                                            {
                                                paths.Add(new HL7Path
                                                {
                                                    TerserPath = $"/{segName}({rep})-{campo}({frep})-{comp + 1}-{sub + 1}",
                                                    LegiblePath = $"{segName}.{campo}.{comp + 1}.{sub + 1}",
                                                    //Description = GetDescription(component)
                                                });
                                            }
                                        }
                                        else
                                        {
                                            paths.Add(new HL7Path
                                            {
                                                TerserPath = $"/{segName}({rep})-{campo}({frep})-{comp + 1}",
                                                LegiblePath = $"{segName}.{campo}.{comp + 1}",
                                                //Description = GetDescription(component)
                                            });
                                        }
                                    }
                                }
                                else
                                {
                                    paths.Add(new HL7Path
                                    {
                                        TerserPath = $"/{segName}({rep})-{campo}({frep})",
                                        LegiblePath = $"{segName}.{campo}",
                                        //Description = GetDescription(field)
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return paths;
        }

        public string GetFieldDescription(string logicalPath)
        {
            var paths = GetPaths();
            var path = paths.FirstOrDefault(p => p.LegiblePath == logicalPath);
            // TODO Implementar obtener Descripción del segmento
            return path?.Description ?? "";
        }

        public string GetDescription(IType type)
        {
            if (type is AbstractPrimitive primitive)
            {
                return primitive.Description;
            }
            else if (type is IComposite composite)
            {
                foreach (var comp in composite.Components)
                {
                    var desc = GetDescription(comp);
                    if (!string.IsNullOrEmpty(desc))
                        return desc;
                }
            }
            return string.Empty;
        }
    }
}
