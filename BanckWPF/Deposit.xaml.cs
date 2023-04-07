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
using System.Data;
using System.Data.SqlClient;

namespace BanckWPF
{
    /// <summary>
    /// Interaction logic for Deposit.xaml
    /// </summary>
    public partial class Deposit : Window
    {
        public Deposit()
        {
            InitializeComponent();
        }

        DAO dao = new DAO();
        SqlDataReader dr;
        string accNum;
        AddPeople ap = new AddPeople();

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateCombo();
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

        void PopulateCombo()
        {
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspPopCombo";
            cmd.CommandType = CommandType.StoredProcedure;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string acc = dr["AccountNumber"].ToString();
                cboAccNum.Items.Add(acc);
            }
            dao.CloseCon();
        }

        void GetBalance()
        {
            accNum = cboAccNum.SelectedItem.ToString();
            int accNumInt = int.Parse(accNum);// essa linha pq ele pede q isso seja int no db
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspSelBal";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNumInt);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string balx = dr["InitialBalance"].ToString();
                string fn = dr["Firstname"].ToString();
                string sn = dr["Surname"].ToString();
                string cy = dr["County"].ToString();

                lblDisplayBalance.Content = balx;
                lblDisplayName.Content = fn + " " + sn + " From " + cy;
            }
            dao.CloseCon();
        }

        private void cboAccNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetBalance();
        }
        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {

            accNum = cboAccNum.SelectedItem.ToString();
            
            decimal amount = decimal.Parse(txtDeposit.Text);
            decimal bal = decimal.Parse(lblDisplayBalance.Content.ToString());
            decimal newBalance = amount + bal;

            //nao achei forma mais facil de fazer --INICIO
            string fname = " ", sname = " " ; 
            int accNumInt = int.Parse(accNum);

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspSelBal";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNumInt);
            dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                string fn = dr["Firstname"].ToString();
                string sn = dr["Surname"].ToString();

                fname = fn;
                sname = sn;

            }

            dao.CloseCon();

            SqlCommand cmd2 = dao.OpenCon().CreateCommand();//PRECISEI CRIAR cmd2
            cmd2.CommandText = "uspUpdateBalWD";// REVER ESSE STORED PROCEDURE
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd2.Parameters.AddWithValue("newBalance", newBalance);
            cmd2.Parameters.AddWithValue("accNum", accNum);
            cmd2.ExecuteNonQuery();
            dao.CloseCon();


            ap.AddLogdement(accNumInt, fname, sname, amount);// ISSO TBM


            MessageBox.Show("Your account has been updated whit " + amount + "\nYour new balance is: " + newBalance, "Account Update", MessageBoxButton.OK, MessageBoxImage.Information);
            txtDeposit.Clear();



        }

        private void btnDepositMenu_Click(object sender, RoutedEventArgs e)
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

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
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

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
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

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != Application.Current.MainWindow)
                {
                    window.Close();
                }
            }
            Transactions tr = new Transactions();
            tr.Show();
        }
    }
}
