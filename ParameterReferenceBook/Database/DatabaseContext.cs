using System.Data.Entity;

namespace ParameterReferenceBook

{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=AccessConnection")
        { }

        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<TypeParameter> TypeParameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TypeParameterConfiguration());
            modelBuilder.Configurations.Add(new ParameterConfiguration());

            modelBuilder.Conventions.Add(new NameConvention());

            modelBuilder.Entity<TypeParameter>()
                        .HasMany(p => p.Parameters)
                        .WithRequired(p => p.TypeParameter)
                        .HasForeignKey(s => s.IdTypeParameter)
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
