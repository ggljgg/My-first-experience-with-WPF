using System.Collections.Generic;
using System.Data.Entity;

namespace ParameterReferenceBook
{
    class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            IList<TypeParameter> newTypeParameters = new List<TypeParameter>();
            #region Data
            newTypeParameters.Add(new TypeParameter()
            {
                Name = "Учебный план",
                IdTypeParameterParent = 0,
                Parameters = new List<Parameter>()
                {
                    new Parameter() { Name = "ГИА", MinValue = 2, MaxValue = 10 },
                    new Parameter() { Name = "ВД/Блок ФК", MinValue = 2, MaxValue = 2 },
                }
            });
            
            newTypeParameters.Add(new TypeParameter()
            {
                Name = "Дисциплины (Модули)",
                IdTypeParameterParent = 1,
                Parameters = new List<Parameter>()
                {
                    new Parameter() { Name = "Дисциплины (Модули).О", MinValue = 205, MaxValue = 225 },
                    new Parameter() { Name = "Дисциплины (Модули).Б", MinValue = 81, MaxValue = 99 },
                    new Parameter() { Name = "Дисциплины (Модули).В", MinValue = 107, MaxValue = 144 }
                }
            });
            
            newTypeParameters.Add(new TypeParameter()
            {
                Name = "Параметры по семестрам",
                IdTypeParameterParent = 1
            });
            
            newTypeParameters.Add(new TypeParameter()
            {
                Name = "Параметры по курсам",
                IdTypeParameterParent = 1,
                Parameters = new List<Parameter>()
                {
                    new Parameter() { Name = "Кол-во ЗЕ по ОЧН", MinValue = 60, MaxValue = 60 },
                    new Parameter() { Name = "Кол-во ЗЕ по ЗАОЧН", MinValue = 60, MaxValue = 60 },
                    new Parameter() { Name = "Кол-во ЗЕ (сокр. срок)", MaxValue = 75 },
                }
            });
            
            newTypeParameters.Add(new TypeParameter()
            {
                Name = "Нагрузка",
                IdTypeParameterParent = 0
            });

            newTypeParameters.Add(new TypeParameter()
            {
                Name = "Индивидуальный план",
                IdTypeParameterParent = 0
            });

            newTypeParameters.Add(new TypeParameter()
            {
                Name = "Test Нагрузка",
                IdTypeParameterParent = 5
            });

            newTypeParameters.Add(new TypeParameter()
            {
                Name = "Test Дисциплины",
                IdTypeParameterParent = 2
            });

            newTypeParameters.Add(new TypeParameter()
            {
                Name = "Test Дисциплины 2",
                IdTypeParameterParent = 8
            });
            #endregion 
            db.TypeParameters.AddRange(newTypeParameters);
            base.Seed(db);
        }
    }
}
