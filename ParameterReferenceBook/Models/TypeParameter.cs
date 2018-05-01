using System.Collections.Generic;

namespace ParameterReferenceBook
{
    public class TypeParameter
    {
        public long IdTypeParameter { get; set; }
        public string Name { get; set; }
        public long IdTypeParameterParent { get; set; }

        public ICollection<Parameter> Parameters { get; set; }
    }
}
