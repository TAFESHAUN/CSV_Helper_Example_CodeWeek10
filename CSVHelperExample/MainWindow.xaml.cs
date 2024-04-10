using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSVHelperExample
{
    public partial class MainWindow : Window
    {

        public const string FILE_PATH = @"C:\Users\User\Desktop\CSV_Helper_Example_Code\CSV_Helper_Example_Code\CSVHelperExample\some-data.csv";
        public List<CsvMap> importedRecords = new();

        /// <summary>
        /// These are the record types I am going to use in my project
        /// </summary>
        public List<CsvMap> savedRecords = new(); // SAVE anyone that was over 30, Worked more then 40hours, Days sick
        public List<CsvMap> corruptedRecords = new();
        public List<CsvMap> empty_OR_missing_data_Records = new();

        public MainWindow()
        {

            InitializeComponent();
            importedRecords = CsvImporter.ImportSomeRecords(FILE_PATH).ToList();

            data_Grid_Csv.DataContext = importedRecords;
        }

        //Some EVENT


    }

}