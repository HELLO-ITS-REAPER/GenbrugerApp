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
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            InitializeComponent();
            List<double> allValues = new List<double>();
            List<double> allSum = new List<double>();
            for (int KategoriInt = 1; KategoriInt < 10; KategoriInt++)
            {


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
                    };

                    allSum.Add(allValues.Sum());
                    allValues.Clear();

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
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {

                    Title = "current data",
                    Values = new ChartValues<double>(allSum)                
                }
            };

                //adding series will update and animate the chart automatically
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = "external data",
                    Values = new ChartValues<double> { 11, 56, 42, 45, 33, 78, 111, 12, }
                });

                //also adding values updates and animates the chart automatically
                SeriesCollection[1].Values.Add(48d);

                Labels = new[] { "Batterier", "Biler", "Elektronikaffald", "Imprægneret træ", "Inventar", "Organisk affald", "Pap og papir", "Plastemballager", "PVC", };
                Formatter = value => value.ToString("N");

                DataContext = this;
            }


        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}

           