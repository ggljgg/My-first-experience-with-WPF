namespace ParameterReferenceBook
{
    public class Parameter
    {
        public long IdParameter { get; set; }
        public string Name { get; set; }
        public long IdTypeParameter { get; set; }
        public float? MinValue { get; set; }
        public float MaxValue { get; set; }
        public string Description { get; set; }

        public TypeParameter TypeParameter { get; set; }
    }
}
