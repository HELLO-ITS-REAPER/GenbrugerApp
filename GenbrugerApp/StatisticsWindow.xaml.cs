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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

namespace GenbrugerApp
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        StatisticsPage StatisticsPage = new StatisticsPage();
        

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
            mainframe.Content = StatisticsPage;
            Chart.Colors = new List<Color>
            {
                Colors.YellowGreen,
                Colors.LightSeaGreen,
                Colors.Blue
            };


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
    }
    }

    

