using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        private List<SkraldData> list = new List<SkraldData>();
        public AddWindow()
        {
            InitializeComponent();

            KategoriComboBox.Items.Add("Batterier");
            KategoriComboBox.Items.Add("Biler");
            KategoriComboBox.Items.Add("Elektronikaffald");
            KategoriComboBox.Items.Add("Imprægneret træ");
            KategoriComboBox.Items.Add("Inventar");
            KategoriComboBox.Items.Add("Organisk affald");
            KategoriComboBox.Items.Add("Pap og papir");
            KategoriComboBox.Items.Add("Plastemballager");
            KategoriComboBox.Items.Add("PVC");

            MåleenhedComboBox.Items.Add("Colli");
            MåleenhedComboBox.Items.Add("Stk.");
            MåleenhedComboBox.Items.Add("Ton");
            MåleenhedComboBox.Items.Add("Kilogram");
            MåleenhedComboBox.Items.Add("Gram");
            MåleenhedComboBox.Items.Add("M3");
            MåleenhedComboBox.Items.Add("Liter");
            MåleenhedComboBox.Items.Add("Hektoliter");
        }

        private void LUK_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public string Mængde { get; set; }
        public string Måleenhed { get; set; }
        public string Kategori { get; set; }
        public string Beskrivelse { get; set; }
        public string Ansvarlig { get; set; }
        public string CVR { get; set; }
        public string AffaldspostID { get; set; }
        
        
        public int KategoriInt;
        public int MåleenhedInt;
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            switch (KategoriComboBox.SelectedItem)
            {
                case "Batterier":
                    KategoriInt = 1;
                    break;
                case "Biler":
                    KategoriInt = 2;
                    break;
                case "Elektronikaffald":
                    KategoriInt = 3;
                    break;
                case "Imprægneret træ":
                    KategoriInt = 4;
                    break;
                case "Inventar":
                    KategoriInt = 5;
                    break;
                case "Organisk affald":
                    KategoriInt = 6;
                    break;
                case "Pap og papir":
                    KategoriInt = 7;
                    break;
                case "Plastemballager":
                    KategoriInt = 8;
                    break;
                case "PVC":
                    KategoriInt = 9;
                    break;
                default:
                    break;
            }
            switch (MåleenhedComboBox.SelectedItem)
            {
                case "Colli":
                    MåleenhedInt = 1;
                    break;
                case "Stk.":
                    MåleenhedInt = 2;
                    break;
                case "Ton":
                    MåleenhedInt = 3;
                    break;
                case "Kilogram":
                    MåleenhedInt = 4;
                    break;
                case "Gram":
                    MåleenhedInt = 5;
                    break;
                case "M3":
                    MåleenhedInt = 6;
                    break;
                case "Liter":
                    MåleenhedInt = 7;
                    break;
                case "Hektoliter":
                    MåleenhedInt = 8;
                    break;
                default:
                    break;
            }
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDelta"].ConnectionString);
                string cmd = string.Format("INSERT INTO Skrald (Mængde, Måleenhed, Kategori, Beskrivelse, Ansvarlig, CVR, Tid, AffaldspostID) " +
                    "VALUES('{0}', '{1}' ,'{2}', '{3}', '{4}', '{5}', format(getdate(), 'yyyy-MM-dd hh:mm'), '{6}')",
                    MængdeTxt.Text.Trim(), MåleenhedInt, KategoriInt, BeskrivelseTxt.Text.Trim(),
                    AnsvarligTxt.Text.Trim(), CvrTxt.Text.Trim(), AffaldspostIDTxt.Text.Trim());
                if (MåleenhedInt != 0 && KategoriInt != 0)
                {
                    SqlCommand command = new SqlCommand(cmd, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    MessageBox.Show("Du SKAL vælge en kategori og en måleenhed.");
                }
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