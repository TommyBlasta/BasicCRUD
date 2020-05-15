using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BasicCRUD
{
    class DatabaseCommunicator : IDisposable
    {
    
        /// <summary>
        /// Contains the SqlConnection object on which the communicator works.
        /// </summary>
        public SqlConnection SqlConnection {get; private set;}
        public DatabaseCommunicator(SqlConnection sqlConnection)
        {
            SqlConnection = sqlConnection;
        }
        /// <summary>
        /// Accesses and returns the session specified by the parameter.
        /// </summary>
        /// <param name="id">The UID of the session to get from the database.</param>
        /// <returns>Returns the session from the database.</returns>
        public Session GetSession(int id)
        {
            ValidateDtb();
            SqlCommand command = new SqlCommand();
            command.Connection = SqlConnection;
            command.CommandText = "SELECT * FROM [Vzdelavani] WHERE [Id]=@id";
            command.Parameters.AddWithValue("@id", id);
            var reader=command.ExecuteReader();
            reader.Read();
            if((reader[0]).Equals(id))
            {
                var toReturn = new Session((int)reader[0], (Session.Subject)Enum.Parse(typeof(Session),(string)reader[1]), (float)reader[2], (DateTime)reader[3]);
                return toReturn;
            }
            return null; 
        }
        /// <summary>
        /// Loads all sessions from the current database.
        /// </summary>
        /// <returns>List<Session> of all stored sessions in the current database.</returns>
        public List<Session> LoadSessions()
        {
            ValidateDtb();
            List<Session> toReturn = new List<Session>();
            SqlCommand command = new SqlCommand();
            command.Connection = SqlConnection;
            command.CommandText = "SELECT * FROM [Vzdelavani]";
            SqlDataReader reader=command.ExecuteReader();
            while(reader.Read())
            {
                toReturn.Add(new Session((int)reader[0],(Session.Subject)Enum.Parse(typeof(Session.Subject),reader[1].ToString()), float.Parse(reader[2].ToString()), (DateTime)reader[3]));
            }
            return toReturn;
        }
        /// <summary>
        /// Adds the parameter session to the current database.
        /// </summary>
        /// <param name="toAdd">The Session object to add to the database.</param>
        /// <returns>Returns true if successful.</returns>
        public bool AddSession(Session toAdd)
        {
            try
            {
                ValidateDtb();
                SqlCommand command = new SqlCommand();
                command.Connection = SqlConnection;
                command.CommandText = "INSERT INTO [Vzdelavani] ([Subject],[Duration],[Date]) VALUES (@subject,@duration,@date)";
                command.Parameters.AddWithValue("@subject", toAdd.SubjectOfSession);
                command.Parameters.AddWithValue("@duration", toAdd.Duration);
                command.Parameters.AddWithValue("@date", toAdd.Date);
                command.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error has occured:"+ex.ToString());
                return false;
            }
            
        }
        /// <summary>
        /// Edits the session and updates it in the database.
        /// </summary>
        /// <param name="toEdit">Session to edit.</param>
        /// <returns>Returns true if successful.</returns>
        bool EditSession(Session toEdit)
        {

            throw new NotImplementedException();
        }
        /// <summary>
        /// Removes the session from database.
        /// </summary>
        /// <param name="toRemove">Session to remove from the database.</param>
        /// <returns>Returns true if succesful.</returns>
        bool RemoveSession(Session toRemove)
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            this.SqlConnection.Dispose();
        }
        /// <summary>
        /// Checks whether the database is safe to work on.
        /// </summary>
        /// <returns>Return true if the database is safe.</returns>
        private bool ValidateDtb()
        {
            if (!SqlConnection.State.Equals(System.Data.ConnectionState.Open))
            {
                SqlConnection.Open();
                return true;
            }
            return true;
        }
    }
}
