using System;
using System.Collections.Generic;
//Imported CSV help libs


namespace csvimport
{
    /// <summary>
    /// Simple CSV Helper Example
    /// This console App showcases how to map CSV data using csv helper
    /// We read in our csv data values using this mapped logic
    /// This app checks to see if the person is born in the 2000's or the 90's
    /// </summary>

    public partial class Program
    {

        static void Main(string[] args)
        {
            var filePath = @"some-data.csv";


            List<CsvPersonData> importedRecords = CsvImporter.ImportSomeRecords(filePath);

            CsvPersonData bobPerson = new CsvPersonData();
            bobPerson.id = 6;
            bobPerson.name = "Bob Jobs";
            bobPerson.gender = "Male";
            bobPerson.birthYear = 1995;
            bobPerson.age = 29;
            importedRecords.Add(bobPerson);


            foreach (CsvPersonData record in importedRecords)
            {
                Console.WriteLine("Record ID: " + record.id);
                Console.WriteLine("Name: " + record.name);
                Console.WriteLine("Gender: " + record.gender);
                if(record.birthYear >= 2000)
                {
                    Console.WriteLine("Born in the 2000's : " + record.birthYear);
                }
                else
                {
                    Console.WriteLine("Born in the 90's : " + record.birthYear);
                }
                Console.WriteLine("Age : " + record.age);
                Console.WriteLine("\n");
            }

            Console.ReadLine();
        }

    }
}
