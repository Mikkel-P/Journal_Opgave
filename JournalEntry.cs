using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Journal_Opgave
{
    class JournalEntry
    {
        private DateTime timeOfDay;
        private string doctorName;
        private string description;

        public DateTime TimeOfDay
        {
            get 
            {
                return timeOfDay;
            }
            set
            {
                this.timeOfDay = value;
            }
        }
        public string DoctorName
        {
            get
            {
                return doctorName;
            }
            set
            {
                this.doctorName = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                this.description = value;
            }
        }

        // ctor
        public JournalEntry(string doctorName, string description, string date = "")
        {


            if (date == "")
            {
                this.TimeOfDay = DateTime.Now;
            }
            else
            {
                // Invariant culture allows to seperate the formatting of DateTime from the OS
                CultureInfo provider = CultureInfo.InvariantCulture;
                this.TimeOfDay = DateTime.ParseExact(date, "yyyy/MM/dd HH:mm", provider);
            }

            this.DoctorName = doctorName;
            this.Description = description;
        }
    }
}
