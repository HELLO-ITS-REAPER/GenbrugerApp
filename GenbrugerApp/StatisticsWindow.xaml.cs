using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CsvHelper;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using Microsoft.VisualBasic.FileIO;

namespace GenbrugerApp
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        static bool filter;


        public StatisticsWindow()
        {
            InitializeComponent();
            comboBoxkategori.Items.Add("vælg kategori");
            comboBoxkategori.Items.Add("Batterier");
            comboBoxkategori.Items.Add("Biler");
            comboBoxkategori.Items.Add("Elektronikaffald");
            comboBoxkategori.Items.Add("Imprægneret træ");
            comboBoxkategori.Items.Add("Inventar");
            comboBoxkategori.Items.Add("Organisk affald");
            comboBoxkategori.Items.Add("Pap og papir");
            comboBoxkategori.Items.Add("Plastemballager");
            comboBoxkategori.Items.Add("PVC");
            StatsPage();



        }
        private async void StatsPage()
        {
            await System.Threading.Tasks.Task.Delay(100);
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


            for (int i = 0; i < importList.Count; i++)
            {
                if (importList[i].Kategori.Contains("1") && importList[i].Måleenhed.Contains("4"))
                {
                    batValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("2") && importList[i].Måleenhed.Contains("2"))
                {
                    bilValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("3") && importList[i].Måleenhed.Contains("3"))
                {
                    elValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("4") && importList[i].Måleenhed.Contains("4"))
                {
                    impValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("5") && importList[i].Måleenhed.Contains("2"))
                {
                    invValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("6") && importList[i].Måleenhed.Contains("4"))
                {
                    orgValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("7") && importList[i].Måleenhed.Contains("5"))
                {
                    papValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("8") && importList[i].Måleenhed.Contains("4"))
                {
                    plaValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else if (importList[i].Kategori.Contains("9") && importList[i].Måleenhed.Contains("2"))
                {
                    pvcValue.Add(Convert.ToDouble(importList[i].Mængde, CultureInfo.InvariantCulture));
                }
                else
                {
                    filter = false;
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
            if (filter == false)
            {
                MessageBox.Show("En eller flere elementer i importeret data opfylder ikke kravene");
            }
            StatisticsPage statisticsPage = new StatisticsPage();

            statisticsPage.ImpSum = impSum;
            mainframe.Content = statisticsPage;


        }


        private List<SkraldData> importList;
        public List<SkraldData> ImportList
        {
            get { return importList; }
            set { importList = value; }

        }



        private void TilbageButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void comboBoxkategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string kategoribox = comboBoxkategori.SelectedItem.ToString();

            StatisticsPage1 StatisticsPage1 = new StatisticsPage1();
            StatisticsPage1.KategoriValgt = kategoribox;

            mainframe1.Content = StatisticsPage1;

        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {

            if (filter != false)
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDelta"].ConnectionString);
                    connection.Open();
                    using (StreamReader reader = new StreamReader(@"C:\Users\Martin\OneDrive - EaDania\C#\WPF\Eksamensprojekt\GenbrugerApp\GenbrugerApp\CsvFolder\TeamBravo_output.csv"))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(';');
                            var sql = "INSERT INTO Skrald (Mængde, Måleenhed, Kategori, Beskrivelse, Ansvarlig, CVR, Tid) " +
                                "VALUES ('" + values[1] + "','" + values[2] + "','" + values[3] + "','" + values[4] + "','" + values[5] + "','" + values[6] +
                                "','" + values[7] + "')";
                            var cmd = new SqlCommand();
                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = connection;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
            }
        }
    }
}


