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
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            InitializeComponent();


            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {

                    Title = "current data",
                    Values = new ChartValues<double> { 10, 50, 39, 50, 45, 30, 34, 6, 10 }
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

           