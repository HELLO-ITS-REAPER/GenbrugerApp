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

namespace GenbrugerApp
{
    /// <summary>
    /// Interaction logic for StatisticsPage1.xaml
    /// </summary>
    public partial class StatisticsPage1 : Page
    { static int hej = 0;
        static int hej1 =0;
        static int hej2 = 0;
        static int hej3 = 0;
        private string kategoriValgt;
        public string KategoriValgt
        {
            get { return kategoriValgt; }
            set { kategoriValgt = value; }

        }
        public StatisticsPage1()
        {
            InitializeComponent();
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
            if (kategoriValgt == "Batterier")
            {
                hej = 5;
                hej1 = 6;
                hej2 = 7;
                hej3 = 8;


            }
            else if (kategoriValgt == "Biler")
            {
                hej = 10;
                hej1 = 10;
                hej2 = 10;
                hej3 = 10;
            }
            else if (kategoriValgt == "Elektronikaffald")
            {
                hej = 10;
                hej1 = 10;
                hej2 = 10;
                hej3 = 10;
            }
            else if (kategoriValgt == "Imprægneret træ")
            {
                hej = 10;
                hej1 = 10;
                hej2 = 10;
                hej3 = 10;
            }
            else if (kategoriValgt == "Inventar")
            {
                hej = 10;
                hej1 = 10;
                hej2 = 10;
                hej3 = 10;
            }
            else if (kategoriValgt == "Organisk affald")
            {
                hej = 10;
                hej1 = 10;
                hej2 = 10;
                hej3 = 10;
            }
            else if (kategoriValgt == "Pap og papir")
            {
                hej = 10;
                hej1 = 10;
                hej2 = 10;
                hej3 = 10;
            }
            else if (kategoriValgt == "Plastemballager")
            {
                hej = 10;
                hej1 = 10;
                hej2 = 10;
                hej3 = 10;
            }
            else if (kategoriValgt == "PVC")
            {
                hej = 10;
                hej1 = 10;
                hej2 = 10;
                hej3 = 10;
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {

                    Title = kategoriValgt,
                    Values = new ChartValues<double> { hej, hej1, hej2, hej3 ,4 }
                },

            };


            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString();



            //modifying any series values will also animate and update the chart
            SeriesCollection[0].Values.Add(5d);

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}
    

