using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Journal_Opgave
{
    class JournalConnector
    {
        // A Journal referance
        Journal currentJournal;

        // ctor
        public JournalConnector() { }

        /// <summary>
        /// Creates a new journal with the attributes from the Journal class
        /// </summary>
        /// <param name="jInfo"></param>
        public void JournalCreation(string[] jInfo)
        {
            currentJournal = new Journal(jInfo);
        }

        /// <summary>
        /// Calls on the method AddJournalEntry from the Journal class, and gives it 2 strings
        /// returns the result to the currentJournal
        /// </summary>
        /// <param name="doctorName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public Journal EntryCreation(string doctorName, string description)
        {
            currentJournal.AddJournalEntry(doctorName, description);
            return currentJournal;
        }

        /// <summary>
        /// Splits the entry string into a string array, which we loop through to 
        /// remove unwanted white spaces from the strings.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="doctorName"></param>
        /// <param name="description"></param>
        /// <param name="date"></param>
        public void EntryStringSplitter(string entry, out string doctorName, out string description, out string date)
        {
            // Splits the string to a string array
            string[] entryInfo = entry.Split('-');

            for (int i = 0; i < entryInfo.Length; i++)
            {
                // Removes white spaces at the beginning or end of the string.
                entryInfo[i] = entryInfo[i].Trim();
            }

            date = entryInfo[0];
            doctorName = entryInfo[1];
            description = entryInfo[2];
        }

        /// <summary>
        /// Loops through the entries string array, and calls on the EntryStringSplitter
        /// method to remove unwanted white spaces from the strings. Then calls on the 
        /// method AddJournalEntry from the Journal class.
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        public Journal AddOldEntries(string[] entries)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                EntryStringSplitter(entries[i], out string doctorName, out string description, out string date);

                currentJournal.AddJournalEntry(doctorName, description, date);
            }
            return currentJournal;
        }

        /// <summary>
        /// Seperates the info from the fileContent array into a list and another array, after
        /// the information is split, the wanted information is copied to a string array
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="journalInfo"></param>
        /// <param name="entries"></param>
        public void ContentHandler(string[] fileContent, out string[] journalInfo, out string[] entries)
        {
            // Creates a string array with 6 indexes
            journalInfo = new string[6];

            // Creates an empty string array
            entries = Array.Empty<string>();

            // Creates a list of strings
            List<string> entryList = new List<string>();

            // Loops through the fileContent array, and either adds to the entryList list
            // or to the journalInfo array depending on the index number
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
            // Copies the content of the entryList list to the entries string array.
            entries = entryList.ToArray();
        }

        #region Age methods
        /// <summary>
        /// Breaks the cpr string down, and uses it to calculate a persons exact
        /// birth date, since the cpr only shows the last 2 numbers of the year
        /// the person was born in. Rearranges the numbers so it later can be 
        /// converted to a DateTime format.
        /// </summary>
        /// <param name="cpr"></param>
        /// <returns></returns>
        public string AgeCalculator(string cpr)
        {
            #region CPR Conversion

            // Splits the string into a char array
            char[] cprCalc = cpr.ToCharArray();
            {
                // Saves index 4, 5 and 6 as char variables so they can be manipulated
                char num5 = cprCalc[4];
                char num6 = cprCalc[5];
                char num7 = cprCalc[6];



                // Converts char num5 and char num6 to strings 
                string convert1 = Convert.ToString(num5);
                string convert2 = Convert.ToString(num6);
                string convert3 = Convert.ToString(num7);

                // Melts the two strings into one
                string putTogether = convert1 + convert2;

                // Converts the putTogether string and convert3 string to integers so we can use them in calculations
                int finalCalc1 = Convert.ToInt32(putTogether);
                int finalCalc2 = Convert.ToInt32(convert3);
                #endregion

                #region CPR Calculation
                // The 3 numbers we broke the cpr string down to lets us determine
                // whether a person is for example 104 or 4 years old since the cpr
                // only saves the last 2 digits of the actual birthyear, we can
                // calculate the actual birthyear with the 5th, 6th and 7th digit.
                // When the calculation has been done it adds the result to the
                // char array cprCalc.
                if (finalCalc1 < 37)
                {
                    if (finalCalc2 < 4)
                    {
                        // 1900-1999
                        cprCalc[6] = '1';
                        cprCalc[7] = '9';
                    }
                    else if (finalCalc2 == 4)
                    {
                        // 2000-2036
                        cprCalc[6] = '2';
                        cprCalc[7] = '0';
                    }
                    else if (finalCalc2 > 4)
                    {
                        // 2000-2057
                        cprCalc[6] = '2';
                        cprCalc[7] = '0';
                    }
                    else if (finalCalc2 == 9)
                    {
                        // 2000-2036
                        cprCalc[6] = '2';
                        cprCalc[7] = '0';
                    }
                }
                else if (finalCalc1 > 36 && finalCalc1 < 58)
                {
                    if (finalCalc2 < 4)
                    {
                        // 1900-1999
                        cprCalc[6] = '1';
                        cprCalc[7] = '9';
                    }
                    else if (finalCalc2 == 4)
                    {
                        // 1937-1999
                        cprCalc[6] = '1';
                        cprCalc[7] = '9';
                    }
                    else if (finalCalc2 > 4)
                    {
                        // 2000-2057
                    }
                    else if (finalCalc2 == 9)
                    {
                        // 1937-1999
                        cprCalc[6] = '1';
                        cprCalc[7] = '9';
                    }
                }
                else if (finalCalc1 > 57)
                {
                    if (finalCalc2 < 4)
                    {
                        // 1900-1999
                        cprCalc[6] = '1';
                        cprCalc[7] = '9';
                    }
                    else if (finalCalc2 == 4)
                    {
                        // 1937-1999
                        cprCalc[6] = '1';
                        cprCalc[7] = '9';
                    }
                    else if (finalCalc2 > 4)
                    {
                        // 1858-1899
                        cprCalc[6] = '1';
                        cprCalc[7] = '8';
                    }
                    else if (finalCalc2 == 9)
                    {
                        // 1937-1999
                        cprCalc[6] = '1';
                        cprCalc[7] = '9';
                    }
                }
                #endregion

                #region CPR Rebuild
                // Assigns the selected indexes of the char array cprCalc
                // to char variables to manipulate them further
                char c1 = cprCalc[0];
                char c2 = cprCalc[1];
                char c3 = cprCalc[2];
                char c4 = cprCalc[3];
                char c5 = cprCalc[4];
                char c6 = cprCalc[5];
                char c7 = cprCalc[6];
                char c8 = cprCalc[7];



                // Puts the specified char's back together into a string in the chosen order
                string exactBirthDate = String.Concat(c1, c2, c3, c4, c7, c8, c5, c6);

                // Returns the cpr with the full birthyear, in a regular date format
                // so it can be used for comparison later
                return exactBirthDate;
                #endregion
            }
        }

        /// <summary>
        /// Receives a date from the AgeCalculator method and converts it
        /// to a DateTime variable. Afterwards a new DateTime variable is 
        /// made that holds the current time, then we compare the two, and
        /// get a return with the persons age in years and days.
        /// </summary>
        /// <param name="cpr"></param>
        /// <returns></returns>
        public string[] AgeComparer(string cpr)
        {
            // String that copies the return of the AgeCalculator method
            string birthDate = AgeCalculator(cpr);

            // Invariant culture allows to seperate the formatting of DateTime from the OS format
            CultureInfo provider = CultureInfo.InvariantCulture;

            // DateTime variable from the string we received from the AgeCalculator method
            DateTime birth = DateTime.ParseExact(birthDate, "ddMMyyyy", provider);

            // DateTime variable with the current date and time
            DateTime current = DateTime.Now;

            // DateTime variable that seperates year, month and day to integers so it can be manipulated
            DateTime thisBDay = new DateTime(current.Year, birth.Month, birth.Day);

            // Subtracts the current year from the given birth date and saves it to an int variable
            int years = current.Year - birth.Year;

            // Subtracts the current day from the given birth day and saves it to an int variable
            int days = current.DayOfYear - thisBDay.DayOfYear;

            // Compares the integer variables years and days and returns a 1, 0 or -1 integer
            // which tells us whether the first instance is before, on or after the given date
            int comparer = DateTime.Compare(current.Date, thisBDay.Date);

            // If birthday is later this year, subtract one year, and add 365
            // to get the age in amount of years and days in current year
            if (comparer < 0)
            {
                years -= 1;
                days = 365 + days;
            }

            // String array with two indexes
            string[] ageResult = new string[2];

            // Converts and saves our final results to the string array
            ageResult[0] = days.ToString();
            ageResult[1] = years.ToString();

            return ageResult;
        }
        #endregion
    }
}