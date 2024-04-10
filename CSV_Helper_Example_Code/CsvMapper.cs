//Imported CSV help libs
using CsvHelper.Configuration;


namespace csvimport
{
    public partial class Program
    {
        public sealed class CsvMapper : ClassMap<CsvPersonData>
        {
            public CsvMapper()
            {
                Map(m => m.id).Index(0);
                Map(m => m.name).Index(1);
                Map(m => m.gender).Index(2);
                Map(m => m.birthYear).Index(3);
                Map(m => m.age).Index(4);
            }
        }

    }
}
