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
        public string Phone { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string AccountType { get; set; }
        public int SortCode { get; set; }
        public decimal InitialBalance { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }



        public Person(string fn, string sn, string email, string ph, string city, string cy, string accType, int sc, decimal iniBal, string adr1, string adr2)
        {
            Firstname = fn;
            Surname = sn;
            Email = email;
            Phone = ph;            
            City = city;
            County = cy;
            AccountType = accType;
            SortCode = sc;
            InitialBalance = iniBal;
            Address1 = adr1;
            Address2 = adr2;


        }

    }
}
