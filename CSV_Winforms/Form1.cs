using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;


namespace CSV_Winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Set minus so we don't write until something is selected
        int selectIndex = -1;

        //Read into datagrid
        private void button1_Click(object sender, EventArgs e)
        {
            var fileName = @"C:\Users\User\Desktop\CSV_Helper_Example_Code\CSV_Helper_Example_Code\CSV_Winforms\some-data.csv";


            List<CsvMap> importedRecords = CsvImporter.ImportSomeRecords(fileName);

            dataGridView1.DataSource = importedRecords;
        }

        //Write to file using imported records
        private void button2_Click(object sender, EventArgs e)
        {
            var fileName = @"C:\Users\User\Desktop\CSV_Helper_Example_Code\CSV_Helper_Example_Code\CSV_Winforms\some-data.csv";

            List<CsvMap> importedRecords = CsvImporter.ImportSomeRecords(fileName);

            var saveFileName = @"C:\Users\User\Desktop\CSV_Helper_Example_Code\CSV_Helper_Example_Code\CSV_Winforms\some-data" + DateTime.Now.ToFileTime() + ".csv";

            using (var writer = new StreamWriter(saveFileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(importedRecords);
            }

            MessageBox.Show("New CSV file generated");
        }
        
        //Write to file using selected cell data
        private void button3_Click(object sender, EventArgs e)
        {
            var saveRecord = new List<CsvMap>();
            if (selectIndex > 0)
            {
                //var data = dataGridView1.Rows[selectIndex];
                //MessageBox.Show(data.ToString());
                //MessageBox.Show(dataGridView1.Rows[selectIndex].Cells[0].Value.ToString());

                //Set data to Map type
                var data = new CsvMap();
                //Get cell data based off selection and convert
                data.id = Convert.ToInt32(dataGridView1.Rows[selectIndex].Cells[0].Value);
                data.name = Convert.ToString(dataGridView1.Rows[selectIndex].Cells[1].Value);
                data.gender = Convert.ToString(dataGridView1.Rows[selectIndex].Cells[2].Value);
                data.birthdayYear = Convert.ToInt32(dataGridView1.Rows[selectIndex].Cells[3].Value);
                data.age = Convert.ToInt32(dataGridView1.Rows[selectIndex].Cells[4].Value);

                //Add data to record logic
                saveRecord.Add(data);

                //New Save file
                var saveFileName = @"C:\Users\User\Desktop\CSV_Helper_Example_Code\CSV_Helper_Example_Code\CSV_Winforms\save" + DateTime.Now.ToFileTime() + ".csv";

                //Save the the record
                using (var writer = new StreamWriter(saveFileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(saveRecord);
                }

                MessageBox.Show("New CSV file generated");
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Show Data Selected
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }

            //Show row index and save this!
            if(dataGridView1.CurrentRow != null)
            {
                //MessageBox.Show(dataGridView1.CurrentRow.ToString());
                selectIndex = dataGridView1.CurrentRow.Index;
                MessageBox.Show(selectIndex.ToString());
            }
        }
    }

    public class CsvImporter
    {
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
    public class CsvMap
    {
        public int id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public int birthdayYear { get; set; }
        public int age { get; set; }
    }

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
