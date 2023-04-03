using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class AddPeople : DAO
    {
        /*Methods for Add manager and user*/

        public void AddNewAcc(string fn, string sn, string email, string ph, string adr1, string adr2, string city, string cy, int accType, int accNum, int sc, decimal iniBal)
        {
            string insert = "INSERT INTO tbNewAccount(Firstname, Surname, Email, Phone, Address1, Address2, City, County, AccountType, AccountNumber, SortCode, InicialBalance) VALUES (@fn, @sn, @email, @ph, @adr1, @adr2, @city, @cy, @accType, @accNum, @sc, @inicBal)";
            SqlCommand cmd = new SqlCommand(insert, OpenCon());

            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@sn", sn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@ph", ph);
            cmd.Parameters.AddWithValue("@adr1", adr1);
            cmd.Parameters.AddWithValue("@adr2", adr2);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@cy", cy);
            cmd.Parameters.AddWithValue("@accType", accType);
            cmd.Parameters.AddWithValue("@accNum", accNum);
            cmd.Parameters.AddWithValue("@sc", sc);
            cmd.Parameters.AddWithValue("@inicBal", iniBal);

            cmd.ExecuteNonQuery();
            CloseCon();
        }





    }
}
