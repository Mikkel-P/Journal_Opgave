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
            bool startMenu = true;
            while (startMenu)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("               Health Clinic Platform");
                Console.WriteLine("==================================================\n");
                Console.WriteLine("1. Create journal");
                Console.WriteLine("2. Load journal");
                Console.WriteLine("3. Exit");
                Console.Write("\r\nEnter a number: ");

                // Switch case for each menu point
                switch (Console.ReadLine())
                {
                    // Creates and loads a journal
                    case "1":
                        CreateJ();
                        break;
                    // Brings forth another menu
                    case "2":
                        journalMenu();
                        break;
                    // Exits the program
                    case "3":
                        startMenu = false;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Creates
        /// </summary>
        public void CreateJ()
        {
            Console.Clear();

            // Collects the user input and saves it to a string
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

            // Creates a string array with 6 indexes
            string[] journalDetails = new string[6];

            // Assigns the earlier collected strings to the newly created string array
            journalDetails[0] = name;
            journalDetails[1] = address;
            journalDetails[2] = cpr;
            journalDetails[3] = email;
            journalDetails[4] = phone;
            journalDetails[5] = prefDoctor;

            // We call our manager class method CreateJournalFile and send our string array to it
            manager.CreateJournalFile(journalDetails);

            LoadJ(cpr);
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

            Console.Write("Write 'EXIT' in all caps when done\nEntry description: ");
            string description = string.Empty;


            while (true)
            {
                string line = Console.ReadLine();
                if (line.Equals("EXIT"))
                {
                    break;
                }
                description += line + "\n";
            }

            manager.CreateEntry(cpr, doctorName, description);
        }

        public void LoadJ(string cpr = "", int indexer = 0)
        {
            if (cpr == "")
            {
                Console.Write("CPR: ");
                cpr = Console.ReadLine();
            }

            Journal JournalViewer = manager.LoadJournalFile(cpr);

            ShowJournal(JournalViewer);
            ShowEntry(JournalViewer, indexer);
            CheckKeyPress(cpr, indexer);
            Console.ReadLine();
        }

        public void journalMenu()
        {
            Console.Clear();
            Console.WriteLine("CPR: ");
            string cpr = Console.ReadLine();
            LoadJ(cpr);
        }

        public void ShowJournal(Journal showInfo)
        {
            Console.Clear();
            Console.WriteLine($"Name: {showInfo.Name}");
            Console.WriteLine($"Address: {showInfo.Address}");
            Console.WriteLine($"CPR: {showInfo.Cpr}");
            Console.WriteLine($"Email: {showInfo.Email}");
            Console.WriteLine($"Phone: {showInfo.Phone}");
            Console.WriteLine($"Preferred doctor: {showInfo.PrefDoctor}");
            Console.WriteLine("--------------------------------------------");
        }

        public void ShowEntry(Journal entryViewer, int e)
        {
            if (entryViewer != null && 0 < entryViewer.JEntry.Count)
            {
                List<JournalEntry> showEntries = entryViewer.JEntry;

                if (e < showEntries.Count && e >= 0)
                {
                    Console.WriteLine($"Doctor: {showEntries[e].DoctorName} - Date: {showEntries[e].TimeOfDay}");
                    Console.WriteLine($"Description: {showEntries[e].Description}");
                }
            }
            else
            {
                Console.WriteLine("No entries found.");
            }
            Console.WriteLine("-----------------------------------------------");

            Console.Write("Press 'ENTER' key to create a new entry.\nPress 'RIGHT ARROW' key to navigate to next journal entry\nPress 'LEFT ARROW' key to navigate to previous journal entry\nPress 'ESC' key to go to previous menu.\n");

        }

        public void CheckKeyPress(string cpr, int indexer)
        {
            ConsoleKey consoleKey = Console.ReadKey().Key;

            // Checks if a certain key is being pressed i.e 'Enter and 'Escape'
            if (consoleKey.Equals(ConsoleKey.Enter))
            {
                CreateE();
            }
            else if (consoleKey.Equals(ConsoleKey.Escape))
            {
                return;
            }
            else if (consoleKey.Equals(ConsoleKey.LeftArrow))
            {
                Console.Clear();
                indexer--;
                LoadJ(cpr, indexer);

            }
            else if (consoleKey.Equals(ConsoleKey.RightArrow))
            {
                Console.Clear();
                indexer++;
                LoadJ(cpr, indexer);
            }
        }
        #endregion
    }
}