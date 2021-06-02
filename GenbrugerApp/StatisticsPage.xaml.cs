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
            StatsPageGraf();
        }
        List<double> batValue = new List<double>();
        List<double> bilValue = new List<double>();
        List<double> elValue = new List<double>();
        List<double> impValue = new List<double>();
        List<double> invValue = new List<double>();
        List<double> orgValue = new List<double>();
        List<double> papValue = new List<double>();
        List<double> plaValue = new List<double>();
        List<double> pvcValue = new List<double>();


        List<double> impSum = new List<double>();
        private async void StatsPageGraf()
        {
            await System.Threading.Tasks.Task.Delay(50);
            for (int i = 0; i < importList.Count; i++)
            {
                if (importList[i].Kategori.Contains("1"))
                {
                    batValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("2"))
                {
                    bilValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("3"))
                {
                    elValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("4"))
                {
                    impValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("5"))
                {
                    invValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("6"))
                {
                    orgValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("7"))
                {
                    papValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("8"))
                {
                    plaValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("9"))
                {
                    pvcValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
            }
            impSum.Add(batValue.Sum());
            impSum.Add(bilValue.Sum());
            impSum.Add(elValue.Sum());
            impSum.Add(impValue.Sum());
            impSum.Add(invValue.Sum());
            impSum.Add(orgValue.Sum());
            impSum.Add(papValue.Sum());
            impSum.Add(plaValue.Sum());
            impSum.Add(pvcValue.Sum());

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
                Values = new ChartValues<double>(impSum)
            });

            //also adding values updates and animates the chart automatically
            SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Batterier", "Biler", "Elektronikaffald", "Imprægneret træ", "Inventar", "Organisk affald", "Pap og papir", "Plastemballager", "PVC", };
            Formatter = value => value.ToString("N");

            DataContext = this;

        }


        private List<SkraldData> importList;
        public List<SkraldData> ImportList
        {
            get { return importList; }
            set { importList = value; }

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}

           