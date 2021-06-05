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
using System.Globalization;
using LiveCharts.Configurations;
using System.Data;

namespace GenbrugerApp
{
    /// <summary>
    /// Interaction logic for StatisticsPage1.xaml
    /// </summary>
    public partial class StatisticsPage1 : Page
    {
        /// Frederik har lavet hele StaticsPage1
        private string kategoriValgt;
        static int KategoriInt;
        static string tid0="";
        static string tid1="";
        static string tid2="";
        static string tid3="";
        static string tid4="";
        static string tid5="";
        static string tid6="";
        static string tid7="";
        static string tid8="";
        static string tid9="";
        static string tid10="";

        public string KategoriValgt
        {
            get { return kategoriValgt; }
            set { kategoriValgt = value; }

        }
        public StatisticsPage1()
        {
            InitializeComponent();


            SqlViewer();

        }


        private async void SqlViewer()
        {
            await System.Threading.Tasks.Task.Delay(20);
            List<double> allValues = new List<double>();
            List<string> allTime = new List<string>();
            List<string> focusedTime = new List<string>();
            List<double> focusedValues = new List<double>();



            if (kategoriValgt == "vælg kategori")
            {

            }
            else
            {



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

                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDelta"].ConnectionString);
                    SqlCommand command = new SqlCommand("SELECT * FROM Skrald WHERE Kategori = '" + KategoriInt + "'", connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allValues.Add(Convert.ToDouble(reader[1]));
                        allTime.Add(Convert.ToString(reader[7]));
                    };
                    connection.Close();

                    allTime.Reverse();
                    allValues.Reverse();


                    if (allTime.Count <= 11)
                    {
                        for (int i = 0; i < allTime.Count ; i++)
                        {
                            focusedTime.Add(allTime[i]);
                            focusedValues.Add(allValues[i]);
                            
                        }
                        focusedTime.Reverse();
                        focusedValues.Reverse();
                        tid0 = focusedTime[0];
                        tid1 = focusedTime[1];
                        tid2 = focusedTime[2];
                        tid3 = focusedTime[3];
                        tid4 = focusedTime[4];
                        tid5 = focusedTime[5];
                        tid6 = focusedTime[6];
                        tid7 = focusedTime[7];
                        tid8 = focusedTime[8];
                        tid9 = focusedTime[9];
                        tid10 = focusedTime[10];
                    }
                    else
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            focusedTime.Add(allTime[i]);
                            focusedValues.Add(allValues[i]);
                        }
                        focusedTime.Reverse();
                        focusedValues.Reverse();
                        tid0 = focusedTime[0];
                        tid1 = focusedTime[1];
                        tid2 = focusedTime[2];
                        tid3 = focusedTime[3];
                        tid4 = focusedTime[4];
                        tid5 = focusedTime[5];
                        tid6 = focusedTime[6];
                        tid7 = focusedTime[7];
                        tid8 = focusedTime[8];
                        tid9 = focusedTime[9];
                        tid10 = focusedTime[10];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection != null) connection.Close();
                }

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
                    Title = "",

                    Values = new ChartValues<double>(focusedValues)
                }
            };
                

                Labels = new[] { tid0, tid1,tid2,tid3,tid4, tid5 , tid6 , tid7 , tid8 , tid9, tid10 };
                YFormatter = value => value.ToString("C");

                DataContext = this;
            }
        }


        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}







    

