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

        public void AddNewAcc(string fn, string sn, string email, string ph, string city, string cy, int accType, int accNum, int sc, decimal iniBal, string adr1, string adr2)
        {
            //string insert = "INSERT INTO tbNewAccount(Firstname, Surname, Email, Phone, City, County, AccountType, AccountNumber, SortCode, InicialBalance, Address1, Address2) VALUES (@fn, @sn, @email, @ph, @city, @cy, @accType, @accNum, @sc, @iniBal, @adr1, @adr2)";
            //SqlCommand cmd = new SqlCommand(insert, OpenCon());

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspAddAccount";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@sn", sn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@ph", ph);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@cy", cy);
            cmd.Parameters.AddWithValue("@accType", accType);
            cmd.Parameters.AddWithValue("@accNum", accNum);
            cmd.Parameters.AddWithValue("@sc", sc);
            cmd.Parameters.AddWithValue("@iniBal", iniBal);
            cmd.Parameters.AddWithValue("@adr1", adr1);
            cmd.Parameters.AddWithValue("@adr2", adr2);

            cmd.ExecuteNonQuery();
            CloseCon();
        }





    }
}
