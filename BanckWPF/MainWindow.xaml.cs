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
using DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace BanckWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        DAO dao = new DAO();
        HashCode hc = new HashCode();

        private void closeApp(object sender, MouseButtonEventArgs e)
        {
            
        }

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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register r = new Register();
            r.Show();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if(textbox.Text == "Username")
            {
                textbox.Text = "";
            }
        }

        private void lblPass_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordbox = sender as PasswordBox;
            if (passwordbox.Password == "Password")
            {
                passwordbox.Password = "";
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUser.Text;
            string pass = hc.PassHash(lblPass.Password);

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspLogin";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);

            SqlDataReader dr = null;
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Logged in");
                txtUser.IsEnabled = false;
                lblPass.IsEnabled = false;
                btnNewAcc.IsEnabled = true;
                btnDeposit.IsEnabled = true;
                btnTransfer.IsEnabled = true;
                btnWithdraw.IsEnabled = true;
                


                txtUser.Clear();
                lblPass.Clear();
            }
            else
            {
                MessageBox.Show("Invalid");
            }
            dao.CloseCon();
                        

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            foreach(Window window in Application.Current.Windows)
            {
                if(window != Application.Current.MainWindow)
                {
                    window.Close();
                }
            }

            NewAccount na = new NewAccount();
            na.Show();


        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Application.Current.MainWindow)
                {
                    window.Close();
                }
            }
            Deposit d = new Deposit();
            d.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Application.Current.MainWindow)
                {
                    window.Close();
                }
            }
            Transfer t = new Transfer();
            t.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Application.Current.MainWindow)
                {
                    window.Close();
                }
            }
            Withdraw w = new Withdraw();
            w.Show();
        }
    }
}
