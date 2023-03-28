using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    class Person
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public int AccountType { get; set; }
        public string AccountNumber { get; set; }
        public int SortCode { get; set; }
        public int InitialBalance { get; set; }
        public double OverdraftLimit { get; set; }



        public Person(string fn, string sn, string email, int ph, string adr1, string adr2, string city, string cy, int accType, string accNum, int sc, int inicBal, double overLimit)
        {
            Firstname = fn;
            Surname = sn;
            Email = email;
            Phone = ph;
            Address1 = adr1;
            Address2 = adr2;
            City = city;
            County = cy;
            AccountType = accType;
            AccountNumber = accNum;
            SortCode = sc;
            InitialBalance = inicBal;
            OverdraftLimit = overLimit;
            
            
        }

    }
}
