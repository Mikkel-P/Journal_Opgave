using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Journal_Opgave
{
    class Dal
    {
        // ctor
        public Dal() { }

        // Creates a string with the pathway for the .txt files, for less repitition
        private string pathway = @".\HealthClinic";

        #region Public methods
        /// <summary>
        /// Creates a folder called HealthClinic, if already there just continues, then creates
        /// a .txt file with the name of the persons cpr to avoid repititions in filenames.
        /// </summary>
        /// <param name="jInfo"></param>
        /// <returns></returns>
        public bool JournalCreator(string[] jInfo) 
        {
            // Creates a folder, does nothing if folder by that name already exists
            Directory.CreateDirectory(pathway);

            // Copies the jInfo string array to the cpr string.
            string cpr = jInfo[2];

            // Creates a FileStream referance for less repetition
            FileStream patientFile;

            // Try/catch to stop the program from exiting should an exception happen.
            try
            {
                // Creates a .txt file with the persons cpr as name
                patientFile = new FileStream(pathway + @"\" + cpr + ".txt", FileMode.CreateNew);
            }
            catch (IOException)
            {
                return false;
            }

            // Creates a new instance of StreamWriter and gives it the earlier created FileStream
            StreamWriter writer = new StreamWriter(patientFile);

            // Loops through the array and prints it content to the .txt file
            for (int i = 0; i < jInfo.Length; i++)
            {
                writer.WriteLine(jInfo[i]);
            }

            writer.WriteLine("--------------------------------------------");

            writer.Close();

            return true;
        }

        /// <summary>
        /// Finds a .txt file with the given cpr as filename, then adds the JournalEntry object
        /// entry's content to the .txt file.
        /// </summary>
        /// <param name="cpr"></param>
        /// <param name="entry"></param>
        public void AddToFile(string cpr, JournalEntry entry)
        {
            FileStream patientFile = new FileStream(pathway + @"\" + cpr + ".txt", FileMode.Append);

            // Allows us to add to the bottom of an existing file
            StreamWriter writer = new StreamWriter(patientFile);

            // Invariant culture allows to seperate the formatting of DateTime from the OS
            CultureInfo provider = CultureInfo.InvariantCulture;

            writer.WriteLine($"{entry.TimeOfDay.ToString("yyyy/MM/dd HH:mm", provider)} - {entry.DoctorName} - {entry.Description}");

            writer.Close();
        }

        /// <summary>
        /// Finds a .txt file with the given cpr as filename, then opens it so we can read it's contents.
        /// </summary>
        /// <param name="cpr"></param>
        /// <returns></returns>
        public string[] FileLoader(string cpr) 
        {
            FileStream patientFile;

            // Try/catch so the console doesn't exit, if the cpr doesn't match a .txt filename
            try
            {
                patientFile = new FileStream(pathway + @"\" + cpr + ".txt", FileMode.Open);                
            }
            catch (FileNotFoundException)
            {
                throw;
            }

            StreamReader reader = new StreamReader(patientFile);

            List<string> fileList = new List<string>();

            // While loop that adds the content to the fileList list
            while (reader.EndOfStream == false)
            {
                fileList.Add(reader.ReadLine());
            }

            reader.Close();

            // Converts the list to an array and returns it
            return fileList.ToArray();
        }
        #endregion
    }
}
