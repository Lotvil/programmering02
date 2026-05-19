using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace CarGUI
{
    internal class Database
    {
        public SQLiteConnection dbConn;
        string databaseFilename = "./car.db";

        public Database()
        {
            dbConn = new SQLiteConnection("Data Source=" + databaseFilename);

            if (!File.Exists(databaseFilename))
            {
                SQLiteConnection.CreateFile(databaseFilename);
            }
        }

        /// <summary>
        /// Om dbConn inte är öppen så öppna den
        /// </summary>
        public void OpenConnection()
        {
            if (dbConn.State != System.Data.ConnectionState.Open)
            {
                dbConn.Open();
            }
        }

        /// <summary>
        /// Om dbConn inte är stängd så stäng den
        /// </summary>
        public void CloseConnection()
        {
            if (dbConn.State != System.Data.ConnectionState.Closed)
            {
                dbConn.Close();
            }
        }
    }
}
