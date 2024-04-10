using System.Collections.Generic;
using System.IO;
using System.Globalization;
//Imported CSV help libs
using CsvHelper;


namespace csvimport
{
    public partial class Program
    {
        public class CsvImporter
        {
            public static List<CsvPersonData> ImportSomeRecords(string fileName)
            {
                //Creates the records to return
                var myRecords = new List<CsvPersonData>();
                //Opens the file we sent over as a stream -> direct file read
                using (var reader = new StreamReader(fileName))
                {
                    //While the text files open treat it as a CSV file instead
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        //Register a Mapped TYPE to my csv
                        csv.Context.RegisterClassMap<CsvMapper>();
                        
                        //Temp values to be used for each row we read
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
                            //Validate data + Check empty in create records
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

            public static CsvPersonData CreateRecord(int id, string name, string gender, int bDayYear, int age)
            {
                CsvPersonData record = new CsvPersonData();

                record.id = id;
                record.name = name;
                record.gender = gender;
                record.birthYear = bDayYear;
                record.age = age;

                return record;
            }
        }

    }
}
