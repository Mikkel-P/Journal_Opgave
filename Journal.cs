using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal_Opgave
{
    class Journal
    {
        // Attributes
        private string name;
        private string address;
        private string cpr;
        private string email;
        private string phone;
        private string prefDoctor;

        private List<JournalEntry> jEntry = new List<JournalEntry>();

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                this.address = value;
            }
        }
        public string Cpr
        {
            get
            {
                return cpr;
            }
            set
            {
                this.cpr = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                this.email = value;
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                this.phone = value;
            }
        }
        public string PrefDoctor
        {
            get
            {
                return prefDoctor;
            }
            set
            {
                this.prefDoctor = value;
            }
        }

        public List<JournalEntry> JEntry
        {
            get
            {
                return jEntry;
            }
            set
            {
                this.jEntry = value;
            }
        }

        // ctor
        public Journal(string[] jInfo)
        {
            this.Name = jInfo[0];
            this.Address = jInfo[1];
            this.Cpr = jInfo[2];
            this.Email = jInfo[3];
            this.Phone = jInfo[4];
            this.PrefDoctor = jInfo[5];
        }

        public void AddJournalEntry(string doctorName, string description, string date = "")
        {
            this.jEntry.Add(new JournalEntry(doctorName, description, date));
        }
    }
}
