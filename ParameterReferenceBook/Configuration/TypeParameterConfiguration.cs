using System.Data.Entity.ModelConfiguration;

namespace ParameterReferenceBook
{
    class TypeParameterConfiguration : EntityTypeConfiguration<TypeParameter>
    {
        public TypeParameterConfiguration()
        {
            ToTable("TypeParameter").HasKey(t => t.IdTypeParameter);
            Property(t => t.Name).IsRequired();
            Property(t => t.IdTypeParameterParent).IsRequired();
        }
    }
}
