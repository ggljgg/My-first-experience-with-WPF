using System.Data.Entity.ModelConfiguration;

namespace ParameterReferenceBook
{
    class ParameterConfiguration : EntityTypeConfiguration<Parameter>
    {
        public ParameterConfiguration()
        {
            ToTable("student.Parameter").HasKey(p => p.IdParameter);
            Property(p => p.ParameterName)
                           .HasMaxLength(30)
                           .IsVariableLength()
                           .IsRequired();
            Property(p => p.IdTypeParameter).IsRequired();
            Property(p => p.MinValue).IsOptional();
            Property(p => p.MaxValue).IsRequired();
            Property(p => p.Description)
                           .IsOptional()
                           .HasMaxLength(200);
        }
    }
}
