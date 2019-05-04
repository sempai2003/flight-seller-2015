using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSeller
{
    public class User
    {
        public string m_email { get; private set; }
        public string m_name { get; private set; }
        public string m_surname { get; private set; }
        public Int64 m_passportNo { get; private set; }
        public DateTime m_validityDate { get; private set; }

        public User(string email, string name, string surname, Int64 passportNo, DateTime valDate)
        {
            m_email = email;
            m_name = name;
            m_surname = surname;
            m_passportNo = passportNo;
            m_validityDate = valDate;
        }
    }
}
