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
            sys.StartMenu();
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
        public void StartMenu()
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
                    // Creates a journal
                    case "1":
                        CreateJ();
                        break;
                    // Loads a journal
                    case "2":
                        LoadInterface();
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
        /// To load a journal we need a string with a cpr, and check
        /// if we have a matching .txt file with that filename
        /// </summary>
        public void LoadInterface()
        {
            Console.Clear();
            Console.WriteLine("CPR: ");
            string cpr = Console.ReadLine();
            LoadJ(cpr);
        }

        /// <summary>
        /// Saves the user inputs as seperate strings, then creates a string array, that can be put into the Journal object
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

            Console.WriteLine("\nJournal was succesfully created. Press 'ENTER' to return to main menu.");
            Console.ReadLine();
        }

        /// <summary>
        /// Saves the user inputs as seperate strings, afterwards sends it to the Manager class method CreateEntry
        /// </summary>
        public void CreateE()
        {
            Console.Clear();

            // Writes the current date and time to the console
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

            Console.Write("Patient's CPR: ");
            string cpr = Console.ReadLine();

            // Calls on the Manager class method LoadJournalFile and sends the cpr string to it
            // so we can identify the correct filename
            manager.LoadJournalFile(cpr);

            Console.Write("Name of responsible doctor: ");
            string doctorName = Console.ReadLine();

            Console.Write("Write 'EXIT' in all caps when done\nEntry description: \n");
            string description = string.Empty;

            // Checks if the user input equals EXIT, breaks out of the loop if it is
            // allows the user to use enter to get several lines of text
            while (true)
            {
                string line = Console.ReadLine();
                if (line.Equals("EXIT"))
                {
                    break;
                }
                // Adds the line string to the description string, and adds a new line to the console window
                description += line + "\n";
            }

            // Calls on the Manager class method CreateEntry and sends the just created strings to it
            manager.CreateEntry(cpr, doctorName, description);
        }

        /// <summary>
        /// Sends a string called cpr to the Manager class method LoadJournalFile, which returns a Journal object which
        /// is used to show both journal info and a single entry, and checks for specific key presses for menu navigation
        /// </summary>
        /// <param name="cpr"></param>
        /// <param name="indexer"></param>
        public void LoadJ(string cpr = "", int indexer = 0)
        {
            if (cpr == "")
            {
                Console.Write("CPR: ");
                cpr = Console.ReadLine();
            }

            Journal journalViewer = manager.LoadJournalFile(cpr);

            ShowJournal(journalViewer);
            ShowEntry(journalViewer, indexer);
            CheckKeyPress(cpr, indexer);
        }

        /// <summary>
        /// Displays journal information residing in the showInfo Journal object
        /// </summary>
        /// <param name="showInfo"></param>
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

        /// <summary>
        /// Displays entry information residing in the showInfo Journal object
        /// </summary>
        /// <param name="entryViewer"></param>
        /// <param name="e"></param>
        public void ShowEntry(Journal entryViewer, int indexer)
        {
            // Checks whether any entries reside in the entryViewer Journal object
            if (entryViewer != null && entryViewer.JEntry.Count > 0)
            {
                // 
                List<JournalEntry> showEntries = entryViewer.JEntry;

                if (indexer < showEntries.Count && indexer >= 0)
                {
                    Console.WriteLine($"Doctor: {showEntries[indexer].DoctorName} - Date: {showEntries[indexer].TimeOfDay}");
                    Console.WriteLine($"Description: {showEntries[indexer].Description}");
                }
            }
            else
            {
                Console.WriteLine("No entries found.");
            }
            Console.WriteLine("-----------------------------------------------");

            Console.Write
                ("Press 'ENTER' key to create a new entry.\n" +
                 "Press 'RIGHT ARROW' key to navigate to next journal entry\n" +
                 "Press 'LEFT ARROW' key to navigate to previous journal entry\n" +
                 "Press 'ESC' key to go to previous menu.\n");

        }

        /// <summary>
        /// Checks if the 'Enter', 'Escape', 'LeftArrow' or 'RightArrow' key is being pressed
        /// </summary>
        /// <param name="cpr"></param>
        /// <param name="indexer"></param>
        public void CheckKeyPress(string cpr, int indexer)
        {
            // Saves the last pressed key to a ConsoleKey variable so we can compare it to specific keys
            ConsoleKey consoleKey = Console.ReadKey().Key;


            // If 'Enter' was the key, it calls on the CreateE method
            if (consoleKey.Equals(ConsoleKey.Enter))
            {
                CreateE();
            }
            // If 'Escape' was the key, it returns us to the previous menu
            else if (consoleKey.Equals(ConsoleKey.Escape))
            {
                return;
            }
            // If 'LeftArrow' was the key, our indexer variable subtracts 1, and shows the previous index
            else if (consoleKey.Equals(ConsoleKey.LeftArrow))
            {
                Console.Clear();
                indexer--;
                LoadJ(cpr, indexer);

            }
            // If 'RightArrow' was the key, our indexer variable counts 1 up, and shows the next index
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