using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CSVHelperExample
{
    /// <summary>
    /// THIS IS A CLASS DESC
    /// </summary>
    public class CsvImporter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<CsvMap> ImportSomeRecords(string fileName)
        {
            var myRecords = new List<CsvMap>();
            using (var reader = new StreamReader(fileName))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CsvMapMap>();

                    int currentID;
                    string name;
                    string gender;
                    int birthdayYear;
                    int age;

                    //Start Reading Csv File
                    csv.Read();
                    //Skip Header
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        //Needs IfNUllOREMPTY check -> skip/break or allow null -> test null values on data set
                        currentID = csv.GetField<int>(0);
                        name = csv.GetField<string>(1);
                        gender = csv.GetField<string>(2);
                        birthdayYear = csv.GetField<int>(3);
                        age = csv.GetField<int>(4);
                        myRecords.Add(CreateRecord(currentID, name, gender, birthdayYear, age));

                    }

                }

            }
            return myRecords;
        }

        /// <summary>
        /// Creates a record as the data comes in
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="bDayYear"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public static CsvMap CreateRecord(int id, string name, string gender, int bDayYear, int age)
        {
            CsvMap record = new CsvMap();

            record.id = id;
            record.name = name;
            record.gender = gender;
            record.birthdayYear = bDayYear;
            record.age = age;

            return record;
        }
    }
}
