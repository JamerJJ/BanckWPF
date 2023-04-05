using DAL;
using System;
using System.Collections.Generic;
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

namespace BanckWPF
{
    /// <summary>
    /// Interaction logic for Transfer.xaml
    /// </summary>
    public partial class Transfer : Window
    {
        public Transfer()
        {
            InitializeComponent();
        }

        DAO dao = new DAO();
        SqlDataReader dr;
        string accNum1, accNum2;
        decimal toDecimal, fromDecimal;// need bc the fields are concatonated with strings and i dont want to change the UI

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
                cboFromAccount.Items.Add(acc);
                cboToAccount.Items.Add(acc);
            }
            dao.CloseCon();
        }

        void GetBalance()
        {
            accNum1 = cboFromAccount.SelectedItem.ToString();
            int accNumInt = int.Parse(cboFromAccount.SelectedItem.ToString());// essa linha pq ele pede q isso seja int no db
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspSelBal";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNumInt);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string balx = dr["InitialBalance"].ToString();
                decimal fromDecimal = decimal.Parse(balx);
                string fn = dr["Firstname"].ToString();
                string sn = dr["Surname"].ToString();
                string cy = dr["County"].ToString();

                lblDisplayFromAcc.Content = fn + " " + sn + " From " + cy + "\n Balance: " + balx;
            }
            dao.CloseCon();
        }

        private void cboFromAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetBalance();
        }

        private void cboToAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetBalance1();
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateCombo();
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            FoundsFrom();
            FoundsTo();
            MessageBox.Show("Transfer worked!");
            //clear stuff
        }

        void GetBalance1()
        {
            accNum2 = cboFromAccount.SelectedItem.ToString();
            int accNumInt = int.Parse(cboToAccount.SelectedItem.ToString());// essa linha pq ele pede q isso seja int no db
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspSelBal";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNumInt);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string balx = dr["InitialBalance"].ToString();
                decimal toDecimal = decimal.Parse(balx);
                string fn = dr["Firstname"].ToString();
                string sn = dr["Surname"].ToString();
                string cy = dr["County"].ToString();
                
                lblDisplayToAcc.Content = fn + " " + sn + " From " + cy + "\n Balance: " + balx;
            }
            dao.CloseCon();
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            Deposit d = new Deposit();
            d.Show();
        }

        private void btnTransferMenu_Click(object sender, RoutedEventArgs e)
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

        void FoundsFrom()
        {
            accNum1 = cboFromAccount.SelectedItem.ToString();
            decimal amount = decimal.Parse(txtAmount.Text);
            decimal bal = fromDecimal;
            decimal newBalance = bal - amount;

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspUpdateBalWD";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@newBalance", newBalance);
            cmd.Parameters.AddWithValue("@accNum", accNum1);

            cmd.ExecuteNonQuery();
            dao.CloseCon();
        }
        void FoundsTo()
        {
            accNum2 = cboToAccount.SelectedItem.ToString();
            decimal amount = decimal.Parse(txtAmount.Text);
            decimal bal = toDecimal;//this is here so i dont have to change the UI
            decimal newBalance = bal + amount;

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspUpdateBalWD";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@newBalance", newBalance);
            cmd.Parameters.AddWithValue("@accNum", accNum2);

            cmd.ExecuteNonQuery();
            dao.CloseCon();
        }
    }
}
