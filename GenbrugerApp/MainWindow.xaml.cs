using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;
using System.Reflection;

namespace GenbrugerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SkraldData> skraldData = new List<SkraldData>();
        public MainWindow()
        {
            InitializeComponent();
            SqlViewer();
        }

        public void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            
            string path = Path.Combine(Environment.CurrentDirectory, @"CsvFolder\");
           
            int Count = 0;
            foreach (string file in Directory.EnumerateFiles(path))
            {
                Count++;
            }

            if (Count == 1)
            {
                string[] filename = Directory.GetFiles(path, "*.csv");
                string filepath = filename[0];
                var lines = File.ReadAllLines(filepath);
                var importList = new List<SkraldData>();
                foreach (var line in lines)
                {
                    var values = line.Split(';');
                    var data = new SkraldData()
                    {
                        SkraldeID = values[0],
                        Mængde = values[1],
                        Måleenhed = values[2],
                        Kategori = values[3],
                        Beskrivelse = values[4],
                        Ansvarlig = values[5],
                        CVR = values[6],
                        Tid = Convert.ToDateTime(values[7])
                    };
                    importList.Add(data);





                }
                StatisticsWindow statisticsWindow = new StatisticsWindow();
                statisticsWindow.Show();
                statisticsWindow.ImportList = importList;
                statisticsWindow.FileName = filepath;
                this.Close();
            }
            else if (Count < 1)
            {
                MessageBox.Show("der blev ikke fundet en CSV fil i CsvFolder");

            }
            else if (Count > 1)
            {
                MessageBox.Show("Fejl der findes flere CSV filer i CsvFolder");

            }

        }

        private void EksportButton_Click(object sender, RoutedEventArgs e)
        {
            /// Frederik
            var csvPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"DELTA-SKRALT.csv");
            File.Delete(csvPath);
            for (int i = 0; i < skraldData.Count; i++)
            {
                using (StreamWriter sw = File.AppendText(csvPath ))
                {
                    sw.WriteLine(skraldData[i].SkraldeID + ";" + skraldData[i].Mængde.Replace(',' , '.') + ";" + skraldData[i].Måleenhed + ";" + skraldData[i].Kategori + ";" + skraldData[i].Beskrivelse + ";" + skraldData[i].Ansvarlig + ";" + skraldData[i].CVR + ";" + Convert.ToString(skraldData[i].Tid).Replace('.', ':'));
                }
            }
            Logger.SaveMessage("Brugeren har eksporteret data fra databasen til " + csvPath);
        }
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow row = Data.ItemContainerGenerator.ContainerFromIndex(Data.SelectedIndex) as DataGridRow;
            SkraldData skraldData = (SkraldData)row.Item;

            EditWindow editWindow = new EditWindow(skraldData);
            editWindow.Show();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridRow row = Data.ItemContainerGenerator.ContainerFromIndex(Data.SelectedIndex) as DataGridRow;
            SkraldData skraldData = (SkraldData)row.Item;
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDelta"].ConnectionString);
                string cmd = string.Format("DELETE FROM Skrald WHERE SkraldeID = '{0}'", skraldData.SkraldeID);

                SqlCommand command = new SqlCommand(cmd, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                MessageBox.Show("Din valgte data er nu blevet slettet.");
                Logger.SaveMessage("Brugeren har slettet en data fra databasen\n" +
                    "Den slettede date:" +
                    "\nSkraldID = '" + skraldData.SkraldeID + "'," +
                    "\nMængde = '" + skraldData.Mængde + "'," +
                    "\nMåleenhed = '" + skraldData.Måleenhed + "'," +
                    "\nKategori = '" + skraldData.Kategori + "'," +
                    "\nBeskrivelse = '" + skraldData.Beskrivelse +
                    "\nAnsvarlig = '" + skraldData.Ansvarlig + "'," +
                    "\nCVR = '" + skraldData.CVR + "'," +
                    "\nTid = '" + skraldData.Tid + "'" +
                    "\nDenne data blev slettet");
                Data.ItemsSource = null;
                SqlViewer();
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

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.Show();
            this.Close();
        }

        private void SqlViewer()
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDelta"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT * FROM Skrald ORDER BY SkraldeID ASC", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                skraldData.Clear();
                while (reader.Read()) skraldData.Add(new SkraldData
                {
                    SkraldeID = reader[0].ToString(),
                    Mængde = reader[1].ToString(),
                    Måleenhed = reader[2].ToString(),
                    Kategori = reader[3].ToString(),
                    Beskrivelse = reader[4].ToString(),
                    Ansvarlig = reader[5].ToString(),
                    CVR = reader[6].ToString(),
                    Tid = Convert.ToDateTime(reader[7])
                });
                connection.Close();
                Data.ItemsSource = skraldData;
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

        private void ListViewItem_Toggle(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == true)
            {
                tt_tilføj.Visibility = Visibility.Collapsed;
                tt_rediger.Visibility = Visibility.Collapsed;
                tt_slet.Visibility = Visibility.Collapsed;
                tt_importér.Visibility = Visibility.Collapsed;
                tt_eksportér.Visibility = Visibility.Collapsed;
                tt_statistik.Visibility = Visibility.Collapsed;
            }

            else
            {
                tt_tilføj.Visibility = Visibility.Visible;
                tt_rediger.Visibility = Visibility.Visible;
                tt_slet.Visibility = Visibility.Visible;
                tt_importér.Visibility = Visibility.Visible;
                tt_eksportér.Visibility = Visibility.Visible;
                tt_statistik.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
            Logo.Opacity = 1;
            Data.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.9;
            Logo.Opacity = 0.2;
            Data.Opacity = 0.2;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            Data.ItemsSource = null;
            SqlViewer();
        }

    }
}
