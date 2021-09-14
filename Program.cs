using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Journal_Opgave
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates an instance of the System class
            System sys = new System();
            // We call our controller method from our System class
            sys.Controller();
        }
    }

    // Made a new class so all the methods don't have to be static
    class System
    {
        // Creates an instance of the Manager class
        Manager manager = new Manager();

        #region Public methods
        /// <summary>
        /// The GUI's start menu
        /// </summary>
        public void Controller()
        {
            // Bool to control the menu
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("               Health Clinic Platform");
                Console.WriteLine("==================================================\n");
                Console.WriteLine("1. Create journal");
                Console.WriteLine("2. Load journal");
                Console.WriteLine("3. Exit");
                Console.Write("\r\nEnter a number: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        CreateJ();
                        LoadJ();
                        break;
                    case "2":
                        journalMenu();
                        break;
                    case "3":
                        showMenu = false;
                        break;
                    default:
                        break;
                }
            }
        }
                
        public void CreateJ()
        {
            Console.Clear();

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("CPR: ");
            string cpr = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone number: ");
            string phone = Console.ReadLine();

            Console.Write("Preferred doctor: ");
            string prefDoctor = Console.ReadLine();

            string[] journalDetails = new string[6];

            journalDetails[0] = name;
            journalDetails[1] = address;
            journalDetails[2] = cpr;
            journalDetails[3] = email;
            journalDetails[4] = phone;
            journalDetails[5] = prefDoctor;
            
            manager.CreateJournalFile(journalDetails);

            LoadJ();
        }

        public void CreateE()
        {
            Console.Clear();

            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

            Console.Write("CPR: ");
            string cpr = Console.ReadLine();

            manager.LoadJournalFile(cpr);

            Console.Write("Name of doctor: ");
            string doctorName = Console.ReadLine();

            Console.Write("Write 'END' when done\n Entry description: ");
            string description = string.Empty;


            while (true)
            {
                string line = Console.ReadLine();
                if (line.Equals("END"))
                {
                    break;
                }
                description += line + "\n";
            }            

            manager.CreateEntry(cpr, doctorName, description);
        }

        public void LoadJ(string cpr = "")
        {
            if (cpr == "")
            {
                Console.Write("CPR: ");
                cpr = Console.ReadLine();
            }

            manager.LoadJournalFile(cpr);
            manager.CurrentAge(cpr);
        }

        public void journalMenu()
        {
            // Bool to control the menu
            bool journalMenu = true;
            while (journalMenu)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("               Health Clinic Platform");
                Console.WriteLine("==================================================\n");
                Console.WriteLine("1. Create entry");
                Console.WriteLine("2. Load and view entries");
                Console.WriteLine("3. Exit");
                Console.Write("\r\nEnter a number: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        LoadJ();
                        CreateE();
                        break;
                    case "2":
                        LoadJ();
                        break;
                    case "3":
                        journalMenu = false;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}