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
        AddPeople ap = new AddPeople();
        int accNumInt1, accNumInt2, senderSC, receiverSC, receiverAccNum, Amount = 0, over = 0;
        string fname = " ", sname = " ", fname1 = " ", sname1 = " ", accType = " ";
        
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
            FoundsFrom();//add option to transfer for otherbanks
            FoundsTo();
            txtAmount.Clear();
            MessageBox.Show("Founds Transfered!");
            ap.AddTransfer(accNumInt1, fname1, sname1, accType, senderSC, receiverSC, receiverAccNum, Amount, over);
        }

        void FoundsFrom()
        {
            accNum1 = cboFromAccount.SelectedItem.ToString();
            decimal amount = decimal.Parse(txtAmount.Text);
            decimal bal = decimal.Parse(lblDisplayValueFrom.Content.ToString());
            decimal newBalance = bal - amount;

            //nao achei forma mais facil de fazer --INICIO
            int over = 0;//rever overdraft
            string fname1 = " ", sname1 = " ";
            int accNumInt1 = int.Parse(accNum1);

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspSelBal";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNumInt1);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string fn = dr["Firstname"].ToString();
                string sn = dr["Surname"].ToString();
                string type = dr["AccountType"].ToString();

                fname1 = fn;
                sname1 = sn;
                accType = type;

            }

            dao.CloseCon();
            SqlCommand cmd2 = dao.OpenCon().CreateCommand();
            cmd2.CommandText = "uspUpdateBalWD";
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd2.Parameters.AddWithValue("@newBalance", newBalance);
            cmd2.Parameters.AddWithValue("@accNum", accNum1);
            cmd2.ExecuteNonQuery();
            dao.CloseCon();

            ap.AddWithdraw(accNumInt1, fname1, sname1, accType, amount, over);
        }

        void FoundsTo()
        {
            accNum2 = cboToAccount.SelectedItem.ToString();
            decimal amount = decimal.Parse(txtAmount.Text);
            decimal bal = decimal.Parse(lblDisplayValueTo.Content.ToString());
            decimal newBalance = bal + amount;

            //nao achei forma mais facil de fazer --INICIO
            string fname = " ", sname = " ";
            int accNumInt2 = int.Parse(accNum2);

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspSelBal";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNumInt2);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string fn = dr["Firstname"].ToString();
                string sn = dr["Surname"].ToString();

                fname = fn;
                sname = sn;

            }

            dao.CloseCon();

            SqlCommand cmd2 = dao.OpenCon().CreateCommand();
            cmd2.CommandText = "uspUpdateBalWD";
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd2.Parameters.AddWithValue("@newBalance", newBalance);
            cmd2.Parameters.AddWithValue("@accNum", accNum2);
            cmd2.ExecuteNonQuery();
            dao.CloseCon();

            ap.AddLogdement(accNumInt2, fname, sname, amount);//precida da table transfer?
        }
    }
}
