using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal_Opgave
{
    class Manager
    {
        JournalConnector jcon = new JournalConnector();

        Dal dal = new Dal();

        // ctor
        public Manager() { }

        public void CreateJournalFile(string[] jInfo)
        {
            dal.JournalCreator(jInfo);
        }

        public void CreateEntry(string cpr, string doctorName, string description)
        {
            Journal currentJournal = LoadJournalFile(cpr);

            jcon.EntryCreation(doctorName, description);

            dal.AddToFile(cpr, currentJournal.JEntry[currentJournal.JEntry.Count-1]);
        }

        public Journal LoadJournalFile(string cpr)
        {
            string[] fileContent = dal.FileLoader(cpr);

            jcon.ContentHandler(fileContent, out string[] journalInfo, out string[] entries);

            jcon.JournalCreation(journalInfo);

            Journal currentJournal = jcon.AddOldEntries(entries);

            return currentJournal;
        }

        public string[] CurrentAge(string cpr)
        {
            return jcon.AgeComparer(cpr);
        }
    }
}
