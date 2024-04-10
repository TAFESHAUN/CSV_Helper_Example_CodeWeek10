using CsvHelper.Configuration;

namespace CSVHelperExample
{
    //CSV HELPER MAPPING TO DATA OBJECT
    public sealed class CsvMapMap : ClassMap<CsvMap>
    {
        public CsvMapMap()
        {
            Map(m => m.id).Index(0);
            Map(m => m.name).Index(1);
            Map(m => m.gender).Index(2);
            Map(m => m.birthdayYear).Index(3);
            Map(m => m.age).Index(4);
        }
    }
}
