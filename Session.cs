using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCRUD
{
    class Session
    {
        public enum Subject
        {
            Csharp,
            SQL,
            WPF,
            UML,
            ASP
        }
        /// <summary>
        /// Unique ID provided by the SQL database.
        /// </summary>
        public int UID { get; private set; }
        public Subject SubjectOfSession { get; private set; }
        public float Duration { get; private set; }
        public DateTime Date { get; private set; }
        public Session(Subject subject,float duration, DateTime date)
        {
            SubjectOfSession = subject;
            Duration = duration;
            Date = date;
        }
        public Session(int ID,Subject subject, float duration, DateTime date)
        {
            UID = ID;
            SubjectOfSession = subject;
            Duration = duration;
            Date = date;
        }

    }
}
