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
using System.Windows.Media.Animation;
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

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateCombo();
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

        private void cboFromAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetBalance();
        }

        private void cboToAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetBalance1();
        }

        void GetBalance()
        {
            accNum1 = cboFromAccount.SelectedItem.ToString();
            int accNumInt = int.Parse(accNum1);// essa linha pq ele pede q isso seja int no db
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
                lblDisplayValueFrom.Content = balx;
                lblDisplayFromAcc.Content = fn + " " + sn + " From " + cy;
            }
            dao.CloseCon();
        }

        void GetBalance1()
        {
            accNum1 = cboToAccount.SelectedItem.ToString();

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspSelBal";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNum1);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string balx = dr["InitialBalance"].ToString();
                string fn = dr["Firstname"].ToString();
                string sn = dr["Surname"].ToString();
                string cy = dr["County"].ToString();
                lblDisplayValueTo.Content = balx;
                lblDisplayToAcc.Content = fn + " " + sn + " From " + cy;
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

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            FoundsFrom();
            FoundsTo();
            txtAmount.Clear();
            MessageBox.Show("amem");
        }

        void FoundsFrom()
        {
            accNum1 = cboFromAccount.SelectedItem.ToString();
            decimal amount = decimal.Parse(txtAmount.Text);
            decimal bal = decimal.Parse(lblDisplayValueFrom.Content.ToString());
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
            decimal bal = decimal.Parse(lblDisplayValueTo.Content.ToString());
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
