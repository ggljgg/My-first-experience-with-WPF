using System.Data.Entity.ModelConfiguration.Conventions;

namespace ParameterReferenceBook
{
    class NameConvention : Convention
    {
        public NameConvention() 
        {
            Properties<string>()
                .Where(n => n.Name == "Name")
                .Configure(n => n.HasMaxLength(30).IsVariableLength());
        }
    }
}
