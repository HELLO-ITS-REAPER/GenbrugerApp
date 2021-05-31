using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
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
using Microsoft.Win32;
using System.IO;
using System.Data;

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

        }

        private void EksportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.Show();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

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
                SqlCommand command = new SqlCommand("SELECT * FROM Skrald", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                skraldData.Clear();
                while (reader.Read()) skraldData.Add(new SkraldData
                {
                    SkraldeID = reader[0].ToString(),
                    Kategori = reader[1].ToString(),
                    Maengde = reader[2].ToString(),
                    Beskrivelse = reader[3].ToString(),
                    Maaleenhed = reader[4].ToString(),
                    Tid = reader[5].ToString(),
                    AffaldspostID = reader[6].ToString(),
                    Ansvarlig = reader[7].ToString(),
                    CVR = reader[8].ToString()
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

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

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

    }
}
