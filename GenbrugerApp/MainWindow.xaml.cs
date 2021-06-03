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
using System.Globalization;
using System.IO;

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

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            var lines = File.ReadAllLines(@"C:\Users\Martin\OneDrive - EaDania\C#\WPF\Eksamensprojekt\GenbrugerApp\GenbrugerApp\CsvFolder\TeamBravo_output.csv");
            var list = new List<SkraldData>();
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
                list.Add(data);
            }
            //list.ForEach(x => MessageBox.Show($"{x.SkraldeID}\t{x.Mængde}\t{x.Måleenhed}\t{x.Kategori}\t{x.Beskrivelse}\t{x.Ansvarlig}\t{x.CVR}\t{x.Tid}"));
        }

        private void EksportButton_Click(object sender, RoutedEventArgs e)
        {
            {
                var csvPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"DELTA-SKRALT.csv");

                using (var writer = new StreamWriter(csvPath))

                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteField("sep=,", false);
                    csv.NextRecord();
                    csv.WriteRecords(skraldData);
                }
            }


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
