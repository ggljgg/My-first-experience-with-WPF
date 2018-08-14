using System.Data.Entity.ModelConfiguration;

namespace ParameterReferenceBook
{
    class TypeParameterConfiguration : EntityTypeConfiguration<TypeParameter>
    {
        public TypeParameterConfiguration()
        {
            ToTable("student.TypeParameter").HasKey(t => t.IdTypeParameter);
            Property(t => t.TypeParameterName)
                           .HasMaxLength(30)
                           .IsVariableLength()
                           .IsRequired();
            Property(t => t.IdTypeParameterParent).IsRequired();
        }
    }
}
