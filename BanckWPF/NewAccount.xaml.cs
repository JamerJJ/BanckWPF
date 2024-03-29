﻿using System;
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
using DAL;
using System.Data;

namespace BanckWPF
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {
        public NewAccount()
        {
            InitializeComponent();
        }

        AddPeople ap =  new AddPeople();

        private void Close_App_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtInicialBal_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text == "Minimum of €50,00")
            {
                textbox.Text = "";
            }
        }

        private void btnNewAccount_Click(object sender, RoutedEventArgs e)
        {
            string fn = txtFn.Text;
            string sn = txtSn.Text;
            string email = txtEmail.Text;
            string ph = txtPh.Text;
            string adr1 = txtAdd1.Text;
            string adr2 = txtAdd2.Text;
            string city = txtCity.Text;
            string cy = cboCy.SelectedItem.ToString();
            int sc = 101010;
            int overD = 0;                                   
            
            string accType = "Savings";
            if (rdoCurrent.IsChecked == true)
            {
                accType = "Current";
            }

            int.TryParse(txtOverAmt.Text, out overD);

            int iniBal = 50;
            int currentValue = int.Parse(txtInicialBal.Text);

            if(currentValue < iniBal)
            {
                MessageBox.Show("Amount less than 50");
                return;
            }
                       
                     
                        
            //Call a method
            //Add o resto dos dados a serem enseridos na DB
            ap.AddNewAcc(fn, sn, email, ph, city, cy, accType, sc, iniBal, adr1, adr2, overD);
            

            //Tidy up
            txtFn.Clear();
            txtSn.Clear();
            txtFn.Clear();
            txtSn.Clear();
            txtEmail.Clear();
            txtPh.Clear();
            txtAdd1.Clear();
            txtAdd2.Clear();
            txtCity.Clear();
            txtInicialBal.Clear();
            txtOverAmt.Clear();

            MessageBox.Show("New Account Created!");
        }

        private void cboCy_Loaded(object sender, RoutedEventArgs e)
        {
            cboCy.ItemsSource = Enum.GetValues(typeof(County));
        }

        private void rdoSavings_Click(object sender, RoutedEventArgs e)
        {
            txtOverAmt.IsEnabled = false;
        }

        private void rdoCurrent_Click(object sender, RoutedEventArgs e)
        {
            txtOverAmt.IsEnabled = true;
        }
    }
}
