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
using DAL;

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

        AddPeople ap;

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
            string add1 = txtAdd1.Text;
            string add2 = txtAdd2.Text;
            string city = txtCity.Text;
            string cy = cboCy.SelectedItem.ToString();

            /*Nao sei se ta certo esse if statment (conferir data type of 50 em dec ou cents)*/
            decimal bal = decimal.Parse(txtInicialBal.Text);
            if (bal < 50)
            {
                MessageBox.Show("Invalid Amount");
            }

            /* RDOS NAO ESTAO FUNCIONANDO CHECAR O METHOD ISCHECKED 
            string acctype = "Current";
            if (rdoSavings.IsChecked)
            {
                acctype = "Savings";
            }
            */

            //Call a method
            //Add o resto dos dados a serem enseridos na DB
            //ap.AddNewAcc(fn, sn,);

            //Tidy up
            txtFn.Clear();
            txtSn.Clear();
        }
    }
}
