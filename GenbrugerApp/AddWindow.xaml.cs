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

namespace GenbrugerApp
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();

            comboBox1.Items.Add("Batterier");
            comboBox1.Items.Add("Biler");
            comboBox1.Items.Add("Elektronikaffald");
            comboBox1.Items.Add("Imprægneret træ");
            comboBox1.Items.Add("Inventar");
            comboBox1.Items.Add("Organisk affald");
            comboBox1.Items.Add("Pap og papir");
            comboBox1.Items.Add("Plastemballager");
            comboBox1.Items.Add("PVC");
        }
    }
}
