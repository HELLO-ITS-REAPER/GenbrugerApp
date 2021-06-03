﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
        StatisticsPage statisticsPage = new StatisticsPage();

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
            Chart.Colors = new List<Color>
            {
                Colors.YellowGreen,
                Colors.LightSeaGreen,
                Colors.Blue
            };


        }
        private async void StatsPage()
        {
            await System.Threading.Tasks.Task.Delay(50);
            statisticsPage.ImportList = importList;
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


