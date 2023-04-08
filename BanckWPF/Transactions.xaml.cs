using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace BanckWPF
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : Window
    {
        public Transactions()
        {
            InitializeComponent();
        }

        DAO dao = new DAO();
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Binding bs = new Binding();
        SqlCommandBuilder sb;

        private void btnAllAcc_Click(object sender, RoutedEventArgs e)
        {
            Binding bs = new Binding();

            da = new SqlDataAdapter();
            dt = new DataTable();

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspAllAccounts";
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            bs.Source = dt;

            dgvTransactions.ItemsSource = dt.DefaultView;
            dao.CloseCon();

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Binding bs = new Binding();

            //da = new SqlDataAdapter();
            //dt = new DataTable();

            //string tranType = cboSearch.SelectedItem.ToString();

            //SqlCommand cmd = dao.OpenCon().CreateCommand();
            //cmd.CommandText = "uspTransactionType";
            //cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@tranType", tranType);

            //da.SelectCommand = cmd;
            //da.Fill(dt);

            //dgvTransactions.ItemsSource = dt.DefaultView;
            //da.Update(dt);
            //dao.CloseCon();
            dgvTransactions.DataContext = Show();

        }

        
        DataTable Show()
        {
            da = new SqlDataAdapter();
            dt = new DataTable();

            string tranType = cboSearch.SelectedItem.ToString();
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspTransactionType";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tranType", tranType);

            dao.OpenCon();
            da.SelectCommand = cmd;
            da.Fill(dt);
            dgvTransactions.ItemsSource = dt.DefaultView;
            da.Update(dt);

            dao.CloseCon();

            return dt;

        }
        private void btnAllTran_Click(object sender, RoutedEventArgs e)
        {
            Binding bs = new Binding();
            da = new SqlDataAdapter();
            dt = new DataTable();

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspAllTransactions";
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            bs.Source = dt;

            dgvTransactions.ItemsSource = dt.DefaultView;
            dao.CloseCon();

            dao.CloseCon();
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

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            sb = new SqlCommandBuilder(da);
            da.Update(dt);
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void cboSearch_Loaded(object sender, RoutedEventArgs e)
        {
            string[] comboItems = new string[] { "New Account", "Deposit", "Withdraw", "Transfer" };
            cboSearch.ItemsSource = comboItems;
        }

        private void dgvTransactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }
    }
}
