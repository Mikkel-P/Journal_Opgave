using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal_Opgave
{
    class Manager
    {
        // Creates an instance of the JournalConnector class
        JournalConnector jcon = new JournalConnector();

        // Creates an instance of the Dal class
        Dal dal = new Dal();

        // ctor
        public Manager() { }

        /// <summary>
        /// Calls on the JournalCreator method from the Dal class.
        /// </summary>
        /// <param name="jInfo"></param>
        public void CreateJournalFile(string[] jInfo)
        {
            dal.JournalCreator(jInfo);
        }

        /// <summary>
        /// Gives the cpr string to the LoadJournalFile method, which finds the correct
        /// .txt that matches the cpr for us to add an entry to. Creates the entry 
        /// through the EntryCreation method from the JournalConnector class. Finally
        /// adds the information to the .txt file that matches the cpr.
        /// </summary>
        /// <param name="cpr"></param>
        /// <param name="doctorName"></param>
        /// <param name="description"></param>
        public void CreateEntry(string cpr, string doctorName, string description)
        {
            Journal currentJournal = LoadJournalFile(cpr);

            jcon.EntryCreation(doctorName, description);

            dal.AddToFile(cpr, currentJournal.JEntry[currentJournal.JEntry.Count-1]);
        }

        /// <summary>
        /// Creates a string array with the return of the FileLoader method of the Dal class
        /// Calls on the ContentHandler and JournalCreation methods, and then sets the 
        /// currentJournal object to the return of the AddOldEntries method from the 
        /// JournalConnector class, and returns the currentJournal.
        /// </summary>
        /// <param name="cpr"></param>
        /// <returns></returns>
        public Journal LoadJournalFile(string cpr)
        {
            string[] fileContent = dal.FileLoader(cpr);

            jcon.ContentHandler(fileContent, out string[] journalInfo, out string[] entries);

            jcon.JournalCreation(journalInfo);

            Journal currentJournal = jcon.AddOldEntries(entries);

            return currentJournal;
        }

        /// <summary>
        /// Calls the JournalConnector method AgeComparer and feeds it the cpr string.
        /// Then returns the result.
        /// </summary>
        /// <param name="cpr"></param>
        /// <returns></returns>
        public string[] CurrentAge(string cpr)
        {
            return jcon.AgeComparer(cpr);
        }
    }
}
