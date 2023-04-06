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

        public void AddNewAcc(string fn, string sn, string email, string ph, string city, string cy, string accType, int sc, decimal iniBal, string adr1, string adr2)
        {

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
            cmd.Parameters.AddWithValue("@sc", sc);
            cmd.Parameters.AddWithValue("@iniBal", iniBal);
            cmd.Parameters.AddWithValue("@adr1", adr1);
            cmd.Parameters.AddWithValue("@adr2", adr2);

            cmd.ExecuteNonQuery();
            CloseCon();
        }
        public void AddTransfer(int accNum, string fn, string sn, string accType, int senderSC, int receiverSC, int receiverAccNum, decimal iniBal, int over)
        {

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspAddTransfer";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNum);
            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@sn", sn);
            cmd.Parameters.AddWithValue("@accType", accType);
            cmd.Parameters.AddWithValue("@senderSC", senderSC);
            cmd.Parameters.AddWithValue("@receiverSC", receiverSC);
            cmd.Parameters.AddWithValue("@receiverAccNum", receiverAccNum);
            cmd.Parameters.AddWithValue("@iniBal", iniBal);
            cmd.Parameters.AddWithValue("@over", over);

            cmd.ExecuteNonQuery();
            CloseCon();
        }

        public void AddWithdraw(int accNum, string fn, string sn, string accType, decimal iniBal, int over)
        {

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspAddWithdraw";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNum);
            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@sn", sn);
            cmd.Parameters.AddWithValue("@accType", accType);
            cmd.Parameters.AddWithValue("@iniBal", iniBal);
            cmd.Parameters.AddWithValue("@over", over);

            cmd.ExecuteNonQuery();
            CloseCon();
        }
        public void AddLogdement(int accNum, string fn, string sn, decimal iniBal)
        {

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspAddLodgement";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNum);
            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@sn", sn);
            cmd.Parameters.AddWithValue("@iniBal", iniBal);

            cmd.ExecuteNonQuery();
            CloseCon();
        }




    }
}
