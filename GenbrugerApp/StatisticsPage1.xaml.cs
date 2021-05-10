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

namespace GenbrugerApp
{
    /// <summary>
    /// Interaction logic for StatisticsPage1.xaml
    /// </summary>
    public partial class StatisticsPage1 : Page
    {
        public StatisticsPage1()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {

                    Title = "Skrald",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
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
    

