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

        private string pathway = @".\HealthClinic";

        public bool JournalCreator(string[] jInfo) 
        {
            // Creates a folder, does nothing if folder by that name already exists
            Directory.CreateDirectory(pathway);

            string cpr = jInfo[2];

            FileStream patientFile;

            //Could be used when creating a folder and file, so the program doesn't break
            try
            {
                // Creates a .txt file with the persons cpr as name
                patientFile = new FileStream(pathway + @"\" + cpr + ".txt", FileMode.CreateNew);
            }
            catch (IOException)
            {
                return false;
            }

            StreamWriter writer = new StreamWriter(patientFile);

            for (int i = 0; i < jInfo.Length; i++)
            {
                writer.WriteLine(jInfo[i]);
            }

            writer.WriteLine("--------------------------------------------");

            writer.Close();

            return true;
        }

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

        public string[] FileLoader(string cpr) 
        {
            FileStream patientFile;

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

            while (reader.EndOfStream == false)
            {
                fileList.Add(reader.ReadLine());
            }

            reader.Close();

            return fileList.ToArray();
        }
    }
}
