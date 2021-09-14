using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal_Opgave
{
    class JournalConnector
    {
        Journal currentJournal;

        // ctor
        public JournalConnector() { }

        public void JournalCreation(string [] jInfo) 
        {
            currentJournal = new Journal(jInfo);
        }

        public Journal EntryCreation(string doctorName, string description)
        {
            currentJournal.AddJournalEntry(doctorName, description);
            return currentJournal;
        }

        public Journal AddOldEntries(string[] entries)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                EntryStringSplitter(entries[i], out string doctorName, out string description, out string date);

                currentJournal.AddJournalEntry(doctorName, description, date);
            }
            return currentJournal;
        }

        public void EntryStringSplitter(string entry, out string doctorName, out string description, out string date)
        {
            string[] entryInfo = entry.Split('-');

            for (int i = 0; i < entryInfo.Length; i++)
            {
                entryInfo[i].Trim();
            }
            date = entryInfo[0];
            doctorName = entryInfo[1];
            description = entryInfo[2];
        }

        public void ContentHandler(string[] fileContent, out string[] journalInfo, out string[] entries)
        {
            journalInfo = new string[6];
            entries = Array.Empty<string>();

            List<string> entryList = new List<string>();            

            for (int i = 0; i < fileContent.Length; i++)
            {
                if (i < 6)
                {
                    journalInfo[i] = fileContent[i];
                }
                else if (i > 6)
                {
                    entryList.Add(fileContent[i]);
                }
            }
            entries = entryList.ToArray();
        }

        public string AgeCalculator(string cpr)
        {
            #region CPR Breakdown
            // Splits the string into a char array
            char[] cprCalc = cpr.ToCharArray();
            {
                char num5 = cprCalc[4];
                char num6 = cprCalc[5];

                char test3 = (char)(num5 + num6);

                if (test3 < 37)
                {
                    if (cprCalc[6] < 4)
                    {
                        // 1900-1999
                        cprCalc.Append<char>(Convert.ToChar(1));
                        cprCalc.Append<char>(Convert.ToChar(9));
                    }
                    else if (cprCalc[6] == 4)
                    {
                        // 2000-2036
                        cprCalc.Append<char>(Convert.ToChar(2));
                        cprCalc.Append<char>(Convert.ToChar(0));
                    }
                    else if (cprCalc[6] > 4)
                    {
                        // 2000-2057
                        cprCalc.Append<char>(Convert.ToChar(2));
                        cprCalc.Append<char>(Convert.ToChar(0));
                    }
                    else if (cprCalc[6] == 9)
                    {
                        // 2000-2036
                        cprCalc.Append<char>(Convert.ToChar(2));
                        cprCalc.Append<char>(Convert.ToChar(0));
                    }
                }

                else if (test3 > 36 && test3 < 58)
                {
                    if (cprCalc[6] < 4)
                    {
                        // 1900-1999
                        cprCalc.Append<char>(Convert.ToChar(1));
                        cprCalc.Append<char>(Convert.ToChar(9));
                    }
                    else if (cprCalc[6] == 4)
                    {
                        // 1937-1999
                        cprCalc.Append<char>(Convert.ToChar(1));
                        cprCalc.Append<char>(Convert.ToChar(9));
                    }
                    else if (cprCalc[6] > 4)
                    {
                        // 2000-2057
                    }
                    else if (cprCalc[6] == 9)
                    {
                        // 1937-1999
                        cprCalc.Append<char>(Convert.ToChar(1));
                        cprCalc.Append<char>(Convert.ToChar(9));
                    }
                }

                else if (test3 > 57)
                {
                    if (cprCalc[6] < 4)
                    {
                        // 1900-1999
                        cprCalc.Append<char>(Convert.ToChar(1));
                        cprCalc.Append<char>(Convert.ToChar(9));
                    }
                    else if (cprCalc[6] == 4)
                    {
                        // 1937-1999
                        cprCalc.Append<char>(Convert.ToChar(1));
                        cprCalc.Append<char>(Convert.ToChar(9));
                    }
                    else if (cprCalc[6] > 4)
                    {
                        // 1858-1899
                        cprCalc.Append<char>(Convert.ToChar(1));
                        cprCalc.Append<char>(Convert.ToChar(8));
                    }
                    else if (cprCalc[6] == 9)
                    {
                        // 1937-1999
                        cprCalc.Append<char>(Convert.ToChar(1));
                        cprCalc.Append<char>(Convert.ToChar(9));
                    }
                }
            }
            #endregion

            #region CPR Rebuild
            char c1 = cprCalc[0];
            char c2 = cprCalc[1];
            char c3 = cprCalc[2];
            char c4 = cprCalc[3];
            char c5 = cprCalc[4];
            char c6 = cprCalc[5];
            char c7 = cprCalc[10];
            char c8 = cprCalc[11];

            string cprResult = Convert.ToString(c1 + c2 + c3 + c4 + c7 + c8 + c5 + c6);

            // Returns the cpr with the full birthyear, and without the usual final four numbers
            return cprResult;
            #endregion
        }

        public string[] AgeComparer(string cpr)
        {
            string birthDate = AgeCalculator(cpr);

            DateTime birth = DateTime.ParseExact(birthDate, "ddMMyyyy", null);
            DateTime today = DateTime.Now;

            DateTime thisBDay = new DateTime(today.Year, birth.Month, birth.Day);

            int years = today.Year - birth.Year;
            int days = today.DayOfYear - thisBDay.DayOfYear;

            int comparer = DateTime.Compare(today.Date, thisBDay.Date);

            // If birthday is later this year, subtract one year, and add 365 to get age in years and amount of days in curent year
            if (comparer < 0)
            {
                years -= 1;
                days = 365 + days;
            }

            string[] ageResult = new string[2];

            ageResult[0] = days.ToString();
            ageResult[1] = years.ToString();

            return ageResult;
        }
    }
}