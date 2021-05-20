using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System.Data.SqlClient;
using System.Configuration;

namespace GenbrugerApp
{
    /// <summary>
    /// Interaction logic for StatisticsPage1.xaml
    /// </summary>
    public partial class StatisticsPage1 : Page
    { 
        static double myList;
        private string kategoriValgt;
        static int KategoriInt;
        public string KategoriValgt
        {
            get { return kategoriValgt; }
            set { kategoriValgt = value; }

        }
        public StatisticsPage1()
        {
            InitializeComponent();

           
            SqlViewer();
            data();

        }

        private async void data()
        {
            await System.Threading.Tasks.Task.Delay(20);
            Chart.Colors = new List<Color>
            {
                Colors.YellowGreen,
                Colors.LightSeaGreen,
                Colors.Blue
            };


            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    
                    Title = kategoriValgt,
                    Values = new ChartValues<double> { myList }
                },

            };


            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString();



            //modifying any series values will also animate and update the chart
            SeriesCollection[0].Values.Add(5d);

            DataContext = this;
           
        }
        private async void SqlViewer()
        {
            await System.Threading.Tasks.Task.Delay(20);
            switch (kategoriValgt)
            {
                case "Batterier":
                    KategoriInt = 1;
                    break;
                case "Biler":
                    KategoriInt = 2;
                    break;
                case "Elektronikaffald":
                    KategoriInt = 3;
                    break;
                case "Imprægneret træ":
                    KategoriInt = 4;
                    break;
                case "Inventar":
                    KategoriInt = 5;
                    break;
                case "Organisk affald":
                    KategoriInt = 6;
                    break;
                case "Pap og papir":
                    KategoriInt = 7;
                    break;
                case "Plastemballager":
                    KategoriInt = 8;
                    break;
                case "PVC":
                    KategoriInt = 9;
                    break;
                default:
                    break;
            }
            SqlConnection connection = null;
            try
            {
                //var myList = new List<String>();
                int i = 0;
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDelta"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT * FROM Skrald WHERE Kategori = '" + KategoriInt + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    //myList.Add(Convert.ToDecimal(reader[2]));
                    if (KategoriInt !=null)
                    {
                        myList = (Convert.ToDouble(reader[1]));
                        i++;
                    }
                  
                    
                    //Maaleenhed = reader[4].ToString(),
                    //Tid = reader[5].ToString(),

                };
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null) connection.Close();
            }
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}
    

