﻿using System;
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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        /// Martin (Hele Edit)
        public Repository repository = new Repository();
        public SkraldData skraldData;
        public string Mængde { get; set; }
        public string Måleenhed { get; set; }
        public string Kategori { get; set; }
        public string Beskrivelse { get; set; }
        public string Ansvarlig { get; set; }
        public string CVR { get; set; }


        public int KategoriInt;
        public int MåleenhedInt;
        public bool KategoriCheck = false;
        public bool MåleenhedCheck = false;
        public char currentCharacter;
        public bool CvrRequirements = false;
        public bool AnsvarligRequirements = false;
        public bool BeskrivelseRequirements = false;
        public bool MængdeRequirements = false;
        public bool TidRequirements = false;
        public EditWindow(SkraldData data)
        {
            InitializeComponent();

            skraldData = data;
            int KategoriIntConverter = Convert.ToInt32(skraldData.Kategori);
            int MåleenhedIntConverter = Convert.ToInt32(skraldData.Måleenhed);
            CvrTxt.Clear();
            AnsvarligTxt.Clear();
            BeskrivelseTxt.Clear();
            TidTxt.Clear();
            MængdeTxt.Clear();
            this.CvrTxt.Text = skraldData.CVR.Trim();
            this.AnsvarligTxt.Text = skraldData.Ansvarlig.Trim();
            this.BeskrivelseTxt.Text = skraldData.Beskrivelse.Trim();
            this.TidTxt.Text = skraldData.Tid.ToString("yyyy-MM-dd HH:mm").Trim();
            this.MængdeTxt.Text = skraldData.Mængde.Trim();
            this.KategoriComboBox.SelectedIndex = KategoriIntConverter - 1;
            this.MåleenhedComboBox.SelectedIndex = MåleenhedIntConverter - 1;

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

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (CvrTxt.Text.Length == 8)
            {
                CvrRequirements = true;
            }

            if (AnsvarligTxt.Text.Length > 0)
            {
                AnsvarligRequirements = true;
            }

            if (BeskrivelseTxt.Text.Length > 0)
            {
                BeskrivelseRequirements = true;
            }

            if (MængdeTxt.Text.Length > 0)
            {
                MængdeRequirements = true;
            }

            if (TidTxt.Text.Length > 0)
            {
                TidRequirements = true;
            }

            switch (KategoriComboBox.SelectedItem)
            {
                case "Batterier":
                    KategoriInt = 1;
                    KategoriCheck = true;
                    break;
                case "Biler":
                    KategoriInt = 2;
                    KategoriCheck = true;
                    break;
                case "Elektronikaffald":
                    KategoriInt = 3;
                    KategoriCheck = true;
                    break;
                case "Imprægneret træ":
                    KategoriInt = 4;
                    KategoriCheck = true;
                    break;
                case "Inventar":
                    KategoriInt = 5;
                    KategoriCheck = true;
                    break;
                case "Organisk affald":
                    KategoriInt = 6;
                    KategoriCheck = true;
                    break;
                case "Pap og papir":
                    KategoriInt = 7;
                    KategoriCheck = true;
                    break;
                case "Plastemballager":
                    KategoriInt = 8;
                    KategoriCheck = true;
                    break;
                case "PVC":
                    KategoriInt = 9;
                    KategoriCheck = true;
                    break;
                default:
                    break;
            }

            switch (MåleenhedComboBox.SelectedItem)
            {
                case "Colli":
                    MåleenhedInt = 1;
                    MåleenhedCheck = true;
                    break;
                case "Stk.":
                    MåleenhedInt = 2;
                    MåleenhedCheck = true;
                    break;
                case "Ton":
                    MåleenhedInt = 3;
                    MåleenhedCheck = true;
                    break;
                case "Kilogram":
                    MåleenhedInt = 4;
                    MåleenhedCheck = true;
                    break;
                case "Gram":
                    MåleenhedInt = 5;
                    MåleenhedCheck = true;
                    break;
                case "M3":
                    MåleenhedInt = 6;
                    MåleenhedCheck = true;
                    break;
                case "Liter":
                    MåleenhedInt = 7;
                    MåleenhedCheck = true;
                    break;
                case "Hektoliter":
                    MåleenhedInt = 8;
                    MåleenhedCheck = true;
                    break;
                default:
                    break;
            }

            try
            {
                if (KategoriCheck && MåleenhedCheck && CvrRequirements &&
                    AnsvarligRequirements && BeskrivelseRequirements && MængdeRequirements && TidRequirements)
                {
                    repository.Edit(MængdeTxt.Text, MåleenhedInt, KategoriInt, BeskrivelseTxt.Text, AnsvarligTxt.Text
                        , CvrTxt.Text, TidTxt.Text, skraldData.SkraldeID.ToString());
                    MessageBox.Show("Dine ændringer er nu opdateret.");
                    Logger.SaveMessage("Brugeren har redigeret en data i databasen\n" +
                    "Den tidligere data:" +
                    "\nMængde = '" + skraldData.Mængde + "'," +
                    "\nMåleenhed = '" + skraldData.Måleenhed + "'," +
                    "\nKategori = '" + skraldData.Kategori + "'," +
                    "\nBeskrivelse = '" + skraldData.Beskrivelse +
                    "\nAnsvarlig = '" + skraldData.Ansvarlig + "'," +
                    "\nCVR = '" + skraldData.CVR + "'," +
                    "\nTid = '" + skraldData.Tid + "'" +
                    "\n\nDen nye data:" +
                    "\nMængde = '" + MængdeTxt.Text + "'," +
                    "\nMåleenhed = '" + MåleenhedInt + "'," +
                    "\nKategori = '" + KategoriInt + "'," +
                    "\nBeskrivelse = '" + BeskrivelseTxt.Text +
                    "\nAnsvarlig = '" + AnsvarligTxt.Text + "'," +
                    "\nCVR = '" + CvrTxt.Text + "'," +
                    "\nTid = '" + TidTxt.Text + "'" +
                    "\nDenne data blev redigeret");
                }
                else
                {
                    MessageBox.Show("Du skal udfylde alle felterne.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LUK_Click(object sender, RoutedEventArgs e) // Mads
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}