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
            da = new SqlDataAdapter();
            dt = new DataTable();

            string accNumber = cboSearch.SelectedItem.ToString();// cbo vai ter os numeros das contas 

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspAccNumber"; //Criar um stored procedure que pegue os dados de uma conta de acordo com o numero dela(whatsapp)
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNumber", accNumber);

            da.SelectCommand = cmd;
            da.Fill(dt);
            bs.Source = dt;
            dgvTransactions.DataContext = bs;

            dao.CloseCon();
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

            dgvTransactions.DataContext = dt;

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
            Deposit d = new Deposit();
            d.Show();
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            Transfer t = new Transfer();
            t.Show();
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            Withdraw w = new Withdraw();
            w.Show();
        }

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            Transactions tr = new Transactions();
            tr.Show();
        }
    }
}
