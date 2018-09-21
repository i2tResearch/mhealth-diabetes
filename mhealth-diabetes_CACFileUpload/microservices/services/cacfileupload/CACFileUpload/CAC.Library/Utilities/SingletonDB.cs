using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAC.Library.Utilities
{
    public class SingletonDB
    {
        /// <summary>
        /// Data base entity
        /// </summary>
        private Model.DB.ModelContainer db;
        /// <summary>
        /// Return a Context Database EF.
        /// </summary>
        public Model.DB.ModelContainer DB
        {
            get { return db; }
            private set { db = value; }
        }
        /// <summary>
        /// ControllerDB instance
        /// </summary>
        private static volatile SingletonDB instance = null;

        /// <summary>
        /// Allowed databases to connect
        /// </summary>
        public enum DBNames
        {

            CACTesterDB = 0,
            CACProduccion = 1
        }

        private static string dbname = DBNames.CACTesterDB.ToString();
        /// <summary>
        /// Allows to get the default database name (SNDTester) or set for another one enum on DBNames
        /// </summary>
        public static string DBName
        {
            get { return dbname; }
            set { dbname = value; }
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        private SingletonDB()
        {
            Connect();
        }
        /// <summary>
        /// Gets an unique instance of SingletonDB.
        /// </summary>
        /// <returns>A single instance of SingletonDB</returns>
        public static SingletonDB getInstance()
        {
            instance = new SingletonDB();
            return instance;
        }

        /// <summary>
        /// Connect to data base.
        /// </summary>
        private void Connect()
        {
            try
            {
                string conn = GetStringConnection(DBName);
                db = new Model.DB.ModelContainer();
            }
            catch (Exception ex)
            {
                IOUtilities.WriteExceptionLog(ex, Configuration.GetClassName<SingletonDB>());
            }
        }

        private static string GetStringConnection(string dbname)
        {
            // Specify the provider name, server and database.
            string metadata = "";
            string providerName = "";
            string serverName = "";
            string databaseName = "";
            string userid = "";
            string password = "";
            string port = "";

            switch (dbname)
            {
               case "CACTesterDB":

                    metadata = @"res://*/Model.DB.Model.csdl|res://*/Model.DB.Model.ssdl|res://*/Model.DB.Model.msl";
                    providerName = "MySql.Data.MySqlClient";
                    port = "";
                    serverName = "";
                    databaseName = "";
                    userid = "";
                    password = "";
                    break;

                case "CACProduccion":
                    metadata = @"res://*/Model.DB.Model.csdl|res://*/Model.DB.Model.ssdl|res://*/Model.DB.Model.msl";
                    providerName = "MySql.Data.MySqlClient";
                    port = "";
                    serverName = "";
                    databaseName = "";
                    userid = "";
                    password = "";
                    break;
            }
            string connstring = $"metadata={metadata};provider={providerName};provider connection string=\"server={serverName};user id={userid};password={password};persistsecurityinfo=True;database={databaseName};port={port}\"";
            return connstring;
        }
    }
}
